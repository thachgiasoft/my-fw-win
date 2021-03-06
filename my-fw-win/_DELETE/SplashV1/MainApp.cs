using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Win;

public static class MainApp
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
    public static void Main()
    {
        try
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //if (IsAlreadyRunning())
            //{
            //    SwitchToCurrentInstance();
            //}
            //else
            {
                Cursor.Current = Cursors.WaitCursor;                
                HelpPlugin.AssembluResolve();
                FrameworkParams.InitAppParams();//Init System
                FrameworkParams.Custom.initAppParam();//Load System Info
                UpdateSystem();//Update System

                #region Thông qua màn hình Login && Flash
                XtraForm login = new frmLogin();
                
                //Option opt = FrameworkParams.option;
                ProtocolForm.pl_wrapper(ref login, null, null);

                if (FrameworkParams.SplashForm == null)
                    FrameworkParams.SplashForm = new frmFWSplash();
                if (FrameworkParams.SplashFormLogin == null)
                    FrameworkParams.SplashFormLogin = new frmFWSplash();
                
                FrameworkParams.SplashForm.Focus();
                
                SplashAppContext splashContext = new SplashAppContext(login, FrameworkParams.SplashForm);
                Application.Run(splashContext);
                
                #endregion
            }
        }
        catch (Exception ex)
        {
            throw new Exception("MainApp không run thành công");
        }
        finally
        {
            Cursor.Current = Cursors.Default;
        }
    }

    /// <summary>
    /// Chưa hiểu hàng này
    /// </summary>
    private static void UpdateSystem()
    {
        if (FrameworkParams.IsHotKey) new PLHotKey();
        if (FrameworkParams.isSupportDeveloper) new Developer();
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