using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ortoped.HelpClasses;

namespace Ortoped.Dialogs
{
  public partial class frmDiagReceipt : Form
  {
    // R=Receipt, I=Invoice
    public string sVal = "", sName = "", sAddress1 = "", sAddress2 = "", sCity = "";

    public frmDiagReceipt()
    {
      InitializeComponent();
    }

    public frmDiagReceipt(string pnr, string fullname)
		{
			InitializeComponent();
			txtPnr.Text = pnr;
			txtNamn.Text = fullname;
		}

    private void frmDiagReceipt_Load(object sender, EventArgs e)
    {
      if (Config.AlwaysUpperCase)
      {
        txtNamn.CharacterCasing = CharacterCasing.Upper;
        txtAdress1.CharacterCasing = CharacterCasing.Upper;
        txtAdress2.CharacterCasing = CharacterCasing.Upper;
        txtOrt.CharacterCasing = CharacterCasing.Upper;
      }
    }

    private void cmdReceipt_Click(object sender, EventArgs e)
    {
      sVal = "R";
      fillVar();
      this.Close();
    }

    private void cmdInvoice_Click(object sender, EventArgs e)
    {
      sVal = "I";
      fillVar();
      this.Close();
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void fillVar()
    {
      sName = txtNamn.Text;
      sAddress1 = txtAdress1.Text;
      sAddress2 = txtAdress2.Text;
      sCity = txtOrt.Text;
    }
  }
}