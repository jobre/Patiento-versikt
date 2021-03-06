using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Ortoped.GarpFunctions;
using Ortoped.HelpClasses;

namespace Ortoped
{
    /// <summary>
    /// Summary description for frmOwnFee.
    /// </summary>
    public class frmOwnFee : System.Windows.Forms.Form
    {
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPnr;
        private System.Windows.Forms.TextBox txtNamn;
        private System.Windows.Forms.Button btnKontant;
        private System.Windows.Forms.Button btnFaktura;
        private System.Windows.Forms.Button btnClose;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtAvgift;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAddress1;
        private System.Windows.Forms.TextBox txtOrt;

        private orderFunc oOH = new orderFunc();
        private string mKnr = "", mOnr = "", mAidId = "", mDokGen = "", mReport = "", mBolag = "";
        private Label labAdress2;
        private TextBox txtAddress2;
        private Label label6;
        private ComboBox cboProdGroup;
        private OrderRowFunc oOR = new OrderRowFunc();
        private OwnFee of = new OwnFee();


        public frmOwnFee()
        {
            InitializeComponent();
        }

        public frmOwnFee(string pnr, string knr, string fullname, string onr, string aidid, string bolag)
        {
            InitializeComponent();
            txtPnr.Text = pnr;
            txtNamn.Text = fullname;
            mKnr = knr;
            mOnr = onr;
            mAidId = aidid;
            mBolag = bolag;
            if (Config.EA_UseProductGroups)
            {
                cboProdGroup.Enabled = true;
                cboProdGroup.Items.Clear();
                cboProdGroup.Items.Add("   - V�lj produktgrupp");
                cboProdGroup.Items.AddRange(of.getAllProductGroups());
                cboProdGroup.SelectedIndex = 0;
            }
            else
            {
                cboProdGroup.Enabled = false;
                cboProdGroup.Items.Clear();
            }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOwnFee));
            this.btnKontant = new System.Windows.Forms.Button();
            this.btnFaktura = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labAdress2 = new System.Windows.Forms.Label();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOrt = new System.Windows.Forms.TextBox();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.txtNamn = new System.Windows.Forms.TextBox();
            this.txtPnr = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboProdGroup = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAvgift = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnKontant
            // 
            this.btnKontant.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnKontant.Location = new System.Drawing.Point(10, 282);
            this.btnKontant.Name = "btnKontant";
            this.btnKontant.Size = new System.Drawing.Size(75, 23);
            this.btnKontant.TabIndex = 1;
            this.btnKontant.Text = "Kontant";
            this.btnKontant.Click += new System.EventHandler(this.btnKontant_Click);
            // 
            // btnFaktura
            // 
            this.btnFaktura.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnFaktura.Location = new System.Drawing.Point(90, 282);
            this.btnFaktura.Name = "btnFaktura";
            this.btnFaktura.Size = new System.Drawing.Size(75, 23);
            this.btnFaktura.TabIndex = 2;
            this.btnFaktura.Text = "Faktura";
            this.btnFaktura.Click += new System.EventHandler(this.btnFaktura_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClose.Location = new System.Drawing.Point(170, 282);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "St�ng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labAdress2);
            this.groupBox1.Controls.Add(this.txtAddress2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtOrt);
            this.groupBox1.Controls.Add(this.txtAddress1);
            this.groupBox1.Controls.Add(this.txtNamn);
            this.groupBox1.Controls.Add(this.txtPnr);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 173);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Uppgifter";
            // 
            // labAdress2
            // 
            this.labAdress2.Location = new System.Drawing.Point(16, 110);
            this.labAdress2.Name = "labAdress2";
            this.labAdress2.Size = new System.Drawing.Size(60, 16);
            this.labAdress2.TabIndex = 11;
            this.labAdress2.Text = "Adress 2:";
            this.labAdress2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAddress2
            // 
            this.txtAddress2.Location = new System.Drawing.Point(82, 110);
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new System.Drawing.Size(169, 20);
            this.txtAddress2.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(20, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Postnr/ort:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(18, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Adress 1:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOrt
            // 
            this.txtOrt.Location = new System.Drawing.Point(82, 136);
            this.txtOrt.Name = "txtOrt";
            this.txtOrt.Size = new System.Drawing.Size(169, 20);
            this.txtOrt.TabIndex = 8;
            // 
            // txtAddress1
            // 
            this.txtAddress1.Location = new System.Drawing.Point(82, 84);
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.Size = new System.Drawing.Size(169, 20);
            this.txtAddress1.TabIndex = 6;
            // 
            // txtNamn
            // 
            this.txtNamn.Location = new System.Drawing.Point(82, 58);
            this.txtNamn.Name = "txtNamn";
            this.txtNamn.Size = new System.Drawing.Size(169, 20);
            this.txtNamn.TabIndex = 5;
            this.txtNamn.TabStop = false;
            // 
            // txtPnr
            // 
            this.txtPnr.Location = new System.Drawing.Point(82, 32);
            this.txtPnr.Name = "txtPnr";
            this.txtPnr.ReadOnly = true;
            this.txtPnr.Size = new System.Drawing.Size(169, 20);
            this.txtPnr.TabIndex = 4;
            this.txtPnr.TabStop = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(20, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Namn:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(20, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Personnr:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cboProdGroup);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtAvgift);
            this.groupBox2.Location = new System.Drawing.Point(8, 187);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(267, 89);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Avgift";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Produktgrupp:";
            // 
            // cboProdGroup
            // 
            this.cboProdGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProdGroup.Enabled = false;
            this.cboProdGroup.FormattingEnabled = true;
            this.cboProdGroup.Location = new System.Drawing.Point(82, 19);
            this.cboProdGroup.Name = "cboProdGroup";
            this.cboProdGroup.Size = new System.Drawing.Size(169, 21);
            this.cboProdGroup.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(28, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Avgift:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAvgift
            // 
            this.txtAvgift.Location = new System.Drawing.Point(82, 52);
            this.txtAvgift.Name = "txtAvgift";
            this.txtAvgift.Size = new System.Drawing.Size(96, 20);
            this.txtAvgift.TabIndex = 1;
            this.txtAvgift.Text = "0";
            // 
            // frmOwnFee
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(283, 313);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnFaktura);
            this.Controls.Add(this.btnKontant);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmOwnFee";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Egenavgift";
            this.Load += new System.EventHandler(this.frmOwnFee_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmOwnFee_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private void frmOwnFee_Load(object sender, System.EventArgs e)
        {
            if (Config.AlwaysUpperCase)
            {
                txtNamn.CharacterCasing = CharacterCasing.Upper;
                txtAddress1.CharacterCasing = CharacterCasing.Upper;
                txtAddress2.CharacterCasing = CharacterCasing.Upper;
                txtOrt.CharacterCasing = CharacterCasing.Upper;
            }
        }

        private void btnKontant_Click(object sender, System.EventArgs e)
        {
            // If statistics on productgroup is selected, but not choosen, stop
            if (Config.EA_UseProductGroups && (cboProdGroup.SelectedIndex == 0))
            {
                MessageBox.Show("Du m�ste v�lja en produktgrupp", "Produktgrupp");
                return;
            }

            try
            {
                string[] s = Config.getReceipt().Split('-');
                mDokGen = s[0];
                mReport = s[1];
            }
            catch { }

            createOwnFee(Config.BetVillkEaKont, txtNamn.Text, txtAddress1.Text, txtAddress2.Text, txtOrt.Text);
            this.Close();
        }

        private void btnFaktura_Click(object sender, System.EventArgs e)
        {
            // If statistics on productgroup is selected, but not choosen, stop
            if (Config.EA_UseProductGroups && (cboProdGroup.SelectedIndex == 0))
            {
                MessageBox.Show("Du m�ste v�lja en produktgrupp", "Produktgrupp");
                return;
            }

            try
            {
                string[] s = Config.getInvoice().Split('-');
                mDokGen = s[0];
                mReport = s[1];
            }
            catch { }

            createOwnFee(Config.BetVillkEaFakt, txtNamn.Text, txtAddress1.Text, txtAddress2.Text, txtOrt.Text);
            this.Close();
        }

        private void createOwnFee(string bvk, string namn, string adress1, string adress2, string ort)
        {
            OrderRowFunc oOR = new OrderRowFunc();
            string sPG = "";

            try
            {
                sPG = cboProdGroup.Text.Substring(0, 2).Trim();
            }
            catch
            {
                sPG = "";
            }

            string sFsNr = of.createOwnFee(mKnr, mOnr, mAidId, bvk, txtAvgift.Text, txtNamn.Text, txtAddress1.Text, txtAddress2.Text, txtOrt.Text, sPG);
            if (!GCS.GCF.noNULL(sFsNr).Equals(""))
                oOR.printInvoice(mDokGen, mReport, sFsNr);
        }

        private void frmOwnFee_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                System.Windows.Forms.SendKeys.Send("{TAB}");
                e.Handled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
