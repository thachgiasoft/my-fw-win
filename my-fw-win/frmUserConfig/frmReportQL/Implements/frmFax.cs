using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public partial class frmFax : XtraForm, IPublicForm
    {
        private string FileName;
        
        public frmFax(string fileName)
        {
            InitializeComponent();
            FileName = fileName;
        }

        public frmFax(string docName, string fileName, string recipientName, string faxNumber)
        {
            InitializeComponent();
            this.textEditDoc.Text = docName;
            FileName = fileName;
            this.comboBoxEditRecipient.Text = recipientName;
            this.comboBoxEditFaxNumber.Text = faxNumber;
        }

        private void simpleButtonSend_Click(object sender, EventArgs e)
        {
            if (TestInput())
            {
                if (HelpFax.SendPdf(this.textEditDoc.Text.Trim(), this.FileName, this.comboBoxEditRecipient.Text.Trim(), this.comboBoxEditFaxNumber.Text.Trim()))
                    HelpMsgBox.ShowNotificationMessage("Gửi thành công");
                else
                    HelpMsgBox.ShowNotificationMessage("Không gửi được");
            }
        }

        private bool TestInput()
        {
            if (this.textEditDoc.Text.Trim() != ""
                && this.comboBoxEditRecipient.Text.Trim() != ""
                && this.comboBoxEditFaxNumber.Text.Trim() != "")
            {
                return true;
            }
            else
            {
                HelpMsgBox.ShowNotificationMessage("Dữ liệu nhập không hợp lệ");
                return false;
            }
        }

        private void simpleButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmFax_Load(object sender, EventArgs e)
        {
            HelpXtraForm.SetFix(this);
        }
    }
}