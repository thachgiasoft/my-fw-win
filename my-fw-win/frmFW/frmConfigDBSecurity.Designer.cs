namespace ProtocolVN.Framework.Win
{
    partial class frmFWConfigDBSecurity
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
            this.label1 = new System.Windows.Forms.Label();
            this.Cong = new DevExpress.XtraEditors.TextEdit();
            this.btnLoadConfigFile = new DevExpress.XtraEditors.SimpleButton();
            this.embDB = new DevExpress.XtraEditors.CheckEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.chkCSDLTuXa = new DevExpress.XtraEditors.CheckEdit();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.chkCSDLMau = new DevExpress.XtraEditors.CheckEdit();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.buttonEdit1 = new DevExpress.XtraEditors.ButtonEdit();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.textServerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textUsername.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textDatabase.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.embDB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkCSDLTuXa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkCSDLMau.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(18, 40);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(68, 13);
            this.lblServer.TabIndex = 0;
            this.lblServer.Text = "Tên máy chủ";
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Location = new System.Drawing.Point(18, 67);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(58, 13);
            this.lblDatabase.TabIndex = 2;
            this.lblDatabase.Text = "File dữ liệu";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(18, 96);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(68, 13);
            this.lblUsername.TabIndex = 3;
            this.lblUsername.Text = "Tên truy cập";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(18, 124);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(51, 13);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Mật khẩu";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(327, 250);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(261, 250);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(13, 250);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(80, 23);
            this.btnConnect.TabIndex = 7;
            this.btnConnect.Text = "Kết nối thử";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // textServerName
            // 
            this.textServerName.Location = new System.Drawing.Point(91, 35);
            this.textServerName.Name = "textServerName";
            this.textServerName.Size = new System.Drawing.Size(185, 20);
            this.textServerName.TabIndex = 0;
            // 
            // textUsername
            // 
            this.textUsername.Location = new System.Drawing.Point(91, 90);
            this.textUsername.Name = "textUsername";
            this.textUsername.Size = new System.Drawing.Size(266, 20);
            this.textUsername.TabIndex = 3;
            // 
            // textPassword
            // 
            this.textPassword.Location = new System.Drawing.Point(91, 118);
            this.textPassword.Name = "textPassword";
            this.textPassword.Properties.PasswordChar = '*';
            this.textPassword.Size = new System.Drawing.Size(266, 20);
            this.textPassword.TabIndex = 4;
            // 
            // textDatabase
            // 
            this.textDatabase.Location = new System.Drawing.Point(91, 62);
            this.textDatabase.Name = "textDatabase";
            this.textDatabase.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.textDatabase.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit1_Properties_ButtonClick);
            this.textDatabase.Size = new System.Drawing.Size(266, 20);
            this.textDatabase.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(282, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cổng";
            // 
            // Cong
            // 
            this.Cong.Location = new System.Drawing.Point(320, 35);
            this.Cong.Name = "Cong";
            this.Cong.Properties.Appearance.Options.UseTextOptions = true;
            this.Cong.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Cong.Properties.Mask.EditMask = "#####";
            this.Cong.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.Cong.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.Cong.Size = new System.Drawing.Size(37, 20);
            this.Cong.TabIndex = 1;
            // 
            // btnLoadConfigFile
            // 
            this.btnLoadConfigFile.Location = new System.Drawing.Point(99, 250);
            this.btnLoadConfigFile.Name = "btnLoadConfigFile";
            this.btnLoadConfigFile.Size = new System.Drawing.Size(80, 23);
            this.btnLoadConfigFile.TabIndex = 9;
            this.btnLoadConfigFile.Text = "Nạp cấu hình";
            this.btnLoadConfigFile.Click += new System.EventHandler(this.btnLoadConfigFile_Click);
            // 
            // embDB
            // 
            this.embDB.Location = new System.Drawing.Point(2, 2);
            this.embDB.Name = "embDB";
            this.embDB.Properties.Caption = "Dùng cơ sở dữ liệu cục bộ";
            this.embDB.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.embDB.Properties.RadioGroupIndex = 1;
            this.embDB.Size = new System.Drawing.Size(247, 19);
            this.embDB.TabIndex = 10;
            this.embDB.TabStop = false;
            this.embDB.CheckedChanged += new System.EventHandler(this.embDB_CheckedChanged);
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.chkCSDLTuXa);
            this.groupControl1.Controls.Add(this.textDatabase);
            this.groupControl1.Controls.Add(this.lblServer);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.lblDatabase);
            this.groupControl1.Controls.Add(this.lblUsername);
            this.groupControl1.Controls.Add(this.textPassword);
            this.groupControl1.Controls.Add(this.lblPassword);
            this.groupControl1.Controls.Add(this.textUsername);
            this.groupControl1.Controls.Add(this.textServerName);
            this.groupControl1.Controls.Add(this.Cong);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(375, 148);
            this.groupControl1.TabIndex = 11;
            // 
            // chkCSDLTuXa
            // 
            this.chkCSDLTuXa.Location = new System.Drawing.Point(3, 2);
            this.chkCSDLTuXa.Name = "chkCSDLTuXa";
            this.chkCSDLTuXa.Properties.Caption = "Dùng cơ sở dữ liệu từ xa";
            this.chkCSDLTuXa.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.chkCSDLTuXa.Properties.RadioGroupIndex = 1;
            this.chkCSDLTuXa.Size = new System.Drawing.Size(158, 19);
            this.chkCSDLTuXa.TabIndex = 0;
            this.chkCSDLTuXa.TabStop = false;
            this.chkCSDLTuXa.CheckedChanged += new System.EventHandler(this.chkCSDLTuXa_CheckedChanged);
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.Controls.Add(this.chkCSDLMau);
            this.groupControl2.Location = new System.Drawing.Point(12, 207);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(375, 25);
            this.groupControl2.TabIndex = 11;
            // 
            // chkCSDLMau
            // 
            this.chkCSDLMau.Location = new System.Drawing.Point(2, 2);
            this.chkCSDLMau.Name = "chkCSDLMau";
            this.chkCSDLMau.Properties.Caption = "Dùng cơ sở dữ liệu mẫu tham khảo";
            this.chkCSDLMau.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.chkCSDLMau.Properties.RadioGroupIndex = 1;
            this.chkCSDLMau.Size = new System.Drawing.Size(201, 19);
            this.chkCSDLMau.TabIndex = 0;
            this.chkCSDLMau.TabStop = false;
            this.chkCSDLMau.CheckedChanged += new System.EventHandler(this.chkEMBCSDLMau_CheckedChanged);
            // 
            // groupControl3
            // 
            this.groupControl3.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl3.AppearanceCaption.Options.UseFont = true;
            this.groupControl3.Controls.Add(this.embDB);
            this.groupControl3.Controls.Add(this.buttonEdit1);
            this.groupControl3.Controls.Add(this.label2);
            this.groupControl3.Location = new System.Drawing.Point(12, 169);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(375, 25);
            this.groupControl3.TabIndex = 11;
            // 
            // buttonEdit1
            // 
            this.buttonEdit1.Enabled = false;
            this.buttonEdit1.Location = new System.Drawing.Point(91, 31);
            this.buttonEdit1.Name = "buttonEdit1";
            this.buttonEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEdit1.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit1_Properties_ButtonClick);
            this.buttonEdit1.Size = new System.Drawing.Size(266, 20);
            this.buttonEdit1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "File dữ liệu";
            // 
            // frmFWConfigDBSecurity
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 281);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.btnLoadConfigFile);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFWConfigDBSecurity";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cấu hình cơ sở dữ liệu";
            this.Load += new System.EventHandler(this.frmConfigDB_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textServerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textUsername.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textDatabase.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.embDB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkCSDLTuXa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkCSDLMau.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit Cong;
        private DevExpress.XtraEditors.SimpleButton btnLoadConfigFile;
        private DevExpress.XtraEditors.CheckEdit embDB;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.CheckEdit chkCSDLTuXa;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.CheckEdit chkCSDLMau;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.ButtonEdit buttonEdit1;
        private System.Windows.Forms.Label label2;
    }
}