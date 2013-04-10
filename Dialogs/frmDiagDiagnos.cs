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
	public class frmDiagDiagnos : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnClose;
		/// <summary>
		/// Required designer variable.
		/// </summary>

		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ListView lwDiagnos;
		public string selDiagnos = "";

		public frmDiagDiagnos()
		{
			InitializeComponent();
		}

		public frmDiagDiagnos(ListViewItem[] lw)
		{
			InitializeComponent();
			lwDiagnos.Items.AddRange(lw);			
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDiagDiagnos));
            this.lwDiagnos = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lwDiagnos
            // 
            this.lwDiagnos.AllowColumnReorder = true;
            this.lwDiagnos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lwDiagnos.Dock = System.Windows.Forms.DockStyle.Top;
            this.lwDiagnos.FullRowSelect = true;
            this.lwDiagnos.HideSelection = false;
            this.lwDiagnos.LabelWrap = false;
            this.lwDiagnos.Location = new System.Drawing.Point(0, 0);
            this.lwDiagnos.MultiSelect = false;
            this.lwDiagnos.Name = "lwDiagnos";
            this.lwDiagnos.Size = new System.Drawing.Size(370, 192);
            this.lwDiagnos.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lwDiagnos.TabIndex = 0;
            this.lwDiagnos.UseCompatibleStateImageBehavior = false;
            this.lwDiagnos.View = System.Windows.Forms.View.Details;
            this.lwDiagnos.DoubleClick += new System.EventHandler(this.lwProduct_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Kod";
            this.columnHeader1.Width = 74;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Klartext";
            this.columnHeader2.Width = 278;
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
            this.btnClose.Text = "Stäng";
            // 
            // frmDiagDiagnos
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(370, 238);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lwDiagnos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDiagDiagnos";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Diagnoskoder";
            this.ResumeLayout(false);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			selDiagnos = lwDiagnos.SelectedItems[0].Text;
			this.Close();
		}

		private void lwProduct_DoubleClick(object sender, System.EventArgs e)
		{
			btnOK_Click(sender,null);
		}
	}
}
