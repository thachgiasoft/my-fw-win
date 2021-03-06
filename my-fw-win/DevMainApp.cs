using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Win;
using ProtocolVN.Framework.Win.Properties;
using ProtocolVN.Framework.Core;
using System.Drawing;

public static class DevMainApp
{
    static System.Threading.Mutex mutex;
    const int SW_RESTORE = 9;

    [DllImport("user32.dll")]
    private static extern int ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll")]
    private static extern int SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern int IsIconic(IntPtr hWnd);

    [STAThread]
    public static void Main(string username, string pass)
    {
        try
        {
            HelpPlugin.AssembluResolve();
            HelpApplication.initAppParams();
            FrameworkParams.Custom.initAppParam();//Load System Info

            if (RadParams.dbServer == null)
            {
                RadParams.dbServer = new FirebirdServer();
                RadParams.dbServer.initDBServerConfig();
            }

            if (FrameworkParams.dbServer == null)
            {
                FrameworkParams.dbServer = new AppFirebirdServer();
                FrameworkParams.dbServer.initDBServerConfig();
            }
                
            login(username, pass);
        }
        catch (Exception ex)
        {
            PLException.AddException(ex);
            HelpSysLog.AddException(ex);
        }
        finally
        {
        }
    }

    private static bool loginSystem(string username, string password)
    {
        if (DAUser.Instance.checkPassword(username, password))
        {
            DAUser.Instance.updateLastLogin(username);
            return true;
        }
        return false;
    }

    private static void login(string username, string password)
    {
        bool check = false;
        User user = new User();

        try
        {
            RadParams.isEMB = null;

            if (DABase.checkDBConnection())
            {
                frmRibbonMain.LoadDesktopDev();

                user.username = username;
                user.password = password;

                //if (FrameworkParams.UsingLDAP && !username.Equals("admin"))
                //    check = LDAPUser.Login(username, password);
                //else
                //    check = login(username, password);

                check = true;

                if (check)
                {
                    user.loadByUserName();
                    FrameworkParams.currentUser = user;
                    FrameworkParams.Custom.InitResAfterLogin();
                    if (RadParams.isEMB == false)//Khi DLL không tồn tại                        
                    {
                        FrameworkParams.ExitApplication(FrameworkParams.EXIT_STATUS.ERROR);
                        return;
                    }
                }
                else
                {
                    FWMsgBox.showInvalidUser();
                    FrameworkParams.isSkipLogin = false;
                }
            }
            else
            {
                FWMsgBox.showInvalidConnectServer(null);
                FrameworkParams.isSkipLogin = false;
            }
        }
        catch (Exception ex)
        {
            PLException.AddException(ex);
            RadParams.isEMB = false;
            FrameworkParams.ExitApplication(FrameworkParams.EXIT_STATUS.ERROR);
            FrameworkParams.isSkipLogin = false;
        }
        finally
        {
        }
    }

    private static bool IsAlreadyRunning()
    {
        string strLoc = Assembly.GetCallingAssembly().Location;
        FileSystemInfo fileInfo = new FileInfo(strLoc);
        string sExeName = fileInfo.Name;
        bool bCreatedNew;

        mutex = new Mutex(true, "Global\\" + sExeName, out bCreatedNew);
        if (bCreatedNew)
            mutex.ReleaseMutex();

        return !bCreatedNew;
    }

    /*
     * switch to current application
     */
    private static void SwitchToCurrentInstance()
    {
        IntPtr hWnd = GetCurrentInstanceWindowHandle();
        if (hWnd != IntPtr.Zero)
        {
            // Restore window if minimised. Do not restore if already in
            // normal or maximised window state, since we don't want to
            // change the current state of the window.
            if (IsIconic(hWnd) != 0)
            {
                ShowWindow(hWnd, SW_RESTORE);
            }

            // Set foreground window.
            SetForegroundWindow(hWnd);
        }
    }

    private static IntPtr GetCurrentInstanceWindowHandle()
    {
        IntPtr hWnd = IntPtr.Zero;
        Process process = Process.GetCurrentProcess();
        Process[] processes = Process.GetProcessesByName(process.ProcessName);
        foreach (Process _process in processes)
        {
            // Get the first instance that is not this instance, has the
            // same process name and was started from the same file name
            // and location. Also check that the process has a valid
            // window handle in this session to filter out other user's
            // processes.
            if (_process.Id != process.Id &&
                _process.MainModule.FileName == process.MainModule.FileName &&
                _process.MainWindowHandle != IntPtr.Zero)
            {
                hWnd = _process.MainWindowHandle;
                break;
            }
        }
        return hWnd;
    }
}