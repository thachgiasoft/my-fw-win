using DevExpress.XtraEditors.DXErrorProvider;
namespace ProtocolVN.Framework.Win
{
    partial class frmUserChild
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
            this.components = new System.ComponentModel.Container();
            this.lblName = new System.Windows.Forms.Label();
            this.lblGroupsContainUser = new System.Windows.Forms.Label();
            this.checkGroupList = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblRePassword = new System.Windows.Forms.Label();
            this.txtPasswordVerify = new DevExpress.XtraEditors.TextEdit();
            this.chkNeverChangePwd = new DevExpress.XtraEditors.CheckEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.txtFullname = new DevExpress.XtraEditors.TextEdit();
            this.chkDisable = new DevExpress.XtraEditors.CheckEdit();
            this.txtUsername = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.checkGroupList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPasswordVerify.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkNeverChangePwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFullname.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDisable.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(5, 16);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(75, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Tên nhân viên";
            // 
            // lblGroupsContainUser
            // 
            this.lblGroupsContainUser.AutoSize = true;
            this.lblGroupsContainUser.Location = new System.Drawing.Point(7, 153);
            this.lblGroupsContainUser.Name = "lblGroupsContainUser";
            this.lblGroupsContainUser.Size = new System.Drawing.Size(170, 13);
            this.lblGroupsContainUser.TabIndex = 6;
            this.lblGroupsContainUser.Text = "Danh sách nhóm chứa người dùng";
            // 
            // checkGroupList
            // 
            this.checkGroupList.CheckOnClick = true;
            this.checkGroupList.Location = new System.Drawing.Point(8, 171);
            this.checkGroupList.MultiColumn = true;
            this.checkGroupList.Name = "checkGroupList";
            this.checkGroupList.Size = new System.Drawing.Size(288, 105);
            this.checkGroupList.TabIndex = 7;
            this.checkGroupList.MouseCaptureChanged += new System.EventHandler(this.checkGroupList_MouseCaptureChanged);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(5, 66);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(51, 13);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "Mật khẩu";
            // 
            // lblRePassword
            // 
            this.lblRePassword.AutoSize = true;
            this.lblRePassword.Location = new System.Drawing.Point(5, 92);
            this.lblRePassword.Name = "lblRePassword";
            this.lblRePassword.Size = new System.Drawing.Size(92, 13);
            this.lblRePassword.TabIndex = 4;
            this.lblRePassword.Text = "Nhập lại mật khẩu";
            // 
            // txtPasswordVerify
            // 
            this.txtPasswordVerify.Location = new System.Drawing.Point(98, 88);
            this.txtPasswordVerify.Name = "txtPasswordVerify";
            this.txtPasswordVerify.Properties.MaxLength = 63;
            this.txtPasswordVerify.Properties.PasswordChar = '*';
            this.txtPasswordVerify.Properties.ValidateOnEnterKey = true;
            this.txtPasswordVerify.Size = new System.Drawing.Size(194, 20);
            this.txtPasswordVerify.TabIndex = 4;
            // 
            // chkNeverChangePwd
            // 
            this.chkNeverChangePwd.Location = new System.Drawing.Point(131, 4);
            this.chkNeverChangePwd.Name = "chkNeverChangePwd";
            this.chkNeverChangePwd.Properties.Caption = "Không được đổi mật khẩu";
            this.chkNeverChangePwd.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Style1;
            this.chkNeverChangePwd.Size = new System.Drawing.Size(148, 22);
            this.chkNeverChangePwd.TabIndex = 0;
            this.chkNeverChangePwd.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(236, 285);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 23);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Đón&g";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(170, 285);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "&Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(98, 62);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.MaxLength = 63;
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Properties.ValidateOnEnterKey = true;
            this.txtPassword.Size = new System.Drawing.Size(194, 20);
            this.txtPassword.TabIndex = 3;
            // 
            // txtFullname
            // 
            this.txtFullname.Location = new System.Drawing.Point(98, 12);
            this.txtFullname.Name = "txtFullname";
            this.txtFullname.Properties.MaxLength = 63;
            this.txtFullname.Properties.ReadOnly = true;
            this.txtFullname.Size = new System.Drawing.Size(165, 20);
            this.txtFullname.TabIndex = 0;
            // 
            // chkDisable
            // 
            this.chkDisable.Location = new System.Drawing.Point(9, 4);
            this.chkDisable.Name = "chkDisable";
            this.chkDisable.Properties.Caption = "Dừng hoạt động";
            this.chkDisable.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Style1;
            this.chkDisable.Size = new System.Drawing.Size(104, 22);
            this.chkDisable.TabIndex = 1;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(98, 37);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Properties.MaxLength = 31;
            this.txtUsername.Size = new System.Drawing.Size(194, 20);
            this.txtUsername.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tên truy cập";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.chkNeverChangePwd);
            this.panelControl1.Controls.Add(this.chkDisable);
            this.panelControl1.Location = new System.Drawing.Point(8, 115);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(284, 31);
            this.panelControl1.TabIndex = 5;
            // 
            // btnSelect
            // 
            this.btnSelect.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnSelect.Location = new System.Drawing.Point(269, 9);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(23, 23);
            this.btnSelect.TabIndex = 1;
            // 
            // frmUserChild
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 314);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtFullname);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtPasswordVerify);
            this.Controls.Add(this.checkGroupList);
            this.Controls.Add(this.lblRePassword);
            this.Controls.Add(this.lblGroupsContainUser);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblName);
            this.MaximizeBox = false;
            this.Name = "frmUserChild";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Người dùng";
            this.Load += new System.EventHandler(this.frmUserChild_Load);
            ((System.ComponentModel.ISupportInitialize)(this.checkGroupList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPasswordVerify.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkNeverChangePwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFullname.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDisable.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblGroupsContainUser;
        private DevExpress.XtraEditors.CheckedListBoxControl checkGroupList;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblRePassword;
        private DevExpress.XtraEditors.TextEdit txtPasswordVerify;
        private DevExpress.XtraEditors.CheckEdit chkNeverChangePwd;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        public DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.TextEdit txtFullname;
        private DevExpress.XtraEditors.CheckEdit chkDisable;
        private DevExpress.XtraEditors.TextEdit txtUsername;
        private System.Windows.Forms.Label label1;
        private DXErrorProvider errorProvider;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnSelect;

    }
}