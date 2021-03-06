using System;
using System.Windows.Forms;
using DevExpress.XtraEditors.DXErrorProvider;
using ProtocolVN.Framework.Core;
namespace ProtocolVN.Framework.Win
{
    public partial class frmChangePwdEN : DevExpress.XtraEditors.XtraForm
    {
        private User user;
        DXErrorProvider error;
        public frmChangePwdEN()
        {
            InitializeComponent();
            initData();
            this.error = GUIValidation.GetErrorProvider(this);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                user.password = textPasswordNew.Text;
                if (user.updatePassword())
                    FWMsgBox.showValidChangePassword();
                else
                    FWMsgBox.showInvalidChangePassword();
                this.Close();
            }
            else
            {
            }
        }

        public void initData()
        {
            user = new User();
            user.loadCookies();
            user.loadByUserName();
        }

        public void trimAllData()
        {
            textPasswordNew.Text = textPasswordNew.Text.Trim();
            textPasswordNewConfirm.Text = textPasswordNewConfirm.Text.Trim();
            textPasswordOld.Text = textPasswordOld.Text.Trim();
        }

        public bool validate()
        {
            trimAllData();
            error.ClearErrors();
            bool flag = true;
            if (HelpIsCheck.isBlankString(textPasswordNew.Text))
            {
                error.SetError(textPasswordNew, ErrorMsgLib.errorRequired("Mật khẩu"));
                flag = false;
            }

            if (HelpIsCheck.isBlankString(textPasswordNewConfirm.Text))
            {
                error.SetError(textPasswordNewConfirm, ErrorMsgLib.errorRequired("Xác nhận mật khẩu"));
                flag = false;
            }

            if (!HelpIsCheck.isValidPassword(textPasswordNew.Text, textPasswordNewConfirm.Text))
            {
                error.SetError(textPasswordNew, ErrorMsgLib.errorPassword(""));
                flag = false;
            }

            if (!DAUser.Instance.checkPassword(user.username, textPasswordOld.Text))
            {
                error.SetError(textPasswordOld, "Mật khẩu cũ không chính xác");
                flag = false;
            }

            return flag;
        }

        private void frmChangePwd_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            
            //this.btnOK.Image = FWImageDic.SAVE_IMAGE16;
            //this.btnClose.Image = FWImageDic.CLOSE_IMAGE16;
        }
    }
}