using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections;
using Microsoft.Web.Services3;
using Microsoft.Web.Services3.Security.Tokens;
using System.Security.Cryptography;
using Microsoft.Web.Services3.Security;
//#if DEBUG
//  using Ortoped.se.sll.bkv.externtest;
//  using Ortoped.se.sll.bkv_sabb.bkv_tstweb1;
//#else
//using Ortoped.se.sll.thord.www;
//#endif
using Ortoped.ThordService;
using Excido;
using Ortoped.HelpClasses;
using Ortoped.GarpFunctions;
using GCS;

namespace Ortoped.Thord
{
    public struct Referral
    {
        public DateTime datum;
        public DateTime giltligfran;
        public String antalar;
        public String remissid;
        public String personnr;
        public String kombikakod;
        public String status;
        public String transfer;
        public String ordinator;
        public String ordination;
        public String notering;
        public String diagnos;
        public String diagnoskod;
        public Aid[] aids;
        public string aidtypes;        // Aidtypes selected on referral
        public String handlaggare;
        public String prioritering;
        public string orderno;       // Ordernummer i Garp
    }

    public struct Aid
    {
        public int aidoid;
        public int aidtypeoid;
        public string isonumber;
        public string externalaidid;
        public string name;
        public string description;
        public bool RemoveMe;         // S�tts p� ett hj�lpmedel som skall tas bort fr�n Thord
        public bool aidtypeoidSpecified;
        public bool needstepSpecified;
        public int needstep;
        public bool CreatedInThord;
        public string orderno;
        public string row;
        public Part[] parts;
        public string ProdStat;
        public bool FirstTimePatient;
    }

    public struct Part
    {
        public int partoid;
        public int aidoid;
        public bool CreatedInThord;
        public bool RemoveMe;         // S�tts p� en part som skall tas bort fr�n Thord
        public bool priceSpecified;
        public decimal price;
        public bool countSpecified;
        public decimal count;
        public string positionid;
        public string orderno;
        public string row;
    }

    /// <summary>
    /// Summary description for ThordFunctions.
    /// </summary>
    public class ThordFunctions
    {
        //private ThordIntegration2 tdProxy;
        //public static ThordIntegration2 ext;

        private ThordIntegration20050308Client tdProxy;
        public static ThordIntegration20050308Client ext;

        /// <summary>
        /// The constructor initialises the class with user/pass
        /// </summary>
        public ThordFunctions()
        {
            initThord(Config.ThordUserId, Config.ThordPassword);
        }
        /// <summary>
        /// Overrides default constructor with supplied user/pass
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="password">password</param>
        public ThordFunctions(string user, string password)
        {
            initThord(user, password);
        }

        /// <summary>
        /// Create the proxy and logon to Thord
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="password">password</param>
        private void initThord(string user, string password)
        {
            try
            {
                //Ortoped.se.sll.thord.www.
                ThordService.ThordIntegration20050308Client client = new ThordService.ThordIntegration20050308Client();

                tdProxy = new ThordIntegration20050308Client();//ThordIntegration2();
                UsernameToken ut = new UsernameToken(user, password, PasswordOption.SendPlainText);

                //tdProxy.RequestSoapContext.Security.Tokens.Add(ut); 
                //tdProxy.RequestSoapContext.Security.Timestamp.TtlInSeconds = 600;
                //tdProxy.RequestSoapContext.Security.MustUnderstand = false;
                //tdProxy.UseDefaultCredentials = true;
                //tdProxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            }
            catch (Exception e)
            {
                MessageBox.Show(null, "Fel vid kommunikation med Thord:\n\n" + e.Message, "Kommunikationsfel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log4Net.Logger.loggError(e, "Fel vid initsiering av Thord", Config.User, "ThordFunctions.initThord");
            }
        }

        /// <summary>
        /// Call Thord and fetch all existing ISO codes
        /// </summary>
        /// <returns>Array of strings</returns>
        public string[] getAllISOCode()
        {
            ArrayList ar = new ArrayList();

            isocodes iso = tdProxy.GetAllISOCode();

            for (int i = 0; i < iso.isocode.Length; i++)
                ar.Add(iso.isocode[i].name);

            string[] s = new string[ar.Count];
            s = (string[])ar.ToArray(typeof(string));

            return s;
        }

        public string[] getAllISOCodeAndNames()
        {
            ArrayList ar = new ArrayList();

            isocodes iso = tdProxy.GetAllISOCode();

            for (int i = 0; i < iso.isocode.Length; i++)
                ar.Add(iso.isocode[i].isonumber + " - " + iso.isocode[i].name);

            string[] s = new string[ar.Count];
            s = (string[])ar.ToArray(typeof(string));

            return s;
        }

        /// <summary>
        /// Call Thord and fetch all existing Aid types
        /// </summary>
        /// <returns>array of strings</returns>
        public string[] getAllAidTypes()
        {
            ArrayList ar = new ArrayList();

            aidtypes aids = tdProxy.GetAllAidTypes();

            for (int i = 0; i < aids.aidtype.Length; i++)
            {
                string aidinfo = "";
                aidinfo = aids.aidtype[i].oid.ToString() + " " + aids.aidtype[i].name;
                ar.Add(aidinfo);
                //ar.Add(aids.aidtype[i].name);
            }
            string[] s = new string[ar.Count];
            s = (string[])ar.ToArray(typeof(string));

            return s;
        }

        public string[] getAllNeedsteps()
        {
            ArrayList ar = new ArrayList();
            needsteps ndstp = tdProxy.GetAllNeedSteps();

            for (int i = 0; i < ndstp.needstep.Length; i++)
            {
                string info = "";
                info = ndstp.needstep[i].oid.ToString() + " " + ndstp.needstep[i].name.Substring(2) + "  (" + ndstp.needstep[i].needstair + ")";
                ar.Add(info);
            }

            string[] s = new string[ar.Count];
            s = (string[])ar.ToArray(typeof(string));

            return s;
        }

        /// <summary>
        /// Test method
        /// </summary>
        /// <returns>hello</returns>
        public string helloSecretThord()
        {
            return tdProxy.HelloSecretThord();
        }

        /// <summary>
        /// Test method
        /// </summary>
        /// <returns>hello</returns>
        public string helloThord()
        {
            try
            {
                return tdProxy.HelloThord();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Calls Thord and fetches all existing Referrals that matches
        /// the selection
        /// </summary>
        /// <param name="from">Date from</param>
        /// <param name="to">Date to</param>
        /// <param name="rsv">Status value of Referral</param>
        /// <returns>Array of referrals</returns>
        public Referral[] getRefs(DateTime from, DateTime to, ReferralStatusValues1 rsv)
        {
            ArrayList rfList = new ArrayList();
            referralselection rs = new referralselection();
            critera cr = new critera();
            referrals reff = null;

            cr.status = rsv;
            cr.fromDate = from;
            cr.toDate = to;

#if DEBUG
            cr.fromDateSpecified = true;
            cr.toDateSpecified = true;
#else
        cr.fromDateSpecified = false;
			  cr.toDateSpecified = false;
#endif

            cr.statusSpecified = true;
            rs.critera = cr;

            try
            {
                reff = tdProxy.GetReferrals(rs);
            }
            catch (Exception e)
            {
                MessageBox.Show(null, "Fel vid kommunikation med Thord:\n\n" + e.Message, "Kommunikationsfel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log4Net.Logger.loggError(e, "Fel vid skapande av proxy", Config.User, "ThordFUnctions.getRefs");
                return null;
            }

            if (reff.referral != null)
            {
                foreach (ReferralDefinition rf in reff.referral)
                {
                    Referral r = new Referral();
                    r.datum = rf.createddate;
                    r.personnr = rf.personnumber;
                    r.remissid = rf.referralnumber;
                    r.prioritering = rf.priority.ToString();

                    try
                    {
                        r.kombikakod = rf.kombika.kombikacode;
                    }
                    catch { }
                    r.status = ECS.noNULL(rf.status.ToString());
                    r.ordinator = ECS.noNULL(rf.prescribername);
                    r.diagnos = ECS.noNULL(rf.diagnosis);
                    r.notering = ECS.noNULL(rf.comment);
                    r.giltligfran = rf.referraldate;
                    r.antalar = ECS.noNULL(rf.validtime.ToString());
                    r.diagnoskod = rf.reducedfunction.ToString();
                    r.handlaggare = ECS.noNULL(rf.desiredadministrator);
                    r.aidtypes = "";

                    //h�mta textv�rdena f�r aidtypes, dvs "Inl�gg/Fotb�dd", "Protes" m.fl
                    if (rf.aidtypes.aidtype.Length > 0)
                    {
                        for (int i = 0; i < rf.aidtypes.aidtype.Length; i++)
                        {
                            switch (rf.aidtypes.aidtype[i])
                            {
                                case 1:
                                    if (r.aidtypes.Length == 0)
                                        r.aidtypes = "Inl�gg/Fotb�dd";
                                    else
                                        r.aidtypes = r.aidtypes + ", Inl�gg/Forb�dd";
                                    break;

                                case 2:
                                    if (r.aidtypes.Length == 0)
                                        r.aidtypes = "Ortopediska skor/Bekv�mskor";
                                    else
                                        r.aidtypes = r.aidtypes + ", Ortopediska skor/Bekv�mskor";
                                    break;

                                case 3:
                                    if (r.aidtypes.Length == 0)
                                        r.aidtypes = "Ortos";
                                    else
                                        r.aidtypes = r.aidtypes + ", Ortos";
                                    break;

                                case 4:
                                    if (r.aidtypes.Length == 0)
                                        r.aidtypes = "Protes";
                                    else
                                        r.aidtypes = r.aidtypes + ", Protes";
                                    break;

                                case 5:
                                    if (r.aidtypes.Length == 0)
                                        r.aidtypes = "Behandlingsskor";
                                    else
                                        r.aidtypes = r.aidtypes + ", Behandlingsskor";
                                    break;

                                case 6:
                                    if (r.aidtypes.Length == 0)
                                        r.aidtypes = "�vrigt";
                                    else
                                        r.aidtypes = r.aidtypes + ", �vrigt";
                                    break;

                                case 7:
                                    if (r.aidtypes.Length == 0)
                                        r.aidtypes = "Behandlande ortos";
                                    else
                                        r.aidtypes = r.aidtypes + ", Behandlande ortos";
                                    break;

                                case 8:
                                    if (r.aidtypes.Length == 0)
                                        r.aidtypes = "Br�ckband";
                                    else
                                        r.aidtypes = r.aidtypes + ", Br�ckband";
                                    break;

                                case 9:
                                    if (r.aidtypes.Length == 0)
                                        r.aidtypes = "Olikstora skor";
                                    else
                                        r.aidtypes = r.aidtypes + ", Olikstora skor";
                                    break;

                                case 10:
                                    if (r.aidtypes.Length == 0)
                                        r.aidtypes = "�ndring av egna skor";
                                    else
                                        r.aidtypes = r.aidtypes + ", �ndring av egna skor";
                                    break;

                                default:
                                    Console.Write("Denna idkod saknas!");
                                    break;
                            }
                        }
                    }
                    rfList.Add(r);
                }
            }

            return (Referral[])rfList.ToArray(typeof(Referral));
        }

        /// <summary>
        /// Calls Thord and fetches information for the supplied persons
        /// </summary>
        /// <param name="pnr">array of person numbers</param>
        /// <returns>personinformation objekt</returns>
        public personinformation getPersonInfo(string[] pnr)
        {

            personinformation pi = new personinformation();

            personselection ps = new personselection();
            ps.personnumber = pnr;
            pi = tdProxy.GetPersonInformation(ps);

            return pi;
        }

        /// <summary>
        /// Connect to Thord and update referrals with new data
        /// </summary>
        /// <param name="refs">Array of Referrals</param>
        /// <param name="garpaids">Array of New Aid Definitions</param>
        public bool sendReferral(Referral referral)
        {
            referralupdates refupdate = new referralupdates();
            ReferralUpdateDefinition[] ru = new ReferralUpdateDefinition[1];
            AidsUpdateDefinition aids = new AidsUpdateDefinition();
            referrals refReturned;
            OrderRowFunc oOR = new OrderRowFunc();

            // If aids exists
            if (referral.aids.Length > 0)
            {
                if (referral.aids[0].CreatedInThord)
                {
                    aids.aidupdate = createAidUpdateObject(referral.aids[0]);
                }
                else
                {
                    aids.newaid = createAidNewObject(referral.aids[0]);
                }
            }

            ru[0] = new ReferralUpdateDefinition();
            ru[0].referralnumber = referral.remissid;
            ru[0].aids = aids;

            refupdate.referralupdate = ru;

            try
            {
                // Talk to the old man
                refReturned = tdProxy.UpdateReferrals(refupdate);

                foreach (AidDefinition ad in refReturned.referral[0].aids.aid)
                {
                    if ((referral.aids[0].orderno.PadRight(6, '_') + referral.aids[0].row.Trim()).Equals(ad.externalaidid))
                    {
                        oOR.updateOid(ad.externalaidid.Substring(0, 6).Replace("_", ""), ad.externalaidid.Substring(6), ad.aidoid.ToString(), "0");

                        // If any parts are present
                        if (ad.parts != null)
                        {
                            foreach (PartDefinition p in ad.parts.part)
                            {
                                oOR.updateOid(p.externalpartid.Substring(0, 6).Replace("_", ""), p.externalpartid.Substring(6), ad.aidoid.ToString(), p.partoid.ToString());
                            }
                        }
                    }

                    // If whole aid should be removed
                    // OBS! This code must exists after ordinary UpdateReferrals, all parts must be
                    // removed before vi can remove the aid.
                    if (referral.aids[0].RemoveMe)
                    {
                        aids = new AidsUpdateDefinition();
                        ru[0] = new ReferralUpdateDefinition();

                        ru[0].referralnumber = referral.remissid;
                        ru[0].aids = aids;
                        refupdate.referralupdate = ru;

                        aids.removeaid = createAidRemoveObject(referral.aids[0]);

                        // Talk to the old man
                        try
                        {
                            refReturned = tdProxy.UpdateReferrals(refupdate);
                            MessageBox.Show("Hj�lpmedlet (" + referral.aids[0].aidoid.ToString() + ") har tagits bort fr�n Thord", "Meddelande fr�n Thord");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Vid radering av hj�lpmedel uppstod ett fel:\n" + ex.Message, "Meddelande fr�n Thord");
                            Log4Net.Logger.loggError(ex, "THORD: SendReferral/RemoveReferral:", Config.User, "ThordFunction.sendReferral");
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Meddelande fr�n Thord");
                Log4Net.Logger.loggError(ex, "THORD: SendReferral", Config.User, "");
                return false;
            }
        }

        /// <summary>
        /// Creates a NewAidDefinition with all parts inkluded. 
        /// </summary>
        /// <param name="referral"></param>
        /// <returns></returns>
        private NewAidDefinition[] createAidNewObject(Aid aid)
        {
            NewAidDefinition[] na = new NewAidDefinition[1];
            NewPartsDefinition parts = new NewPartsDefinition();

            na[0] = new NewAidDefinition();
            na[0].aidtypeoid = aid.aidtypeoid;
            na[0].aidtypeoidSpecified = true;
            na[0].isonumber = aid.isonumber;
            na[0].description = aid.description;
            na[0].name = aid.name;
            na[0].needstep = aid.needstep;
            na[0].needstepSpecified = aid.needstepSpecified;
            na[0].externalaidid = aid.orderno.PadRight(6, '_') + aid.row.Trim();
            na[0].isFirstTimePatientSpecified = true;
            na[0].isFirstTimePatient = aid.FirstTimePatient;
            parts.newpart = createPartNewDefinition(aid.parts);
            na[0].parts = parts;

            return na;
        }

        private NewPartDefinition[] createPartNewDefinition(Part[] parts)
        {
            ArrayList al = new ArrayList();
            NewPartDefinition np;

            if (parts == null)
                return null;

            foreach (Part p in parts)
            {
                if (!p.CreatedInThord && !p.RemoveMe)
                {
                    np = new NewPartDefinition();
                    np.positionid = p.positionid;
                    np.count = p.count;
                    np.countSpecified = p.countSpecified;
                    np.price = p.price;
                    np.priceSpecified = p.priceSpecified;
                    np.externalpartid = p.orderno.PadRight(6, '_') + p.row.Trim();
                    al.Add(np);
                }
            }

            return (NewPartDefinition[])al.ToArray(typeof(NewPartDefinition));
        }

        private PartUpdateDefinition[] createPartUpdateDefinition(Part[] parts)
        {
            ArrayList al = new ArrayList();
            PartUpdateDefinition pu;

            foreach (Part p in parts)
            {
                if (p.CreatedInThord && !p.RemoveMe)
                {
                    if (p.partoid != 0)
                    {
                        pu = new PartUpdateDefinition();
                        pu.positionid = p.positionid;
                        pu.partoid = p.partoid;
                        pu.partoidSpecified = true;
                        pu.count = p.count;
                        pu.countSpecified = p.countSpecified;
                        pu.price = p.price;
                        pu.priceSpecified = p.priceSpecified;
                        pu.externalpartid = p.orderno.PadRight(6, '_') + p.row.Trim();
                        al.Add(pu);
                    }
                    else
                        MessageBox.Show("Artikeln (" + p.positionid + ") saknar identitet i Thord (PartOid). Denna artikel kan inte\nuppdateras till Thord utan m�ste hanteras manuellt", "Kan inte posta till Thord");

                }
            }

            return (PartUpdateDefinition[])al.ToArray(typeof(PartUpdateDefinition));
        }

        private RemoveAidDefinition[] createAidRemoveObject(Aid aid)
        {
            RemoveAidDefinition[] au = new RemoveAidDefinition[1];

            au[0] = new RemoveAidDefinition();
            au[0].aidoid = aid.aidoid;
            au[0].aidoidSpecified = true;

            return au;
        }

        private AidUpdateDefinition[] createAidUpdateObject(Aid aid)
        {
            AidUpdateDefinition[] au = new AidUpdateDefinition[1];
            NewAidDefinition[] na = new NewAidDefinition[1];
            PartsUpdateDefinition parts = new PartsUpdateDefinition();

            if (aid.aidoid != 0)
            {
                au[0] = new AidUpdateDefinition();
                au[0].aidtypeoid = aid.aidtypeoid;
                au[0].aidtypeoidSpecified = true;
                au[0].isonumber = aid.isonumber;
                au[0].description = aid.description;
                au[0].name = aid.name;
                au[0].aidoid = aid.aidoid;
                au[0].aidoidSpecified = true;
                au[0].externalaidid = aid.orderno.PadRight(6, '_') + aid.row.Trim();
                au[0].isFirstTimePatientSpecified = true;
                au[0].isFirstTimePatient = aid.FirstTimePatient;
                if (ECS.noNULL(aid.ProdStat).Equals("1"))
                {
                    au[0].statusSpecified = true;
                    au[0].status = AidStatusValues.KLARFORFAKTURERING;
                }

                parts.newpart = createPartNewDefinition(aid.parts);
                parts.partupdate = createPartUpdateDefinition(aid.parts);
                parts.removepart = createPartRemoveDefinition(aid.parts);
                au[0].parts = parts;
            }
            else
            {
                MessageBox.Show("Hj�lpmedlet (" + aid.isonumber + ") saknar identitet i Thord (AidOid). Detta hj�lpmedel kan inte\nuppdateras till Thord utan m�ste hanteras manuellt", "Kan inte posta till Thord");
            }

            return au;
        }

        private RemovePartDefinition[] createPartRemoveDefinition(Part[] parts)
        {
            ArrayList al = new ArrayList();
            RemovePartDefinition pr;

            foreach (Part p in parts)
            {
                if (p.CreatedInThord && p.RemoveMe)
                {
                    if (p.partoid != 0)
                    {
                        pr = new RemovePartDefinition();
                        pr.partoid = p.partoid;
                        pr.partoidSpecified = true;
                        al.Add(pr);
                    }
                    else
                        MessageBox.Show("Artikeln (" + p.positionid + ") saknar identitet i Thord (PartOid). Denna artikel kan inte\nuppdateras till Thord utan m�ste hanteras manuellt", "Kan inte posta till Thord");
                }
            }

            return (RemovePartDefinition[])al.ToArray(typeof(RemovePartDefinition));
        }

        /// <summary>
        /// Connect to Thord and change status of referrals to "MOTTAGEN"
        /// </summary>
        /// <param name="refs">Array of referrals</param>
        public void sendTransferedReferrals(Referral[] refs)
        {
            referralupdates refupdate = new referralupdates();
            ReferralUpdateDefinition[] refdef = new ReferralUpdateDefinition[refs.Length];
            int i = 0;

            if (refs.Length != 0)
            {
                foreach (Referral rf in refs)
                {
                    ReferralUpdateDefinition rd = new ReferralUpdateDefinition();

                    rd.referralnumber = rf.remissid;
                    rd.statusSpecified = true;
                    rd.status = ReferralStatusValues.MOTTAGEN;
                    refdef[i] = rd;
                    i++;
                }
                try
                {
                    refupdate.referralupdate = refdef;
                    tdProxy.UpdateReferrals(refupdate);
                }
                catch { }
            }
        }


    }
}

