namespace ProtocolVN.Framework.Win
{
    partial class PLDuyetCheckbox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupCB = new DevExpress.XtraEditors.GroupControl();
            this.checkChoDuyet = new DevExpress.XtraEditors.CheckEdit();
            this.checkKhongDuyet = new DevExpress.XtraEditors.CheckEdit();
            this.checkDuyet = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupCB)).BeginInit();
            this.groupCB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkChoDuyet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkKhongDuyet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkDuyet.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupCB
            // 
            this.groupCB.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.groupCB.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.groupCB.CaptionLocation = DevExpress.Utils.Locations.Left;
            this.groupCB.Controls.Add(this.checkChoDuyet);
            this.groupCB.Controls.Add(this.checkKhongDuyet);
            this.groupCB.Controls.Add(this.checkDuyet);
            this.groupCB.Location = new System.Drawing.Point(3, 0);
            this.groupCB.Name = "groupCB";
            this.groupCB.ShowCaption = false;
            this.groupCB.Size = new System.Drawing.Size(216, 20);
            this.groupCB.TabIndex = 0;
            // 
            // checkChoDuyet
            // 
            this.checkChoDuyet.EditValue = true;
            this.checkChoDuyet.Location = new System.Drawing.Point(3, 1);
            this.checkChoDuyet.Name = "checkChoDuyet";
            this.checkChoDuyet.Properties.Caption = "Chờ duyệt";
            this.checkChoDuyet.Size = new System.Drawing.Size(74, 19);
            this.checkChoDuyet.TabIndex = 0;
            this.checkChoDuyet.CheckedChanged += new System.EventHandler(this.checkChoDuyet_CheckedChanged);
            // 
            // checkKhongDuyet
            // 
            this.checkKhongDuyet.EditValue = true;
            this.checkKhongDuyet.Location = new System.Drawing.Point(77, 1);
            this.checkKhongDuyet.Name = "checkKhongDuyet";
            this.checkKhongDuyet.Properties.Caption = "Không duyệt";
            this.checkKhongDuyet.Size = new System.Drawing.Size(86, 19);
            this.checkKhongDuyet.TabIndex = 1;
            this.checkKhongDuyet.CheckedChanged += new System.EventHandler(this.checkKhongDuyet_CheckedChanged);
            // 
            // checkDuyet
            // 
            this.checkDuyet.EditValue = true;
            this.checkDuyet.Location = new System.Drawing.Point(161, 1);
            this.checkDuyet.Name = "checkDuyet";
            this.checkDuyet.Properties.Caption = "Duyệt";
            this.checkDuyet.Size = new System.Drawing.Size(55, 19);
            this.checkDuyet.TabIndex = 2;
            this.checkDuyet.CheckedChanged += new System.EventHandler(this.checkDuyet_CheckedChanged);
            // 
            // PLDuyetSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupCB);
            this.Name = "PLDuyetSelect";
            this.Size = new System.Drawing.Size(224, 20);
            ((System.ComponentModel.ISupportInitialize)(this.groupCB)).EndInit();
            this.groupCB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkChoDuyet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkKhongDuyet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkDuyet.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupCB;
        private DevExpress.XtraEditors.CheckEdit checkDuyet;
        private DevExpress.XtraEditors.CheckEdit checkChoDuyet;
        private DevExpress.XtraEditors.CheckEdit checkKhongDuyet;
    }
}
