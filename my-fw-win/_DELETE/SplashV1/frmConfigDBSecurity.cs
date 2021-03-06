using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
namespace ProtocolVN.Framework.Win
{
    public partial class frmConfigDBSecurity : XtraForm
    {
        private ConfigDB config;

        public frmConfigDBSecurity()
        {
            InitializeComponent();
            this.Icon = FrameworkParams.ApplicationIcon;
            this.initData(true);
            PLKey key = new PLKey(this);
            key.Add(Keys.F9, ShowConfigFile);
            key.Add(Keys.F8, GetRuntimeDBConf);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if(config.database=="")
                this.getData();

            if (config.checkConnectDB())
            {
                FWMsgBox.showValidConnect();
            }
            else
            {
                FWMsgBox.showInvalidConnectServer();
            }                
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            showLoginForm();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Mới")
            {
                config.server = "";
                config.databaseName = "";
                config.username = "SYSDBA";
                config.password = "";
                config.port = 3050;
                config.database = "";

                initData(false);
                btnSave.Text = "Lưu";
            }
            else if (btnSave.Text == "Lưu")
            {
                if (config.database == "") getData();
                RadParams.usingDBConfigFile = false;
                if (PLMessageBox.ShowConfirmMessage("Bạn có muốn ẩn thông tin kết nối?") == DialogResult.Yes)
                {
                    config.database = "Security;" + DateTime.Now.ToString("dd/MM/yyyy");
                    config.update();
                    SaveFileDialog dlg = new SaveFileDialog();
                    dlg.Title = "Cấu hình kết nối";
                    dlg.Filter = "Tập tin cấu hình kết nối (*.cpl)|*.cpl|Tất cả (*.*)|*.*";
                    string dir = System.Environment.CurrentDirectory;

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        System.Environment.CurrentDirectory = dir;
                        try
                        {
                            System.IO.File.Copy(dir + @"\conf\db.cpl", dlg.FileName, true);
                        }
                        catch { }
                    }
                }
                else
                {
                    config.update();
                }
                showLoginForm();
            }
        }

        private void showLoginForm()
        {
            this.Dispose();

            frmLogin myFrmLogin = new frmLogin();
            myFrmLogin.ShowDialog();
        }

        public void initData(bool IsFile)
        {
            if(IsFile){
                config = new ConfigDB();
                config.load();
            }

            //Check it
            if (config.database == "")
            {
                textServerName.EditValue = config.server;
                textDatabase.EditValue = config.databaseName;
                textUsername.EditValue = config.username;
                textPassword.EditValue = config.password;
                Cong.Text = config.port + "";

                textServerName.Properties.ReadOnly = false;
                textDatabase.Properties.ReadOnly = false;
                textDatabase.Enabled = true;
                textUsername.Properties.ReadOnly = false;
                textPassword.Properties.ReadOnly = false;
                Cong.Properties.ReadOnly = false;

                btnSave.Text = "Lưu";
            }
            else if (config.database.StartsWith("Security"))
            {
                textServerName.EditValue = "-- protocolvn.com --";
                textDatabase.EditValue = "-- protocolvn.com --";
                textUsername.EditValue = "-- protocolvn.com --";
                textPassword.EditValue = "-- protocolvn.com --";
                Cong.Text = "3050";

                textServerName.Properties.ReadOnly = true;
                textDatabase.Properties.ReadOnly = true;
                textDatabase.Enabled = false;
                textUsername.Properties.ReadOnly = true;
                textPassword.Properties.ReadOnly = true;
                Cong.Properties.ReadOnly = true;

                btnSave.Text = "Mới";
            }
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

        private void btnLoadConfigFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Cấu hình kết nối";
            dlg.Filter = "Tập tin cấu hình kết nối (*.cpl)|*.cpl|Tất cả (*.*)|*.*";
            string dir = System.Environment.CurrentDirectory;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                System.Environment.CurrentDirectory = dir;
                try
                {
                    System.IO.File.Copy(dlg.FileName, dir + @"\conf\db.cpl", true);
                }
                catch { }

                initData(true);
            }
        }

        public void GetRuntimeDBConf()
        {
            InputBoxResult test = PLInputBox.Show("Xin vui lòng nhập vào mật khẩu để xem nội dung của tập tin cấu hình."
              , "Mật khẩu tập tin cấu hình");

            if (test.ReturnCode == DialogResult.OK)
            {
                if (test.Text.Trim().StartsWith(DateTime.Now.ToString("MM/dd/yyyy").ToString()))
                {
                    try
                    {
                        string[] twostring = test.Text.Trim().Split(';');
                        config.database = "Security;" + twostring[1];
                        //config.databaseName = null;
                        config.databaseName = RadParams.RUNTIME_PATH + @"\" + twostring[1];

                        config.update();
                    }
                    catch
                    {
                    }
                }
                else
                {
                    PLMessageBox.ShowNotificationMessage("Mật khẩu không chính xác.");
                }
            }
        }

        public void ShowConfigFile()
        {            
            InputBoxResult test = PLInputBox.Show("Xin vui lòng nhập vào mật khẩu để xem nội dung của tập tin cấu hình"
              , "Mật khẩu tập tin cấu hình");

            if (test.ReturnCode == DialogResult.OK)
            {
                if (test.Text.Trim().Equals(DateTime.Now.ToString("dd/MM/yyyy").ToString()))
                {
                    PLMessageBox.ShowNotificationMessage(
                        "Server: " + config.server + "\n" +
                        "DB Name: " + config.databaseName + "\n" +
                        "UserName: " + config.username + "\n" +
                        "Password: " + config.password + "\n" +
                        "Port: " + config.port + "\n" +
                        "Database: " + config.database
                    );
                }
                else
                {
                    PLMessageBox.ShowNotificationMessage("Mật khẩu không chính xác.");
                }
            }
        }             
    }
}