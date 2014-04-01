using System;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Globalization;
using Ortoped.GarpGEM;
using Ortoped.HelpClasses;
using Ortoped.Definitions;
using Ortoped.Dialogs;
using Excido;
using Ortoped.Thord;
using GCS;
using System.ServiceModel;
using Ortoped.Definitions;

namespace Ortoped.GarpFunctions
{
    /// <summary>
    /// Summary description for OrderRowFunc.
    /// </summary>
    public class OrderRowFunc : ProductionService.IOrderServiceCallback
    {
        private OrderRowCOM mOR;
        private ProductCOM oProduct = new ProductCOM();
        private SaleCOM oSale = new SaleCOM();
        private OrderRowText orText = new OrderRowText();
        private string mErrorMsg = "";
        private GarpReferrals garpref;
        private ThordFunctions thordfunc;
        private orderFunc oOH = new orderFunc();
        private Ortoped.ProductionService.OrderServiceClient mProdClient;
        public HelpClasses.Table oProdStat = new HelpClasses.Table("1R");

        public OrderRowFunc()
        {
           
            if (Config.IsThordUser)
            {
                garpref = new GarpReferrals();
                thordfunc = new ThordFunctions();
            }

            try
            {
                mOR = new OrderRowCOM();
            }
            catch { }

            try
            {
                InstanceContext context = new InstanceContext(this);
                mProdClient = new ProductionService.OrderServiceClient(context);
            }
            catch(Exception e)
            {
                Logger.loggError(e, "Error creating OrderServiceClient", "", "OrderRowFunc()");
            }
        }

        public enum CreditType { OnlyPatient = 0, OnlyInvoiceCustomer = 1, Both = 2 };

        public struct Product
        {
            public string ProductNr;
            public string ProductName;
            public string Apris;

            public static Product[] convertFromArray(ArrayList arr)
            {
                Product[] pr = new Product[arr.Count];

                for (int i = 0; i < arr.Count; i++)
                {
                    string[] s = arr[i].ToString().Split(";".ToCharArray(), 5);
                    pr[i].ProductNr = s[0];
                    pr[i].ProductName = s[1];
                    pr[i].Apris = s[2];
                }
                return pr;
            }

            public static ListViewItem[] convertToListView(Product[] pr)
            {
                ArrayList al = new ArrayList();
                ListViewItem[] lw = new ListViewItem[pr.Length];

                for (int i = 0; i < lw.Length; i++)
                {
                    lw[i] = new ListViewItem(pr[i].ProductNr);
                    lw[i].SubItems.Add(pr[i].ProductName);
                }

                return lw;
            }

        }

        public Definitions.Aid getAid(string onr, string aidid, bool includeOwnFeeRow)
        {
            return mOR.getAid(onr, aidid, includeOwnFeeRow);
        }

        public List<Definitions.Aid> getAllAid(string onr)
        {
            return mOR.getAllAid(onr);
        }

        /// <summary>
        /// Kontrollerar att  hjälpmedelt har en rad satt som viewinlist, om inte
        /// så sätts första raden till att vara viewinlist
        /// 
        /// Returnerar "true" om en viewstat har satts (inte fanns)
        /// </summary>
        /// <param name="onr"></param>
        /// <param name="?"></param>
        /// <param name="?"></param>
        private bool checkViewStatOnAid(string onr, string aidid)
        {
            int foundIdx = 0;
            Definitions.Aid aid = mOR.getAid(onr, aidid, false);

            // If getAllRows returned something (can return nothing if only
            // an EgenAvgift exists on aidid)
            if (aid.OrderRows.Count > 0)
            {
                for (int i = 0; i < aid.OrderRows.Count; i++)
                {
                    if (getViewState(aid.OrderRows[i].OrderNo, aid.OrderRows[i].Rad))
                    {
                        foundIdx = i;
                        break;
                    }
                }
                if (foundIdx > 0)
                {
                    setAsViewInList(aid.OrderRows[foundIdx].OrderNo, aid.OrderRows[foundIdx].AidNr, aid.OrderRows[foundIdx].Rad);
                    return true;
                }
                else
                {
                    setAsViewInList(aid.OrderRows[0].OrderNo, aid.OrderRows[0].AidNr, aid.OrderRows[0].Rad);
                    return false;
                }
            }

            return false;
        }

        //public OrderRowDefinitions.OrderRow[] getAllRows(string onr)
        //{
        //    ArrayList s = new ArrayList();

        //    if (mOR.findFirstRow(onr))
        //    {
        //        do
        //        {
        //            s.Add(getRow(onr, mOR.RDC));
        //        }
        //        while (!mOR.nextRow());
        //    }
        //    return OrderRowDefinitions.OrderRow.convertFromArray(s);
        //}

        /// <summary>
        /// Returnerar alla rader på et hjälpmedel, includerat egenavgifter
        /// </summary>
        /// <param name="onr"></param>
        /// <param name="aid"></param>
        /// <returns></returns>
        public OrderRowDefinitions.OrderRow[] getAllRowsOwnFeeIncluded(string onr, string aidid)
        {
            Definitions.Aid aid = mOR.getAid(onr, aidid, true);
            return aid.OrderRows.ToArray();
        }

        public OrderRowDefinitions.OwnFee getPatientsOwnFeeOrderNo(string onr, string aid)
        {
            OrderRowDefinitions.OwnFee[] ownfees = mOR.getAllOwnFee(onr, aid);
            OrderRowDefinitions.OwnFee ownfee = new OrderRowDefinitions.OwnFee();

            if (ownfees.Length > 1)
            {
                frmOwnFeeList oOfl = new frmOwnFeeList();
                oOfl.ownfees = ownfees;
                oOfl.ShowDialog();
                ownfee = oOfl.selectedOwnFee;
                oOfl.Dispose();
                GC.Collect();
            }
            else
                if (ownfees.Length == 1)
                    ownfee = ownfees[0];

            return ownfee;
        }


        public bool creditAid(string onr, string aid, CreditType ctor)
        {
            OrderRowDefinitions.OwnFee ownfee = new OrderRowDefinitions.OwnFee();
            bool bOK = false;

            switch (ctor)
            {
                case CreditType.OnlyPatient:
                    ownfee = getPatientsOwnFeeOrderNo(onr, aid);
                    if (ownfee.OrderNo != null)
                    {
                        OwnFee of = new OwnFee();
                        OwnFee.OwnFeeResult ofr = of.creditRowOnPatientsOrder(ownfee.PatientsOrderNo, ownfee.PatientsRowNo);
                        orText.addOwnFeeConnection(ofr.OrderNo, ofr.OrderRow, onr.PadRight(6) + " - " + aid + " (hjälpmedelsid)");

                        try
                        {
                            if (ECS.noNULL(ownfee.PaymentTerms).Equals(Config.BetVillkEaKont))
                            {
                                //Config con = new Config(mBolag);
                                string[] s = Config.getReceipt().Split('-');
                                string mDokGen = s[0];
                                string mReport = s[1];
                                printInvoice(s[0], s[1], ofr.FsNo);
                            }
                            else
                            {
                                //Config con = new Config(mBolag);
                                string[] s = Config.getInvoice().Split('-');
                                string mDokGen = s[0];
                                string mReport = s[1];
                                printInvoice(s[0], s[1], ofr.FsNo);
                            }
                        }
                        catch (Exception ex)
                        {
                            Log4Net.Logger.loggError(ex, "Problem when printing after creation of credit on OnlyPatient", Config.User, "");
                        }

                        bOK = true;
                    }
                    break;
                case CreditType.OnlyInvoiceCustomer:
                    creditAidOnOriginalOrder(onr, aid);
                    bOK = true;
                    break;
                case CreditType.Both:
                    ownfee = getPatientsOwnFeeOrderNo(onr, aid);
                    if (ownfee.OrderNo != null)
                    {
                        OwnFee of = new OwnFee();
                        OwnFee.OwnFeeResult ofr = of.creditRowOnPatientsOrder(ownfee.PatientsOrderNo, ownfee.PatientsRowNo);
                        orText.addOwnFeeConnection(ofr.OrderNo, ofr.OrderRow, onr.PadRight(6) + " - " + aid + " (hjälpmedelsid)");

                        try
                        {
                            if (ECS.noNULL(ownfee.PaymentTerms).Equals(Config.BetVillkEaKont))
                            {
                                //Config con = new Config(mBolag);
                                string[] s = Config.getReceipt().Split('-');
                                string mDokGen = s[0];
                                string mReport = s[1];
                                printInvoice(s[0], s[1], ofr.FsNo);
                            }
                            else
                            {
                                //Config con = new Config(mBolag);
                                string[] s = Config.getInvoice().Split('-');
                                string mDokGen = s[0];
                                string mReport = s[1];
                                printInvoice(s[0], s[1], ofr.FsNo);
                            }
                        }
                        catch (Exception ex)
                        {
                            Log4Net.Logger.loggError(ex, "Problem when printing after creation of credit on Both", Config.User, "");
                        }

                        bOK = true;
                        creditAidOnOriginalOrder(onr, aid);
                    }
                    break;
                default:
                    break;
            }
            return bOK;
        }

        private void creditAidOnOriginalOrder(string onr, string aid)
        {
            bool bIsDelivered;

            OrderRowDefinitions.OrderRow[] or = getAllRowsOwnFeeIncluded(onr, aid);

            for (int i = 0; i < or.Length; i++)
            {
                if (or[i].Levstatus.Equals("5"))
                    bIsDelivered = true;
                else
                    bIsDelivered = false;

                or[i].Rad = addNewRow(onr, aid, or[i].Artikel);

                // Om beloppsrad så är det en egenavgift
                if (or[i].Beloppsrad)
                {
                    try
                    {
                        or[i].APris = ECS.doubleToString(ECS.stringToDouble(or[i].APris) * -1, '.');
                    }
                    catch
                    {
                        or[i].APris = "0";
                    }
                }
                else
                    or[i].Antal = "-" + or[i].Antal;

                or[i].ViewInList = false;

                saveOrderRow(or[i], true, false);
                saveOrderRow(or[i], true, false);
                if (bIsDelivered)
                    mOR.deliverRow(or[i].OrderNo, or[i].Rad, or[i].Antal, "", "");
            }
        }

        /// <summary>
        /// Levererar ett helt hjälpmedel på samma följesedelsnummer
        /// 
        /// </summary>
        /// <param name="or">OrderRow</param>
        /// <param name="bvk">Paymentterms that should be used</param>
        /// <returns>Följesedelsnummer som leveransen utfördes på</returns>
        public string deliverAid(OrderRowDefinitions.OrderRow[] or, string bvk, bool checkdate)
        {
            string sFSNr = "", levdate = "";

            // Hämta leveranstid från första raden som inte är en EA, detta för att det inte skall finnas 
            // någon chans att det blir olka levtider på ett hjälpmedelsid
            if (or.Length > 0)
            {
                foreach(OrderRowDefinitions.OrderRow r in or)
                {
                    if (!mOR.isEgenAvgift(r.Artikel))
                    {
                        levdate = ECS.noNULL(r.LevTid);
                        break;
                    }
                }
                
            }

            // If no check on date should be done and date ar "", set today as date
            if (!checkdate && levdate.Equals(""))
                levdate = DateTime.Today.ToString("yyMMdd");

            // Leverara bara om det finns en leveranstid
            if (!levdate.Trim().Equals(""))
            {
                sFSNr = mOR._deliverAid(or, levdate, bvk);
                
                try
                {
                    or[0].Prodstatus = oProdStat.getTx1ByKey("1");
                    or[0].LevTid = levdate;
                    saveOrderRow(or[0], true, true);
                }
                catch { }
            }
            return sFSNr;
        }

        public string deliverOwnFeeRow(string onr, string row, string antal, string datum, string fsnr)
        {
            return mOR.deliverRow(onr, row, antal, datum, fsnr);
        }

        public OrderRowDefinitions.OrderRow getRow(string onr, string row)
        {
            return mOR.getRow(onr, row);
        }

        /// <summary>
        /// Spara orderrad
        /// 
        /// </summary>
        /// <param name="or"></param>
        /// <returns></returns>
        public bool saveOrderRow(OrderRowDefinitions.OrderRow or, bool saveCommonData, bool saveToThord)
        {
            // Avbryt om ordern är stängd
            if (!isOrderOpen(or.OrderNo) || or.Rad.Equals(""))
                return false;

            // Finns inte artikelnummer, radera raden och avbryt men returnera ändå
            // true då det inte skall genereas något felmeddelande
            if (or.Artikel.Equals(""))
            {
                removeRow(or.OrderNo, or.Rad);
                return true;
            }

            if (!mOR.findRow(or.OrderNo, or.Rad))
                or = mOR.doInsert(or.OrderNo, or.AidNr, or.Artikel);

            mOR.doPost(or, saveCommonData);

            // && !or.RemissNo.Equals("")
            if (Config.IsThordUser && saveToThord && !or.Warrenty)
            {
                if (!ECS.noNULL(or.Thord_NeedStep).Trim().Equals(""))
                    thordfunc.sendReferral(garpref.getReferral(or.OrderNo, or.AidNr));
                else
                {
                    //MessageBox.Show("Du måste ange en behovstrappa för att spara till Thord.", "Behovstrappa saknas");
                    Exception e = new Exception("Du måste ange en behovstrappa för att spara till Thord.");
                    e.Source = "Behovstrappa";
                    throw e;
                }
            }
            
            saveToProduction(or);

            return true;
        }

        ///// <summary>
        ///// Saves information that is common for all rows on a AidId
        ///// 
        ///// </summary>
        //private void saveCommonOrderRow(ref OrderRowDefinitions.OrderRow or)
        //{
        //    OrderRowCOM orc = new OrderRowCOM();

        //    if (orc.findFirstRow(or.OrderNo))
        //    {
        //        do
        //        {
        //            if ((orc.AidID == or.AidNr) && (!orc.isEgenAvgift))
        //            {
        //                fillCommonOrderRowFields(ref or);
        //            }
        //        }
        //        while (!orc.nextRow());
        //    }
        //}

        private void saveToProduction(OrderRowDefinitions.OrderRow or)
        {
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                try
                {
                    ProductionService.ProductionAidDTO dto = new ProductionService.ProductionAidDTO();

                    dto.OrderNo = or.OrderNo;
                    dto.AidId = int.Parse(or.AidNr);
                    dto.Box = oProdStat.getIdByTX1(or.Prodstatus);
                    dto.ArtNo = or.Artikel;
                    dto.ArtName = oProduct.getNameById(or.Artikel);
                    dto.CompanyId = Config.CompanyId;
                    dto.Urgent = or.Urgent;
                    dto.Title = or.ProductionTitle;
                    dto.PromisedDeliverDate = or.PromisedDeliverDate;
                    dto.ConditionDate = or.ConditionDate;

                    mProdClient.UpdateAid(dto);

                }
                catch (Exception e)
                {
                    Logger.loggError(e, "Error updating ProductionService", "", "saveToProduction(OrderRowDefinitions.OrderRow or)");
                }
            });

        }

        /// <summary>
        /// Spara orderradstexter
        /// 
        /// </summary>
        /// <param name="onr"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool saveOrderRowTexts(string onr, string row, string text)
        {
            // Avbryt om ordern är stängd
            if (!isOrderOpen(onr) || row.Equals(""))
                return false;

            orText.saveText(onr, row, text);
            return true;
        }


        /// <summary>
        /// Hämte textrader för denna rad
        /// 
        /// </summary>
        /// <param name="onr"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public string getOrderRowTexts(string onr, string row)
        {
            return orText.getTexts(onr, row);
        }


        //public string getAidsTexts(string onr, string aidid)
        //{
        //    Definitions.Aid aid = mOR.getAid(onr, aidid, false);

        //    return orText.getAidsText(aid.Product.OrderNo, aid.Product.Rad);
        //}

        public void saveAidsTexts(string onr, string aidid, string text)
        {
            Definitions.Aid aid = mOR.getAid(onr, aidid, false);
            orText.saveAidsText(onr, aid.Product.Rad, text);
        }

        public bool removeRow(string onr, string row)
        {
            OrderRowDefinitions.OrderRow delRow = new OrderRowDefinitions.OrderRow();
            Referral r = new Referral();

            // Avbryt om ordern är stängd
            if (!isOrderOpen(onr))
                return false;

            delRow = getRow(onr, row);

            // Only if Thord user
            if (Config.IsThordUser)
            {
                r = garpref.getReferralForRemovalOfPart(delRow.OrderNo, delRow.AidNr, row);
            }

            // Om denna rad är den som visas i översikten så visa en annan rad istället.
            if (getViewState(onr, row))
            {
                //string id = mOR.AidID;
                mOR.doDelete(onr, row);
                OrderRowDefinitions.OrderRow[] or = mOR.getAid(onr, delRow.AidNr, false).OrderRows.ToArray(); ;
                if (or.Length > 0)
                    setAsViewInList(or[0].OrderNo, or[0].AidNr, or[0].Rad);
            }
            else
                mOR.doDelete(onr, row);

            // Only if Thord user
            if (Config.IsThordUser)
            {
                if (delRow.CreatedInThord && !r.remissid.Equals(""))
                {
                    thordfunc.sendReferral(r);
                }
            }

            return true;
        }

        /// <summary>
        /// Loppar igenom alla rader och ändrar orderrad som har ViewInList
        /// på AidID enligt parameter
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="row"></param>
        public void setAsViewInList(string onr, string aidid, string row)
        {
            Definitions.Aid aid = mOR.getAid(onr, aidid, false);

            foreach (OrderRowDefinitions.OrderRow o in aid.OrderRows)
            {
                // Är det raden som skall sättas? Alla andra sätt till false
                if (o.Rad.Trim().Equals(row.Trim()))
                    mOR.setViewStat(o.OrderNo, o.Rad, true);
                else
                    mOR.setViewStat(o.OrderNo, o.Rad, false);
            }
        }

        public bool getViewState(string onr, string row)
        {
            return mOR.getViewStat(onr, row);
        }


        public OrderRowDefinitions.OrderRow addNewAid(string onr)
        {
            OrderRowDefinitions.OrderRow or = new OrderRowDefinitions.OrderRow();
            string s = "";

            // Avbryt om ordern är stängd
            if (!isOrderOpen(onr))
                return or;

            s = mOR.getNextAidId(onr);

            return mOR.doInsert(onr, s, "");
        }

        /// <summary>
        /// Lägger till ny rad och returnerar radnummer
        /// </summary>
        /// <param name="onr"></param>
        /// <param name="aid"></param>
        /// <returns></returns>
        public string addNewRow(string onr, string aid, string anr)
        {
            // Avbryt om ordern är stängd
            if (!isOrderOpen(onr))
                return null;

            return mOR.doInsert(onr, aid, anr).Rad;
        }

        public Product[] findProductByName(string name)
        {
            ArrayList arr = new ArrayList();

            if (!oProduct.findByName(name))
                oProduct.next();

            while ((!oProduct.EOF) && (oProduct.BEN.ToLower().StartsWith(name.ToLower())))
            {
                arr.Add(oProduct.ANR + ";" + oProduct.BEN + ";" + oProduct.PRI);
                oProduct.next();
            }
            return Product.convertFromArray(arr);
        }

        public Product[] findProductByID(string id)
        {
            Product[] pr = new Product[1];

            if (oProduct.findByID(id))
            {
                fillProduct(ref pr[0]);
            }
            return pr;
        }

        public string[] getListOfSalesman()
        {
            return oSale.getListOfAllHandler(Config.Kst);
        }

        /// <summary>
        /// Skriver faktura, idx är index i dokumentet (skall vara följesedelsnummer)
        /// 
        /// </summary>
        /// <param name="dokgen"></param>
        /// <param name="dok"></param>
        /// <param name="idx"></param>
        public void printInvoice(string dokgen, string dok, string idx)
        {
            try
            {
                mOR.runReport(dokgen, dok, idx);
            }
            catch
            {
                MessageBox.Show(null, "Utskrift av Faktura/kvitto genomfördes ej.\nTrolig orsak är att dokument " + dokgen + "-" + dok + " inte finns ", "Fel vid utskrift", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        /// <summary>
        ///  Uppdaterar ett hjälpmedel med ärendeinformation (en textrad per orderrad i hjälpmedlet)
        ///  
        /// </summary>
        /// <param name="onr"></param>
        /// <param name="aidid"></param>
        /// <param name="errandid"></param>
        /// <param name="date"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        public void updateAidWithErrand(string onr, string aidid, string errandid, string date, string start, string stop)
        {
            string s = errandid.PadRight(7) + date.PadRight(8) + start.PadRight(8) + stop, sid = "";
            Definitions.Aid aid = mOR.getAid(onr, aidid, false);

            foreach (Definitions.OrderRowDefinitions.OrderRow or in aid.OrderRows)
            {
                // Om 255 returneras hittades ingen befintlig rad och en ny läggs upp
                sid = orText.findErrand(onr, or.Rad, errandid);
                if (sid.Equals("255"))

                    orText.addErrandText(onr, or.Rad, s);
                else
                    orText.updateErrandText(onr, or.Rad, sid, s);
            }
        }

        public new void updateOid(string onr, string row, string aidoid, string partoid)
        {
            mOR.updateOid(onr, row, aidoid, partoid);
        }

        private void fillProduct(ref Product pr)
        {
            pr.ProductNr = oProduct.ANR;
            pr.ProductName = oProduct.BEN;
            pr.Apris = GCF.noNULL(oProduct.PRI) == "" ? "0" : oProduct.PRI;
        }

        /// <summary>
        /// Kontrollerar om ordern är fryst. Är ordern fryst kan
        /// meddelande hämtas med getErrorMsg
        /// </summary>
        /// <param name="onr"></param>
        /// <returns></returns>
        public bool isOrderOpen(string onr)
        {
            // Om OH är fryst, avbryt
            mOR.findOH(onr);
            if (mOR.isClosed(onr))
            {
                mErrorMsg = "Order " + onr.Trim() + " är stängd";
                return false;
            }
            else
            {
                mErrorMsg = "";
                return true;
            }
        }

        public string getErrorMsg()
        {
            return mErrorMsg;
        }

        public void OnMessageAdded(ProductionService.ProductionAidDTO order, DateTime timestamp)
        {
            
        }

        public void openMatPlan()
        {
            mOR.selectMenuItem(530);
        }
    }
}

