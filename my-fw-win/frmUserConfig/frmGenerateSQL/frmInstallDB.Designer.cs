namespace ProtocolVN.Framework.Win
{
    partial class frmInstallDB
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
            this.Install = new DevExpress.XtraEditors.CheckEdit();
            this.UnInstall = new DevExpress.XtraEditors.CheckEdit();
            this.btn_Yes = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Close = new DevExpress.XtraEditors.SimpleButton();
            this.SqlScript = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lbl_TinhTrang = new DevExpress.XtraEditors.LabelControl();
            this.btn_Check = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.Install.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UnInstall.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SqlScript.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // Install
            // 
            this.Install.Location = new System.Drawing.Point(121, 9);
            this.Install.Name = "Install";
            this.Install.Properties.Caption = "Cài đặt dữ liệu";
            this.Install.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.Install.Size = new System.Drawing.Size(96, 19);
            this.Install.TabIndex = 0;
            this.Install.CheckedChanged += new System.EventHandler(this.Install_CheckedChanged);
            // 
            // UnInstall
            // 
            this.UnInstall.Location = new System.Drawing.Point(223, 9);
            this.UnInstall.Name = "UnInstall";
            this.UnInstall.Properties.Caption = "Hủy bỏ dữ liệu";
            this.UnInstall.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.UnInstall.Size = new System.Drawing.Size(114, 19);
            this.UnInstall.TabIndex = 0;
            this.UnInstall.CheckedChanged += new System.EventHandler(this.UnInstall_CheckedChanged);
            // 
            // btn_Yes
            // 
            this.btn_Yes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Yes.Location = new System.Drawing.Point(576, 409);
            this.btn_Yes.Name = "btn_Yes";
            this.btn_Yes.Size = new System.Drawing.Size(75, 23);
            this.btn_Yes.TabIndex = 1;
            this.btn_Yes.Text = "Thực hiện";
            this.btn_Yes.Click += new System.EventHandler(this.btn_Yes_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.Location = new System.Drawing.Point(657, 409);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 1;
            this.btn_Close.Text = "Đóng";
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // SqlScript
            // 
            this.SqlScript.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SqlScript.Location = new System.Drawing.Point(12, 57);
            this.SqlScript.Name = "SqlScript";
            this.SqlScript.Size = new System.Drawing.Size(720, 343);
            this.SqlScript.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(14, 38);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(94, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Nội dung SQL Script";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(14, 11);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(77, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Chọn SQL Script";
            // 
            // lbl_TinhTrang
            // 
            this.lbl_TinhTrang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_TinhTrang.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TinhTrang.Appearance.Options.UseFont = true;
            this.lbl_TinhTrang.Appearance.Options.UseTextOptions = true;
            this.lbl_TinhTrang.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lbl_TinhTrang.Location = new System.Drawing.Point(433, 12);
            this.lbl_TinhTrang.Name = "lbl_TinhTrang";
            this.lbl_TinhTrang.Size = new System.Drawing.Size(299, 25);
            this.lbl_TinhTrang.TabIndex = 2;
            this.lbl_TinhTrang.Text = "Tình trạng dữ liệu Tình trạng";
            // 
            // btn_Check
            // 
            this.btn_Check.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Check.Location = new System.Drawing.Point(452, 409);
            this.btn_Check.Name = "btn_Check";
            this.btn_Check.Size = new System.Drawing.Size(118, 23);
            this.btn_Check.TabIndex = 1;
            this.btn_Check.Text = "Kiểm tra dữ liệu";
            this.btn_Check.Click += new System.EventHandler(this.btn_Check_Click);
            // 
            // frmInstallDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 437);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.lbl_TinhTrang);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.SqlScript);
            this.Controls.Add(this.Install);
            this.Controls.Add(this.UnInstall);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_Check);
            this.Controls.Add(this.btn_Yes);
            this.Name = "frmInstallDB";
            this.Text = "Cài đặt dữ liệu hệ thống";
            ((System.ComponentModel.ISupportInitialize)(this.Install.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UnInstall.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SqlScript.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.CheckEdit UnInstall;
        private DevExpress.XtraEditors.CheckEdit Install;
        private DevExpress.XtraEditors.SimpleButton btn_Yes;
        private DevExpress.XtraEditors.SimpleButton btn_Close;
        private DevExpress.XtraEditors.MemoEdit SqlScript;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lbl_TinhTrang;
        private DevExpress.XtraEditors.SimpleButton btn_Check;
    }
}