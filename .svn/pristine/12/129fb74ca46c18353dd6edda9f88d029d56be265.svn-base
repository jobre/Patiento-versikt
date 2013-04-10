using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Ortoped.HelpClasses;

namespace Ortoped.Dialogs
{
    public class frmDiagOH : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ListView lwOrderHuvud;
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        public string selOnr = "";
        private ColumnHeader colPriority;
        //public int iSortCol = 0;
        private HelpClasses.ListViewItemComparer mListViewSorter;

        public frmDiagOH()
        {
            InitializeComponent();
        }

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDiagOH));
            this.lwOrderHuvud = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPriority = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lwOrderHuvud
            // 
            resources.ApplyResources(this.lwOrderHuvud, "lwOrderHuvud");
            this.lwOrderHuvud.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader5,
            this.colPriority,
            this.columnHeader6,
            this.columnHeader4});
            this.lwOrderHuvud.FullRowSelect = true;
            this.lwOrderHuvud.Name = "lwOrderHuvud";
            this.lwOrderHuvud.UseCompatibleStateImageBehavior = false;
            this.lwOrderHuvud.View = System.Windows.Forms.View.Details;
            this.lwOrderHuvud.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lwOrderHuvud_ColumnClick);
            this.lwOrderHuvud.DoubleClick += new System.EventHandler(this.lwOrderHuvud_DoubleClick);
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // columnHeader3
            // 
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            // 
            // columnHeader5
            // 
            resources.ApplyResources(this.columnHeader5, "columnHeader5");
            // 
            // colPriority
            // 
            resources.ApplyResources(this.colPriority, "colPriority");
            // 
            // columnHeader6
            // 
            resources.ApplyResources(this.columnHeader6, "columnHeader6");
            // 
            // columnHeader4
            // 
            resources.ApplyResources(this.columnHeader4, "columnHeader4");
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdClose
            // 
            resources.ApplyResources(this.cmdClose, "cmdClose");
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Name = "cmdClose";
            // 
            // frmDiagOH
            // 
            this.AcceptButton = this.cmdOK;
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.cmdClose;
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.lwOrderHuvud);
            this.Name = "frmDiagOH";
            this.ResumeLayout(false);

        }
        #endregion


        public frmDiagOH(ListViewItem[] lw)
        {
            InitializeComponent();
            lwOrderHuvud.Items.AddRange(lw);

            mListViewSorter = new HelpClasses.ListViewItemComparer();
            this.lwOrderHuvud.ListViewItemSorter = mListViewSorter;

            // Set the column number that is to be sorted; default to ascending.
            mListViewSorter.SortColumn = 1;
            mListViewSorter.Order = SortOrder.Descending;
            this.lwOrderHuvud.Sort();
        }

        private void cmdOK_Click(object sender, System.EventArgs e)
        {
            if (lwOrderHuvud.SelectedItems.Count > 0)
                selOnr = lwOrderHuvud.SelectedItems[0].Text;
            else
                selOnr = "";

            this.Close();
        }

        //private void frmDiagOH_Load(object sender, System.EventArgs e)
        //{
        //    try
        //    {
        //        this.lwOrderHuvud.ListViewItemSorter = new ListViewItemComparer(iSortCol);
        //        lwOrderHuvud.Items[0].Selected = true;
        //    }
        //    catch { }
        //}

        private void lwOrderHuvud_DoubleClick(object sender, System.EventArgs e)
        {
            cmdOK_Click(sender, e);
        }

        private void lwOrderHuvud_ColumnClick(object sender, ColumnClickEventArgs e)
        {
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
            this.lwOrderHuvud.Sort();
        }
    }
}
