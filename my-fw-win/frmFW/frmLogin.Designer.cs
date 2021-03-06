
namespace ProtocolVN.Framework.Win
{
    partial class frmLogin
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
            this.txtUsername = new DevExpress.XtraEditors.TextEdit();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.chkRememberPwd = new DevExpress.XtraEditors.CheckEdit();
            this.btnConfig = new DevExpress.XtraEditors.SimpleButton();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.chkAutoLogin = new DevExpress.XtraEditors.CheckEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SanPham = new DevExpress.XtraEditors.LabelControl();
            this.PhienBan = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit2 = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRememberPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAutoLogin.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(12, 80);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(79, 13);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Tên đăng nhập";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(12, 108);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(51, 13);
            this.lblPassword.TabIndex = 0;
            this.lblPassword.Text = "Mật khẩu";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(97, 77);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(214, 20);
            this.txtUsername.TabIndex = 1;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(97, 105);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(214, 20);
            this.txtPassword.TabIndex = 2;
            // 
            // chkRememberPwd
            // 
            this.chkRememberPwd.Location = new System.Drawing.Point(12, 134);
            this.chkRememberPwd.Name = "chkRememberPwd";
            this.chkRememberPwd.Properties.Caption = "Nhớ mật khẩu";
            this.chkRememberPwd.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Style10;
            this.chkRememberPwd.Size = new System.Drawing.Size(95, 22);
            this.chkRememberPwd.TabIndex = 3;
            // 
            // btnConfig
            // 
            this.btnConfig.Location = new System.Drawing.Point(15, 168);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(25, 23);
            this.btnConfig.TabIndex = 4;
            this.btnConfig.ToolTip = "Cấu hình cơ sở dữ liệu";
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(236, 168);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "Thoát";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(155, 168);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // chkAutoLogin
            // 
            this.chkAutoLogin.Location = new System.Drawing.Point(149, 134);
            this.chkAutoLogin.Name = "chkAutoLogin";
            this.chkAutoLogin.Properties.Caption = "Đăng nhập tự động";
            this.chkAutoLogin.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Style10;
            this.chkAutoLogin.Size = new System.Drawing.Size(123, 22);
            this.chkAutoLogin.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.SanPham);
            this.panel1.Controls.Add(this.PhienBan);
            this.panel1.Controls.Add(this.pictureEdit2);
            this.panel1.Location = new System.Drawing.Point(-1, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(325, 66);
            this.panel1.TabIndex = 40;
            // 
            // SanPham
            // 
            this.SanPham.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SanPham.Appearance.ForeColor = System.Drawing.Color.Red;
            this.SanPham.Appearance.Options.UseFont = true;
            this.SanPham.Appearance.Options.UseForeColor = true;
            this.SanPham.Appearance.Options.UseTextOptions = true;
            this.SanPham.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.SanPham.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.SanPham.Location = new System.Drawing.Point(21, 12);
            this.SanPham.Name = "SanPham";
            this.SanPham.Size = new System.Drawing.Size(223, 25);
            this.SanPham.TabIndex = 22;
            this.SanPham.Text = "PL-PRODUCTS";
            // 
            // PhienBan
            // 
            this.PhienBan.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PhienBan.Appearance.Options.UseFont = true;
            this.PhienBan.Appearance.Options.UseTextOptions = true;
            this.PhienBan.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.PhienBan.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.PhienBan.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.PhienBan.Location = new System.Drawing.Point(24, 43);
            this.PhienBan.Name = "PhienBan";
            this.PhienBan.Size = new System.Drawing.Size(145, 16);
            this.PhienBan.TabIndex = 22;
            this.PhienBan.Text = "v";
            // 
            // pictureEdit2
            // 
            this.pictureEdit2.EditValue = ((object)(resources.GetObject("pictureEdit2.EditValue")));
            this.pictureEdit2.Location = new System.Drawing.Point(253, 2);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Properties.AllowFocused = false;
            this.pictureEdit2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit2.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit2.Properties.ReadOnly = true;
            this.pictureEdit2.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEdit2.Size = new System.Drawing.Size(67, 60);
            this.pictureEdit2.TabIndex = 32;
            // 
            // frmLogin
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(323, 200);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnConfig);
            this.Controls.Add(this.chkAutoLogin);
            this.Controls.Add(this.chkRememberPwd);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUsername);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmLogin_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRememberPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAutoLogin.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        public DevExpress.XtraEditors.TextEdit txtUsername;
        public DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.CheckEdit chkRememberPwd;
        private DevExpress.XtraEditors.SimpleButton btnConfig;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private DevExpress.XtraEditors.CheckEdit chkAutoLogin;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl SanPham;
        private DevExpress.XtraEditors.LabelControl PhienBan;
        private DevExpress.XtraEditors.PictureEdit pictureEdit2;

    }
}

