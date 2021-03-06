namespace ProtocolVN.Framework.Win
{
    partial class frmChangePwd
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
            this.lblOldPwd = new System.Windows.Forms.Label();
            this.lblNewPwd = new System.Windows.Forms.Label();
            this.lblReNewPwd = new System.Windows.Forms.Label();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.textPasswordOld = new DevExpress.XtraEditors.TextEdit();
            this.textPasswordNew = new DevExpress.XtraEditors.TextEdit();
            this.textPasswordNewConfirm = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.textPasswordOld.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textPasswordNew.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textPasswordNewConfirm.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblOldPwd
            // 
            this.lblOldPwd.AutoSize = true;
            this.lblOldPwd.Location = new System.Drawing.Point(7, 15);
            this.lblOldPwd.Name = "lblOldPwd";
            this.lblOldPwd.Size = new System.Drawing.Size(65, 13);
            this.lblOldPwd.TabIndex = 0;
            this.lblOldPwd.Text = "Mật khẩu cũ";
            // 
            // lblNewPwd
            // 
            this.lblNewPwd.AutoSize = true;
            this.lblNewPwd.Location = new System.Drawing.Point(7, 43);
            this.lblNewPwd.Name = "lblNewPwd";
            this.lblNewPwd.Size = new System.Drawing.Size(70, 13);
            this.lblNewPwd.TabIndex = 0;
            this.lblNewPwd.Text = "Mật khẩu mới";
            // 
            // lblReNewPwd
            // 
            this.lblReNewPwd.AutoSize = true;
            this.lblReNewPwd.Location = new System.Drawing.Point(7, 71);
            this.lblReNewPwd.Name = "lblReNewPwd";
            this.lblReNewPwd.Size = new System.Drawing.Size(111, 13);
            this.lblReNewPwd.TabIndex = 0;
            this.lblReNewPwd.Text = "Nhập lại mật khẩu mới";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(222, 103);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(156, 103);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(60, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "Lưu";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // textPasswordOld
            // 
            this.textPasswordOld.Location = new System.Drawing.Point(126, 11);
            this.textPasswordOld.Name = "textPasswordOld";
            this.textPasswordOld.Properties.PasswordChar = '*';
            this.textPasswordOld.Size = new System.Drawing.Size(156, 20);
            this.textPasswordOld.TabIndex = 1;
            // 
            // textPasswordNew
            // 
            this.textPasswordNew.Location = new System.Drawing.Point(126, 39);
            this.textPasswordNew.Name = "textPasswordNew";
            this.textPasswordNew.Properties.PasswordChar = '*';
            this.textPasswordNew.Size = new System.Drawing.Size(156, 20);
            this.textPasswordNew.TabIndex = 2;
            // 
            // textPasswordNewConfirm
            // 
            this.textPasswordNewConfirm.Location = new System.Drawing.Point(126, 67);
            this.textPasswordNewConfirm.Name = "textPasswordNewConfirm";
            this.textPasswordNewConfirm.Properties.PasswordChar = '*';
            this.textPasswordNewConfirm.Size = new System.Drawing.Size(156, 20);
            this.textPasswordNewConfirm.TabIndex = 3;
            // 
            // frmChangePwd
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 133);
            this.Controls.Add(this.textPasswordNewConfirm);
            this.Controls.Add(this.textPasswordNew);
            this.Controls.Add(this.textPasswordOld);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblReNewPwd);
            this.Controls.Add(this.lblNewPwd);
            this.Controls.Add(this.lblOldPwd);
            this.Name = "frmChangePwd";
            this.Text = "Đổi mật khẩu";
            this.Load += new System.EventHandler(this.frmChangePwd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textPasswordOld.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textPasswordNew.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textPasswordNewConfirm.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblOldPwd;
        private System.Windows.Forms.Label lblNewPwd;
        private System.Windows.Forms.Label lblReNewPwd;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.TextEdit textPasswordOld;
        private DevExpress.XtraEditors.TextEdit textPasswordNew;
        private DevExpress.XtraEditors.TextEdit textPasswordNewConfirm;
    }
}