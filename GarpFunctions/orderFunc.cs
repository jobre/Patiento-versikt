using System;
using System.Collections;
using System.Windows.Forms;
using System.Globalization;
using Ortoped.HelpClasses;
using Ortoped.GarpGEM;
using Ortoped.Definitions;
using GCS;

namespace Ortoped.GarpFunctions
{
    /// <summary>
    /// Summary description for orderFunc.
    /// </summary>
    public class orderFunc : GarpGEM.OrderCOM
    {
        private Table oDiag = new Table("GEMORT");
        private OrderRowText orText = new OrderRowText();
        private SaleCOM oSale = new SaleCOM();
        private Contacts oContacts = new Contacts();
        private string mErrorMsg = "";
        private Hashtable htContacts = new Hashtable();
        private Hashtable htThordReferrals = new Hashtable();

        public orderFunc()
        {

        }

        public OrderHeadDefinition getPatientsLastOH(string knr)
        {
            OrderHeadDefinition o = new OrderHeadDefinition();

            if (findLastOrder(knr))
                fillOH(ref o);
            else
                fillOH(ref o, true);
            return o;
        }

        public OrderHeadDefinition[] getAllOHWithOrderType(string oty, bool newRead)
        {
            OrderHeadDefinition[] oh;

            // If user wnts a new read from db or if cashed hashtable is emty
            if (newRead || htThordReferrals.Count == 0)
            {
                oh = getAllOrdersWithOty(oty);

                // Add OH to hashtable for cashed reading
                foreach (OrderHeadDefinition o in oh)
                {
                    try
                    {
                        htThordReferrals.Add(o.OrderNo, o);
                    }
                    catch (Exception ex)
                    {
                        Log4Net.Logger.loggError(ex, "Error when adding order to hashtable", Config.User, "orderFunc.getAllOHWithOrderType");
                    }
                }
                return oh;
            }
            else
            {
                ArrayList alOH = new ArrayList();
                ArrayList alOHRemove = new ArrayList();

                OrderHeadDefinition oTestOTY;

                foreach (OrderHeadDefinition o in htThordReferrals.Values)
                {
                    oTestOTY = getOrder(o.OrderNo);

                    if (!oTestOTY.OrderType.Equals(oty))
                        alOHRemove.Add(o.OrderNo);
                    else
                        alOH.Add(o);
                }

                // Remove OH from hashtable that already is handled
                foreach (string s in alOHRemove)
                {
                    htThordReferrals.Remove(s);
                }

                return (OrderHeadDefinition[])alOH.ToArray(typeof(OrderHeadDefinition));
            }
        }

        public OrderHeadDefinition[] getPatientsAllOH(string knr)
        {
            ArrayList arr = new ArrayList();
            OrderHeadDefinition oh;

            if (findCustFirstOrder(knr))
            {
                while ((Patient.Trim() == knr.Trim()) && (!EOF))
                {
                    // Hoppa över order med ordertyp "E" (Egenavgiftsorder)
                    if (!OTY.Equals("E"))
                    {
                        oh = new OrderHeadDefinition();

                        oh.OrderNo = ONR;
                        oh.ValidFrom = ValidFrom;
                        oh.ValidYearsCount = ValidYearsCount;
                        oh.isClosed = FRY;
                        oh.RekvNo = RekvNr;
                        oh.Priority = Priority;
                        oh.Ordination = Ordination;

                        arr.Add(oh);
                    }
                    next();
                }
            }
            return (OrderHeadDefinition[])arr.ToArray(typeof(OrderHeadDefinition)); //OrderHeadDefinition.convertFromArray(arr);
        }

        public OrderHeadDefinition getOrder(string onr)
        {
            OrderHeadDefinition o = new OrderHeadDefinition();

            if (findOrder(onr))
                fillOH(ref o);
            else
                fillOH(ref o, true);

            return o;
        }

        public bool saveOH(OrderHeadDefinition o)
        {
            if (findOrder(o.OrderNo))
            {
                if (!FRY)
                {
                    OTY = o.OrderType;
                    Patient = o.PatientNo;
                    FKN = (o.InvoiceCustomer == "" ? Patient : o.InvoiceCustomer);
                    BVK = o.PaymentCondition;
                    Klinik = o.Clinik;
                    KlinikNamn = o.ClinikName;
                    ErReferens = o.YourReference;
                    Ordination = o.Ordination;
                    Diagnoskod = o.DiagnoseCode;
                    Notering = o.Notation;
                    OHText = o.OHtext;
                    Tillagg = o.Diagnose;
                    ValidFrom = o.ValidFrom;
                    ValidYearsCount = o.ValidYearsCount;
                    X1F = o.AidCount;
                    Signature = o.Signature;
                    Prislista = o.Pricelist;
                    KombikaCode = o.KombikaCode;
                    ReferralNr = o.ReferralNo;
                    AidType = o.AidType;
                    FRY = o.isClosed;
                    Priority = o.Priority;
                    doPost();
                    findOrder(o.OrderNo); // Do findorder again after post, we heve problems with texts otherwise.
                    Ordinator = o.SelOrdinator;
                    doPost();
                }
                else
                {
                    mErrorMsg = "Order " + o.OrderNo.Trim() + " är fryst och kan inte sparas";
                    return false;
                }
            }
            else
            {
                mErrorMsg = "Order " + o.OrderNo.Trim() + " was not found, may be deleted";
                return false;
            }

            return true;
        }

        public PurchaseDefenitions[] getAllPurchaseOrders(string cust_onr)
        {
            GarpGenericDB IGA = new GarpGenericDB("IGA");
            GarpGenericDB IGF = new GarpGenericDB("IGF");
            GarpGenericDB LA = new GarpGenericDB("LA");
            PurchaseDefenitions purchase;
            ArrayList alPurchase = new ArrayList();

            IGF.first();

            while (!IGF.EOF)
            {
                if (GCF.noNULL(IGF.getValue("TX3")).StartsWith(cust_onr.Trim()))
                {
                    purchase = new PurchaseDefenitions();
                    purchase.IO_No = IGF.getValue("ONR").Trim();
                    purchase.CustomerOrderNo = IGF.getValue("TX3");

                    if (IGA.find(IGF.getValue("ONR")))
                    {
                        purchase.IO_Status = IGA.getValue("LEF");
                        purchase.SupplierNo = IGA.getValue("LNR");

                        if (LA.find(IGA.getValue("LNR")))
                            purchase.SupplierName = LA.getValue("NAM");
                    }
                    alPurchase.Add(purchase);
                }

                IGF.next();
            }
            return (PurchaseDefenitions[])alPurchase.ToArray(typeof(PurchaseDefenitions));
        }


        /// <summary>
        /// Lägger upp ett nytt orderhuvud och sparar detta. Om användare
        /// ångrar sig måste en removeOH göras.
        /// </summary>
        /// <param name="knr"></param>
        /// <returns></returns>
        public OrderHeadDefinition addOH(string knr)
        {
            string numberset = Config.getNumberSerie();

            if(GCF.noNULL(numberset).Equals(""))
            {
                numberset = "A";
                Log4Net.Logger.loggCritical("Could not get nunberset from Config, fallback too numberset A", Config.User, "GarpFunctions.addOH(string knr)");
            }
            
            if (doInsert(knr, Config.getNumberSerie()))
            {
                OrderHeadDefinition o = new OrderHeadDefinition();
                fillOH(ref o);
                return o;
            }
            else
                return null;
        }

        /// <summary>
        /// Lägger upp ett nytt orderhuvud och sparar detta. Om användare
        /// ångrar sig måste en removeOH göras.
        /// </summary>
        /// <param name="knr"></param>
        /// <returns></returns>
        public OrderHeadDefinition addOH(string knr, string numberset)
        {
            if (numberset.Equals(""))
                numberset = "A";

            doInsert(knr, numberset);
            OrderHeadDefinition o = new OrderHeadDefinition();
            fillOH(ref o);
            return o;
        }

        /// <summary>
        /// Raderar hela ordern med alla rader, kontroll görs på om det
        /// finns levererade rader på ordern.
        /// </summary>
        /// <param name="onr"></param>
        /// <returns></returns>
        public void removeOrder(string onr)
        {
            doDelete(onr);
        }

        /// <summary>
        /// Returnerar alla contactpersoner med befattning "X" i en
        /// array av string[].
        /// </summary>
        /// <param name="knr"></param>
        /// <returns></returns>
        public string[] getOrdinatorsOnCustomer(string knr)
        {
            ArrayList s = new ArrayList();
            Contacts.ContactsDef[] cdf;

            if (knr.Equals(""))
                cdf = oContacts.getContacts("X");
            else
                cdf = oContacts.getContacts(knr, "X");

            foreach (Contacts.ContactsDef c in cdf)
            {
                s.Add(c.Name + ("  (" + c.CustomerNo + ")"));
            }

            return (string[])s.ToArray(typeof(string));
        }

        public string[] getAllOrdinators(bool force_new_read)
        {
            Contacts.ContactsDef[] cdf;
            ArrayList al = new ArrayList();

            if (force_new_read)
            {
                htContacts.Clear();
                cdf = oContacts.getContacts("X");

                foreach (Contacts.ContactsDef c in cdf)
                {
                    try
                    {
                        if (!htContacts.ContainsKey(c.Name + ("  (" + c.CustomerNo + ")")))
                            htContacts.Add(c.Name + ("  (" + c.CustomerNo + ")"), c.CustomerNo);
                    }
                    catch (Exception ex)
                    {
                        Log4Net.Logger.loggError(ex, "Ett fel uppstod vid läsning av ordinatörer" + c.CustomerNo, Config.User, "orderFunc.getAllOrdinators");
                    }
                }
            }

            foreach (string s in htContacts.Keys)
                al.Add(s);

            return (string[])al.ToArray(typeof(string));
        }

        public string getOrdinatorsCustomerNo(string ordinator)
        {
            if (htContacts.ContainsKey(ordinator))
                return htContacts[ordinator].ToString();
            else
                return "";
        }

        public string[] getListOfSignatures()
        {
            return oSale.getListOfAllSignatures(Config.Kst);
        }

        public void changeAddress(string onr, string name, string address1, string address2, string city)
        {
            if (findOrder(onr))
            {
                NAM = name;
                AD1 = address1;
                AD2 = address2;
                ORT = city;
                doPost();
            }
        }

        public void changeAddressOnFS(string fsnr, string name, string address1, string address2, string city)
        {
            _changeAddressOnFS(fsnr, name, address1, address2, city);
        }

        private void fillOH(ref OrderHeadDefinition o)
        {
            o.OrderNo = ONR;
            o.OrderType = OTY;
            o.PaymentCondition = BVK;
            o.OrderDate = ODT;
            o.PatientNo = Patient;
            o.InvoiceCustomer = (((FKN == null) || (FKN == Patient)) ? "" : FKN);
            o.InvoiceCustomerName = FakKnrNamn;
            o.Clinik = Klinik;
            o.ClinikName = KlinikNamn;
            o.YourReference = ErReferens;
            o.Ordinator = getOrdinatorsOnCustomer(Klinik);
            o.SelOrdinator = (Ordinator == null ? "" : Ordinator);
            o.Ordination = (Ordination == null ? "" : Ordination);
            o.DiagnoseCode = Diagnoskod;
            o.Diagnose = Tillagg;
            o.Notation = Notering;
            o.OHtext = OHText;
            o.ValidFrom = ValidFrom;
            o.ValidYearsCount = ValidYearsCount;
            o.AidCount = X1F;
            o.Signature = (Signature == null ? "" : Signature);
            o.Pricelist = Prislista;
            o.RekvNo = RekvNr;
            o.KombikaCode = KombikaCode;
            o.ReferralNo = ReferralNr;
            o.AidType = AidType;
            o.Priority = Priority;
            o.isClosed = FRY;
            o.PatientsSSN = SocialSequrityNumber;
            o.ReferralNo = ReferralNr;
            o.DeliverStatus = DeliverStatus;
            o.CanChangeInvoiceCustomer = CanChangeInvoiceCustomer;
        }

        private void fillOH(ref OrderHeadDefinition o, bool clear)
        {
            o.OrderNo = "";
            o.OrderType = "";
            o.PaymentCondition = "";
            o.OrderDate = "";
            o.PatientNo = "";
            o.InvoiceCustomer = "";
            o.Clinik = "";
            o.ClinikName = "";
            o.YourReference = "";
            o.Ordinator = new string[0];
            o.SelOrdinator = "";
            o.Ordination = "";
            o.DiagnoseCode = "";
            o.Diagnose = "";
            o.Notation = "";
            o.OHtext = "";
            o.ValidFrom = DateTime.Now;
            o.ValidYearsCount = 0;
            o.AidCount = "";
            o.Signature = "";
            o.Pricelist = "";
            o.RekvNo = "";
            o.KombikaCode = "";
            o.ReferralNo = "";
            o.AidType = "";
            o.Priority = "";
            o.isClosed = false;
        }

        public void printNotice(string dokgen, string dok, string onr, string aidid, string errandid)
        {
            try
            {
                Garp.IReport dokNotice = app.ReportGenerators.Item(dokgen).Reports.Item(dok);
                dokNotice.RangeFrom = onr;
                dokNotice.RangeTo = onr;
                dokNotice.SetDialogResponse("ID", aidid);
                dokNotice.SetDialogResponse("ERRANDID", errandid);
                dokNotice.Run();
            }
            catch (Exception e)
            {
                Log4Net.Logger.loggError(e, "Error when printing notice (dokgen, dok, onr, aidid, errandid): '" + dokgen + "' '" + dok + "' '" + onr + "' '" + aidid + "' '" + errandid, Config.User, "");
                MessageBox.Show(null, "Ett fel uppstod vid utskrift av Kallelse.\n\nFelmeddelande\n" + e.Message + "\n\nStacktrace\n" + e.StackTrace, "Fel vid utskrift", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void printReceipt(string dokgen, string dok, string fs_nr)
        {
            try
            {
                Garp.IReport dokNotice = app.ReportGenerators.Item(dokgen).Reports.Item(dok);
                dokNotice.RangeFrom = fs_nr;
                dokNotice.RangeTo = fs_nr;
                dokNotice.Run();
            }
            catch (Exception e)
            {
                Log4Net.Logger.loggError(e, "Error when printing recipt (dokgen, dok, fs_nr): '" + dokgen + "' '" + dok + "' '" + fs_nr, Config.User, "");
                MessageBox.Show(null, "Ett fel uppstod vid utskrift av Kallelse.\n\nFelmeddelande\n" + e.Message + "\n\nStacktrace\n" + e.StackTrace, "Fel vid utskrift", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void printWorkOrder(string dokgen, string dok, string onr, string aidid, string errandid)
        {
            try
            {
                Garp.IReport dokNotice = app.ReportGenerators.Item(dokgen).Reports.Item(dok);
                dokNotice.RangeFrom = onr;
                dokNotice.RangeTo = onr;
                dokNotice.SetDialogResponse("ID", aidid);
                dokNotice.SetDialogResponse("ERRANDID", errandid);
                dokNotice.Run();
            }
            catch (Exception e)
            {
                Log4Net.Logger.loggError(e, "Error when printing workorder (dokgen, dok, onr, aidid, errandid): '" + dokgen + "' '" + dok + "' '" + onr + "' '" + aidid + "' '" + errandid, Config.User, "");
                MessageBox.Show(null, "Ett fel uppstod vid utskrift av Arbetsorder.\n\nFelmeddelande\n" + e.Message + "\n\nStacktrace\n" + e.StackTrace, "Fel vid utskrift", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void closeOrder(string onr)
        {
            if (findOrder(onr))
            {
                FRY = true;
                doPost();
            }
        }

        public void openOrder(string onr)
        {
            if (findOrder(onr))
            {
                FRY = false;
                doPost();
            }
        }

        public string getErrorMsg()
        {
            return mErrorMsg;
        }

        public string getCompanyId()
        {
            return app.Bolag;
        }
    }
}
