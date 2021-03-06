using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
namespace ProtocolVN.Framework.Win
{
    sealed public partial class frmOldPLAbout : XtraForm, INonLog, IPublicForm
    {
        public frmOldPLAbout()
        {
            InitializeComponent();
            HelpXtraForm.SetSlideEffect(this);
            this.TenCty.Text = FrameworkParams.CustomerName;
            this.SanPham.Text = FrameworkParams.ProductName;
        }

        private void frmAbout_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void frmAbout_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLicence_Click(object sender, EventArgs e)
        {
            frmLicence lic = new frmLicence();
            ProtocolForm.ShowModalDialog(this, lic);
        }

        private void frmPLAbout_Load(object sender, EventArgs e)
        {
            HelpXtraForm.SetFix(this);
        }

        private void email_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            try{
                string EmailAddress = "sales@protocolvn.com";
                System.Diagnostics.Process.Start(String.Format("mailto:{0}", EmailAddress));
            }
            catch { }
        }
    }
}
