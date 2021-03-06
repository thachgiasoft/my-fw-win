using System;
using System.Data;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
namespace ProtocolVN.Framework.Win
{
    //Đã gắn cấu hình nó trong frmConfigServer
    [Obsolete("Không sử dụng")]
    public partial class frmConfigFTP : XtraForm
    {
        public frmConfigFTP()
        {
            InitializeComponent();
            this.initData();
        }

        private void frmConfigFTP_Load(object sender, EventArgs e)
        {
            HelpXtraForm.SetFix(this);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            this.trimAllData();
            if (HelpFTP.Instance.checkConnect(textServerName.Text,Cong.Text,textUsername.Text,textPassword.Text))
                FWMsgBox.showValidConnect();
            else
                FWMsgBox.showInvalidConnectServer(this);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveData();
            this.Close();
        }

        private void initData()
        {
            DataSet ds = new DataSet();
            ConfigFile.ReadXML(HelpFTP.FTP_FILE , ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                textServerName.EditValue = ds.Tables[0].Rows[0]["ip"];
                textUsername.EditValue = ds.Tables[0].Rows[0]["username"];
                textPassword.EditValue = ds.Tables[0].Rows[0]["password"];
                Cong.Text = ds.Tables[0].Rows[0]["port"].ToString();
            }
        }

        private void trimAllData()
        {
            textServerName.Text = textServerName.Text.Trim();
            textUsername.Text = textUsername.Text.Trim();
            textPassword.Text = textPassword.Text.Trim();
            Cong.Text = Cong.Text.ToString();
        }
        
        private void saveData()
        {
            trimAllData();
            string xmlFile = @"<?xml version='1.0' encoding='utf-8' standalone='yes'?>
                            <NewDataSet>
                              <ftp>
                                <ip>" + textServerName.Text + "</ip>" +
                                "<username>" + textUsername.Text + "</username>" +
                                "<password>" + textPassword.Text + "</password>" +
                                "<port>" + Cong.Text + "</port>" +
                              "</ftp>"+
                            "</NewDataSet>";
            ConfigFile.WriteXML(HelpFTP.FTP_FILE , xmlFile);

            HelpFTP.loadFtp();
        }
    }
}