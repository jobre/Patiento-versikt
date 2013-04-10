using System;
using Excido;
using Ortoped.HelpClasses;
using GCS;

namespace Ortoped.GarpGEM
{
    /// <summary>
    /// Summary description for CustomerCOM.
    /// </summary>
    abstract public class CustomerCOM : GarpCOM
    {
        private GarpGenericDB mKA, mKA_Search;
        private Prislista oPricelist;

        protected CustomerCOM()
        {
            mKA = new GarpGenericDB("KA");
            mKA_Search = new GarpGenericDB("KA");
            oPricelist = new Prislista();
        }

        /// <summary>
        /// Gör find() på index 1 (kundnummer) och fyller på alla
        /// propertyvärden med data.
        /// 
        /// </summary>
        /// <param name="custNr"></param>
        /// <returns></returns>
        protected bool findByCust(string custNr)
        {
            mKA.index = 1;
            return mKA.find(custNr);
        }

        /// <summary>
        /// Gör find() på index 2 (namn) och fyller på alla
        /// propertyvärden med data.
        /// 
        /// </summary>
        /// <param name="pnr"></param>
        /// <returns>Patient</returns>
        protected bool findByIdx2(string nam)
        {
            bool b = false;

            mKA.index = 2;

            // Sök på max 10 tecken, annars är det osäkert om pekaren 
            // hamnar före eller efter matchande poster
            if (nam.Length <= 10)
                b = mKA.find(nam);
            else
            {
                if(mKA.find(nam.Substring(0, 10)))
                {
                    b = true;
                }
                else
                {
                    mKA.next();

                    while (mKA.getValue("NAM").PadRight(10).Substring(0, 10).Equals(nam.Substring(0, 10)) && !mKA.EOF)
                    {
                        if (mKA.getValue("NAM").Trim().Equals(nam.Trim()))
                        {
                            b = true;
                            break;
                        }

                        mKA.next();
                    }
                }
            }

            return b;
        }

        /// <summary>
        /// En funktion för att göra fristående sökning och returnera 
        /// den angivna (på kunden) fakturakundens namn
        /// </summary>
        /// <param name="knr"></param>
        /// <returns></returns>
        protected string getFknName(string knr)
        {
            mKA.index = 1;
            mKA_Search.find(knr);
            return mKA_Search.getValue("NAM");
        }

        protected bool next()
        {
            mKA.next();
            return mKA.EOF;
        }

        protected bool prior()
        {
            mKA.prev();
            return mKA.EOF;
        }

        /// <summary>
        /// Hittar nästa lediga kundnummer
        /// 
        /// </summary>
        /// <returns></returns>
        protected string doInsert()
        {
            mKA.insert();
            return "P" + app.Counters.GetCustomerNo(true);
        }

        protected bool checkNextCustomerNo()
        {
            string sCustNo = "P" + app.Counters.GetCustomerNo(false);

            mKA.index = 1;
            if (mKA.find(sCustNo))
                return false;
            else
                return true;
        }

        protected bool doPost()
        {
            if (KNR != "")
            {
                mKA.post();
                return true;
            }
            else
            {
                return false;
            }
        }


        #region Propertys

        /// <summary>
        /// Kundnummer
        /// </summary>
        protected string KNR
        {
            get { return mKA.getValue("KNR"); }
            set { mKA.setValue("KNR", value); }
        }

        /// <summary>
        /// Personnummer
        /// </summary>
        protected string PNR
        {
            get { return mKA.getValue("NAM"); }
            set { mKA.setValue("NAM", value); }
        }

        /// <summary>
        /// Förnamn (Efternamn måste ALLTID anges först)
        /// </summary>
        protected string SureNAM
        {
            get
            {
                try
                {
                    string[] s = mKA.getValue("AD1").Split((" ").ToCharArray());
                    return s[s.Length - 1];
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    string[] s = mKA.getValue("AD1").Split((" ").ToCharArray(), 2);
                    if (s.Length > 0)
                    {
                        mKA.setValue("AD1", s[0] + " " + value);
                    }
                    else
                        mKA.setValue("AD1", mKA.getValue("AD1") + " " + value);
                }
                catch (Exception e)
                {
                    mKA.setValue("AD1", mKA.getValue("AD1") + " " + value);
                    Log4Net.Logger.loggError(e, "Error when saving Patients surename, KNR: " + mKA.getValue("KNR") + " Result: " + mKA.getValue("AD1"), Config.User, "CustomerCOM.SureNAM");
                }
            }
        }

        /// <summary>
        /// Efternamn
        /// </summary>
        protected string LastNAM
        {
            get
            {
                try
                {
                    string[] s = mKA.getValue("AD1").Split((" ").ToCharArray());
                    if (s.Length > 2)
                        return s[0] + " " + s[1];
                    else
                        return s[0];
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    if (!mKA.getValue("AD1").Equals(""))
                    {
                        string[] s = mKA.getValue("AD1").Split((" ").ToCharArray(), 2);
                        s[0] = value;

                        mKA.setValue("AD1", s[0] + " " + s[1]);
                    }
                    else
                        mKA.setValue("AD1", value);
                }
                catch (Exception e)
                {
                    Log4Net.Logger.loggError(e, "Error when saving Patients lastname", Config.User, "CustomerCOM.LastNAM");
                    mKA.setValue("AD1", value);
                }
            }
        }

        /// <summary>
        /// Adress
        /// </summary>
        protected string AD2
        {
            get { return mKA.getValue("AD2"); }
            set { mKA.setValue("AD2", value); }
        }

        /// <summary>
        /// Pnr/Ort
        /// </summary>
        protected string ORT
        {
            get { return mKA.getValue("ORT"); }
            set { mKA.setValue("ORT", value); }
        }

        /// <summary>
        /// Fakturakund (Landsting)
        /// </summary>
        protected string FKN
        {
            get { return mKA.getValue("FKN"); }
            set { mKA.setValue("KNR", value); }
        }

        protected string KAT
        {
            get { return mKA.getValue("KAT"); }
            set { mKA.setValue("KAT", value); }
        }

        /// <summary>
        /// Anmärkning
        /// </summary>
        protected string Remark
        {
            get { return mKA.getValue("TX4") + mKA.getValue("TX5"); }
        }

        /// <summary>
        /// Telefon Bostad
        /// </summary>
        protected string TelHome
        {
            get { return mKA.getValue("TEL"); }
            set { mKA.setValue("TEL", value); }
        }

        /// <summary>
        /// Telefon arbete
        /// </summary>
        protected string TelWork
        {
            get { return mKA.getValue("FAX"); }
            set { mKA.setValue("FAX", value); }
        }

        /// <summary>
        /// Telefon Mobil
        /// </summary>
        protected string TelMobile
        {
            get { return mKA.getValue("TX6"); }
            set { mKA.setValue("TX6", value); }
        }

        /// <summary>
        /// Telefon Mobil
        /// </summary>
        protected string TelSMS
        {
            get { return mKA.getValue("TX3"); }
            set { mKA.setValue("TX3", value); }
        }

        protected bool Journal
        {
            get
            {
                if (mKA.getValue("KD1").Equals("J"))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    mKA.setValue("KD1", "J");
                else
                    mKA.setValue("KD1", "");
            }
        }

        protected bool CopDok
        {
            get
            {
                if (mKA.getValue("KD2").Equals("J"))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    mKA.setValue("KD2", "J");
                else
                    mKA.setValue("KD2", "");
            }
        }

        /// <summary>
        /// Returns true if the customer is aktiv.
        /// </summary>
        protected bool IsValid
        {
            get
            {
                if (mKA.getValue("KD3").ToLower().Equals("n"))
                    return false;
                else
                    return true;
            }
        }

        protected string ViewCode
        {
            get { return mKA.getValue("KD6"); }
            set { mKA.setValue("KD6", value); }
        }

        protected bool EOF
        {
            get { return mKA.EOF; }
        }

        protected string PriceListName
        {
            get { return oPricelist.getPriceListById(mKA.getValue("PRL")); }
        }

        protected string PriceListCode
        {
            get { return mKA.getValue("PRL"); }
            set { mKA.setValue("PRL", value); }
        }

        protected bool JointInvoicing
        {
            get
            {
                if (mKA.getValue("SMF").Equals("1"))
                    return true;
                else
                    return false;
            }
        }

        protected bool InterestInvoice
        {
            get
            {
                if (mKA.getValue("RDB").Equals("1"))
                    return true;
                else
                    return false;
            }

            set
            {
                if (value)
                    mKA.setValue("RDB", "1");
                else
                    mKA.setValue("RDB", "");
            }
        }

        protected bool PaymentReminder
        {
            get
            {
                if (mKA.getValue("KRV").Equals("1"))
                    return true;
                else
                    return false;
            }

            set
            {
                if (value)
                    mKA.setValue("KRV", "1");
                else
                    mKA.setValue("KRV", "");
            }
        }

        public string PaymentTerms
        {
            get { return mKA.getValue("BVK"); }
            set { mKA.setValue("BVK", value); }
        }

        public string DeliverTerms
        {
            get { return mKA.getValue("LVK"); }
            set { mKA.setValue("LVK", value); }
        }

        public string WayOfDeliver
        {
            get { return mKA.getValue("LSE"); }
            set { mKA.setValue("LSE", value); }
        }

        public string VATCode
        {
            get { return mKA.getValue("MOM"); }
            set { mKA.setValue("MOM", value); }
        }

        public bool Deceased
        {
            get
            {
                if (mKA.getValue("KTY").Equals("z"))
                    return true;
                else
                    return false;
            }

            set
            {
                if (value)
                    mKA.setValue("KTY", "z");
                else
                    mKA.setValue("KTY", "");
            }
        }

        public double OpenBalance
        {
            get
            {
                return ECS.stringToDouble(mKA.getValue("SLD"));
            }
        }

        #endregion

        ~CustomerCOM()
        {
            mKA = null;
            GC.Collect();
        }
    }
}
