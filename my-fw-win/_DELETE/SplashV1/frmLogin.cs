using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
using System.Diagnostics;
namespace ProtocolVN.Framework.Win
{
    public partial class frmLogin : XtraForm
    {
        private User user;

        public frmLogin()
        {
            InitializeComponent();
            this.btnExit.Image = FWImageDic.EXIT_IMAGE16;
            this.btnConfig.Image = FWImageDic.CONFIG_IMAGE16;
            this.btnLogin.Image = FWImageDic.LOGIN_IMAGE16;
            this.Icon = FrameworkParams.ApplicationIcon;
            components = new System.ComponentModel.Container();
            user = new User();
            user.loadCookies();
            txtUsername.EditValue = user.username;
            txtPassword.EditValue = user.password;
            if (user.savePass == "Y") chkRememberPwd.Checked = true;
            if (user.isAutoLogin == "Y") chkAutoLogin.Checked = true;
            if (chkAutoLogin.Checked) FrameworkParams.isSkipLogin = true;
            // init skin
            FrameworkParams.currentSkin = new PLSkin(this.components);
            FrameworkParams.option = new Option();
            FrameworkParams.option.load();

            Application.CurrentCulture = ApplyFormatAction.GetCultureInfo();

            FrameworkParams.currentSkin.SelectSkin(HelpNumber.ParseInt32(FrameworkParams.option.Skin));
            this.Shown += new EventHandler(frmLogin_Shown);
        }

        void frmLogin_Shown(object sender, EventArgs e)
        {
            Application.UseWaitCursor = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            FrameworkParams.ExitApplication(FrameworkParams.EXIT_STATUS.NORMAL_THANKS);
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            this.Hide();     
            frmConfigDBSecurity frmCon = new frmConfigDBSecurity();
            frmCon.ShowDialog();
        }

        public void LoginAction()
        {
            bool check = false;
            string remPass = "N";
            string remAuto = "N";
            try
            {
                //this.UseWaitCursor = true;
                Cursor.Current = Cursors.WaitCursor;
                RadParams.isEMB = null;                
                if (DABase.checkDBConnection())
                {
                    user.username = txtUsername.Text.Trim();
                    user.password = txtPassword.Text.Trim();
                    
                    if (FrameworkParams.UsingLDAP && !user.username.Equals("admin"))
                        check = LDAPUser.Login(user.username, user.password);
                    else
                        check = user.login();

                    if (check)
                    {
                        this.Hide();
                        FrameworkParams.SplashFormLogin.Show();
                        //InternetConn.Monitor(); 
                        if (chkRememberPwd.Checked) remPass = "Y";
                        else
                        {
                            user.password = "";
                            remPass = "N";
                        }
                        if (chkAutoLogin.Checked) remAuto = "Y";

                        user.updateCookies(remPass, remAuto);                        
                        user.loadByUserName();
                        this.UseWaitCursor = false;
                        FrameworkParams.currentUser = user;
                        FrameworkParams.Custom.InitResAfterLogin();
                        XtraForm main = new frmRibbonMain();
                        FrameworkParams.MainForm = main;
                        ((frmRibbonMain)main).LoadBarManager(user.username);                        
                        //new AppWarning(((frmRibbonMain) main));
                        while (this.UseWaitCursor == true);                        
                        //RadParams.isEMB = false;
                        ((frmRibbonMain)main).LoadDesktopForm();
                        if (RadParams.isEMB == false)//Khi DLL không tồn tại                        
                        {
                            this.Hide(); this.Dispose();
                            FrameworkParams.ExitApplication(FrameworkParams.EXIT_STATUS.ERROR);
                            return;
                        }

                        if (FrameworkParams.IsCheckNewVersion) UpdateProgram.ShowNewVersionInfo();
                    }
                    else
                    {
                        //this.UseWaitCursor = false;
                        Cursor.Current = Cursors.Default;
                        FWMsgBox.showInvalidUser();
                        FrameworkParams.isSkipLogin = false;
                    }
                }
                else
                {
                    //this.UseWaitCursor = false;
                    Cursor.Current = Cursors.Default;
                    FWMsgBox.showInvalidConnectServer();
                    FrameworkParams.isSkipLogin = false;
                }
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
                RadParams.isEMB = false;
                this.Hide(); this.Dispose();
                FrameworkParams.ExitApplication(FrameworkParams.EXIT_STATUS.ERROR);
                
                FrameworkParams.isSkipLogin = false;
            }
            finally
            {
                FrameworkParams.SplashFormLogin.Hide();
                Cursor.Current = Cursors.Default;
            }            
        }

        public void btnLogin_Click(object sender, EventArgs e)
        {
            LoginAction();
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.UseWaitCursor = false;
            FrameworkParams.ExitApplication(FrameworkParams.EXIT_STATUS.NORMAL_THANKS);
        }
    }
}