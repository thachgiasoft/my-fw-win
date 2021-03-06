using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
namespace ProtocolVN.Framework.Win
{
    //Màn hình được thay thế bằng frmConfigDBSecurity
    [Obsolete("Không sử dụng")]
    public partial class frmConfigDB : XtraForm
    {
        private ConfigDB config;

        public frmConfigDB()
        {
            InitializeComponent();
            this.Icon = FrameworkParams.ApplicationIcon;
            this.initData();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            this.getData();

            if (config.checkConnectDB())
            {
                FWMsgBox.showValidConnect();
            }
            else
            {
                FWMsgBox.showInvalidConnectServer(this);                 
            }                
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            showLoginForm();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            getData();
            config.update();
            RadParams.usingDBConfigFile = false;
            showLoginForm();
        }

        private void showLoginForm()
        {
            this.Dispose();

            frmLogin myFrmLogin = new frmLogin();
            myFrmLogin.ShowDialog();
        }

        public void initData()
        {
            config = new ConfigDB();
            config.load();
            //Check it
            textServerName.EditValue = config.server;
            textDatabase.EditValue = config.databaseName;
            textUsername.EditValue = config.username;
            textPassword.EditValue = config.password;
            Cong.Text = config.port + "";
        }

        public void trimAllData()
        {
            textServerName.Text = textServerName.Text.Trim();
            textDatabase.Text = textDatabase.Text.Trim();
            textUsername.Text = textUsername.Text.Trim();
            textPassword.Text = textPassword.Text.Trim();
            Cong.Text = Cong.Text.ToString();
        }
        
        public void getData()
        {
            trimAllData();
            config.server = textServerName.Text;
            config.databaseName = textDatabase.Text;
            config.username = textUsername.Text;
            config.password = textPassword.Text;
            config.port = HelpNumber.ParseInt32(Cong.Text);
        }
        
        private void buttonEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Cơ sở dữ liệu";
            dlg.Filter = "Tập tin cơ sở dữ liệu (*.gdb)|*.gdb|Tập tin cơ sở dữ liệu (*.fdb)|*.fdb|Tất cả (*.*)|*.*";
            string dir = System.Environment.CurrentDirectory;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textDatabase.Text = dlg.FileName;
                System.Environment.CurrentDirectory = dir;
            }
        }

        private void frmConfigDB_Load(object sender, EventArgs e)
        {
            this.btnSave.Image = FWImageDic.SAVE_IMAGE16;
            this.btnClose.Image = FWImageDic.CLOSE_IMAGE16;
        }

        private void MacDinh_Click(object sender, EventArgs e)
        {
            textUsername.Text = "SYSDBA";
            textPassword.Text = "masterkey";
            Cong.Text = "3050";
        }
    }
}