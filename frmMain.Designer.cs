using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Globalization;
using System.Configuration;
using Ortoped.GarpFunctions;
using Ortoped.HelpClasses;
using Ortoped.Dialogs;
using Ortoped.Definitions;
using System.Xml;
using Microsoft.Win32;
using Excido;
using System.IO;
using Ortoped.Thord;
using GCS;

namespace Ortoped
{
    public partial class frmMain : System.Windows.Forms.Form
    {

        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.Panel pnlMainLeft;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.TextBox txtONR;
        private System.Windows.Forms.ContextMenu mnuOrderRow;
        private System.Windows.Forms.MenuItem menuItem12;
        private System.Windows.Forms.MenuItem mnuNewAid;
        private System.Windows.Forms.MenuItem mnuOwnFee;
        private System.Windows.Forms.Button btnOrderList;
        private System.Windows.Forms.MenuItem mnuDeliver;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem mnuPrintDocument;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.StatusBarPanel pnlBol;
        private System.Windows.Forms.StatusBarPanel pnlKst;
        private System.Windows.Forms.StatusBarPanel pnlUser;
        private System.Windows.Forms.StatusBarPanel pnlGroup;
        private System.Windows.Forms.StatusBarPanel pnlOrderStat;
        private System.Windows.Forms.StatusBarPanel pnlCustomConfig;
        private GroupBox grbOr;
        private TabControl tabctrlRow;
        private TabPage tabPage1;
        private ListView lwOr;
        private ColumnHeader colAidNr;
        private ColumnHeader colArt;
        private ColumnHeader colBen;
        private ColumnHeader colAnt;
        private ColumnHeader colApris;
        private ColumnHeader colEgenAvgift;
        private ColumnHeader colHandl;
        private ColumnHeader colProdstatus;
        private ColumnHeader colLevtid;
        private TabPage tabNew;
        private GroupBox grbAid;
        private ComboBox cboAidPriority;
        private TextBox txtOrDatum;
        private CheckBox chkGaranti;
        private Label label28;
        private TextBox txtAidId;
        private DateTimePicker dtpLevtid;
        private Label label27;
        private Label label26;
        private ComboBox cboProdStatus;
        private Label label25;
        private ComboBox cboHandler;
        private Label label3;
        private Label label18;
        private GroupBox grbArt;
        private GroupBox groupBox1;
        private RadioButton rdBestalld;
        private RadioButton rdBestallEj;
        private RadioButton rdBestall;
        private CheckBox chkViewState;
        private TextBox txtRDC;
        private Button btnDelete;
        private Button btnAdd;
        private TextBox txtORA;
        private Label labORA;
        private Label label22;
        private TextBox txtPRI;
        private TextBox txtBEN;
        private Label label21;
        private Label label1;
        private TextBox txtANR;
        private TabPage tabPage2;
        private RichTextBox txtOrText;
        private GroupBox grbTid;
        private ListView lwErrand;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader6;
        private GroupBox grbPatient;
        private CheckBox chkCopDok;
        private Label label16;
        private Label label15;
        private TextBox txtTelMobil;
        private TextBox txtTelArbete;
        private TextBox txtKNR;
        private Label labANM;
        private TextBox txtANM;
        private CheckBox chkJournal;
        private TextBox txtTelBostad;
        private TextBox txtORT;
        private TextBox txtADD;
        private TextBox txtSN;
        private TextBox txtLN;
        private TextBox txtPNR;
        private Label labTEL;
        private Label labORT;
        private Label labADD;
        private Label labSN;
        private Label labLN;
        private Label labPnr;
        private ColumnHeader columnHeader9;
        private TextBox txtLevDate;
        private RichTextBox txtAidText;
        private Label label34;
        private TextBox txtLabAidTexter;
        private TextBox txtLabArtTexter;
        private MenuItem menuItem9;
        private ColumnHeader colFakNr;
        private ColumnHeader colFakDat;
        private ComboBox cboNeedStep;
        private Label label24;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem avslutaToolStripMenuItem;
        private ToolStripMenuItem omToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripSeparator toolStripSeparator1;
        private CheckBox chkDeceased;
        private MenuItem menuItem10;
        private MenuItem menuItem11;
        private MenuItem menuItem13;
        private ContextMenuStrip mnuAidRows;
        private ToolStripMenuItem mnuSetViewStat;
        private MenuItem mnuReceipt;
        private MenuItem menuItem2;
        private Button btnPrevPatient;
        private Button btnSwitchPatient;
        private Button btnCloseOrder;
        private Button btnDeleteOrder;
        private Button btnNewOrder;
        private Button btnClose;
        private Button btnGoToCustomer;
        private Button btnGoToAccounting;
        private Button btnGoToCalendar;
        private ToolStripMenuItem mnuSendErrorReport;
        private ToolStripMenuItem mnuThord;
        private ToolStripMenuItem mnuGetReferrals;
        private TextBox txtHandler;
        private StatusBarPanel pnlORSaved;
        private CheckBox chkUpdatedInThord;
        private StatusBarPanel pnlSaveStat;
        private MenuStrip mnuMain;
        private ToolTip toolTip1;
        private ToolStripMenuItem arkivToolStripMenuItem;
        private ToolStripMenuItem mnuQuit;
        private ToolStripMenuItem thordToolStripMenuItem;
        private ToolStripMenuItem getReferrals;
        private TextBox txtAidOid;
        private TextBox txtPartOid;
        private CheckBox chkFirstTimePatient;
        private IContainer components;

        public frmMain()
        {
            //createRegistryPost();
            InitializeComponent();
            createObject();
        }

        public frmMain(string orderNo)
        {
            Log4Net.Logger.loggInfo("frmMain ordernr: " + orderNo, "", "frmMain(string orderNo)");
			
            //createRegistryPost();
            InitializeComponent();
            if (createObject())
                setOrder(orderNo);
        }

        private bool createObject()
        {
            try
            {
                frmControler = FormControler.getInstance();

                oCust = new CustomerFunc();
                oOH = new orderFunc();
                oOR = new OrderRowFunc();
                oErr = new ErrandFunc();
                oCon = new Contacts();
                oPrislista = new Prislista();
                oDelM = new DeliveryMode();
                oDiagnos = new Diagnos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Garp verkar inte vara startat eller så inträffades \nett fel vid start (se felmeddelande), prova att starta Garp \noch försök igen \n\n(" + ex.Message + ")", "Fel vid start", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return false;
            }
            return true;
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.txtONR = new System.Windows.Forms.TextBox();
            this.grbOr = new System.Windows.Forms.GroupBox();
            this.tabctrlRow = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lwOr = new System.Windows.Forms.ListView();
            this.colAidNr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colArt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colBen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAnt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colApris = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEgenAvgift = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHandl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colProdstatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLevtid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFakNr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFakDat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHlpTextExists = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colUrgent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colProdTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAidText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mnuOrderRow = new System.Windows.Forms.ContextMenu();
            this.mnuNewAid = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.mnuOwnFee = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.mnuDeliver = new System.Windows.Forms.MenuItem();
            this.mnuReceipt = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.mnuPrintDocument = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.tabNew = new System.Windows.Forms.TabPage();
            this.scProductsAndSum = new System.Windows.Forms.SplitContainer();
            this.grbArtList = new System.Windows.Forms.GroupBox();
            this.lwAidRows = new System.Windows.Forms.ListView();
            this.colAidRowsArtNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAidRowsBen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAidRowsPcs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAidRowsRdc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mnuAidRows = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuSetViewStat = new System.Windows.Forms.ToolStripMenuItem();
            this.visaMaterialplaneringenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grpSum = new System.Windows.Forms.GroupBox();
            this.labSumInternal = new System.Windows.Forms.Label();
            this.labSumExternal = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.cboProdStatus = new System.Windows.Forms.ComboBox();
            this.grbAid = new System.Windows.Forms.GroupBox();
            this.labRemissNr = new System.Windows.Forms.Label();
            this.txtOR_ONR = new System.Windows.Forms.TextBox();
            this.txtHolder = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.chkFirstTimePatient = new System.Windows.Forms.CheckBox();
            this.txtPartOid = new System.Windows.Forms.TextBox();
            this.txtAidOid = new System.Windows.Forms.TextBox();
            this.txtHandler = new System.Windows.Forms.TextBox();
            this.txtLevDate = new System.Windows.Forms.TextBox();
            this.cboAidPriority = new System.Windows.Forms.ComboBox();
            this.txtOrDatum = new System.Windows.Forms.TextBox();
            this.chkGaranti = new System.Windows.Forms.CheckBox();
            this.label28 = new System.Windows.Forms.Label();
            this.txtAidId = new System.Windows.Forms.TextBox();
            this.dtpLevtid = new System.Windows.Forms.DateTimePicker();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.cboHandler = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.grbArt = new System.Windows.Forms.GroupBox();
            this.chkUpdatedInThord = new System.Windows.Forms.CheckBox();
            this.label24 = new System.Windows.Forms.Label();
            this.cboNeedStep = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdBestalld = new System.Windows.Forms.RadioButton();
            this.rdBestallEj = new System.Windows.Forms.RadioButton();
            this.rdBestall = new System.Windows.Forms.RadioButton();
            this.chkViewState = new System.Windows.Forms.CheckBox();
            this.txtRDC = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtORA = new System.Windows.Forms.TextBox();
            this.labORA = new System.Windows.Forms.Label();
            this.txtPRI = new System.Windows.Forms.TextBox();
            this.txtBEN = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtANR = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtLabAidTexter = new System.Windows.Forms.TextBox();
            this.txtLabArtTexter = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.txtAidText = new System.Windows.Forms.RichTextBox();
            this.txtOrText = new System.Windows.Forms.RichTextBox();
            this.tabpProduction = new System.Windows.Forms.TabPage();
            this.dtpConditionDate = new System.Windows.Forms.DateTimePicker();
            this.label36 = new System.Windows.Forms.Label();
            this.chkUrgent = new System.Windows.Forms.CheckBox();
            this.dtpPromisedDeliverDate = new System.Windows.Forms.DateTimePicker();
            this.txtProductionTitle = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.grbTid = new System.Windows.Forms.GroupBox();
            this.btnGoToCalendar = new System.Windows.Forms.Button();
            this.lwErrand = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grbPatient = new System.Windows.Forms.GroupBox();
            this.txtTelSMS = new System.Windows.Forms.TextBox();
            this.btnCopDoc2 = new System.Windows.Forms.Button();
            this.btnCopDoc1 = new System.Windows.Forms.Button();
            this.btnGoToAccounting = new System.Windows.Forms.Button();
            this.btnGoToCustomer = new System.Windows.Forms.Button();
            this.chkDeceased = new System.Windows.Forms.CheckBox();
            this.chkCopDok = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtTelMobil = new System.Windows.Forms.TextBox();
            this.txtTelArbete = new System.Windows.Forms.TextBox();
            this.txtKNR = new System.Windows.Forms.TextBox();
            this.labANM = new System.Windows.Forms.Label();
            this.txtANM = new System.Windows.Forms.TextBox();
            this.chkJournal = new System.Windows.Forms.CheckBox();
            this.txtTelBostad = new System.Windows.Forms.TextBox();
            this.txtORT = new System.Windows.Forms.TextBox();
            this.txtADD = new System.Windows.Forms.TextBox();
            this.txtSN = new System.Windows.Forms.TextBox();
            this.txtLN = new System.Windows.Forms.TextBox();
            this.txtPNR = new System.Windows.Forms.TextBox();
            this.labTEL = new System.Windows.Forms.Label();
            this.labORT = new System.Windows.Forms.Label();
            this.labADD = new System.Windows.Forms.Label();
            this.labSN = new System.Windows.Forms.Label();
            this.labLN = new System.Windows.Forms.Label();
            this.labPnr = new System.Windows.Forms.Label();
            this.pnlMainLeft = new System.Windows.Forms.Panel();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNewOrder = new System.Windows.Forms.Button();
            this.btnDeleteOrder = new System.Windows.Forms.Button();
            this.btnCloseOrder = new System.Windows.Forms.Button();
            this.btnSwitchPatient = new System.Windows.Forms.Button();
            this.btnPrevPatient = new System.Windows.Forms.Button();
            this.grbOH = new System.Windows.Forms.GroupBox();
            this.txtOTADate = new System.Windows.Forms.TextBox();
            this.dtOTADate = new System.Windows.Forms.DateTimePicker();
            this.btnSMS = new System.Windows.Forms.Button();
            this.btnStartProdView = new System.Windows.Forms.Button();
            this.btnOrderList = new System.Windows.Forms.Button();
            this.btnThord = new System.Windows.Forms.Button();
            this.btnIO = new System.Windows.Forms.Button();
            this.btnSaveOH = new System.Windows.Forms.Button();
            this.txtOrdination = new System.Windows.Forms.TextBox();
            this.txtTillagg = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.txtGilltigFrom = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.txtSignature = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboOrdinator = new System.Windows.Forms.ComboBox();
            this.cboAidType = new System.Windows.Forms.ComboBox();
            this.txtERF = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.txtDiagID = new System.Windows.Forms.TextBox();
            this.cboPrislista = new System.Windows.Forms.ComboBox();
            this.txtDiagTxt = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.txtEndDate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAidCount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtYears = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cboSignature = new System.Windows.Forms.ComboBox();
            this.txtODT = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtKlinikNamn = new System.Windows.Forms.TextBox();
            this.txtNotering = new System.Windows.Forms.TextBox();
            this.txtKlinik = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.txtFKN = new System.Windows.Forms.TextBox();
            this.btnDiagUppslag = new System.Windows.Forms.Button();
            this.txtFKN_NAM = new System.Windows.Forms.TextBox();
            this.dtpGilltigFrom = new System.Windows.Forms.DateTimePicker();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.pnlBol = new System.Windows.Forms.StatusBarPanel();
            this.pnlGroup = new System.Windows.Forms.StatusBarPanel();
            this.pnlUser = new System.Windows.Forms.StatusBarPanel();
            this.pnlKst = new System.Windows.Forms.StatusBarPanel();
            this.pnlCustomConfig = new System.Windows.Forms.StatusBarPanel();
            this.pnlOrderStat = new System.Windows.Forms.StatusBarPanel();
            this.pnlORSaved = new System.Windows.Forms.StatusBarPanel();
            this.pnlSaveStat = new System.Windows.Forms.StatusBarPanel();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.avslutaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThord = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGetReferrals = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSendErrorReport = new System.Windows.Forms.ToolStripMenuItem();
            this.omToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bwThord = new System.ComponentModel.BackgroundWorker();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.arkivToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.thordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hjälpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skickaLoggfilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcutsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ordernummerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.personnummerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.getReferrals = new System.Windows.Forms.ToolStripMenuItem();
            this.bwGetOrdinators = new System.ComponentModel.BackgroundWorker();
            this.bwGarp = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlBottom.SuspendLayout();
            this.grbOr.SuspendLayout();
            this.tabctrlRow.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabNew.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scProductsAndSum)).BeginInit();
            this.scProductsAndSum.Panel1.SuspendLayout();
            this.scProductsAndSum.Panel2.SuspendLayout();
            this.scProductsAndSum.SuspendLayout();
            this.grbArtList.SuspendLayout();
            this.mnuAidRows.SuspendLayout();
            this.grpSum.SuspendLayout();
            this.grbAid.SuspendLayout();
            this.grbArt.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabpProduction.SuspendLayout();
            this.grbTid.SuspendLayout();
            this.grbPatient.SuspendLayout();
            this.pnlMainLeft.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.grbOH.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlKst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCustomConfig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlOrderStat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlORSaved)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSaveStat)).BeginInit();
            this.mnuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.txtONR);
            this.pnlBottom.Controls.Add(this.grbOr);
            this.pnlBottom.Controls.Add(this.grbTid);
            this.pnlBottom.Controls.Add(this.grbPatient);
            this.pnlBottom.Controls.Add(this.pnlMainLeft);
            this.pnlBottom.Controls.Add(this.grbOH);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBottom.Location = new System.Drawing.Point(0, 24);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1088, 673);
            this.pnlBottom.TabIndex = 0;
            this.pnlBottom.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlBottom_Paint);
            // 
            // txtONR
            // 
            this.txtONR.Location = new System.Drawing.Point(665, 34);
            this.txtONR.Name = "txtONR";
            this.txtONR.Size = new System.Drawing.Size(115, 20);
            this.txtONR.TabIndex = 1;
            this.txtONR.TabStop = false;
            this.txtONR.Enter += new System.EventHandler(this.txtONR_Enter);
            this.txtONR.Leave += new System.EventHandler(this.txtONR_Leave);
            // 
            // grbOr
            // 
            this.grbOr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbOr.Controls.Add(this.tabctrlRow);
            this.grbOr.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.grbOr.Location = new System.Drawing.Point(105, 391);
            this.grbOr.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.grbOr.Name = "grbOr";
            this.grbOr.Size = new System.Drawing.Size(971, 254);
            this.grbOr.TabIndex = 41;
            this.grbOr.TabStop = false;
            this.grbOr.Text = "Hjälpmedel";
            // 
            // tabctrlRow
            // 
            this.tabctrlRow.Controls.Add(this.tabPage1);
            this.tabctrlRow.Controls.Add(this.tabNew);
            this.tabctrlRow.Controls.Add(this.tabPage2);
            this.tabctrlRow.Controls.Add(this.tabpProduction);
            this.tabctrlRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabctrlRow.Location = new System.Drawing.Point(3, 16);
            this.tabctrlRow.Name = "tabctrlRow";
            this.tabctrlRow.SelectedIndex = 0;
            this.tabctrlRow.Size = new System.Drawing.Size(965, 235);
            this.tabctrlRow.TabIndex = 0;
            this.tabctrlRow.SelectedIndexChanged += new System.EventHandler(this.tabctrlRow_SelectedIndexChanged);
            this.tabctrlRow.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabctrlRow_KeyDown);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lwOr);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(957, 209);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Översikt rader";
            // 
            // lwOr
            // 
            this.lwOr.AllowColumnReorder = true;
            this.lwOr.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colAidNr,
            this.colArt,
            this.colBen,
            this.colAnt,
            this.colApris,
            this.colEgenAvgift,
            this.colHandl,
            this.colProdstatus,
            this.colLevtid,
            this.colFakNr,
            this.colFakDat,
            this.colHlpTextExists,
            this.colUrgent,
            this.colProdTitle,
            this.colAidText});
            this.lwOr.ContextMenu = this.mnuOrderRow;
            this.lwOr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lwOr.FullRowSelect = true;
            this.lwOr.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lwOr.HideSelection = false;
            this.lwOr.Location = new System.Drawing.Point(0, 0);
            this.lwOr.MultiSelect = false;
            this.lwOr.Name = "lwOr";
            this.lwOr.Size = new System.Drawing.Size(957, 209);
            this.lwOr.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.lwOr.TabIndex = 1;
            this.lwOr.UseCompatibleStateImageBehavior = false;
            this.lwOr.View = System.Windows.Forms.View.Details;
            this.lwOr.SelectedIndexChanged += new System.EventHandler(this.lwOr_SelectedIndexChanged);
            this.lwOr.DoubleClick += new System.EventHandler(this.lwOr_DoubleClick);
            // 
            // colAidNr
            // 
            this.colAidNr.DisplayIndex = global::Ortoped.Properties.Settings.Default.colAidNrDI;
            this.colAidNr.Text = "ID";
            this.colAidNr.Width = global::Ortoped.Properties.Settings.Default.colAidNrWidth;
            // 
            // colArt
            // 
            this.colArt.DisplayIndex = global::Ortoped.Properties.Settings.Default.colArtDI;
            this.colArt.Text = "Artikel";
            this.colArt.Width = global::Ortoped.Properties.Settings.Default.colArtWidth;
            // 
            // colBen
            // 
            this.colBen.DisplayIndex = global::Ortoped.Properties.Settings.Default.colBenDI;
            this.colBen.Text = "Benämning";
            this.colBen.Width = global::Ortoped.Properties.Settings.Default.colBenWidth;
            // 
            // colAnt
            // 
            this.colAnt.DisplayIndex = global::Ortoped.Properties.Settings.Default.colAntDI;
            this.colAnt.Text = "Antal";
            this.colAnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colAnt.Width = global::Ortoped.Properties.Settings.Default.colAntWidth;
            // 
            // colApris
            // 
            this.colApris.DisplayIndex = global::Ortoped.Properties.Settings.Default.colAprisDI;
            this.colApris.Text = "Tot.pris";
            this.colApris.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colApris.Width = global::Ortoped.Properties.Settings.Default.colAprisWidth;
            // 
            // colEgenAvgift
            // 
            this.colEgenAvgift.DisplayIndex = global::Ortoped.Properties.Settings.Default.colEgenAvgiftDI;
            this.colEgenAvgift.Text = "Egenavgift";
            this.colEgenAvgift.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colEgenAvgift.Width = global::Ortoped.Properties.Settings.Default.colEgenAvgiftWidth;
            // 
            // colHandl
            // 
            this.colHandl.DisplayIndex = global::Ortoped.Properties.Settings.Default.colhandlDI;
            this.colHandl.Text = "Handläggare";
            this.colHandl.Width = global::Ortoped.Properties.Settings.Default.colhandlWidth;
            // 
            // colProdstatus
            // 
            this.colProdstatus.DisplayIndex = global::Ortoped.Properties.Settings.Default.colProdStatusDI;
            this.colProdstatus.Text = "Status";
            this.colProdstatus.Width = global::Ortoped.Properties.Settings.Default.colProdStatusWidth;
            // 
            // colLevtid
            // 
            this.colLevtid.DisplayIndex = global::Ortoped.Properties.Settings.Default.colLevtidDI;
            this.colLevtid.Text = "Lev.tid";
            this.colLevtid.Width = global::Ortoped.Properties.Settings.Default.colLevtidWidth;
            // 
            // colFakNr
            // 
            this.colFakNr.DisplayIndex = global::Ortoped.Properties.Settings.Default.colFakNrDI;
            this.colFakNr.Text = "FakturaNr";
            this.colFakNr.Width = global::Ortoped.Properties.Settings.Default.colFakNrWidth;
            // 
            // colFakDat
            // 
            this.colFakDat.DisplayIndex = global::Ortoped.Properties.Settings.Default.colFakDatDI;
            this.colFakDat.Text = "Fakturadat.";
            this.colFakDat.Width = global::Ortoped.Properties.Settings.Default.colFakDatWidth;
            // 
            // colHlpTextExists
            // 
            this.colHlpTextExists.Text = "Text";
            this.colHlpTextExists.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colHlpTextExists.Width = 35;
            // 
            // colUrgent
            // 
            this.colUrgent.DisplayIndex = global::Ortoped.Properties.Settings.Default.colUrgentDI;
            this.colUrgent.Text = "Akut";
            this.colUrgent.Width = global::Ortoped.Properties.Settings.Default.colUrgentWidth;
            // 
            // colProdTitle
            // 
            this.colProdTitle.DisplayIndex = global::Ortoped.Properties.Settings.Default.colProdTitleDI;
            this.colProdTitle.Text = "Prod. titel";
            this.colProdTitle.Width = global::Ortoped.Properties.Settings.Default.colProdTitleWidth;
            // 
            // colAidText
            // 
            this.colAidText.Text = "Text";
            this.colAidText.Width = 20;
            // 
            // mnuOrderRow
            // 
            this.mnuOrderRow.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuNewAid,
            this.menuItem12,
            this.mnuOwnFee,
            this.menuItem2,
            this.mnuDeliver,
            this.mnuReceipt,
            this.menuItem9,
            this.menuItem5,
            this.menuItem3,
            this.menuItem8,
            this.menuItem6,
            this.mnuPrintDocument,
            this.menuItem7});
            this.mnuOrderRow.Popup += new System.EventHandler(this.mnuOrderRow_Popup);
            // 
            // mnuNewAid
            // 
            this.mnuNewAid.Index = 0;
            this.mnuNewAid.Shortcut = System.Windows.Forms.Shortcut.CtrlI;
            this.mnuNewAid.Text = "Nytt hjälpmedel";
            this.mnuNewAid.Click += new System.EventHandler(this.mnuNewAid_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 1;
            this.menuItem12.Text = "-";
            // 
            // mnuOwnFee
            // 
            this.mnuOwnFee.Index = 2;
            this.mnuOwnFee.Text = "Egenavgift";
            this.mnuOwnFee.Click += new System.EventHandler(this.mnuOwnFee_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 3;
            this.menuItem2.Text = "-";
            // 
            // mnuDeliver
            // 
            this.mnuDeliver.Index = 4;
            this.mnuDeliver.Text = "Leverera hjälpmedel";
            this.mnuDeliver.Click += new System.EventHandler(this.mnuDeliver_Click);
            // 
            // mnuReceipt
            // 
            this.mnuReceipt.Index = 5;
            this.mnuReceipt.Shortcut = System.Windows.Forms.Shortcut.CtrlD;
            this.mnuReceipt.Text = "Skriv ut kvitto (genomför leverans)";
            this.mnuReceipt.Click += new System.EventHandler(this.mnuReceipt_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 6;
            this.menuItem9.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem10,
            this.menuItem11,
            this.menuItem13});
            this.menuItem9.Text = "Kreditera hjälpmedel";
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 0;
            this.menuItem10.Text = "1. Endast egenavgift till patienten";
            this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 1;
            this.menuItem11.Text = "2. Hela hjälpmedlet till fakturakund";
            this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 2;
            this.menuItem13.Text = "3. Hela hjälpmedlet till fakturakund samt egenavgift till patienten";
            this.menuItem13.Click += new System.EventHandler(this.menuItem13_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 7;
            this.menuItem5.Text = "-";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 8;
            this.menuItem3.Text = "Gå till kalender med patient...";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 9;
            this.menuItem8.Text = "Gå till kalender med hjälp-id...";
            this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 10;
            this.menuItem6.Text = "-";
            // 
            // mnuPrintDocument
            // 
            this.mnuPrintDocument.Index = 11;
            this.mnuPrintDocument.Text = "Skriv ut Dokument";
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 12;
            this.menuItem7.Text = "Skriv ut Arbetsorder";
            this.menuItem7.Visible = false;
            // 
            // tabNew
            // 
            this.tabNew.Controls.Add(this.scProductsAndSum);
            this.tabNew.Controls.Add(this.cboProdStatus);
            this.tabNew.Controls.Add(this.grbAid);
            this.tabNew.Controls.Add(this.grbArt);
            this.tabNew.Location = new System.Drawing.Point(4, 22);
            this.tabNew.Name = "tabNew";
            this.tabNew.Size = new System.Drawing.Size(957, 209);
            this.tabNew.TabIndex = 3;
            this.tabNew.Text = "Detaljer";
            // 
            // scProductsAndSum
            // 
            this.scProductsAndSum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scProductsAndSum.Location = new System.Drawing.Point(602, 8);
            this.scProductsAndSum.Name = "scProductsAndSum";
            // 
            // scProductsAndSum.Panel1
            // 
            this.scProductsAndSum.Panel1.Controls.Add(this.grbArtList);
            // 
            // scProductsAndSum.Panel2
            // 
            this.scProductsAndSum.Panel2.Controls.Add(this.grpSum);
            this.scProductsAndSum.Size = new System.Drawing.Size(595, 187);
            this.scProductsAndSum.SplitterDistance = 275;
            this.scProductsAndSum.TabIndex = 2;
            // 
            // grbArtList
            // 
            this.grbArtList.Controls.Add(this.lwAidRows);
            this.grbArtList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbArtList.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.grbArtList.Location = new System.Drawing.Point(0, 0);
            this.grbArtList.Name = "grbArtList";
            this.grbArtList.Size = new System.Drawing.Size(275, 187);
            this.grbArtList.TabIndex = 4;
            this.grbArtList.TabStop = false;
            this.grbArtList.Text = "Ingående artiklar";
            // 
            // lwAidRows
            // 
            this.lwAidRows.AllowColumnReorder = true;
            this.lwAidRows.CheckBoxes = true;
            this.lwAidRows.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colAidRowsArtNo,
            this.colAidRowsBen,
            this.colAidRowsPcs,
            this.colAidRowsRdc});
            this.lwAidRows.ContextMenuStrip = this.mnuAidRows;
            this.lwAidRows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lwAidRows.FullRowSelect = true;
            this.lwAidRows.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lwAidRows.HideSelection = false;
            this.lwAidRows.Location = new System.Drawing.Point(3, 16);
            this.lwAidRows.MultiSelect = false;
            this.lwAidRows.Name = "lwAidRows";
            this.lwAidRows.Size = new System.Drawing.Size(269, 168);
            this.lwAidRows.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lwAidRows.TabIndex = 0;
            this.lwAidRows.TabStop = false;
            this.lwAidRows.UseCompatibleStateImageBehavior = false;
            this.lwAidRows.View = System.Windows.Forms.View.Details;
            this.lwAidRows.SelectedIndexChanged += new System.EventHandler(this.lwAidRows_SelectedIndexChanged);
            // 
            // colAidRowsArtNo
            // 
            this.colAidRowsArtNo.DisplayIndex = global::Ortoped.Properties.Settings.Default.colAidRowsArtNo_DI;
            this.colAidRowsArtNo.Text = "ArtikelNr";
            this.colAidRowsArtNo.Width = global::Ortoped.Properties.Settings.Default.colAidRowsArtNr_Width;
            // 
            // colAidRowsBen
            // 
            this.colAidRowsBen.DisplayIndex = global::Ortoped.Properties.Settings.Default.colAidRowsBen_DI;
            this.colAidRowsBen.Text = "Benämning";
            this.colAidRowsBen.Width = global::Ortoped.Properties.Settings.Default.colAidRowsBen_Width;
            // 
            // colAidRowsPcs
            // 
            this.colAidRowsPcs.DisplayIndex = global::Ortoped.Properties.Settings.Default.colAidRowsPcs_DI;
            this.colAidRowsPcs.Text = "Antal";
            this.colAidRowsPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colAidRowsPcs.Width = global::Ortoped.Properties.Settings.Default.colAidRowsPcs_Width;
            // 
            // colAidRowsRdc
            // 
            this.colAidRowsRdc.Width = 0;
            // 
            // mnuAidRows
            // 
            this.mnuAidRows.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSetViewStat,
            this.visaMaterialplaneringenToolStripMenuItem});
            this.mnuAidRows.Name = "mnuAidRows";
            this.mnuAidRows.Size = new System.Drawing.Size(223, 48);
            // 
            // mnuSetViewStat
            // 
            this.mnuSetViewStat.Name = "mnuSetViewStat";
            this.mnuSetViewStat.Size = new System.Drawing.Size(222, 22);
            this.mnuSetViewStat.Text = "Visa denna i \"översikt rader\"";
            this.mnuSetViewStat.Click += new System.EventHandler(this.mnuSetViewStat_Click);
            // 
            // visaMaterialplaneringenToolStripMenuItem
            // 
            this.visaMaterialplaneringenToolStripMenuItem.Name = "visaMaterialplaneringenToolStripMenuItem";
            this.visaMaterialplaneringenToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.visaMaterialplaneringenToolStripMenuItem.Text = "Gå till materialplaneringen...";
            this.visaMaterialplaneringenToolStripMenuItem.Click += new System.EventHandler(this.visaMaterialplaneringenToolStripMenuItem_Click);
            // 
            // grpSum
            // 
            this.grpSum.AutoSize = true;
            this.grpSum.Controls.Add(this.labSumInternal);
            this.grpSum.Controls.Add(this.labSumExternal);
            this.grpSum.Controls.Add(this.label35);
            this.grpSum.Controls.Add(this.label17);
            this.grpSum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSum.Location = new System.Drawing.Point(0, 0);
            this.grpSum.Name = "grpSum";
            this.grpSum.Size = new System.Drawing.Size(316, 187);
            this.grpSum.TabIndex = 5;
            this.grpSum.TabStop = false;
            this.grpSum.Text = "Summa";
            // 
            // labSumInternal
            // 
            this.labSumInternal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labSumInternal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labSumInternal.ForeColor = System.Drawing.Color.IndianRed;
            this.labSumInternal.Location = new System.Drawing.Point(206, 120);
            this.labSumInternal.Name = "labSumInternal";
            this.labSumInternal.Size = new System.Drawing.Size(88, 17);
            this.labSumInternal.TabIndex = 3;
            this.labSumInternal.Text = "0:-";
            this.labSumInternal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labSumExternal
            // 
            this.labSumExternal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labSumExternal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labSumExternal.ForeColor = System.Drawing.Color.ForestGreen;
            this.labSumExternal.Location = new System.Drawing.Point(204, 53);
            this.labSumExternal.Name = "labSumExternal";
            this.labSumExternal.Size = new System.Drawing.Size(92, 17);
            this.labSumExternal.TabIndex = 2;
            this.labSumExternal.Text = "0:-";
            this.labSumExternal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label35
            // 
            this.label35.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(236, 93);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(57, 13);
            this.label35.TabIndex = 1;
            this.label35.Text = "Kostnad:";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(222, 28);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(74, 13);
            this.label17.TabIndex = 0;
            this.label17.Text = "Ordervärde:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboProdStatus
            // 
            this.cboProdStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboProdStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboProdStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProdStatus.Items.AddRange(new object[] {
            ""});
            this.cboProdStatus.Location = new System.Drawing.Point(17, 121);
            this.cboProdStatus.Name = "cboProdStatus";
            this.cboProdStatus.Size = new System.Drawing.Size(185, 21);
            this.cboProdStatus.Sorted = true;
            this.cboProdStatus.TabIndex = 1;
            this.cboProdStatus.SelectedValueChanged += new System.EventHandler(this.cboProdStatus_SelectedValueChanged);
            // 
            // grbAid
            // 
            this.grbAid.Controls.Add(this.labRemissNr);
            this.grbAid.Controls.Add(this.txtOR_ONR);
            this.grbAid.Controls.Add(this.txtHolder);
            this.grbAid.Controls.Add(this.label13);
            this.grbAid.Controls.Add(this.chkFirstTimePatient);
            this.grbAid.Controls.Add(this.txtPartOid);
            this.grbAid.Controls.Add(this.txtAidOid);
            this.grbAid.Controls.Add(this.txtHandler);
            this.grbAid.Controls.Add(this.txtLevDate);
            this.grbAid.Controls.Add(this.cboAidPriority);
            this.grbAid.Controls.Add(this.txtOrDatum);
            this.grbAid.Controls.Add(this.chkGaranti);
            this.grbAid.Controls.Add(this.label28);
            this.grbAid.Controls.Add(this.txtAidId);
            this.grbAid.Controls.Add(this.dtpLevtid);
            this.grbAid.Controls.Add(this.label27);
            this.grbAid.Controls.Add(this.label26);
            this.grbAid.Controls.Add(this.cboHandler);
            this.grbAid.Controls.Add(this.label3);
            this.grbAid.Controls.Add(this.label18);
            this.grbAid.Controls.Add(this.label25);
            this.grbAid.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.grbAid.Location = new System.Drawing.Point(8, 8);
            this.grbAid.Name = "grbAid";
            this.grbAid.Size = new System.Drawing.Size(290, 188);
            this.grbAid.TabIndex = 0;
            this.grbAid.TabStop = false;
            this.grbAid.Text = "Gemensam info Hjälpmedel";
            // 
            // labRemissNr
            // 
            this.labRemissNr.AutoSize = true;
            this.labRemissNr.Location = new System.Drawing.Point(79, 92);
            this.labRemissNr.Name = "labRemissNr";
            this.labRemissNr.Size = new System.Drawing.Size(41, 13);
            this.labRemissNr.TabIndex = 22;
            this.labRemissNr.Text = "label36";
            this.labRemissNr.Visible = false;
            // 
            // txtOR_ONR
            // 
            this.txtOR_ONR.Location = new System.Drawing.Point(202, 139);
            this.txtOR_ONR.Name = "txtOR_ONR";
            this.txtOR_ONR.Size = new System.Drawing.Size(81, 20);
            this.txtOR_ONR.TabIndex = 21;
            this.txtOR_ONR.Visible = false;
            // 
            // txtHolder
            // 
            this.txtHolder.Location = new System.Drawing.Point(203, 69);
            this.txtHolder.Name = "txtHolder";
            this.txtHolder.ReadOnly = true;
            this.txtHolder.Size = new System.Drawing.Size(80, 20);
            this.txtHolder.TabIndex = 20;
            this.txtHolder.TabStop = false;
            // 
            // label13
            // 
            this.label13.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label13.Location = new System.Drawing.Point(200, 54);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 16);
            this.label13.TabIndex = 19;
            this.label13.Text = "Nuv. handl.";
            // 
            // chkFirstTimePatient
            // 
            this.chkFirstTimePatient.AutoSize = true;
            this.chkFirstTimePatient.Location = new System.Drawing.Point(8, 143);
            this.chkFirstTimePatient.Name = "chkFirstTimePatient";
            this.chkFirstTimePatient.Size = new System.Drawing.Size(116, 17);
            this.chkFirstTimePatient.TabIndex = 18;
            this.chkFirstTimePatient.TabStop = false;
            this.chkFirstTimePatient.Text = "Förstagångspatient";
            this.chkFirstTimePatient.UseVisualStyleBackColor = true;
            this.chkFirstTimePatient.Visible = false;
            this.chkFirstTimePatient.CheckedChanged += new System.EventHandler(this.chkFirstTimePatient_CheckedChanged);
            // 
            // txtPartOid
            // 
            this.txtPartOid.Location = new System.Drawing.Point(124, 43);
            this.txtPartOid.Name = "txtPartOid";
            this.txtPartOid.Size = new System.Drawing.Size(67, 20);
            this.txtPartOid.TabIndex = 17;
            this.txtPartOid.Visible = false;
            // 
            // txtAidOid
            // 
            this.txtAidOid.Location = new System.Drawing.Point(202, 20);
            this.txtAidOid.Name = "txtAidOid";
            this.txtAidOid.Size = new System.Drawing.Size(72, 20);
            this.txtAidOid.TabIndex = 16;
            this.txtAidOid.Visible = false;
            // 
            // txtHandler
            // 
            this.txtHandler.Location = new System.Drawing.Point(8, 68);
            this.txtHandler.Name = "txtHandler";
            this.txtHandler.ReadOnly = true;
            this.txtHandler.Size = new System.Drawing.Size(188, 20);
            this.txtHandler.TabIndex = 15;
            this.txtHandler.TextChanged += new System.EventHandler(this.valueChangedOR);
            // 
            // txtLevDate
            // 
            this.txtLevDate.Location = new System.Drawing.Point(203, 114);
            this.txtLevDate.Name = "txtLevDate";
            this.txtLevDate.Size = new System.Drawing.Size(80, 20);
            this.txtLevDate.TabIndex = 14;
            this.txtLevDate.TabStop = false;
            this.txtLevDate.Leave += new System.EventHandler(this.txtLevDate_Leave);
            // 
            // cboAidPriority
            // 
            this.cboAidPriority.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboAidPriority.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboAidPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAidPriority.Location = new System.Drawing.Point(128, 161);
            this.cboAidPriority.Name = "cboAidPriority";
            this.cboAidPriority.Size = new System.Drawing.Size(155, 21);
            this.cboAidPriority.TabIndex = 11;
            this.cboAidPriority.TabStop = false;
            this.cboAidPriority.SelectedValueChanged += new System.EventHandler(this.cboLevsatt_SelectedValueChanged);
            // 
            // txtOrDatum
            // 
            this.txtOrDatum.Location = new System.Drawing.Point(124, 20);
            this.txtOrDatum.Name = "txtOrDatum";
            this.txtOrDatum.ReadOnly = true;
            this.txtOrDatum.Size = new System.Drawing.Size(72, 20);
            this.txtOrDatum.TabIndex = 9;
            this.txtOrDatum.TabStop = false;
            this.txtOrDatum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkGaranti
            // 
            this.chkGaranti.Location = new System.Drawing.Point(8, 166);
            this.chkGaranti.Name = "chkGaranti";
            this.chkGaranti.Size = new System.Drawing.Size(62, 16);
            this.chkGaranti.TabIndex = 8;
            this.chkGaranti.TabStop = false;
            this.chkGaranti.Text = "Garanti";
            this.chkGaranti.CheckedChanged += new System.EventHandler(this.chkGaranti_CheckedChanged);
            // 
            // label28
            // 
            this.label28.Location = new System.Drawing.Point(8, 20);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(24, 16);
            this.label28.TabIndex = 7;
            this.label28.Text = "ID:";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAidId
            // 
            this.txtAidId.Location = new System.Drawing.Point(36, 20);
            this.txtAidId.Name = "txtAidId";
            this.txtAidId.ReadOnly = true;
            this.txtAidId.Size = new System.Drawing.Size(36, 20);
            this.txtAidId.TabIndex = 6;
            this.txtAidId.TabStop = false;
            this.txtAidId.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dtpLevtid
            // 
            this.dtpLevtid.CustomFormat = "yyMMdd";
            this.dtpLevtid.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpLevtid.Location = new System.Drawing.Point(203, 113);
            this.dtpLevtid.Name = "dtpLevtid";
            this.dtpLevtid.Size = new System.Drawing.Size(80, 20);
            this.dtpLevtid.TabIndex = 2;
            this.dtpLevtid.TabStop = false;
            // 
            // label27
            // 
            this.label27.Location = new System.Drawing.Point(200, 97);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(72, 16);
            this.label27.TabIndex = 4;
            this.label27.Text = "Lev.datum";
            // 
            // label26
            // 
            this.label26.Location = new System.Drawing.Point(6, 98);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(104, 16);
            this.label26.TabIndex = 3;
            this.label26.Text = "Produktionsstatus";
            // 
            // cboHandler
            // 
            this.cboHandler.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboHandler.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboHandler.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHandler.Items.AddRange(new object[] {
            ""});
            this.cboHandler.Location = new System.Drawing.Point(8, 68);
            this.cboHandler.Name = "cboHandler";
            this.cboHandler.Size = new System.Drawing.Size(188, 21);
            this.cboHandler.Sorted = true;
            this.cboHandler.TabIndex = 0;
            this.cboHandler.SelectedValueChanged += new System.EventHandler(this.cboHandler_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(76, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Datum:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(126, 144);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(48, 16);
            this.label18.TabIndex = 12;
            this.label18.Text = "Prioritet:";
            // 
            // label25
            // 
            this.label25.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label25.Location = new System.Drawing.Point(8, 54);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(113, 16);
            this.label25.TabIndex = 1;
            this.label25.Text = "Patienthandläggare";
            // 
            // grbArt
            // 
            this.grbArt.Controls.Add(this.chkUpdatedInThord);
            this.grbArt.Controls.Add(this.label24);
            this.grbArt.Controls.Add(this.cboNeedStep);
            this.grbArt.Controls.Add(this.groupBox1);
            this.grbArt.Controls.Add(this.chkViewState);
            this.grbArt.Controls.Add(this.txtRDC);
            this.grbArt.Controls.Add(this.btnDelete);
            this.grbArt.Controls.Add(this.btnAdd);
            this.grbArt.Controls.Add(this.txtORA);
            this.grbArt.Controls.Add(this.labORA);
            this.grbArt.Controls.Add(this.txtPRI);
            this.grbArt.Controls.Add(this.txtBEN);
            this.grbArt.Controls.Add(this.label21);
            this.grbArt.Controls.Add(this.txtANR);
            this.grbArt.Controls.Add(this.label22);
            this.grbArt.Controls.Add(this.label1);
            this.grbArt.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.grbArt.Location = new System.Drawing.Point(304, 8);
            this.grbArt.Name = "grbArt";
            this.grbArt.Size = new System.Drawing.Size(292, 188);
            this.grbArt.TabIndex = 1;
            this.grbArt.TabStop = false;
            this.grbArt.Text = "Artikel";
            // 
            // chkUpdatedInThord
            // 
            this.chkUpdatedInThord.AutoSize = true;
            this.chkUpdatedInThord.Location = new System.Drawing.Point(77, 92);
            this.chkUpdatedInThord.Name = "chkUpdatedInThord";
            this.chkUpdatedInThord.Size = new System.Drawing.Size(118, 17);
            this.chkUpdatedInThord.TabIndex = 16;
            this.chkUpdatedInThord.Text = "Uppdaterad i Thord";
            this.chkUpdatedInThord.UseVisualStyleBackColor = true;
            this.chkUpdatedInThord.Visible = false;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(13, 23);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(76, 13);
            this.label24.TabIndex = 15;
            this.label24.Text = "Behovstrappa:";
            this.label24.Visible = false;
            // 
            // cboNeedStep
            // 
            this.cboNeedStep.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboNeedStep.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboNeedStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNeedStep.DropDownWidth = 400;
            this.cboNeedStep.FormattingEnabled = true;
            this.cboNeedStep.Items.AddRange(new object[] {
            " ",
            "1 - Personlig ADL i hemmet (ADL)",
            "2 - Instrumentell ADL (ADL)",
            "3 - ADL får att delta i samhällslivet (ADL)",
            "4 - ADL för att utföra fritt valda aktiviteter (ADL)",
            "5 - Ändra och bibehålla kroppställning (Förflyttning)",
            "6 - Överflyttning (Förflyttning)",
            "7 - Förflytta sig i bostaden (Förflyttning)",
            "8 - Förflytta sig utanför bostaden för att utföra dagliga livets aktiviteter (För" +
                "flyttning)",
            "9 - Förflytta sig för att ta promenader (Förflyttning)",
            "10 - Förflytta sig för att deltaga i samhällslivet (Förflyttning)",
            "11 - Förflytta sig för att utföra fritt vald aktivitet (Förflyttning)",
            "18 - Livsuppehålladne aktivitet (Vård och behandling )",
            "19 - Vård och behandling av fysiska funktioner (Vård och behandling )",
            "20 - Upprätthålla och förbättra fysiska funktioner (Vård och behandling )",
            "21 - Vård och behandling för att utöva fritt valda aktiviteter (Vård och behandli" +
                "ng )"});
            this.cboNeedStep.Location = new System.Drawing.Point(95, 20);
            this.cboNeedStep.Name = "cboNeedStep";
            this.cboNeedStep.Size = new System.Drawing.Size(186, 21);
            this.cboNeedStep.TabIndex = 14;
            this.cboNeedStep.Visible = false;
            this.cboNeedStep.SelectedValueChanged += new System.EventHandler(this.cboNeedStep_SelectedValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdBestalld);
            this.groupBox1.Controls.Add(this.rdBestallEj);
            this.groupBox1.Controls.Add(this.rdBestall);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(16, 142);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 40);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Beställning";
            // 
            // rdBestalld
            // 
            this.rdBestalld.Location = new System.Drawing.Point(196, 14);
            this.rdBestalld.Name = "rdBestalld";
            this.rdBestalld.Size = new System.Drawing.Size(64, 18);
            this.rdBestalld.TabIndex = 2;
            this.rdBestalld.Text = "Beställd";
            // 
            // rdBestallEj
            // 
            this.rdBestallEj.Checked = true;
            this.rdBestallEj.Location = new System.Drawing.Point(8, 17);
            this.rdBestallEj.Name = "rdBestallEj";
            this.rdBestallEj.Size = new System.Drawing.Size(72, 16);
            this.rdBestallEj.TabIndex = 1;
            this.rdBestallEj.TabStop = true;
            this.rdBestallEj.Text = "Beställ ej";
            // 
            // rdBestall
            // 
            this.rdBestall.Location = new System.Drawing.Point(101, 16);
            this.rdBestall.Name = "rdBestall";
            this.rdBestall.Size = new System.Drawing.Size(64, 16);
            this.rdBestall.TabIndex = 0;
            this.rdBestall.Text = "Beställ";
            // 
            // chkViewState
            // 
            this.chkViewState.Location = new System.Drawing.Point(95, 42);
            this.chkViewState.Name = "chkViewState";
            this.chkViewState.Size = new System.Drawing.Size(128, 16);
            this.chkViewState.TabIndex = 12;
            this.chkViewState.TabStop = false;
            this.chkViewState.Text = "Visa artikel i översikt";
            this.chkViewState.Visible = false;
            // 
            // txtRDC
            // 
            this.txtRDC.Location = new System.Drawing.Point(228, 94);
            this.txtRDC.Name = "txtRDC";
            this.txtRDC.ReadOnly = true;
            this.txtRDC.Size = new System.Drawing.Size(48, 20);
            this.txtRDC.TabIndex = 10;
            this.txtRDC.TabStop = false;
            this.txtRDC.Visible = false;
            // 
            // btnDelete
            // 
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDelete.Location = new System.Drawing.Point(224, 114);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(57, 23);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.TabStop = false;
            this.btnDelete.Text = "Radera";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAdd.Location = new System.Drawing.Point(149, 114);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(64, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Ny artikel";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtORA
            // 
            this.txtORA.Location = new System.Drawing.Point(152, 68);
            this.txtORA.Name = "txtORA";
            this.txtORA.Size = new System.Drawing.Size(64, 20);
            this.txtORA.TabIndex = 1;
            this.txtORA.TextChanged += new System.EventHandler(this.valueChangedOR);
            this.txtORA.Enter += new System.EventHandler(this.txtORA_Enter);
            this.txtORA.Leave += new System.EventHandler(this.txtORA_Leave);
            // 
            // labORA
            // 
            this.labORA.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labORA.Location = new System.Drawing.Point(153, 54);
            this.labORA.Name = "labORA";
            this.labORA.Size = new System.Drawing.Size(63, 16);
            this.labORA.TabIndex = 6;
            this.labORA.Text = "Antal";
            // 
            // txtPRI
            // 
            this.txtPRI.Location = new System.Drawing.Point(224, 68);
            this.txtPRI.Name = "txtPRI";
            this.txtPRI.Size = new System.Drawing.Size(56, 20);
            this.txtPRI.TabIndex = 2;
            this.txtPRI.TextChanged += new System.EventHandler(this.valueChangedOR);
            this.txtPRI.Enter += new System.EventHandler(this.txtPRI_Enter);
            this.txtPRI.Leave += new System.EventHandler(this.txtPRI_Leave);
            // 
            // txtBEN
            // 
            this.txtBEN.Location = new System.Drawing.Point(16, 114);
            this.txtBEN.Name = "txtBEN";
            this.txtBEN.ReadOnly = true;
            this.txtBEN.Size = new System.Drawing.Size(128, 20);
            this.txtBEN.TabIndex = 3;
            this.txtBEN.TabStop = false;
            // 
            // label21
            // 
            this.label21.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label21.Location = new System.Drawing.Point(16, 98);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(88, 16);
            this.label21.TabIndex = 2;
            this.label21.Text = "Benämning";
            // 
            // txtANR
            // 
            this.txtANR.Enabled = false;
            this.txtANR.Location = new System.Drawing.Point(16, 68);
            this.txtANR.Name = "txtANR";
            this.txtANR.Size = new System.Drawing.Size(128, 20);
            this.txtANR.TabIndex = 0;
            this.txtANR.TextChanged += new System.EventHandler(this.valueChangedOR);
            this.txtANR.Enter += new System.EventHandler(this.txtANR_Enter);
            this.txtANR.Leave += new System.EventHandler(this.txtANR_Leave);
            // 
            // label22
            // 
            this.label22.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label22.Location = new System.Drawing.Point(224, 54);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(40, 14);
            this.label22.TabIndex = 5;
            this.label22.Text = "Pris";
            // 
            // label1
            // 
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Location = new System.Drawing.Point(16, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Artikel";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtLabAidTexter);
            this.tabPage2.Controls.Add(this.txtLabArtTexter);
            this.tabPage2.Controls.Add(this.label34);
            this.tabPage2.Controls.Add(this.txtAidText);
            this.tabPage2.Controls.Add(this.txtOrText);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(957, 209);
            this.tabPage2.TabIndex = 4;
            this.tabPage2.Text = "Text";
            // 
            // txtLabAidTexter
            // 
            this.txtLabAidTexter.Location = new System.Drawing.Point(3, 20);
            this.txtLabAidTexter.Name = "txtLabAidTexter";
            this.txtLabAidTexter.ReadOnly = true;
            this.txtLabAidTexter.Size = new System.Drawing.Size(439, 20);
            this.txtLabAidTexter.TabIndex = 5;
            // 
            // txtLabArtTexter
            // 
            this.txtLabArtTexter.Location = new System.Drawing.Point(3, 20);
            this.txtLabArtTexter.Name = "txtLabArtTexter";
            this.txtLabArtTexter.ReadOnly = true;
            this.txtLabArtTexter.Size = new System.Drawing.Size(416, 20);
            this.txtLabArtTexter.TabIndex = 4;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(3, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(39, 17);
            this.label34.TabIndex = 2;
            this.label34.Text = "Text";
            // 
            // txtAidText
            // 
            this.txtAidText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAidText.BackColor = System.Drawing.SystemColors.Info;
            this.txtAidText.Location = new System.Drawing.Point(3, 53);
            this.txtAidText.Name = "txtAidText";
            this.txtAidText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtAidText.Size = new System.Drawing.Size(1045, 142);
            this.txtAidText.TabIndex = 1;
            this.txtAidText.Text = "";
            this.txtAidText.Enter += new System.EventHandler(this.txtAidText_Enter);
            this.txtAidText.Leave += new System.EventHandler(this.txtAidText_Leave);
            // 
            // txtOrText
            // 
            this.txtOrText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOrText.BackColor = System.Drawing.SystemColors.Info;
            this.txtOrText.Location = new System.Drawing.Point(3, 40);
            this.txtOrText.Name = "txtOrText";
            this.txtOrText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtOrText.Size = new System.Drawing.Size(1045, 142);
            this.txtOrText.TabIndex = 0;
            this.txtOrText.Text = "";
            this.txtOrText.Enter += new System.EventHandler(this.txtOrText_Enter);
            this.txtOrText.Leave += new System.EventHandler(this.txtOrText_Leave);
            // 
            // tabpProduction
            // 
            this.tabpProduction.BackColor = System.Drawing.SystemColors.Control;
            this.tabpProduction.Controls.Add(this.dtpConditionDate);
            this.tabpProduction.Controls.Add(this.label36);
            this.tabpProduction.Controls.Add(this.chkUrgent);
            this.tabpProduction.Controls.Add(this.dtpPromisedDeliverDate);
            this.tabpProduction.Controls.Add(this.txtProductionTitle);
            this.tabpProduction.Controls.Add(this.label19);
            this.tabpProduction.Controls.Add(this.label14);
            this.tabpProduction.Location = new System.Drawing.Point(4, 22);
            this.tabpProduction.Name = "tabpProduction";
            this.tabpProduction.Padding = new System.Windows.Forms.Padding(3);
            this.tabpProduction.Size = new System.Drawing.Size(957, 209);
            this.tabpProduction.TabIndex = 5;
            this.tabpProduction.Text = "Produktion";
            // 
            // dtpConditionDate
            // 
            this.dtpConditionDate.CustomFormat = "yyMMdd";
            this.dtpConditionDate.Location = new System.Drawing.Point(15, 107);
            this.dtpConditionDate.Name = "dtpConditionDate";
            this.dtpConditionDate.Size = new System.Drawing.Size(200, 20);
            this.dtpConditionDate.TabIndex = 9;
            this.dtpConditionDate.ValueChanged += new System.EventHandler(this.dtpConditionDate_ValueChanged);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(12, 92);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(86, 13);
            this.label36.TabIndex = 8;
            this.label36.Text = "Provningsdatum:";
            // 
            // chkUrgent
            // 
            this.chkUrgent.AutoSize = true;
            this.chkUrgent.Location = new System.Drawing.Point(17, 17);
            this.chkUrgent.Name = "chkUrgent";
            this.chkUrgent.Size = new System.Drawing.Size(48, 17);
            this.chkUrgent.TabIndex = 7;
            this.chkUrgent.Text = "Akut";
            this.chkUrgent.UseVisualStyleBackColor = true;
            this.chkUrgent.CheckedChanged += new System.EventHandler(this.chkUrgent_CheckedChanged);
            // 
            // dtpPromisedDeliverDate
            // 
            this.dtpPromisedDeliverDate.CustomFormat = "yyMMdd";
            this.dtpPromisedDeliverDate.Location = new System.Drawing.Point(14, 152);
            this.dtpPromisedDeliverDate.Name = "dtpPromisedDeliverDate";
            this.dtpPromisedDeliverDate.Size = new System.Drawing.Size(200, 20);
            this.dtpPromisedDeliverDate.TabIndex = 6;
            this.dtpPromisedDeliverDate.ValueChanged += new System.EventHandler(this.dtpPromisedDeliverDate_ValueChanged);
            // 
            // txtProductionTitle
            // 
            this.txtProductionTitle.Location = new System.Drawing.Point(15, 62);
            this.txtProductionTitle.Name = "txtProductionTitle";
            this.txtProductionTitle.Size = new System.Drawing.Size(444, 20);
            this.txtProductionTitle.TabIndex = 4;
            this.txtProductionTitle.TextChanged += new System.EventHandler(this.valueChangedOR);
            this.txtProductionTitle.Leave += new System.EventHandler(this.txtProductionTitle_Leave);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(12, 136);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(86, 13);
            this.label19.TabIndex = 2;
            this.label19.Text = "Lovat lev.datum:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 46);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(30, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "Titel:";
            // 
            // grbTid
            // 
            this.grbTid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grbTid.Controls.Add(this.btnGoToCalendar);
            this.grbTid.Controls.Add(this.lwErrand);
            this.grbTid.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.grbTid.Location = new System.Drawing.Point(105, 278);
            this.grbTid.Name = "grbTid";
            this.grbTid.Size = new System.Drawing.Size(466, 106);
            this.grbTid.TabIndex = 40;
            this.grbTid.TabStop = false;
            this.grbTid.Text = "Tidsbokningar";
            // 
            // btnGoToCalendar
            // 
            this.btnGoToCalendar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoToCalendar.FlatAppearance.BorderSize = 0;
            this.btnGoToCalendar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGoToCalendar.Image = global::Ortoped.Properties.Resources.date_time;
            this.btnGoToCalendar.Location = new System.Drawing.Point(427, 15);
            this.btnGoToCalendar.Name = "btnGoToCalendar";
            this.btnGoToCalendar.Size = new System.Drawing.Size(32, 32);
            this.btnGoToCalendar.TabIndex = 27;
            this.btnGoToCalendar.UseVisualStyleBackColor = true;
            this.btnGoToCalendar.Click += new System.EventHandler(this.btnGoToCalendar_Click);
            // 
            // lwErrand
            // 
            this.lwErrand.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lwErrand.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader1,
            this.columnHeader9,
            this.columnHeader6});
            this.lwErrand.FullRowSelect = true;
            this.lwErrand.Location = new System.Drawing.Point(8, 14);
            this.lwErrand.Name = "lwErrand";
            this.lwErrand.Size = new System.Drawing.Size(414, 87);
            this.lwErrand.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.lwErrand.TabIndex = 3;
            this.lwErrand.UseCompatibleStateImageBehavior = false;
            this.lwErrand.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Datum";
            this.columnHeader7.Width = 61;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Starttid";
            this.columnHeader4.Width = 45;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Pågår";
            this.columnHeader5.Width = 45;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Order - ID";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Handläggare";
            this.columnHeader9.Width = 140;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "ID";
            this.columnHeader6.Width = 0;
            // 
            // grbPatient
            // 
            this.grbPatient.BackColor = System.Drawing.SystemColors.Control;
            this.grbPatient.Controls.Add(this.txtTelSMS);
            this.grbPatient.Controls.Add(this.btnCopDoc2);
            this.grbPatient.Controls.Add(this.btnCopDoc1);
            this.grbPatient.Controls.Add(this.btnGoToAccounting);
            this.grbPatient.Controls.Add(this.btnGoToCustomer);
            this.grbPatient.Controls.Add(this.chkDeceased);
            this.grbPatient.Controls.Add(this.chkCopDok);
            this.grbPatient.Controls.Add(this.label16);
            this.grbPatient.Controls.Add(this.label15);
            this.grbPatient.Controls.Add(this.txtTelMobil);
            this.grbPatient.Controls.Add(this.txtTelArbete);
            this.grbPatient.Controls.Add(this.txtKNR);
            this.grbPatient.Controls.Add(this.labANM);
            this.grbPatient.Controls.Add(this.txtANM);
            this.grbPatient.Controls.Add(this.chkJournal);
            this.grbPatient.Controls.Add(this.txtTelBostad);
            this.grbPatient.Controls.Add(this.txtORT);
            this.grbPatient.Controls.Add(this.txtADD);
            this.grbPatient.Controls.Add(this.txtSN);
            this.grbPatient.Controls.Add(this.txtLN);
            this.grbPatient.Controls.Add(this.txtPNR);
            this.grbPatient.Controls.Add(this.labTEL);
            this.grbPatient.Controls.Add(this.labORT);
            this.grbPatient.Controls.Add(this.labADD);
            this.grbPatient.Controls.Add(this.labSN);
            this.grbPatient.Controls.Add(this.labLN);
            this.grbPatient.Controls.Add(this.labPnr);
            this.grbPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grbPatient.Location = new System.Drawing.Point(105, 12);
            this.grbPatient.Name = "grbPatient";
            this.grbPatient.Size = new System.Drawing.Size(466, 255);
            this.grbPatient.TabIndex = 1;
            this.grbPatient.TabStop = false;
            this.grbPatient.Text = "Patient";
            // 
            // txtTelSMS
            // 
            this.txtTelSMS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTelSMS.Location = new System.Drawing.Point(246, 184);
            this.txtTelSMS.Name = "txtTelSMS";
            this.txtTelSMS.Size = new System.Drawing.Size(138, 20);
            this.txtTelSMS.TabIndex = 29;
            this.txtTelSMS.TabStop = false;
            this.txtTelSMS.Leave += new System.EventHandler(this.txtTelSMS_Leave);
            // 
            // btnCopDoc2
            // 
            this.btnCopDoc2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopDoc2.Enabled = false;
            this.btnCopDoc2.Location = new System.Drawing.Point(390, 180);
            this.btnCopDoc2.Name = "btnCopDoc2";
            this.btnCopDoc2.Size = new System.Drawing.Size(70, 24);
            this.btnCopDoc2.TabIndex = 28;
            this.btnCopDoc2.Text = "CopDoc 2";
            this.btnCopDoc2.UseVisualStyleBackColor = true;
            this.btnCopDoc2.Click += new System.EventHandler(this.btnCopDoc2_Click);
            // 
            // btnCopDoc1
            // 
            this.btnCopDoc1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopDoc1.Enabled = false;
            this.btnCopDoc1.Location = new System.Drawing.Point(390, 153);
            this.btnCopDoc1.Name = "btnCopDoc1";
            this.btnCopDoc1.Size = new System.Drawing.Size(70, 24);
            this.btnCopDoc1.TabIndex = 27;
            this.btnCopDoc1.Text = "CopDoc 1";
            this.btnCopDoc1.UseVisualStyleBackColor = true;
            this.btnCopDoc1.Click += new System.EventHandler(this.btnCopDoc1_Click);
            // 
            // btnGoToAccounting
            // 
            this.btnGoToAccounting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoToAccounting.FlatAppearance.BorderSize = 0;
            this.btnGoToAccounting.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGoToAccounting.Image = global::Ortoped.Properties.Resources.money2;
            this.btnGoToAccounting.Location = new System.Drawing.Point(426, 54);
            this.btnGoToAccounting.Name = "btnGoToAccounting";
            this.btnGoToAccounting.Size = new System.Drawing.Size(32, 32);
            this.btnGoToAccounting.TabIndex = 26;
            this.btnGoToAccounting.TabStop = false;
            this.btnGoToAccounting.UseVisualStyleBackColor = true;
            this.btnGoToAccounting.Click += new System.EventHandler(this.btnGoToAccounting_Click);
            // 
            // btnGoToCustomer
            // 
            this.btnGoToCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoToCustomer.FlatAppearance.BorderSize = 0;
            this.btnGoToCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGoToCustomer.Image = global::Ortoped.Properties.Resources.user1_information1;
            this.btnGoToCustomer.Location = new System.Drawing.Point(426, 19);
            this.btnGoToCustomer.Name = "btnGoToCustomer";
            this.btnGoToCustomer.Size = new System.Drawing.Size(32, 32);
            this.btnGoToCustomer.TabIndex = 25;
            this.btnGoToCustomer.TabStop = false;
            this.btnGoToCustomer.UseVisualStyleBackColor = true;
            this.btnGoToCustomer.Click += new System.EventHandler(this.btnGoToCustomer_Click);
            // 
            // chkDeceased
            // 
            this.chkDeceased.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDeceased.AutoSize = true;
            this.chkDeceased.Location = new System.Drawing.Point(323, 19);
            this.chkDeceased.Name = "chkDeceased";
            this.chkDeceased.Size = new System.Drawing.Size(61, 17);
            this.chkDeceased.TabIndex = 24;
            this.chkDeceased.TabStop = false;
            this.chkDeceased.Text = "Avliden";
            this.chkDeceased.UseVisualStyleBackColor = true;
            this.chkDeceased.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chkDeceased_MouseUp);
            // 
            // chkCopDok
            // 
            this.chkCopDok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCopDok.Location = new System.Drawing.Point(390, 231);
            this.chkCopDok.Name = "chkCopDok";
            this.chkCopDok.Size = new System.Drawing.Size(68, 18);
            this.chkCopDok.TabIndex = 22;
            this.chkCopDok.TabStop = false;
            this.chkCopDok.Text = "CopDoc";
            this.chkCopDok.CheckedChanged += new System.EventHandler(this.chkCopDok_CheckedChanged);
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(6, 182);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(82, 23);
            this.label16.TabIndex = 21;
            this.label16.Text = "Tel Mobil/SMS:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(24, 161);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(64, 16);
            this.label15.TabIndex = 20;
            this.label15.Text = "Tel Arbete:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTelMobil
            // 
            this.txtTelMobil.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTelMobil.Location = new System.Drawing.Point(96, 184);
            this.txtTelMobil.Name = "txtTelMobil";
            this.txtTelMobil.ReadOnly = true;
            this.txtTelMobil.Size = new System.Drawing.Size(144, 20);
            this.txtTelMobil.TabIndex = 19;
            this.txtTelMobil.TabStop = false;
            // 
            // txtTelArbete
            // 
            this.txtTelArbete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTelArbete.Location = new System.Drawing.Point(96, 160);
            this.txtTelArbete.Name = "txtTelArbete";
            this.txtTelArbete.ReadOnly = true;
            this.txtTelArbete.Size = new System.Drawing.Size(288, 20);
            this.txtTelArbete.TabIndex = 18;
            this.txtTelArbete.TabStop = false;
            // 
            // txtKNR
            // 
            this.txtKNR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKNR.Location = new System.Drawing.Point(392, 123);
            this.txtKNR.Name = "txtKNR";
            this.txtKNR.Size = new System.Drawing.Size(68, 20);
            this.txtKNR.TabIndex = 17;
            this.txtKNR.TabStop = false;
            this.txtKNR.Visible = false;
            // 
            // labANM
            // 
            this.labANM.Location = new System.Drawing.Point(16, 209);
            this.labANM.Name = "labANM";
            this.labANM.Size = new System.Drawing.Size(72, 16);
            this.labANM.TabIndex = 14;
            this.labANM.Text = "Anmärkning:";
            this.labANM.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtANM
            // 
            this.txtANM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtANM.Location = new System.Drawing.Point(96, 208);
            this.txtANM.Multiline = true;
            this.txtANM.Name = "txtANM";
            this.txtANM.ReadOnly = true;
            this.txtANM.Size = new System.Drawing.Size(288, 41);
            this.txtANM.TabIndex = 2;
            this.txtANM.TabStop = false;
            this.txtANM.Tag = "0";
            // 
            // chkJournal
            // 
            this.chkJournal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkJournal.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkJournal.Location = new System.Drawing.Point(390, 205);
            this.chkJournal.Name = "chkJournal";
            this.chkJournal.Size = new System.Drawing.Size(70, 24);
            this.chkJournal.TabIndex = 3;
            this.chkJournal.TabStop = false;
            this.chkJournal.Text = "Se journal";
            this.chkJournal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkJournal.CheckedChanged += new System.EventHandler(this.chkJournal_CheckedChanged);
            // 
            // txtTelBostad
            // 
            this.txtTelBostad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTelBostad.Location = new System.Drawing.Point(96, 136);
            this.txtTelBostad.Name = "txtTelBostad";
            this.txtTelBostad.ReadOnly = true;
            this.txtTelBostad.Size = new System.Drawing.Size(288, 20);
            this.txtTelBostad.TabIndex = 11;
            this.txtTelBostad.TabStop = false;
            // 
            // txtORT
            // 
            this.txtORT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtORT.Location = new System.Drawing.Point(96, 112);
            this.txtORT.Name = "txtORT";
            this.txtORT.ReadOnly = true;
            this.txtORT.Size = new System.Drawing.Size(288, 20);
            this.txtORT.TabIndex = 10;
            this.txtORT.TabStop = false;
            // 
            // txtADD
            // 
            this.txtADD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtADD.Location = new System.Drawing.Point(96, 88);
            this.txtADD.Name = "txtADD";
            this.txtADD.ReadOnly = true;
            this.txtADD.Size = new System.Drawing.Size(288, 20);
            this.txtADD.TabIndex = 9;
            this.txtADD.TabStop = false;
            // 
            // txtSN
            // 
            this.txtSN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSN.Location = new System.Drawing.Point(96, 64);
            this.txtSN.Name = "txtSN";
            this.txtSN.ReadOnly = true;
            this.txtSN.Size = new System.Drawing.Size(288, 20);
            this.txtSN.TabIndex = 8;
            this.txtSN.TabStop = false;
            // 
            // txtLN
            // 
            this.txtLN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLN.Location = new System.Drawing.Point(96, 41);
            this.txtLN.Name = "txtLN";
            this.txtLN.ReadOnly = true;
            this.txtLN.Size = new System.Drawing.Size(288, 20);
            this.txtLN.TabIndex = 7;
            this.txtLN.TabStop = false;
            // 
            // txtPNR
            // 
            this.txtPNR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPNR.Location = new System.Drawing.Point(96, 17);
            this.txtPNR.Name = "txtPNR";
            this.txtPNR.Size = new System.Drawing.Size(221, 20);
            this.txtPNR.TabIndex = 0;
            this.txtPNR.Tag = "0";
            this.txtPNR.Enter += new System.EventHandler(this.txtPNR_Enter);
            this.txtPNR.Leave += new System.EventHandler(this.txtPNR_Leave);
            // 
            // labTEL
            // 
            this.labTEL.Location = new System.Drawing.Point(6, 137);
            this.labTEL.Name = "labTEL";
            this.labTEL.Size = new System.Drawing.Size(82, 16);
            this.labTEL.TabIndex = 5;
            this.labTEL.Text = "Tel Bostad";
            this.labTEL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labORT
            // 
            this.labORT.Location = new System.Drawing.Point(24, 112);
            this.labORT.Name = "labORT";
            this.labORT.Size = new System.Drawing.Size(64, 16);
            this.labORT.TabIndex = 4;
            this.labORT.Text = "Pnr/Ort:";
            this.labORT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labADD
            // 
            this.labADD.Location = new System.Drawing.Point(24, 89);
            this.labADD.Name = "labADD";
            this.labADD.Size = new System.Drawing.Size(64, 16);
            this.labADD.TabIndex = 3;
            this.labADD.Text = "Adress:";
            this.labADD.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labSN
            // 
            this.labSN.Location = new System.Drawing.Point(24, 65);
            this.labSN.Name = "labSN";
            this.labSN.Size = new System.Drawing.Size(64, 16);
            this.labSN.TabIndex = 2;
            this.labSN.Text = "Förnamn:";
            this.labSN.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labLN
            // 
            this.labLN.Location = new System.Drawing.Point(24, 41);
            this.labLN.Name = "labLN";
            this.labLN.Size = new System.Drawing.Size(64, 16);
            this.labLN.TabIndex = 1;
            this.labLN.Text = "Efternamn:";
            this.labLN.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labPnr
            // 
            this.labPnr.Location = new System.Drawing.Point(32, 17);
            this.labPnr.Name = "labPnr";
            this.labPnr.Size = new System.Drawing.Size(56, 16);
            this.labPnr.TabIndex = 0;
            this.labPnr.Text = "PersonNr:";
            this.labPnr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlMainLeft
            // 
            this.pnlMainLeft.Controls.Add(this.pnlLeft);
            this.pnlMainLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMainLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlMainLeft.Name = "pnlMainLeft";
            this.pnlMainLeft.Size = new System.Drawing.Size(96, 673);
            this.pnlMainLeft.TabIndex = 21;
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlLeft.Controls.Add(this.btnClose);
            this.pnlLeft.Controls.Add(this.btnNewOrder);
            this.pnlLeft.Controls.Add(this.btnDeleteOrder);
            this.pnlLeft.Controls.Add(this.btnCloseOrder);
            this.pnlLeft.Controls.Add(this.btnSwitchPatient);
            this.pnlLeft.Controls.Add(this.btnPrevPatient);
            this.pnlLeft.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pnlLeft.Location = new System.Drawing.Point(8, 8);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(85, 560);
            this.pnlLeft.TabIndex = 17;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::Ortoped.Properties.Resources.arrow_left_blue;
            this.btnClose.Location = new System.Drawing.Point(7, 453);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(69, 70);
            this.btnClose.TabIndex = 49;
            this.btnClose.Text = "&Avsluta";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnNewOrder
            // 
            this.btnNewOrder.FlatAppearance.BorderSize = 0;
            this.btnNewOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewOrder.Image = global::Ortoped.Properties.Resources.add;
            this.btnNewOrder.Location = new System.Drawing.Point(7, 16);
            this.btnNewOrder.Name = "btnNewOrder";
            this.btnNewOrder.Size = new System.Drawing.Size(69, 70);
            this.btnNewOrder.TabIndex = 48;
            this.btnNewOrder.Text = "&Ny order";
            this.btnNewOrder.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnNewOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNewOrder.UseVisualStyleBackColor = true;
            this.btnNewOrder.Click += new System.EventHandler(this.btnNewOrder_Click);
            // 
            // btnDeleteOrder
            // 
            this.btnDeleteOrder.FlatAppearance.BorderSize = 0;
            this.btnDeleteOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteOrder.Image = global::Ortoped.Properties.Resources.delete;
            this.btnDeleteOrder.Location = new System.Drawing.Point(7, 91);
            this.btnDeleteOrder.Name = "btnDeleteOrder";
            this.btnDeleteOrder.Size = new System.Drawing.Size(69, 70);
            this.btnDeleteOrder.TabIndex = 47;
            this.btnDeleteOrder.Text = "&Radera order";
            this.btnDeleteOrder.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDeleteOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteOrder.UseVisualStyleBackColor = true;
            this.btnDeleteOrder.Click += new System.EventHandler(this.btnDeleteOrder_Click);
            // 
            // btnCloseOrder
            // 
            this.btnCloseOrder.FlatAppearance.BorderSize = 0;
            this.btnCloseOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseOrder.Image = global::Ortoped.Properties.Resources.box;
            this.btnCloseOrder.Location = new System.Drawing.Point(7, 164);
            this.btnCloseOrder.Name = "btnCloseOrder";
            this.btnCloseOrder.Size = new System.Drawing.Size(69, 70);
            this.btnCloseOrder.TabIndex = 46;
            this.btnCloseOrder.Text = "&Stäng order";
            this.btnCloseOrder.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCloseOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCloseOrder.UseVisualStyleBackColor = true;
            this.btnCloseOrder.Click += new System.EventHandler(this.btnCloseOrder_Click);
            // 
            // btnSwitchPatient
            // 
            this.btnSwitchPatient.FlatAppearance.BorderSize = 0;
            this.btnSwitchPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSwitchPatient.Image = global::Ortoped.Properties.Resources.user1_into;
            this.btnSwitchPatient.Location = new System.Drawing.Point(7, 240);
            this.btnSwitchPatient.Name = "btnSwitchPatient";
            this.btnSwitchPatient.Size = new System.Drawing.Size(69, 70);
            this.btnSwitchPatient.TabIndex = 45;
            this.btnSwitchPatient.Text = "&Byt Patient";
            this.btnSwitchPatient.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSwitchPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSwitchPatient.UseVisualStyleBackColor = true;
            this.btnSwitchPatient.Click += new System.EventHandler(this.btnSwitchPatient_Click);
            // 
            // btnPrevPatient
            // 
            this.btnPrevPatient.FlatAppearance.BorderSize = 0;
            this.btnPrevPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevPatient.Image = global::Ortoped.Properties.Resources.users_back;
            this.btnPrevPatient.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPrevPatient.Location = new System.Drawing.Point(7, 317);
            this.btnPrevPatient.Name = "btnPrevPatient";
            this.btnPrevPatient.Size = new System.Drawing.Size(69, 69);
            this.btnPrevPatient.TabIndex = 25;
            this.btnPrevPatient.Text = "&Förg. Patient";
            this.btnPrevPatient.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPrevPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPrevPatient.UseVisualStyleBackColor = true;
            this.btnPrevPatient.Click += new System.EventHandler(this.btnPrevPatient_Click);
            // 
            // grbOH
            // 
            this.grbOH.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbOH.BackColor = System.Drawing.SystemColors.Control;
            this.grbOH.Controls.Add(this.txtOTADate);
            this.grbOH.Controls.Add(this.dtOTADate);
            this.grbOH.Controls.Add(this.btnSMS);
            this.grbOH.Controls.Add(this.btnStartProdView);
            this.grbOH.Controls.Add(this.btnOrderList);
            this.grbOH.Controls.Add(this.btnThord);
            this.grbOH.Controls.Add(this.btnIO);
            this.grbOH.Controls.Add(this.btnSaveOH);
            this.grbOH.Controls.Add(this.txtOrdination);
            this.grbOH.Controls.Add(this.txtTillagg);
            this.grbOH.Controls.Add(this.label12);
            this.grbOH.Controls.Add(this.label33);
            this.grbOH.Controls.Add(this.txtGilltigFrom);
            this.grbOH.Controls.Add(this.label30);
            this.grbOH.Controls.Add(this.txtSignature);
            this.grbOH.Controls.Add(this.label6);
            this.grbOH.Controls.Add(this.cboOrdinator);
            this.grbOH.Controls.Add(this.cboAidType);
            this.grbOH.Controls.Add(this.txtERF);
            this.grbOH.Controls.Add(this.label31);
            this.grbOH.Controls.Add(this.txtDiagID);
            this.grbOH.Controls.Add(this.cboPrislista);
            this.grbOH.Controls.Add(this.txtDiagTxt);
            this.grbOH.Controls.Add(this.label32);
            this.grbOH.Controls.Add(this.txtEndDate);
            this.grbOH.Controls.Add(this.label2);
            this.grbOH.Controls.Add(this.txtAidCount);
            this.grbOH.Controls.Add(this.label4);
            this.grbOH.Controls.Add(this.txtYears);
            this.grbOH.Controls.Add(this.label5);
            this.grbOH.Controls.Add(this.label29);
            this.grbOH.Controls.Add(this.label7);
            this.grbOH.Controls.Add(this.cboSignature);
            this.grbOH.Controls.Add(this.txtODT);
            this.grbOH.Controls.Add(this.label8);
            this.grbOH.Controls.Add(this.label23);
            this.grbOH.Controls.Add(this.label9);
            this.grbOH.Controls.Add(this.txtKlinikNamn);
            this.grbOH.Controls.Add(this.txtNotering);
            this.grbOH.Controls.Add(this.txtKlinik);
            this.grbOH.Controls.Add(this.label10);
            this.grbOH.Controls.Add(this.label20);
            this.grbOH.Controls.Add(this.label11);
            this.grbOH.Controls.Add(this.pictureBox5);
            this.grbOH.Controls.Add(this.txtFKN);
            this.grbOH.Controls.Add(this.btnDiagUppslag);
            this.grbOH.Controls.Add(this.txtFKN_NAM);
            this.grbOH.Controls.Add(this.dtpGilltigFrom);
            this.grbOH.Enabled = false;
            this.grbOH.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grbOH.Location = new System.Drawing.Point(579, 12);
            this.grbOH.Name = "grbOH";
            this.grbOH.Size = new System.Drawing.Size(497, 372);
            this.grbOH.TabIndex = 1;
            this.grbOH.TabStop = false;
            this.grbOH.Text = "Orderhuvud";
            // 
            // txtOTADate
            // 
            this.txtOTADate.Location = new System.Drawing.Point(86, 48);
            this.txtOTADate.MinimumSize = new System.Drawing.Size(60, 0);
            this.txtOTADate.Name = "txtOTADate";
            this.txtOTADate.Size = new System.Drawing.Size(80, 20);
            this.txtOTADate.TabIndex = 503;
            this.txtOTADate.Leave += new System.EventHandler(this.txtOTADate_Leave);
            // 
            // dtOTADate
            // 
            this.dtOTADate.Checked = false;
            this.dtOTADate.CustomFormat = "yyMMdd";
            this.dtOTADate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtOTADate.Location = new System.Drawing.Point(87, 48);
            this.dtOTADate.MinimumSize = new System.Drawing.Size(90, 0);
            this.dtOTADate.Name = "dtOTADate";
            this.dtOTADate.Size = new System.Drawing.Size(114, 20);
            this.dtOTADate.TabIndex = 504;
            this.dtOTADate.TabStop = false;
            this.dtOTADate.Value = new System.DateTime(2005, 4, 21, 0, 0, 0, 0);
            this.dtOTADate.ValueChanged += new System.EventHandler(this.dtOTADate_ValueChanged);
            this.dtOTADate.VisibleChanged += new System.EventHandler(this.dtOTADate_VisibleChanged);
            // 
            // btnSMS
            // 
            this.btnSMS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSMS.FlatAppearance.BorderSize = 0;
            this.btnSMS.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSMS.Image = global::Ortoped.Properties.Resources.sms;
            this.btnSMS.Location = new System.Drawing.Point(453, 84);
            this.btnSMS.Name = "btnSMS";
            this.btnSMS.Size = new System.Drawing.Size(32, 32);
            this.btnSMS.TabIndex = 502;
            this.toolTip2.SetToolTip(this.btnSMS, "Skicka SMS");
            this.btnSMS.UseVisualStyleBackColor = true;
            this.btnSMS.Click += new System.EventHandler(this.btnSMS_Click);
            // 
            // btnStartProdView
            // 
            this.btnStartProdView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartProdView.FlatAppearance.BorderSize = 0;
            this.btnStartProdView.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStartProdView.Image = global::Ortoped.Properties.Resources.gear;
            this.btnStartProdView.Location = new System.Drawing.Point(453, 50);
            this.btnStartProdView.Name = "btnStartProdView";
            this.btnStartProdView.Size = new System.Drawing.Size(32, 32);
            this.btnStartProdView.TabIndex = 501;
            this.toolTip2.SetToolTip(this.btnStartProdView, "Starta Produktionsöversikten");
            this.btnStartProdView.UseVisualStyleBackColor = true;
            this.btnStartProdView.Click += new System.EventHandler(this.btnStartProdView_Click);
            // 
            // btnOrderList
            // 
            this.btnOrderList.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOrderList.Location = new System.Drawing.Point(207, 21);
            this.btnOrderList.Name = "btnOrderList";
            this.btnOrderList.Size = new System.Drawing.Size(72, 23);
            this.btnOrderList.TabIndex = 37;
            this.btnOrderList.Text = "&Visa tidigare";
            this.btnOrderList.Click += new System.EventHandler(this.btnOrderList_Click);
            // 
            // btnThord
            // 
            this.btnThord.Location = new System.Drawing.Point(280, 20);
            this.btnThord.Name = "btnThord";
            this.btnThord.Size = new System.Drawing.Size(45, 24);
            this.btnThord.TabIndex = 87;
            this.btnThord.Text = "Thord";
            this.btnThord.UseVisualStyleBackColor = true;
            this.btnThord.Visible = false;
            this.btnThord.Click += new System.EventHandler(this.btnThord_Click);
            // 
            // btnIO
            // 
            this.btnIO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIO.FlatAppearance.BorderSize = 0;
            this.btnIO.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnIO.Image = global::Ortoped.Properties.Resources.document_pinned;
            this.btnIO.Location = new System.Drawing.Point(453, 11);
            this.btnIO.Name = "btnIO";
            this.btnIO.Size = new System.Drawing.Size(32, 37);
            this.btnIO.TabIndex = 91;
            this.toolTip2.SetToolTip(this.btnIO, "Visa alla inköpsorder som är kopplade till denna order");
            this.btnIO.UseVisualStyleBackColor = true;
            this.btnIO.Click += new System.EventHandler(this.btnIO_Click);
            // 
            // btnSaveOH
            // 
            this.btnSaveOH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveOH.FlatAppearance.BorderSize = 0;
            this.btnSaveOH.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSaveOH.Image = global::Ortoped.Properties.Resources.disk_blue;
            this.btnSaveOH.Location = new System.Drawing.Point(568, 119);
            this.btnSaveOH.Name = "btnSaveOH";
            this.btnSaveOH.Size = new System.Drawing.Size(32, 32);
            this.btnSaveOH.TabIndex = 90;
            this.btnSaveOH.UseVisualStyleBackColor = true;
            this.btnSaveOH.Visible = false;
            this.btnSaveOH.Click += new System.EventHandler(this.btnSaveOH_Click);
            // 
            // txtOrdination
            // 
            this.txtOrdination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOrdination.Location = new System.Drawing.Point(87, 237);
            this.txtOrdination.Multiline = true;
            this.txtOrdination.Name = "txtOrdination";
            this.txtOrdination.Size = new System.Drawing.Size(403, 38);
            this.txtOrdination.TabIndex = 57;
            this.txtOrdination.Leave += new System.EventHandler(this.checkIfOrderHeadIsChanged);
            // 
            // txtTillagg
            // 
            this.txtTillagg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTillagg.BackColor = System.Drawing.Color.White;
            this.txtTillagg.Location = new System.Drawing.Point(86, 195);
            this.txtTillagg.Multiline = true;
            this.txtTillagg.Name = "txtTillagg";
            this.txtTillagg.Size = new System.Drawing.Size(404, 36);
            this.txtTillagg.TabIndex = 56;
            this.txtTillagg.Leave += new System.EventHandler(this.checkIfOrderHeadIsChanged);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(40, 98);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 16);
            this.label12.TabIndex = 71;
            this.label12.Text = "Klinik:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(52, 382);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(28, 13);
            this.label33.TabIndex = 86;
            this.label33.Text = "Typ:";
            this.label33.Visible = false;
            // 
            // txtGilltigFrom
            // 
            this.txtGilltigFrom.Location = new System.Drawing.Point(87, 280);
            this.txtGilltigFrom.MinimumSize = new System.Drawing.Size(60, 0);
            this.txtGilltigFrom.Name = "txtGilltigFrom";
            this.txtGilltigFrom.Size = new System.Drawing.Size(80, 20);
            this.txtGilltigFrom.TabIndex = 59;
            this.txtGilltigFrom.Leave += new System.EventHandler(this.txtGilltigFrom_Leave);
            // 
            // label30
            // 
            this.label30.Location = new System.Drawing.Point(339, 281);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(48, 16);
            this.label30.TabIndex = 80;
            this.label30.Text = "Antal/år:";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSignature
            // 
            this.txtSignature.Location = new System.Drawing.Point(86, 339);
            this.txtSignature.MinimumSize = new System.Drawing.Size(150, 0);
            this.txtSignature.Name = "txtSignature";
            this.txtSignature.ReadOnly = true;
            this.txtSignature.Size = new System.Drawing.Size(150, 20);
            this.txtSignature.TabIndex = 88;
            this.txtSignature.Visible = false;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 171);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 15);
            this.label6.TabIndex = 63;
            this.label6.Text = "Diagnoskod:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboOrdinator
            // 
            this.cboOrdinator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboOrdinator.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboOrdinator.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboOrdinator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOrdinator.Items.AddRange(new object[] {
            ""});
            this.cboOrdinator.Location = new System.Drawing.Point(86, 121);
            this.cboOrdinator.Name = "cboOrdinator";
            this.cboOrdinator.Size = new System.Drawing.Size(361, 21);
            this.cboOrdinator.Sorted = true;
            this.cboOrdinator.TabIndex = 52;
            this.cboOrdinator.SelectedIndexChanged += new System.EventHandler(this.checkIfOrderHeadIsChanged);
            this.cboOrdinator.SelectionChangeCommitted += new System.EventHandler(this.cboOrdinator_SelectionChangeCommitted);
            this.cboOrdinator.Leave += new System.EventHandler(this.cboOrdinator_Leave);
            // 
            // cboAidType
            // 
            this.cboAidType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAidType.DropDownWidth = 150;
            this.cboAidType.FormattingEnabled = true;
            this.cboAidType.Items.AddRange(new object[] {
            "",
            "1 - Inlägg / Fotbädd",
            "2 - Ortopediska skor / bekvämskor",
            "3 - Ortos",
            "4 - Protes",
            "5 - Behandlingsskor",
            "6 - Övrigt",
            "7 - Behandlande ortos",
            "8 - Bråckband",
            "9 - Olikstora skor",
            "10 - Ändring av egna skor"});
            this.cboAidType.Location = new System.Drawing.Point(85, 378);
            this.cboAidType.Name = "cboAidType";
            this.cboAidType.Size = new System.Drawing.Size(130, 21);
            this.cboAidType.TabIndex = 85;
            this.cboAidType.Visible = false;
            this.cboAidType.SelectedIndexChanged += new System.EventHandler(this.checkIfOrderHeadIsChanged);
            // 
            // txtERF
            // 
            this.txtERF.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtERF.Location = new System.Drawing.Point(86, 145);
            this.txtERF.Name = "txtERF";
            this.txtERF.Size = new System.Drawing.Size(404, 20);
            this.txtERF.TabIndex = 53;
            this.txtERF.Leave += new System.EventHandler(this.checkIfOrderHeadIsChanged);
            // 
            // label31
            // 
            this.label31.Location = new System.Drawing.Point(239, 341);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(45, 16);
            this.label31.TabIndex = 84;
            this.label31.Text = "Prislista:";
            // 
            // txtDiagID
            // 
            this.txtDiagID.Location = new System.Drawing.Point(86, 169);
            this.txtDiagID.Name = "txtDiagID";
            this.txtDiagID.Size = new System.Drawing.Size(145, 20);
            this.txtDiagID.TabIndex = 54;
            this.txtDiagID.Enter += new System.EventHandler(this.txtDiagID_Enter);
            this.txtDiagID.Leave += new System.EventHandler(this.txtDiagID_Leave);
            // 
            // cboPrislista
            // 
            this.cboPrislista.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPrislista.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboPrislista.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPrislista.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPrislista.Location = new System.Drawing.Point(284, 337);
            this.cboPrislista.Name = "cboPrislista";
            this.cboPrislista.Size = new System.Drawing.Size(206, 21);
            this.cboPrislista.TabIndex = 83;
            this.cboPrislista.SelectedIndexChanged += new System.EventHandler(this.checkIfOrderHeadIsChanged);
            // 
            // txtDiagTxt
            // 
            this.txtDiagTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDiagTxt.Location = new System.Drawing.Point(267, 169);
            this.txtDiagTxt.Name = "txtDiagTxt";
            this.txtDiagTxt.ReadOnly = true;
            this.txtDiagTxt.Size = new System.Drawing.Size(223, 20);
            this.txtDiagTxt.TabIndex = 500;
            this.txtDiagTxt.TabStop = false;
            // 
            // label32
            // 
            this.label32.Location = new System.Drawing.Point(250, 282);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(23, 16);
            this.label32.TabIndex = 82;
            this.label32.Text = "Till:";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtEndDate
            // 
            this.txtEndDate.Location = new System.Drawing.Point(273, 280);
            this.txtEndDate.Name = "txtEndDate";
            this.txtEndDate.ReadOnly = true;
            this.txtEndDate.Size = new System.Drawing.Size(64, 20);
            this.txtEndDate.TabIndex = 81;
            this.txtEndDate.TabStop = false;
            this.txtEndDate.TextChanged += new System.EventHandler(this.txtEndDate_TextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(25, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "&Ordernr:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAidCount
            // 
            this.txtAidCount.Location = new System.Drawing.Point(387, 269);
            this.txtAidCount.Name = "txtAidCount";
            this.txtAidCount.Size = new System.Drawing.Size(20, 20);
            this.txtAidCount.TabIndex = 62;
            this.txtAidCount.Leave += new System.EventHandler(this.checkIfOrderHeadIsChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(24, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 18);
            this.label4.TabIndex = 58;
            this.label4.Text = "Ordinatör:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtYears
            // 
            this.txtYears.Location = new System.Drawing.Point(223, 279);
            this.txtYears.Name = "txtYears";
            this.txtYears.Size = new System.Drawing.Size(24, 20);
            this.txtYears.TabIndex = 61;
            this.txtYears.Text = "0";
            this.txtYears.Leave += new System.EventHandler(this.txtYears_Leave);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 17);
            this.label5.TabIndex = 60;
            this.label5.Text = "Er referens:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label29
            // 
            this.label29.Location = new System.Drawing.Point(204, 281);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(20, 16);
            this.label29.TabIndex = 79;
            this.label29.Text = "År:";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(22, 208);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 17);
            this.label7.TabIndex = 65;
            this.label7.Text = "Diagnos:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboSignature
            // 
            this.cboSignature.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboSignature.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboSignature.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSignature.Location = new System.Drawing.Point(86, 338);
            this.cboSignature.MinimumSize = new System.Drawing.Size(150, 0);
            this.cboSignature.Name = "cboSignature";
            this.cboSignature.Size = new System.Drawing.Size(150, 21);
            this.cboSignature.Sorted = true;
            this.cboSignature.TabIndex = 66;
            this.cboSignature.SelectedIndexChanged += new System.EventHandler(this.checkIfOrderHeadIsChanged);
            // 
            // txtODT
            // 
            this.txtODT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtODT.Location = new System.Drawing.Point(327, 22);
            this.txtODT.Name = "txtODT";
            this.txtODT.ReadOnly = true;
            this.txtODT.Size = new System.Drawing.Size(119, 20);
            this.txtODT.TabIndex = 78;
            this.txtODT.TabStop = false;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(15, 249);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 16);
            this.label8.TabIndex = 67;
            this.label8.Text = "Ordination:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.Location = new System.Drawing.Point(6, 49);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(73, 19);
            this.label23.TabIndex = 77;
            this.label23.Text = "Datum OTA:";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(11, 281);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 16);
            this.label9.TabIndex = 68;
            this.label9.Text = "Giltig från:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtKlinikNamn
            // 
            this.txtKlinikNamn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKlinikNamn.Location = new System.Drawing.Point(208, 97);
            this.txtKlinikNamn.Name = "txtKlinikNamn";
            this.txtKlinikNamn.ReadOnly = true;
            this.txtKlinikNamn.Size = new System.Drawing.Size(239, 20);
            this.txtKlinikNamn.TabIndex = 76;
            this.txtKlinikNamn.TabStop = false;
            // 
            // txtNotering
            // 
            this.txtNotering.AcceptsReturn = true;
            this.txtNotering.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNotering.Location = new System.Drawing.Point(87, 306);
            this.txtNotering.Multiline = true;
            this.txtNotering.Name = "txtNotering";
            this.txtNotering.Size = new System.Drawing.Size(403, 28);
            this.txtNotering.TabIndex = 64;
            this.txtNotering.Leave += new System.EventHandler(this.checkIfOrderHeadIsChanged);
            // 
            // txtKlinik
            // 
            this.txtKlinik.Location = new System.Drawing.Point(86, 97);
            this.txtKlinik.Name = "txtKlinik";
            this.txtKlinik.Size = new System.Drawing.Size(115, 20);
            this.txtKlinik.TabIndex = 51;
            this.txtKlinik.Enter += new System.EventHandler(this.txtKlinik_Enter);
            this.txtKlinik.Leave += new System.EventHandler(this.txtKlinik_Leave);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(23, 312);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 16);
            this.label10.TabIndex = 69;
            this.label10.Text = "Notering:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(8, 74);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(72, 16);
            this.label20.TabIndex = 75;
            this.label20.Text = "Fakturakund:";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(15, 338);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 18);
            this.label11.TabIndex = 70;
            this.label11.Text = "Signatur:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(568, 9);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(32, 32);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox5.TabIndex = 74;
            this.pictureBox5.TabStop = false;
            // 
            // txtFKN
            // 
            this.txtFKN.Location = new System.Drawing.Point(86, 73);
            this.txtFKN.Name = "txtFKN";
            this.txtFKN.Size = new System.Drawing.Size(115, 20);
            this.txtFKN.TabIndex = 50;
            this.txtFKN.Enter += new System.EventHandler(this.txtFKN_Enter);
            this.txtFKN.Leave += new System.EventHandler(this.txtFKN_Leave);
            // 
            // btnDiagUppslag
            // 
            this.btnDiagUppslag.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDiagUppslag.Location = new System.Drawing.Point(234, 166);
            this.btnDiagUppslag.Name = "btnDiagUppslag";
            this.btnDiagUppslag.Size = new System.Drawing.Size(24, 24);
            this.btnDiagUppslag.TabIndex = 73;
            this.btnDiagUppslag.TabStop = false;
            this.btnDiagUppslag.Text = "...";
            this.btnDiagUppslag.Click += new System.EventHandler(this.btnDiagUppslag_Click);
            // 
            // txtFKN_NAM
            // 
            this.txtFKN_NAM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFKN_NAM.Location = new System.Drawing.Point(208, 73);
            this.txtFKN_NAM.Name = "txtFKN_NAM";
            this.txtFKN_NAM.ReadOnly = true;
            this.txtFKN_NAM.Size = new System.Drawing.Size(239, 20);
            this.txtFKN_NAM.TabIndex = 72;
            this.txtFKN_NAM.TabStop = false;
            // 
            // dtpGilltigFrom
            // 
            this.dtpGilltigFrom.CustomFormat = "yyMMdd";
            this.dtpGilltigFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpGilltigFrom.Location = new System.Drawing.Point(88, 280);
            this.dtpGilltigFrom.MinimumSize = new System.Drawing.Size(90, 0);
            this.dtpGilltigFrom.Name = "dtpGilltigFrom";
            this.dtpGilltigFrom.Size = new System.Drawing.Size(114, 20);
            this.dtpGilltigFrom.TabIndex = 59;
            this.dtpGilltigFrom.TabStop = false;
            this.dtpGilltigFrom.Value = new System.DateTime(2005, 4, 21, 0, 0, 0, 0);
            this.dtpGilltigFrom.ValueChanged += new System.EventHandler(this.dtpGilltigFrom_ValueChanged);
            this.dtpGilltigFrom.VisibleChanged += new System.EventHandler(this.dtpGilltigFrom_VisibleChanged);
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 674);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.pnlBol,
            this.pnlGroup,
            this.pnlUser,
            this.pnlKst,
            this.pnlCustomConfig,
            this.pnlOrderStat,
            this.pnlORSaved,
            this.pnlSaveStat});
            this.statusBar1.ShowPanels = true;
            this.statusBar1.Size = new System.Drawing.Size(1088, 23);
            this.statusBar1.TabIndex = 17;
            // 
            // pnlBol
            // 
            this.pnlBol.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.pnlBol.Name = "pnlBol";
            this.pnlBol.Width = 10;
            // 
            // pnlGroup
            // 
            this.pnlGroup.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.pnlGroup.Name = "pnlGroup";
            this.pnlGroup.Width = 10;
            // 
            // pnlUser
            // 
            this.pnlUser.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.pnlUser.Name = "pnlUser";
            this.pnlUser.Width = 10;
            // 
            // pnlKst
            // 
            this.pnlKst.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.pnlKst.Name = "pnlKst";
            this.pnlKst.Width = 10;
            // 
            // pnlCustomConfig
            // 
            this.pnlCustomConfig.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.pnlCustomConfig.Name = "pnlCustomConfig";
            this.pnlCustomConfig.Width = 10;
            // 
            // pnlOrderStat
            // 
            this.pnlOrderStat.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.pnlOrderStat.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.pnlOrderStat.Name = "pnlOrderStat";
            this.pnlOrderStat.Width = 460;
            // 
            // pnlORSaved
            // 
            this.pnlORSaved.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.pnlORSaved.Name = "pnlORSaved";
            this.pnlORSaved.Text = "Orderrad ändrad: NEJ";
            this.pnlORSaved.Width = 460;
            // 
            // pnlSaveStat
            // 
            this.pnlSaveStat.Name = "pnlSaveStat";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.avslutaToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(46, 20);
            this.toolStripMenuItem1.Text = "Arkiv";
            // 
            // avslutaToolStripMenuItem
            // 
            this.avslutaToolStripMenuItem.Name = "avslutaToolStripMenuItem";
            this.avslutaToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.avslutaToolStripMenuItem.Text = "Avsluta";
            this.avslutaToolStripMenuItem.Click += new System.EventHandler(this.avslutaToolStripMenuItem_Click);
            // 
            // mnuThord
            // 
            this.mnuThord.Name = "mnuThord";
            this.mnuThord.Size = new System.Drawing.Size(51, 20);
            this.mnuThord.Text = "Thord";
            // 
            // mnuGetReferrals
            // 
            this.mnuGetReferrals.Name = "mnuGetReferrals";
            this.mnuGetReferrals.Size = new System.Drawing.Size(166, 22);
            this.mnuGetReferrals.Text = "Hämta remisser...";
            this.mnuGetReferrals.Click += new System.EventHandler(this.mnuGetReferrals_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(171, 22);
            this.toolStripMenuItem2.Text = "Inställningar...";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(168, 6);
            // 
            // mnuSendErrorReport
            // 
            this.mnuSendErrorReport.Name = "mnuSendErrorReport";
            this.mnuSendErrorReport.Size = new System.Drawing.Size(171, 22);
            this.mnuSendErrorReport.Text = "Skicka felrapport...";
            this.mnuSendErrorReport.Click += new System.EventHandler(this.mnuSendErrorReport_Click);
            // 
            // omToolStripMenuItem
            // 
            this.omToolStripMenuItem.Name = "omToolStripMenuItem";
            this.omToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.omToolStripMenuItem.Text = "Om...";
            this.omToolStripMenuItem.Click += new System.EventHandler(this.omToolStripMenuItem_Click);
            // 
            // bwThord
            // 
            this.bwThord.WorkerSupportsCancellation = true;
            this.bwThord.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.bwThord.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arkivToolStripMenuItem,
            this.thordToolStripMenuItem,
            this.hjälpToolStripMenuItem,
            this.shortcutsToolStripMenuItem});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(1088, 24);
            this.mnuMain.TabIndex = 18;
            this.mnuMain.Text = "menuStrip1";
            // 
            // arkivToolStripMenuItem
            // 
            this.arkivToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuQuit});
            this.arkivToolStripMenuItem.Name = "arkivToolStripMenuItem";
            this.arkivToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.arkivToolStripMenuItem.Text = "Arkiv";
            // 
            // mnuQuit
            // 
            this.mnuQuit.Name = "mnuQuit";
            this.mnuQuit.Size = new System.Drawing.Size(113, 22);
            this.mnuQuit.Text = "Avsluta";
            this.mnuQuit.Click += new System.EventHandler(this.mnuQuit_Click);
            // 
            // thordToolStripMenuItem
            // 
            this.thordToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGetReferrals});
            this.thordToolStripMenuItem.Name = "thordToolStripMenuItem";
            this.thordToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.thordToolStripMenuItem.Text = "Thord";
            // 
            // hjälpToolStripMenuItem
            // 
            this.hjälpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.skickaLoggfilToolStripMenuItem,
            this.toolStripMenuItem3});
            this.hjälpToolStripMenuItem.Name = "hjälpToolStripMenuItem";
            this.hjälpToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.hjälpToolStripMenuItem.Text = "Hjälp";
            // 
            // skickaLoggfilToolStripMenuItem
            // 
            this.skickaLoggfilToolStripMenuItem.Name = "skickaLoggfilToolStripMenuItem";
            this.skickaLoggfilToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.skickaLoggfilToolStripMenuItem.Text = "Skicka felrapport...";
            this.skickaLoggfilToolStripMenuItem.Click += new System.EventHandler(this.skickaLoggfilToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(171, 22);
            this.toolStripMenuItem3.Text = "Om...";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // shortcutsToolStripMenuItem
            // 
            this.shortcutsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ordernummerToolStripMenuItem,
            this.personnummerToolStripMenuItem});
            this.shortcutsToolStripMenuItem.Name = "shortcutsToolStripMenuItem";
            this.shortcutsToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.shortcutsToolStripMenuItem.Text = "Shortcuts";
            this.shortcutsToolStripMenuItem.Visible = false;
            // 
            // ordernummerToolStripMenuItem
            // 
            this.ordernummerToolStripMenuItem.Name = "ordernummerToolStripMenuItem";
            this.ordernummerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.O)));
            this.ordernummerToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.ordernummerToolStripMenuItem.Text = "Ordernummer";
            this.ordernummerToolStripMenuItem.Click += new System.EventHandler(this.ordernummerToolStripMenuItem_Click);
            // 
            // personnummerToolStripMenuItem
            // 
            this.personnummerToolStripMenuItem.Name = "personnummerToolStripMenuItem";
            this.personnummerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.P)));
            this.personnummerToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.personnummerToolStripMenuItem.Text = "Personnummer";
            this.personnummerToolStripMenuItem.Click += new System.EventHandler(this.personnummerToolStripMenuItem_Click);
            // 
            // getReferrals
            // 
            this.getReferrals.Name = "getReferrals";
            this.getReferrals.Size = new System.Drawing.Size(166, 22);
            this.getReferrals.Text = "Hämta remisser...";
            // 
            // bwGetOrdinators
            // 
            this.bwGetOrdinators.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwGetOrdinators_DoWork);
            this.bwGetOrdinators.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwGetOrdinators_RunWorkerCompleted);
            // 
            // timer1
            // 
            this.timer1.Interval = 5;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // toolTip2
            // 
            this.toolTip2.IsBalloon = true;
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1088, 697);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.mnuMain);
            this.DataBindings.Add(new System.Windows.Forms.Binding("WindowState", global::Ortoped.Properties.Settings.Default, "frmMain_WindowsState", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.mnuMain;
            this.Name = "frmMain";
            this.Text = "Patientöversikt";
            this.WindowState = global::Ortoped.Properties.Settings.Default.frmMain_WindowsState;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMain_Closing);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmMain_KeyPress);
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.grbOr.ResumeLayout(false);
            this.tabctrlRow.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabNew.ResumeLayout(false);
            this.scProductsAndSum.Panel1.ResumeLayout(false);
            this.scProductsAndSum.Panel2.ResumeLayout(false);
            this.scProductsAndSum.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scProductsAndSum)).EndInit();
            this.scProductsAndSum.ResumeLayout(false);
            this.grbArtList.ResumeLayout(false);
            this.mnuAidRows.ResumeLayout(false);
            this.grpSum.ResumeLayout(false);
            this.grpSum.PerformLayout();
            this.grbAid.ResumeLayout(false);
            this.grbAid.PerformLayout();
            this.grbArt.ResumeLayout(false);
            this.grbArt.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabpProduction.ResumeLayout(false);
            this.tabpProduction.PerformLayout();
            this.grbTid.ResumeLayout(false);
            this.grbPatient.ResumeLayout(false);
            this.grbPatient.PerformLayout();
            this.pnlMainLeft.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.grbOH.ResumeLayout(false);
            this.grbOH.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlKst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCustomConfig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlOrderStat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlORSaved)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSaveStat)).EndInit();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private GroupBox grbOH;
        private Label label12;
        private DateTimePicker dtpGilltigFrom;
        private Label label33;
        private TextBox txtGilltigFrom;
        private Label label30;
        private TextBox txtSignature;
        private Label label6;
        private Button btnThord;
        private ComboBox cboOrdinator;
        private ComboBox cboAidType;
        private TextBox txtERF;
        private Label label31;
        private TextBox txtDiagID;
        private ComboBox cboPrislista;
        private TextBox txtDiagTxt;
        private Label label32;
        private TextBox txtTillagg;
        private TextBox txtEndDate;
        private Label label2;
        private TextBox txtAidCount;
        private Label label4;
        private TextBox txtYears;
        private Label label5;
        private Label label29;
        private Label label7;
        private ComboBox cboSignature;
        private TextBox txtOrdination;
        private TextBox txtODT;
        private Label label8;
        private Label label23;
        private Label label9;
        private TextBox txtKlinikNamn;
        private TextBox txtNotering;
        private TextBox txtKlinik;
        private Label label10;
        private Label label20;
        private Label label11;
        private PictureBox pictureBox5;
        private TextBox txtFKN;
        private Button btnDiagUppslag;
        private TextBox txtFKN_NAM;
        private BackgroundWorker bwGetOrdinators;
        private Button btnSaveOH;
        private ToolStripMenuItem hjälpToolStripMenuItem;
        private ToolStripMenuItem skickaLoggfilToolStripMenuItem;
        private BackgroundWorker bwGarp;
        private Timer timer1;
        private ToolStripMenuItem toolStripMenuItem3;
        private Button btnIO;
        private ToolTip toolTip2;
        private ColumnHeader colHlpTextExists;
        private ToolStripMenuItem shortcutsToolStripMenuItem;
        private ToolStripMenuItem ordernummerToolStripMenuItem;
        private ToolStripMenuItem personnummerToolStripMenuItem;
        private Button btnCopDoc2;
        private Button btnCopDoc1;
        private TextBox txtTelSMS;
        private TextBox txtHolder;
        private Label label13;
        private TabPage tabpProduction;
        private CheckBox chkUrgent;
        private DateTimePicker dtpPromisedDeliverDate;
        private TextBox txtProductionTitle;
        private Label label19;
        private Label label14;
        private Button btnStartProdView;
        private ColumnHeader colUrgent;
        private ColumnHeader colProdTitle;
        private TextBox txtOR_ONR;
        private SplitContainer scProductsAndSum;
        private GroupBox grbArtList;
        private ListView lwAidRows;
        private ColumnHeader colAidRowsArtNo;
        private ColumnHeader colAidRowsBen;
        private ColumnHeader colAidRowsPcs;
        private ColumnHeader colAidRowsRdc;
        private GroupBox grpSum;
        private Label labSumInternal;
        private Label labSumExternal;
        private Label label35;
        private Label label17;
        private ColumnHeader colAidText;
        private Label labRemissNr;
        private Button btnSMS;
        private DateTimePicker dtpConditionDate;
        private Label label36;
        private ToolStripMenuItem visaMaterialplaneringenToolStripMenuItem;
        private TextBox txtOTADate;
        private DateTimePicker dtOTADate;
    }
}

