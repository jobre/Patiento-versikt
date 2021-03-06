using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Ortoped.GarpFunctions;

namespace Ortoped.Dialogs
{
	/// <summary>
	/// Summary description for frmDiagPatient.
	/// </summary>
	public class frmDiagFkn : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnClose;
		/// <summary>
		/// Required designer variable.
		/// </summary>

		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ListView lwFkn;
		public string selCust = "";

		public frmDiagFkn()
		{
			InitializeComponent();
		}

		public frmDiagFkn(ListViewItem[] lw, ref int selIdx)
		{
			InitializeComponent();
			lwFkn.Items.AddRange(lw);			
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDiagFkn));
            this.lwFkn = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lwFkn
            // 
            this.lwFkn.AllowColumnReorder = true;
            this.lwFkn.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lwFkn.Dock = System.Windows.Forms.DockStyle.Top;
            this.lwFkn.FullRowSelect = true;
            this.lwFkn.HideSelection = false;
            this.lwFkn.LabelWrap = false;
            this.lwFkn.Location = new System.Drawing.Point(0, 0);
            this.lwFkn.MultiSelect = false;
            this.lwFkn.Name = "lwFkn";
            this.lwFkn.Size = new System.Drawing.Size(634, 192);
            this.lwFkn.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lwFkn.TabIndex = 0;
            this.lwFkn.UseCompatibleStateImageBehavior = false;
            this.lwFkn.View = System.Windows.Forms.View.Details;
            this.lwFkn.DoubleClick += new System.EventHandler(this.lwFkn_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Kundnummer";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Namn";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Ort";
            this.columnHeader3.Width = 150;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(8, 200);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(88, 200);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "St�ng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmDiagFkn
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(634, 238);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lwFkn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDiagFkn";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Fakturakunder";
            this.Load += new System.EventHandler(this.frmDiagPatient_Load);
            this.ResumeLayout(false);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			selCust = lwFkn.SelectedItems[0].Text;
			this.Close();
		}

		private void frmDiagPatient_Load(object sender, System.EventArgs e)
		{
			try
			{
				lwFkn.Items[0].Selected = true;
			}
			catch{}
		}

		private void lwFkn_DoubleClick(object sender, System.EventArgs e)
		{
			btnOK_Click(sender, null);
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			selCust = "";
		}
	}
}
