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
using Ortoped.Thord;
using Log4Net;
using GCS;
using System.Threading;
using System.IO;

namespace Ortoped
{
	/// <summary>
	/// Static class to handle configurationfile (configure.xml)
	/// 
	/// </summary>
	public partial class frmMain : System.Windows.Forms.Form
	{
		static Mutex mutex = new Mutex(true, "{8F6F0AC4-B9A1-45fd-A8CF-72F04E6BDE8F}");
        private const string FILENAME = "order.txt";
        private const int AIDNR = 0,
                            ART = 1,
                            BEN = 2,
                            ANT = 3,
                            APRIS = 4,
                            EGENAVGIFT = 5,
                            HANDL = 6,
                            PRODSTATUS = 7,
                            LEVTID = 8,
                            FAKNR = 9,
                            FAKDAT = 10,
                            HLPTEXTEXISTS = 11,
                            URGENT = 12,
                            PRODTITLE = 13,
                            AIDTEXT = 14;

		private CustomerFunc oCust;
		private orderFunc oOH;
		private OrderRowFunc oOR;
		private ErrandFunc oErr;
		private Contacts oCon;
		private Diagnos oDiagnos;
		private Prislista oPrislista;
		private DeliveryMode oDelM;
		private BackgroundWorker bwThord;
		private FormControler frmControler;
		private int iCurrentTab = 0;
		private bool ignoreItemCheckEvent = true, ignorePnrLeaveEvent = false, ignoreSave = false,
					  ignoreTabSwitch = false, ignoreDetailEvents = false, bFormUpdateInProgress = false,
					  bSaveInProgress = false, btxtONR_Leave_Event = false, bORChanged = false, ignoreCheckSignature = false;
		private string mSendMessage = "";
		private KeyboardSpy keyboardSpy;
        private int mSavedHeight, mSavedWidht;

		[STAThread]
		static void Main(string[] args)
		{
			try
			{
				// If no instans on this user is started we start a new one.
				if (mutex.WaitOne(TimeSpan.Zero, true))
				{
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

					if (args.Length > 0)
					{
						Application.Run(new Ortoped.frmMain(args[0]));
					}
					else
					{
						Application.Run(new Ortoped.frmMain());
					}
					mutex.ReleaseMutex();
				}
				else // Instans of PÖ already started, we post a brodcast message to current instans
				{
					if (args.Length > 0)
					{
						using (StreamWriter sw = new StreamWriter(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), FILENAME)))
						{
							sw.Write(args[0]);
							sw.Close();
							sw.Dispose();
						}
					}

					// send our Win32 message to make the currently running instance
					// jump on top of all the other windows
					MessageHelper.MessageHelper.PostMessage(
						(IntPtr)MessageHelper.MessageHelper.HWND_BROADCAST,
						MessageHelper.MessageHelper.WM_SHOWME,
						IntPtr.Zero,
						IntPtr.Zero);
				}
			}
			catch (Exception e)
			{
				Log4Net.Logger.loggError(e, "", "", "Main(string[] args)");
			}
		}


		/// <summary>
		/// Handle messages sent to this instans.
		/// </summary>
		/// <param name="m"></param>
		protected override void WndProc(ref Message m)
		{
			try
			{
				if (m.Msg == MessageHelper.MessageHelper.WM_SHOWME)
				{
					if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), FILENAME)))
					{
						try
						{
							using (StreamReader sr = new StreamReader(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), FILENAME)))
							{
								mSendMessage = sr.ReadToEnd().ToString().Trim();
								sr.Close();
								sr.Dispose();
							}
						}
						catch (Exception e)
						{
							Log4Net.Logger.loggError(e, "Error reading file: " + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), FILENAME), Config.User, "WndProc");
						}

						try
						{
							//File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), FILENAME));
						}
						catch (Exception e)
						{
							Log4Net.Logger.loggError(e, "Error deleteing file: " + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), FILENAME), Config.User, "WndProc");
						}

						try
						{
							setOrder(mSendMessage);
						}
						catch (Exception e)
						{
							Log4Net.Logger.loggError(e, "Error deleteing file: " + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), FILENAME), Config.User, "WndProc");
						}

					}
					ShowMe();
				}
			}
			catch (Exception e)
			{

			}

			base.WndProc(ref m);

		}

		private void ShowMe()
		{
			if (WindowState == FormWindowState.Minimized)
			{
				WindowState = FormWindowState.Normal;
			}
			// get our current "TopMost" value (ours will always be false though)
			bool top = TopMost;
			// make our form jump to the top of everything
			TopMost = true;
			// set it back to whatever it was
			TopMost = top;
		}

		private void frmMain_Load(object sender, System.EventArgs e)
		{
			Log4Net.Logger.loggInfo("Starting application", Config.User, "frmMain_Load");
            //tabctrlRow.TabPages.Remove(tabpProduction);

            mSavedHeight = this.Height;
            mSavedWidht = this.Width;

            try
            {
                if (bool.Parse(ConfigurationManager.AppSettings["SMS_Enabled"].ToString()))
                    btnSMS.Enabled = true;
                else
                    btnSMS.Enabled = false;
            }
            catch 
            {
                btnSMS.Enabled = false;
            }

			// Check if we should we should use ALWAYS UPPERCASE
			if (Config.AlwaysUpperCase)
			{
				txtKlinik.CharacterCasing = CharacterCasing.Upper;
				txtERF.CharacterCasing = CharacterCasing.Upper;
				txtTillagg.CharacterCasing = CharacterCasing.Upper;
				txtOrdination.CharacterCasing = CharacterCasing.Upper;
				txtNotering.CharacterCasing = CharacterCasing.Upper;
			}

			try
			{
				Config.IntPtrGarp = GCS_Process.ProcessActivate.GetCurrentInstanceWindowHandle("Garp");
				Log4Net.Logger.loggInfo("IntPtr of Garp is: " + Config.IntPtrGarp.ToString(), Config.User, "frmMain");
			}
			catch (Exception ex)
			{
				Log4Net.Logger.loggError(ex, "Error while retriving inptr for Garp", Config.User, "frmMain");
			}

			// CopDoc buttons
			try
			{
				if (Config.getCopDoc.Length > 0)
				{
					Config.CopDoc[] cd = Config.getCopDoc;

					foreach (Config.CopDoc c in cd)
					{
						if (c.btnIdx == 0)
						{
							btnCopDoc1.Enabled = true;
							btnCopDoc1.Text = c.ButtonText;
						}
						else if (c.btnIdx == 1)
						{
							btnCopDoc2.Enabled = true;
							btnCopDoc2.Text = c.ButtonText;
						}
					}
				}
				else
				{
					btnCopDoc1.Enabled = false;
					btnCopDoc2.Enabled = false;
				}
			}
			catch
			{
				btnCopDoc1.Enabled = false;
				btnCopDoc2.Enabled = false;
			}



			// Add menues for printing
			try
			{
				mnuOrderRow.MenuItems[11].MenuItems.AddRange(Config.getKallelse(new EventHandler(this.printEvent)));
				mnuOrderRow.MenuItems[11].MenuItems.AddRange(Config.getArbetsOrder(new EventHandler(this.printEvent)));
			}
			catch
			{
				MessageBox.Show(this, "Ett fel uppstod när menyalternativ för utskrift lades till", "Fel vid configuration", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}

			try
			{
				pnlMainLeft.Dock = DockStyle.Left;
				pnlLeft.Dock = DockStyle.Fill;
				grbOr.Tag = "";
				grbOH.Tag = "";
				grbPatient.Tag = "";
				txtONR.Parent = pnlBottom;
				txtKNR.Parent = pnlBottom;
				//lwAidRowsbtnOrderList.Parent = pnlBottom;
			}
			catch (Exception ex)
			{
				Log4Net.Logger.loggError(ex, "Ett fel uppstod när grbOr skulle initsieras ", Config.User, "frmMain_Load");
			}


			try
			{
				cboProdStatus.Items.Add("");
				cboProdStatus.Items.AddRange(oOR.oProdStat.getListOfReservations());
			}
			catch (Exception ex)
			{
				Log4Net.Logger.loggError(ex, "Ett fel uppstod när Produktionsstatus skulle initsieras", Config.User, "frmMain_Load");
			}

			// Get list of all handlers and updater combobox in "Details" (Signatures / Salesman i Garp)
			try
			{
				cboHandler.Items.AddRange(oOR.getListOfSalesman());
			}
			catch (Exception ex)
			{
				Log4Net.Logger.loggError(ex, "Ett fel uppstod när Handläggare skulle initsieras", Config.User, "frmMain_Load");
			}

			try
			{
				cboSignature.Items.AddRange(oOH.getListOfSignatures());
				cboSignature.Items.Add(" ");
			}
			catch (Exception ex)
			{
				Log4Net.Logger.loggError(ex, "Ett fel uppstod när Signaturer skulle initsieras", Config.User, "frmMain_Load");
			}

			// Get all pricelists
			try
			{
				cboPrislista.Items.AddRange(oPrislista.getPricelists());
				cboPrislista.Items.Add(" ");
			}
			catch (Exception ex)
			{
				Log4Net.Logger.loggError(ex, "Ett fel uppstod när Prislistor skulle initsieras", Config.User, "frmMain_Load");
			}

			try
			{
				//dtpGilltigFrom.Value = DateTime.ParseExact(DateTime.Today.ToString("yyMMdd"),"yyMMdd",new CultureInfo("sv-SE"));
				dtpLevtid.Value = DateTime.ParseExact(DateTime.Today.ToString("yyMMdd"), "yyMMdd", new CultureInfo("sv-SE"));

			}
			catch { }


			try
			{
				cboAidPriority.Items.Add("");
				cboAidPriority.Items.AddRange(oDelM.getListOfNames());
			}
			catch { }

			// Thordunikt
			if (Config.IsThordUser)
			{
				cboNeedStep.Visible = true;
				label24.Visible = true;
				mnuThord.Visible = true;
				btnThord.Visible = true;
				labRemissNr.Visible = true;
				chkFirstTimePatient.Visible = true;
				Log4Net.Logger.loggInfo("Configure as Thord user", Config.User, "frmMain_Load");
			}
			else
			{
				cboNeedStep.Visible = false;
				label24.Visible = false;
				mnuThord.Visible = false;
				btnThord.Visible = false;
				labRemissNr.Visible = false;
				chkFirstTimePatient.Visible = false;
				Log4Net.Logger.loggInfo("Configure as regulare (not Thord) user", Config.User, "frmMain_Load");
			}

#if !DEBUG
			frmProgress oProg = new frmProgress("Hämtar ordinatörer, vänta...", ref bwGetOrdinators);
			bwGetOrdinators.RunWorkerAsync();
			oProg.ShowDialog();
#else
			oOH.getAllOrdinators(true);
#endif

			try
			{
				this.Text = Application.ProductName + " - " + Application.ProductVersion;

#if DEBUG
				this.Text += " ### DEBUGMODE ###";
#endif
			}
			catch
			{
				this.Text = Application.ProductName;
			}

			/**
			 * Try to start keboardspy to use multiple clipboard values  
			 * when switching to Garp
			 **/
			try
			{
				keyboardSpy = new KeyboardSpy(Config.User, Config.IntPtrGarp);

				//if (File.Exists(Application.StartupPath + @"\ExternalResources\keyboardspy.exe"))
				//{
				//  pKeyBoardSpy = Process.Start(Application.StartupPath + @"\ExternalResources\keyboardspy.exe");
				//}
			}
			catch (Exception ex)
			{
				Log4Net.Logger.loggError(ex, "Error when trying to start keyboardspy", Config.User, "frmMain_Load");
			}

			// Show information in statusbar?
			showStatusBar(Config.ShowPreferences);
		}

		#region ==================================== FUNCTIONS ==================================================================

		/// <summary>
		/// Kontrollera vad resultatet blev genom att undersöka parametern "p".
		/// T ex kontroll och hantering av flera träffar eller inga träffar.
		/// </summary>
		/// <param name="p"></param>
		private bool checkPatient(PatientDefenition[] p)
		{
			if (p.Length == 0) // Ingen patient hittades
			{
				// Om det är en sökning skall inte upplägg av patient visas
				if (txtPNR.Text.StartsWith("."))
				{
					MessageBox.Show("Ingen Patienten som matchade sökbegreppet hittades", "Patient hittades ej", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return false;
				}
				else
				{
					if (MessageBox.Show("Patienten finns inte, vill du lägga upp en ny?", "patient saknas", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
					{
						return addPatient();
					}
					else // Användaren valde att inte lägga upp ny patient
					{
						return false;
					}
				}
			}
			else if (p.Length > 1) // Fler än en patient hittades
			{
				Ortoped.Dialogs.frmDiagPatient oDiaPat = new Ortoped.Dialogs.frmDiagPatient(PatientDefenition.convertToPatient(p));
				oDiaPat.ShowDialog();
				int idx = oDiaPat.selidx;
                string snn = oDiaPat.SSN;
                PatientDefenition selectedPatient = null;
				oDiaPat.Dispose();

                // Kontrollera om användaren valde Cancel (idx == -1)
				if (idx != -1)
				{
                    foreach (PatientDefenition p1 in p)
                    {
                        if (p1.SSN.Equals(snn))
                        {
                            selectedPatient = p1;
                            break;
                        }
                    }

                    // Check if it's a valid patient 
					if (selectedPatient.IsValid)
					{
						if (updateForm(selectedPatient))
							return true;
						else
							return false;
					}
					else
						return false;
				}
				else
					return false;
			}
			else	// Endast en patient hittades
			{
				if (p[0].IsValid)
				{
					updateForm(p[0]);
					return true;
				}
				else
					return false;
			}
		}

		private bool checkKlinik(CustomerFunc.Klinik[] p)
		{
			CustomerFunc.Klinik selKlinik = new CustomerFunc.Klinik();

			if (p.Length == 0) // Ingen Klinik hittades
			{
				MessageBox.Show("Hittade ingen klinik.", "Kund saknas", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
			}
			else if (p.Length > 1) // Fler än en kliniker hittades
			{
				int i = -1;
				Ortoped.Dialogs.frmDiagFkn oDiaFkn = new Ortoped.Dialogs.frmDiagFkn(CustomerFunc.Klinik.convertToFkn(p), ref i);
				oDiaFkn.ShowDialog();
				if (!oDiaFkn.selCust.Equals(""))
					selKlinik = oCust.getKlinikByCust(oDiaFkn.selCust)[0];
				oDiaFkn.Dispose();
			}
			else	// Endast en klinik hittades
			{
				selKlinik = p[0];
			}

			// Kontrollera om någon Klinik är vald
			if (selKlinik.CustNr != null)
			{
				// Uppdatera fält
				txtKlinik.Text = selKlinik.CustNr;
				txtKlinikNamn.Text = selKlinik.Name;
				string sOrd = cboOrdinator.Text;
				cboOrdinator.Items.Clear();
				cboOrdinator.Items.Add("");
				cboOrdinator.Items.AddRange(oOH.getOrdinatorsOnCustomer(txtKlinik.Text));
				cboOrdinator.SelectedIndex = cboOrdinator.FindStringExact(sOrd);

				// Om fakturakund (Landsting) ej är angivet så hämta fakturakunden från klinik
				if (txtFKN.Text.Trim() == "")
				{
					CustomerFunc.Fakturakund selFkn = oCust.getFakturakundByCust(selKlinik.InvoiceCustNr);
					if (!GCF.noNULL(selFkn.CustNr).Equals(""))
					{
						txtFKN.Text = selKlinik.InvoiceCustNr;
						txtFKN_NAM.Text = selKlinik.InvoiceCustName;
						cboPrislista.Text = selFkn.PriceList;
					}
				}
				return true;
			}
			else return false;
		}

		private bool checkFkn(CustomerFunc.Fakturakund[] p)
		{
			CustomerFunc.Fakturakund selFkn = new CustomerFunc.Fakturakund();

			if (p.Length == 0) // Ingen Klinik hittades
			{
				MessageBox.Show("Fakturakunden finns inte.", "Fakturakund saknas", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
			}
			else if (p.Length > 1) // Fler än en kliniker hittades
			{
				int i = -1;
				Ortoped.Dialogs.frmDiagFkn oDiaFkn = new Ortoped.Dialogs.frmDiagFkn(CustomerFunc.Fakturakund.convertToFkn(p), ref i);
				oDiaFkn.ShowDialog();
				if (!oDiaFkn.selCust.Equals(""))
					selFkn = oCust.getFakturakundByCust(oDiaFkn.selCust);

				oDiaFkn.Dispose();
			}
			else	// Endast en klinik hittades
			{
				selFkn = p[0];
			}

			if (selFkn.CustNr != null)
			{
				// Uppdatera f�lt
				txtFKN.Text = selFkn.CustNr;            // p[0].CustNr; 
				txtFKN_NAM.Text = selFkn.Name;          // p[0].Name;
				cboPrislista.Text = selFkn.PriceList;   // p[0].PriceList;
				return true;
			}
			else return false;
		}

		private bool checkProduct(OrderRowFunc.Product[] pr)
		{
			OrderRowFunc.Product selProd = new OrderRowFunc.Product();

			if (pr.Length == 0) // Ingen artikel hittades
			{
				MessageBox.Show("Artikeln finns inte.", "Artikel saknas", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
			}
			else if (pr.Length > 1) // Fler än en artikel hittades
			{
				int i = -1;
				Ortoped.Dialogs.frmDiagProduct oProd = new Ortoped.Dialogs.frmDiagProduct(OrderRowFunc.Product.convertToListView(pr), ref i);
				oProd.ShowDialog();
				selProd = oOR.findProductByID(oProd.selProd)[0];
				oProd.Dispose();
			}
			else	// Endast en artikel hittades
			{
				selProd = pr[0];
			}

			if (selProd.ProductNr != null)
			{
				// Uppdatera fält
				txtANR.Text = selProd.ProductNr;
				txtBEN.Text = selProd.ProductName;
				txtPRI.Text = selProd.Apris;
				return true;
			}
			else return false;
		}

		private bool checkSignature()
		{
            try
            {
                // Kontrollera att signatur finns innan order uppdateras
                if (cboSignature.Text.Trim().Equals("") && cboSignature.Visible && !GCS.GCF.noNULL(frmControler.CurrentOH.OrderNo).Equals("") && !ignoreCheckSignature)
                {
                    MessageBox.Show(this, "Signatur saknas! Ange signatur innan någon annan åtgärd utförs", "Signatur saknas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Log4Net.Logger.loggInfo("Signature was not given on order " + frmControler.CurrentOH.OrderNo, Config.User, "");
                    return false;
                }
                else
                    return true;
            }
            catch (Exception e)
            {
                Log4Net.Logger.loggInfo("Error while checking siganture " + frmControler.CurrentOH.OrderNo, Config.User, "checkSignature");
                return false;
            }
		}

		private OrderHeadDefinition fillOH(OrderHeadDefinition currentOH)
		{
			OrderHeadDefinition o = currentOH;

			if (o.PatientNo.Trim().Equals("") && !o.OrderNo.Trim().Equals(""))
			{
				if (!frmControler.CurrentPatient.PatientNo.Trim().Equals(""))
				{
					o.PatientNo = frmControler.CurrentPatient.PatientNo;
					Log4Net.Logger.loggWarning("In fillOH on order " + frmControler.CurrentOH.OrderNo + " Patient was emty, this is not good! Instead patient " + frmControler.CurrentPatient.PatientNo + " was used", Config.User, "");
				}
				else
				{
					Log4Net.Logger.loggCritical("No patient found either in order or current patient ( orderno: " + frmControler.CurrentOH.OrderNo + " )", Config.User, "");
				}
			}

			//o.PatientNo = o.PatientNo == "" ? currentPatient.CustNr : o.PatientNo;
			o.InvoiceCustomer = txtFKN.Text;
			o.Clinik = txtKlinik.Text;
			o.ClinikName = txtKlinikNamn.Text;
            o.ReferralDate = dtOTADate.Value;
			if (cboOrdinator.Text.Contains("("))
				o.SelOrdinator = cboOrdinator.Text.Substring(0, cboOrdinator.Text.IndexOf("("));
			else
				o.SelOrdinator = cboOrdinator.Text;
			o.YourReference = txtERF.Text;
			o.DiagnoseCode = txtDiagID.Text;
			o.Ordination = txtOrdination.Text;
			o.Diagnose = txtTillagg.Text;
			o.Notation = txtNotering.Text;
			o.OrderType = "1";
			o.ValidFrom = dtpGilltigFrom.Value;
			int i;
			try
			{
				if (int.TryParse(txtYears.Text, out i))
					o.ValidYearsCount = i;
				else
				{
					o.ValidYearsCount = 0;
					//Log4Net.Logger.loggCritical("Could not convert ValidYearsCount to Int, string value " + txtYears.Text, Config.User, "frmMain.fillOH");
				}
			}
			catch (Exception ex)
			{
				Log4Net.Logger.loggError(ex, "Error while parsing date", Config.User, "frmMain.fillOH");
			}
			o.AidCount = txtAidCount.Text;
			if (cboSignature.Visible)
				o.Signature = cboSignature.Text;
			else
				o.Signature = txtSignature.Text;

			try
			{
                if(cboPrislista.Text.Length >= 1)
				    o.Pricelist = cboPrislista.Text.Substring(0, 1);
			}
			catch
			{
				Log4Net.Logger.loggCritical("Problem while setting pricelist", Config.User, "frmMain.fillOH");
			}

			return o;
		}

		/// <summary>
		/// Function to fill object that is passed to SaveOrderRow function (in OrderRowFunc)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="fillprice"></param>
		/// <returns></returns>
		private OrderRowDefinitions.OrderRow fillOrderRow(object sender, bool fillprice)
		{
			OrderRowDefinitions.OrderRow or = new OrderRowDefinitions.OrderRow();

			saveOrder(false);

            // Changed row below in in a atemt to get rid of problem with orderrows that
            // are saved to wrong orderno. Now we always use the orderno from the row itself.
            //frmControler.CurrentOH.OrderNo;
            or.OrderNo = txtOR_ONR.Text; 
			or.AidDate = txtOrDatum.Text;
			or.AidNr = txtAidId.Text;
			or.Rad = txtRDC.Text;
			if (cboHandler.Text.Trim().Equals(""))
				or.SelectedHandler = txtHandler.Text;
			else
				or.SelectedHandler = cboHandler.Text;
			or.Prodstatus = cboProdStatus.Text;
			or.LevTid = txtLevDate.Text;
			or.AidPriority = oDelM.getKeyByName(cboAidPriority.Text);
			or.Artikel = txtANR.Text;
			or.Antal = txtORA.Text == "" ? "0" : txtORA.Text;
			//if (fillprice)
		    or.APris = txtPRI.Text == "" ? "0" : txtPRI.Text;
			or.Text = txtOrText.Text;
			or.InkStat = getBestallSelektion();
			or.Warrenty = chkGaranti.Checked;
			or.RemissNo = labRemissNr.Text;
			int.TryParse(txtAidOid.Text.Trim(), out or.AidOid);
			int.TryParse(txtPartOid.Text.Trim(), out or.PartOid);
			or.FirstTimePatient = chkFirstTimePatient.Checked;
			or.ProductionTitle = txtProductionTitle.Text;
			or.Urgent = chkUrgent.Checked;

            try
            {
                or.PromisedDeliverDate =  DateTime.ParseExact(txtPromisedDeliverDate.Text, "yyMMdd", new CultureInfo("sv-SE"));
            }
            catch
            {
                or.PromisedDeliverDate = null;
            }

            try
            {
                or.ConditionDate =  DateTime.ParseExact(txtConditionDate.Text, "yyMMdd", new CultureInfo("sv-SE"));
            }
            catch
            {
                or.ConditionDate = null;
            }

			try
			{
				or.Thord_NeedStep = cboNeedStep.Text.Substring(0, 2).Trim();
			}
			catch
			{
				or.Thord_NeedStep = "";
			}
			if (lwAidRows.Items.Count == 0)
				or.ViewInList = true;
			else
			{
				or.ViewInList = chkViewState.Checked;
			}

			return or;
		}

		private bool clearAllOHFields()
		{
			if (!saveOrder(true))
				return false;

			ignoreSave = true;
			tabctrlRow.SelectedIndex = 0;
			txtONR.Text = "";
            dtOTADate.Value = DateTime.Now;
            txtOTADate.Text = "";
			txtODT.Text = "";
			txtFKN.Text = "";
			txtFKN_NAM.Text = "";
			txtERF.Text = "";
			labRemissNr.Text = "";
			txtKlinik.Text = "";
			txtKlinikNamn.Text = "";
			cboOrdinator.Items.Clear();
			cboOrdinator.Text = "";
			txtDiagID.Text = "";
			txtDiagTxt.Text = "";
			txtTillagg.Text = "";
			txtOrdination.Text = "";
			txtNotering.Text = "";
			pnlOrderStat.Text = "";
			dtpGilltigFrom.Value = DateTime.Now;
			txtGilltigFrom.Text = dtpGilltigFrom.Value.ToString("yyMMdd");
			txtYears.Text = "";
			txtAidCount.Text = "";
			txtEndDate.Text = "";
			cboSignature.Text = " ";
			cboSignature.Visible = true;
			txtSignature.Visible = false;
			frmControler.CurrentOH = new OrderHeadDefinition();
			cboPrislista.Text = " ";
			grbOH.Enabled = false;
			txtFKN.Enabled = true;
			txtKlinik.Enabled = true;
            
 

			ignoreSave = false;

			return true;
		}

		private void clearAllPatientsFields()
		{
			txtKNR.Text = "";
			txtLN.Text = "";
			txtSN.Text = "";
			txtADD.Text = "";
			txtORT.Text = "";
			txtTelBostad.Text = "";
			txtANM.Text = "";
			txtPNR.Text = "";
			txtPNR.BackColor = Color.White;
			toolTip1.SetToolTip(txtPNR, "");
			txtTelArbete.Text = "";
			txtTelBostad.Text = "";
			txtTelMobil.Text = "";
			txtTelSMS.Text = "";
			chkJournal.Enabled = false;
			chkCopDok.Enabled = false;
			chkDeceased.Enabled = false;
			chkJournal.Checked = false;
			//      chkJournal.BackColor = SystemColors.Control;
			chkCopDok.Checked = false;
			chkDeceased.Checked = false;
			frmControler.CurrentPatient = new PatientDefenition();
		}

		private void clearAllAppointments()
		{
			lwErrand.Items.Clear();
		}

		private void clearAllOrderrows()
		{
			lwOr.Items.Clear();
		}

		private void clearOrderrowDetailPane(bool leave_tab)
		{
            ignoreDetailEvents = true;

            cboHandler.Text = "";
			txtHandler.Text = "";
			cboProdStatus.Text = "";
			cboAidPriority.Text = "";
			cboNeedStep.Text = "";
			dtpLevtid.Value = DateTime.ParseExact(DateTime.Today.ToString("yyMMdd"), "yyMMdd", new CultureInfo("sv-SE"));
            txtLevDate.Text = "";
            txtRDC.Text = "";
			txtANR.Text = "";
			txtBEN.Text = "";
			txtORA.Text = "";
			txtPRI.Text = "";
			txtAidId.Text = "";
			txtOrDatum.Text = "";
			lwAidRows.Items.Clear();
			txtProductionTitle.Text = "";
			chkUrgent.Checked = false;
			dtpPromisedDeliverDate.Value = DateTime.Now;
            dtpConditionDate.Value = DateTime.Now;
            txtPromisedDeliverDate.Text = "";
            txtConditionDate.Text = "";
            labSumExternal.Text = "0:-";
            labSumInternal.Text = "0:-";

			grbAid.Enabled = false;
			grbArt.Enabled = false;
			grbArtList.Enabled = false;

			// Visa översikten för att inte poster skall sparas på fel order
			if (leave_tab)
			{
				ignoreSave = true;
				tabctrlRow.SelectedIndex = 0;
				ignoreSave = false;
			}

            ignoreDetailEvents = false;
		}

		private bool updateForm(PatientDefenition p, OrderHeadDefinition o)
		{
			try
			{
				// Do not execute when already in progress
				if (bFormUpdateInProgress)
					return false;
				else
					bFormUpdateInProgress = true;

				// If current OH couldnt be saved then abort.
				if (!saveOrder(true))
				{
                    try
                    {
                        txtONR.Text = GCF.noNULL(frmControler.CurrentOH.OrderNo);
                        txtPNR.Text = GCF.noNULL(frmControler.CurrentPatient.SSN);
                        bFormUpdateInProgress = false;
                        Log4Net.Logger.loggWarning("In updateForm the saveOrder(true) failed on order " + frmControler.CurrentOH.OrderNo + " when trying to update order " + o.OrderNo, Config.User, "");
                        return false;
                    }
                    catch (Exception e)
                    {
                        Log4Net.Logger.loggWarning("Problem with saveorder in updateForm", Config.User, "");
                        return false;
                    } 
				}

				// Deaktivera sparafunktionen
				ignoreSave = true;


                try
                {
                    // Move current patient to last patient befor update of new patient 
                    // (if not currentpatient is emty or the same as the one to update form with)
                    if (!frmControler.CurrentPatient.PatientNo.Trim().Equals("") && !frmControler.CurrentPatient.PatientNo.Trim().Equals(p.PatientNo.Trim()))
                        frmControler.LastPatient = new PatientDefenition(frmControler.CurrentPatient);
                }
                catch (Exception e)
                {
                    Log4Net.Logger.loggWarning("Problem moving current patient to last patient", Config.User, "updateForm()");
                }

				// If current order is the same as the new order, update definition after save
				if (o.OrderNo.Trim().Equals(frmControler.CurrentOH.OrderNo))
					o = oOH.getOrder(o.OrderNo);

                try
                {
                    // Save current orderhead and patient
                    frmControler.CurrentOH = new OrderHeadDefinition(o);
                    frmControler.CurrentPatient = new PatientDefenition(p);
                }
                catch (Exception e)
                {
                    Log4Net.Logger.loggWarning("Problem saving current orderhead and patient", Config.User, "updateForm()");
                }

                try
                {
                    // Patient
                    txtKNR.Text = p.PatientNo;
                    txtPNR.Text = p.SSN;
                    txtLN.Text = p.LastName;
                    txtSN.Text = p.SureName;
                    txtADD.Text = p.Address;
                    txtORT.Text = p.PoCity;
                    txtTelBostad.Text = p.TelHome;
                    txtTelArbete.Text = p.TelWork;
                    txtTelMobil.Text = p.TelMobile;
                    txtTelSMS.Text = p.TelSMS;
                    chkCopDok.Enabled = true;
                    chkJournal.Enabled = true;
                    chkDeceased.Enabled = true;
                    chkJournal.Checked = p.Journal;
                    chkCopDok.Checked = p.CopDok;
                    chkDeceased.Checked = p.Deceased;
                    txtANM.Text = p.Remark;

                    if (p.OpenBalance > 0)
                    {
                        txtPNR.BackColor = Color.Salmon;
                        toolTip1.SetToolTip(txtPNR, "Öppet saldo: " + p.OpenBalance + ":-");
                    }
                    else
                    {
                        txtPNR.BackColor = Color.White;
                        toolTip1.SetToolTip(txtPNR, "");
                    }

                }
                catch (Exception e)
                {
                    Log4Net.Logger.loggWarning("Problem setting values on patient", Config.User, "updateForm()");
                }

                try
                {
                    // OH
                    if (o.isClosed)
                    {
                        pnlOrderStat.Text = "### Ordern är stängd ###";
                        btnCloseOrder.Text = "&Öppna order";
                    }
                    else
                    {
                        pnlOrderStat.Text = "";
                        btnCloseOrder.Text = "&Stäng order";
                    }

                    // If OH is delivererd, or partly delivered, we do not allow changes of these fields  ## JB 2010-05-19
                    if (o.CanChangeInvoiceCustomer)
                    {
                        txtFKN.Enabled = true;
                        txtKlinik.Enabled = true;
                    }
                    else
                    {
                        txtFKN.Enabled = false;
                        txtKlinik.Enabled = false;
                    }

                    txtONR.Text = o.OrderNo;
                    txtODT.Text = o.OrderDate;
                    txtFKN.Text = o.InvoiceCustomer;
                    txtFKN_NAM.Text = o.InvoiceCustomerName;
                    txtKlinik.Text = o.Clinik;
                    txtKlinikNamn.Text = o.ClinikName;
                    cboOrdinator.Items.Clear();
                    cboOrdinator.Text = "";
                    cboSignature.Text = "";


                    // ReferralDate (arrived at OTA)
                    try
                    {
                        dtOTADate.Value = o.ReferralDate;
                    }
                    catch (Exception ex)
                    {
                        Log4Net.Logger.loggError(ex, "Error while handling datetime ReferralDate", Config.User, "frmMain.updateForm");
                    }

                    // Hide combobox if signature is not found (and signature is not "")
                    //2009-05-06:JB if ((cboSignature.FindStringExact(o.Signature.Trim()) == -1) && !o.Signature.Trim().Equals(""))
                    if (!o.Signature.Trim().Equals(""))
                    {
                        cboSignature.Visible = false;
                        txtSignature.Visible = true;
                        txtSignature.Text = o.Signature;
                    }
                    else
                    {
                        cboSignature.Visible = true;
                        txtSignature.Visible = false;
                        try
                        {
                            cboSignature.SelectedIndex = cboSignature.FindStringExact(o.Signature);
                        }
                        catch (Exception e)
                        {
                            Log4Net.Logger.loggWarning("Problem setting signature on OH", Config.User, "updateForm()");
                        }
                    }

                    try
                    {
                        if (o.Ordinator != null)
                        {
                            cboOrdinator.Items.Add("");
                            cboOrdinator.Items.AddRange(o.Ordinator);
                        }

                        int idx = cboOrdinator.FindString(o.SelOrdinator);
                        if (idx != -1)
                            cboOrdinator.SelectedIndex = idx;
                    }
                    catch (Exception e)
                    {
                        Log4Net.Logger.loggWarning("Problem setting ordinator on OH", Config.User, "updateForm()");
                    }

                    txtERF.Text = o.YourReference;
                    labRemissNr.Text = o.ReferralNo;
                    txtDiagID.Text = o.DiagnoseCode;
                    try { txtDiagTxt.Text = oDiagnos.getDiagnosById(txtDiagID.Text.Trim()); }
                    catch { txtDiagTxt.Text = ""; Log4Net.Logger.loggWarning("Problem setting diag on OH", Config.User, "updateForm()");  }
                    txtOrdination.Text = o.Ordination;
                    txtTillagg.Text = o.Diagnose;
                    txtNotering.Text = o.Notation;

                    try
                    {
                        dtpGilltigFrom.Value = o.ValidFrom;
                        txtGilltigFrom.Text = o.ValidFrom.ToString("yyMMdd");
                        txtYears.Text = o.ValidYearsCount.ToString();
                    }
                    catch (Exception ex)
                    {
                        Log4Net.Logger.loggError(ex, "Error while handling datetime", Config.User, "frmMain.updateForm");
                    }

                    try
                    {
                        txtEndDate.Text = dtpGilltigFrom.Value.AddYears(int.Parse(txtYears.Text)).ToString("yyMMdd");
                    }
                    catch { }
                    txtAidCount.Text = o.AidCount;
                    cboPrislista.Text = oPrislista.getPriceListById(o.Pricelist);

                    tabctrlRow.SelectedIndex = 0;

                    try
                    {
                        lwOr.Items.Clear();
                        lwOr.Items.AddRange(Definitions.Aid.convertToListView(oOR.getAllAid(frmControler.CurrentOH.OrderNo), oOR));
                    }
                    catch (Exception e)
                    {
                        Log4Net.Logger.loggError(e, "Error while handling aids", Config.User, "updateForm");
                        Log4Net.Logger.loggInfo("Ordernummer: " + frmControler.CurrentOH.OrderNo, "", "");
                    }

                    try
                    {
                        if (lwOr.Items.Count > 0)
                            lwOr.Items[0].Selected = true;

                        clearOrderrowDetailPane(true);
                    }
                    catch
                    {
                        clearOrderrowDetailPane(true);
                    }

                    // Get unbound errand if orderno is given
                    if (!frmControler.CurrentOH.OrderNo.Trim().Equals(""))
                    {
                        if (oErr.getUnboundErrands(txtKNR.Text, frmControler.CurrentOH.OrderNo).Length > 0)
                        {
                            frmDiagUnboundErrand oDue = new frmDiagUnboundErrand(txtKNR.Text, frmControler.CurrentOH.OrderNo);
                            oDue.ShowDialog();
                        }
                    }

                    lwErrand.Items.Clear();
                    lwErrand.Items.AddRange(ErrandFunc.Errand.convertToErrand(oErr.getErrands(txtKNR.Text, frmControler.CurrentOH.OrderNo)));

                    // Kontrollera om vi skall enabla groupboxarna för OH och OR eller ej
                    if (o.OrderNo.Trim() == "")
                    {
                        grbOH.Enabled = false;
                        grbOr.Enabled = false;
                        tabctrlRow.Enabled = false;
                    }
                    else
                    {
                        grbOH.Enabled = true;
                        grbOr.Enabled = true;
                        tabctrlRow.Enabled = true;
                        //cboOrdinator.Focus();
                    }

                    if (!p.DoesExist)
                        grbTid.Enabled = false;
                    else
                        grbTid.Enabled = true;

                    // Aktivera sparafunktionen
                    ignoreSave = false;

                    bFormUpdateInProgress = false;
                    grbOH.ForeColor = grbPatient.ForeColor;
                }
                catch (Exception e)
                {
                    Log4Net.Logger.loggWarning("Problem setting values on OH", Config.User, "updateForm()");
                    return false;
                }

				Log4Net.Logger.loggInfo("Successfully updated order " + frmControler.CurrentOH.OrderNo + " on patient " + GCF.noNULL(frmControler.CurrentPatient.PatientNo), Config.User, "");
				return true;
			}
			catch (Exception ex)
			{
				Log4Net.Logger.loggError(ex, "Error in updateForm", Config.User, "");
				bFormUpdateInProgress = false;
				return false;
			}
		}

		private bool updateForm(PatientDefenition p)
		{
			return updateForm(p, oOH.getPatientsLastOH(p.PatientNo));
		}

		private bool updateForm(OrderHeadDefinition o)
		{
			return updateForm(oCust.getPatientByCust(o.PatientNo), o);
		}

		private void updateAidList()
		{
			int i = lwOr.SelectedItems[0].Index;
			lwOr.Items.Clear();
            lwOr.Items.AddRange(Definitions.Aid.convertToListView(oOR.getAllAid(frmControler.CurrentOH.OrderNo), oOR));
			if (lwOr.Items.Count > i)
				lwOr.Items[i].Selected = true;
		}

		/// <summary>
		/// Spara order
		/// </summary>
		/// <returns></returns>
		private bool saveOrder(bool checksignature)
		{
			OrderHeadDefinition oNew;
			PatientDefenition p;
			bool bOHChanged;

			// Do not execute when already in progress
			if (bSaveInProgress)
				return false;
			else
				bSaveInProgress = true;

			// Get the current orderhead
			if (frmControler.CurrentOH != null)
			{
				oNew = fillOH(new OrderHeadDefinition(frmControler.CurrentOH));
				p = frmControler.CurrentPatient;
				bOHChanged = frmControler.isOrderHeadChanged(frmControler.CurrentOH, oNew);
			}
			else
			{
				bSaveInProgress = false;
				//Log4Net.Logger.loggInfo("Saved aborted due to null in CurrentOH", Config.User);
				grbOH.ForeColor = grbPatient.ForeColor;
				return true;
			}

			// Check that ordernumber exists
			if (frmControler.CurrentOH.OrderNo.Trim().Equals(""))
			{
				bSaveInProgress = false;
				//Log4Net.Logger.loggInfo("Saved aborted due to no ordernumber", Config.User);
				grbOH.ForeColor = grbPatient.ForeColor;
				return true;
			}

			// Avbryt men returnera true
			if (ignoreSave)
			{
				//Log4Net.Logger.loggInfo("In saveOrder the order " + frmControler.CurrentOH.OrderNo + " was not saved because ignorSave was " + ignoreSave.ToString(), Config.User);
				bSaveInProgress = false;
				grbOH.ForeColor = grbPatient.ForeColor;
				return true;
			}

			// Avbryt men returnera true
			if (!bOHChanged)
			{
				//Log4Net.Logger.loggInfo("In saveOrder the order " + frmControler.CurrentOH.OrderNo + " was not saved because orderhead changed status was " + bOHChanged.ToString(), Config.User);
				bSaveInProgress = false;
				return true;
			}

			// Spara
			if (oOH.saveOH(oNew))
			{
				bSaveInProgress = false;
				frmControler.CurrentOH = new OrderHeadDefinition(oNew);
				Log4Net.Logger.loggInfo("Order " + frmControler.CurrentOH.OrderNo + " was successfully saved on patient " + oNew.PatientNo + " and current patient is " + frmControler.CurrentPatient.PatientNo, Config.User, "");
				grbOH.ForeColor = grbPatient.ForeColor;
				return true;
			}
			else
			{
				Log4Net.Logger.loggWarning("In saveOrder the order " + frmControler.CurrentOH.OrderNo + " was not saved, cause: " + oOH.getErrorMsg(), Config.User, "");
				bSaveInProgress = false;
				//if (MessageBox.Show(this, oOH.getErrorMsg() + "\n\nOrdern sparades inte, vill du fortsätta?", "Ordern sparades inte", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
				//  return true;
				//else
				//  return false;
				grbOH.ForeColor = grbPatient.ForeColor;
				return true;
			}
		}

		private bool addPatient()
		{
			frmAddCust newCust = new frmAddCust(txtPNR.Text);
			PatientDefenition patient;
			clearAllPatientsFields();	// Rensa innan patientupplägg visas
			clearAllOHFields();
			clearAllOrderrows();
			clearAllAppointments();
			clearOrderrowDetailPane(true);
			newCust.ShowDialog();
			patient = newCust.newPatient;

			// Kontrollera vad som returneras från formuläret för nyupplägg
			if (!GCF.noNULL(patient.SSN).Equals(""))
			{
				bool b = updateForm(newCust.newPatient);
				newCust.Dispose();
				// Gick uppdatering bra eller ej?
				if (b)
					return true;
				else
					return false;
			}
			else
			{
				if (!txtPNR.Tag.ToString().Equals(""))
				{
					try
					{
						updateForm(oCust.getPatientByPnr(txtPNR.Tag.ToString(), true, true)[0]);
					}
					catch (Exception e)
					{
						Log4Net.Logger.loggError(e, "Problem to get Patient from saved tag on txtPNR", Config.User, "");
						return false;
					}
				}
				return false;
			}
		}


		private void printEvent(object sender, System.EventArgs e)
		{
			MenuItem mnu = (MenuItem)sender;
			string sErrandId = "";

			if (!saveOrder(false))
				return;

			// Om ett hjälpmedel är valt, uppdatera ärendeinformation
			if (lwOr.SelectedItems.Count > 0)
			{
				ErrandFunc.Errand[] er = oErr.getErrandsOnAid(GCF.noNULL(frmControler.CurrentPatient.PatientNo), frmControler.CurrentOH.OrderNo, lwOr.SelectedItems[0].Text);

				// Uppdatera textrader med fräsch ärendedata
				foreach (ErrandFunc.Errand errand in er)
					oOR.updateAidWithErrand(frmControler.CurrentOH.OrderNo, lwOr.SelectedItems[0].Text, errand.ErrandID, errand.StartDatum, errand.Starttid, errand.Tidsperiod);

				// Om det finns mer än en tidsbokning på detta hjälpmedel så får användaren välja tidsbokning
				if (er.Length > 1)
				{
					frmDiagChooseErrand oCE = new frmDiagChooseErrand(txtKNR.Text, frmControler.CurrentOH.OrderNo, lwOr.SelectedItems[0].Text);
					oCE.ShowDialog();
					sErrandId = oCE.mErrandId;
				}
			}

			try
			{
				string[] s = mnu.Text.Split('-');

				// Kallelse eller Arbetsorder
				if (mnu.Parent.ToString().IndexOf("Dokument") != -1)
				{
					// Om ett hjälpmedel är valt, uppdatera ärendeinformation
					if (lwOr.SelectedItems.Count > 0)
						oOH.printNotice(s[0], s[1].Substring(0, 2), frmControler.CurrentOH.OrderNo, lwOr.SelectedItems[0].Text, sErrandId);
					else
						MessageBox.Show(this, "Du måste välja ett hjälmedel innan du kan skriva ut Dokument", "Inget hjälpmedel är valt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				else
				{
					oOH.printWorkOrder(s[0], s[1].Substring(0, 2), frmControler.CurrentOH.OrderNo, lwOr.SelectedItems[0].Text, sErrandId);
				}
			}
			catch
			{
				MessageBox.Show(this, "Du måste välja ett hjälpmedel innan du kan skriva ut Arbetsorder", "Inget hjälpmedel är valt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void showStatusBar(bool b)
		{
			if (b)
			{
				statusBar1.ShowPanels = true;
				statusBar1.Panels[0].Text = "Bolag: " + Config.CompanyId;
				statusBar1.Panels[1].Text = "Grupp: " + Config.Group;
				statusBar1.Panels[2].Text = "Användare: " + Config.User;
				statusBar1.Panels[3].Text = "Kostnadsställe: " + Config.Kst;
				if (Config.UserDefinedConfig)
					statusBar1.Panels[4].Text = "Unika inst.: Ja";
				else
					statusBar1.Panels[4].Text = "Unika inst.: Nej";
			}
			else
			{
				statusBar1.ShowPanels = false;
				statusBar1.Panels[0].Text = "";
				statusBar1.Panels[1].Text = "";
				statusBar1.Panels[2].Text = "";
				statusBar1.Panels[3].Text = "";
				statusBar1.Panels[4].Text = "";
			}
		}

		private void createRegistryPost()
		{
			//Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software").OpenSubKey("eXcido");
			//Microsoft.Win32.RegistryKey rkExcido;

			//try
			//{
			//    if (rk == null)
			//    {
			//        rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software", true);
			//        rkExcido = rk.CreateSubKey("eXcido");
			//        rkExcido.SetValue("Path", Application.StartupPath);
			//        rkExcido.SetValue("ConfigureFile", Application.StartupPath + @"\Configure.xml");
			//    }
			//}
			//catch (Exception e)
			//{
			//    MessageBox.Show(this, "Det gick inte att skapa nädvändiga registerposter av följande anledning: \n\n" + e.Message, "Kan inte skapa registerpost", MessageBoxButtons.OK, MessageBoxIcon.Information);
			//}
		}

		private string doDeliver(string bvk, bool checkdate, bool prompt)
		{
			string sFSNr = "";

			if (lwOr.SelectedItems.Count > 0)
			{
				if (GCF.noNULL(lwOr.SelectedItems[0].SubItems[PRODSTATUS].Text).ToString().Equals("0"))
				{
					// If we should prompt, do it
					if (prompt)
					{
						if (MessageBox.Show("Vill du genomföra leveransen?", "Leverans", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
						{
							return sFSNr;
						}
					}

					// Show that vi are saving to Thord          
					if (bORChanged && !labRemissNr.Text.Trim().Equals("")) // Thordvalues is changed and it's a referral from Thord
					{
						pnlSaveStat.Text = "Sparar till Thord...";
						pnlSaveStat.Icon = new Icon(GetType(), "disk_blue.ico");
					}

					sFSNr = oOR.deliverAid(oOR.getAllRowsOwnFeeIncluded(frmControler.CurrentOH.OrderNo, lwOr.SelectedItems[0].Text), bvk, checkdate);

					pnlSaveStat.Text = "";
					pnlSaveStat.Icon = null;

					if (!sFSNr.Trim().Equals(""))
					{
						updateAidList();
						updateForm(frmControler.CurrentOH);
					}
					else
						MessageBox.Show(this, "Leveransen utfördes EJ! Kontrollera att datum är angivet under Detaljer på hjälpmedlet", "Leverans ej genomförd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			else
				MessageBox.Show(this, "Välj ett hjälpmedel först", "Val saknas", MessageBoxButtons.OK, MessageBoxIcon.Information);

			return sFSNr;
		}

		private void setFormatClipBoardInfo(string text)
		{
			Clipboard.Clear();
			Clipboard.SetText("GCSMARK" + text, TextDataFormat.Text);
			Log4Net.Logger.loggInfo("Sent " + "GCSMARK" + text + " to clipboard", Config.User, "");

			GCS_Process.ProcessActivate.activateByPid(Config.IntPtrGarp);
		}



		#endregion


		#region ======================================== OH_EVENT ===============================================================

		private void txtPNR_Leave(object sender, System.EventArgs e)
		{
			if (!frmControler.CurrentPatient.SSN.Equals(txtPNR.Text) && !frmControler.CurrentPatient.SSN.Equals(""))
			{
				if (!checkSignature())
				{
					txtPNR.Text = frmControler.CurrentPatient.SSN;
					return;
				}
			}

			// If field is changed and not emty
			if ((txtPNR.Tag.ToString() != txtPNR.Text) && (!txtPNR.Text.Trim().Equals("")) && !ignorePnrLeaveEvent)
			{
				bool bFindExact = false;
				string search = "";
				ignorePnrLeaveEvent = true;

				// Is it at search or at whole index (SSN)
				if (txtPNR.Text.StartsWith("."))
				{
					bFindExact = false;
					search = txtPNR.Text.Substring(1);
				}
				else
				{
					bFindExact = true;
					search = txtPNR.Text;
				}

				// Om ingen patient hittades eller lades upp
				if (!checkPatient(oCust.getPatientByPnr(search, bFindExact, true)))
				{
					txtPNR.Focus();
					txtPNR.Text = txtPNR.Tag.ToString();
					txtPNR.SelectAll();
				}
				else
					txtONR.Focus();
			}
			else if (txtPNR.Text.Trim().Equals(""))
			{
				clearAllPatientsFields();
				clearAllOHFields();
				clearAllAppointments();
				clearAllOrderrows();
				clearOrderrowDetailPane(true);
				txtONR.Focus();
			}
			else
				txtONR.Focus();

			ignorePnrLeaveEvent = false;
		}

		private void txtPNR_Enter(object sender, System.EventArgs e)
		{
			txtPNR.Tag = txtPNR.Text;
		}

		private void txtKNR_Enter(object sender, System.EventArgs e)
		{
			txtKNR.Tag = txtKNR.Text;
		}

		private void frmMain_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)13)
			{
				if (!txtOrText.Focused && !txtAidText.Focused)
				{
					System.Windows.Forms.SendKeys.Send("{TAB}");
					e.Handled = true;
				}
			}
		}

		private void btnPrevPatient_Click(object sender, EventArgs e)
		{
			if (!checkSignature())
				return;

			try
			{
				if (!GCF.noNULL(frmControler.LastPatient.PatientNo).Equals(""))
				{
					updateForm(oCust.getPatientByCust(frmControler.LastPatient.PatientNo));
				}
			}
			catch { }
		}

		private void txtFKN_Leave(object sender, System.EventArgs e)
		{
			checkIfOrderHeadIsChanged(null, null);

			if ((txtFKN.Tag.ToString() != txtFKN.Text) && !txtFKN.Text.Trim().Equals(""))
			{
				if (txtFKN.Text.StartsWith("."))
				{
					if (!checkFkn(oCust.getFakturakundByName(txtFKN.Text.Substring(1))))
					{
						txtFKN.Focus();
						txtFKN.Text = txtFKN.Tag.ToString();
						txtFKN.SelectAll();
					}
				}
				else
				{
					CustomerFunc.Fakturakund[] f = new CustomerFunc.Fakturakund[1];
					f[0] = oCust.getFakturakundByCust(txtFKN.Text);
					if (!checkFkn(f))
					{
						txtFKN.Focus();
						txtFKN.Text = txtFKN.Tag.ToString();
						txtFKN.SelectAll();
					}
				}
			}
			else if (txtFKN.Text.Trim() == "")
			{
				txtFKN_NAM.Text = "";
			}
		}

		private void txtFKN_Enter(object sender, System.EventArgs e)
		{
			txtFKN.Tag = txtFKN.Text;
		}

		private void txtGem_Enter(object sender, System.EventArgs e)
		{
			TextBox t = (TextBox)sender;
			t.Tag = t.Text;
		}

		private void txtGem_Leave(object sender, System.EventArgs e)
		{
			TextBox t = (TextBox)sender;
			if ((t.Tag.ToString().Trim() != t.Text.Trim()) && (t.Parent.Visible == true))
				t.Parent.Tag = "unsaved";
		}

		private void txtKlinik_Enter(object sender, System.EventArgs e)
		{
			txtKlinik.Tag = txtKlinik.Text;
		}

		private void txtKlinik_Leave(object sender, System.EventArgs e)
		{
			checkIfOrderHeadIsChanged(null, null);

			if ((GCF.noNULL(txtKlinik.Tag).ToString() != txtKlinik.Text) && !txtKlinik.Text.Trim().Equals(""))
			{
				if (txtKlinik.Text.StartsWith("."))
				{
					if (!checkKlinik(oCust.getKlinikByName(txtKlinik.Text.Substring(1))))
					{
						txtKlinik.Focus();
						txtKlinik.Text = txtKlinik.Tag.ToString();
						txtKlinik.SelectAll();
					}
				}
				else
				{
					if (!checkKlinik(oCust.getKlinikByCust(txtKlinik.Text)))
					{
						txtKlinik.Focus();
						txtKlinik.Text = txtKlinik.Tag.ToString();
						txtKlinik.SelectAll();
					}
				}
			}
			else if (txtKlinik.Text.Trim() == "")
			{
				cboOrdinator.Items.Clear();
				cboOrdinator.Items.Add("");
				cboOrdinator.Items.AddRange(oOH.getAllOrdinators(false));
				txtKlinikNamn.Text = "";
			}
		}

		private void mnuNewAid_Click(object sender, System.EventArgs e)
		{
			string[] s = { "yyMMdd", "yyyyMMdd", "yyyy-MM-dd" };
			DateTime dt;

            if (tabctrlRow.SelectedIndex != 0)
            {
                if (MessageBox.Show("Fliken 'Översikt rader' måste vara aktiv när du skapar nytt hjälpmedel, vill du aktivera denna flik?", "Nytt hjälpmedel", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
                {
                    tabctrlRow.SelectedTab = tabctrlRow.TabPages[0];
                    return;
                }
                else
                    return;
            }

			if (!checkSignature())
				return;

            saveOrder(false);

			if (chkDeceased.Checked)
			{
				if (MessageBox.Show(this, "Patienten är avliden, vill du verkligen registrera\nett nytt hjälpmedel på denna patient?", "Patient avliden", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.No))
					return;
			}

			// Check if expiredate on order is reach.
			if (DateTime.TryParseExact(txtEndDate.Text, s, new CultureInfo("sv-SE"), DateTimeStyles.AssumeLocal, out dt))
			{
				if (DateTime.Today > dt)
					if (MessageBox.Show(this, "Gilltighetstiden på denna order är passerad. Vill du gå vidare ändå?", "Gilltighetstid", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
						return;
			}

			// Kontrollera om ordern är öppen
			if (!oOR.isOrderOpen(frmControler.CurrentOH.OrderNo))
			{
				MessageBox.Show(this, oOR.getErrorMsg(), "Nyupplägg är inte tillåtet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			try
			{
				ignoreTabSwitch = true;
				ignoreDetailEvents = true;

				clearOrderrowDetailPane(false);

				tabctrlRow.SelectedTab = tabctrlRow.TabPages[1];
				OrderRowDefinitions.OrderRow or = oOR.addNewAid(frmControler.CurrentOH.OrderNo);
                txtOR_ONR.Text = or.OrderNo;
                grbAid.Enabled = true;
				grbArt.Enabled = true;
				grbArtList.Enabled = true;
				lwAidRows.Items.Clear();
				txtAidId.Text = or.AidNr;
				txtOrDatum.Text = or.AidDate;
				cboHandler.SelectedIndex = 0;
				cboHandler.Text = or.SelectedHandler;
				cboProdStatus.SelectedIndex = 0;
				cboAidPriority.SelectedIndex = 0;
				cboNeedStep.SelectedIndex = 0;
				chkGaranti.Checked = false;
				chkFirstTimePatient.Checked = false;
				txtLevDate.Text = "";
				txtLevDate.Enabled = true;
				txtANR.Enabled = true;
				txtANR.Text = "";
				txtRDC.Text = or.Rad;
				txtORA.Text = "";
				txtPRI.Text = "";
				txtBEN.Text = "";
				labORA.Text = "Antal";

				cboHandler.Visible = true;
				txtHandler.Visible = false;
				cboHandler.Focus();
			}
			catch (Exception ex)
			{
				Log4Net.Logger.loggError(ex, "Error in mnuNewAid_Click", Config.User, "");
			}

			ignoreTabSwitch = false;
			ignoreDetailEvents = false;
		}

		private void tabctrlRow_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (!ignoreTabSwitch)
			{
				switch (tabctrlRow.SelectedIndex)
				{
					case 0:	// översikt
						try
						{
							if (!ignoreSave)
							{
								if (bORChanged && !labRemissNr.Text.Trim().Equals("")) // Thordvalues is changed and it's a referral from Thord
								{
									pnlSaveStat.Text = "Sparar till Thord...";
									pnlSaveStat.Icon = new Icon(GetType(), "disk_blue.ico");
									OrderRowDefinitions.OrderRow or = fillOrderRow(null, false);
									try
									{
										oOR.saveOrderRow(or, false, true);
									}
									catch (Exception ex)
									{
										MessageBox.Show(ex.Message, ex.Source);
									}

									pnlSaveStat.Text = "";
									pnlSaveStat.Icon = null;
								}
								else
									oOR.saveOrderRow(fillOrderRow(null, false), false, false);
							}
							updateAidList();

							valueNotChangedOR(sender, e);
						}
						catch
						{
							lwOr.Items.Clear();
                            lwOr.Items.AddRange(Definitions.Aid.convertToListView(oOR.getAllAid(frmControler.CurrentOH.OrderNo), oOR));
							if (lwOr.Items.Count > 0)
								lwOr.Items[0].Selected = true;
						}
						txtOrText.Text = "";
						iCurrentTab = 0;
						break;
					case 1:	// Rad
						if (lwOr.SelectedItems.Count > 0)
						{
							ignoreItemCheckEvent = false;

							if (iCurrentTab == 0)
								updateOrderRowDetailPane(oOR.getAid(frmControler.CurrentOH.OrderNo, lwOr.SelectedItems[0].SubItems[AIDNR].Text, false), true);
							else
								updateOrderRowDetailPane(oOR.getAid(frmControler.CurrentOH.OrderNo, txtAidId.Text, false), true);

							ignoreItemCheckEvent = true;
						}
						else
						{
							clearOrderrowDetailPane(false);
						};
						iCurrentTab = 1;

						bORChanged = false;
						pnlORSaved.Text = "Orderrad ändrad: NEJ";
						break;

					case 2:	// Textrader
						if (iCurrentTab == 0)	// Från översikt
						{
							if (lwOr.SelectedItems.Count > 0)
							{
                                label34.Text = "Hjälpmedelstext";
                                // 2012-10-24 JB SÄtts under lwOR.SelectedINdex event
                                //txtAidText.Text = oOR.getAidsTexts(frmControler.CurrentOH.OrderNo, lwOr.SelectedItems[0].Text);
                                txtLabAidTexter.Text = lwOr.SelectedItems[0].Text + " - " + lwOr.SelectedItems[0].SubItems[ART].Text + " - " + lwOr.SelectedItems[0].SubItems[BEN].Text;
                                txtAidText.Visible = true;
                                txtLabAidTexter.Visible = true;
                                txtOrText.Visible = false;
                                
                                txtLabArtTexter.Visible = false;

                                txtAidText.Focus();
							}
						}

						if (iCurrentTab == 1)	// Från artikelrader
						{
							label34.Text = "Artikeltext";
							txtLabArtTexter.Text = txtANR.Text + " - " + txtBEN.Text;
							txtAidText.Visible = false;
							txtLabAidTexter.Visible = false;
							txtOrText.Visible = true;
							txtLabArtTexter.Visible = true;

							if (lwAidRows.SelectedItems.Count > 0)
							{
								txtOrText.Text = oOR.getOrderRowTexts(frmControler.CurrentOH.OrderNo, txtRDC.Text);
							}
                            if(txtLabAidTexter.CanFocus)
							    txtLabArtTexter.Focus();

                            if(txtOrText.CanFocus)
							    txtOrText.Focus();
						}
						iCurrentTab = 2;
                        tabctrlRow.Refresh();
						break;

                    case 3:	// Production
                        if (lwOr.SelectedItems.Count > 0)
                        {
                            ignoreItemCheckEvent = false;

                            if (iCurrentTab == 0)
                                updateOrderRowDetailPane(oOR.getAid(frmControler.CurrentOH.OrderNo, lwOr.SelectedItems[0].SubItems[AIDNR].Text, false), true);
                            else
                                updateOrderRowDetailPane(oOR.getAid(frmControler.CurrentOH.OrderNo, txtAidId.Text, false), true);

                            ignoreItemCheckEvent = true;
                        }
                        else
                        {
                            clearOrderrowDetailPane(false);
                        };
                        iCurrentTab = 3;

                        bORChanged = false;
                        pnlORSaved.Text = "Orderrad ändrad: NEJ";
                        break;
					default:
						break;
				}
			}
            else
			    iCurrentTab = tabctrlRow.SelectedIndex;
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			oOR.removeRow(frmControler.CurrentOH.OrderNo, txtRDC.Text);

			lwOr.Items.Clear();
            lwOr.Items.AddRange(Definitions.Aid.convertToListView(oOR.getAllAid(frmControler.CurrentOH.OrderNo), oOR));
		}

		private void btnDiagUppslag_Click(object sender, System.EventArgs e)
		{
			// Om ingen order är satt tillåter vi inte uppdatering av detta fält
			if (frmControler.CurrentOH.OrderNo.Trim().Equals(""))
				txtONR.Focus();

			frmDiagDiagnos oDiagForm = new frmDiagDiagnos(oDiagnos.getDiagnosList());
			oDiagForm.ShowDialog();
			txtDiagID.Text = oDiagForm.selDiagnos;
			txtDiagTxt.Text = oDiagnos.getDiagnosById(txtDiagID.Text);
			oDiagForm.Dispose();
		}

		private void txtONR_Leave(object sender, System.EventArgs e)
		{
			// Don't trigg twice
			if (btxtONR_Leave_Event)
				return;
			else
				btxtONR_Leave_Event = true;

			try
			{
				if (!checkSignature())
				{
					txtONR.Text = frmControler.CurrentOH.OrderNo;
					//txtONR.Focus();
					btxtONR_Leave_Event = false;
					return;
				}

				if (!txtONR.Text.Trim().Equals(""))
				{
					OrderHeadDefinition oTempOH = oOH.getOrder(txtONR.Text);
					// If not null, an order with given ordernumber is found
					if (oTempOH.OrderNo != null)
					{
						PatientDefenition oTempPatient = oCust.getPatientByCust(oTempOH.PatientNo);
						if (oTempPatient.DoesExist)
							updateForm(oTempOH);
						else
						{
							clearAllOHFields();
							clearAllAppointments();
							clearAllOrderrows();
							clearOrderrowDetailPane(true);
							txtONR.Focus();
						}
					}
					else
					{
						clearAllOHFields();
						clearAllAppointments();
						clearAllOrderrows();
						clearOrderrowDetailPane(true);
						txtONR.Focus();
					}
				}
				else
				{
					if (clearAllOHFields())
					{
						clearAllAppointments();
						clearAllOrderrows();
						clearOrderrowDetailPane(true);
					}
					else
						txtONR.Text = frmControler.CurrentOH.OrderNo;
				}
			}
			catch (Exception ex)
			{
				Log4Net.Logger.loggError(ex, "Error in ONR_Leave", Config.User, "frmMain.txtONR_Leave");
                txtFKN.Focus();
				btxtONR_Leave_Event = false;
			}

            txtFKN.Focus();
			btxtONR_Leave_Event = false;
		}

		private void txtYears_Leave(object sender, System.EventArgs e)
		{
			try
			{
				// Blankt blir noll (0)
				if (txtYears.Text.Equals(""))
					txtYears.Text = "0";

				txtEndDate.Text = dtpGilltigFrom.Value.AddYears(int.Parse(txtYears.Text)).ToString("yyMMdd");
			}
			catch
			{
				MessageBox.Show(this, "Antal är ett ogiltigt värde, måste vara ett heltal", "Värdefel");
			}
			checkIfOrderHeadIsChanged(null, null);
		}

		private void mnuDeliver_Click(object sender, System.EventArgs e)
		{
			doDeliver("", true, true);
		}

		private void txtDiagID_Leave(object sender, System.EventArgs e)
		{
			checkIfOrderHeadIsChanged(null, null);

			if ((txtDiagID.Tag.ToString() != txtDiagID.Text))
			{
				if (!txtDiagID.Text.Equals(""))
				{
					if (oDiagnos.ContainsId(txtDiagID.Text.Trim()))
					{
						txtDiagTxt.Text = oDiagnos.getDiagnosById(txtDiagID.Text);
						txtDiagID.Focus();
						txtDiagID.SelectAll();
					}
					else
					{
						txtDiagID.Text = "";
						txtDiagTxt.Text = "";
						txtDiagID.Focus();
					}

				}
				else
					txtDiagTxt.Text = "";
			}
		}

		private void txtDiagID_Enter(object sender, System.EventArgs e)
		{
			txtDiagID.Tag = txtDiagID.Text;
		}

		private void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (!frmControler.CurrentOH.OrderNo.Trim().Equals(""))
			{
				if (!saveOrder(true))
					e.Cancel = true;
			}
		}

		//private void txtEnterEmtyField(object sender, System.EventArgs e)
		//{
		//    //Om inge order är satt tillåter vi inte uppdatering av detta fält
		//    if (GCF.noNULL(frmControler.CurrentOH.OrderNo).Trim().Equals(""))
		//        txtONR.Focus();
		//}

		//private void txtNotering_Enter(object sender, System.EventArgs e)
		//{
		//    // Om inge order är satt tillåter vi inte uppdatering av detta fält
		//    if (GCF.noNULL(frmControler.CurrentOH.OrderNo).Trim().Equals(""))
		//        txtONR.Focus();
		//}

		private void setOrder(string onr)
		{
			try
			{
				Log4Net.Logger.loggInfo("Enter setOrder with orderno:" + onr, Config.User, "frmMain.setOrder");

				if (onr.Contains("-"))
				{
					onr = onr.Substring(0, onr.IndexOf('-'));
				}

				OrderHeadDefinition oTempOH = oOH.getOrder(onr);

				if (oTempOH.OrderNo != null)
				{
					PatientDefenition oTempPatient = oCust.getPatientByCust(oTempOH.PatientNo);
					if (oTempPatient.DoesExist)
					{
						updateForm(oTempOH);
						txtONR.Focus(); // 101124-JB: Ändrat från txtERF till txtONR
					}
					else
					{
						Log4Net.Logger.loggCritical("Patienten som är angiven på ordern finns ej upplagd Kundnr: " + oTempOH.PatientNo, Config.User, "");
						clearAllPatientsFields();
						clearAllOHFields();
						clearAllAppointments();
						clearAllOrderrows();
						clearOrderrowDetailPane(true);
					}
				}
			}
			catch (Exception ex)
			{
				Log4Net.Logger.loggError(ex, "Error in setOrder", Config.User, "");
			}
		}

		// 
		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			//ProcessActivate pa = new ProcessActivate();
			//pa.activateByTitle("Garp");
			if (lwOr.SelectedItems.Count > 0)
				setFormatClipBoardInfo(txtKNR.Text + ";" + frmControler.CurrentOH.OrderNo.Trim() + "-" + lwOr.SelectedItems[0].Text + ";" + txtPNR.Text);
			else
				setFormatClipBoardInfo(txtKNR.Text + ";" + frmControler.CurrentOH.OrderNo.Trim() + ";" + txtPNR.Text);
			oErr.showCalendar();
			//Clipboard.SetDataObject(txtKNR.Text);
		}

		private void menuItem8_Click(object sender, System.EventArgs e)
		{
			//ProcessActivate pa = new ProcessActivate();
			//pa.activateByTitle("Garp");
			if (lwOr.SelectedItems.Count > 0)
				setFormatClipBoardInfo(txtKNR.Text + ";" + frmControler.CurrentOH.OrderNo.Trim() + "-" + lwOr.SelectedItems[0].Text + ";" + txtPNR.Text);
			else
				setFormatClipBoardInfo(txtKNR.Text + ";" + frmControler.CurrentOH.OrderNo.Trim() + ";" + txtPNR.Text);

			oErr.showCalendar();
		}

		private void chkJournal_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chkJournal.Focused && chkJournal.Enabled)
				oCust.setJournal(txtKNR.Text, chkJournal.Checked);

			if (chkJournal.Checked)
				chkJournal.BackColor = Color.Red;
			else
				chkJournal.BackColor = SystemColors.Control;

		}

		private void chkCopDok_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chkCopDok.Focused && chkCopDok.Enabled)
				oCust.setCopDoc(txtKNR.Text, chkCopDok.Checked);
		}

		private void txtOrText_Leave(object sender, System.EventArgs e)
		{
			if (!txtRDC.Text.Trim().Equals(""))
				oOR.saveOrderRowTexts(frmControler.CurrentOH.OrderNo, txtRDC.Text, txtOrText.Text);
		}

		//private void cboOrdinator_Enter(object sender, System.EventArgs e)
		//{
		//    // Om inge order är satt tillåter vi inte uppdatering av detta fält
		//    if (frmControler.CurrentOH.OrderNo.Trim().Equals(""))
		//        txtONR.Focus();
		//}

		private void frmMain_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			//MessageBox.Show(e.KeyValue.ToString());
			if (e.Control && e.Alt && e.Shift && e.KeyValue.Equals((int)Keys.V))
			{
				if (statusBar1.ShowPanels)
					showStatusBar(false);
				else
					showStatusBar(true);
			}

            if (e.Control && e.Alt && e.Shift && e.KeyValue.Equals((int)Keys.S))
            {
                if (this.Width == mSavedWidht)
                {
                    this.Width = 1024;
                    this.Height = 768;
                }
                else
                {
                    this.Width = mSavedWidht;
                    this.Height = mSavedHeight;
                }
                e.SuppressKeyPress = true;
            }

			if (e.Control && e.Alt && e.Shift && e.KeyValue.Equals((int)Keys.H))
			{
				if (txtRDC.Visible)
				{
					txtRDC.Visible = false;
					txtKNR.Visible = false;
					txtAidOid.Visible = false;
					txtPartOid.Visible = false;
					chkUpdatedInThord.Visible = false;
				}
				else
				{
					txtRDC.Visible = true;
					txtKNR.Visible = true;
					txtAidOid.Visible = true;
					txtPartOid.Visible = true;
					chkUpdatedInThord.Visible = true;
				}
			}

			// To enable shortcut CTRL + I (new aid) in whole form
			if (e.Control && e.KeyValue == 73)
				mnuNewAid_Click(sender, e);

			if (e.KeyCode.Equals(Keys.F5))
				btnSaveOH_Click(null, null);

			if (e.KeyCode.Equals(Keys.F5))
				btnSaveOH_Click(null, null);
		}

		private void btnOrderList_Click(object sender, System.EventArgs e)
		{
			if (!checkSignature())
				return;

			Ortoped.Dialogs.frmDiagOH oOrderHuvud = new Ortoped.Dialogs.frmDiagOH(OrderHeadDefinition.convertToListView(oOH.getPatientsAllOH(txtKNR.Text)));
			oOrderHuvud.ShowDialog();

			if (oOrderHuvud.selOnr != "")
			{
				OrderHeadDefinition oTempOH = oOH.getOrder(oOrderHuvud.selOnr);
				updateForm(oTempOH);
			}
			oOrderHuvud.Dispose();
		}

		private void txtLevDate_Leave(object sender, EventArgs e)
		{
			DateTime dt = new DateTime();
			string[] s = { "yyMMdd", "yyyyMMdd", "yyyy-MM-dd" };

			if (!txtLevDate.Text.Equals(""))
			{
				if (DateTime.TryParseExact(txtLevDate.Text, s, new CultureInfo("sv-SE"), DateTimeStyles.AssumeLocal, out dt))
					txtLevDate.Text = dt.ToString("yyMMdd");
				else
					MessageBox.Show("Ogiltligt datum");
			}

			if (txtLevDate.Focused && !txtANR.Text.Trim().Equals("") && !ignoreDetailEvents)
				if (!oOR.saveOrderRow(fillOrderRow(sender, false), true, false))
					MessageBox.Show(this, oOR.getErrorMsg(), "Uppdatering är inte tillåtet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		private void avslutaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Avsluta menyn");
			Properties.Settings.Default.Save();
		}

		private void omToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmAbout oAbout = new frmAbout();
			oAbout.ShowDialog();
		}

		private void toolStripMenuItem2_Click(object sender, EventArgs e)
		{
			frmSettings oSettings = new frmSettings();
			oSettings.ShowDialog();
		}

		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!checkSignature())
			{
				e.Cancel = true;
				return;
			}

			Ortoped.Properties.Settings.Default.frmMain_WindowsState = this.WindowState;
			Ortoped.Properties.Settings.Default.colAidNrDI = colAidNr.DisplayIndex;
			Ortoped.Properties.Settings.Default.colAidNrWidth = colAidNr.Width;
			Ortoped.Properties.Settings.Default.colAntDI = colAnt.DisplayIndex;
			Ortoped.Properties.Settings.Default.colAntWidth = colAnt.Width;
			Ortoped.Properties.Settings.Default.colAprisDI = colApris.DisplayIndex;
			Ortoped.Properties.Settings.Default.colAprisWidth = colApris.Width;
			Ortoped.Properties.Settings.Default.colArtDI = colArt.DisplayIndex;
			Ortoped.Properties.Settings.Default.colArtWidth = colArt.Width;
			Ortoped.Properties.Settings.Default.colBenDI = colBen.DisplayIndex;
			Ortoped.Properties.Settings.Default.colBenWidth = colBen.Width;
			Ortoped.Properties.Settings.Default.colEgenAvgiftDI = colEgenAvgift.DisplayIndex;
			Ortoped.Properties.Settings.Default.colEgenAvgiftWidth = colEgenAvgift.Width;
			Ortoped.Properties.Settings.Default.colFakDatDI = colFakDat.DisplayIndex;
			Ortoped.Properties.Settings.Default.colFakDatWidth = colFakDat.Width;
			Ortoped.Properties.Settings.Default.colFakNrDI = colFakNr.DisplayIndex;
			Ortoped.Properties.Settings.Default.colFakNrWidth = colFakNr.Width;
			Ortoped.Properties.Settings.Default.colhandlDI = colHandl.DisplayIndex;
			Ortoped.Properties.Settings.Default.colhandlWidth = colHandl.Width;
			Ortoped.Properties.Settings.Default.colLevtidDI = colLevtid.DisplayIndex;
			Ortoped.Properties.Settings.Default.colLevtidWidth = colLevtid.Width;
			Ortoped.Properties.Settings.Default.colProdStatusDI = colProdstatus.DisplayIndex;
			Ortoped.Properties.Settings.Default.colProdStatusWidth = colProdstatus.Width;
			Ortoped.Properties.Settings.Default.colAidRowsArtNo_DI = colAidRowsArtNo.DisplayIndex;
			Ortoped.Properties.Settings.Default.colAidRowsArtNr_Width = colAidRowsArtNo.Width;
			Ortoped.Properties.Settings.Default.colAidRowsBen_DI = colAidRowsBen.DisplayIndex;
			Ortoped.Properties.Settings.Default.colAidRowsBen_Width = colAidRowsBen.Width;
			Ortoped.Properties.Settings.Default.colAidRowsPcs_DI = colAidRowsPcs.DisplayIndex;
			Ortoped.Properties.Settings.Default.colAidRowsPcs_Width = colAidRowsPcs.Width;
			Ortoped.Properties.Settings.Default.Save();

			// To solve problem with unsaved AidTexts 
			if (txtONR.CanFocus)
				txtONR.Focus();
		}

		private void cboOrdinator_SelectionChangeCommitted(object sender, EventArgs e)
		{
			if (cboOrdinator.Focused && txtKlinik.Text.Trim().Equals(""))
			{
				txtKlinik.Text = oOH.getOrdinatorsCustomerNo(cboOrdinator.Text);
				txtKlinik_Leave(sender, e);
			}
		}

		private void menuItem10_Click(object sender, EventArgs e)
		{
			if (!oOR.isOrderOpen(frmControler.CurrentOH.OrderNo.Trim()))
			{
				MessageBox.Show(this, oOR.getErrorMsg(), "Kreditering är inte tillåtet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (lwOr.SelectedItems.Count > 0)
			{
				bool bStat = oOR.creditAid(frmControler.CurrentOH.OrderNo.Trim(), lwOr.SelectedItems[0].Text, OrderRowFunc.CreditType.OnlyPatient);
				updateAidList();
				if (bStat)
					MessageBox.Show(this, "Egenavgiften är nu krediterat", "Kreditering", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
				MessageBox.Show(this, "Välj ett hjälpmedel först", "Val saknas", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void menuItem11_Click(object sender, EventArgs e)
		{
			if (!oOR.isOrderOpen(frmControler.CurrentOH.OrderNo))
			{
				MessageBox.Show(this, oOR.getErrorMsg(), "Kreditering är inte tillåtet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (lwOr.SelectedItems.Count > 0)
			{
				bool bstat = oOR.creditAid(frmControler.CurrentOH.OrderNo, lwOr.SelectedItems[0].Text, OrderRowFunc.CreditType.OnlyInvoiceCustomer);
				updateAidList();
				if (bstat)
					MessageBox.Show(this, "Hjälpmedlet är nu krediterat", "Kreditering", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
				MessageBox.Show(this, "Välj ett hjälpmedel först", "Val saknas", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void menuItem13_Click(object sender, EventArgs e)
		{
			if (!oOR.isOrderOpen(frmControler.CurrentOH.OrderNo))
			{
				MessageBox.Show(this, oOR.getErrorMsg(), "Kreditering är inte tillåtet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (lwOr.SelectedItems.Count > 0)
			{
				bool bstat = oOR.creditAid(frmControler.CurrentOH.OrderNo, lwOr.SelectedItems[0].Text, OrderRowFunc.CreditType.Both);
				updateAidList();
				if (bstat)
					MessageBox.Show(this, "Hjälpmedlet är nu krediterat", "Kreditering", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
				MessageBox.Show(this, "Välj ett hjälpmedel först", "Val saknas", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void lwAidRows_DoubleClick(object sender, EventArgs e)
		{
			tabctrlRow.SelectedIndex = 2;
		}

		private void mnuSetViewStat_Click(object sender, EventArgs e)
		{
			int iSel = lwAidRows.SelectedItems[0].Index;

			ignoreItemCheckEvent = false;
			if (lwAidRows.SelectedItems[0].Checked)
				lwAidRows.SelectedItems[0].Checked = false;
			else	// Raden var inte bockad
			{
				try
				{
					for (int i = 0; i < lwAidRows.CheckedItems.Count; i++)
						lwAidRows.CheckedItems[i].Checked = false;
				}
				catch { }
				// Sätt rätt rad i databasen
				oOR.setAsViewInList(frmControler.CurrentOH.OrderNo, txtAidId.Text, lwAidRows.Items[iSel].Tag.ToString());
				chkViewState.Checked = oOR.getViewState(frmControler.CurrentOH.OrderNo, txtRDC.Text);
				lwAidRows.SelectedItems[0].Checked = true;
			}
			ignoreItemCheckEvent = true;
		}

		private void mnuReceipt_Click(object sender, EventArgs e)
		{
			CustomerFunc.Fakturakund fa;

			if (!saveOrder(false))
				return;

			if (lwOr.SelectedItems[0].SubItems[PRODSTATUS].Text.ToString().Equals("5"))
				return;

			if (txtFKN.Text.Equals(""))
				fa = oCust.getFakturakundByCust(txtKNR.Text);
			else
				fa = oCust.getFakturakundByCust(txtFKN.Text);

			string fs_nr = "";
			string[] s = null;

			if (!fa.JointInvoicing && !fa.Category.Equals("01"))
			{
				frmDiagReceipt oReceipt = new frmDiagReceipt(txtPNR.Text, txtSN.Text + " " + txtLN.Text);
				oReceipt.ShowDialog();

				// If user canceled
				if (oReceipt.sVal.Equals(""))
					return;

				// If receipt
				if (oReceipt.sVal.Equals("R"))
				{
					fs_nr = doDeliver(Config.BetVillkEaKont, false, false);
					s = Config.getReceipt().Split('-');
				}

				// If invoice
				if (oReceipt.sVal.Equals("I"))
				{
					fs_nr = doDeliver(Config.BetVillkEaFakt, false, false);
					s = Config.getInvoice().Split('-');
				}

				if (!fs_nr.Equals(""))
				{
					try
					{
						// Save address
						oOH.changeAddressOnFS(fs_nr, oReceipt.sName, oReceipt.sAddress1, oReceipt.sAddress2, oReceipt.sCity);
						oOH.printReceipt(s[0], s[1].Substring(0, 2), fs_nr);
					}
					catch (Exception ex)
					{
						Log4Net.Logger.loggError(ex, "Error when printing receipt, string from config.getReceipt : " + Config.getReceipt(), Config.User, "");
						MessageBox.Show(this, "Ett problem uppstod vid hämtning av kvittodokument", "Fel vid utskrift av Kvitto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
				oReceipt.Dispose();
			}
			else
				MessageBox.Show(this, "Antingen har fakturakunden samlingsfaktura eller är av kundkategori 01.\nKvitto är inte tillåtet under någon av dessa omständigheter", "Kvitto inte tillåtet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		private void btnSwitchPatient_Click(object sender, EventArgs e)
		{
            if (!saveOrder(false))
                return;

			if (!checkSignature())
				return;

			//Save last custno for previous function
			if (!GCF.noNULL(frmControler.CurrentPatient.PatientNo).Trim().Equals(""))
				frmControler.LastPatient = oCust.getPatientByCust(frmControler.CurrentPatient.PatientNo);

			clearAllPatientsFields();
			clearAllOHFields();
			clearAllOrderrows();
			clearAllAppointments();
			clearOrderrowDetailPane(true);
			txtPNR.Focus();
		}

		private void btnCloseOrder_Click(object sender, EventArgs e)
		{
			if (!saveOrder(false))
				return;

			if (btnCloseOrder.Text.Equals("&Stäng order"))
			{
				oOH.closeOrder(frmControler.CurrentOH.OrderNo);
				pnlOrderStat.Text = "### Ordern är stängd ###";
				btnCloseOrder.Text = "&Öppna order";
			}
			else if (btnCloseOrder.Text.Equals("&Öppna order"))
			{
				oOH.openOrder(frmControler.CurrentOH.OrderNo);
				pnlOrderStat.Text = "";
				btnCloseOrder.Text = "&Stäng order";
			}
		}

		private void btnDeleteOrder_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(this, "Vill du verkligen radera order " + frmControler.CurrentOH.OrderNo, "Radering av order", MessageBoxButtons.YesNo, MessageBoxIcon.Warning).Equals(DialogResult.Yes))
			{
				if (!frmControler.CurrentOH.OrderNo.Trim().Equals(""))
				{
					oOH.removeOrder(frmControler.CurrentOH.OrderNo);
					frmControler.CurrentOH = new OrderHeadDefinition();
				}
				updateForm(oCust.getPatientByCust(frmControler.CurrentPatient.PatientNo));
			}
		}

		private void btnNewOrder_Click(object sender, EventArgs e)
		{
			if (txtKNR.Text.Equals(""))
			{
				MessageBox.Show(this, "Du måste ange en patient innan order kan skapas", "Välj patient", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}

			if (chkDeceased.Checked)
			{
				if (MessageBox.Show(this, "Patienten är avliden, vill du verkligen registrera\nen ny order på denna patient?", "Patient avliden", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.No))
					return;
			}

			btnNewOrder.Focus();

			if (!checkSignature())
				return;

			clearAllOHFields();
			clearAllOrderrows();
			clearOrderrowDetailPane(true);

			OrderHeadDefinition o = oOH.addOH(txtKNR.Text);

			if (o != null)
				updateForm(o);
			else
				MessageBox.Show("Det gick inte att skapa en ny order", "Fel vid orderupplägg", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

			ignoreCheckSignature = true;
			txtKlinik.Focus();
			ignoreCheckSignature = false;
		}

		private void btnGoToCustomer_Click(object sender, EventArgs e)
		{
			if (lwOr.SelectedItems.Count > 0)
				setFormatClipBoardInfo(txtKNR.Text + ";" + frmControler.CurrentOH.OrderNo + "-" + lwOr.SelectedItems[0].Text + ";" + txtPNR.Text);
			else
				setFormatClipBoardInfo(txtKNR.Text + ";" + frmControler.CurrentOH.OrderNo + ";" + txtPNR.Text);
			oCust.showCustomerForm();
		}

		private void btnGoToAccounting_Click(object sender, EventArgs e)
		{
			if (lwOr.SelectedItems.Count > 0)
				setFormatClipBoardInfo(txtKNR.Text + ";" + frmControler.CurrentOH.OrderNo + "-" + lwOr.SelectedItems[0].Text + ";" + txtPNR.Text);
			else
				setFormatClipBoardInfo(txtKNR.Text + ";" + frmControler.CurrentOH.OrderNo + ";" + txtPNR.Text);
			oCust.showAccountForm();
		}

		private void btnGoToCalendar_Click(object sender, EventArgs e)
		{
			if (lwOr.SelectedItems.Count > 0)
				setFormatClipBoardInfo(txtKNR.Text + ";" + frmControler.CurrentOH.OrderNo + "-" + lwOr.SelectedItems[0].Text + ";" + txtPNR.Text);
			else
				setFormatClipBoardInfo(txtKNR.Text + ";" + frmControler.CurrentOH.OrderNo + ";" + txtPNR.Text);
			oErr.showCalendar();
		}

		private void mnuSendErrorReport_Click(object sender, EventArgs e)
		{
			frmErrorReport oER = new frmErrorReport(this.Handle);
			oER.ShowDialog();
		}

		private void mnuGetReferrals_Click(object sender, EventArgs e)
		{
			Thord.frmDiagThord oThord = new Thord.frmDiagThord();
			oThord.ShowDialog();
		}

		private void mnuOrderRow_Popup(object sender, EventArgs e)
		{
			if (lwOr.SelectedItems.Count > 0)
			{
				if (frmControler.CurrentOH.OrderNo.Trim().Equals(""))
				{
					foreach (MenuItem m in mnuOrderRow.MenuItems)
						m.Enabled = false;
					return;
				}
				else
				{
					foreach (MenuItem m in mnuOrderRow.MenuItems)
						m.Enabled = true;
				}

				try
				{
                    if (lwOr.SelectedItems[0].SubItems[PRODSTATUS].Text.Equals("5"))
					{
						mnuDeliver.Enabled = false;
						mnuOwnFee.Enabled = false;
						mnuReceipt.Enabled = false;
					}
					else
					{
						mnuDeliver.Enabled = true;
						mnuOwnFee.Enabled = true;
						mnuReceipt.Enabled = true;
					}
				}
				catch (Exception ex)
				{
					Log4Net.Logger.loggError(ex, "Error when rightclicking on lwOr", Config.User, "");
				}
			}
			else // No row selected
			{
				foreach (MenuItem m in mnuOrderRow.MenuItems)
				{
					if (m.Text.Equals("Nytt hjälpmedel") && !frmControler.CurrentOH.OrderNo.Trim().Equals(""))
						m.Enabled = true;
					else
						m.Enabled = false;
				}
			}
		}

		private void mnuOwnFee_Click(object sender, EventArgs e)
		{
			if (lwOr.SelectedItems.Count > 0)
			{
				frmOwnFee oOwnFee = new frmOwnFee(txtPNR.Text, txtKNR.Text, txtSN.Text + " " + txtLN.Text, frmControler.CurrentOH.OrderNo, lwOr.SelectedItems[0].Text, oOH.getCompanyId());
				oOwnFee.ShowDialog();
				updateAidList();
			}
			else
				MessageBox.Show(this, "Välj ett hjälpmedel först", "Val saknas", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void dtpGilltigFrom_ValueChanged(object sender, EventArgs e)
		{
			if (!GCF.noNULL(dtpGilltigFrom.Tag).ToString().Equals("-1"))
			{
				txtGilltigFrom.Text = dtpGilltigFrom.Value.ToString("yyMMdd");
				checkIfOrderHeadIsChanged(null, null);
			}
			dtpGilltigFrom.Tag = "";
		}

		private void dtpGilltigFrom_VisibleChanged(object sender, EventArgs e)
		{
			if (!GCF.noNULL(dtpGilltigFrom.Tag).ToString().Equals("-1"))
				txtGilltigFrom.Text = dtpGilltigFrom.Value.ToString("yyMMdd");
			dtpGilltigFrom.Tag = "";

		}

		private void txtGilltigFrom_Leave(object sender, EventArgs e)
		{
			try
			{
				dtpGilltigFrom.Tag = "-1";
				dtpGilltigFrom.Value = DateTime.ParseExact(txtGilltigFrom.Text, "yyMMdd", new CultureInfo("sv-SE"));

				checkIfOrderHeadIsChanged(null, null);
			}
			catch
			{
				txtGilltigFrom.Text = dtpGilltigFrom.Value.ToString("yyMMdd");
				MessageBox.Show("Fel format på datum, måste vara ÅÅMMDD");
			}
		}

		private void txtOrText_Enter(object sender, EventArgs e)
		{
			try
			{
				txtOrText.Select(txtOrText.TextLength, txtOrText.TextLength);
			}
			catch { }
		}

		private void txtAidText_Enter(object sender, EventArgs e)
		{
			try
			{
				txtAidText.Select(txtAidText.TextLength, txtAidText.TextLength);
			}
			catch { }
		}

		private void cboOrdinator_Leave(object sender, EventArgs e)
		{
			// This is to complement "ChabgeCommitted", with this we get an update even if user uses TAB
			if (cboOrdinator.Focused && txtKlinik.Text.Trim().Equals(""))
			{
				txtKlinik.Text = oOH.getOrdinatorsCustomerNo(cboOrdinator.Text);
				txtKlinik_Leave(sender, e);
			}

		}

		private void tabctrlRow_KeyDown(object sender, KeyEventArgs e)
		{
			//MessageBox.Show(e.KeyValue.ToString());
			// On CTRL + DEL
			if (e.Control && e.KeyValue == 68)
			{
				tabctrlRow.SelectedIndex = 0;
				mnuReceipt_Click(sender, e);
			}
		}

		private void btnThord_Click(object sender, EventArgs e)
		{
			if (!checkSignature())
				return;

			bwThord.RunWorkerAsync();

			frmProgress frmProg = new frmProgress("Läser remisser hämtade från Thord...", ref bwThord);
			frmProg.ShowDialog();

			if (bwThord.IsBusy)
				bwThord.CancelAsync();
		}

		private void lwOr_DoubleClick(object sender, EventArgs e)
		{
			ignoreDetailEvents = true;
			tabctrlRow.SelectedIndex = 1;
			ignoreDetailEvents = false;
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			e.Result = OrderHeadDefinition.convertToThordListView(oOH.getAllOHWithOrderType(Config.getNumberSerie(), false));
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			Ortoped.Dialogs.frmDiagThord oOrderHuvud = new Ortoped.Dialogs.frmDiagThord((ListViewItem[])e.Result, true);
			oOrderHuvud.iSortCol = 8;

			oOrderHuvud.ShowDialog();
			if (oOrderHuvud.selOnr != "")
			{
				clearAllOHFields();
				clearAllAppointments();
				clearAllOrderrows();
				clearAllPatientsFields();
				clearOrderrowDetailPane(true);
				OrderHeadDefinition oh = oOH.getOrder(oOrderHuvud.selOnr);
				updateForm(oh);
			}
			oOrderHuvud.Dispose();
		}

		#endregion


		#region ============================================ DETAILS_EVENTS =======================================================

		private void cboHandler_SelectedValueChanged(object sender, System.EventArgs e)
		{
			if (cboHandler.Focused && !txtANR.Text.Trim().Equals("") && !ignoreDetailEvents)
			{
				valueChangedOR(sender, e);
				if (!oOR.saveOrderRow(fillOrderRow(sender, false), true, false))
					MessageBox.Show(this, oOR.getErrorMsg(), "Uppdatering är inte tillåtet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void cboProdStatus_SelectedValueChanged(object sender, System.EventArgs e)
		{
			if (cboProdStatus.Focused && !txtANR.Text.Trim().Equals("") && !ignoreDetailEvents)
			{
				if (!oOR.saveOrderRow(fillOrderRow(sender, false), true, false))
					MessageBox.Show(this, oOR.getErrorMsg(), "Uppdatering är inte tillåtet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void chkGaranti_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chkGaranti.Focused && !txtANR.Text.Trim().Equals("") && !ignoreDetailEvents)
			{
				if (!oOR.saveOrderRow(fillOrderRow(sender, false), true, false))
					MessageBox.Show(this, oOR.getErrorMsg(), "Uppdatering är inte tillåtet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void cboLevsatt_SelectedValueChanged(object sender, EventArgs e)
		{
			if (cboAidPriority.Focused && !txtANR.Text.Trim().Equals("") && !ignoreDetailEvents)
			{
				if (!oOR.saveOrderRow(fillOrderRow(sender, false), true, false))
					MessageBox.Show(this, oOR.getErrorMsg(), "Uppdatering är inte tillåtet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void cboNeedStep_SelectedValueChanged(object sender, EventArgs e)
		{
			if (cboNeedStep.Focused && !txtANR.Text.Trim().Equals("") && !ignoreDetailEvents)
			{
				valueChangedOR(sender, e);
				if (!oOR.saveOrderRow(fillOrderRow(sender, false), true, false))
					MessageBox.Show(this, oOR.getErrorMsg(), "Uppdatering är inte tillåtet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void chkFirstTimePatient_CheckedChanged(object sender, EventArgs e)
		{
			if (chkFirstTimePatient.Focused && !txtANR.Text.Trim().Equals("") && !ignoreDetailEvents)
			{
				valueChangedOR(sender, e);
				if (!oOR.saveOrderRow(fillOrderRow(sender, false), true, false))
					MessageBox.Show(this, oOR.getErrorMsg(), "Uppdatering är inte tillåtet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			if (chkDeceased.Checked)
			{
				if (MessageBox.Show(this, "Patienten är avliden, vill du verkligen registrera\nen ny artikel på denna patient?", "Patient avliden", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.No))
					return;
			}

			// Kontrollera om ordern är öppen
			if (!oOR.isOrderOpen(frmControler.CurrentOH.OrderNo))
			{
				MessageBox.Show(this, oOR.getErrorMsg(), "Nyupplägg är inte tillåtet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			// Om inte hjälpmedel finns läggs detta upp
			if (txtAidId.Text.Trim().Equals(""))
				mnuNewAid_Click(sender, e);
			else
				txtRDC.Text = oOR.addNewRow(frmControler.CurrentOH.OrderNo, txtAidId.Text, "");

			txtANR.Enabled = true;
			txtANR.Text = "";
			txtORA.Text = "";
			txtPRI.Text = "";
			txtBEN.Text = "";
			labORA.Text = "Antal";
			chkViewState.Checked = false;
			txtANR.Focus();
		}

		private void txtANR_Enter(object sender, System.EventArgs e)
		{
			txtANR.Tag = txtANR.Text;
		}

		private void txtORA_Enter(object sender, System.EventArgs e)
		{
			txtORA.Tag = txtORA.Text;
			if (txtORA.Text.Equals(""))
				txtORA.Text = "0";
			txtORA.SelectAll();
		}

		private void txtPRI_Enter(object sender, System.EventArgs e)
		{
			txtPRI.Tag = txtPRI.Text;
		}

		private void txtANR_Leave(object sender, System.EventArgs e)
		{
			// Om fältet ändrats och inte är blankt och RDC är ifyllt
			if ((txtANR.Tag.ToString() != txtANR.Text) && (!txtANR.Text.Trim().Equals("")))
			{
				// Sök på namn eller artikelnummer
				if (txtANR.Text.StartsWith("."))
				{
					// Om ingen product hittades
					if (!checkProduct(oOR.findProductByName(txtANR.Text.Substring(1))))
					{
						txtANR.Focus();
						txtANR.Text = txtPNR.Tag.ToString();
						txtANR.SelectAll();
					}
					else
					{
						// Spara om ordern är öppen
						if (!oOR.saveOrderRow(fillOrderRow(sender, false), true, false))
							MessageBox.Show(this, oOR.getErrorMsg(), "Uppdatering är inte tillåtet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						else
						{
							// Uppdatera apris med värde från orderrad
							OrderRowDefinitions.OrderRow or = oOR.getRow(frmControler.CurrentOH.OrderNo, txtRDC.Text);
							txtPRI.Text = or.APris;
							labORA.Text = "Antal ( " + or.Enhet + " )";
							txtANR.Enabled = false;
						}

					}
				}
				else // Söker på artikelnummer
				{
					if (!checkProduct(oOR.findProductByID(txtANR.Text)))
					{
						txtANR.Focus();
						txtANR.Text = txtANR.Tag.ToString();
						txtANR.SelectAll();
					}
					else
					{
						// Spara om ordern är öppen
						if (!oOR.saveOrderRow(fillOrderRow(sender, false), true, false))
							MessageBox.Show(this, oOR.getErrorMsg(), "Uppdatering är inte tillåtet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						else
						{
							// Uppdatera apris med värde från orderrad
							OrderRowDefinitions.OrderRow or = oOR.getRow(frmControler.CurrentOH.OrderNo, txtRDC.Text);
							labORA.Text = "Antal ( " + or.Enhet + " )";
							txtPRI.Text = or.APris;
							txtANR.Enabled = false;
						}
					}
				}
			}
			else if (!txtANR.Text.Trim().Equals(""))
			{
				txtANR.Text = txtANR.Tag.ToString();
				if(!txtANR.Text.Equals(""))
                    txtANR.Enabled = false;
			}
		}

		private void updateOrderRowDetailPane(Definitions.Aid aid, bool updatelistview)
		{
            OrderRowDefinitions.OrderRow or = new OrderRowDefinitions.OrderRow();

            if (updatelistview)
            {
                clearOrderrowDetailPane(false);
                or = aid.Product;
            }
            else
            {
                string row = lwAidRows.SelectedItems[0].Tag.ToString();

                foreach (OrderRowDefinitions.OrderRow o in aid.OrderRows)
                {
                    if (o.Rad.Equals(row))
                    {
                        or = o;
                        break;
                    }
                }

                if (or.OrderNo == null)
                {
                    or = aid.Product;
                    MessageBox.Show("Den markerade raden hittades inte, hjälpmedlet sätt som aktiv rad istället");
                }
            }

			// Abort if no orderrow was found
			if (GCF.noNULL(aid.OrderNo).Equals(""))
				return;

			cboHandler.Text = or.SelectedHandler;

            labSumInternal.Text = aid.SumInternalProducts.ToString() + ":-";
            labSumExternal.Text = aid.SumExternalProducts.ToString() + ":-";

			// Hide combobox if selected handler is not found (and selected handler is not "")
			if ((cboHandler.FindStringExact(or.SelectedHandler.Trim()) == -1) && !or.SelectedHandler.Trim().Equals(""))
			{
				cboHandler.Visible = false;
				txtHandler.Visible = true;
				cboHandler.SelectedIndex = 0; // clear text
				txtHandler.Text = or.SelectedHandler;
			}
			else
			{
				cboHandler.Visible = true;
				txtHandler.Visible = false;
				txtHandler.Text = ""; // clear text
				cboHandler.SelectedIndex = cboHandler.FindStringExact(or.SelectedHandler);
			}

			txtAidId.Text = or.AidNr;
            txtOR_ONR.Text = or.OrderNo;
			txtRDC.Text = or.Rad;
			cboProdStatus.Text = "";
			cboProdStatus.Text = or.Prodstatus;
			txtOrDatum.Text = or.AidDate;
			txtLevDate.Text = or.LevTid;
			txtAidOid.Text = or.AidOid.ToString();
			txtPartOid.Text = or.PartOid.ToString();
			chkFirstTimePatient.Checked = or.FirstTimePatient;

			if (or.Levstatus.Equals("5"))
				txtLevDate.Enabled = false;
			else
				txtLevDate.Enabled = true;

			txtANR.Text = or.Artikel;
			txtBEN.Text = or.ProductName;
			txtORA.Text = or.Antal;
			txtPRI.Text = or.APris;
			labORA.Text = "Antal ( " + or.Enhet.Trim() + " )";
			//			cboHandler.Text = or.SelectedHandler;
			txtOrText.Text = or.Text;
			setBestallSelektion(or.InkStat);
			chkViewState.Checked = or.ViewInList;
			chkGaranti.Checked = or.Warrenty;
			cboAidPriority.Text = oDelM.getNameByKey(or.AidPriority);
			cboNeedStep.SelectedIndex = cboNeedStep.FindString(or.Thord_NeedStep);
			chkUpdatedInThord.Checked = or.CreatedInThord;
			txtProductionTitle.Text = or.ProductionTitle;
			chkUrgent.Checked = or.Urgent;
            //dtpPromisedDeliverDate.Value = or.PromisedDeliverDate.Value;
            //dtpConditionDate.Value = or.ConditionDate.Value;
            txtConditionDate.Text = or.ConditionDate.HasValue ? or.ConditionDate.Value.ToString("yyMMdd") : "";
            txtPromisedDeliverDate.Text = or.PromisedDeliverDate.HasValue ? or.PromisedDeliverDate.Value.ToString("yyMMdd") : "";
            txtHolder.Text = or.Holder;

			grbArtList.Enabled = true;

			if (or.Levstatus.Equals("0"))
			{
				grbAid.Enabled = true;
				grbArt.Enabled = true;
			}
			else
			{
				grbAid.Enabled = false;
				grbArt.Enabled = false;
			}

			if (updatelistview)
			{
				//ignoreItemCheckEvent = true;
				lwAidRows.Items.Clear();
				lwAidRows.Items.AddRange(OrderRowDefinitions.OrderRow.convertToSmallListView(oOR.getAid(or.OrderNo, or.AidNr, false).OrderRows.ToArray()));
				this.lwAidRows.ListViewItemSorter = new ListViewItemComparer(3);
				//ignoreItemCheckEvent = false;
				try
				{
					ListViewItem[] slw = lwAidRows.Items.Find(or.Rad, true);
					int i = slw[0].Index;
					lwAidRows.Items[i].Selected = true;
				}
				catch (Exception e)
				{
					Log4Net.Logger.loggError(e, "", Config.User, "");
				}
			}

			// If row is not createded inThord, but should be, set it as changed to trigg saving pricedure
			if (!or.CreatedInThord && !GCF.noNULL(or.RemissNo).Equals(""))
			{
				valueNotChangedOR(null, null);
			}

			//bORChanged = false;
			//pnlORSaved.Text = "Orderrad ändrad: NEJ";
		}

		private void lwAidRows_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			bool bLocalChanged = bORChanged;
            
			if ((lwAidRows.SelectedItems.Count > 0) && lwAidRows.Focused)
			{
				// Spara om ordern är öppen
				if (!oOR.saveOrderRow(fillOrderRow("", false), true, false))
					MessageBox.Show(this, oOR.getErrorMsg(), "Uppdatering är inte tillåtet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				else
					updateOrderRowDetailPane(oOR.getAid(frmControler.CurrentOH.OrderNo, txtAidId.Text, false), false);

				// Check for saved value befor switching orderrow, we don't want the
				// changeflag to be raised because we only switched row
				if (bLocalChanged)
					valueChangedOR(sender, e);
				else
					valueNotChangedOR(sender, e);
			}
		}

		private void txtORA_Leave(object sender, System.EventArgs e)
		{
            double amount = 0;

            if (!txtORA.Focused || ignoreDetailEvents)
                return;

			try
			{
                if (!Double.TryParse(txtORA.Text.Trim().Replace('.', ','), out amount))
                {
                    MessageBox.Show(this, "Angivet antal har ett felaktigt format, endast numeriska värden är tillåtna", "Felaktigt format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtORA.Text = "1";
                    txtORA.Focus();
                    return;
                }

				if (amount == 0)
				{
					MessageBox.Show(this, "Antal måste vara större än noll. Antalet kommer att sättas till 1", "Noll i antal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtORA.Text = "1";
				}

				if (txtORA.Text.Trim() != txtORA.Tag.ToString() && (!txtORA.Text.Trim().Equals("")))
				{
					if (!oOR.saveOrderRow(fillOrderRow(sender, false), true, false))
						MessageBox.Show(this, oOR.getErrorMsg(), "Uppdatering är inte tillåtet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					else
					{
						// Uppdatera apris med värde från orderrad
						OrderRowDefinitions.OrderRow or = oOR.getRow(frmControler.CurrentOH.OrderNo, txtRDC.Text);
                        Definitions.Aid aid = oOR.getAid(txtOR_ONR.Text, txtAidId.Text, false);
                        ignoreItemCheckEvent = false;
                        
                        labSumExternal.Text = aid.SumExternalProducts.ToString() + ":-";
                        labSumInternal.Text = aid.SumInternalProducts.ToString() + ":-";

						txtPRI.Text = or.APris;
						lwAidRows.Items.Clear();
						lwAidRows.Items.AddRange(OrderRowDefinitions.OrderRow.convertToSmallListView(oOR.getAid(frmControler.CurrentOH.OrderNo, txtAidId.Text, false).OrderRows.ToArray()));
						this.lwAidRows.ListViewItemSorter = new ListViewItemComparer(3);

						if (lwAidRows.Items.Count == 1)
						{
							lwAidRows.Items[0].Checked = true;
							chkViewState.Checked = true;
						}
						ignoreItemCheckEvent = true;
					}
				}
			}
			catch
			{
				MessageBox.Show(this, "Angivet antal har ett felaktigt format, endast numeriska värden är tillåtna", "Felaktigt format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtORA.Text = "1";
				txtORA.Focus();
			}
		}

		private void txtPRI_Leave(object sender, System.EventArgs e)
		{
			try
			{
                if (ignoreDetailEvents)
                    return;

				// Just to test if Pris is numeric
				double d = Double.Parse(txtPRI.Text.Replace(".", ","));

				if (txtPRI.Text.Trim() != txtPRI.Tag.ToString() && (!txtRDC.Text.Trim().Equals("")))
				{
					if (!oOR.saveOrderRow(fillOrderRow(sender, true), true, false))
						MessageBox.Show(this, oOR.getErrorMsg(), "Uppdatering är inte tillåtet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					else
					{
                        Definitions.Aid aid = oOR.getAid(txtOR_ONR.Text, txtAidId.Text, false);

                        ignoreItemCheckEvent = false;

                        labSumExternal.Text = aid.SumExternalProducts.ToString() + ":-";
                        labSumInternal.Text = aid.SumInternalProducts.ToString() + ":-";

                        lwAidRows.Items.Clear();
						lwAidRows.Items.AddRange(OrderRowDefinitions.OrderRow.convertToSmallListView(oOR.getAid(frmControler.CurrentOH.OrderNo, txtAidId.Text, false).OrderRows.ToArray()));
						this.lwAidRows.ListViewItemSorter = new ListViewItemComparer(3);
                        
						if (lwAidRows.Items.Count == 1)
						{
							lwAidRows.Items[0].Checked = true;
							chkViewState.Checked = true;
						}
						ignoreItemCheckEvent = true;
					}
				}
			}
			catch
			{
				MessageBox.Show(this, "Angivet pris har ett felaktigt format, endast numeriska värden är tillåtna", "Felaktigt format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtPRI.Focus();
			}
		}

		private void lwAidRows_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
		{
			if (ignoreItemCheckEvent)
				e.NewValue = e.CurrentValue;
		}

		private string getBestallSelektion()
		{
			if (rdBestalld.Checked)
			{
				return "5";
			}
			else if (rdBestall.Checked)
			{
				return "1";
			}
			else if (rdBestallEj.Checked)
			{
				return "0";
			}
			else
				return "0";
		}

		private void setBestallSelektion(string stat)
		{
			try
			{
				if (stat.Equals("5"))
				{
					rdBestalld.Checked = true;
				}
				else if (stat.Equals("1"))
				{
					rdBestall.Checked = true;
				}
				else if (stat.Equals("0"))
				{
					rdBestallEj.Checked = true;
				}
			}
			catch { }
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			// Kontrollera om ordern är öppen
			if (!oOR.isOrderOpen(frmControler.CurrentOH.OrderNo))
			{
				MessageBox.Show(this, oOR.getErrorMsg(), "Radering är inte tillåtet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			try
			{
				oOR.removeRow(frmControler.CurrentOH.OrderNo, txtRDC.Text);
				if (lwOr.SelectedItems.Count > 0)
                    updateOrderRowDetailPane(oOR.getAid(frmControler.CurrentOH.OrderNo, lwOr.SelectedItems[0].SubItems[AIDNR].Text, false), true);
				else
					clearOrderrowDetailPane(true);
			}
			catch { }
		}

		private void valueNotChangedOR(object sender, EventArgs e)
		{
			bORChanged = false;
			pnlORSaved.Text = "Orderrad ändrad: NEJ";
		}

		private void valueChangedOR(object sender, EventArgs e)
		{
			bORChanged = true;
			pnlORSaved.Text = "Orderrad ändrad: JA";
		}

		private void txtAidText_Leave(object sender, EventArgs e)
		{
			if (lwOr.SelectedItems.Count > 0)
				oOR.saveAidsTexts(frmControler.CurrentOH.OrderNo, lwOr.SelectedItems[0].Text, txtAidText.Text);
		}

		#endregion

		private void mnuQuit_Click(object sender, EventArgs e)
		{
			if (!checkSignature())
				return;

			this.Close();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			if (!checkSignature())
				return;

			this.Close();
		}

		private void bwGetOrdinators_DoWork(object sender, DoWorkEventArgs e)
		{
			oOH.getAllOrdinators(true);
		}

		private void bwGetOrdinators_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			cboOrdinator.Enabled = true;
		}

		private void checkIfOrderHeadIsChanged(object sender, EventArgs e)
		{
			OrderHeadDefinition oNew;

			// Get the current orderhead
			if (frmControler.CurrentOH != null)
			{
				oNew = fillOH(new OrderHeadDefinition(frmControler.CurrentOH));
				if (frmControler.isOrderHeadChanged(frmControler.CurrentOH, oNew))
					grbOH.ForeColor = Color.SteelBlue;
				else
					grbOH.ForeColor = grbPatient.ForeColor;
			}
			else
				grbOH.ForeColor = grbPatient.ForeColor;
		}

		private void btnSaveOH_Click(object sender, EventArgs e)
		{
			saveOrder(false);
		}

		private void skickaLoggfilToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmErrorReport err = new frmErrorReport(this.Handle);

			err.ShowDialog();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			Log4Net.Logger.loggInfo("timer1 fired", "", " timer1_Tick");
			setOrder(mSendMessage);
			timer1.Stop();
		}

		private void toolStripMenuItem3_Click(object sender, EventArgs e)
		{
			frmAbout oAbout = new frmAbout();
			oAbout.ShowDialog();
		}

		private void btnIO_Click(object sender, EventArgs e)
		{
			PurchaseDefenitions[] p = oOH.getAllPurchaseOrders(frmControler.CurrentOH.OrderNo);

			frmDiagIO oIO = new frmDiagIO(p);
			oIO.ShowDialog();
		}

		private void ordernummerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			txtONR.Focus();
		}

		private void personnummerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			txtPNR.Focus();
		}

		private void btnCopDoc1_Click(object sender, EventArgs e)
		{
			Config.CopDoc cd = Config.getCopDoc[0];
			GarpGenericDB garp;
			Process copdoc = new Process();

			if (cd.Table.Equals("OGA"))
			{
				garp = new GarpGenericDB(cd.Table);

				// If we found the post searched for
				if (garp.find(txtONR.Text))
				{
					cd.Arguments = cd.Arguments.Replace("{" + cd.Table + ";", "");
					cd.Arguments = cd.Arguments.Replace(cd.Field + "}", garp.getValue(cd.Field));

					copdoc.StartInfo.FileName = cd.Path;
					copdoc.StartInfo.Arguments = cd.Arguments;
					try
					{
						Log4Net.Logger.loggInfo("CopDoc started with following string: " + copdoc.StartInfo.FileName + " " + copdoc.StartInfo.Arguments, Config.User, "frmMain.btnCopDoc1_Click()");
						copdoc.Start();
					}
					catch (Exception ex)
					{
						Log4Net.Logger.loggError(ex, "Error when starting CopDoc Path:" + cd.Path + " Arguments:" + cd.Arguments, Config.User, "frmMain.btnCopDoc1_Click");
						MessageBox.Show(this, "Det gick inte att starta CopDoc", "CopDoc", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				else
				{
					MessageBox.Show(this, "Hittade inte den eftersökta ordern i Garp", "Order hittades ej", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			else if (cd.Table.Equals("KA"))
			{
				garp = new GarpGenericDB(cd.Table);

				// If we found the post searched for
				if (garp.find(txtKNR.Text))
				{
					cd.Arguments = cd.Arguments.Replace("{" + cd.Table + ";", "");
					cd.Arguments = cd.Arguments.Replace(cd.Field + "}", garp.getValue(cd.Field));

					copdoc.StartInfo.FileName = cd.Path;
					copdoc.StartInfo.Arguments = cd.Arguments;
					try
					{
						Log4Net.Logger.loggInfo("CopDoc started with following string: " + copdoc.StartInfo.FileName + " " + copdoc.StartInfo.Arguments, Config.User, "frmMain.btnCopDoc1_Click()");
						copdoc.Start();
					}
					catch (Exception ex)
					{
						Log4Net.Logger.loggError(ex, "Error when starting CopDoc Path:" + cd.Path + " Arguments:" + cd.Arguments, Config.User, "frmMain.btnCopDoc1_Click");
						MessageBox.Show(this, "Det gick inte att starta CopDoc", "CopDoc", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				else
				{
					MessageBox.Show(this, "Hittade inte den eftersökta patienten i Garp", "Patient hittades ej", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}

			}

		}

		private void btnCopDoc2_Click(object sender, EventArgs e)
		{
			Config.CopDoc cd = Config.getCopDoc[1];
			GarpGenericDB garp;
			Process copdoc = new Process();

			if (cd.Table.Equals("OGA"))
			{
				garp = new GarpGenericDB(cd.Table);

				// If we found the post searched for
				if (garp.find(txtONR.Text))
				{
					cd.Arguments = cd.Arguments.Replace("{" + cd.Table + ";", "");
					cd.Arguments = cd.Arguments.Replace(cd.Field + "}", "\"" + garp.getValue(cd.Field) + "\"");

					copdoc.StartInfo.FileName = cd.Path;
					copdoc.StartInfo.Arguments = cd.Arguments;
					try
					{
						Log4Net.Logger.loggInfo("CopDoc started with following string: " + copdoc.StartInfo.FileName + " " + copdoc.StartInfo.Arguments, Config.User, "frmMain.btnCopDoc1_Click()");
						copdoc.Start();
					}
					catch (Exception ex)
					{
						Log4Net.Logger.loggError(ex, "Error when starting CopDoc Path:" + cd.Path + " Arguments:" + cd.Arguments, Config.User, "frmMain.btnCopDoc1_Click");
						MessageBox.Show(this, "Det gick inte att starta CopDoc", "CopDoc", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				else
				{
					MessageBox.Show(this, "Hittade inte den eftersökta patienten i Garp", "Patient hittades ej", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			else if (cd.Table.Equals("KA"))
			{
				garp = new GarpGenericDB(cd.Table);

				// If we found the post searched for
				if (garp.find(txtKNR.Text))
				{
					cd.Arguments = cd.Arguments.Replace("{" + cd.Table + ";", "");
					cd.Arguments = cd.Arguments.Replace(cd.Field + "}", garp.getValue(cd.Field));

					copdoc.StartInfo.FileName = cd.Path;
					copdoc.StartInfo.Arguments = cd.Arguments;
					try
					{
						copdoc.Start();
					}
					catch (Exception ex)
					{
						Log4Net.Logger.loggError(ex, "Error when starting CopDoc Path:" + cd.Path + " Arguments:" + cd.Arguments, Config.User, "frmMain.btnCopDoc1_Click");
						MessageBox.Show(this, "Det gick inte att starta CopDoc", "CopDoc", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				else
				{
					MessageBox.Show(this, "Hittade inte den eftersökta patienten i Garp", "Patient hittades ej", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}

			}
		}

		private void txtONR_Enter(object sender, EventArgs e)
		{
		}

		private void txtTelSMS_Leave(object sender, EventArgs e)
		{
			if (frmControler.CurrentPatient != null)
				frmControler.CurrentPatient.TelSMS = txtTelSMS.Text;

			oCust.updateCustomer(frmControler.CurrentPatient);
		}

		private void txtEndDate_TextChanged(object sender, EventArgs e)
		{

		}

		private void chkUrgent_CheckedChanged(object sender, EventArgs e)
		{
			if (chkUrgent.Focused && !ignoreDetailEvents)
			{
				valueChangedOR(sender, e);
				if (!oOR.saveOrderRow(fillOrderRow(sender, false), true, false))
					MessageBox.Show(this, oOR.getErrorMsg(), "Uppdatering är inte tillåtet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void txtProductionTitle_Leave(object sender, EventArgs e)
		{
			if (!ignoreDetailEvents)
			{
				valueChangedOR(sender, e);
				if (!oOR.saveOrderRow(fillOrderRow(sender, false), true, false))
					MessageBox.Show(this, oOR.getErrorMsg(), "Uppdatering är inte tillåtet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void dtpPromisedDeliverDate_ValueChanged(object sender, EventArgs e)
		{
			if (dtpPromisedDeliverDate.Focused && !txtANR.Text.Trim().Equals("") && !ignoreDetailEvents)
			{
                txtPromisedDeliverDate.Text = dtpPromisedDeliverDate.Value.ToString("yyMMdd");
				valueChangedOR(sender, e);
				if (!oOR.saveOrderRow(fillOrderRow(sender, false), true, false))
					MessageBox.Show(this, oOR.getErrorMsg(), "Uppdatering är inte tillåtet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

        private void dtpConditionDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpConditionDate.Focused && !txtANR.Text.Trim().Equals("") && !ignoreDetailEvents)
            {
                txtConditionDate.Text = dtpConditionDate.Value.ToString("yyMMdd");
                valueChangedOR(sender, e);
                if (!oOR.saveOrderRow(fillOrderRow(sender, false), true, false))
                    MessageBox.Show(this, oOR.getErrorMsg(), "Uppdatering är inte tillåtet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnStartProdView_Click(object sender, EventArgs e)
        {
            try
            {
                string s = ConfigurationManager.AppSettings["Production"].ToString();
 
                if(File.Exists(s))
                    Process.Start(s, txtONR.Text);
            }
            catch (Exception ex)
            {
                Log4Net.Logger.loggError(ex, "Error while opening ProduktionOverview", "", "");
            }

        }

        private void lwOr_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lwOr.SelectedItems.Count > 0)
                    txtAidText.Text = lwOr.SelectedItems[0].SubItems[AIDTEXT].Text;
            }
            catch { }
        }

        private void chkDeceased_MouseUp(object sender, MouseEventArgs e)
        {
            oCust.setDeceased(txtKNR.Text, chkDeceased.Checked);
        }

        private void btnSMS_Click(object sender, EventArgs e)
        {
            frmSendSms sms = new frmSendSms(frmControler.CurrentPatient, Config.CompanyId);
            sms.ShowDialog();
        }

        private void pnlBottom_Paint(object sender, PaintEventArgs e)
        {

        }

        private void visaMaterialplaneringenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setFormatClipBoardInfo(txtKNR.Text + ";" + frmControler.CurrentOH.OrderNo + ";" + txtPNR.Text);
            oOR.openMatPlan();
        }

        private void dtOTADate_ValueChanged(object sender, EventArgs e)
        {
            if (!GCF.noNULL(dtOTADate.Tag).ToString().Equals("-1"))
            {
                txtOTADate.Text = dtOTADate.Value.ToString("yyMMdd");
                checkIfOrderHeadIsChanged(null, null);
            }
            dtOTADate.Tag = "";

        }

        private void txtOTADate_Leave(object sender, EventArgs e)
        {
            try
            {
                dtOTADate.Tag = "-1";
                dtOTADate.Value = DateTime.ParseExact(txtOTADate.Text, "yyMMdd", new CultureInfo("sv-SE"));

                checkIfOrderHeadIsChanged(null, null);
            }
            catch
            {
                txtOTADate.Text = dtOTADate.Value.ToString("yyMMdd");
                MessageBox.Show("Fel format på datum, måste vara ÅÅMMDD");
            }
        }

        private void dtOTADate_VisibleChanged(object sender, EventArgs e)
        {
            if (!GCF.noNULL(dtOTADate.Tag).ToString().Equals("-1"))
                txtOTADate.Text = dtOTADate.Value.ToString("yyMMdd");
            dtOTADate.Tag = "";

        }

        private void txtConditionDate_Leave(object sender, EventArgs e)
        {
            if (txtConditionDate.Focused && !txtANR.Text.Trim().Equals("") && !ignoreDetailEvents)
            {
                valueChangedOR(sender, e);
                if (!oOR.saveOrderRow(fillOrderRow(sender, false), true, false))
                    MessageBox.Show(this, oOR.getErrorMsg(), "Uppdatering är inte tillåtet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtPromisedDeliverDate_Leave(object sender, EventArgs e)
        {
            if (txtPromisedDeliverDate.Focused && !txtANR.Text.Trim().Equals("") && !ignoreDetailEvents)
            {
                valueChangedOR(sender, e);
                if (!oOR.saveOrderRow(fillOrderRow(sender, false), true, false))
                    MessageBox.Show(this, oOR.getErrorMsg(), "Uppdatering är inte tillåtet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
 
        }

	}
}


