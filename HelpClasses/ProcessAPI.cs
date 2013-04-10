using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using GCS;
using System.Security.Permissions;
using System.Security.Principal;
using HANDLE = System.IntPtr;
using System.Management;

namespace SingleInstance
{
    public class ProcessAPI
    {
        public const int TOKEN_QUERY = 0X00000008;
        private static WindowsIdentity _user = WindowsIdentity.GetCurrent();

        const int ERROR_NO_MORE_ITEMS = 259;

        enum TOKEN_INFORMATION_CLASS
        {
            TokenUser = 1,
            TokenGroups,
            TokenPrivileges,
            TokenOwner,
            TokenPrimaryGroup,
            TokenDefaultDacl,
            TokenSource,
            TokenType,
            TokenImpersonationLevel,
            TokenStatistics,
            TokenRestrictedSids,
            TokenSessionId
        }

        [StructLayout(LayoutKind.Sequential)]
        struct TOKEN_USER
        {
            public _SID_AND_ATTRIBUTES User;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct _SID_AND_ATTRIBUTES
        {
            public IntPtr Sid;
            public int Attributes;
        }

        [DllImport("advapi32")]
        static extern bool OpenProcessToken(
            HANDLE ProcessHandle, // handle to process
            int DesiredAccess, // desired access to process
            ref IntPtr TokenHandle // handle to open access token
        );

        [DllImport("kernel32")]
        static extern HANDLE GetCurrentProcess();

        [DllImport("advapi32", CharSet = CharSet.Auto)]
        static extern bool GetTokenInformation(
            HANDLE hToken,
            TOKEN_INFORMATION_CLASS tokenInfoClass,
            IntPtr TokenInformation,
            int tokeInfoLength,
            ref int reqLength
        );

        [DllImport("kernel32")]
        static extern bool CloseHandle(HANDLE handle);

        [DllImport("advapi32", CharSet = CharSet.Auto)]
        static extern bool ConvertSidToStringSid(
            IntPtr pSID,
            [In, Out, MarshalAs(UnmanagedType.LPTStr)] ref string pStringSid
        );

        [DllImport("advapi32", CharSet = CharSet.Auto)]
        static extern bool ConvertStringSidToSid(
            [In, MarshalAs(UnmanagedType.LPTStr)] string pStringSid,
            ref IntPtr pSID
        );

        /// <summary>
        /// Collect User Info
        /// </summary>
        /// <param name="pToken">Process Handle</param>
        public static bool DumpUserInfo(HANDLE pToken, out IntPtr SID)
        {
            int Access = TOKEN_QUERY;
            HANDLE procToken = IntPtr.Zero;
            bool ret = false;
            SID = IntPtr.Zero;
            try
            {
                if (OpenProcessToken(pToken, Access, ref procToken))
                {
                    ret = ProcessTokenToSid(procToken, out SID);
                    CloseHandle(procToken);
                }
                return ret;
            }
            catch (Exception err)
            {
                return false;
            }
        }

        private static bool ProcessTokenToSid(HANDLE token, out IntPtr SID)
        {
            TOKEN_USER tokUser;
            const int bufLength = 256;
            IntPtr tu = Marshal.AllocHGlobal(bufLength);
            bool ret = false;
            SID = IntPtr.Zero;
            try
            {
                int cb = bufLength;
                ret = GetTokenInformation(token, TOKEN_INFORMATION_CLASS.TokenUser, tu, cb, ref cb);
                if (ret)
                {
                    tokUser = (TOKEN_USER)Marshal.PtrToStructure(tu, typeof(TOKEN_USER));
                    SID = tokUser.User.Sid;
                }
                return ret;
            }
            catch (Exception err)
            {
                return false;
            }
            finally
            {
                Marshal.FreeHGlobal(tu);
            }
        }

        public static string ExGetProcessInfoByPID(int PID, out string SID)//, out string OwnerSID)
        {
            IntPtr _SID = IntPtr.Zero;
            SID = String.Empty;
            try
            {
                Process process = Process.GetProcessById(PID);
                if (DumpUserInfo(process.Handle, out _SID))
                {
                    ConvertSidToStringSid(_SID, ref SID);
                }
                return process.ProcessName;
            }
            catch
            {
                return "Unknown";
            }
        }

        public static bool isCurrentUserProcess(int ProcessID)
        {
            string stringSID = String.Empty, domain;
            //string process = ExGetProcessInfoByPID(ProcessID, out stringSID);
            stringSID = GetProcessInfoByPID(ProcessID, out stringSID, out domain);
            
            if (String.Compare(stringSID, _user.User.Value, true) == 0)
            {
                System.Windows.Forms.MessageBox.Show("MATCH ProcessID: " + ProcessID.ToString());
                return true;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("NO MATCH ProcessID: " + ProcessID.ToString());
                System.Windows.Forms.MessageBox.Show(stringSID + "\n" + _user.Name); 
                return false;
            }
        }

        public static string GetProcessInfoByPID(int PID, out string User, out string Domain)
        {
            User = String.Empty;
            Domain = String.Empty;
            string OwnerSID = String.Empty;
            string processname = String.Empty;
            try
            {

                ObjectQuery sq = new ObjectQuery("Select * from Win32_Process Where ProcessID = '" + PID + "'");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(sq);
                if (searcher.Get().Count == 0)
                    return OwnerSID;
                
                foreach (ManagementObject oReturn in searcher.Get())
                {
                    string[] o = new String[2];
                    //Invoke the method and populate the o var with the user name and domain                         
                    oReturn.InvokeMethod("GetOwner", (object[])o);

                    //int pid = (int)oReturn["ProcessID"];                                                           
                    processname = (string)oReturn["Name"];
                    //dr[2] = oReturn["Description"];                                                                
                    User = o[0];
                    if (User == null)
                        User = String.Empty;
                    Domain = o[1];
                    if (Domain == null)
                        Domain = String.Empty;
                    string[] sid = new String[1];
                    oReturn.InvokeMethod("GetOwnerSid", (object[])sid);
                    OwnerSID = sid[0];
                    return OwnerSID;
                }
            }
            catch
            {
                return OwnerSID;
            }
            return OwnerSID;
        }
    }
}
