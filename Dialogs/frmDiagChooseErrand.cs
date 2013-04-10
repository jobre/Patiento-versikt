using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Ortoped.GarpFunctions;

namespace Ortoped.Dialogs
{
	/// <summary>
	/// Summary description for frmDiagChooseErrand.
	/// </summary>
	public class frmDiagChooseErrand : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView lwOrder;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ListView lwErrand;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private ErrandFunc erUnbound = new ErrandFunc();
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnClose;
		private string mCust = "", mOnr = "", mAidid = "";

		public string mErrandId = "";

		public frmDiagChooseErrand()
		{
			InitializeComponent();
		}

		public frmDiagChooseErrand(string cust, string onr, string aidid)
		{
			mCust = cust;
			mOnr = onr;
			mAidid = aidid;

			InitializeComponent();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDiagChooseErrand));
            this.lwOrder = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lwErrand = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lwOrder
            // 
            this.lwOrder.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11});
            this.lwOrder.FullRowSelect = true;
            this.lwOrder.HideSelection = false;
            this.lwOrder.Location = new System.Drawing.Point(0, 0);
            this.lwOrder.MultiSelect = false;
            this.lwOrder.Name = "lwOrder";
            this.lwOrder.Size = new System.Drawing.Size(336, 112);
            this.lwOrder.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lwOrder.TabIndex = 9;
            this.lwOrder.UseCompatibleStateImageBehavior = false;
            this.lwOrder.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Order";
            this.columnHeader6.Width = 54;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Datum fr o m";
            this.columnHeader7.Width = 73;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Datum t o m";
            this.columnHeader8.Width = 72;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Status";
            this.columnHeader9.Width = 45;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Rekvisition";
            this.columnHeader10.Width = 0;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Ordination";
            this.columnHeader11.Width = 80;
            // 
            // lwErrand
            // 
            this.lwErrand.AllowColumnReorder = true;
            this.lwErrand.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lwErrand.Dock = System.Windows.Forms.DockStyle.Top;
            this.lwErrand.FullRowSelect = true;
            this.lwErrand.HideSelection = false;
            this.lwErrand.LabelWrap = false;
            this.lwErrand.Location = new System.Drawing.Point(0, 0);
            this.lwErrand.MultiSelect = false;
            this.lwErrand.Name = "lwErrand";
            this.lwErrand.Size = new System.Drawing.Size(576, 192);
            this.lwErrand.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lwErrand.TabIndex = 10;
            this.lwErrand.UseCompatibleStateImageBehavior = false;
            this.lwErrand.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Datum";
            this.columnHeader1.Width = 66;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Starttid";
            this.columnHeader2.Width = 74;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Pågår";
            this.columnHeader3.Width = 64;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Kontaktperson";
            this.columnHeader4.Width = 175;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "ID";
            this.columnHeader5.Width = 0;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(8, 208);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "Välj";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(88, 208);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "Stäng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmDiagChooseErrand
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(576, 246);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lwErrand);
            this.Controls.Add(this.lwOrder);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDiagChooseErrand";
            this.Text = "Välj ärende";
            this.Load += new System.EventHandler(this.frmDiagChooseErrand_Load);
            this.ResumeLayout(false);

		}
		#endregion

		private void frmDiagChooseErrand_Load(object sender, System.EventArgs e)
		{
			lwErrand.Items.AddRange(ErrandFunc.Errand.convertToErrand(erUnbound.getErrandsOnAid(mCust,mOnr,mAidid)));
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			if(lwErrand.SelectedItems.Count > 0)
			{
				mErrandId = lwErrand.SelectedItems[0].SubItems[4].Text;
				this.Close();
			}

		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();		
		}
	}
}
