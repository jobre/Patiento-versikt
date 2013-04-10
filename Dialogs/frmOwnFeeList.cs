using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ortoped.Definitions;
using Excido;

namespace Ortoped.Dialogs
{
  public partial class frmOwnFeeList : Form
  {
    public OrderRowDefinitions.OwnFee[] ownfees;
    public OrderRowDefinitions.OwnFee selectedOwnFee;

    public frmOwnFeeList()
    {
      InitializeComponent();
    }


    private void btnOK_Click(object sender, EventArgs e)
    {
      if(lwOwnFees.SelectedItems.Count > 0)
        selectedOwnFee = ownfees[lwOwnFees.SelectedItems[0].Index];
    }

    private void frmOwnFeeList_Load(object sender, EventArgs e)
    {
      foreach(OrderRowDefinitions.OwnFee of in ownfees)
      {
        ListViewItem lw = new ListViewItem(of.OrderNo);
        lw.SubItems.Add(of.AidId);
        lw.SubItems.Add(ECS.doubleToString(of.Amount,'.'));
        lwOwnFees.Items.Add(lw);
      }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}