using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Ortoped.Definitions
{
    public class OrderRowDefinitions
    {

        public struct OwnFee
        {
            public string OrderNo;
            public string AidId;
            public string RowNo;
            public double Amount;
            public string PatientsOrderNo;
            public string PatientsRowNo;
            public string PaymentTerms;
            public bool Delivered;
        }

        public struct OrderRow
        {
            public string OrderNo;
            public string Rad;
            public string AidNr;
            public string Egenavgift;
            public string EA_ProductGroup;          // Productgroup for ownfee (optional, if not set = blank)
            public bool ViewInList;					// Skall artikeln visas i �versiktsfliken?
            public bool Warrenty;					// Garanti
            public string Artikel;
            public string ProductName;
            public string Antal;
            public string APris;
            public string LevTid;
            public string InkStat;					// Ink�psstatus
            public string Enhet;					// Enhet
            public string AidDate;					// Datum d� orderraden skapades
            public string Prodstatus;				// Produktionsstatus
            public string Levstatus;				// Leveransstatus
            public string DeliverMode;			    // Leveranss�tt
            public string SelectedHandler;
            public string AccountNo;				// Kontonummer
            public string Text;
            public string AidsText;					// Text kopplat till hj�lpmedelsid
            public bool Beloppsrad;
            public string InvoiceNo;
            public string InvoiceDate;
            public string DeliverDate;			    // Faktiskt leveransdatum
            public string Thord_NeedStep;		    // Behovstrappa
            public string ProductionTitle;          // Produktion
            public string Holder;                   // Nuvarande handl�ggare
            public bool Urgent;                     // Akut
            public DateTime PromisedDeliverDate;    // Production
            public int AidOid;                      // Unik identitet i Thord (hj�lpmedel)
            public int PartOid;                     // Unik identitet i Thord (position)
            public bool CreatedInThord;             // Flagga f�r om den �r skapad i Thord (X2F = 1 inneb�r att den �r skapad i Thord)
            public string RemissNo;                 // Remissnummer om det �r en Thord order
            public bool FirstTimePatient;           // THORD: F�rstag�ngspatient

            public static ListViewItem[] convertToSmallListView(OrderRow[] or)
            {
                ListViewItem[] lw = new ListViewItem[or.Length];

                for (int i = 0; i < lw.Length; i++)
                {
                    lw[i] = new ListViewItem(or[i].Artikel);
                    lw[i].SubItems.Add(or[i].ProductName);
                    lw[i].SubItems.Add(or[i].Antal);
                    lw[i].SubItems.Add(or[i].Rad).Name = or[i].Rad;
                    lw[i].Tag = or[i].Rad;
                    if (or[i].ViewInList)
                        lw[i].Checked = true;
                }

                return lw;
            }


            /// <summary>
            /// Konvertera en ArrayList till OrderRow object.
            /// </summary>
            /// <param name="arr"></param>
            /// <returns></returns>
            public static OrderRow[] convertFromArray(ArrayList arr)
            {
                OrderRow[] or = new OrderRow[arr.Count];

                for (int i = 0; i < arr.Count; i++)
                {
                    or[i] = (OrderRow)arr[i];
                }
                return or;
            }
        }

    }
}
