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
    public partial class frmOldFWRunFirst : XtraFormPublicPL, INonLog
    {
        public frmOldFWRunFirst()
        {
            InitializeComponent();
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

        private void labelControl4_Click(object sender, EventArgs e)
        {
            //Khai bao to chuc
            string xml =
            @"<?xml version='1.0' encoding='utf-8' standalone='yes'?>
                <basiccats>
                  <group id ='1'>
                    <lang id='vn'>Sơ đồ tổ chức</lang>
                    <lang id='en'></lang>
                    " + DMFWNhanVien.I.Item() + @" 
                    " + DMFWPhongBan.I.Item() + @"
                  </group>
                </basiccats>
            ";
            frmCategory frm = new frmCategory(xml);
            ProtocolForm.ShowWindow(FrameworkParams.MainForm, frm, false);
            this.Close();
        }

        private void lbphienban_Click(object sender, EventArgs e)
        {
            //Khai bao danh muc
            frmCategory frm = new frmCategory();
            ProtocolForm.ShowWindow(FrameworkParams.MainForm, frm, false);
            this.Close();
        }

        private void labelControl3_Click(object sender, EventArgs e)
        {
            //Cau hinh tuy bien
            frmXPOption frm = new frmXPOption();
            ProtocolForm.ShowModalDialog(this, frm);
        }

        private void labelControl5_Click(object sender, EventArgs e)
        {
            //Cau hinh tham so in an
            frmAppReportParams dlg = new frmAppReportParams();
            ProtocolForm.ShowModalDialog(this, dlg);
        }

        private void labelControl7_Click(object sender, EventArgs e)
        {
            //Huong dan su dung san pham
            if (RadParams.HELP_FILE == null)
            {
                frmPLAbout dlg = new frmPLAbout();
                ProtocolForm.ShowModalDialog(FrameworkParams.MainForm, dlg);
            }
            else
            {
                PLHelp.openCHM();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}