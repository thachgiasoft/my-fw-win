using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Win;

namespace ProtocolVN.Framework.Win
{
    //Lớp cho phép hiện thị messagebox. Chú ý chỉ dùng lớp này cho việc hiển thị thông báo lỗi.    
    [Obsolete("Không sử dụng")]
    public class PLMessageBoxWin : XtraForm
    {
        private SimpleButton sb2;
        private SimpleButton sb1;
        private PictureEdit pictureEdit1;
        private Label lbText;
        private PanelControl pc1;
        public int Kq;

        public PLMessageBoxWin()
        {
            InitializeComponent();
            Kq = 0;
            //this.TopMost = true;
            this.TopLevel = true;
        }

        public PLMessageBoxWin(string text)
        {
            InitializeComponent();
            Kq = 0;
            //this.TopMost = true;
            this.TopLevel = true;

            this.sb1.Visible = true;
            this.sb2.Visible = false;
            this.pictureEdit1.Image = RadImageDic.MS_INFO_48;

            this.Text = "PROTOCOLVN";
            this.sb1.Text = "Đóng";
            this.lbText.Text = text;

            this.DoAutoPos();
            this.sb1.Location = new System.Drawing.Point(((this.pc1.Width / 2) - 
                (this.sb1.Width / 2)), this.sb1.Location.Y);            
        }

        private void InitializeComponent()
        {
            this.sb2 = new DevExpress.XtraEditors.SimpleButton();
            this.sb1 = new DevExpress.XtraEditors.SimpleButton();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.lbText = new System.Windows.Forms.Label();
            this.pc1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pc1)).BeginInit();
            this.pc1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sb2
            // 
            this.sb2.Location = new System.Drawing.Point(192, 5);
            this.sb2.Name = "sb2";
            this.sb2.Size = new System.Drawing.Size(75, 23);
            this.sb2.TabIndex = 0;
            this.sb2.Text = "Không";
            this.sb2.Click += new System.EventHandler(this.sb2_Click);
            // 
            // sb1
            // 
            this.sb1.Location = new System.Drawing.Point(108, 5);
            this.sb1.Name = "sb1";
            this.sb1.Size = new System.Drawing.Size(75, 23);
            this.sb1.TabIndex = 1;
            this.sb1.Text = "Có";
            this.sb1.Click += new System.EventHandler(this.sb1_Click);
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Location = new System.Drawing.Point(12, 9);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.AllowFocused = false;
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ShowMenu = false;
            this.pictureEdit1.Size = new System.Drawing.Size(54, 51);
            this.pictureEdit1.TabIndex = 7;
            // 
            // lbText
            // 
            this.lbText.AutoSize = true;
            this.lbText.Location = new System.Drawing.Point(83, 9);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(49, 13);
            this.lbText.TabIndex = 6;
            this.lbText.Text = "Message\r\n";
            this.lbText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pc1
            // 
            this.pc1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pc1.Controls.Add(this.sb2);
            this.pc1.Controls.Add(this.sb1);
            this.pc1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pc1.Location = new System.Drawing.Point(0, 71);
            this.pc1.Name = "pc1";
            this.pc1.Size = new System.Drawing.Size(361, 32);
            this.pc1.TabIndex = 0;
            // 
            // PLMessageBox
            // 
            this.AcceptButton = this.sb2;
            this.ClientSize = new System.Drawing.Size(361, 103);
            this.Controls.Add(this.pc1);
            this.Controls.Add(this.pictureEdit1);
            this.Controls.Add(this.lbText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PLMessageBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pc1)).EndInit();
            this.pc1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void sb2_Click(object sender, EventArgs e)
        {
            Kq = 2;
            this.Close();
        }

        private void sb1_Click(object sender, EventArgs e)
        {
            Kq = 1;
            this.Close();
        }

        private void DoAutoPos()
        {
            this.Width = Math.Max(Math.Max((80 + this.lbText.Width + 12), (80 + this.pictureEdit1.Width + 12)), 200);
            this.Height = Math.Max((32 + this.lbText.Height + 24), (32 + this.pictureEdit1.Height + 24)) + 16;

            int x = 80 + ((this.Width - 80 - 12) / 2) - (this.lbText.Width / 2);
            int y = ((this.Height - 32 - 16) / 2) - (this.lbText.Height / 2) - 5;
            this.lbText.Location = new System.Drawing.Point(x, y);
        }        
    }
}
