using System;
using Ortoped.HelpClasses;
using Excido;
using Ortoped.Definitions;
using System.Collections;
using System.Globalization;
using GCS;

namespace Ortoped.GarpGEM
{
    /// <summary>
    /// Summary description for OrderCOM.
    /// </summary>
    public class OrderCOM : GarpCOM
    {
        private GarpGenericDB mOGA, mOGL, mOGF, mOGR, mKA, mHKL, mHKA;
        private Ortoped.HelpClasses.ohText oText = new Ortoped.HelpClasses.ohText();

        /// <summary>
        /// Constructor för OrderCOM
        /// </summary>
        public OrderCOM()
        {
            mOGA = new GarpGenericDB("OGA");
            mOGF = new GarpGenericDB("OGF");
            mOGL = new GarpGenericDB("OGL");
            mOGR = new GarpGenericDB("OGR");
            mKA = new GarpGenericDB("KA");
            mHKL = new GarpGenericDB("HKL");
            mHKA = new GarpGenericDB("HKA");
        }

        /// <summary>
        /// Skapar grunden till ett orderhuvud genom att göra en Insert()
        /// och fyller på med indexvärden och avslutar med Post().
        /// </summary>
        /// <param name="knr"></param>
        /// <returns></returns>
        protected bool doInsert(string knr, string numberset)
        {
            try
            {
                string onr = mOGA.getApp.EDI.GetOrdernr(true, numberset);

                if (mOGA.find(onr))
                {
                    Log4Net.Logger.loggCritical("When creating new order, orderno already exists" + knr + " with orderset " + numberset + " orderno " + onr, Config.User, "OrderCOM.doInsert");
                    return false;
                }

                if (!GCF.noNULL(onr).Equals(""))
                {
                    mOGA.insert();
                    mOGA.setValue("ONR", onr);
                    mOGA.setValue("KNR", knr);
                    mOGA.setValue("OSE", "K");
                    mOGA.setValue("KST", Config.Kst);

                    mOGF.insert();
                    mOGF.setValue("ONR", mOGA.getValue("ONR"));
                    mOGF.setValue("OSE", "K");

                    mOGL.insert();
                    mOGL.setValue("ONR", mOGA.getValue("ONR"));
                    mOGL.setValue("OSE", "K");
                    oText.findTexts(mOGA.getValue("ONR"));
                    doPost();
                }
                else
                {
                    Log4Net.Logger.loggCritical("Creating order on customer " + knr + " with orderset " + numberset + " resulted in an emty ordernumber", Config.User, "OrderCOM.doInsert");
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Kontrollerar om det finns orderrader, finns inte detta så raderas
        /// orderhuvudet.
        /// 
        /// </summary>
        /// <param name="onr"></param>
        /// <returns></returns>
        protected void doDelete(string onr)
        {
            try
            {
                mOGR.find(onr.PadRight(6));
                mOGR.next();
            }
            catch { }

            try
            {
                // If no orderrows wehre found
                if (!mOGR.getValue("ONR").Trim().Equals(onr.Trim()))
                {
                    mOGA.index = 1;
                    if (mOGA.find(onr))
                    {
                        if (mOGF.find(onr))
                            mOGF.delete();

                        oText.deleteAllTextRows(onr);
                        mOGA.delete();
                    }
                }
            }
            catch
            {
                try
                {
                    mOGA.index = 1;
                    if (mOGA.find(onr))
                    {
                        if (mOGF.find(onr)) mOGF.delete();
                        oText.deleteAllTextRows(onr);
                        mOGA.delete();
                    }
                }
                catch (Exception e)
                {
                    Log4Net.Logger.loggError(e, "Error deleting OH", "", "doDelete(string onr)");
                }
            }
        }

        protected bool findCustFirstOrder(string knr)
        {
            mOGA.index = 2;
            mOGA.find(knr);
            mOGA.next();

            // Kontrollera om det är samma kund, då har kunden ordrar			
            try
            {
                if (Patient.Trim() == knr.Trim())
                {
                    mOGF.find(ONR);
                    mOGL.find(ONR);
                    oText.findTexts(ONR);
                    return true;
                }
                else
                    return false;
            }
            catch { return false; }
        }

        /// <summary>
        /// Hitta sista gilltiga ordern (där ordertyp != "E") på patient
        /// </summary>
        /// <param name="knr"></param>
        /// <returns></returns>
        protected bool findLastOrder(string knr)
        {
            mOGA.index = (int)2;
            mOGA.find(knr.PadRight(10) + "ZZZZZZ");
            mOGA.prev();

            while ((OTY == "E") && (Patient.Trim() == knr.Trim()) && (!mOGA.BOF))
            {
                mOGA.prev();
            }

            if (Patient.Trim() == knr.Trim())
            {
                mOGF.find(ONR);
                mOGL.find(ONR);
                oText.findTexts(ONR);
                return true;
            }
            else
            {
                return false;
            }

        }

        protected void _changeAddressOnFS(string fsno, string name, string address1, string address2, string city)
        {
            // Set paymentterms on FS if bvk is not ""
            mHKL.find(fsno);
            mHKL.next();
            if (mHKL.getValue("HNR").Trim().Equals(fsno.Trim()))
            {
                mHKL.setValue("NAM", name);
                mHKL.setValue("AD1", address1);
                mHKL.setValue("AD2", address2);
                mHKL.setValue("ORT", city);
                mHKL.post();
            }
        }

        protected bool findOrder(string onr)
        {
            mOGA.index = 1;

            if (mOGA.find(onr.PadRight(6)))
            {
                mOGF.find(onr.PadRight(6));
                if (!mOGL.find(onr.PadRight(6)))
                {
                    mOGL.insert();
                    mOGL.setValue("ONR", onr);
                    mOGL.post();
                }
                oText.findTexts(ONR);
                return true;
            }
            else
                return false;
        }

        protected OrderHeadDefinition[] getAllOrdersWithOty(string oty)
        {
            ArrayList arr = new ArrayList();
            OrderHeadDefinition oh = new OrderHeadDefinition();
            GarpGenericDB.FilterItem[] filter = new GarpGenericDB.FilterItem[2];
            GarpGenericDB.FilterItem f;

            f = new GarpGenericDB.FilterItem();
            f.Type = GarpGenericDB.ItemType.Field;
            f.Ucase = false;
            f.Value = "OTY";
            filter[1] = f;

            f = new GarpGenericDB.FilterItem();
            f.Type = GarpGenericDB.ItemType.Constant;
            f.FieldType = "C";
            f.Ucase = false;
            f.Value = oty;
            filter[0] = f;

            mOGA.index = 1;
            mOGA.setFilter(filter, "1=2");

            while (!EOF)
            {
                // Hoppa över order med ordertyp "E" (Egenavgiftsorder)
                if (OTY.Equals(oty))
                {
                    mOGF.find(ONR);
                    mOGL.find(ONR);
                    oText.findTexts(ONR);

                    oh = new OrderHeadDefinition();
                    oh.OrderNo = ONR;
                    oh.PatientsSSN = SocialSequrityNumber;
                    oh.ValidFrom = ValidFrom;
                    oh.ValidYearsCount = ValidYearsCount;
                    oh.isClosed = FRY;
                    oh.RekvNo = RekvNr;
                    oh.Ordination = Ordination;
                    oh.Priority = Priority;
                    arr.Add(oh);
                }
                next();
            }
            mOGA.deactivateFilter();

            return (OrderHeadDefinition[])arr.ToArray(typeof(OrderHeadDefinition));
        }

        protected void next()
        {
            mOGA.next();
            mOGF.find(ONR);
            mOGL.find(ONR);
            oText.findTexts(ONR);
        }

        protected bool doPost()
        {
            mOGA.post();
            // Saknas tabellen så lägg upp den
            if (!Signature.Equals("") && !mOGF.find(ONR.PadRight(6)))
            {
                string stemp = Signature;
                mOGF.insert();
                mOGF.setValue("ONR", ONR);
                Signature = stemp;
            }
            mOGF.post();
            mOGL.post();
            oText.saveText(ONR);
            return true;
        }

        #region Propertys

        protected string ONR
        {
            get
            {
                return mOGA.getValue("ONR");
            }

            set
            {
                mOGL.setValue("ONR", value);
            }
        }

        /// <summary>
        /// Levernskundnummer (Patient)
        /// </summary>
        protected string Patient
        {
            get
            {
                return mOGA.getValue("KNR");
            }
            set
            {
                mOGL.setValue("KNR", value);
            }
        }

        /// <summary>
        /// Social sequrity number of current patient.
        /// </summary>
        protected string SocialSequrityNumber
        {
            get
            {
                if (mKA.find(Patient))
                {
                    return mKA.getValue("NAM");
                }
                else
                {
                    return "Hittar inte kunden";
                }
            }
        }

        /// <summary>
        /// Fakturakundnummer
        /// </summary>
        protected string FKN
        {
            get
            {
                return mOGA.getValue("FKN");
            }
            set
            {
                mOGA.setValue("FKN", value);
            }
        }

        /// <summary>
        /// Namn
        /// 
        /// </summary>
        protected string NAM
        {
            get
            {
                return mOGL.getValue("NAM");
            }
            set
            {
                mOGL.setValue("NAM", value);
            }
        }

        protected string AD1
        {
            get
            {
                return mOGL.getValue("AD1");
            }
            set
            {
                mOGL.setValue("AD1", value);
            }
        }

        protected string AD2
        {
            get
            {
                return mOGL.getValue("AD2");
            }
            set
            {
                mOGL.setValue("AD2", value);
            }
        }

        protected string ORT
        {
            get
            {
                return mOGL.getValue("ORT");
            }
            set
            {
                mOGL.setValue("ORT", value);
            }
        }

        /// <summary>
        /// Namnet på fakturakunden
        /// </summary>
        protected string FakKnrNamn
        {
            get
            {
                if ((mKA.find(FKN)) && (FKN.Trim() != Patient.Trim()))
                {
                    return mKA.getValue("NAM"); ;
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// Kliniks kundnummer
        /// </summary>
        protected string Klinik
        {
            get
            {
                return mOGL.getValue("AD1");
            }

            set
            {
                mOGL.setValue("AD1", value);
            }
        }

        /// <summary>
        /// Kliniknamn
        /// </summary>
        protected string KlinikNamn
        {
            get
            {
                return mOGL.getValue("NAM");
            }

            set
            {
                mOGL.setValue("NAM", value);
            }
        }

        /// <summary>
        /// Ordertyp
        /// </summary>
        protected string OTY
        {
            get
            {
                return mOGA.getValue("OTY");
            }

            set
            {
                mOGA.setValue("OTY", value);
            }
        }

        /// <summary>
        /// Betalningsvillkor
        /// </summary>
        protected string BVK
        {
            get
            {
                return mOGA.getValue("BVK");
            }

            set
            {
                if (!GCF.noNULL(value).Trim().Equals(""))
                    mOGA.setValue("BVK", value);
            }
        }


        /// <summary>
        /// Leveranssätt
        /// </summary>
        protected string LSE
        {
            get
            {
                return mOGA.getValue("LSE");
            }

            set
            {
                if (!GCF.noNULL(value).Trim().Equals(""))
                    mOGA.setValue("LSE", value);
            }
        }

        /// <summary>
        /// Ordinatör (Er referens)
        /// </summary>
        protected string Ordinator
        {
            get
            {
                return mOGA.getValue("ERF");
            }

            set
            {
                mOGA.setValue("ERF", value);
            }
        }

        /// <summary>
        /// Prislista
        /// </summary>
        protected string PRL
        {
            get
            {
                return mOGA.getValue("PRL");
            }

            set
            {
                mOGA.setValue("PRL", value);
            }
        }

        /// <summary>
        /// Orderdatum
        /// </summary>
        protected string ODT
        {
            get
            {
                return mOGA.getValue("ODT");
            }

            set
            {
                mOGA.setValue("ODT", value);
            }
        }

        /// <summary>
        /// Om ordern är stängd (makulerad) eller ej
        /// </summary>
        protected bool FRY
        {
            get
            {
                if (mOGA.getValue("FRY").Equals("z"))
                    return true;
                else
                    return false;
            }

            set
            {
                if (value)
                    mOGA.setValue("FRY", "z");
                else
                    mOGA.setValue("FRY", "");
            }
        }

        /// <summary>
        /// Används till att ange när en remiss börjar gälla
        /// </summary>
        protected DateTime ValidFrom
        {
            get
            {
                DateTime valid;
                if (DateTime.TryParseExact(mOGA.getValue("BLT"), "yyMMdd", new CultureInfo("sv-SE"), DateTimeStyles.None, out valid))
                    return valid;
                else
                {
                    //Log4Net.Logger.loggCritical("Date ValidFrom could note be parsed to DateTime, value in string: " + mOGA.getValue("BLT"), Config.User, "updateForm");
                    return DateTime.Now;
                }
            }

            set
            {
                mOGA.setValue("BLT", value.ToString("yyMMdd"));
            }
        }

        /// <summary>
        /// Används till Antal år en remiss är gilltig
        /// </summary>
        protected int ValidYearsCount
        {
            get
            {
                int i;
                if (int.TryParse(mOGA.getValue("TPT"), out i))
                    return i;
                else
                {
                    Log4Net.Logger.loggCritical("String ValidYearCount could note be parsed to Int, value in string: " + mOGA.getValue("TPT"), Config.User, "OrderCOM.ValidYearsCount");
                    return 0;
                }
            }

            set
            {
                mOGA.setValue("TPT", value.ToString());
            }
        }

        /// <summary>
        /// Används till antal hjälpmedel
        /// </summary>
        protected string X1F
        {
            get
            {
                return mOGA.getValue("X1F") == null ? "" : mOGA.getValue("X1F");
            }

            set
            {
                mOGA.setValue("X1F", value);
            }
        }

        protected string Tillagg
        {
            get
            {
                return ECS.noNULL(oText.Tillagg);
            }

            set
            {
                oText.Tillagg = ECS.noNULL(value);
            }
        }

        protected string Ordination
        {
            get
            {
                return ECS.noNULL(oText.Ordination);
            }

            set
            {
                oText.Ordination = ECS.noNULL(value);
            }
        }

        protected string Notering
        {
            get
            {
                return ECS.noNULL(oText.Notering);
            }

            set
            {
                oText.Notering = ECS.noNULL(value);
            }
        }

        protected string OHText
        {
            get
            {
                return oText.OHText;
            }

            set
            {
                oText.OHText = value;
            }
        }

        protected string Signature
        {
            get
            {
                return mOGF.getValue("TX3") == null ? "" : mOGF.getValue("TX3");
            }

            set
            {
                mOGF.setValue("TX3", value);
            }
        }

        protected string ErReferens
        {
            get
            {
                return mOGF.getValue("TX1");
            }

            set
            {
                mOGF.setValue("TX1", value);
            }
        }

        protected string Diagnoskod
        {
            get
            {
                return mOGA.getValue("LTU");
            }

            set
            {
                mOGA.setValue("LTU", value);
            }
        }

        protected string AidType
        {
            get
            {
                return mOGA.getValue("LTP");
            }

            set
            {
                mOGA.setValue("LTP", value);
            }
        }

        protected string DeliverStatus
        {
            get
            {
                return mOGA.getValue("LEF");
            }
        }

        protected string Priority
        {
            get
            {
                try
                {
                    if (mOGF.getValue("GFG").Trim().Equals("N"))
                        return "";

                    else if (mOGF.getValue("GFG").Trim().Equals("S"))
                        return "SUBAKUT";

                    else if (mOGF.getValue("GFG").Trim().Equals("A"))
                        return "AKUT";

                    else
                        return "";

                }
                catch (Exception ex)
                {
                    Log4Net.Logger.loggError(ex, "Error while switch in property Priority", Config.User, "OrderCOM.Priority");
                    return "";
                }
            }

            set
            {
                mOGF.setValue("GFG", value);
            }
        }

        protected string Prislista
        {
            get
            {
                return mOGA.getValue("PRL") == null ? "" : mOGA.getValue("PRL");
            }

            set
            {
                mOGA.setValue("PRL", value);
            }
        }

        /// <summary>
        /// Rekvisitionsnummer från Opas.
        /// </summary>
        protected string RekvNr
        {
            get
            {
                return mOGF.getValue("TX4") == null ? "" : mOGF.getValue("TX4");
            }
        }

        protected string KombikaCode
        {
            get
            {
                if (mOGF.getValue("TX2").Length > 15)
                    return mOGF.getValue("TX2").Substring(0, 15);
                else
                    return mOGF.getValue("TX2");
            }

            set
            {
                if (mOGF.getValue("TX2").Length > 15)
                {
                    string s = mOGF.getValue("TX2").Substring(15);
                    mOGF.setValue("TX2", value.PadRight(15) + s);
                }
                else
                    mOGF.setValue("TX2", value);
            }
        }
        protected string ReferralNr
        {
            get
            {
                if (mOGF.getValue("TX2").Length > 15)
                    return mOGF.getValue("TX2").Substring(15);
                else
                    return "";
            }

            set
            {
                string s;
                if (mOGF.getValue("TX2").Length > 15)
                {
                    s = mOGF.getValue("TX2").Substring(0, 15);
                    mOGF.setValue("TX2", s + value.PadRight(15));
                }
                else
                {
                    s = mOGF.getValue("TX2").PadRight(15);
                    mOGF.setValue("TX2", s + value.PadRight(15));
                }
            }
        }

        protected bool CanChangeInvoiceCustomer
        {
            get
            {
                bool canChange = true;
                mHKA.index = 2;
                if (!mHKA.find(ONR))
                    mHKA.next();

                while (mHKA.getValue("ONR").Trim().Equals(ONR.Trim()) && !mHKA.EOF)
                {
                    if (!mHKA.getValue("FAF").Equals("5"))
                    {
                        canChange = false;
                        break;

                    }
                    mHKA.next();
                }

                return canChange;
            }

        }

        protected bool EOF
        {
            get
            {
                return mOGA.EOF;
            }
        }

        #endregion


        ~OrderCOM()
        {
            mOGA = null;
            mOGF = null;
            mOGL = null;
            mOGR = null;
            mKA = null;

            GC.Collect();
        }
    }
}
