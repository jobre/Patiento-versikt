using System;
using System.Collections.Generic;
using System.Globalization;
using System.Collections;
using System.Text;
using System.Windows.Forms;
//#if DEBUG
//  using Ortoped.se.sll.bkv.externtest;
//  using Ortoped.se.sll.bkv_sabb.bkv_tstweb1;
//#else
using Ortoped.se.sll.thord.www;
//#endif
using Ortoped.HelpClasses;
using Ortoped;
using Excido;
using Ortoped.Definitions;
using GCS;

namespace Ortoped.Thord
{
    /// <summary>
    /// Struct GarpAid
    /// </summary>
    public struct GarpAid
    {
        public String aidID;
        public String namn;
        public AidStatusValues status;
        public String isonummer;
        public int aidtypeid;
        public int behovstrappa;
    }
    /// <summary>
    /// Class GarpReferrals
    /// Handles all data transfer to and from Garp
    /// </summary>
    public class GarpReferrals
    {
        private Garp.Application app;
        private Garp.ITable mKA;
        private Garp.ITable mAGA;
        private Garp.ITable mAGT;
        private Ortoped.GarpFunctions.CustomerFunc oCust;
        private Garp.ITabField mAGA_ANR, mAGA_SES, mAGA_STA, mAGA_KD1, mAGA_KD6;
        private Garp.ITabField mAGT_TX15, mAGT_ANR;
        private Garp.ITabField mKA_KNR, mKA_NAM, mKA_AD1, mKA_AD2, mKA_ORT, mKA_KAT, mKA_LVK, mKA_KD6, mKA_BVK, mKA_PRL, mKA_MOM, mKA_LSE, mKA_KTY;
        private String sKNR;

        /// <summary>
        /// The constructor creates the Garp.ApplicationClass
        /// and all the Garp objects
        /// </summary>
        public GarpReferrals()
        {
            try
            {
                app = new Garp.Application();
                oCust = new Ortoped.GarpFunctions.CustomerFunc();
            }
            catch
            {
                Console.WriteLine("Garp kunde inte startas..");
            }

            mKA = app.Tables.Item("KA");
            mAGA = app.Tables.Item("AGA");
            mAGT = app.Tables.Item("AGT");

            mAGA_ANR = mAGA.Fields.Item("ANR");
            mAGA_SES = mAGA.Fields.Item("SES");
            mAGA_STA = mAGA.Fields.Item("STA");
            mAGA_KD1 = mAGA.Fields.Item("KD1");
            mAGA_KD6 = mAGA.Fields.Item("KD6");

            mAGT_ANR = mAGT.Fields.Item("ANR");
            mAGT_TX15 = mAGT.Fields.Item("TX15");

            mKA_KNR = mKA.Fields.Item("KNR");
            mKA_NAM = mKA.Fields.Item("NAM");
            mKA_AD1 = mKA.Fields.Item("AD1");
            mKA_AD2 = mKA.Fields.Item("AD2");
            mKA_ORT = mKA.Fields.Item("ORT");
            mKA_KAT = mKA.Fields.Item("KAT");
            mKA_LVK = mKA.Fields.Item("LVK");
            mKA_KD6 = mKA.Fields.Item("KD6");
            mKA_BVK = mKA.Fields.Item("BVK");
            mKA_PRL = mKA.Fields.Item("PRL");
            mKA_MOM = mKA.Fields.Item("MOM");
            mKA_LSE = mKA.Fields.Item("LSE");
            mKA_KTY = mKA.Fields.Item("KTY");
        }

        public Referral[] updateTables(Referral[] refList, string compid)
        {
            ArrayList alReturnRef = new ArrayList();
            ArrayList alReferrals = new ArrayList();
            PatientDefenition[] oPatient = null;

            alReferrals.AddRange(refList);


            foreach (Referral r in alReferrals)
            {
                if (!r.remissid.Equals(""))
                {
                    oPatient = oCust.getPatientByPnr(r.personnr.Insert(8, "-"), true, true);

                    // Check if patient exists, if not try to create one
                    if (oPatient.Length == 0)
                    {
                        //ingen kund hittades - skapa denna
                        try
                        {
                            ArrayList alCust = new ArrayList();
                            alCust.Add(r.personnr);
                            createCustomer(alCust);
                            oPatient = oCust.getPatientByPnr(r.personnr.Insert(8, "-"), true, true);
                        }
                        catch
                        {
                            MessageBox.Show("Ett fel inträffade vid upplägg av patient " + r.personnr, "Fel vid upplägg av patient", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }

                    try // If an exception is raised or oPatient is emty, don't add order no.
                    {
                        if (oPatient.Length > 0)
                        {
                            Referral r2 = r;
                            r2.orderno = insertReferrals(r, oPatient[0].PatientNo, compid);
                            alReturnRef.Add(r2);
                        }
                    }
                    catch { }
                }
            }

            return (Referral[])alReturnRef.ToArray(typeof(Referral));
        }

        /// <summary>
        /// Fetches Referals with the supplies Ordernumber
        /// </summary>
        /// <param name="ordnr">The ordernumber</param>
        /// <returns></returns>
        public Referral[] getReferrals(String ordnr)
        {
            ArrayList aList = new ArrayList();
            Referral rfl = new Referral();
            Ortoped.GarpFunctions.orderFunc of = new Ortoped.GarpFunctions.orderFunc();
            Ortoped.Definitions.OrderHeadDefinition oOH = of.getOrder(ordnr);

            if (oOH.OrderNo != null)
            {
                rfl.datum = DateTime.ParseExact(oOH.OrderDate, "yyMMdd", new CultureInfo("sv-SE"));
                rfl.ordinator = oOH.SelOrdinator;
                rfl.personnr = oOH.PatientNo;
                rfl.diagnos = oOH.Diagnose;
                rfl.notering = oOH.Notation;
                rfl.kombikakod = oOH.KombikaCode;
                rfl.remissid = oOH.ReferralNo;
            }

            aList.Add(rfl);

            return (Referral[])aList.ToArray(typeof(Referral));
        }

        /// <summary>
        /// Creates a missing customer and sets it's values
        /// from Thord.
        /// </summary>
        /// <param name="personnr">person number</param>
        public void createCustomer(ArrayList personnr)
        {
            string[] pnummer = (string[])personnr.ToArray(typeof(string));
            ThordFunctions tf = new ThordFunctions();
            ThordService.personinformation pinfo = new ThordService.personinformation();
            PatientDefenition newPatient = new PatientDefenition();

            pinfo = tf.getPersonInfo(pnummer);
            for (int i = 0; i < pinfo.person.Length; i++)
            {
                string namn = pinfo.person[i].lastname + pinfo.person[i].firstname;
                namn = namn.Replace("/", " ");

                String prnummer = pinfo.person[i].personnumber.Insert(8, "-");

                newPatient.PatientNo = "";
                newPatient.SSN = prnummer;
                newPatient.LastName = pinfo.person[i].lastname;
                newPatient.SureName = pinfo.person[i].firstname;
                newPatient.Address = pinfo.person[i].street;
                newPatient.PoCity = pinfo.person[i].postalcode.PadRight(7) + pinfo.person[i].postaladdress;
                newPatient.TelHome = pinfo.person[i].phonehome;
                newPatient.TelWork = pinfo.person[i].phonework;
                newPatient.TelMobile = pinfo.person[i].phonemobile;
                newPatient.Category = Ortoped.HelpClasses.Config.getCustTemplate.Category;
                newPatient.DeliverTerms = Ortoped.HelpClasses.Config.getCustTemplate.DeliverTerms;
                newPatient.PaymentTerms = Ortoped.HelpClasses.Config.getCustTemplate.PaymentTerms;
                newPatient.PriceList = Ortoped.HelpClasses.Config.getCustTemplate.PriceList;
                newPatient.VATCode = Ortoped.HelpClasses.Config.getCustTemplate.VATCode;
                newPatient.WayOfDeliver = Ortoped.HelpClasses.Config.getCustTemplate.WayOfDeliver;
                newPatient.ViewCode = Ortoped.HelpClasses.Config.getCustTemplate.ViewCode;
                newPatient.InterestInvoice = Ortoped.HelpClasses.Config.getCustTemplate.InterestInvoice;
                newPatient.PaymentReminder = Ortoped.HelpClasses.Config.getCustTemplate.PaymentReminder;
                newPatient.DoesExist = true;

                try
                {
                    oCust.addCustomer(newPatient);
                }
                catch (Exception ex)
                {
                    Log4Net.Logger.loggError(ex, "Error while adding patient", Config.User, "GarpReferrals.createCustomer");
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Insert the supplied referal into Garp tables
        /// </summary>
        /// <param name="r"></param>
        public string insertReferrals(Referral r, string knr, string compid)
        {
            sKNR = knr;

            Ortoped.GarpFunctions.orderFunc of = new Ortoped.GarpFunctions.orderFunc();
            Ortoped.Definitions.OrderHeadDefinition newOH = of.addOH(sKNR, compid);

            if (newOH == null)
            {
                MessageBox.Show(null, "Ett fel uppstod vid skapande av order: " + newOH.OrderNo, "Fel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw new Exception("Kunde inte skapa order");
            }

            try
            {
                newOH.Pricelist = "A";
                newOH.DiagnoseCode = r.diagnoskod;
                newOH.ValidFrom = r.giltligfran;
                newOH.SelOrdinator = r.ordinator;
                newOH.ValidYearsCount = int.Parse(r.antalar);
                newOH.InvoiceCustomer = "140";
                newOH.OrderType = compid;
                newOH.KombikaCode = r.kombikakod;
                newOH.ReferralNo = r.remissid;
                newOH.YourReference = r.remissid;
                newOH.Ordination = r.aidtypes + " " + r.ordination;
                newOH.Diagnose = r.diagnos;
                newOH.Priority = r.prioritering;
                if (ECS.noNULL(r.handlaggare).Equals(""))
                    newOH.Notation = r.notering;
                else
                    newOH.Notation = r.notering + " Önskad handläggare: " + r.handlaggare;

                if (!of.saveOH(newOH))
                    throw new Exception("Kunde inte skapa order");
            }
            catch
            {
                MessageBox.Show(null, "Ett fel uppstod vid skapande av order: " + newOH.OrderNo, "Fel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw new Exception("Kunde inte skapa order");
            }
            return newOH.OrderNo;
        }

        /// <summary>
        /// Get Aid from Garp and build Aid definition with all parts
        /// </summary>
        /// <param name="ordernr">order number</param>
        /// <returns>Array of New Aid Definitions</returns>
        private Aid[] getAids(string ordernr, string aidid)
        {
            HelpClasses.Table oProdStat = new HelpClasses.Table("1R");
            Aid[] aup = new Aid[1];
            ArrayList sAidList = new ArrayList();
            ArrayList parts = new ArrayList();
            Ortoped.GarpFunctions.OrderRowFunc orf = new Ortoped.GarpFunctions.OrderRowFunc();
            OrderRowDefinitions.OrderRow[] or;

            or = orf.getAid(ordernr, aidid, false).OrderRows.ToArray();

            if (or.Length > 0)
            {
                //första orderrad till sista orderrad
                foreach (OrderRowDefinitions.OrderRow mor in or)
                {
                    if (mor.ViewInList) // AID
                    {
                        // aup.externalaidid = mOGR.Fields.Item("ANR").Value;
                        string externalaidid = mor.AidDate + mor.OrderNo.PadRight(6) + mor.AidNr + mor.Rad.PadLeft(3);
                        aup[0].name = mor.ProductName;

                        //garpaidid bör inte ligga i description, var skall det ligga?
                        aup[0].description = externalaidid;

                        if (mAGA.Find(mor.Artikel))
                        {
                            //hämta aidtypeoid!
                            aup[0].aidtypeoidSpecified = true;
                            aup[0].aidtypeoid = mAGA_SES.Value == null ? 0 : Int32.Parse(mAGA_SES.Value);
                            aup[0].aidoid = mor.AidOid;
                            aup[0].CreatedInThord = mor.CreatedInThord;
                            aup[0].orderno = mor.OrderNo;
                            aup[0].row = mor.Rad;
                            aup[0].isonumber = mAGA_STA.Value == null ? "" : mAGA_STA.Value;
                            aup[0].externalaidid = externalaidid;
                            aup[0].ProdStat = oProdStat.getIdByTX1(mor.Prodstatus);
                            aup[0].needstepSpecified = true;
                            aup[0].FirstTimePatient = mor.FirstTimePatient;
                            if (!mor.Thord_NeedStep.Equals(""))
                                aup[0].needstep = int.Parse(mor.Thord_NeedStep);
                            else
                                MessageBox.Show("Behovstrappa är inte angiven på detta hjälpmedel", "Behovstrappa");
                        }
                    }

                    Part p = new Part();

                    p.CreatedInThord = mor.CreatedInThord;
                    p.orderno = mor.OrderNo;
                    p.row = mor.Rad;
                    p.partoid = mor.PartOid;
                    p.countSpecified = true;
                    p.count = Convert.ToDecimal(mor.Antal.Replace(".", ","));

                    if (mAGT.Find(mor.Artikel))
                    {
                        p.positionid = mor.Artikel; //mAGT_TX15.Value == null ? "" : mAGT_TX15.Value;
                        if (mAGA.Find(mor.Artikel))
                        {
                            if (ECS.noNULL(mAGA_KD6.Value).Equals("N")) // If KD6 = "N" then the part have no price in Garp
                            {
                                p.priceSpecified = true;
                                p.price = Convert.ToDecimal(mor.APris.Replace(".", ","));
                            }
                            else
                            {
                                p.priceSpecified = false;
                                p.price = 0;
                            }
                        }
                        parts.Add(p);
                    }
                }
            }
            else
            {
                MessageBox.Show(null, "Order: " + ordernr + " saknas", "Finns ej", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            aup[0].parts = (Part[])parts.ToArray(typeof(Part));

            return aup;
        }

        /// <summary>
        /// Fetches garp data and creates instances of referralefinition
        /// </summary>
        /// <param name="ordernr">order number</param>
        /// <returns>Array of New Aid Definitions</returns>
        public Referral getReferral(string ordernr, string aidid)
        {
            Ortoped.GarpFunctions.orderFunc ohf = new Ortoped.GarpFunctions.orderFunc();
            OrderHeadDefinition oh;
            Referral rf = new Referral();
            oh = ohf.getOrder(ordernr);

            if (!oh.ReferralNo.Equals(""))
            {
                rf.personnr = oh.PatientsSSN;
                rf.remissid = oh.ReferralNo;
                rf.kombikakod = oh.KombikaCode;
                rf.ordinator = oh.SelOrdinator;
                //      rf.datum = oh.Orderdatum;
                rf.diagnos = oh.DiagnoseCode;
                rf.notering = oh.Notation;
                rf.orderno = oh.OrderNo;

                rf.aids = getAids(ordernr, aidid);
            }

            return rf;
        }

        /// <summary>
        /// Fetches garp data and creates instances of referralefinition
        /// </summary>
        /// <param name="ordernr">order number</param>
        /// <returns>Array of New Aid Definitions</returns>
        public Referral getReferralForRemovalOfPart(string ordernr, string aidid, string rowremoval)
        {
            Ortoped.GarpFunctions.orderFunc ohf = new Ortoped.GarpFunctions.orderFunc();
            OrderHeadDefinition oh;
            Referral rf = new Referral();
            oh = ohf.getOrder(ordernr);

            if (!oh.ReferralNo.Equals(""))
            {
                rf.personnr = oh.PatientsSSN;
                rf.remissid = oh.ReferralNo;
                rf.kombikakod = oh.KombikaCode;
                rf.ordinator = oh.SelOrdinator;
                //      rf.datum = oh.Orderdatum;
                rf.diagnos = oh.DiagnoseCode;
                rf.notering = oh.Notation;
                rf.orderno = oh.OrderNo;

                rf.aids = getAids(ordernr, aidid);

                for (int i = 0; i < rf.aids[0].parts.Length; i++)
                {
                    if (rf.aids[0].parts[i].orderno.Equals(ordernr) && rf.aids[0].parts[i].row.Equals(rowremoval))
                        rf.aids[0].parts[i].RemoveMe = true;

                    if (rf.aids[0].parts.Length <= 1)
                        rf.aids[0].RemoveMe = true;
                }
            }
            return rf;
        }

        /// <summary>
        /// Checks wether or not the article supplied is Egenavgift or not
        /// </summary>
        /// <param name="anr">article number</param>
        /// <returns>true/false</returns>
        public bool isEgenavgift(string anr)
        {

            mAGA.First();
            if (mAGA.Find(anr))
            {
                if (mAGA_KD1.Value == "E")
                    return true;
                else
                    return false;
            }
            else return false;

        }
    }
}
