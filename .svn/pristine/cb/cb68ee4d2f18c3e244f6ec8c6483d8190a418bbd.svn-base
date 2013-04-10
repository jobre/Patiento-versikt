using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ortoped.HelpClasses;
using GCS;
using GCS_Process;
using System.IO;
using System.Diagnostics;
using Log4Net;

namespace Ortoped
{
    public partial class frmErrorReport : Form
    {
        private IntPtr mHandle;

        public frmErrorReport(System.IntPtr handle)
        {
            InitializeComponent();
            mHandle = handle;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Mail mail = new Mail();
            string info = "";
            ArrayList al = new ArrayList();
            Log4Net.Logger.loggInfo("Sending support mail", Config.User, "");

            try
            {
                info = "\n\n\nMachinename: " + Environment.MachineName;
                info += "\nOS: " + Environment.OSVersion;
                info += "\nUser: " + Environment.UserName;
                info += "\nDoamin: " + Environment.UserDomainName;
                info += "\nConfigPath " + Config.ConfigPath;
                info += "\n\nStackTrace: " + Environment.StackTrace;

                ScreenCapture sc = new ScreenCapture();
                string imgWholeScreen = Path.GetTempPath() + "Screen_" + Path.ChangeExtension(Path.GetRandomFileName(), ".jpg");
                al.Add(imgWholeScreen);

                string imgPatient = Path.GetTempPath() + "Patient_" + Path.ChangeExtension(Path.GetRandomFileName(), ".jpg");
                al.Add(imgPatient);

                //string logPath = Path.GetTempPath() + "log_" + Path.ChangeExtension(Path.GetRandomFileName(), ".gcslogg");
                //File.Copy(Log4Net.Logger.getFileName(Config.User), logPath, true);
                //al.Add(logPath);

                //string confPath = Path.GetTempPath() + "config_" + Path.ChangeExtension(Path.GetRandomFileName(), ".gcslogg");
                //File.Copy(Config., confPath, true);
                //al.Add(confPath);

                sc.CaptureScreenToFile(imgWholeScreen, System.Drawing.Imaging.ImageFormat.Jpeg);
                sc.CaptureWindowToFile(mHandle, imgPatient, System.Drawing.Imaging.ImageFormat.Jpeg);

                mail.sendMail(Config.SMTP, 25, Config.SMTP_FromAddress, Config.SMTP_SupportAddress, "ErrorMessage from " + Config.User, rtMessage.Text + info, (string[])al.ToArray(typeof(string)), Config.SMTP_User, Config.SMTP_Password);

                this.Close();
            }
            catch (Exception ex)
            {
                Log4Net.Logger.loggError(ex, "Error while configuring mail to support", Config.User, "frmErrorReport.btnSend_Click");
            }
        }

        private void frmErrorReport_Load(object sender, EventArgs e)
        {
            //toolStripStatusLabel1.ToolTipText = Log4Net.Logger.getFileName(Config.User);
            toolStripStatusLabel2.ToolTipText = Config.ConfigPath;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnShowLoggFile_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                //System.Diagnostics.Process.Start("notepad.exe", Log4Net.Logger.getFileName(Config.User));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripStatusLabel2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("iexplore.exe", Config.ConfigPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}