using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
//#if DEBUG
//using Ortoped.se.sll.bkv.externtest;
//  using Ortoped.se.sll.bkv_sabb.bkv_tstweb1;
//#else
using Ortoped.se.sll.thord.www;
//#endif
using System.Reflection;
using System.IO;
using Ortoped.HelpClasses;
using Thord.HelpClasses;
using Ortoped.Definitions;
using Excido;
using Ortoped.Thord;
using GCS;

namespace Ortoped.Thord
{
    /// <summary>
    /// Thord Mainform
    /// </summary>
    public partial class frmDiagThord : Form
    {
        private ThordFunctions tf;
        private Referral[] rfList;
        //    private Referral[] gpList;
        private GarpReferrals gf;
        private Ortoped.GarpFunctions.CustomerFunc oCust;

        /// <summary>
        /// The constructor initialises components and creates ThordFunctions instance
        /// </summary>
        public frmDiagThord()
        {
            InitializeComponent();
            this.lwReferral.ListViewItemSorter = new ListViewItemComparer(0);
            cboStatus.SelectedIndex = 2;
        }

        private void ThordMainForm_Load(object sender, EventArgs e)
        {
            //string sVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion;
            //this.Text = "Garp Remiss  " +sVersion;

            try
            {
                if (Application.ProductVersion.Substring(4, 1).Equals("0"))
                    this.Text = Application.ProductName + " - " + Application.ProductVersion.Substring(0, 3);
                else
                    this.Text = Application.ProductName + " - " + Application.ProductVersion.Substring(0, 5);
                
                labUser.Text = Config.ThordUserId;
                labPassword.Text = Config.ThordPassword;
            }
            catch
            {
                this.Text = Application.ProductName;
            }
            try
            {
                oCust = new Ortoped.GarpFunctions.CustomerFunc();
                tf = new ThordFunctions();
                gf = new GarpReferrals();
            }
            catch (Exception ex)
            {
                Log4Net.Logger.loggError(ex, "Error when collection refferals from Thord", Config.User, "frmDiagThord.ThordMainForm_Load");
            }

        }

        private void btnGetReferral_Click(object sender, System.EventArgs e)
        {
            if (cboStatus.Text.Length == 0)
            {
                MessageBox.Show(this, "Välj en status!", "Status saknas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    lwReferral.Items.Clear();
                    rfList = tf.getRefs(dtpFrom.Value, dtpTo.Value, (ThordService.ReferralStatusValues1)Enum.Parse(typeof(ThordService.ReferralStatusValues1), cboStatus.Text, true));
                    if (rfList != null)
                    {
                        if (rfList.Length > 0)
                        {
                            lwReferral.Items.AddRange(ConvertFunctions.convertReferralToListViewItems(rfList));
                            lwReferral.ListViewItemSorter = new ListViewItemComparer(4);
                        }
                        else
                            MessageBox.Show(this, "Inga remisser hittades på valt urval", "Remisser saknas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    Log4Net.Logger.loggError(ex, "Error when reading referrals from Thord", Config.User, "frmDiagThord.btnGetReferral_Click");
                    MessageBox.Show(this, "Ett fel i uppstod vid hämtning av remisser\n\n" + ex.Message + "\n\n" + ex.StackTrace, "Remisser", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        static object StringToEnum(Type t, string Value)
        {
            foreach (FieldInfo fi in t.GetFields())
                if (fi.Name == Value.ToUpper())
                    return fi.GetValue(null);    // We use null because
            // enumeration values
            // are static
            throw new Exception(string.Format("Can't convert {0} to {1}", Value, t.ToString()));
        }

        private void btnUppdat_Click(object sender, EventArgs e)
        {
            ArrayList al = new ArrayList();
            if (comboBox1.SelectedIndex == -1)
                return;

            cboGarpOrderNr.Items.Clear();

            int count = 0;
            if (rfList != null)
            {
                if (rfList.Length == 0)
                    rfList = tf.getRefs(dtpFrom.Value, dtpTo.Value, (ThordService.ReferralStatusValues1)Enum.Parse(typeof(ThordService.ReferralStatusValues1), cboStatus.Text, true));

                foreach (ListViewItem lw in lwReferral.Items)
                {
                    if (lw.Checked)
                        try
                        {
                            foreach (Referral rf in rfList)
                            {
                                if (ECS.noNULL(rf.remissid).Equals(lw.SubItems[1].Text))
                                {
                                    al.Add(rf);
                                    count = count + 1;
                                }
                            }
                        }
                        catch { }
                }

                rfList = (Referral[])al.ToArray(typeof(Referral));

                if (count > 0)
                {
                    if (comboBox1.SelectedIndex == 0)
                        rfList = gf.updateTables(rfList, "B");

                    if (comboBox1.SelectedIndex == 1)
                        rfList = gf.updateTables(rfList, "C");

                    foreach (Referral r in rfList)
                        cboGarpOrderNr.Items.Add(r.orderno);

                    // Chage status of referrals to "MOTTAGEN" (only in Release mode)
#if !DEBUG
		  tf.sendTransferedReferrals(rfList);
#else
                    MessageBox.Show("Ingen uppdatering av remissers status sker eftersom applicationen körs i debugmode");
#endif

                    foreach (ListViewItem lw in lwReferral.Items)
                        lwReferral.Items.Remove(lw);

                    MessageBox.Show(this, "Remisserna har lästs in.", "Inläsning klar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, "Du har inte valt ngn remiss.", "Data saknas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(this, "Du har inte hämtat några remisser.", "Data saknas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lwReferral_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            this.lwReferral.ListViewItemSorter = new ListViewItemComparer(e.Column);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(tf.helloThord());
                MessageBox.Show(tf.helloSecretThord());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Felmeddelande från Thord: " + ex.Message, "Thord", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmDiagThord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.Control && e.Alt)
            {
                if (grpTest.Visible)
                    grpTest.Visible = false;
                else
                    grpTest.Visible = true;
            }
        }

        private void btnHlpType_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.AddRange(tf.getAllAidTypes());
        }

        private void btnISO_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.AddRange(tf.getAllISOCodeAndNames());

        }

        private void grpTest_Enter(object sender, EventArgs e)
        {

        }

    }
}