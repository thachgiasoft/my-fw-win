using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
namespace ProtocolVN.Framework.Win
{
    sealed public partial class frmPLAbout : XtraFormPublicPL, INonLog
    {
        public frmPLAbout()
        {
            InitializeComponent();
            this.ShowIcon = false;

            HelpXtraForm.SetSlideEffect(this);
            this.TenCty.Text = FrameworkParams.CustomerName;
            this.SanPham.Text = FrameworkParams.ProductName;
            this.PhienBan.Text += HelpApplication.getVersion();
            PLKey key = new PLKey(this);
            key.Add(Keys.F5, showLicence);
        }

        private void showLicence()
        {
            frmLicence lic = new frmLicence();
            ProtocolForm.ShowModalDialog(this, lic);
        }

        

        private void frmAbout_MouseClick(object sender, MouseEventArgs e)
        {
            //this.Close();
        }

        private void frmAbout_KeyPress(object sender, KeyPressEventArgs e)
        {
            //this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLicence_Click(object sender, EventArgs e)
        {
            if (__PL__.IsUseLicx)
            { new DevExpres.Tutor.WinLicNew(); }

            //showLicence();
            //String txt = (new DevExpres.Tutor.WinLicNew()).getInfo().ToString();
            //try
            //{
            //    //this.licInfo.Text = ((DevExpres.Tutor.ILicence)FrameworkParams.Lic).getLicenceInfo().ToString();
            //    this.licInfo.Text = txt;
            //}
            //catch
            //{
            //    this.licInfo.Text = "Giấy phép đã hết hạn sử dụng. Xin vui lòng liên hệ Công ty phần mềm PROTOCOL (www.protocolvn.com).";
            //}
        }
        
        private void frmPLAbout_Load(object sender, EventArgs e)
        {
            HelpXtraForm.SetFix(this);
            
            this.Text = "";
        }

        private void email_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            try{
                string EmailAddress = "sales@protocolvn.com";
                System.Diagnostics.Process.Start(String.Format("mailto:{0}", EmailAddress));
            }
            catch { }
        }

        private void kt_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("ymsgr:sendIM?kt.protocol");
            }
            catch { }
        }

        private void kd_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("ymsgr:sendIM?kd.protocol");
            }
            catch { }
        }

        private void dksd_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            frmDieuKhoanSuDung dk = new frmDieuKhoanSuDung();
            HelpProtocolForm.ShowModalDialog(this, dk);
        }       
    }
}
