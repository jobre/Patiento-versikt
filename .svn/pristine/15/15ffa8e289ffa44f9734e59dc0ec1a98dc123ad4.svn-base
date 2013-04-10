using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ortoped
{
  public partial class frmProgress : Form
  {
//    public string sProgText;
    private BackgroundWorker mWorker;

    public frmProgress()
    {
      InitializeComponent();
    }

    public frmProgress(string progtext, ref BackgroundWorker worker)
    {
      InitializeComponent();
      labProgText.Text = progtext;
      mWorker = worker;
    }

    private void frmProgress_Load(object sender, EventArgs e)
    {
      prgbMain.Minimum = 0;
      prgbMain.Maximum = 200;
      prgbMain.Step = 1;

      timer1.Start();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      prgbMain.PerformStep();
      if (!mWorker.IsBusy)
        this.Close();
    }

    private void frmProgress_FormClosing(object sender, FormClosingEventArgs e)
    {
      timer1.Stop();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}