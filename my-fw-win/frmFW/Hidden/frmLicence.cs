using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    sealed public partial class frmLicence : DevExpress.XtraEditors.XtraForm
    {
        public frmLicence()
        {
            InitializeComponent();
            String txt = (new DevExpres.Tutor.WinLic()).getInfo().ToString();
            
            try
            {
                //this.licInfo.Text = ((DevExpres.Tutor.ILicence)FrameworkParams.Lic).getLicenceInfo().ToString();
                this.licInfo.Text = txt;
            }
            catch
            {
                this.licInfo.Text = "Giấy phép đã hết hạn sử dụng. Xin vui lòng liên hệ Công ty phần mềm PROTOCOL (www.protocolvn.com).";
            }
            this.TenCty.Text = FrameworkParams.CustomerName;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLicence_Click(object sender, EventArgs e)
        {
            String txt = (new DevExpres.Tutor.WinLicNew()).getInfo().ToString();

            try
            {
                //this.licInfo.Text = ((DevExpres.Tutor.ILicence)FrameworkParams.Lic).getLicenceInfo().ToString();
                this.licInfo.Text = txt;
            }
            catch
            {
                this.licInfo.Text = "Giấy phép đã hết hạn sử dụng. Xin vui lòng liên hệ Công ty phần mềm PROTOCOL (www.protocolvn.com).";
            }
        }

        private void frmLicence_Load(object sender, EventArgs e)
        {
            HelpXtraForm.SetFix(this);
        }
    }
}