using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Ortoped.HelpClasses;
using Ortoped.GarpFunctions;
using Ortoped.Definitions;

namespace Ortoped.Dialogs
{
	/// <summary>
	/// Summary description for frmDiagOH.
	/// </summary>
	public class frmDiagThord : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdClose;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ListView lwOrderHuvud;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		public string selOnr = "";
    private ColumnHeader colPriority;
    private ColumnHeader colPnr;
    private ColumnHeader colSort;
    private Button btnNewRead;
    public int iSortCol = 0;

		public frmDiagThord()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDiagThord));
            this.lwOrderHuvud = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPnr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPriority = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.btnNewRead = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lwOrderHuvud
            // 
            this.lwOrderHuvud.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lwOrderHuvud.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.colPnr,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader5,
            this.colPriority,
            this.columnHeader6,
            this.columnHeader4,
            this.colSort});
            this.lwOrderHuvud.FullRowSelect = true;
            this.lwOrderHuvud.Location = new System.Drawing.Point(0, 0);
            this.lwOrderHuvud.Name = "lwOrderHuvud";
            this.lwOrderHuvud.Size = new System.Drawing.Size(820, 262);
            this.lwOrderHuvud.TabIndex = 0;
            this.lwOrderHuvud.UseCompatibleStateImageBehavior = false;
            this.lwOrderHuvud.View = System.Windows.Forms.View.Details;
            this.lwOrderHuvud.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lwOrderHuvud_ColumnClick);
            this.lwOrderHuvud.SelectedIndexChanged += new System.EventHandler(this.lwOrderHuvud_SelectedIndexChanged);
            this.lwOrderHuvud.DoubleClick += new System.EventHandler(this.lwOrderHuvud_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Order";
            this.columnHeader1.Width = 79;
            // 
            // colPnr
            // 
            this.colPnr.Text = "Personnummer";
            this.colPnr.Width = 109;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Datum fr o m";
            this.columnHeader2.Width = 76;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Datum t o m";
            this.columnHeader3.Width = 77;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Status";
            // 
            // colPriority
            // 
            this.colPriority.Text = "Prioritet";
            this.colPriority.Width = 70;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Rekv.Nr";
            this.columnHeader6.Width = 66;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Ordination";
            this.columnHeader4.Width = 270;
            // 
            // colSort
            // 
            this.colSort.Text = "Sorteringskolumn";
            this.colSort.Width = 0;
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdOK.Location = new System.Drawing.Point(8, 278);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 1;
            this.cmdOK.Text = "OK";
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdClose.Location = new System.Drawing.Point(96, 278);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 2;
            this.cmdClose.Text = "Stäng";
            // 
            // btnNewRead
            // 
            this.btnNewRead.Location = new System.Drawing.Point(719, 278);
            this.btnNewRead.Name = "btnNewRead";
            this.btnNewRead.Size = new System.Drawing.Size(75, 23);
            this.btnNewRead.TabIndex = 3;
            this.btnNewRead.Text = "Ny läsning";
            this.btnNewRead.UseVisualStyleBackColor = true;
            this.btnNewRead.Click += new System.EventHandler(this.btnNewRead_Click);
            // 
            // frmDiagThord
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(818, 316);
            this.Controls.Add(this.btnNewRead);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.lwOrderHuvud);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDiagThord";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ej signerade remisser från Thord";
            this.Load += new System.EventHandler(this.frmDiagOH_Load);
            this.ResumeLayout(false);

		}
		#endregion


		public frmDiagThord(ListViewItem[] lw, bool thorduser)
		{
			InitializeComponent();
			lwOrderHuvud.Items.AddRange(lw);

      if (thorduser)
        btnNewRead.Visible = true;
      else
        btnNewRead.Visible = false;

		}

		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			selOnr = lwOrderHuvud.SelectedItems[0].Text;
			this.Close();
		}

		private void frmDiagOH_Load(object sender, System.EventArgs e)
		{
			try
			{
        this.lwOrderHuvud.ListViewItemSorter = new ListViewItemComparer(iSortCol);
        lwOrderHuvud.Items[0].Selected = true;
			}
			catch{}
		}

		private void lwOrderHuvud_DoubleClick(object sender, System.EventArgs e)
		{
			cmdOK_Click(sender,e);
		}

    private void lwOrderHuvud_ColumnClick(object sender, ColumnClickEventArgs e)
    {
      if(e.Column == 5)
        this.lwOrderHuvud.ListViewItemSorter = new ListViewItemComparer(lwOrderHuvud.Columns.Count -1);
      else
        this.lwOrderHuvud.ListViewItemSorter = new ListViewItemComparer(e.Column);
    }

    private void lwOrderHuvud_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void btnNewRead_Click(object sender, EventArgs e)
    {
      orderFunc oOH = new orderFunc();
      lwOrderHuvud.Items.Clear();
      lwOrderHuvud.Items.AddRange(OrderHeadDefinition.convertToThordListView(oOH.getAllOHWithOrderType(Config.getNumberSerie(), false)));
    }
	}

}
