using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Ortoped.GarpFunctions;
using Ortoped.HelpClasses;
using Ortoped.Definitions;

namespace Ortoped.Dialogs
{
    /// <summary>
    /// Summary description for frmDiagUnboundErrand.
    /// </summary>
    public class frmDiagUnboundErrand : System.Windows.Forms.Form
    {
        private System.Windows.Forms.ListView lwErrand;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader columnHeader5;

        private ErrandFunc erUnbound;
        private orderFunc oOH = new orderFunc();
        private OrderRowFunc oOR = new OrderRowFunc();
        private OrderRowText oText = new OrderRowText();
        private OrderHeadDefinition oOHDefenition = new OrderHeadDefinition();

        public string sCustomer, sOnr;
        private System.Windows.Forms.ListView lwOrder;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ListView lwAid;


        public frmDiagUnboundErrand()
        {
            InitializeComponent();
        }

        public frmDiagUnboundErrand(string cust, string onr)
        {
            InitializeComponent();
            erUnbound = new ErrandFunc();
            sCustomer = cust;
            sOnr = onr;

            lwErrand.Items.AddRange(ErrandFunc.Errand.convertToErrand(erUnbound.getUnboundErrands(cust, onr)));
            if (lwErrand.Items.Count > 0)
                lwErrand.Items[0].Selected = true;

            // Fyll combo med orderinformation
            lwOrder.Items.AddRange(OrderHeadDefinition.convertToListView(oOH.getPatientsAllOH(cust)));
        }



        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDiagUnboundErrand));
            this.lwErrand = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lwAid = new System.Windows.Forms.ListView();
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lwOrder = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
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
            this.lwErrand.Size = new System.Drawing.Size(650, 192);
            this.lwErrand.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lwErrand.TabIndex = 1;
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
            // btnAdd
            // 
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAdd.Location = new System.Drawing.Point(16, 360);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Lägg till";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClose.Location = new System.Drawing.Point(96, 360);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Stäng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Ordernummer:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lwAid);
            this.groupBox1.Controls.Add(this.lwOrder);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 200);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(632, 152);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Koppla tidbokning till";
            // 
            // lwAid
            // 
            this.lwAid.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14});
            this.lwAid.FullRowSelect = true;
            this.lwAid.HideSelection = false;
            this.lwAid.Location = new System.Drawing.Point(352, 40);
            this.lwAid.MultiSelect = false;
            this.lwAid.Name = "lwAid";
            this.lwAid.Size = new System.Drawing.Size(272, 96);
            this.lwAid.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lwAid.TabIndex = 9;
            this.lwAid.UseCompatibleStateImageBehavior = false;
            this.lwAid.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "ID";
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Artikel";
            this.columnHeader13.Width = 78;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Benämning";
            this.columnHeader14.Width = 117;
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
            this.lwOrder.Location = new System.Drawing.Point(16, 40);
            this.lwOrder.MultiSelect = false;
            this.lwOrder.Name = "lwOrder";
            this.lwOrder.Size = new System.Drawing.Size(328, 96);
            this.lwOrder.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lwOrder.TabIndex = 8;
            this.lwOrder.UseCompatibleStateImageBehavior = false;
            this.lwOrder.View = System.Windows.Forms.View.Details;
            this.lwOrder.SelectedIndexChanged += new System.EventHandler(this.lwOrder_SelectedIndexChanged);
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
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(352, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Hjälpmedel";
            // 
            // frmDiagUnboundErrand
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(650, 392);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lwErrand);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDiagUnboundErrand";
            this.Text = "Koppla ärenden";
            this.Load += new System.EventHandler(this.frmDiagUnboundErrand_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private void frmDiagUnboundErrand_Load(object sender, System.EventArgs e)
        {
            int idx = 0;

            foreach (ListViewItem lw in lwOrder.Items)
            {
                if (lw.Text.Equals(sOnr))
                {
                    idx = lw.Index;
                    break;
                }
            }

            lwOrder.Items[idx].Selected = true;
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            string sAidId = "";

            if ((lwErrand.SelectedItems.Count > 0) && (lwOrder.SelectedItems.Count > 0))
            {
                // Check if aidid i selected, we alow user to deside if they want to choose aidid or not
                if (lwAid.SelectedItems.Count > 0)
                {
                    sAidId = lwAid.SelectedItems[0].Text;
                    oOR.updateAidWithErrand(lwOrder.SelectedItems[0].Text, sAidId, lwErrand.SelectedItems[0].SubItems[5].Text, lwErrand.SelectedItems[0].SubItems[0].Text, lwErrand.SelectedItems[0].SubItems[1].Text, lwErrand.SelectedItems[0].SubItems[2].Text);
                }

                erUnbound.updateErrandWithOrderNumber(lwErrand.SelectedItems[0].SubItems[5].Text, sCustomer, lwOrder.SelectedItems[0].Text, sAidId);
                lwErrand.SelectedItems[0].Remove();
            }
            else
            {
                MessageBox.Show(this, "Du måste välja ärende, order och hjälmedel som skall kopplas", "Val saknas", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void lwOrder_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            lwAid.Items.Clear();

            try
            {
                // Fyll combo med orderinformation
                lwAid.Items.AddRange(Aid.convertToAnotherSmallListView(oOR.getAllAid(lwOrder.SelectedItems[0].Text.Trim())));
            }
            catch { }
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
