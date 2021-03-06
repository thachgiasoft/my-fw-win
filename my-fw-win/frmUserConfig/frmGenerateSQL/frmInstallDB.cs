using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
using ProtocolVN.Framework.Win;
namespace ProtocolVN.Framework.Win
{
    public enum DbOperation
    {
        Install  = 0,
        Uninstall = 1
    }
    public partial class frmInstallDB : DevExpress.XtraEditors.XtraForm
    {
        private string Msg = "";
        private bool _checkdb = true;
        IPLInstall _FWInstall = null;

        public frmInstallDB(IPLInstall install)
        {
            _FWInstall = install;
            InitializeComponent();
            Check();
        }
        public frmInstallDB(IPLInstall install, DbOperation type)
        {
            _FWInstall = install;
            InitializeComponent();
            Check();
            if (type == DbOperation.Install)
                InstallDb();
            else
                UninstallDb();
        }

        private void InstallDb()
        {
            UnInstall.Checked = false;
            this.SqlScript.Text = _FWInstall.GetInstallSQL();
        }

        private void UninstallDb()
        {
            Install.Checked = false;
            this.SqlScript.Text = _FWInstall.GetUnInstallSQL();
        }
        private void btn_Check_Click(object sender, EventArgs e)
        {
            Check();
        }

        private void btn_Yes_Click(object sender, EventArgs e)
        {
            if (this.SqlScript.Text.Trim().Length == 0)
            {
                PLMessageBox.ShowErrorMessage("Vui lòng chọn một SQLScript để thực hiện.");
                return;
            }
            bool IsFinish = true;
            if (Install.Checked)
                Msg = "Bạn có muốn cài đặt dữ liệu không ?";
            else
                Msg = "Bạn có muốn hủy bỏ dữ liệu không ?";
            if (PLMessageBox.ShowConfirmMessage(Msg)== DialogResult.Yes)
            {
                try
                {
                    FrameworkParams.wait = new WaitingMsg();
                    IsFinish = DatabaseFBExt.RunStringSQLScript(this.SqlScript.Text);
                }
                finally
                {
                    if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
                }
                if (IsFinish)
                {
                    HelpMsgBox.ShowNotificationMessage("Thực hiện thành công");
                }
                else
                {
                    HelpMsgBox.ShowNotificationMessage("Thực hiện không thành công");
                }
            }    
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Install_CheckedChanged(object sender, EventArgs e)
        {
            if (Install.CheckState == CheckState.Checked)
                InstallDb();
        }

        private void UnInstall_CheckedChanged(object sender, EventArgs e)
        {
            if (UnInstall.CheckState == CheckState.Checked)
                UninstallDb();
        }
       
        private void Check()
        {
            _checkdb = DatabaseFBExt.RunStringSQLScript(_FWInstall.CheckSQL());
            if (_checkdb)
            {
                this.lbl_TinhTrang.Text = "Dữ liệu đang ổn định";
            }
            else
            {
                this.lbl_TinhTrang.Text = "Dữ liệu không ổn định";
            }
        }

    
    }
}