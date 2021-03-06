using System;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    public partial class frmPermissionFail : XtraForm, IPublicForm
    {
        public frmPermissionFail()
        {
            //Tránh trường hợp có màn hình chờ mà thông báo màn hình Lỗi phân quyền.
            if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();

            InitializeComponent();
            this.TopLevel = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPermissionFail_Load(object sender, EventArgs e)
        {
            HelpXtraForm.SetFix(this);
        }        
    }
}
