using System;
//using System.Windows.Forms;
using System.Runtime.InteropServices;
//using System.Text;
using System.Diagnostics;
//using System.Threading;
//using System.Reflection;
//using System.IO;
using GCS;
using System.Threading;

namespace SingleInstance
{
    /// <summary>
    /// Summary description for SingleApp.
    /// </summary>
    public class SingleApplication
    {
        //    //private static string s = "";
        //    static Mutex mutex = new Mutex(true, "{8F6F0AC4-B9A1-45fd-A8CF-72F04E6BDE8F}");

        //    public SingleApplication()
        //    {

        //    }
        //    /// <summary>
        //    /// Imports 
        //    /// </summary>

        //    [DllImport("user32.dll")]
        //    private static extern int ShowWindow(IntPtr hWnd, int nCmdShow);

        //    [DllImport("user32.dll")]
        //    private static extern int SetForegroundWindow(IntPtr hWnd);

        //    [DllImport("user32.dll")]
        //    private static extern int IsIconic(IntPtr hWnd);

        //    /// <summary>
        //    /// GetCurrentInstanceWindowHandle
        //    /// </summary>
        //    /// <returns></returns>
        //    //private static IntPtr GetCurrentInstanceWindowHandle()
        //    //{
        //    //    IntPtr hWnd = IntPtr.Zero;
        //    //    Process process = Process.GetCurrentProcess();
        //    //    Process[] processes = Process.GetProcessesByName(process.ProcessName);

        //    //    foreach (Process _process in processes)
        //    //    {
        //    //        // Get the first instance that is not this instance, has the
        //    //        // same process name and was started from the same file name
        //    //        // and location. Also check that the process has a valid
        //    //        // window handle in this session to filter out other user's
        //    //        // processes.
        //    //        if (_process.Id != process.Id &&
        //    //            _process.MainModule.FileName == process.MainModule.FileName &&
        //    //            _process.MainWindowHandle != IntPtr.Zero)
        //    //        {
        //    //            hWnd = _process.MainWindowHandle;
        //    //            break;
        //    //        }
        //    //    }
        //    //    return hWnd;
        //    //}
        //    /// <summary>
        //    /// SwitchToCurrentInstance
        //    /// </summary>
        //    private static void SwitchToCurrentInstance(IntPtr hWnd)
        //    {
        //        // Restore window if minimised. Do not restore if already in
        //        // normal or maximised window state, since we don't want to
        //        // change the current state of the window.
        //        if (IsIconic(hWnd) != 0)
        //        {
        //            ShowWindow(hWnd, SW_RESTORE);
        //        }

        //        // Set foreground window.
        //        SetForegroundWindow(hWnd);
        //    }

        //    /// <summary>
        //    /// Execute a form base application if another instance already running on
        //    /// the system activate previous one
        //    /// </summary>
        //    /// <param name="frmMain">main form</param>
        //    /// <returns>true if no previous instance is running</returns>
        //    //		public static bool Run(System.Windows.Forms.Form frmMain, string orderNo)
        //    public static bool Run(string orderNo)
        //    {
        //        try
        //        {
        //            IntPtr hWnd = IsAlreadyRunning();
        //            Log4Net.Logger.loggInfo("hwnd is " + hWnd.ToString(), "GCS", "SingleApplication.Run(string orderno)");

        //            if (!hWnd.Equals(IntPtr.Zero))
        //            {
        //                Log4Net.Logger.loggInfo("Before Switchtocurreent", "GCS", DateTime.Now.ToString());

        //                SwitchToCurrentInstance(hWnd);
        //                MessageHelper.MessageHelper msg = new MessageHelper.MessageHelper();
        //                msg.sendWindowsStringMessage(hWnd.ToInt32(), 0, orderNo);
        //                Log4Net.Logger.loggInfo("After sendmessage", "GCS", DateTime.Now.ToString());

        //                return false;
        //            }

        //            System.Windows.Forms.Application.Run(new Ortoped.frmMain(orderNo));
        //            return true;
        //        }
        //        catch (Exception e)
        //        {
        //            Log4Net.Logger.loggError(e, "", "", "Run(string orderNo)");
        //            return false;
        //        }
        //    }

        //    /// <summary>
        //    /// for console base application
        //    /// </summary>
        //    /// <returns></returns>
        //    public static bool Run()
        //    {
        //        try
        //        {
        //            if (!IsAlreadyRunning().Equals(IntPtr.Zero))
        //            {
        //                Log4Net.Logger.loggInfo("Patientöversikten is already running", "GCS", "SingleApplication.Run()");
        //                return false;
        //            }
        //            Log4Net.Logger.loggInfo("Patientöversikten is starting", "GCS", "SingleApplication.Run()");
        //            System.Windows.Forms.Application.Run(new Ortoped.frmMain());
        //            return true;
        //        }
        //        catch (Exception e)
        //        {
        //            Log4Net.Logger.loggError(e, "", "", "Run()");
        //            return false;
        //        }
        //    }

        //    /// <summary>
        //    /// check if given exe are running or not
        //    /// </summary>
        //    /// <returns>returns true if already running</returns>
        //    private static IntPtr IsAlreadyRunning()
        //    {
        //        IntPtr hWnd = IntPtr.Zero;
        //        Process[] processes = Process.GetProcessesByName("Ortoped");// GetProcesses();
        //        int current_pid = Process.GetCurrentProcess().Id;
        //        Log4Net.Logger.loggInfo("Applications current pid is:" + current_pid.ToString(), "GCS", "SingleApplication.IsAlreadyRunning");
        //        Log4Net.Logger.loggInfo("Applications current Name is:" + Process.GetCurrentProcess().ProcessName , "GCS", "SingleApplication.IsAlreadyRunning");
        //        //System.Windows.Forms.MessageBox.Show("My PID: " + current_pid.ToString());

        //        foreach (Process p in processes)
        //        {
        //            //System.Windows.Forms.MessageBox.Show("Processlist: " + p.ProcessName.ToString() + " Process ID: " + p.Id.ToString());
        //            //System.Windows.Forms.MessageBox.Show("Started by user: " + p.StartInfo.UserName);

        //            //System.Windows.Forms.MessageBox.Show("Cheking process: " + p.Id);
        //            if (ProcessAPI.isCurrentUserProcess(p.Id))
        //            {
        //                //System.Windows.Forms.MessageBox.Show("IS CURRENT USER PROCESS: " + p.Id);
        //                if (p.Id != current_pid)
        //                {
        //                    hWnd = p.MainWindowHandle;
        //                    Log4Net.Logger.loggInfo("Hittade en GCS_Patientöversikt.exe som redan var startad: " + p.Id.ToString() + " med main hwnd: " + hWnd, "GCS", "SingleApplication.IsAlreadyRunning");
        //                    //System.Windows.Forms.MessageBox.Show("Hittade en GCS_Patientöversikt.exe som redan var startad: " + p.Id.ToString() + " med main hwnd: " + hWnd);
        //                    break;
        //                }
        //            }
        //        }

        //        return hWnd;
        //    }

        //    //static Mutex mutex;
        //    const int SW_RESTORE = 9;
        //}
    }
}


//if ((p.ProcessName.ToLower().StartsWith("GCS_Patientöversikt".ToLower())))
//{
//  Log4Net.Logger.loggInfo(p.Id.ToString(), "GCS", "SingleApplication.IsAlreadyRunning");
//  Log4Net.Logger.loggInfo(p.ProcessName, "GCS", "SingleApplication.IsAlreadyRunning");
//  if (ProcessAPI.isCurrentUserProcess(p.Id))
//  {
//    Log4Net.Logger.loggInfo("Process id " + p.Id + " is a current user process", "GCS", "SingleApplication.IsAlreadyRunning");

//    while (p.MainWindowHandle == IntPtr.Zero) 
//    {
//      p.Refresh();
//    }
//    hWnd = p.MainWindowHandle;
//    Log4Net.Logger.loggInfo("Hittade en GCS_Patientöversikt.exe som redan var startad: " + p.ProcessName + " med main hwnd: " + hWnd, "GCS", "SingleApplication.IsAlreadyRunning");
//    break;
//  }
//  else
//    Log4Net.Logger.loggInfo("Process id " + p.Id + " is NOT a current user process", "GCS", "ProcessAPI.isCurrentUserProcess");

//}
