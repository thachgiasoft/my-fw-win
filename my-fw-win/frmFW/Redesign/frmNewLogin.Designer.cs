
namespace ProtocolVN.Framework.Win
{
    partial class frmNewLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;        

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtUsername = new DevExpress.XtraEditors.TextEdit();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.chkRememberPwd = new DevExpress.XtraEditors.CheckEdit();
            this.btnConfig = new DevExpress.XtraEditors.SimpleButton();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.chkAutoLogin = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.lbaddress = new DevExpress.XtraEditors.LabelControl();
            this.email = new DevExpress.XtraEditors.HyperLinkEdit();
            this.lbemail = new DevExpress.XtraEditors.LabelControl();
            this.hyperLinkEdit1 = new DevExpress.XtraEditors.HyperLinkEdit();
            this.lbdt = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lbtel = new DevExpress.XtraEditors.LabelControl();
            this.lbtencty = new DevExpress.XtraEditors.LabelControl();
            this.btnMoRong = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRememberPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAutoLogin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.email.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(49, 109);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(79, 13);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Tên đăng nhập";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(49, 137);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(51, 13);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Mật khẩu";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(0, 208);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(443, 3);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(138, 106);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(240, 20);
            this.txtUsername.TabIndex = 1;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(138, 132);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(240, 20);
            this.txtPassword.TabIndex = 3;
            // 
            // chkRememberPwd
            // 
            this.chkRememberPwd.Location = new System.Drawing.Point(48, 245);
            this.chkRememberPwd.Name = "chkRememberPwd";
            this.chkRememberPwd.Properties.Caption = "Nhớ mật khẩu";
            this.chkRememberPwd.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Style10;
            this.chkRememberPwd.Size = new System.Drawing.Size(95, 22);
            this.chkRememberPwd.TabIndex = 7;
            // 
            // btnConfig
            // 
            this.btnConfig.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnConfig.Location = new System.Drawing.Point(224, 272);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(168, 23);
            this.btnConfig.TabIndex = 10;
            this.btnConfig.Text = "Dùng máy chủ dữ liệu khác";
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(303, 168);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Thoát";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(210, 168);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(86, 23);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // chkAutoLogin
            // 
            this.chkAutoLogin.Location = new System.Drawing.Point(48, 273);
            this.chkAutoLogin.Name = "chkAutoLogin";
            this.chkAutoLogin.Properties.Caption = "Đăng nhập tự động";
            this.chkAutoLogin.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Style10;
            this.chkAutoLogin.Size = new System.Drawing.Size(123, 22);
            this.chkAutoLogin.TabIndex = 8;
            // 
            // checkEdit1
            // 
            this.checkEdit1.Enabled = false;
            this.checkEdit1.Location = new System.Drawing.Point(221, 245);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Appearance.Options.UseTextOptions = true;
            this.checkEdit1.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.checkEdit1.Properties.Caption = "Dùng máy chủ dữ liệu dùng thử";
            this.checkEdit1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Style10;
            this.checkEdit1.Size = new System.Drawing.Size(185, 22);
            this.checkEdit1.TabIndex = 9;
            this.checkEdit1.ToolTip = "Khi dùng máy chủ dùng thử sẽ không ảnh hưởng đến dự liệu của công ty.";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel1.Controls.Add(this.pictureEdit1);
            this.panel1.Controls.Add(this.lbaddress);
            this.panel1.Controls.Add(this.email);
            this.panel1.Controls.Add(this.lbemail);
            this.panel1.Controls.Add(this.hyperLinkEdit1);
            this.panel1.Controls.Add(this.lbdt);
            this.panel1.Controls.Add(this.labelControl2);
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Controls.Add(this.lbtel);
            this.panel1.Controls.Add(this.lbtencty);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(425, 89);
            this.panel1.TabIndex = 50;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(12, 10);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.AllowFocused = false;
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ReadOnly = true;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEdit1.Size = new System.Drawing.Size(86, 73);
            this.pictureEdit1.TabIndex = 33;
            // 
            // lbaddress
            // 
            this.lbaddress.Appearance.ForeColor = System.Drawing.Color.White;
            this.lbaddress.Appearance.Options.UseForeColor = true;
            this.lbaddress.Location = new System.Drawing.Point(104, 32);
            this.lbaddress.Name = "lbaddress";
            this.lbaddress.Size = new System.Drawing.Size(290, 13);
            this.lbaddress.TabIndex = 42;
            this.lbaddress.Text = "129 Thích Quảng Đức, Phường 4, Quận Phú Nhuận, TP.HCM";
            // 
            // email
            // 
            this.email.EditValue = "sales@protocolvn.com";
            this.email.Location = new System.Drawing.Point(134, 68);
            this.email.Name = "email";
            this.email.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.email.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            this.email.Properties.Appearance.Options.UseBackColor = true;
            this.email.Properties.Appearance.Options.UseForeColor = true;
            this.email.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.email.Size = new System.Drawing.Size(114, 18);
            this.email.TabIndex = 0;
            this.email.TabStop = false;
            // 
            // lbemail
            // 
            this.lbemail.Appearance.ForeColor = System.Drawing.Color.White;
            this.lbemail.Appearance.Options.UseForeColor = true;
            this.lbemail.Location = new System.Drawing.Point(104, 70);
            this.lbemail.Name = "lbemail";
            this.lbemail.Size = new System.Drawing.Size(28, 13);
            this.lbemail.TabIndex = 43;
            this.lbemail.Text = "Email:";
            // 
            // hyperLinkEdit1
            // 
            this.hyperLinkEdit1.EditValue = "www.protocolvn.com";
            this.hyperLinkEdit1.Location = new System.Drawing.Point(303, 68);
            this.hyperLinkEdit1.Name = "hyperLinkEdit1";
            this.hyperLinkEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hyperLinkEdit1.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            this.hyperLinkEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.hyperLinkEdit1.Properties.Appearance.Options.UseForeColor = true;
            this.hyperLinkEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hyperLinkEdit1.Size = new System.Drawing.Size(110, 18);
            this.hyperLinkEdit1.TabIndex = 1;
            this.hyperLinkEdit1.TabStop = false;
            // 
            // lbdt
            // 
            this.lbdt.Appearance.ForeColor = System.Drawing.Color.White;
            this.lbdt.Appearance.Options.UseForeColor = true;
            this.lbdt.Location = new System.Drawing.Point(104, 52);
            this.lbdt.Name = "lbdt";
            this.lbdt.Size = new System.Drawing.Size(89, 13);
            this.lbdt.TabIndex = 40;
            this.lbdt.Text = "Tel: (08) 842 3838";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(259, 52);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(93, 13);
            this.labelControl2.TabIndex = 41;
            this.labelControl2.Text = "Fax: (08) 842 3839";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(217, 10);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(74, 19);
            this.labelControl1.TabIndex = 38;
            this.labelControl1.Text = "Software";
            // 
            // lbtel
            // 
            this.lbtel.Appearance.ForeColor = System.Drawing.Color.White;
            this.lbtel.Appearance.Options.UseForeColor = true;
            this.lbtel.Location = new System.Drawing.Point(259, 70);
            this.lbtel.Name = "lbtel";
            this.lbtel.Size = new System.Drawing.Size(43, 13);
            this.lbtel.TabIndex = 44;
            this.lbtel.Text = "Website:";
            // 
            // lbtencty
            // 
            this.lbtencty.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbtencty.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lbtencty.Appearance.Options.UseFont = true;
            this.lbtencty.Appearance.Options.UseForeColor = true;
            this.lbtencty.Location = new System.Drawing.Point(104, 7);
            this.lbtencty.Name = "lbtencty";
            this.lbtencty.Size = new System.Drawing.Size(107, 23);
            this.lbtencty.TabIndex = 39;
            this.lbtencty.Text = "PROTOCOL";
            // 
            // btnMoRong
            // 
            this.btnMoRong.Location = new System.Drawing.Point(138, 168);
            this.btnMoRong.Name = "btnMoRong";
            this.btnMoRong.Size = new System.Drawing.Size(66, 23);
            this.btnMoRong.TabIndex = 6;
            this.btnMoRong.Text = "Mở rộng";
            this.btnMoRong.Click += new System.EventHandler(this.btnMoRong_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 219);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tùy chọn đăng nhập";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(204, 219);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tùy chọn máy chủ dữ liệu";
            // 
            // frmLogin
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(422, 305);
            this.Controls.Add(this.btnMoRong);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnConfig);
            this.Controls.Add(this.chkAutoLogin);
            this.Controls.Add(this.checkEdit1);
            this.Controls.Add(this.chkRememberPwd);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUsername);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = HelpApplication.getTitleForm("Đăng nhập hệ thống");
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmLogin_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRememberPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAutoLogin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.email.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.GroupBox groupBox2;
        public DevExpress.XtraEditors.TextEdit txtUsername;
        public DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.CheckEdit chkRememberPwd;
        private DevExpress.XtraEditors.SimpleButton btnConfig;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private DevExpress.XtraEditors.CheckEdit chkAutoLogin;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.LabelControl lbaddress;
        private DevExpress.XtraEditors.HyperLinkEdit email;
        private DevExpress.XtraEditors.LabelControl lbemail;
        private DevExpress.XtraEditors.HyperLinkEdit hyperLinkEdit1;
        private DevExpress.XtraEditors.LabelControl lbdt;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lbtel;
        private DevExpress.XtraEditors.LabelControl lbtencty;
        private DevExpress.XtraEditors.SimpleButton btnMoRong;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

    }
}

