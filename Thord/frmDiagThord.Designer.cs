namespace Ortoped.Thord
{
    partial class frmDiagThord
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.components = new System.ComponentModel.Container();
            this.tmHello = new System.Windows.Forms.Timer(this.components);
            this.tmReferral = new System.Windows.Forms.Timer(this.components);
            this.tmISO = new System.Windows.Forms.Timer(this.components);
            this.tmHelloSecret = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.cboGarpOrderNr = new System.Windows.Forms.ComboBox();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnUppdat = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGetReferrals = new System.Windows.Forms.Button();
            this.btnTestCom = new System.Windows.Forms.Button();
            this.lwReferral = new System.Windows.Forms.ListView();
            this.colDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRefferalId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPnr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colKK = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPriority = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpTest = new System.Windows.Forms.GroupBox();
            this.labPassword = new System.Windows.Forms.Label();
            this.labUser = new System.Windows.Forms.Label();
            this.btnISO = new System.Windows.Forms.Button();
            this.btnHlpType = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnDeletePart = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.grpTest.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpTo);
            this.groupBox1.Controls.Add(this.dtpFrom);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.cboGarpOrderNr);
            this.groupBox1.Controls.Add(this.cboStatus);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnUppdat);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnGetReferrals);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 298);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(568, 101);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selektering";
            // 
            // dtpTo
            // 
            this.dtpTo.Location = new System.Drawing.Point(158, 32);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(145, 20);
            this.dtpTo.TabIndex = 4;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Location = new System.Drawing.Point(7, 32);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(145, 20);
            this.dtpFrom.TabIndex = 3;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "OT-Center",
            "OT-Center Syd"});
            this.comboBox1.Location = new System.Drawing.Point(158, 72);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(145, 21);
            this.comboBox1.TabIndex = 10;
            // 
            // cboGarpOrderNr
            // 
            this.cboGarpOrderNr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGarpOrderNr.FormattingEnabled = true;
            this.cboGarpOrderNr.Location = new System.Drawing.Point(323, 72);
            this.cboGarpOrderNr.Name = "cboGarpOrderNr";
            this.cboGarpOrderNr.Size = new System.Drawing.Size(121, 21);
            this.cboGarpOrderNr.TabIndex = 12;
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Items.AddRange(new object[] {
            "Avslutad",
            "Giltighetstidutgatt",
            "Inkommen",
            "Mottagen",
            "Overford",
            "Paborjad"});
            this.cboStatus.Location = new System.Drawing.Point(323, 32);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(121, 21);
            this.cboStatus.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(320, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Skapade order i Garp:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(155, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Till:";
            // 
            // btnUppdat
            // 
            this.btnUppdat.Location = new System.Drawing.Point(7, 72);
            this.btnUppdat.Name = "btnUppdat";
            this.btnUppdat.Size = new System.Drawing.Size(145, 23);
            this.btnUppdat.TabIndex = 9;
            this.btnUppdat.Text = "Läs in";
            this.btnUppdat.UseVisualStyleBackColor = true;
            this.btnUppdat.Click += new System.EventHandler(this.btnUppdat_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(320, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Status";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(158, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Datum t o m";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Datum fr o m";
            // 
            // btnGetReferrals
            // 
            this.btnGetReferrals.Location = new System.Drawing.Point(450, 32);
            this.btnGetReferrals.Name = "btnGetReferrals";
            this.btnGetReferrals.Size = new System.Drawing.Size(98, 23);
            this.btnGetReferrals.TabIndex = 2;
            this.btnGetReferrals.Text = "Hämta remisser";
            this.btnGetReferrals.Click += new System.EventHandler(this.btnGetReferral_Click);
            // 
            // btnTestCom
            // 
            this.btnTestCom.Location = new System.Drawing.Point(19, 173);
            this.btnTestCom.Name = "btnTestCom";
            this.btnTestCom.Size = new System.Drawing.Size(98, 20);
            this.btnTestCom.TabIndex = 14;
            this.btnTestCom.Text = "Förbindelseprov";
            this.btnTestCom.UseVisualStyleBackColor = true;
            this.btnTestCom.Click += new System.EventHandler(this.button1_Click);
            // 
            // lwReferral
            // 
            this.lwReferral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lwReferral.CheckBoxes = true;
            this.lwReferral.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDate,
            this.colRefferalId,
            this.colPnr,
            this.colKK,
            this.colPriority});
            this.lwReferral.FullRowSelect = true;
            this.lwReferral.Location = new System.Drawing.Point(0, 0);
            this.lwReferral.MultiSelect = false;
            this.lwReferral.Name = "lwReferral";
            this.lwReferral.Size = new System.Drawing.Size(568, 292);
            this.lwReferral.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.lwReferral.TabIndex = 11;
            this.lwReferral.UseCompatibleStateImageBehavior = false;
            this.lwReferral.View = System.Windows.Forms.View.Details;
            this.lwReferral.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lwReferral_ColumnClick);
            // 
            // colDate
            // 
            this.colDate.Text = "Datum";
            this.colDate.Width = 79;
            // 
            // colRefferalId
            // 
            this.colRefferalId.Text = "RemissId";
            this.colRefferalId.Width = 106;
            // 
            // colPnr
            // 
            this.colPnr.Text = "PersonNr";
            this.colPnr.Width = 91;
            // 
            // colKK
            // 
            this.colKK.Text = "Kombikakod";
            this.colKK.Width = 109;
            // 
            // colPriority
            // 
            this.colPriority.Text = "Prioritet";
            this.colPriority.Width = 69;
            // 
            // grpTest
            // 
            this.grpTest.Controls.Add(this.labPassword);
            this.grpTest.Controls.Add(this.labUser);
            this.grpTest.Controls.Add(this.btnISO);
            this.grpTest.Controls.Add(this.btnHlpType);
            this.grpTest.Controls.Add(this.listBox1);
            this.grpTest.Controls.Add(this.btnDeletePart);
            this.grpTest.Controls.Add(this.label4);
            this.grpTest.Controls.Add(this.textBox1);
            this.grpTest.Controls.Add(this.btnUpdate);
            this.grpTest.Controls.Add(this.btnNew);
            this.grpTest.Controls.Add(this.btnTestCom);
            this.grpTest.Location = new System.Drawing.Point(33, 38);
            this.grpTest.Name = "grpTest";
            this.grpTest.Size = new System.Drawing.Size(504, 236);
            this.grpTest.TabIndex = 13;
            this.grpTest.TabStop = false;
            this.grpTest.Text = "Testpanel";
            this.grpTest.Visible = false;
            // 
            // labPassword
            // 
            this.labPassword.AutoSize = true;
            this.labPassword.Location = new System.Drawing.Point(22, 214);
            this.labPassword.Name = "labPassword";
            this.labPassword.Size = new System.Drawing.Size(0, 13);
            this.labPassword.TabIndex = 24;
            // 
            // labUser
            // 
            this.labUser.AutoSize = true;
            this.labUser.Location = new System.Drawing.Point(22, 201);
            this.labUser.Name = "labUser";
            this.labUser.Size = new System.Drawing.Size(0, 13);
            this.labUser.TabIndex = 23;
            // 
            // btnISO
            // 
            this.btnISO.Location = new System.Drawing.Point(336, 191);
            this.btnISO.Name = "btnISO";
            this.btnISO.Size = new System.Drawing.Size(75, 23);
            this.btnISO.TabIndex = 22;
            this.btnISO.Text = "ISO";
            this.btnISO.UseVisualStyleBackColor = true;
            this.btnISO.Click += new System.EventHandler(this.btnISO_Click);
            // 
            // btnHlpType
            // 
            this.btnHlpType.Location = new System.Drawing.Point(253, 191);
            this.btnHlpType.Name = "btnHlpType";
            this.btnHlpType.Size = new System.Drawing.Size(75, 23);
            this.btnHlpType.TabIndex = 21;
            this.btnHlpType.Text = "Hjälpmedelstyp";
            this.btnHlpType.UseVisualStyleBackColor = true;
            this.btnHlpType.Click += new System.EventHandler(this.btnHlpType_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(256, 24);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(230, 147);
            this.listBox1.TabIndex = 20;
            // 
            // btnDeletePart
            // 
            this.btnDeletePart.Location = new System.Drawing.Point(153, 103);
            this.btnDeletePart.Name = "btnDeletePart";
            this.btnDeletePart.Size = new System.Drawing.Size(75, 23);
            this.btnDeletePart.TabIndex = 19;
            this.btnDeletePart.Text = "Radera part";
            this.btnDeletePart.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Remiss";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(17, 47);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 17;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(17, 103);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 16;
            this.btnUpdate.Text = "Uppdatera";
            this.btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(98, 103);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(49, 24);
            this.btnNew.TabIndex = 15;
            this.btnNew.Text = "Ny";
            this.btnNew.UseVisualStyleBackColor = true;
            // 
            // frmDiagThord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 399);
            this.Controls.Add(this.grpTest);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lwReferral);
            this.KeyPreview = true;
            this.Name = "frmDiagThord";
            this.Text = "Garp Remiss";
            this.Load += new System.EventHandler(this.ThordMainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDiagThord_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpTest.ResumeLayout(false);
            this.grpTest.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmHello;
        private System.Windows.Forms.Timer tmReferral;
        private System.Windows.Forms.Timer tmISO;
      private System.Windows.Forms.Timer tmHelloSecret;
      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.ComboBox cboGarpOrderNr;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.ComboBox comboBox1;
      private System.Windows.Forms.Button btnUppdat;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.ComboBox cboStatus;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.DateTimePicker dtpTo;
      private System.Windows.Forms.DateTimePicker dtpFrom;
      private System.Windows.Forms.Button btnGetReferrals;
      private System.Windows.Forms.ListView lwReferral;
      private System.Windows.Forms.ColumnHeader colDate;
      private System.Windows.Forms.ColumnHeader colRefferalId;
      private System.Windows.Forms.ColumnHeader colPnr;
      private System.Windows.Forms.ColumnHeader colKK;
      private System.Windows.Forms.ColumnHeader colPriority;
      private System.Windows.Forms.Button btnTestCom;
      private System.Windows.Forms.GroupBox grpTest;
      private System.Windows.Forms.Button btnDeletePart;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.TextBox textBox1;
      private System.Windows.Forms.Button btnUpdate;
      private System.Windows.Forms.Button btnNew;
      private System.Windows.Forms.Button btnHlpType;
      private System.Windows.Forms.ListBox listBox1;
      private System.Windows.Forms.Button btnISO;
      private System.Windows.Forms.Label labPassword;
      private System.Windows.Forms.Label labUser;
    }
}