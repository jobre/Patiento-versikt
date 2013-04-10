using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Ortoped.GarpFunctions;
using Ortoped.HelpClasses;
using Excido;
using Ortoped.Definitions;
using GCS;

namespace Ortoped
{
    /// <summary>
    /// Summary description for frmAddCust.
    /// </summary>
    public class frmAddCust : System.Windows.Forms.Form
    {
        #region

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.TextBox txtPnr;
        private System.Windows.Forms.TextBox txtSN;
        private System.Windows.Forms.TextBox txtLN;
        private System.Windows.Forms.TextBox txtADD;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTelBostad;
        private System.Windows.Forms.TextBox txtTelArbete;
        private System.Windows.Forms.TextBox txtTelMobil;
        #endregion

        public PatientDefenition newPatient = new PatientDefenition();
        private System.Windows.Forms.TextBox txtPostNr;
        private System.Windows.Forms.TextBox txtOrt;
        private HelpClasses.Table oTa = new Ortoped.HelpClasses.Table("16");
        private TextBox txtTelSMS;
        private Label label9;
        private CustomerFunc myCust = new CustomerFunc();

        public frmAddCust()
        {
            InitializeComponent();
        }

        public frmAddCust(string pnr)
        {
            InitializeComponent();
            if ((pnr.Length > 8) && (pnr.IndexOf('-') == -1))
                txtPnr.Text = pnr.Insert(8, "-");
            else
                txtPnr.Text = pnr;

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddCust));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTelSMS = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtOrt = new System.Windows.Forms.TextBox();
            this.txtTelMobil = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTelArbete = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTelBostad = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPostNr = new System.Windows.Forms.TextBox();
            this.txtADD = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLN = new System.Windows.Forms.TextBox();
            this.txtSN = new System.Windows.Forms.TextBox();
            this.txtPnr = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTelSMS);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtOrt);
            this.groupBox1.Controls.Add(this.txtTelMobil);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtTelArbete);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtTelBostad);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtPostNr);
            this.groupBox1.Controls.Add(this.txtADD);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtLN);
            this.groupBox1.Controls.Add(this.txtSN);
            this.groupBox1.Controls.Add(this.txtPnr);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 244);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Personuppgifter";
            // 
            // txtTelSMS
            // 
            this.txtTelSMS.Location = new System.Drawing.Point(96, 216);
            this.txtTelSMS.Name = "txtTelSMS";
            this.txtTelSMS.Size = new System.Drawing.Size(168, 20);
            this.txtTelSMS.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(32, 216);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 16);
            this.label9.TabIndex = 18;
            this.label9.Text = "Tel SMS:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOrt
            // 
            this.txtOrt.Location = new System.Drawing.Point(160, 120);
            this.txtOrt.Name = "txtOrt";
            this.txtOrt.Size = new System.Drawing.Size(104, 20);
            this.txtOrt.TabIndex = 5;
            // 
            // txtTelMobil
            // 
            this.txtTelMobil.Location = new System.Drawing.Point(96, 192);
            this.txtTelMobil.Name = "txtTelMobil";
            this.txtTelMobil.Size = new System.Drawing.Size(168, 20);
            this.txtTelMobil.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(32, 192);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 16);
            this.label8.TabIndex = 16;
            this.label8.Text = "Tel Mobil:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTelArbete
            // 
            this.txtTelArbete.Location = new System.Drawing.Point(96, 168);
            this.txtTelArbete.Name = "txtTelArbete";
            this.txtTelArbete.Size = new System.Drawing.Size(168, 20);
            this.txtTelArbete.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(32, 168);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "Tel Arb:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTelBostad
            // 
            this.txtTelBostad.Location = new System.Drawing.Point(96, 144);
            this.txtTelBostad.Name = "txtTelBostad";
            this.txtTelBostad.Size = new System.Drawing.Size(168, 20);
            this.txtTelBostad.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(24, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "Tel Bostad:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(32, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "Pnr/Ort:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(24, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 20);
            this.label7.TabIndex = 10;
            this.label7.Text = "Adress:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPostNr
            // 
            this.txtPostNr.Location = new System.Drawing.Point(96, 120);
            this.txtPostNr.Name = "txtPostNr";
            this.txtPostNr.Size = new System.Drawing.Size(56, 20);
            this.txtPostNr.TabIndex = 4;
            this.txtPostNr.Leave += new System.EventHandler(this.txtPostNr_Leave);
            // 
            // txtADD
            // 
            this.txtADD.Location = new System.Drawing.Point(96, 96);
            this.txtADD.Name = "txtADD";
            this.txtADD.Size = new System.Drawing.Size(168, 20);
            this.txtADD.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(24, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Efternamn:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(32, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Förnamn:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(2, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Personnummer:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLN
            // 
            this.txtLN.Location = new System.Drawing.Point(96, 48);
            this.txtLN.Name = "txtLN";
            this.txtLN.Size = new System.Drawing.Size(168, 20);
            this.txtLN.TabIndex = 1;
            // 
            // txtSN
            // 
            this.txtSN.Location = new System.Drawing.Point(96, 72);
            this.txtSN.Name = "txtSN";
            this.txtSN.Size = new System.Drawing.Size(168, 20);
            this.txtSN.TabIndex = 2;
            // 
            // txtPnr
            // 
            this.txtPnr.Location = new System.Drawing.Point(96, 24);
            this.txtPnr.Name = "txtPnr";
            this.txtPnr.Size = new System.Drawing.Size(168, 20);
            this.txtPnr.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(155, 258);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(64, 24);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(225, 258);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(56, 24);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Avbryt";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmAddCust
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(298, 289);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddCust";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lägg upp ny patient";
            this.Activated += new System.EventHandler(this.frmAddCust_Activated);
            this.Load += new System.EventHandler(this.frmAddCust_Load);
            this.Shown += new System.EventHandler(this.frmAddCust_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmAddCust_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private void frmAddCust_Activated(object sender, System.EventArgs e)
        {
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            fillPatient();
        }

        private void fillPatient()
        {
            PersonNummer pnr = new PersonNummer();
            string s = myCust.checkPnr(txtPnr.Text);

            if (s.Trim().Equals(""))
            {
                // Kontrollera checksiffra
                if (!pnr.calculate(txtPnr.Text))
                {
                    if (!(MessageBox.Show(this, "Checksiffran stämmer inte på detta personnummer, vill du gå vidare ändå?", "Fel checksiffra", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes))
                        return;
                }

                newPatient.SSN = txtPnr.Text;
                newPatient.LastName = txtLN.Text;
                newPatient.SureName = txtSN.Text;
                newPatient.Address = txtADD.Text;
                newPatient.PoCity = txtPostNr.Text + " " + txtOrt.Text;
                newPatient.TelHome = txtTelBostad.Text;
                newPatient.TelWork = txtTelArbete.Text;
                newPatient.TelMobile = txtTelMobil.Text;
                newPatient.TelSMS = txtTelSMS.Text;
                newPatient.Category = Config.getCustTemplate.Category;
                newPatient.DeliverTerms = Config.getCustTemplate.DeliverTerms;
                newPatient.PaymentTerms = Config.getCustTemplate.PaymentTerms;
                newPatient.PriceList = Config.getCustTemplate.PriceList;
                newPatient.VATCode = Config.getCustTemplate.VATCode;
                newPatient.WayOfDeliver = Config.getCustTemplate.WayOfDeliver;
                newPatient.ViewCode = Config.getCustTemplate.ViewCode;
                newPatient.InterestInvoice = Config.getCustTemplate.InterestInvoice;
                newPatient.PaymentReminder = Config.getCustTemplate.PaymentReminder;
                newPatient.DoesExist = true;

                if (!ECS.noNULL(newPatient.PatientNo).Equals(""))
                {// Make global customer
                    // Delete old name, otherwise the name gets corrupted when
                    // the two patients are united
                    newPatient.SureName = "";
                    newPatient.LastName = "";
                    myCust.updateCustomer(newPatient);

                    newPatient.SureName = txtSN.Text;
                    newPatient.LastName = txtLN.Text;
                    newPatient.ViewCode = "";
                    myCust.updateCustomer(newPatient);
                }
                else
                {// Create new customer
                    try
                    {
                        if (myCust.checkNextCustomerNo())
                        {
                            PatientDefenition[] pd = myCust.getPatientByPnr(txtPnr.Text.Trim(), true, false);

                            if (pd.Length == 0)
                            {
                                newPatient = myCust.addCustomer(newPatient);
                                myCust.updateCustomer(newPatient);
                            }
                            else
                            {
                                MessageBox.Show(this, "Personnummret " + txtPnr.Text + " fanns redan, inget upplägg gjordes", "Personnummret finns redan", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                                newPatient.SSN = pd[0].SSN;
                                newPatient.LastName = pd[0].LastName;
                                newPatient.SureName = pd[0].SureName;
                                newPatient.Address = pd[0].Address;
                                newPatient.PoCity = pd[0].PoCity;
                                newPatient.TelHome = pd[0].TelHome;
                                newPatient.TelWork = pd[0].TelWork;
                                newPatient.TelMobile = pd[0].TelMobile;
                                newPatient.TelSMS = pd[0].TelSMS;
                                newPatient.Category = pd[0].Category;
                                newPatient.DeliverTerms = pd[0].DeliverTerms;
                                newPatient.PaymentTerms = pd[0].PaymentTerms;
                                newPatient.PriceList = pd[0].PriceList;
                                newPatient.VATCode = pd[0].VATCode;
                                newPatient.WayOfDeliver = pd[0].WayOfDeliver;
                                newPatient.ViewCode = pd[0].ViewCode;
                                newPatient.InterestInvoice = pd[0].InterestInvoice;
                                newPatient.PaymentReminder = pd[0].PaymentReminder;
                                newPatient.DoesExist = pd[0].DoesExist;
                                newPatient.PatientNo = pd[0].PatientNo;
                            }

                        }
                        else
                        {
                            newPatient.SSN = "";
                            MessageBox.Show(this, "Kundnumret är upptaget. Detta beror på problem med nummerräknaren i Garp, kontrollera nummerräknaren och försök igen.", "Kundnumret finns redan", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.Message, "Ett fel inträffade vid upplägg av patient", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        Log4Net.Logger.loggError(ex, "Error while adding patient", Config.User, "");
                    }
                }

                this.Close();
            }
            else
            {
                MessageBox.Show(this, s, "Upplägg av patient misslyckades", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void txtPostNr_Leave(object sender, System.EventArgs e)
        {
            // Fyll ut om det saknas
            if (txtPostNr.Text.Length < 5)
                txtPostNr.Text = txtPostNr.Text.PadRight(5, '0');

            // Lägg till blanksteg om det saknas
            if (!txtPostNr.Text.Substring(3, 1).Equals(" "))
                txtPostNr.Text = txtPostNr.Text.Insert(3, " ");

            // Hämta pnr översättning om det finns
            txtOrt.Text = oTa.getTx1ByKey(txtPostNr.Text.Trim());
        }

        private void frmAddCust_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                System.Windows.Forms.SendKeys.Send("{TAB}");
                e.Handled = true;
            }
        }

        private void frmAddCust_Load(object sender, EventArgs e)
        {
            if (Config.AlwaysUpperCase)
            {
                txtSN.CharacterCasing = CharacterCasing.Upper;
                txtLN.CharacterCasing = CharacterCasing.Upper;
                txtADD.CharacterCasing = CharacterCasing.Upper;
                txtOrt.CharacterCasing = CharacterCasing.Upper;

            }
        }

        private void frmAddCust_Shown(object sender, EventArgs e)
        {
            newPatient.SSN = txtPnr.Text;
            PatientDefenition[] sPat = myCust.getPatientByPnr(newPatient.SSN, true, false);

            if (sPat.Length > 0)
            {
                if (MessageBox.Show(this, "Patienten finns redan fast med tillhörighet " + sPat[0].ViewCode + " ,vill du skapa en gemensam patient?", "Patient redan upplagd", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
                {
                    newPatient.PatientNo = sPat[0].PatientNo;
                    txtLN.Text = sPat[0].LastName;
                    txtSN.Text = sPat[0].SureName;
                    txtADD.Text = sPat[0].Address;
                    try
                    {
                        txtPostNr.Text = ECS.noNULL(sPat[0].PoCity).Substring(0, 6);
                        txtOrt.Text = ECS.noNULL(sPat[0].PoCity).Substring(6);
                    }
                    catch (Exception ex)
                    {
                        Log4Net.Logger.loggError(ex, "Error when parsing PoCity in frmAddCust", Config.User, "");
                    }
                    txtTelArbete.Text = sPat[0].TelWork;
                    txtTelBostad.Text = sPat[0].TelHome;
                    txtTelMobil.Text = sPat[0].TelMobile;
                }
            }
            txtLN.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            newPatient.SSN = "";
        }

    }
}
