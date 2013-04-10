using System;
using System.Xml;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32;
using GCS;

namespace Ortoped.HelpClasses
{

    /// <summary>
    /// Summary description for Config.
    /// </summary>
    public class Config : GarpGEM.GarpCOM
    {
        private static ArrayList alKallelse = new ArrayList(), alArbetsOrder = new ArrayList(), alPatient = new ArrayList(), alViewCode = new ArrayList(), alCopDoc = new ArrayList();
        private static Hashtable hsOrderNumberSet = new Hashtable();
        private static string mDiagnosFile = "",
                              mReceipt = "",
                              mInvoice = "",
                              mUserCode = "",
                              mUser = "",
                              mNumberSerie = "A",
                              mKst = "",
                              mBetVillkEaKont = "00",
                              mBetVillkEaFakt = "30",
                              mCompanyId = "",
                              mThordUserId = "",
                              mThordPassword = "",
                              mSMTP = "",
                              mSMTP_User = "",
                              mSMTP_Password = "",
                              mSMTP_FromAddress = "",
                              mSMTP_SupportAddress = "",
                              mConfigPath;



        private static bool mUseUserKodes = false,
                            mShowPreferences = true,
                            mThordUser = false,
                            mAlwaysUpperCase = false,
                            mEA_UseProductGroups = false;


        private static IntPtr mIntPtrGarp = IntPtr.Zero;
        private static CustTemplate mCustTemplate;
        private static CopDoc[] mCopDoc;

        public struct CustTemplate
        {
            //			public string PatientCode;
            public string PaymentTerms;
            public string DeliverTerms;
            public string WayOfDeliver;
            public string Category;
            public string VATCode;
            public string Type;
            public string ViewCode;
            public string PriceList;
            public bool InterestInvoice;
            public bool PaymentReminder;
        }

        public struct CopDoc
        {
            public int btnIdx;
            public string Path;
            public string Arguments;
            public string Table;
            public string Field;
            public string ButtonText;
        }

        static Config()
        {
            mCompanyId = app.Bolag;
            getUser(app.User);
            parseXML(app.Bolag);
        }

        /// <summary>
        /// Get users kod
        /// </summary>
        /// <param name="anv"></param>
        private static void getUser(string anv)
        {
            string s = "";

            Garp.ITable TA = app.Tables.Item("TA");
            if (TA.Find(("WYUN" + anv).PadRight(10, ' ')))
            {
                s = TA.Fields.Item("TX1").Value;
                if (s.Length > 0)
                    mUserCode = s.Substring(0, 1);
            }

            if (!mUserCode.Equals("") && s.Substring(1, 1).Equals("#"))
            {
                mUseUserKodes = true;
                mUser = anv + " - " + s.Substring(2);
            }
            else
            {
                mUseUserKodes = false;
                mUser = anv + " - " + s.Substring(2);
            }
        }

        private static void parseXML(string bolag)
        {
            string file = "configure.xml";
            XmlDocument xdoc = new XmlDocument();
            XmlNodeList xbolaglist;
            XmlNode xbolag = null;
            RegistryKey rkExcido = Registry.LocalMachine.OpenSubKey("Software").OpenSubKey("eXcido");

            try
            {
                /* If a registrypost exists, read configure from saved location */
                if (rkExcido != null)
                {
                    file = rkExcido.GetValue("ConfigureFile", "configure.xml").ToString();
                    mConfigPath = file;
                }
                else
                    mConfigPath = Application.StartupPath + file;
            }
            catch { }

            try
            {
                xdoc.Load(file);
                xbolaglist = xdoc.GetElementsByTagName("Bolag");
            }
            catch
            {
                return;
            };

            // Hitta eftersökt bolag
            foreach (XmlNode node in xbolaglist)
            {
                if (bolag.Equals(node.Attributes.GetNamedItem("ID").InnerText))
                {
                    xbolag = node;
                    break;
                }
            }

            // Om en post för detta bolag hittades, hämta parametrar
            if (xbolag != null)
            {
                DiagnosPath = xbolag.Attributes.GetNamedItem("DiagnosFil").InnerText;

                try
                {
                    if (GCF.noNULL(xbolag.Attributes.GetNamedItem("VisaDetaljer").InnerText).Equals("N"))
                        mShowPreferences = false;
                }
                catch { }

                try
                {
                    if (GCF.noNULL(xbolag.Attributes.GetNamedItem("AlltidVersaler").InnerText).Equals("J"))
                        mAlwaysUpperCase = true;
                }
                catch { }

                int iCopDoc = 0;
                if (xbolag["CopDoc"] != null)
                {
                    foreach (XmlNode node in xbolag.SelectNodes("CopDoc"))
                    {
                        CopDoc c = new CopDoc();

                        try
                        {
                            c.btnIdx = iCopDoc;
                            c.ButtonText = node.Attributes["ButtonText"].InnerText;
                            c.Path = node.Attributes["Path"].InnerText; ;
                            c.Arguments = node.Attributes["Arguments"].InnerText; ;
                        }
                        catch (Exception ex)
                        {
                            Log4Net.Logger.loggError(ex, "Problem when reading CopDoc config", Config.User, "Config.parseXML");
                        }

                        try
                        {
                            if (node.Attributes["Arguments"].InnerText.Contains("{") && node.Attributes["Arguments"].InnerText.Contains(";") && node.Attributes["Arguments"].InnerText.Contains("}"))
                            {
                                int startTab = node.Attributes["Arguments"].InnerText.IndexOf("{") + 1;
                                int lengthTab = (node.Attributes["Arguments"].InnerText.IndexOf(";") - node.Attributes["Arguments"].InnerText.IndexOf("{")) - 1;
                                int startField = node.Attributes["Arguments"].InnerText.IndexOf(";") + 1;
                                int lengthField = (node.Attributes["Arguments"].InnerText.IndexOf("}") - node.Attributes["Arguments"].InnerText.IndexOf(";")) - 1;

                                c.Table = node.Attributes["Arguments"].InnerText.Substring(startTab, lengthTab);
                                c.Field = node.Attributes["Arguments"].InnerText.Substring(startField, lengthField);
                            }
                        }
                        catch (Exception ex)
                        {
                            Log4Net.Logger.loggError(ex, "Problem when reading CopDoc table and fields", Config.User, "Config.parseXML");
                        }

                        alCopDoc.Add(c);

                        iCopDoc++;
                    }

                    mCopDoc = (CopDoc[])alCopDoc.ToArray(typeof(CopDoc));
                }


                if ((xbolag["Anvandare"] != null) && mUseUserKodes)
                {
                    setUserPreferences(xbolag["Anvandare"].GetElementsByTagName("AnvKod"));
                }
                else
                    mUseUserKodes = false;

                try
                {
                    if (GCF.noNULL(xbolag.Attributes.GetNamedItem("VisaProduktGrupper").InnerText).Equals("J"))
                        mEA_UseProductGroups = true;
                    else
                        mEA_UseProductGroups = false;
                }
                catch
                {
                    mEA_UseProductGroups = false;
                }
            }
            else
                return;

            try
            {
                mSMTP = GCF.noNULL(xdoc["Configure"]["MailConfig"].Attributes["Smtp"].InnerText);
                mSMTP_User = GCF.noNULL(xdoc["Configure"]["MailConfig"].Attributes["User"].InnerText);
                mSMTP_Password = GCF.noNULL(xdoc["Configure"]["MailConfig"].Attributes["Password"].InnerText);
                mSMTP_FromAddress = GCF.noNULL(xdoc["Configure"]["MailConfig"].Attributes["FromAddress"].InnerText);
                mSMTP_SupportAddress = GCF.noNULL(xdoc["Configure"]["MailConfig"].Attributes["SupportAddress"].InnerText);
            }
            catch { }

        }

        private static void buildViewList(XmlNodeList lst)
        {
            foreach (XmlNode node in lst)
            {
                if (node["AnvKod"].InnerText.Equals(mUserCode))
                {
                    foreach (XmlNode node2 in node.ChildNodes)
                    {
                        if (node2.Name.Equals("PatientKod"))
                            alViewCode.Add(node2.InnerText);
                    }
                }
            }
        }

        /// <summary>
        /// Returns current users numberserie (A,B,C,D)
        /// 
        /// </summary>
        /// <returns></returns>
        public static string getNumberSerie()
        {
            return mNumberSerie;
        }

        /// <summary>
        /// Returnerar en array MenuItem innehållande alla Kallelse dokument
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static MenuItem[] getKallelse(EventHandler e)
        {
            MenuItem[] mnu = new MenuItem[alKallelse.Count];

            for (int i = 0; i < alKallelse.Count; i++)
            {
                mnu[i] = new MenuItem(alKallelse[i].ToString(), e);
            }
            return mnu;
        }

        /// <summary>
        /// /// Returnerar en array MenuItem innehållande alla Arbetsorder dokument
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static MenuItem[] getArbetsOrder(EventHandler e)
        {
            MenuItem[] mnu = new MenuItem[alArbetsOrder.Count];

            for (int i = 0; i < alArbetsOrder.Count; i++)
            {
                mnu[i] = new MenuItem(alArbetsOrder[i].ToString(), e);
            }
            return mnu;
        }

        public static string getReceipt()
        {
            return mReceipt;
        }

        public static string getInvoice()
        {
            return mInvoice;
        }

        /// <summary>
        /// Checks with Configure.xml if current user should se current patient. Takes patients 
        /// viewcode as parameter (KD3)
        /// 
        /// </summary>
        /// <param name="pat_code"></param>
        /// <returns></returns>
        public static bool viewPatient(string pat_code)
        {
            // ** Show patient if -
            // The code is found in the viewlist (alViewCode) OR
            // No codes is defined in "Configure.xml" OR
            // The user has no viewcode set OR
            // Patients viewcode is ""
            if (alViewCode.Contains(pat_code) || (alViewCode.Count == 0) || !mUseUserKodes || pat_code.Equals(""))
                return true;
            else
                return false;
        }


        /// <summary>
        /// Bygger upp en arraylist av dokument
        /// 
        /// </summary>
        /// <param name="lst"></param>
        private static void setDocList(XmlNodeList lst, ref ArrayList ar)
        {
            foreach (XmlNode node in lst)
            {
                ar.Add(node.Attributes.GetNamedItem("DokumentId").InnerText + " - " + node.Attributes.GetNamedItem("Namn").InnerText);
            }
        }

        /// <summary>
        /// Builds a list with aktive patients for this company (KD3 field)
        /// 
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="ar"></param>
        private static void setPatientKodes(XmlNodeList lst, ref ArrayList ar)
        {
            foreach (XmlNode node in lst)
            {
                ar.Add(node["DokumentId"].InnerText + " - " + node["Namn"].InnerText);
            }
        }

        private static void setReceipt(XmlNode node)
        {
            if (node != null)
                mReceipt = node.Attributes.GetNamedItem("DokumentId").InnerText + " - " + node.Attributes.GetNamedItem("Namn").InnerText;
        }

        private static void setInvoice(XmlNode node)
        {
            if (node != null)
                mInvoice = node.Attributes.GetNamedItem("DokumentId").InnerText + " - " + node.Attributes.GetNamedItem("Namn").InnerText;
        }

        private static void setCustTemplate(XmlNode node)
        {
            if (node != null)
            {
                if (node["BetVillk"] != null)
                    mCustTemplate.PaymentTerms = node["BetVillk"].Attributes.GetNamedItem("ID").InnerText;
                else
                    mCustTemplate.PaymentTerms = "";

                if (node["LevVillk"] != null)
                    mCustTemplate.DeliverTerms = node["LevVillk"].Attributes.GetNamedItem("ID").InnerText;
                else
                    mCustTemplate.DeliverTerms = "";

                if (node["LevSatt"] != null)
                    mCustTemplate.WayOfDeliver = node["LevSatt"].Attributes.GetNamedItem("ID").InnerText;
                else
                    mCustTemplate.WayOfDeliver = "";

                if (node["Kategori"] != null)
                    mCustTemplate.Category = node["Kategori"].Attributes.GetNamedItem("ID").InnerText;
                else
                    mCustTemplate.Category = "";

                if (node["MomsKod"] != null)
                    mCustTemplate.VATCode = node["MomsKod"].Attributes.GetNamedItem("ID").InnerText;
                else
                    mCustTemplate.VATCode = "";

                if (node["Typ"] != null)
                    mCustTemplate.Type = node["Typ"].Attributes.GetNamedItem("ID").InnerText;
                else
                    mCustTemplate.Type = "";

                if (node["Prislista"] != null)
                    mCustTemplate.PriceList = node["Prislista"].Attributes.GetNamedItem("ID").InnerText;
                else
                    mCustTemplate.PriceList = "";

                if (node["PatientKod"] != null)
                    mCustTemplate.ViewCode = node["PatientKod"].Attributes.GetNamedItem("ID").InnerText;
                else
                    mCustTemplate.ViewCode = "";

                if (node["Betalningspåminnelse"] != null)
                {
                    if (GCF.noNULL(node["Betalningspåminnelse"].Attributes.GetNamedItem("ID").InnerText).Equals("J"))
                        mCustTemplate.PaymentReminder = true;
                    else
                        mCustTemplate.PaymentReminder = false;
                }

                if (node["Räntefaktura"] != null)
                {
                    if (GCF.noNULL(node["Räntefaktura"].Attributes.GetNamedItem("ID").InnerText).Equals("J"))
                        mCustTemplate.InterestInvoice = true;
                    else
                        mCustTemplate.InterestInvoice = false;
                }
            }
        }

        public static string DiagnosPath
        {
            get { return mDiagnosFile; }
            set { mDiagnosFile = value; }
        }

        public static string Kst
        {
            get { return mKst; }
            set { mKst = value; }
        }

        public static string BetVillkEaKont
        {
            get { return mBetVillkEaKont; }
            set { mBetVillkEaKont = value; }
        }

        public static string BetVillkEaFakt
        {
            get { return mBetVillkEaFakt; }
            set { mBetVillkEaFakt = value; }
        }

        public static string CompanyId
        {
            get { return mCompanyId; }
        }

        public static string Group
        {
            get { return mUserCode; }
        }

        public static string User
        {
            get { return mUser; }
        }

        public static bool UserDefinedConfig
        {
            get { return mUseUserKodes; }
        }

        public static bool IsThordUser
        {
            get { return mThordUser; }
        }

        public static bool EA_UseProductGroups
        {
            get { return mEA_UseProductGroups; }
        }

        public static bool AlwaysUpperCase
        {
            get { return mAlwaysUpperCase; }
        }

        public static string ThordUserId
        {
            get { return mThordUserId; }
        }

        public static string ThordPassword
        {
            get { return mThordPassword; }
        }

        public static string SMTP
        {
            get { return mSMTP; }
        }

        public static string SMTP_User
        {
            get { return mSMTP_User; }
        }

        public static string SMTP_Password
        {
            get { return mSMTP_Password; }
        }

        public static string SMTP_SupportAddress
        {
            get { return mSMTP_SupportAddress; }
        }

        public static string SMTP_FromAddress
        {
            get { return mSMTP_FromAddress; }
        }

        public static string ConfigPath
        {
            get { return mConfigPath; }
        }

        /// <summary>
        /// PID of used Garp instance
        /// 
        /// </summary>
        public static IntPtr IntPtrGarp
        {
            get { return mIntPtrGarp; }
            set { mIntPtrGarp = value; }
        }

        public static bool ShowPreferences
        {
            get { return mShowPreferences; }
        }

        public static CustTemplate getCustTemplate
        {
            get { return mCustTemplate; }
        }

        public static CopDoc[] getCopDoc
        {
            get { return mCopDoc; }
        }

        private static void setUserPreferences(XmlNodeList lst)
        {
            foreach (XmlNode node in lst)
            {
                if (node.Attributes.GetNamedItem("ID").InnerText.Equals(mUserCode))
                {
                    mNumberSerie = node["Serie"].GetAttribute("ID");
                    Kst = node.Attributes.GetNamedItem("Kst").InnerText;
                    BetVillkEaKont = node.Attributes.GetNamedItem("BetVillkEaKont").InnerText;
                    BetVillkEaFakt = node.Attributes.GetNamedItem("BetVillkEaFakt").InnerText;

                    try
                    {
                        if (node.Attributes.GetNamedItem("Thord").InnerText.Equals("J"))
                            mThordUser = true;
                        else
                            mThordUser = false;
                    }
                    catch
                    {
                        mThordUser = false;
                    }

                    try
                    {
                        setDocList(node["Dokument"].GetElementsByTagName("Kallelse"), ref alKallelse);
                        setDocList(node["Dokument"].GetElementsByTagName("ArbetsOrder"), ref alArbetsOrder);
                        setReceipt(node["Dokument"]["Kontantkvitto"]);
                        setInvoice(node["Dokument"]["Faktura"]);
                        setCustTemplate(node["Kundmall"]);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ett problem uppstod när dokument skulle läsas från configure.xml.\n\nFelmeddelande:\n" + ex.Message + "\n\n" + ex.StackTrace, "Läsning av configure.xml");
                    }

                    try
                    {
                        XmlNode nThord = node["Thord"];

                        if (nThord != null)
                        {
                            mThordUserId = nThord["User"].Attributes.GetNamedItem("Id").InnerText;
                            mThordPassword = nThord["User"].Attributes.GetNamedItem("Password").InnerText;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(null, "Det gick inte att läsa ett värde (Thord) från Configure.xml\n\nFelmeddelande:\n" + ex.Message + "\n\n" + ex.StackTrace, "Fel i Configure.xml", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    foreach (XmlNode node2 in node.SelectNodes("PatientKod"))
                        alViewCode.Add(node2.Attributes.GetNamedItem("ID").InnerText);

                    break;
                }
            }
        }
    }
}
