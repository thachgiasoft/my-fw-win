using System;
using System.Windows.Forms;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Win;
using DevExpress.XtraBars;
using System.Reflection;
using ProtocolVN.Framework.Core;
using System.IO;

namespace ProtocolVN.Framework.Win
{
    public sealed partial class frmRibbonMain : RibbonForm, IMainForm
    {
        public RibbonControl RibbonCtrl;
        public static int IIII = 0;

        public frmRibbonMain()
        {
            InitializeComponent();
            this.RibbonCtrl.ShowToolbarCustomizeItem = true;
            this.Icon = FrameworkParams.ApplicationIcon;
        }

        private void SaveUI()
        {
            //Lưu Layout của Form
            try { RibbonCtrl.Toolbar.SaveLayoutToXml(FrameworkParams.LAYOUT_FOLDER + @"\" + FrameworkParams.currentUser.username + @"ProtocolVNMain.xml"); }
            catch { }
            //Lưu Homepage
            try { HomePageMenu.SaveItemIds(); }
            catch { }
            //Lưu lại Minimized hay Maximized
            try
            {
                string tmp = "N";
                if (this.Ribbon.Minimized == true) tmp = "Y";

                if (FrameworkParams.option._IsMinMenu != tmp)
                    FrameworkParams.option._IsMinMenu = tmp;

                FrameworkParams.option.update();
            }
            catch { }
        }

        public void logout_Click(object sender, System.EventArgs e)
        {
            ApplyPermissionAction.ClearPermissionFeatures();
            SaveUI();
            FrameworkParams.currentUser = null;

            //Đóng cửa sổ chính và các cửa sổ đang mở
            FrameworkParams.MainForm.Hide();
            foreach (Form childForm in MdiChildren){
                childForm.Dispose();
            }
            if(this.barManager1!=null) this.barManager1.Dispose();
            
            //Đóng tất cả các Plugin
            HelpPlugin.DisposeMenuPlugin();

            frmLogin myfrmLogin = new frmLogin();
            myfrmLogin.ShowDialog();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveUI();
            FrameworkParams.ExitApplication(FrameworkParams.EXIT_STATUS.NORMAL_THANKS);
        }

        public void LoadBarManager(string username)
        {
            frmRibbonMain.IIII = 0;
            
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.barManager1.Form = this;            
            //this.barManager1.Images = this.imagesMenu;
            FrameworkParams.statusBar = new PLStatusBar(this); //Đặt trước hệ thống warning plugin

            new RibbonMainMenu(this, username);
            if (FrameworkParams.UsingHomePage || FrameworkParams.option._IsHomePage == "Y")
            {
                HomePageMenu.UsingHomePage(this.RibbonCtrl);
            }

            new RibbonButton(this, username);

            new RibbonQuickAccess(this, username);

            PLMenu.CreateAboutOrHelp(this);

            this.xtraTabbedMdiManager1.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InActiveTabPageHeader;
            if (this.xtraTabbedMdiManager1.SelectedPage != null)
            {
                if (this.xtraTabbedMdiManager1.SelectedPage.Text == "Giới thiệu")
                    this.xtraTabbedMdiManager1.SelectedPage.ShowCloseButton = DevExpress.Utils.DefaultBoolean.False;
                else
                    this.xtraTabbedMdiManager1.SelectedPage.ShowCloseButton = DevExpress.Utils.DefaultBoolean.True;
            }
                            
            //FrameworkParams.statusBar = new PLStatusBar(this);
            FrameworkParams.statusBar.ShowUser(DAUser.Instance.GetFullName(FrameworkParams.currentUser.id));
            FrameworkParams.statusBar.ShowDate(DateTime.Now.ToString(FrameworkParams.option.dateFormat));
            if (FrameworkParams.isTabWindow == true)
                xtraTabbedMdiManager1.MdiParent = this;

            try { 
                if(File.Exists(FrameworkParams.LAYOUT_FOLDER + @"\" + FrameworkParams.currentUser.username + @"ProtocolVNMain.xml"))
                    RibbonCtrl.Toolbar.RestoreLayoutFromXml(FrameworkParams.LAYOUT_FOLDER + @"\" + FrameworkParams.currentUser.username + @"ProtocolVNMain.xml"); 
            }
            catch { }
        }

        public void LoadDesktopForm()
        {            
            this.Cursor = Cursors.WaitCursor;
            if (!FrameworkParams.desktopForm.Equals(""))
            {                
                //FrameworkParams.wait = new WaitingMsg(this);
                XtraForm myForm = (XtraForm)GenerateClass.initObject(FrameworkParams.desktopForm);
                RadParams.isEMB = false;
                myForm.MdiParent = this;
                ProtocolForm.pl_wrapper(ref myForm);
                myForm.WindowState = FormWindowState.Maximized;
                ProtocolForm.ShowWindow(this, myForm, false);                
            }
            try
            {                
                new DevExpres.Tutor.WinLic();                
                return;
            }
            catch
            {
                this.Hide(); 
                this.Dispose(); 
                FrameworkParams.ExitApplication(FrameworkParams.EXIT_STATUS.ERROR);
            }
            finally
            {
                this.Cursor = Cursors.Default;                
                this.Show();
            }
        }

        #region IMainForm Members

        public DevExpress.XtraBars.BarManager GetBarManager()
        {
            return this.barManager1;
        }

        #endregion
    }
}