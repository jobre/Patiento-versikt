using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Ortoped.Dialogs
{
	/// <summary>
	/// Summary description for frmDiagPatient.
	/// </summary>
	public class frmDiagPatient : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ListView lwPatient;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnClose;
        private HelpClasses.ListViewItemComparer mListViewSorter;
		/// <summary>
		/// Required designer variable.
		/// </summary>

		private System.ComponentModel.Container components = null;
		public int selidx = -1;
        public string SSN = "";

        //public frmDiagPatient()
        //{
        //    InitializeComponent();
        //}

		public frmDiagPatient(ListViewItem[] lw)
		{
			InitializeComponent();
			lwPatient.Items.AddRange(lw);

            mListViewSorter = new HelpClasses.ListViewItemComparer();
            this.lwPatient.ListViewItemSorter = mListViewSorter;

            // Set the column number that is to be sorted; default to ascending.
            mListViewSorter.SortColumn = 0;
            mListViewSorter.Order = SortOrder.Ascending;
            this.lwPatient.Sort();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDiagPatient));
            this.lwPatient = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lwPatient
            // 
            this.lwPatient.AllowColumnReorder = true;
            this.lwPatient.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lwPatient.Dock = System.Windows.Forms.DockStyle.Top;
            this.lwPatient.FullRowSelect = true;
            this.lwPatient.HideSelection = false;
            this.lwPatient.LabelWrap = false;
            this.lwPatient.Location = new System.Drawing.Point(0, 0);
            this.lwPatient.MultiSelect = false;
            this.lwPatient.Name = "lwPatient";
            this.lwPatient.Size = new System.Drawing.Size(634, 192);
            this.lwPatient.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lwPatient.TabIndex = 0;
            this.lwPatient.UseCompatibleStateImageBehavior = false;
            this.lwPatient.View = System.Windows.Forms.View.Details;
            this.lwPatient.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lwPatient_ColumnClick);
            this.lwPatient.DoubleClick += new System.EventHandler(this.lwPatient_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Personnummer";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Efternamn";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Förnamn";
            this.columnHeader3.Width = 150;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Pnr/Ort";
            this.columnHeader4.Width = 200;
            // 
            // btnOK
            // 
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
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
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(88, 200);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Stäng";
            // 
            // frmDiagPatient
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(634, 238);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lwPatient);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDiagPatient";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Patienter";
            this.Load += new System.EventHandler(this.frmDiagPatient_Load);
            this.ResumeLayout(false);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			selidx = lwPatient.SelectedItems[0].Index;
            SSN = lwPatient.SelectedItems[0].Text;
 			this.Close();
            
		}

		private void frmDiagPatient_Load(object sender, System.EventArgs e)
		{
			try
			{
				lwPatient.Items[0].Selected = true;
			}
			catch{}

		}

		private void lwPatient_DoubleClick(object sender, System.EventArgs e)
		{
			btnOK_Click(sender, null);
		}

        private void lwPatient_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListView temp = new ListView();

            if (e.Column == mListViewSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (mListViewSorter.Order == SortOrder.Ascending)
                {
                    mListViewSorter.Order = SortOrder.Descending;
                }
                else
                {
                    mListViewSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                mListViewSorter.SortColumn = e.Column;
                mListViewSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.lwPatient.Sort();
          
        }

	}
}
