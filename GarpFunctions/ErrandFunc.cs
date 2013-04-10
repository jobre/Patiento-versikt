using System;
using System.Collections;
using Ortoped;
using System.Windows.Forms;
using Ortoped.Definitions;
using Excido;
using System.Globalization;

namespace Ortoped.GarpFunctions
{
    /// <summary>
    /// Summary description for ErrandFunc.
    /// </summary>
    public class ErrandFunc : GarpGEM.ErrandCOM
    {
        public ErrandFunc()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public struct Errand
        {
            public string ErrandID;
            public string StartDatum;
            public string Starttid;
            public string Tidsperiod;
            public string Aktivitet;
            public string Kontaktperson;
            public string Ansvarig;
            public string Prioritet;
            public string Refnr;
            public string Status;
            public string Sekretess;
            public string Dagar;
            public string Kod1;
            public string Kod2;

            /// <summary>
            /// Konverterar en Errand till ListViewItem. Används med fördel
            /// till att uppdatera ListView med en array av Errand
            /// </summary>
            /// <param name="or"></param>
            /// <returns></returns>
            public static ListViewItem[] convertToErrand(Errand[] e)
            {
                //				ArrayList al = new ArrayList();
                ListViewItem[] lw = new ListViewItem[e.Length];

                for (int i = 0; i < lw.Length; i++)
                {
                    lw[i] = new ListViewItem(e[i].StartDatum);
                    lw[i].SubItems.Add(e[i].Starttid);
                    lw[i].SubItems.Add(e[i].Tidsperiod);
                    lw[i].SubItems.Add(e[i].Kontaktperson); //.PadLeft(10).Substring(0, 6).Trim() + " - " + e[i].Kontaktperson.PadLeft(10).Substring(6));
                    lw[i].SubItems.Add(e[i].Ansvarig);
                    lw[i].SubItems.Add(e[i].ErrandID);
                    if (ECS.noNULL(e[i].Status).Equals("1"))
                        lw[i].BackColor = System.Drawing.Color.FromArgb(CommonDefinitions.STATUS_RECEIVED);

                    if (ECS.noNULL(e[i].Status).Equals("2"))
                        lw[i].BackColor = System.Drawing.Color.FromArgb(CommonDefinitions.STATUS_ABSENT);

                    if (ECS.noNULL(e[i].Status).Equals("3"))
                        lw[i].BackColor = System.Drawing.Color.FromArgb(CommonDefinitions.STATUS_DONE);

                    if (ECS.noNULL(e[i].Status).Equals("4"))
                        lw[i].BackColor = System.Drawing.Color.FromArgb(CommonDefinitions.STATUS_CANCELED);
                }

                return lw;
            }

            /// <summary>
            /// Konvertera en ArrayList till Errand object.
            /// </summary>
            /// <param name="arr"></param>
            /// <returns></returns>
            public static Errand[] convertFromArray(ArrayList arr)
            {
                Errand[] e = new Errand[arr.Count];

                for (int i = 0; i < arr.Count; i++)
                {
                    e[i] = (Errand)arr[i];
                }
                return e;
            }

        }


        /// <summary>
        /// Hämtar alla ärenden som finns på denna patient och order.
        /// 
        /// </summary>
        /// <param name="cust"></param>
        /// <param name="onr"></param>
        /// <returns></returns>
        public Errand[] getErrands(string cust, string onr)
        {
            ArrayList s = new ArrayList();

            if (findFirstErrand(cust))
            {
                do
                {
                    s.Add(fillErrand());
                }
                while (!nextErrand());
            }
            return Errand.convertFromArray(s);
        }

        /// <summary>
        /// Hämtar alla ärenden på denna patient och hjälpmedel
        /// 
        /// </summary>
        /// <param name="cust"></param>
        /// <param name="onr"></param>
        /// <param name="aidid"></param>
        /// <returns></returns>
        public Errand[] getErrandsOnAid(string cust, string onr, string aidid)
        {
            ArrayList s = new ArrayList();

            if (findFirstErrand(cust))
            {
                do
                {
                    if (Kontaktperson.Trim().Equals(onr.PadRight(6) + aidid))
                        s.Add(fillErrand());
                }
                while (!nextErrand());
            }
            return Errand.convertFromArray(s);
        }

        /// <summary>
        /// Hämta alla ärenden på denna patient som inte är kopplade till någon order.
        /// Innebär alla ärenden med blankt i fältet Refnr.
        /// </summary>
        /// <param name="cust"></param>
        /// <param name="onr"></param>
        /// <returns></returns>
        public Errand[] getUnboundErrands(string cust, string onr)
        {
            ArrayList al = new ArrayList();
            DateTime dt;
            string[] s = { "yyMMdd", "yyyyMMdd", "yyyy-MM-dd" };

            if (findFirstErrand(cust))
            {
                do
                {
                    if (Kontaktperson.Trim().Equals(""))
                    {
                        // Check date, must be correct format and always today or later
                        if (DateTime.TryParseExact(StartDatum, s, new CultureInfo("sv-SE"), DateTimeStyles.AssumeLocal, out dt))
                        {
                            if (dt.CompareTo(DateTime.Today) >= 0)
                                al.Add(fillErrand());
                        }
                    }
                }
                while (!nextErrand());
            }
            return Errand.convertFromArray(al);
        }

        /// <summary>
        /// Uppdatera ett ärende med ordernummer och hjälpmedelsid
        /// 
        /// </summary>
        /// <param name="errand"></param>
        /// <param name="customer"></param>
        /// <param name="onr"></param>
        public void updateErrandWithOrderNumber(string errand, string customer, string onr, string aidid)
        {
            string sUpdateString = "";

            if (ECS.noNULL(aidid).Equals(""))
                sUpdateString = onr.PadRight(6).Trim();
            else
                sUpdateString = onr.PadRight(6).Trim() + "-" + aidid;

            mErrand.Kundnr = customer;
            mErrand.ArkivId = errand;
            mErrand.Read();

            mErrand.ArkivId = errand;
            mErrand.Kontaktperson = sUpdateString;
            mErrand.Kundnr = mErrand.Kundnr;
            mErrand.Typ = "0";
            mErrand.DateFrom = mErrand.DateFrom;
            mErrand.DateTo = mErrand.DateTo;
            mErrand.TimeFrom = mErrand.TimeFrom;
            mErrand.TimeTo = mErrand.TimeTo;
            mErrand.Update();
        }

        public void showCalendar()
        {
            app.SelectMenuItem(224);
            app.Visible = true;
        }

        private Errand fillErrand()
        {
            Errand e = new Errand();

            e.StartDatum = StartDatum;
            e.Starttid = Starttid;
            e.Tidsperiod = Tidsperiod;
            e.Aktivitet = Aktivitet;
            e.Kontaktperson = Kontaktperson;
            e.Ansvarig = getHandlerNameById(Ansvarig);
            e.Prioritet = Prioritet;
            e.Refnr = Refnr;
            e.Status = Status;
            e.Sekretess = Sekretess;
            e.ErrandID = ArkivID;
            e.Dagar = Dagar;
            e.Kod1 = Kod1;
            e.Kod2 = Kod2;

            return e;
        }

    }
}
