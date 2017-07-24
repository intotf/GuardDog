using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;
using IU = Infrastructure.Utility;

namespace GuardDog
{
    /// <summary>
    /// 程序类提权方法
    /// </summary>
    static class ProcessEx
    {
        /// <summary>
        /// 开始运行程序
        /// 如果是服务进和开始桌面程序，使用提权可在管理员桌面看到UI
        /// </summary>
        /// <param name="startInfo"></param>
        /// <returns></returns>
        public static Process Start(ProcessStartInfo startInfo)
        {
            if (string.IsNullOrEmpty(startInfo.WorkingDirectory))
            {
                startInfo.WorkingDirectory = Path.GetDirectoryName(Path.GetFullPath(startInfo.FileName));
            }

            if (Process.GetCurrentProcess().SessionId == 0)
            {
                IU.Debugger.WriteLine("Run in service mode");
                return StartAsUser(startInfo);
            }
            else
            {
                IU.Debugger.WriteLine("Run in desktop mode");
                return Process.Start(startInfo);
            }
        }

        private static Process StartAsUser(ProcessStartInfo startInfo)
        {
            var hToken = WindowsIdentity.GetCurrent().Token;
            var hDupedToken = IntPtr.Zero;

            var pi = new PROCESS_INFORMATION();
            var sa = new SECURITY_ATTRIBUTES();
            sa.Length = Marshal.SizeOf(sa);

            var si = new STARTUPINFO();
            si.cb = Marshal.SizeOf(si);

            var dwSessionID = WTSGetActiveConsoleSessionId();
            if (WTSQueryUserToken(dwSessionID, out hToken) == false)
            {
                var ex = new Exception("WTSQueryUserToken failure");
                IU.Debugger.WriteLine(ex.Message);
                throw ex;
            }

            var dupState = DuplicateTokenEx(
                  hToken,
                  GENERIC_ALL_ACCESS,
                  ref sa,
                  (int)SECURITY_IMPERSONATION_LEVEL.SecurityIdentification,
                  (int)TOKEN_TYPE.TokenPrimary,
                  ref hDupedToken
               );

            if (dupState == false)
            {
                var ex = new Exception("DuplicateTokenEx failure");
                IU.Debugger.WriteLine(ex.Message);
                throw ex;
            }

            var lpEnvironment = IntPtr.Zero;
            if (CreateEnvironmentBlock(out lpEnvironment, hDupedToken, false) == false)
            {
                var ex = new Exception("CreateEnvironmentBlock failure");
                IU.Debugger.WriteLine(ex.Message);
                throw ex;
            }


            var result = CreateProcessAsUser(
                                   hDupedToken,
                                   startInfo.FileName,
                                   startInfo.Arguments,
                                   ref sa, ref sa,
                                   false, 0, IntPtr.Zero,
                                   startInfo.WorkingDirectory, ref si, ref pi);

            if (!result)
            {
                var ex = new Exception("CreateProcessAsUser failure");
                IU.Debugger.WriteLine(ex.Message);
                throw ex;
            }

            if (pi.hProcess != IntPtr.Zero)
                CloseHandle(pi.hProcess);
            if (pi.hThread != IntPtr.Zero)
                CloseHandle(pi.hThread);
            if (hDupedToken != IntPtr.Zero)
                CloseHandle(hDupedToken);

            return Process.GetProcessById(pi.dwProcessID);
        }

        [StructLayout(LayoutKind.Sequential)]
        struct STARTUPINFO
        {
            public Int32 cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public Int32 dwX;
            public Int32 dwY;
            public Int32 dwXSize;
            public Int32 dwXCountChars;
            public Int32 dwYCountChars;
            public Int32 dwFillAttribute;
            public Int32 dwFlags;
            public Int16 wShowWindow;
            public Int16 cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public Int32 dwProcessID;
            public Int32 dwThreadID;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct SECURITY_ATTRIBUTES
        {
            public Int32 Length;
            public IntPtr lpSecurityDescriptor;
            public bool bInheritHandle;
        }

        enum SECURITY_IMPERSONATION_LEVEL
        {
            SecurityAnonymous,
            SecurityIdentification,
            SecurityImpersonation,
            SecurityDelegation
        }

        enum TOKEN_TYPE
        {
            TokenPrimary = 1,
            TokenImpersonation
        }

        const int GENERIC_ALL_ACCESS = 0x10000000;

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        static extern bool CloseHandle(IntPtr handle);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        static extern bool CreateProcessAsUser(
          IntPtr hToken,
          string lpApplicationName,
          string lpCommandLine,
          ref SECURITY_ATTRIBUTES lpProcessAttributes,
          ref SECURITY_ATTRIBUTES lpThreadAttributes,
          bool bInheritHandle,
          Int32 dwCreationFlags,
          IntPtr lpEnvrionment,
          string lpCurrentDirectory,
          ref STARTUPINFO lpStartupInfo,
          ref PROCESS_INFORMATION lpProcessInformation);

        [DllImport("advapi32.dll", SetLastError = true)]
        static extern bool DuplicateTokenEx(
          IntPtr hExistingToken,
          Int32 dwDesiredAccess,
          ref SECURITY_ATTRIBUTES lpThreadAttributes,
          Int32 ImpersonationLevel,
          Int32 dwTokenType,
          ref IntPtr phNewToken);

        [DllImport("wtsapi32.dll", SetLastError = true)]
        static extern bool WTSQueryUserToken(
          Int32 sessionId,
          out IntPtr Token);

        [DllImport("userenv.dll", SetLastError = true)]
        static extern bool CreateEnvironmentBlock(
            out IntPtr lpEnvironment,
            IntPtr hToken,
            bool bInherit);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int WTSGetActiveConsoleSessionId();
    }
}
