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
	public class frmDiagProduct : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnClose;
		/// <summary>
		/// Required designer variable.
		/// </summary>

		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ListView lwProduct;
		public string selProd = "";

		public frmDiagProduct()
		{
			InitializeComponent();
		}

		public frmDiagProduct(ListViewItem[] lw, ref int selIdx)
		{
			InitializeComponent();
			lwProduct.Items.AddRange(lw);			
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDiagProduct));
            this.lwProduct = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lwProduct
            // 
            this.lwProduct.AllowColumnReorder = true;
            this.lwProduct.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lwProduct.Dock = System.Windows.Forms.DockStyle.Top;
            this.lwProduct.FullRowSelect = true;
            this.lwProduct.HideSelection = false;
            this.lwProduct.LabelWrap = false;
            this.lwProduct.Location = new System.Drawing.Point(0, 0);
            this.lwProduct.MultiSelect = false;
            this.lwProduct.Name = "lwProduct";
            this.lwProduct.Size = new System.Drawing.Size(402, 192);
            this.lwProduct.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lwProduct.TabIndex = 0;
            this.lwProduct.UseCompatibleStateImageBehavior = false;
            this.lwProduct.View = System.Windows.Forms.View.Details;
            this.lwProduct.DoubleClick += new System.EventHandler(this.lwProduct_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Artikelnummer";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Benämning";
            this.columnHeader2.Width = 258;
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
            // frmDiagProduct
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(402, 238);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lwProduct);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDiagProduct";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Artiklar";
            this.Load += new System.EventHandler(this.frmDiagPatient_Load);
            this.ResumeLayout(false);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			selProd = lwProduct.SelectedItems[0].Text;
			this.Close();
		}

		private void frmDiagPatient_Load(object sender, System.EventArgs e)
		{
			try
			{
				lwProduct.Items[0].Selected = true;
			}
			catch{}
		}

		private void lwProduct_DoubleClick(object sender, System.EventArgs e)
		{
			btnOK_Click(sender,null);
		}
	}
}
