using System;
using System.Windows.Forms;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Win;
using DevExpress.XtraBars;
using System.Reflection;
using ProtocolVN.Framework.Core;
using System.IO;
using System.Threading;
using System.Diagnostics;
using DevExpress.XtraTabbedMdi;
using System.Collections.Generic;
using DevExpress.XtraTab;

namespace ProtocolVN.Framework.Win
{
    public sealed partial class frmRibbonMain : RibbonForm, IMainForm
    {
        internal RibbonControl RibbonCtrl;
        internal static int IIII = 0;

        public frmRibbonMain()
        {
            InitializeComponent();
            //this.Text = "www.protocolvn.com";
            this.Text = HelpApplication.getProductName();
            this.RibbonCtrl.ShowToolbarCustomizeItem = true;
            this.Icon = FrameworkParams.ApplicationIcon;
            this.Shown += new EventHandler(frmRibbonMain_Shown);
            this.FormClosing += new FormClosingEventHandler(frmRibbonMain_FormClosing);

            xtraTabbedMdiManager1.MouseUp += new MouseEventHandler(_tabbedMDIManager_MouseUp);
            SetTabbedMDI(true);  // this is normally controlled by a main menu option

            PLKey k = new PLKey(this);
            k.Add(Keys.F1, delegate(){
                if (File.Exists(RadParams.HELP_FILE)==false)
                {
                    frmPLAbout dlg = new frmPLAbout();
                    ProtocolForm.ShowModalDialog(this, dlg);
                }
                else
                {
                    PLHelp.openCHM();
                }
            });
            this.RibbonCtrl.RibbonStyle = FrameworkParams.style;
            this.Load += new EventHandler(frmRibbonMain_Load);
        }

        void frmRibbonMain_Load(object sender, EventArgs e)
        {
            if (frmLogin.frmLoginInstance != null)
            {
                frmLogin.frmLoginInstance.Hide();
                //frmLogin.frmLoginInstance.Dispose();
            }
        }
        public static bool isForceExit = false;
        void frmRibbonMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (frmRibbonMain.isForceExit == true) 
            {
                e.Cancel = false;
                frmRibbonMain.isForceExit = false;
                return;
            }

            if (frmFWRunExit.confirmExit() == false)
            {
                e.Cancel = true;
            }
            else{
                e.Cancel = false;
            }
        }

        void frmRibbonMain_Shown(object sender, EventArgs e)
        {
            if (frmLogin.frmLoginInstance != null)
            {
                frmLogin.frmLoginInstance.Hide();
                //frmLogin.frmLoginInstance.Dispose();
                //frmLogin.frmLoginInstance.Close();
            }
            HelpXtraForm.ShowUserDialog(this, new frmFWRunFirst());
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

        public void processLogOut()
        {
            lock (PermissionStore.AppFeatures)
            {
                ApplyPermissionAction.ClearPermissionFeatures();
            }

            SaveUI();
            FrameworkParams.currentUser = null;

            //Đóng cửa sổ chính và các cửa sổ đang mở

            foreach (Form childForm in MdiChildren)
            {
                childForm.Dispose();
            }
            if (this.barManager1 != null) this.barManager1.Dispose();

            //Stop Stickies
            ProtocolVN.Plugin.NoteBook.StickiesMethodExec.StopStickies();

            //Đóng tất cả các Plugin
            HelpPlugin.DisposeMenuPlugin();

            //Call release resource
            FrameworkParams.Custom.ReleaseResAfterLogout();

            FrameworkParams.MainForm.Hide();

            frmLogin myfrmLogin = new frmLogin();
            myfrmLogin.Show();

            if(FrameworkParams.MainForm is frmLogin){}                
            else FrameworkParams.MainForm.Dispose();
        }
        public void logout_Click(object sender, System.EventArgs e)
        {
            WaitingMsg msg = new WaitingMsg("Đang xử lý đăng xuất", "Vui lòng chờ trong giây lát.");
            try
            {
                processLogOut();
            }
            catch { }
            finally
            {
                msg.Finish();
            }
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
            FrameworkParams.statusBar = new FWStatusBar(this); //Đặt trước hệ thống warning plugin

            new RibbonMainMenu(this, username);
            if (FrameworkParams.UsingHomePage || FrameworkParams.option._IsHomePage == "Y")
            {
                HomePageMenu.UsingHomePage(this.RibbonCtrl);
            }

            new RibbonButton(this, username);

            new RibbonQuickAccess(this, username);

            //_AddPROTOCOLVNCOM(this);

            CreateAboutOrHelp(this);

            this.xtraTabbedMdiManager1.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InActiveTabPageHeader;
            if (this.xtraTabbedMdiManager1.SelectedPage != null)
            {
                if (RightClickTitleBarHelper.isDesktopForm(this.xtraTabbedMdiManager1.SelectedPage.MdiChild))                
                    this.xtraTabbedMdiManager1.SelectedPage.ShowCloseButton = DevExpress.Utils.DefaultBoolean.False;
                else
                    this.xtraTabbedMdiManager1.SelectedPage.ShowCloseButton = DevExpress.Utils.DefaultBoolean.True;
            }
                            
            //FrameworkParams.statusBar = new PLStatusBar(this);
            FrameworkParams.statusBar.ShowUser(FrameworkParams.currentUser.fullname);
            FrameworkParams.statusBar.ShowDate(DateTime.Now.ToString("HH:mm " + FrameworkParams.option.dateFormat));
            if (FrameworkParams.isTabWindow == true)
                xtraTabbedMdiManager1.MdiParent = this;

            try { 
                if(File.Exists(FrameworkParams.LAYOUT_FOLDER + @"\" + FrameworkParams.currentUser.username + @"ProtocolVNMain.xml")){
                    RibbonCtrl.Toolbar.RestoreLayoutFromXml(FrameworkParams.LAYOUT_FOLDER + @"\" + FrameworkParams.currentUser.username + @"ProtocolVNMain.xml");
                    if (FrameworkParams.currentSkin != null && FrameworkParams.style != RibbonControlStyle.Office2007)
                        SkinGalleryHelper.AddSkinMenuToQuickShortcut(this.Ribbon, FrameworkParams.currentSkin.Skin);
                }
            }
            catch { }
        }

        public void LoadDesktopForm()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (!FrameworkParams.desktopForm.Equals(""))
                {                
                    //FrameworkParams.wait = new WaitingMsg(this);
                    XtraForm myForm = (XtraForm)GenerateClass.initObject(FrameworkParams.desktopForm);                    
                    myForm.MdiParent = this;
                    ProtocolForm.pl_wrapper(ref myForm);
                    myForm.WindowState = FormWindowState.Maximized;
                    ProtocolForm.ShowWindow(this, myForm, false);                
                }

                //if (FrameworkParams.IsBeforeLogin == false)
                {
                    if (__PL__.IsUseLicx)
                    {
                        RadParams.isEMB = false;
                        new DevExpres.Tutor.WinLic();
                    }
                    else
                    {
                        RadParams.isEMB = true;
                    }

                }
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
            }

            this.Show();
            if (FrameworkParams.Custom != null)
            {
                FrameworkParams.Custom.InitAferShowDesktop();
            }
        }

        public static void LoadDesktopDev()
        {
            try
            {
                if (__PL__.IsUseLicx)
                {
                    RadParams.isEMB = false;
                    new DevExpres.Tutor.WinLic();
                }
                else
                {
                    RadParams.isEMB = true;

                }
                
                return;
            }
            catch
            {
                FrameworkParams.ExitApplication(FrameworkParams.EXIT_STATUS.ERROR);
            }
            finally
            {
            }
        }

        #region IMainForm Members

        public DevExpress.XtraBars.BarManager GetBarManager()
        {
            return this.barManager1;
        }

        #endregion

        #region Xử lý right click trên Title

        private void SetTabbedMDI(bool isTabbedMDI) {
            xtraTabbedMdiManager1.SetNextMdiChildMode = SetNextMdiChildMode.Windows;
            if (isTabbedMDI) 
                xtraTabbedMdiManager1.MdiParent = this;
            else
                xtraTabbedMdiManager1.MdiParent = null;
        }

        void _tabbedMDIManager_MouseUp(object sender, MouseEventArgs e) {
            try
            {
                DevExpress.XtraTab.ViewInfo.BaseTabHitInfo info = xtraTabbedMdiManager1.CalcHitInfo(e.Location);
                if (e.Button != MouseButtons.Right || info.HitTest != DevExpress.XtraTab.ViewInfo.XtraTabHitTest.PageHeader)
                    return;
                IXtraTabPage page = info.Page;
                
                //Các Item dùng chung cho tất cả TabPage.
                if (barManager == null){
                    InitPopupMainMenu(this);
                }

                if (titleMenu == null)
                {
                    titleMenu = new RightClickTitleBarWindow(this, popupMenu, (XtraMdiTabPage)page);
                }
                else {
                    if (titleMenu.tagPage.Equals((XtraMdiTabPage)page) == false)
                    {
                        titleMenu.deleteAllItem(barManager);
                        titleMenu = new RightClickTitleBarWindow(this, popupMenu, (XtraMdiTabPage)page);
                    }
                }
                popupMenu.ShowPopup(Control.MousePosition);

            }
            catch { }
        }
        BarManager barManager = null;
        PopupMenu popupMenu = null;
        RightClickTitleBarWindow titleMenu = null;              

        private void InitPopupMainMenu(XtraForm form)
        {
            //Init Manager for PopupMenu
            barManager = new BarManager();
            barManager.Form = form;
            barManager.UseAltKeyForMenu = true;

            //Init PopupMenu
            popupMenu = new PopupMenu();
            popupMenu.Manager = barManager;

            //Init Reset item
            BarButtonItem close_other_item = new BarButtonItem();
            close_other_item.Caption = "Đóng các màn hình khác";
            close_other_item.ItemClick += new ItemClickEventHandler(mnuCloseOther_Click);
            popupMenu.ItemLinks.Add(close_other_item);

            //Init Copy_FURL item
            BarButtonItem close_all_item = new BarButtonItem();
            close_all_item.Caption = "Đóng tất cả màn hình";
            close_all_item.ItemClick += new ItemClickEventHandler(mnuCloseAll_Click);
            popupMenu.ItemLinks.Add(close_all_item);

            if (FrameworkParams.isSupportDeveloper)
            {
                BarButtonItem form_info_item = new BarButtonItem();
                form_info_item.Caption = RightClickTitleBarDialog.MENU_TITLE_FORM_INFO_TEXT;
                form_info_item.ItemClick += new ItemClickEventHandler(form_info_item_ItemClick);
                popupMenu.ItemLinks.Add(form_info_item, true);
            }
        }

        

        void mnuCloseTab_Click(object sender, ItemClickEventArgs e)
        {
            XtraMdiTabPage page = this.xtraTabbedMdiManager1.SelectedPage;
            if (page != null)
            {
                page.MdiChild.Close();
            }
        }
        void mnuCloseOther_Click(object sender, ItemClickEventArgs e)
        {
            XtraMdiTabPage page = this.xtraTabbedMdiManager1.SelectedPage;
            if (page != null)
            {
                XtraMdiTabPageCollection pages = xtraTabbedMdiManager1.Pages;
                for (int pg = pages.Count - 1; pg >= 0; pg--)
                {
                    if (pages[pg] != page && RightClickTitleBarHelper.isDesktopForm(pages[pg].MdiChild) == false)
                        pages[pg].MdiChild.Dispose();
                }
            }
        }
        void mnuCloseAll_Click(object sender, ItemClickEventArgs e)
        {
            XtraMdiTabPage page = this.xtraTabbedMdiManager1.SelectedPage;
            if (page != null)
            {
                XtraMdiTabPageCollection pages = xtraTabbedMdiManager1.Pages;
                for (int pg = pages.Count - 1; pg >= 0; pg--)
                {
                    if (RightClickTitleBarHelper.isDesktopForm(pages[pg].MdiChild) == false)
                        pages[pg].MdiChild.Dispose();
                }
            }
        }
        #endregion        

        #region Gắn nút About or Help
        //Cố định để hiển thị phần giúp đỡ của toàn bộ hệ thống.
        internal static void CreateAboutOrHelp(frmRibbonMain form)
        {
            DevExpress.XtraBars.BarButtonItem iAbout;
            iAbout = new DevExpress.XtraBars.BarButtonItem();
            iAbout.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            iAbout.PaintStyle = BarItemPaintStyle.CaptionGlyph;
            iAbout.Glyph = ImageCollectionMan.Instance.GetImage1616(25);
            if (File.Exists(RadParams.HELP_FILE) == false)
            {
                iAbout.Caption = "Phân mềm";
                iAbout.Description = "Thông tin phần mềm.";
                iAbout.Hint = "Nhận thông tin về phần mềm\n" + HelpApplication.getProductName();
                iAbout.Name = "iAbout";
            }
            else
            {
                iAbout.Caption = "Trợ giúp " + HelpApplication.getProductName() + "(F1)";
                iAbout.Description = "Hướng dẫn sử dụng";
                iAbout.Hint = "Nhận trợ giúp sử dụng\n" + HelpApplication.getProductName();
                iAbout.Name = "iHelp";
            }
            iAbout.ItemClick += delegate(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
            {
                if (File.Exists(RadParams.HELP_FILE)==false)
                {
                    frmPLAbout dlg = new frmPLAbout();
                    ProtocolForm.ShowModalDialog(form, dlg);
                }
                else
                {
                    PLHelp.openCHM();
                }
            };
            form.Ribbon.Items.Add(iAbout);
            form.Ribbon.PageHeaderItemLinks.Add(iAbout);
        }        
        #endregion

        #region Add website
        internal static void _AddPROTOCOLVNCOM(frmRibbonMain form)
        {
            DevExpress.XtraBars.BarStaticItem www = new DevExpress.XtraBars.BarStaticItem();
            //DevExpress.XtraBars.BarButtonItem www = new DevExpress.XtraBars.BarButtonItem();
            www.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            www.Appearance.ForeColor = System.Drawing.Color.Blue;
            www.Appearance.Options.UseForeColor = true;
            www.ShowImageInToolbar = true;
            www.Appearance.Options.UseFont = true;
            www.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            www.Caption = "www.protocolvn.com";
            www.Name = "www";
            www.TextAlignment = System.Drawing.StringAlignment.Near;
            www.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            www.ItemClick += delegate(object sender, ItemClickEventArgs e)
            {
                System.Diagnostics.Process.Start("http://www.protocolvn.com");
            };
            www.Appearance.Options.UseImage = true;
            www.Glyph = FWImageDic.LOGO_IMAGE16;
            www.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            form.Ribbon.Items.Add(www);
            form.Ribbon.PageHeaderItemLinks.Add(www);
            
        }
        #endregion

        #region Xử lý RightClick trên MDIChild Form.
        void form_info_item_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraMdiTabPage page = this.xtraTabbedMdiManager1.SelectedPage;
            RightClickTitleBarHelper.showFormInfo(page.MdiChild);
        }
        #endregion
    }
}