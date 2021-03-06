namespace ProtocolVN.Framework.Win
{
    partial class frmAppParams
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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.vGridMain = new DevExpress.XtraVerticalGrid.VGridControl();
            this.ParamDescrip_lb = new DevExpress.XtraEditors.LabelControl();
            this.Param_lb = new DevExpress.XtraEditors.LabelControl();
            this.btnDong = new DevExpress.XtraEditors.SimpleButton();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vGridMain)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.vGridMain);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.ParamDescrip_lb);
            this.splitContainerControl1.Panel2.Controls.Add(this.Param_lb);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(484, 469);
            this.splitContainerControl1.SplitterPosition = 351;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // vGridMain
            // 
            this.vGridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vGridMain.LayoutStyle = DevExpress.XtraVerticalGrid.LayoutViewStyle.SingleRecordView;
            this.vGridMain.Location = new System.Drawing.Point(0, 0);
            this.vGridMain.Name = "vGridMain";
            this.vGridMain.OptionsBehavior.Editable = false;
            this.vGridMain.Size = new System.Drawing.Size(484, 351);
            this.vGridMain.TabIndex = 1;
            // 
            // ParamDescrip_lb
            // 
            this.ParamDescrip_lb.Location = new System.Drawing.Point(10, 34);
            this.ParamDescrip_lb.Name = "ParamDescrip_lb";
            this.ParamDescrip_lb.Size = new System.Drawing.Size(0, 13);
            this.ParamDescrip_lb.TabIndex = 47;
            // 
            // Param_lb
            // 
            this.Param_lb.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Param_lb.Appearance.Options.UseFont = true;
            this.Param_lb.Location = new System.Drawing.Point(10, 10);
            this.Param_lb.Name = "Param_lb";
            this.Param_lb.Size = new System.Drawing.Size(0, 13);
            this.Param_lb.TabIndex = 46;
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.Location = new System.Drawing.Point(401, 481);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(75, 23);
            this.btnDong.TabIndex = 1;
            this.btnDong.Text = "Đón&g";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuu.Location = new System.Drawing.Point(320, 481);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(75, 23);
            this.btnLuu.TabIndex = 2;
            this.btnLuu.Text = "&Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Location = new System.Drawing.Point(-8, 473);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(500, 3);
            this.groupBox3.TabIndex = 69;
            this.groupBox3.TabStop = false;
            // 
            // frmAppParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 508);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "frmAppParams";
            this.Text = "Tham số nghiệp vụ";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vGridMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.SimpleButton btnDong;
        public DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraVerticalGrid.VGridControl vGridMain;
        private DevExpress.XtraEditors.LabelControl ParamDescrip_lb;
        private DevExpress.XtraEditors.LabelControl Param_lb;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}