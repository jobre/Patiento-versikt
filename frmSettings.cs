using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ortoped.HelpClasses;

namespace Ortoped
{
  public partial class frmSettings : Form
  {
    HandleConfig hc = new HandleConfig();
    HandleConfig.Company company;

    public frmSettings()
    {
      InitializeComponent();
    }

    private void frmSettings_Load(object sender, EventArgs e)
    {
      System.Configuration.UserScopedSettingAttribute userAttr = new System.Configuration.UserScopedSettingAttribute();
      System.ComponentModel.AttributeCollection attrs = new System.ComponentModel.AttributeCollection(userAttr);

      propertyGrid1.BrowsableAttributes = attrs;
      propertyGrid1.SelectedObject = Properties.Settings.Default;
      
      cboCompany.Items.AddRange(hc.getAllCompanies());
    }

    private void button1_Click(object sender, EventArgs e)
    {
      Properties.Settings.Default.Save();
    }

    private void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
      company = hc.getCompany(cboCompany.Text.Substring(0, 3));

      txtDiagFile.Text = company.DiagFile;
      chkAlwaysCapital.Checked = company.AlwaysCapitalLetters;
      chkShowDetails.Checked = company.ShowDetails;
      chkShowProductGroups.Checked = company.EA_UseProductGroups;
    }
  }
}