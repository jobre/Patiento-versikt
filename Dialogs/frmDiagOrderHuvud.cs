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
	public class frmDiagOrderHuvud : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnClose;
		/// <summary>
		/// Required designer variable.
		/// </summary>

		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ListView lwOrderHuvud;
		public string selOrder = "";

		public frmDiagOrderHuvud()
		{
			InitializeComponent();
		}


		public frmDiagOrderHuvud(ListViewItem[] lw, ref int selIdx)
		{
			InitializeComponent();
			lwOrderHuvud.Items.AddRange(lw);			
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDiagOrderHuvud));
            this.lwOrderHuvud = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lwOrderHuvud
            // 
            this.lwOrderHuvud.AllowColumnReorder = true;
            this.lwOrderHuvud.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lwOrderHuvud.Dock = System.Windows.Forms.DockStyle.Top;
            this.lwOrderHuvud.FullRowSelect = true;
            this.lwOrderHuvud.HideSelection = false;
            this.lwOrderHuvud.LabelWrap = false;
            this.lwOrderHuvud.Location = new System.Drawing.Point(0, 0);
            this.lwOrderHuvud.MultiSelect = false;
            this.lwOrderHuvud.Name = "lwOrderHuvud";
            this.lwOrderHuvud.Size = new System.Drawing.Size(596, 192);
            this.lwOrderHuvud.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.lwOrderHuvud.TabIndex = 0;
            this.lwOrderHuvud.UseCompatibleStateImageBehavior = false;
            this.lwOrderHuvud.View = System.Windows.Forms.View.Details;
            this.lwOrderHuvud.DoubleClick += new System.EventHandler(this.lwOrderHuvud_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Ordernummer";
            this.columnHeader1.Width = 92;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Datum";
            this.columnHeader2.Width = 78;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Status";
            this.columnHeader3.Width = 63;
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
            this.btnClose.Location = new System.Drawing.Point(86, 200);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Stäng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmDiagOrderHuvud
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(596, 238);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lwOrderHuvud);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDiagOrderHuvud";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Order";
            this.Load += new System.EventHandler(this.frmDiagOrderHuvud_Load);
            this.ResumeLayout(false);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			selOrder = lwOrderHuvud.SelectedItems[0].Text;
			this.Close();
		}

		private void frmDiagOrderHuvud_Load(object sender, System.EventArgs e)
		{
			try
			{
				lwOrderHuvud.Items[0].Selected = true;
			}
			catch{}
		}

		private void lwOrderHuvud_DoubleClick(object sender, System.EventArgs e)
		{
			btnOK_Click(sender,null);
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}
