using System;
using System.Collections;
using System.Collections.Generic;
using Ortoped;
using Ortoped.HelpClasses;
using Ortoped.Definitions;
using GCS;
using System.Globalization;

namespace Ortoped.GarpGEM
{
    /// <summary>
    /// Summary description for OrderRowCOM.
    /// </summary>
    public class OrderRowCOM : GarpGEM.GarpCOM
    {
        private OrderRowText oText = new OrderRowText();
        private string mInvoiceNo, mInvoiceDate, mDeliverDate;
        private GarpGenericDB mOGR, mOGR2, mOGR_Search, mOGA, mAGA, mAGT, mHKA, mHKR, mKR;
        private DeliveryMode oDelM = new DeliveryMode();
        private ProductCOM oProduct;
        private OrderRowText oOrText;
        private OrderRowText orText = new OrderRowText();
        private HelpClasses.Table oProdStat = new HelpClasses.Table("1R");

        public OrderRowCOM()
        {
            mOGR = new GarpGenericDB("OGR");
            mOGR2 = new GarpGenericDB("OGR2");
            mOGA = new GarpGenericDB("OGA");
            mAGA = new GarpGenericDB("AGA");
            mAGT = new GarpGenericDB("AGT");
            mHKA = new GarpGenericDB("HKA");
            mHKR = new GarpGenericDB("HKR");
            mKR = new GarpGenericDB("KR");
            mOGR_Search = new GarpGenericDB("OGR");
            oProduct = new ProductCOM();
            oOrText = new OrderRowText();

            mHKA.index = 2;
        }


        #region NewFunctions

        public List<Aid> getAllAid(string onr)
        {
            List<Aid> lst = new List<Aid>();
            Aid aid;

            mOGR.find(onr);
            mOGR.next();

            while (mOGR.getValue("ONR").Equals(onr) && !mOGR.EOF)
            {
                OrderRowDefinitions.OrderRow or = fillOrderRow(ref mOGR);

                // Check if we alreade have aid in list
                if (lst.Exists(o => o.AidNr == or.AidNr))
                    aid = lst.Find(o => o.AidNr == or.AidNr);
                else
                {
                    aid = new Aid();
                    aid.OrderRows = new List<OrderRowDefinitions.OrderRow>();
                    aid.AidNr = or.AidNr;

                    if (mOGA.find(onr))
                        aid.PaymentTerms = mOGA.getValue("BVK");

                    lst.Add(aid);
                }

                if (!isEgenAvgift(mOGR.getValue("ANR")))
                {
                    if (or.ViewInList)
                    {
                        aid.Product = or;
                        aid.Text = oOrText.getAidsText(or.OrderNo, or.Rad);
                    }

                    try
                    {
                        Log4Net.Logger.loggInfo("STEP 9.1.01", "", "");

                        if (isInternalProduct(or.Artikel))
                            aid.SumInternalProducts += Convert.ToDouble(or.APris.Replace(".", ",")) * Convert.ToDouble(or.Antal.Replace(".", ","));
                        else
                            aid.SumExternalProducts += Convert.ToDouble(or.APris.Replace(".", ",")) * Convert.ToDouble(or.Antal.Replace(".", ","));
                    }
                    catch (Exception e)
                    {
                        Log4Net.Logger.loggError(e, "Error in getAid while determine internal or external product", "", "");
                    }

                    aid.OrderRows.Add(or);
                }
                else
                {
                    try
                    {
                        aid.OwnFee += Convert.ToDouble(or.APris.Replace(".", ","));
                    }
                    catch (Exception e)
                    {
                        aid.OwnFee += 0;
                        Log4Net.Logger.loggError(e, "Error in getAid while determine internal or external product", "", "");
                    }
                }

                mOGR.next();     
            }
            

            foreach (Aid a in lst)
            {
                if (GCF.noNULL(a.Product.OrderNo).Equals(""))
                {
                    try
                    {
                        a.Product = a.OrderRows[0];
                    }
                    catch { }
                }
            }
            return lst;
        }



        public Aid getAid(string onr, string aidid, bool includeOwnFeeRow)
        {
            ArrayList s = new ArrayList();
            Aid aid = new Aid();
            aid.OrderRows = new System.Collections.Generic.List<OrderRowDefinitions.OrderRow>();
            aid.AidNr = aidid;

            mOGR.find(onr);
            mOGR.next();

            if (mOGA.find(onr))
                aid.PaymentTerms = mOGA.getValue("BVK");

            aid.OrderNo = mOGR.getValue("ONR");
            
            while (mOGR.getValue("ONR").Equals(onr) && !mOGR.EOF)
            {
                OrderRowDefinitions.OrderRow or = fillOrderRow(ref mOGR);
                
 
                if ((or.AidNr == aidid.Trim()))
                {
                    if (!isEgenAvgift(mOGR.getValue("ANR")))
                    {
                        if (or.ViewInList)
                        {
                            aid.Product = or;
                            aid.Text = oOrText.getAidsText(or.OrderNo, or.Rad);
                        }

                        try
                        {
                            if (isInternalProduct(or.Artikel))
                                aid.SumInternalProducts += Convert.ToDouble(or.APris.Replace(".", ",")) * Convert.ToDouble(or.Antal.Replace(".", ","));
                            else
                                aid.SumExternalProducts += Convert.ToDouble(or.APris.Replace(".", ",")) * Convert.ToDouble(or.Antal.Replace(".", ","));
                        }
                        catch (Exception e)
                        {
                            Log4Net.Logger.loggError(e, "Error in getAid while determine internal or external product", "", "");

                        }

                        aid.OrderRows.Add(or);
                    }
                    else
                    {
                        try
                        {
                            aid.OwnFee += Convert.ToDouble(or.APris.Replace(".", ","));

                        }
                        catch (Exception e)
                        {
                            aid.OwnFee += 0;
                            Log4Net.Logger.loggError(e, "Error in getAid while determine internal or external product", "", "");
                        }

                        if (includeOwnFeeRow)
                            aid.OrderRows.Add(or);

                    }
                }

                mOGR.next();
            }

            if (GCF.noNULL(aid.Product.OrderNo).Equals(""))
            {
                try
                {
                    aid.Product = aid.OrderRows[0];
                }
                catch { }
            }

            return aid;
        }

        public bool isEgenAvgift(string anr)
        {
            if (mAGA.find(anr))
            {
                if (mAGA.getValue("KD1").Equals("E"))
                    return true;
                else
                    return false;
            }
            else return false;
        }

        /// <summary>
        /// Skall denna artikel visas i översiktslistan. Detta värde sparas
        /// i X1F flaggan.
        /// </summary>
        public bool getViewStat(string onr, string row)
        {
            try
            {
                if (mOGR_Search.find(onr.PadRight(6) + row.PadLeft(3)))
                {
                    if (mOGR_Search.getValue("X1F").Equals("x"))
                        return true;
                    else
                        return false;
                }
            }
            catch { return false; }

            return false;
            //set
            //{
            //    if (value)
            //        mOGR_Search.setValue("X1F", "x");
            //    else
            //        mOGR_Search.setValue("X1F", "0");
            //}
        }

        public void setViewStat(string onr, string row, bool value)
        {
            try
            {
                if (mOGR.find(onr.PadRight(6) + row.PadLeft(3)))
                {
                    if (value)
                        mOGR.setValue("X1F", "x");
                    else
                        mOGR.setValue("X1F", "0");

                    mOGR.post();
                }
            }
            catch { }
        }

        public bool getWarranty(string id)
        {
            if (mOGR.getValue("RBK").Equals("G"))
                return true;
            else
                return false;
        }

        public void setWarranty(bool value, ref GarpGenericDB data)
        {
            if (value)
            {
                data.setValue("RAB", "-100");
                data.setValue("RBK", "G");
            }
            else
            {
                data.setValue("RAB", "");
                data.setValue("RBK", "");
            }
        }

        public string getFormatedAidDate(string date)
        {
                
            if (date.IndexOf(".") > 0)
                date = date.Split('.')[0];

            if (double.Parse(date) == 0)
                return DateTime.Now.ToString("yyMMdd");
            else
                return double.Parse(date.Substring(1)).ToString().PadLeft(6, '0');
        }

        //public string getDeliverMode(string onr)
        //{
        //    if (mOGA.find(onr))
        //    {
        //        if (GCF.noNULL(mOGA.getValue("LSE")).Equals(""))
        //            return oDelM.getNameByKey(GCF.noNULL(mOGA.getValue("LSE")));
        //        else
        //            return "";
        //    }
        //    else
        //        return "";
        //}

        public bool isClosed(string onr)
        {
            if (mOGA.find(onr))
            {
                if (mOGA.getValue("FRY").Equals("z"))
                    return true;
                else
                    return false;
            }
            else
                return true;
        }

#endregion



        public OrderRowDefinitions.OrderRow doInsert(string onr, string aidId, string artnr)
        {
            string srow;

            mOGR.find(onr.PadRight(6) + "254");
            mOGR.prev();

            if (mOGR.getValue("ONR").Trim() == onr.Trim())
            {
                srow = Convert.ToString(Convert.ToInt32(mOGR.getValue("RDC")) + 1);
            }
            else
            {
                if (mOGR.find(onr.PadRight(6) + "  1"))
                    srow = "2";
                else
                    srow = "1";
            }

            try
            {
                mOGR.insert();
                mOGR.setValue("ONR",onr);
                mOGR.setValue("RDC",srow);
                mOGR.setValue("OSE","K");
                mOGR.setValue("ANR",artnr);
                mOGR.post();
                mOGR.next();

                if (mOGR.find(onr.PadRight(6) + srow.PadLeft(3)))
                {
                    int ade = 0;

                    try
                    {
                        ade = GCF.noNULL(mOGR.getValue("ADE")) == "" ? 0 : int.Parse(GCF.noNULL(mOGR.getValue("ADE")));
                    }
                    catch { ade = 0; }

                    if (ade > 0)
                        mOGR.setValue("NX1", aidId.TrimStart('0') + ".".PadRight(ade + 1, '0'));
                    else
                        mOGR.setValue("NX1", aidId);

                    mOGR.setValue("LDT", "");           // JB: 2012-09-17 We want to disconnect this value from OH BDT
                }

                mOGR.post();

                if (!mOGR2.find(mOGR.getValue("ONR").PadRight(6) + mOGR.getValue("RDC").PadLeft(2)))
                {
                    createOGR2Row(mOGR.getValue("ONR"), mOGR.getValue("RDC"));
                }

                return fillOrderRow(ref mOGR);
            }
            catch
            {
                return new OrderRowDefinitions.OrderRow();
            }
        }

        public void doPost(OrderRowDefinitions.OrderRow or, bool saveCommonData)
        {
            try
            {
                fillGarpTableFields(or, ref mOGR);

                mOGR.post();
                mOGR2.post();

                // Saves common data
                if (saveCommonData && !isEgenAvgift(or.Artikel))
                    saveCommonOrderRow(or, ref mOGR);

            }
            catch { }
        }

        /// <summary>
        /// Saves information that is common for all rows on a AidId
        /// 
        /// </summary>
        private void saveCommonOrderRow(OrderRowDefinitions.OrderRow or, ref GarpGenericDB data)
        {
            data.find(or.OrderNo);
            data.next();

            while (data.getValue("ONR").Equals(or.OrderNo) && !data.EOF)
            {
                if ((getFormatedAidId(data.getValue("NX1")) == or.AidNr) && (!isEgenAvgift(data.getValue("ANR"))))
                {
                    fillCommonOrderRowFields(or, data);
                }
                data.next();
            }
        }

        public void doDelete(string onr, string row)
        {
            if (mOGR.find(onr.PadRight(6) + row) & mOGR.getValue("LVF").Equals("0"))
            {
                mOGR.delete();
                if (mOGR2.find(onr.PadRight(6) + row.PadLeft(3)))
                    mOGR2.delete();
            }
        }

        /// <summary>
        /// Hitta raden
        /// </summary>
        /// <param name="onr"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool findRow(string onr, string row)
        {
            string s1 = onr.PadRight(6);
            string s2 = row.PadLeft(3);
            string s3 = s1 + s2;
            mOGR.find(s1 + "  2");
            bool b = mOGR.find(s1 + s2);

            if (b)
            {
                // Om OGR2 inte finns, skapa posten
                if (!mOGR2.find(mOGR.getValue("ONR").PadRight(6) + mOGR.getValue("RDC").PadLeft(3)))
                    createOGR2Row(mOGR.getValue("ONR"), mOGR.getValue("RDC"));
            }
            return b;
        }

        public OrderRowDefinitions.OrderRow getRow(string onr, string row)
        {
            if (mOGR.find(onr.PadRight(6) + row.PadLeft(3)))
                return fillOrderRow(ref mOGR);
            else
                return new OrderRowDefinitions.OrderRow();
        }

         public bool findOH(string onr)
        {
            return mOGA.find(onr);
        }

        public string getNextAidId(string onr)
        {
            int i = 0;

            mOGR.find(onr);
            mOGR.next();

            // Loopa igenom och spara det högsta hjälmedelsnumret som används
            while (mOGR.getValue("ONR").Trim().Equals(onr.Trim()) && !mOGR.EOF)
            {
                if (int.Parse(getFormatedAidId(mOGR.getValue("NX1"))) > i)
                    i = int.Parse(getFormatedAidId(mOGR.getValue("NX1")));

                mOGR.next();
            }

            i++;
            return i.ToString();
        }

        private void createOGR2Row(string onr, string row)
        {
            mOGR2.insert();
            mOGR2.setValue("ONR",onr);
            mOGR2.setValue("RDC",row);
            mOGR2.setValue("OSE","K");
            mOGR2.post();
        }

        public void fillFieldsFromOtherTables(string onr, string row)
        {
            mInvoiceNo = "";
            mInvoiceDate = "";
            mDeliverDate = "";

            mHKA.index = 2;
            mHKA.find(onr.PadRight(6));
            mHKA.next();

            try
            {
                while (mHKA.getValue("ONR").Trim().Equals(onr.Trim()) && !mHKA.EOF)
                {
                    if (mHKR.find(mHKA.getValue("HNR").PadRight(6) + "  1" + row.PadLeft(3)))
                    {
                        if (mHKR.getValue("ONR").Trim().Equals(onr.Trim()))
                        {
                            mDeliverDate = mHKR.getValue("LDT");
                            mInvoiceNo = mHKA.getValue("FNR");

                            if (mKR.find(app.Bolag + mHKA.getValue("FNR")))
                            {
                                mInvoiceDate = mKR.getValue("FAD");
                            }
                            break;
                        }
                    }
                    mHKA.next();
                }
            }
            catch { }
        }

        public void updateOid(string onr, string row, string aidoid, string partoid)
        {
            // Save status as updated in Thord
            try
            {
                if (mOGR.find(onr.PadRight(6) + row.PadLeft(3)))
                {
                    mOGR.setValue("X2F","1");
                    mOGR.post();
                }
            }
            catch (Exception ex)
            {
                Log4Net.Logger.loggError(ex, "THORD: Vid postning av OGR (UpdatedInThord)", Config.User, "OrderRowCOM.updateOid");
                throw new Exception("Hittade inte orderrad" + onr + " - " + row);
            }

            // Saves Thord unik values to row
            if (mOGR2.find(onr.PadRight(6) + row.PadLeft(3)))
            {
                try
                {
                    mOGR2.setValue("NU5",aidoid);
                    mOGR2.setValue("NU6",partoid);
                    mOGR2.post();
                }
                catch (Exception ex)
                {
                    Log4Net.Logger.loggError(ex, "THORD: Vid postning av OGR2 (aidoid)", Config.User, "OrderRowCOM.updateOid");
                    throw new Exception("Hittade inte orderrad" + onr + " - " + row);
                }

            }
            else
            {
                Log4Net.Logger.loggError(new Exception("THORD: Hittade inte orderrad"), "Hittade inte orderrad" + onr + " - " + row, Config.User, "");
                //        throw new Exception("Hittade inte orderrad" + onr + " - " + row);
            }
        }

        public OrderRowDefinitions.OwnFee[] getAllOwnFee(string onr, string aidid)
        {
            OrderRowDefinitions.OwnFee ord = new OrderRowDefinitions.OwnFee();
            ArrayList al = new ArrayList();
            Definitions.Aid aid = getAid(onr, aidid, true);

            foreach (Definitions.OrderRowDefinitions.OrderRow o in aid.OrderRows)
            {
                if (GCF.noNULL(o.Artikel).Equals("EA"))
                {
                    ord = new OrderRowDefinitions.OwnFee();
                    ord.AidId = o.AidNr;
                    ord.OrderNo = o.OrderNo;
                    ord.RowNo = o.Rad;
                    if (o.Levstatus.Equals("5"))
                        ord.Delivered = true;
                    else
                        ord.Delivered = false;
                    ord.Amount = GCF.stringToDouble(o.APris);
                    oText.findOwnFeeConnection(o.OrderNo, o.Rad, ref ord);
                    findOH(ord.PatientsOrderNo);
                    ord.PaymentTerms = aid.PaymentTerms;
                    al.Add(ord);
                }
            }

            return (OrderRowDefinitions.OwnFee[])al.ToArray(typeof(OrderRowDefinitions.OwnFee));
        }


        # region Propertys

        public string _deliverAid(OrderRowDefinitions.OrderRow[] or, string levdate, string bvk)
        {
            string sFSNr = "";
            HelpClasses.Table oProdStat = new HelpClasses.Table("1R");

            for (int i = 0; i < or.Length; i++)
            {
                // Fösta raden levereras på ny FS och de nästkommande på samma FS
                if (sFSNr.Equals(""))
                    sFSNr = deliverRow(or[i].OrderNo, or[i].Rad, or[i].Antal, levdate, "A");
                else
                    sFSNr = deliverRow(or[i].OrderNo, or[i].Rad, or[i].Antal, levdate, sFSNr);
            }

            // Set paymentterms on FS if bvk is not ""
            mHKA.index = 1;
            mHKA.find(sFSNr);
            mHKA.next();
            if (mHKA.getValue("HNR").Trim().Equals(sFSNr.Trim()) && !bvk.Equals(""))
            {
                mHKA.setValue("BVK",bvk);
                mHKA.post();
            }
            return sFSNr;
        }

        public string deliverRow(string onr, string row, string antal, string datum, string fsnr)
        {
            Garp.IOrderRowDeliver deliver = app.OrderRowDeliver;

            deliver.FSedel = fsnr;
            deliver.Ordernr = onr;
            deliver.Radnr = row;
            deliver.Antal = antal;
            deliver.Date = datum;
            deliver.Deliver();

            return deliver.FSedel;
        }

        private bool isInternalProduct(string anr)
        {
            try
            {
                // Function to only show price on external products, internal gets 0 i price
                if (mAGA.find(anr) && mAGT.find(anr))
                {
                    if (GCF.noNULL(mAGT.getValue("TX13")).Equals("2"))
                        return true;
                    else
                        return false;
                }
            }
            catch { return false; }
            
            return false;
        }

  


        public void runReport(string gen, string id, string idx)
        {
            GarpGenericDB o = new GarpGenericDB("OGR");

            try
            {
                Garp.IReport dokNotice = o.getApp.ReportGenerators.Item(gen).Reports.Item(id.Trim());
                dokNotice.RangeFrom = idx;
                dokNotice.RangeTo = idx;
                dokNotice.Run();
            }
            catch { }
        }

        private string getFormatedAidId(string id)
        {

            id = id == null ? "1" : id.Trim().Replace(".", ",");

            // Om det är ett decimaltal måste vi formatera detta för att få fram rätt AidID
            if (id.IndexOf(",") > -1)
            {
                // Det finns två varianter.
                // 1. Talet större än 0. Dett är alla tal från konverterad data. Numer är även patientöversikten
                //		anpassad till detta format. 
                //
                // 2. Talet är mindre än noll, AidID sparas då i decimaldelan av talet. Detta var standard i 
                //		begynnelsen av Patientöversikten, detta har nu ändrats på grund av att konverteringen
                //		inte var gjord på detta vis .
                //
                // 3. Talet är större än noll men är ändå en variant två. Detta kan ske när man har fyllt 
                //		decimaldelen med variant två tal (t ex 100 om ant.dec. är två).
                if (double.Parse(id) > 0)
                {
                    // Splitta strängen
                    string[] s2 = id.Split(',');

                    // Är det en variant tre tal
                    if (int.Parse(s2[1]) > 0)
                        id = s2[0] + s2[1];
                    else
                        id = s2[0];
                }
                else
                {
                    id = id.Split(',')[1];
                }
            }
            return (id.Trim().PadLeft(3, '0'));
        }
        #endregion

        private OrderRowDefinitions.OrderRow fillOrderRow(ref GarpGenericDB data)
        {
            mOGR2.find(data.getValue("ONR").PadRight(6) + data.getValue("RDC").PadLeft(3));

            OrderRowDefinitions.OrderRow or = new OrderRowDefinitions.OrderRow();
            fillFieldsFromOtherTables(data.getValue("ONR"), data.getValue("RDC"));

            or.OrderNo = data.getValue("ONR");
            or.Rad = data.getValue("RDC");
            or.AidNr = getFormatedAidId(data.getValue("NX1"));
            or.Artikel = data.getValue("ANR");
            or.ProductName = oProduct.getNameById(data.getValue("ANR"));
            or.Antal = data.getValue("ORA");

            if (isInternalProduct(or.Artikel))
                or.APris = mOGR.getValue("LVP");
            else
                or.APris = mOGR.getValue("PRI");

            or.LevTid = data.getValue("LDT");
            or.InkStat = data.getValue("INK");
            or.Enhet = data.getValue("ENH");
            or.AidDate = getFormatedAidDate(data.getValue("DIM"));
            or.Prodstatus = oProdStat.getTable(data.getValue("RES")).TX1;
            or.SelectedHandler = data.getValue("BNX");
            or.Levstatus = data.getValue("LVF");
            //or.DeliverMode = getDeliverMode(or.OrderNo);
            or.Text = orText.getTexts(or.OrderNo, or.Rad);
            or.AidsText = orText.getAidsText(or.OrderNo, or.Rad);
            or.ViewInList = mOGR.getValue("X1F").Equals("x") ? true : false;
            or.Warrenty = mOGR.getValue("RBK").Equals("G") ? true : false;
            or.Beloppsrad = mOGR.getValue("BRA").Equals("*") ? true : false;
            or.DeliverDate = GCF.noNULL(mDeliverDate);
            or.InvoiceNo = GCF.noNULL(mInvoiceNo);;
            or.InvoiceDate = GCF.noNULL(mInvoiceDate);;
            or.Thord_NeedStep = mOGR2.getValue("C2A");
            or.AidPriority = mOGR2.getValue("C2B");

            try
            {   
                or.AidOid = 0;
                int.TryParse(mOGR2.getValue("NU5"), out or.AidOid);
            }
            catch(Exception e)
            {
                or.AidOid = 0;
            }

            try
            {
                or.PartOid = 0;
                int.TryParse(mOGR2.getValue("NU6"), out or.PartOid);
            }
            catch (Exception e)
            {
                or.PartOid = 0;
            }

            or.CreatedInThord = data.getValue("X2F").Equals("1") ? true : false;
            or.FirstTimePatient = mOGR2.getValue("C1A").Equals("1") ? true : false;
            or.EA_ProductGroup = mOGR.getValue("EXT");
            or.ProductionTitle = mOGR2.getValue("C20") + mOGR2.getValue("C10");
            or.Urgent = mOGR2.getValue("C1B").Equals("1") ? true : false;
            or.Priority = mOGR2.getValue("C1C");

            string[] s = { "yyMMdd", "yyyyMMdd", "yyyy-MM-dd" };
            try
            {
                DateTime dt;
                DateTime.TryParseExact(mOGR2.getValue("C06"), s, new CultureInfo("sv-SE"), DateTimeStyles.AssumeLocal, out dt);

                if (dt.AddYears(50) < DateTime.Now)
                    or.PromisedDeliverDate = DateTime.Now;
                else
                    or.PromisedDeliverDate = dt;
            }
            catch { or.PromisedDeliverDate = DateTime.Now; }

            try
            {
                DateTime dt;
                DateTime.TryParseExact(mOGR2.getValue("C07").Trim(), s, new CultureInfo("sv-SE"), DateTimeStyles.AssumeLocal, out dt);

                if (dt.AddYears(50) < DateTime.Now)
                    or.ConditionDate = DateTime.Now;
                else
                    or.ConditionDate = dt;
            }
            catch { or.ConditionDate = DateTime.Now; }

            or.Holder = mOGR2.getValue("C05") == "" ? null : mOGR2.getValue("C05");
            return or;
        }

        private void fillGarpTableFields(OrderRowDefinitions.OrderRow or, ref GarpGenericDB data)
        {
            data.setValue("ANR", or.Artikel);
            data.setValue("ORA", or.Antal.Replace(",", "."));


            if (isInternalProduct(or.Artikel))
            {
                if (GCF.noNULL(or.APris).Equals(""))
                    mOGR.setValue("LVP", "0");
                else
                    mOGR.setValue("LVP", or.APris.Replace(",", "."));

                mOGR.setValue("LPF", "F");

                // PRI is always zero on internal products
                mOGR.setValue("PRI", "0");
            }
            else
            {
                if (GCF.noNULL(or.APris).Equals(""))
                    mOGR.setValue("PRI", "0");
                else
                    mOGR.setValue("PRI", or.APris.Replace(",", "."));
            }

            //data.setValue("PRI", or.APris);
            
            try
            {
                if (!GCF.noNULL(or.LevTid).Equals(""))
                    data.setValue("LDT", or.LevTid);
            }
            catch { }

            
            if (GCF.noNULL(or.AidDate).Trim() != "")
                mOGR.setValue("DIM", "1" + or.AidDate);
            else
                mOGR.setValue("DIM", GCF.noNULL(or.AidDate));

            data.setValue("RES", oProdStat.getIdByTX1(or.Prodstatus));
            data.setValue("BNX", or.SelectedHandler);
            data.setValue("INK", or.InkStat);

            if (or.ViewInList)
                data.setValue("X1F", "x");
            else
                data.setValue("X1F", "0");

            if (or.Warrenty)
            {
                data.setValue("RAB", "-100");
                data.setValue("RBK", "G");
            }
            else
            {
                data.setValue("RAB", "");
                data.setValue("RBK", "");
            }

            if (or.Beloppsrad)
                data.setValue("BRA", "*");
            else
                data.setValue("BRA", "");


            mOGR2.setValue("C2A", or.Thord_NeedStep);
            mOGR2.setValue("NU5", or.AidOid.ToString());
            mOGR2.setValue("NU6", or.PartOid.ToString());

            data.setValue("EXT", or.EA_ProductGroup);

            mOGR2.setValue("C1C", or.Priority);
            mOGR2.setValue("C2B", or.AidPriority);

            // ********* Producktions, vet inte ens om dett blir aktuellt ************
            if (GCF.noNULL(or.ProductionTitle).Length > 20)
            {
                mOGR2.setValue("C20", or.ProductionTitle.Substring(0, 20));
                mOGR2.setValue("C10", or.ProductionTitle.Substring(20));
            }
            else
            {
                mOGR2.setValue("C20", or.ProductionTitle);
                mOGR2.setValue("C10", "");
            }

            try
            {
                if (or.Urgent)
                    mOGR2.setValue("C1B", "1");
                else
                    mOGR2.setValue("C1B", "0");
            }
            catch { mOGR2.setValue("C1B", "0"); }


            string[] s = { "yyMMdd", "yyyyMMdd", "yyyy-MM-dd" };
            try
            {
                mOGR2.setValue("C06", or.PromisedDeliverDate.HasValue ? or.PromisedDeliverDate.Value.ToString("yyMMdd") : "");
            }
            catch { mOGR2.setValue("C06", DateTime.Today.ToString("yyMMdd")); }

            try
            {
                mOGR2.setValue("C07", or.ConditionDate.HasValue ? or.ConditionDate.Value.ToString("yyMMdd") : "");
            }
            catch { mOGR2.setValue("C07", DateTime.Today.ToString("yyMMdd")); }

        }

        private void fillCommonOrderRowFields(OrderRowDefinitions.OrderRow or, GarpGenericDB data)
        {
            mOGR.setValue("LDT", GCF.noNULL(or.LevTid));
            mOGR.setValue("DIM", !GCF.noNULL(or.AidDate).Equals("") ? "1" + or.AidDate : or.AidDate);
            mOGR.setValue("RES",oProdStat.getIdByTX1(GCF.noNULL(or.Prodstatus)));
            mOGR.setValue("BNX", or.SelectedHandler);
            //mOGA.setValue("LSE", !GCF.noNULL(GCF.noNULL(or.DeliverMode)).Equals("") ? oDelM.getKeyByName(or.DeliverMode) : "");

            if (or.Warrenty)
            {
                mOGR.setValue("RAB", "-100");
                mOGR.setValue("RBK", "G");
            }
            else
            {
                mOGR.setValue("RAB", "");
                mOGR.setValue("RBK", "");
            }
            
            if(mOGR2.find(or.OrderNo.PadRight(6) + or.Rad.PadLeft(3)))
            {
                mOGR2.setValue("C2A", GCF.noNULL(or.Thord_NeedStep));
                mOGR2.setValue("C1A", or.FirstTimePatient ? "1" : "0");
            }

            orText.saveAidsText(or.OrderNo, or.Rad, or.AidsText);
        }

        public void selectMenuItem(int id)
        {
            app.SelectMenuItem(id);
        }
    }
}

