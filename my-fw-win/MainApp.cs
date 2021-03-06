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

    public class ThreadExceptionHandler
    {
        public void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
        {
            //HelpMsgBox._showSystemErrorMessage("Xin lỗi về sự bất tiện này. Vui lòng khởi động lại ứng dụng.\n Nếu vẫn xãy ra lỗi này vui lòng liên hệ đến Công ty PROTOCOL.", true);
            //HelpMsgBox.ShowNotificationMessage("Xin lỗi về sự bất tiện này. Vui lòng khởi động lại ứng dụng.\n Nếu vẫn xãy ra lỗi này vui lòng liên hệ đến Công ty PROTOCOL.");
            //MessageBox.Show(e.Exception.Message, "An exception occurred:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            PLException.AddException(new PLException(e.Exception));
            frmFWUserError frm = new frmFWUserError(e);
            frm.ShowDialog();
        }
    }

    [STAThread]
    public static void Main()
    {
        try
        {
            //if (IsAlreadyRunning())
            //{
            //    SwitchToCurrentInstance();
            //}
            //else
            {
                 Application.ThreadException += new ThreadExceptionEventHandler(new ThreadExceptionHandler().ApplicationThreadException);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                
                SplashScreen.Instance.Font = new System.Drawing.Font("Verdana", 8.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                SplashScreen.Instance.ForeColor = Color.White;
                SplashScreen.SetBackgroundImage(Resources.splashbg);                                
                SplashScreen.SetTitleString("");
                SplashScreen.BeginDisplay();

                SplashScreen.SetCommentaryString("..Đang nạp thư viện ứng dụng.");
                HelpPlugin.AssembluResolve();
                HelpApplication.initAppParams();//Init System
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

                //Đặt trước để có thể debug kết nối không thành công từ bên ngoài.
                if (FrameworkParams.IsHotKey)
                {
                    new PLHotKey();
                    //Gắn các chức năng truy xuất nhanh
                    //- Hỗ trợ phím tắt cho phép
                    //    + Thoát khỏi chương trình nhanh
                    //    + Bật tắt Bộ gõ tiếng việt
                    //    + Calc Máy tính bỏ túi
                    //    + Note để ghi note ( nâng cấp lên lưu tự động )
                }
                if (FrameworkParams.isSupportDeveloper) new SupportDeveloper();
                
                #region Thông qua màn hình Login && Flash
                XtraForm login = new frmLogin();                
                ProtocolForm.pl_wrapper(ref login, null, null);                
                Application.Run(login);
                #endregion

                
            }
        }
        catch (Exception ex)
        {
            
            PLException.AddException(ex);
            HelpSysLog.AddException(ex);
            //PLDebug.ShowExceptionInfo();
            //HelpMsgBox.ShowNotificationMessage("Dữ liệu đang gặp vấn đề. Vui lòng liên hệ Công ty P R O T O C O L.");            
            //throw new Exception("MainApp không run thành công");
        }
        finally
        {
            Cursor.Current = Cursors.Default;
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