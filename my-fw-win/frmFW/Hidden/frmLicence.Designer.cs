namespace ProtocolVN.Framework.Win
{
    partial class frmLicence
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLicence));
            this.licInfo = new DevExpress.XtraEditors.LabelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnLicence = new DevExpress.XtraEditors.SimpleButton();
            this.TenCty = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.lbphienban = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // licInfo
            // 
            this.licInfo.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.licInfo.Appearance.ForeColor = System.Drawing.Color.Black;
            this.licInfo.Appearance.Options.UseFont = true;
            this.licInfo.Appearance.Options.UseForeColor = true;
            this.licInfo.Appearance.Options.UseTextOptions = true;
            this.licInfo.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.licInfo.AutoEllipsis = true;
            this.licInfo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.licInfo.Location = new System.Drawing.Point(14, 107);
            this.licInfo.Name = "licInfo";
            this.licInfo.Size = new System.Drawing.Size(404, 78);
            this.licInfo.TabIndex = 45;
            this.licInfo.Text = resources.GetString("licInfo.Text");
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(348, 198);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 23);
            this.btnClose.TabIndex = 43;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnLicence
            // 
            this.btnLicence.Location = new System.Drawing.Point(272, 198);
            this.btnLicence.Name = "btnLicence";
            this.btnLicence.Size = new System.Drawing.Size(70, 23);
            this.btnLicence.TabIndex = 44;
            this.btnLicence.Text = "Cập nhật";
            this.btnLicence.Click += new System.EventHandler(this.btnLicence_Click);
            // 
            // TenCty
            // 
            this.TenCty.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TenCty.Appearance.ForeColor = System.Drawing.Color.Black;
            this.TenCty.Appearance.Options.UseFont = true;
            this.TenCty.Appearance.Options.UseForeColor = true;
            this.TenCty.Appearance.Options.UseTextOptions = true;
            this.TenCty.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TenCty.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.TenCty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.TenCty.Location = new System.Drawing.Point(119, 40);
            this.TenCty.Name = "TenCty";
            this.TenCty.Size = new System.Drawing.Size(310, 14);
            this.TenCty.TabIndex = 42;
            this.TenCty.Text = "Cty TNHH Phần Mềm Giao Thức";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(14, 17);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.AllowFocused = false;
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ReadOnly = true;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEdit1.Size = new System.Drawing.Size(99, 84);
            this.pictureEdit1.TabIndex = 41;
            // 
            // lbphienban
            // 
            this.lbphienban.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbphienban.Appearance.Options.UseFont = true;
            this.lbphienban.Location = new System.Drawing.Point(119, 17);
            this.lbphienban.Name = "lbphienban";
            this.lbphienban.Size = new System.Drawing.Size(113, 13);
            this.lbphienban.TabIndex = 40;
            this.lbphienban.Text = "Phiên bản cung cấp cho";
            // 
            // frmLicence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 230);
            this.Controls.Add(this.licInfo);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnLicence);
            this.Controls.Add(this.TenCty);
            this.Controls.Add(this.pictureEdit1);
            this.Controls.Add(this.lbphienban);
            this.Name = "frmLicence";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Về giấy phép";
            this.Load += new System.EventHandler(this.frmLicence_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraEditors.LabelControl licInfo;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnLicence;
        public DevExpress.XtraEditors.LabelControl TenCty;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.LabelControl lbphienban;
    }
}