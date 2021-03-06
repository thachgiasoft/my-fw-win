using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.DanhMuc;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public partial class frmFWRunExit : XtraFormPublicPL, INonLog
    {
        public bool? isExit = null;

        public frmFWRunExit()
        {
            InitializeComponent();
            this.SanPham.Text = FrameworkParams.ProductName;
            this.PhienBan.Text += HelpApplication.getVersion();
            this.ShowIcon = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRunFirst_Load(object sender, EventArgs e)
        {
            HelpXtraForm.SetFix(this);
        }
        
        private void pictureEdit1_Click_1(object sender, EventArgs e)
        {
            try{
                System.Diagnostics.Process.Start("ymsgr:sendIM?kt.protocol");
            }
            catch { }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.isExit = false;
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.isExit = true;
            this.Close();
        }

        public static bool confirmExit(){
            frmFWRunExit form = new frmFWRunExit();
            HelpXtraForm.ShowUserModalDialog(FrameworkParams.MainForm, form);

            if (form.Tag!=null && form.Tag.ToString().Equals("NO"))
                return true;

            if (form.isExit == null || form.isExit == true )
                return true;
            else
                return false;
        }
    }
}