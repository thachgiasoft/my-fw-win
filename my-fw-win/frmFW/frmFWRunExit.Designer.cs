namespace ProtocolVN.Framework.Win
{
    partial class frmFWRunExit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFWRunExit));
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.lbluuy = new DevExpress.XtraEditors.LabelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SanPham = new DevExpress.XtraEditors.LabelControl();
            this.PhienBan = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(134, 97);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(60, 23);
            this.simpleButton1.TabIndex = 44;
            this.simpleButton1.Text = "Không";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(68, 97);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(60, 23);
            this.simpleButton2.TabIndex = 45;
            this.simpleButton2.Text = "Có";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // lbluuy
            // 
            this.lbluuy.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbluuy.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lbluuy.Appearance.Options.UseFont = true;
            this.lbluuy.Appearance.Options.UseForeColor = true;
            this.lbluuy.Appearance.Options.UseTextOptions = true;
            this.lbluuy.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lbluuy.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lbluuy.Location = new System.Drawing.Point(21, 72);
            this.lbluuy.Name = "lbluuy";
            this.lbluuy.Size = new System.Drawing.Size(221, 13);
            this.lbluuy.TabIndex = 45;
            this.lbluuy.Text = "Bạn có muốn thoát khỏi chương trình không?";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.SanPham);
            this.panel1.Controls.Add(this.PhienBan);
            this.panel1.Controls.Add(this.pictureEdit1);
            this.panel1.Location = new System.Drawing.Point(-1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(264, 56);
            this.panel1.TabIndex = 46;
            // 
            // SanPham
            // 
            this.SanPham.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SanPham.Appearance.ForeColor = System.Drawing.Color.Red;
            this.SanPham.Appearance.Options.UseFont = true;
            this.SanPham.Appearance.Options.UseForeColor = true;
            this.SanPham.Appearance.Options.UseTextOptions = true;
            this.SanPham.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.SanPham.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.SanPham.Location = new System.Drawing.Point(13, 12);
            this.SanPham.Name = "SanPham";
            this.SanPham.Size = new System.Drawing.Size(182, 19);
            this.SanPham.TabIndex = 22;
            this.SanPham.Text = "PL-PRODUCTS";
            // 
            // PhienBan
            // 
            this.PhienBan.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PhienBan.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.PhienBan.Appearance.Options.UseFont = true;
            this.PhienBan.Appearance.Options.UseForeColor = true;
            this.PhienBan.Appearance.Options.UseTextOptions = true;
            this.PhienBan.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.PhienBan.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.PhienBan.Location = new System.Drawing.Point(13, 37);
            this.PhienBan.Name = "PhienBan";
            this.PhienBan.Size = new System.Drawing.Size(159, 13);
            this.PhienBan.TabIndex = 22;
            this.PhienBan.Text = "Phiên bản v";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(207, 6);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.AllowFocused = false;
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ReadOnly = true;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEdit1.Size = new System.Drawing.Size(50, 44);
            this.pictureEdit1.TabIndex = 32;
            // 
            // frmFWRunExit
            // 
            this.AcceptButton = this.simpleButton1;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 120);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.lbluuy);
            this.Name = "frmFWRunExit";
            this.Load += new System.EventHandler(this.frmRunFirst_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        public DevExpress.XtraEditors.LabelControl lbluuy;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl SanPham;
        private DevExpress.XtraEditors.LabelControl PhienBan;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;

    }
}