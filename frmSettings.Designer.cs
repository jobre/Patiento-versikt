namespace Ortoped
{
  partial class frmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabConfigure = new System.Windows.Forms.TabPage();
            this.tabctrlDetails = new System.Windows.Forms.TabControl();
            this.tabUser = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtThordPassword = new System.Windows.Forms.TextBox();
            this.txtThordUser = new System.Windows.Forms.TextBox();
            this.chkIsThordUser = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEA_BetVilkInvoice = new System.Windows.Forms.TextBox();
            this.txtEA_BetVilkCash = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDiagFile = new System.Windows.Forms.TextBox();
            this.chkShowProductGroups = new System.Windows.Forms.CheckBox();
            this.chkAlwaysCapital = new System.Windows.Forms.CheckBox();
            this.chkShowDetails = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboCompany = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabConfigure.SuspendLayout();
            this.tabctrlDetails.SuspendLayout();
            this.tabUser.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid1.Location = new System.Drawing.Point(44, 47);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(420, 121);
            this.propertyGrid1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(84, 323);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabConfigure);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(647, 501);
            this.tabControl1.TabIndex = 2;
            // 
            // tabConfigure
            // 
            this.tabConfigure.Controls.Add(this.tabctrlDetails);
            this.tabConfigure.Controls.Add(this.groupBox1);
            this.tabConfigure.Location = new System.Drawing.Point(4, 22);
            this.tabConfigure.Name = "tabConfigure";
            this.tabConfigure.Padding = new System.Windows.Forms.Padding(3);
            this.tabConfigure.Size = new System.Drawing.Size(639, 475);
            this.tabConfigure.TabIndex = 0;
            this.tabConfigure.Text = "Grundinställningar";
            this.tabConfigure.UseVisualStyleBackColor = true;
            // 
            // tabctrlDetails
            // 
            this.tabctrlDetails.Controls.Add(this.tabUser);
            this.tabctrlDetails.Controls.Add(this.tabPage4);
            this.tabctrlDetails.Location = new System.Drawing.Point(9, 100);
            this.tabctrlDetails.Name = "tabctrlDetails";
            this.tabctrlDetails.SelectedIndex = 0;
            this.tabctrlDetails.Size = new System.Drawing.Size(610, 360);
            this.tabctrlDetails.TabIndex = 3;
            // 
            // tabUser
            // 
            this.tabUser.Controls.Add(this.groupBox4);
            this.tabUser.Controls.Add(this.groupBox3);
            this.tabUser.Controls.Add(this.groupBox2);
            this.tabUser.Location = new System.Drawing.Point(4, 22);
            this.tabUser.Name = "tabUser";
            this.tabUser.Padding = new System.Windows.Forms.Padding(3);
            this.tabUser.Size = new System.Drawing.Size(602, 334);
            this.tabUser.TabIndex = 0;
            this.tabUser.Text = "Användare";
            this.tabUser.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.txtThordPassword);
            this.groupBox4.Controls.Add(this.txtThordUser);
            this.groupBox4.Controls.Add(this.chkIsThordUser);
            this.groupBox4.Location = new System.Drawing.Point(239, 95);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(167, 107);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Thord";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Lösenord:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Användare:";
            // 
            // txtThordPassword
            // 
            this.txtThordPassword.Location = new System.Drawing.Point(70, 71);
            this.txtThordPassword.Name = "txtThordPassword";
            this.txtThordPassword.Size = new System.Drawing.Size(80, 20);
            this.txtThordPassword.TabIndex = 2;
            // 
            // txtThordUser
            // 
            this.txtThordUser.Location = new System.Drawing.Point(70, 43);
            this.txtThordUser.Name = "txtThordUser";
            this.txtThordUser.Size = new System.Drawing.Size(80, 20);
            this.txtThordUser.TabIndex = 1;
            // 
            // chkIsThordUser
            // 
            this.chkIsThordUser.AutoSize = true;
            this.chkIsThordUser.Location = new System.Drawing.Point(13, 19);
            this.chkIsThordUser.Name = "chkIsThordUser";
            this.chkIsThordUser.Size = new System.Drawing.Size(105, 17);
            this.chkIsThordUser.TabIndex = 0;
            this.chkIsThordUser.Text = "Thordanvändare";
            this.chkIsThordUser.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtEA_BetVilkInvoice);
            this.groupBox3.Controls.Add(this.txtEA_BetVilkCash);
            this.groupBox3.Location = new System.Drawing.Point(10, 95);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(213, 87);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Egenavgift";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Betalningsvillkor faktura:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Betalningsvillkor kontant:";
            // 
            // txtEA_BetVilkInvoice
            // 
            this.txtEA_BetVilkInvoice.Location = new System.Drawing.Point(146, 49);
            this.txtEA_BetVilkInvoice.MaxLength = 2;
            this.txtEA_BetVilkInvoice.Name = "txtEA_BetVilkInvoice";
            this.txtEA_BetVilkInvoice.Size = new System.Drawing.Size(33, 20);
            this.txtEA_BetVilkInvoice.TabIndex = 1;
            // 
            // txtEA_BetVilkCash
            // 
            this.txtEA_BetVilkCash.Location = new System.Drawing.Point(146, 23);
            this.txtEA_BetVilkCash.MaxLength = 2;
            this.txtEA_BetVilkCash.Name = "txtEA_BetVilkCash";
            this.txtEA_BetVilkCash.Size = new System.Drawing.Size(33, 20);
            this.txtEA_BetVilkCash.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(581, 77);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Välj användargrupp";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(10, 35);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(231, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Användargrupp:";
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(602, 334);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDiagFile);
            this.groupBox1.Controls.Add(this.chkShowProductGroups);
            this.groupBox1.Controls.Add(this.chkAlwaysCapital);
            this.groupBox1.Controls.Add(this.chkShowDetails);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboCompany);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(612, 88);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bolag";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(282, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Diagnosfil:";
            // 
            // txtDiagFile
            // 
            this.txtDiagFile.Location = new System.Drawing.Point(285, 35);
            this.txtDiagFile.Name = "txtDiagFile";
            this.txtDiagFile.Size = new System.Drawing.Size(147, 20);
            this.txtDiagFile.TabIndex = 5;
            // 
            // chkShowProductGroups
            // 
            this.chkShowProductGroups.AutoSize = true;
            this.chkShowProductGroups.Location = new System.Drawing.Point(285, 65);
            this.chkShowProductGroups.Name = "chkShowProductGroups";
            this.chkShowProductGroups.Size = new System.Drawing.Size(141, 17);
            this.chkShowProductGroups.TabIndex = 4;
            this.chkShowProductGroups.Text = "Använd produktgrupper:";
            this.chkShowProductGroups.UseVisualStyleBackColor = true;
            // 
            // chkAlwaysCapital
            // 
            this.chkAlwaysCapital.AutoSize = true;
            this.chkAlwaysCapital.Location = new System.Drawing.Point(150, 65);
            this.chkAlwaysCapital.Name = "chkAlwaysCapital";
            this.chkAlwaysCapital.Size = new System.Drawing.Size(91, 17);
            this.chkAlwaysCapital.TabIndex = 3;
            this.chkAlwaysCapital.Text = "Alltid versaler:";
            this.chkAlwaysCapital.UseVisualStyleBackColor = true;
            // 
            // chkShowDetails
            // 
            this.chkShowDetails.AutoSize = true;
            this.chkShowDetails.Location = new System.Drawing.Point(21, 65);
            this.chkShowDetails.Name = "chkShowDetails";
            this.chkShowDetails.Size = new System.Drawing.Size(86, 17);
            this.chkShowDetails.TabIndex = 2;
            this.chkShowDetails.Text = "Visa detaljer:";
            this.chkShowDetails.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Välj bolag:";
            // 
            // cboCompany
            // 
            this.cboCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompany.FormattingEnabled = true;
            this.cboCompany.Location = new System.Drawing.Point(21, 35);
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.Size = new System.Drawing.Size(249, 21);
            this.cboCompany.TabIndex = 0;
            this.cboCompany.SelectedIndexChanged += new System.EventHandler(this.cboCompany_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.propertyGrid1);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(639, 475);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 501);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSettings";
            this.Text = "frmSettings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabConfigure.ResumeLayout(false);
            this.tabctrlDetails.ResumeLayout(false);
            this.tabUser.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PropertyGrid propertyGrid1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabConfigure;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cboCompany;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TabControl tabctrlDetails;
    private System.Windows.Forms.TabPage tabUser;
    private System.Windows.Forms.TabPage tabPage4;
    private System.Windows.Forms.CheckBox chkAlwaysCapital;
    private System.Windows.Forms.CheckBox chkShowDetails;
    private System.Windows.Forms.CheckBox chkShowProductGroups;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtDiagFile;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.ComboBox comboBox1;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtEA_BetVilkInvoice;
    private System.Windows.Forms.TextBox txtEA_BetVilkCash;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtThordPassword;
    private System.Windows.Forms.TextBox txtThordUser;
    private System.Windows.Forms.CheckBox chkIsThordUser;
  }
}