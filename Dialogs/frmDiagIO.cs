using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ortoped.Definitions;
using Ortoped.HelpClasses;

namespace Ortoped.Dialogs
{
  public partial class frmDiagIO : Form
  {
    public frmDiagIO()
    {
      InitializeComponent();
    }

    public frmDiagIO(PurchaseDefenitions[] pDef)
    {
      InitializeComponent();

      ListViewItem lw;
      foreach (PurchaseDefenitions p in pDef)
      {
        lw = new ListViewItem(p.IO_No);
        lw.SubItems.Add(p.CustomerOrderNo);
        lw.SubItems.Add(p.SupplierNo);
        lw.SubItems.Add(p.SupplierName);
        lw.SubItems.Add(p.IO_Status);

        switch (p.IO_Status)
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

        lwIO.Items.Add(lw);          
      }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void lwIO_ColumnClick(object sender, ColumnClickEventArgs e)
    {
      this.lwIO.ListViewItemSorter = new ListViewItemComparer(e.Column);
    }
  }
}
