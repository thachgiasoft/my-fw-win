using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
using System.Diagnostics;
namespace ProtocolVN.Framework.Win
{
    //PHUOCNT: Tại sao qua XTRAFORMPL bị lỗi mở form
    sealed public partial class frmFWConfigDBSecurity : XtraFormPublicPL
    {
        //Lưu thông tin cấu hình ẩn.
        private ConfigDB config;

        private const string dbEMBServer = "DATA-PROTOCOL";
        //Lưu thông tin cấu hình nhúng.
        private string strServerName = dbEMBServer;
        private string strUsername = "SYSDBA";
        private string strPassword = "masterkey";
        private int strCong = 3050;

        private string strDatabase;
        private static string DB_DEMO = RadParams.RUNTIME_PATH + "\\data\\" + getExecutableName() + "-demo.gdb";
        private static string DB_APP = RadParams.RUNTIME_PATH + "\\data\\" + getExecutableName() + ".gdb";

        public frmFWConfigDBSecurity()
        {
            InitializeComponent();
            this.Icon = FrameworkParams.ApplicationIcon;
            setVisibleDungEMBCSDLCucBo(false);
            setVisibleDungCSDLTuXa(false);

            this.initData(true);
            //PLKey key = new PLKey(this);
            //key.Add(Keys.F5, ShowConfigFile);
            //key.Add(Keys.F6, GetRuntimeDBConf);
        }

        private static string getExecutableName()
        {
            return System.Reflection.Assembly.GetEntryAssembly().GetName().Name.ToLower();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            bool current = RadParams.isEmbeded;
            bool result = false;
            try
            {
                if (config.database == "") this.getData();
                FrameworkParams.wait = new WaitingMsg("Vui lòng chờ trong giây lát!", "Đang kiểm tra kết nối");
                
                if (chkCSDLTuXa.Checked)
                {
                    RadParams.isEmbeded = false;
                }
                else
                {
                    RadParams.isEmbeded = true;
                }
                result = config.checkConnectDB();
            }
            catch { }
            finally {
                if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
                if (result) FWMsgBox.showValidConnect();
                else FWMsgBox.showInvalidConnectServer(this);
                RadParams.isEmbeded = current;
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
                chkCSDLTuXa.Enabled = true;
                chkCSDLMau.Enabled = true;
                embDB.Enabled = true;
            }
            else if (btnSave.Text == "Lưu")
            {
                if (config.database == "") getData();
                RadParams.usingDBConfigFile = false;
                if (chkCSDLTuXa.Checked == true && PLMessageBox.ShowConfirmMessage("Bạn có muốn ẩn thông tin kết nối và xuất ra file cấu hình kết nối?") == DialogResult.Yes)
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

                //
                if (config.server.Equals(dbEMBServer))
                {
                    RadParams.isEmbeded = true;
                }
                else
                {
                    RadParams.isEmbeded = false;
                }

                showLoginForm();
            }
        }

        private void showLoginForm()
        {
            this.Dispose();

            //frmLogin myFrmLogin = new frmLogin();
            //myFrmLogin.ShowDialog();
            frmLogin.frmLoginInstance.Show();
        }

        private void initData(bool IsFile)
        {
            if (IsFile)
            {
                //if (Debugger.IsAttached == false)
                //{
                    config = new ConfigDB();
                    config.load();
                //}
                //else
                //{
                //    config = new ConfigDB();
                //    config.server = "";
                //    config.databaseName = "";
                //    config.username = "";
                //    config.password = "";
                //    config.database = "";
                //}
            }

            if (config.server == dbEMBServer)
            {
                if (config.databaseName == DB_DEMO)
                {
                    chkCSDLMau.Checked = true;
                }
                else if (config.databaseName == DB_APP)
                {
                    embDB.Checked = true;
                }
            }
            else
            {
                chkCSDLTuXa.Checked = true;
            }

            //Check it
            if (config.database == "")
            {
                if (chkCSDLTuXa.Checked)
                {
                    textServerName.EditValue = config.server;
                    textDatabase.EditValue = config.databaseName;
                    textUsername.EditValue = config.username;
                    textPassword.EditValue = config.password;
                    Cong.Text = config.port + "";
                }
                else
                {
                    textServerName.EditValue = "";
                    textDatabase.EditValue = "";
                    textUsername.EditValue = "";
                    textPassword.EditValue = "";
                    Cong.Text = "";
                }

                textServerName.Properties.ReadOnly = false;
                textDatabase.Properties.ReadOnly = false;
                //textDatabase.Enabled = true;
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
                //textDatabase.Enabled = false;
                textUsername.Properties.ReadOnly = true;
                textPassword.Properties.ReadOnly = true;
                Cong.Properties.ReadOnly = true;

                btnSave.Text = "Mới";
                
                chkCSDLTuXa.Enabled = false;
                chkCSDLMau.Enabled = false;
                embDB.Enabled = false;
            }

            
        }

        private void trimAllData()
        {
            textServerName.Text = textServerName.Text.Trim();
            textDatabase.Text = textDatabase.Text.Trim();
            textUsername.Text = textUsername.Text.Trim();
            textPassword.Text = textPassword.Text.Trim();
            Cong.Text = Cong.Text.ToString();
        }
        
        private void getData()
        {
            trimAllData();

            if (chkCSDLMau.Checked)
            {
                config.server = this.strServerName;
                config.databaseName = DB_DEMO;
                config.username = this.strUsername;
                config.password = this.strPassword;
                config.port = this.strCong;
            }
            else if (chkCSDLTuXa.Checked)
            {
                config.server = textServerName.Text;
                config.databaseName = textDatabase.Text;
                config.username = textUsername.Text;
                config.password = textPassword.Text;
                config.port = HelpNumber.ParseInt32(Cong.Text);
            }
            else if(embDB.Checked){
                config.server = this.strServerName;
                config.databaseName = this.buttonEdit1.Text;
                config.username = this.strUsername;
                config.password = this.strPassword;
                config.port = this.strCong;
            }
            
        }
        
        private void buttonEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Cơ sở dữ liệu";
            dlg.Filter = "Tập tin cơ sở dữ liệu (*.gdb)|*.gdb|Tập tin cơ sở dữ liệu (*.fdb)|*.fdb|Tất cả (*.*)|*.*";
            string dir = System.Environment.CurrentDirectory;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (embDB.Checked == true)//Cục bộ
                    buttonEdit1.Text = dlg.FileName;                    
                else//Từ xa
                    textDatabase.Text = dlg.FileName;
                System.Environment.CurrentDirectory = dir;
            }
        }

        private void frmConfigDB_Load(object sender, EventArgs e)
        {
            //this.btnSave.Image = FWImageDic.SAVE_IMAGE16;
            //this.btnClose.Image = FWImageDic.CLOSE_IMAGE16;
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

                chkCSDLTuXa.Checked = true;
            }
        }

        
        
        
        




        private void embDB_CheckedChanged(object sender, EventArgs e)
        {
            if (embDB.Checked == true)
            {
                this.chkCSDLTuXa.Checked = false;
                this.chkCSDLMau.Checked = false;
                setVisibleDungEMBCSDLCucBo(true);
            }
            else
                setVisibleDungEMBCSDLCucBo(false);
        }

        private void chkCSDLTuXa_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCSDLTuXa.Checked == true)
            {
                this.embDB.Checked = false;
                this.chkCSDLMau.Checked = false;
                setVisibleDungCSDLTuXa(true);
            }
            else
                setVisibleDungCSDLTuXa(false);
        }

        private void chkEMBCSDLMau_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCSDLMau.Checked == true)
            {
                this.embDB.Checked = false;
                this.chkCSDLTuXa.Checked = false;
            }
            else
            {
                //...   
            }
        }

        private void setVisibleDungEMBCSDLCucBo(bool flag)
        {
            if (flag)
            {
                buttonEdit1.Text = DB_APP;
                //buttonEdit1.Visible = false;
                buttonEdit1.Enabled = false;
                //buttonEdit1.Focus();
            }
            else {
                buttonEdit1.Enabled = false;
            }
        }

        private void setVisibleDungCSDLTuXa(bool flag)
        {
            if (flag)
            {
                this.textServerName.Enabled = true;
                this.Cong.Enabled = true;
                this.textDatabase.Enabled = true;
                this.textPassword.Enabled = true;
                this.textUsername.Enabled = true;
                this.textServerName.Enabled = true;
                this.textServerName.Focus();

                //this.btnLoadConfigFile.Visible = true;
            }
            else 
            {
                this.textServerName.Enabled = false;
                this.Cong.Enabled = false;
                this.textDatabase.Enabled = false;
                this.textPassword.Enabled = false;
                this.textUsername.Enabled = false;
                this.textServerName.Enabled = false;

                //this.btnLoadConfigFile.Visible = false;
            }
        }


        #region Show Hidden Info
        private void GetRuntimeDBConf()
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
                    HelpMsgBox.ShowNotificationMessage("Mật khẩu không chính xác.");
                }
            }
        }

        private void ShowConfigFile()
        {
            InputBoxResult test = PLInputBox.Show("Xin vui lòng nhập vào mật khẩu để xem nội dung của tập tin cấu hình"
              , "Mật khẩu tập tin cấu hình");

            if (test.ReturnCode == DialogResult.OK)
            {
                if (test.Text.Trim().Equals(DateTime.Now.ToString("dd/MM/yyyy").ToString()))
                {
                    HelpMsgBox.ShowNotificationMessage(
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
                    HelpMsgBox.ShowNotificationMessage("Mật khẩu không chính xác.");
                }
            }
        }
        #endregion
    }
}