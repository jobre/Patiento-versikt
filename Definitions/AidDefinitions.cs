using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using GCS;

namespace Ortoped.Definitions
{
    public class Aid
    {
        public string OrderNo { get; set; }
        public string AidNr { get; set; }
        public string Antal { get; set; }
        public string APris { get; set; }
        public string PaymentTerms { get; set; }
        public string Text { get; set; }
        public double OwnFee { get; set; }                   // Egenavgift, summerad på hjälpmedlet
        public double SumInternalProducts { get; set; }      // Sumering av alla interna produkters priser
        public double SumExternalProducts { get; set; }      // Sumering av alla externa produkters priser
        public OrderRowDefinitions.OrderRow Product { get; set; }     // Orderow that shows in aidlist (har viewstate = true)
        public List<OrderRowDefinitions.OrderRow> OrderRows { get; set; }


        //public struct Aid
        //{
        //    public string OrderNo;
        //    public string AidNr;
        //    public string Antal;
        //    public string APris;
        //    public double OwnFee;                   // Egenavgift, summerad på hjälpmedlet
        //    public double SumInternlaProducts;      // Sumering av alla interna produkters priser
        //    public double SumExternalProducts;      // Sumering av alla externa produkters priser
        //    public OrderRowDefinitions.OrderRow Product;     // Orderow that shows in aidlist (har viewstate = true)
        //    public List<OrderRowDefinitions.OrderRow> OrderRows;

        //    /// <summary>
        //    /// Konverterar en OrderRow till ListViewItem. Används med fördel
        //    /// till att uppdatera ListView med en array av OrderRow
        //    /// </summary>
        //    /// <param name="or"></param>
        //    /// <returns></returns>
 
        //}

        /// <summary>
        /// Konverterar en OrderRow till ListViewItem. Används med fördel
        /// till att uppdatera ListView med en array av OrderRow
        /// </summary>
        /// <param name="or"></param>
        /// <returns></returns>
        public static ListViewItem[] convertToListView(List<Aid> aids, Ortoped.GarpFunctions.OrderRowFunc orf)
        {
            List<ListViewItem> lstLvi = new List<ListViewItem>();

            foreach(Aid a in aids)
            {
                try
                {
                    ListViewItem lw = new ListViewItem(a.AidNr);

                    lw = new ListViewItem(a.Product.AidNr);
                    lw.SubItems.Add(a.Product.Artikel);
                    lw.SubItems.Add(a.Product.ProductName);
                    lw.SubItems.Add(a.Product.Antal);
                    lw.SubItems.Add(a.SumExternalProducts.ToString());
                    lw.SubItems.Add(a.OwnFee.ToString());
                    lw.SubItems.Add(a.Product.SelectedHandler);
                    lw.SubItems.Add(a.Product.Levstatus);
                    lw.SubItems.Add(a.Product.DeliverDate);
                    lw.SubItems.Add(a.Product.InvoiceNo);
                    lw.SubItems.Add(a.Product.InvoiceDate);
                    if (!GCF.noNULL(a.Text).Equals(""))
                        lw.SubItems.Add("Ja");
                    else
                        lw.SubItems.Add("");
                    if (a.Product.Urgent)
                        lw.SubItems.Add("Akut");
                    else
                        lw.SubItems.Add("");
                    lw.SubItems.Add(a.Product.ProductionTitle);

                    lw.Tag = a.Product.Rad;

                    switch (a.Product.Levstatus)
                    {
                        case "4":
                            lw.BackColor = System.Drawing.Color.FromArgb(CommonDefinitions.PART_DELIVERED);
                            break;
                        case "5":
                            lw.BackColor = System.Drawing.Color.FromArgb(CommonDefinitions.DELIVERED);
                            break;
                        default:
                            break;
                    }

                    lw.SubItems.Add(a.Text);

                    lstLvi.Add(lw);
                }
                catch (Exception e)
                {
                    Log4Net.Logger.loggError(e, "Error whlie creating ListView", "", "covertToListView");
                }
            }

            return lstLvi.ToArray();
        }

        public static ListViewItem[] convertToAnotherSmallListView(List<Aid> aids)
        {
            List<ListViewItem> lstLvi = new List<ListViewItem>();

            foreach (Aid a in aids)
            {
                ListViewItem lw = new ListViewItem(a.AidNr);
                lw = new ListViewItem(a.Product.AidNr);
                lw.SubItems.Add(a.Product.Artikel);
                lw.SubItems.Add(a.Product.ProductName);

                lstLvi.Add(lw);
            }

            return lstLvi.ToArray();
        }

    }
}
