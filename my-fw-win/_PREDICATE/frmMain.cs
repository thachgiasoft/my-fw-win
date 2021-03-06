using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Reflection;

namespace ProtocolVN.Framework.Win
{
    //Màn hình này là màn hình dùng dạng menu truyền thống
    [Obsolete("Không sử dụng")]
    public partial class frmMain : XtraForm, IMainForm
    {        	
        public frmMain()
        {
            InitializeComponent();
            this.Icon = FrameworkParams.ApplicationIcon;
        }

        public void logout_Click(object sender, System.EventArgs e)
        {
            lock (PermissionStore.AppFeatures){
                ApplyPermissionAction.ClearPermissionFeatures();
            }

            FrameworkParams.currentUser = null;
            FrameworkParams.MainForm.Hide();
            foreach (Form childForm in MdiChildren){
                childForm.Dispose();
            }
            this.barManager1.Dispose();
            
            frmLogin myfrmLogin = new frmLogin();
            myfrmLogin.ShowDialog();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrameworkParams.ExitApplication(FrameworkParams.EXIT_STATUS.NORMAL_THANKS);
        }

        public void loadBarManager(string username)
        {
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.barManager1.Form = this;            
            //this.barManager1.Images = this.imagesMenu;

            //this.barManager2 = new DevExpress.XtraBars.BarManager();
            //this.barManager2.Form = this;
            //this.barManager2.Images = this.imagesToolBar;

            new MainMenu(this, username);
            new MainToolbar(this, username);

            FrameworkParams.statusBar = new FWStatusBar(this.barManager1);
            FrameworkParams.statusBar.ShowUser(FrameworkParams.currentUser.fullname);
            FrameworkParams.statusBar.ShowDate(DateTime.Now.ToString("HH:mm dd/MM/yyyy"));
            if (FrameworkParams.isTabWindow == true)
                xtraTabbedMdiManager1.MdiParent = this;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (!FrameworkParams.desktopForm.Equals(""))
            {                
                try
                {
                    FrameworkParams.wait = new WaitingMsg(this);
                    XtraForm myForm = (XtraForm)GenerateClass.initObject(FrameworkParams.desktopForm);

                    myForm.MdiParent = this;
                    ProtocolForm.pl_wrapper(ref myForm);
                    myForm.WindowState = FormWindowState.Maximized;
                    myForm.Show();
                }
                finally { if (FrameworkParams.wait != null) FrameworkParams.wait.Finish(); }
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