namespace ProtocolVN.Framework.Win
{
    partial class frmConfigDB
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
            this.lblServer = new System.Windows.Forms.Label();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnConnect = new DevExpress.XtraEditors.SimpleButton();
            this.textServerName = new DevExpress.XtraEditors.TextEdit();
            this.textUsername = new DevExpress.XtraEditors.TextEdit();
            this.textPassword = new DevExpress.XtraEditors.TextEdit();
            this.textDatabase = new DevExpress.XtraEditors.ButtonEdit();
            this.MacDinh = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.Cong = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.textServerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textUsername.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textDatabase.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cong.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(8, 15);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(68, 13);
            this.lblServer.TabIndex = 0;
            this.lblServer.Text = "Tên máy chủ";
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Location = new System.Drawing.Point(8, 42);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(69, 13);
            this.lblDatabase.TabIndex = 2;
            this.lblDatabase.Text = "Cơ sở dữ liệu";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(8, 71);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(68, 13);
            this.lblUsername.TabIndex = 3;
            this.lblUsername.Text = "Tên truy cập";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(8, 99);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(51, 13);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Mật khẩu";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(276, 133);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(195, 133);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(8, 133);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 7;
            this.btnConnect.Text = "Kết nối thử";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // textServerName
            // 
            this.textServerName.Location = new System.Drawing.Point(81, 12);
            this.textServerName.Name = "textServerName";
            this.textServerName.Size = new System.Drawing.Size(156, 20);
            this.textServerName.TabIndex = 0;
            // 
            // textUsername
            // 
            this.textUsername.Location = new System.Drawing.Point(81, 67);
            this.textUsername.Name = "textUsername";
            this.textUsername.Size = new System.Drawing.Size(266, 20);
            this.textUsername.TabIndex = 3;
            // 
            // textPassword
            // 
            this.textPassword.Location = new System.Drawing.Point(81, 95);
            this.textPassword.Name = "textPassword";
            this.textPassword.Properties.PasswordChar = '*';
            this.textPassword.Size = new System.Drawing.Size(266, 20);
            this.textPassword.TabIndex = 4;
            // 
            // textDatabase
            // 
            this.textDatabase.Location = new System.Drawing.Point(81, 39);
            this.textDatabase.Name = "textDatabase";
            this.textDatabase.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.textDatabase.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit1_Properties_ButtonClick);
            this.textDatabase.Size = new System.Drawing.Size(266, 20);
            this.textDatabase.TabIndex = 2;
            // 
            // MacDinh
            // 
            this.MacDinh.Location = new System.Drawing.Point(89, 133);
            this.MacDinh.Name = "MacDinh";
            this.MacDinh.Size = new System.Drawing.Size(75, 23);
            this.MacDinh.TabIndex = 8;
            this.MacDinh.Text = "Mặc định";
            this.MacDinh.Click += new System.EventHandler(this.MacDinh_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(240, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cổng";
            // 
            // Cong
            // 
            this.Cong.Location = new System.Drawing.Point(278, 12);
            this.Cong.Name = "Cong";
            this.Cong.Properties.Appearance.Options.UseTextOptions = true;
            this.Cong.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Cong.Properties.Mask.EditMask = "#####";
            this.Cong.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.Cong.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.Cong.Size = new System.Drawing.Size(69, 20);
            this.Cong.TabIndex = 1;
            // 
            // frmConfigDB
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 167);
            this.Controls.Add(this.textDatabase);
            this.Controls.Add(this.textPassword);
            this.Controls.Add(this.textUsername);
            this.Controls.Add(this.Cong);
            this.Controls.Add(this.textServerName);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.MacDinh);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.lblDatabase);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblServer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfigDB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cấu hình dữ liệu";
            this.Load += new System.EventHandler(this.frmConfigDB_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textServerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textUsername.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textDatabase.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cong.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnConnect;
        private DevExpress.XtraEditors.TextEdit textServerName;
        private DevExpress.XtraEditors.TextEdit textUsername;
        private DevExpress.XtraEditors.TextEdit textPassword;
        private DevExpress.XtraEditors.ButtonEdit textDatabase;
        private DevExpress.XtraEditors.SimpleButton MacDinh;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit Cong;
    }
}