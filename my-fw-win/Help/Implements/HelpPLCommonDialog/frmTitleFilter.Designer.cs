namespace ProtocolVN.Framework.Win
{
    partial class frmTitleFilter
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
            this.txt_Title = new DevExpress.XtraEditors.MemoEdit();
            this.btn_DongY = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Huy = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Title.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_Title
            // 
            this.txt_Title.Location = new System.Drawing.Point(12, 12);
            this.txt_Title.Name = "txt_Title";
            this.txt_Title.Size = new System.Drawing.Size(308, 79);
            this.txt_Title.TabIndex = 0;
            // 
            // btn_DongY
            // 
            this.btn_DongY.Location = new System.Drawing.Point(161, 97);
            this.btn_DongY.Name = "btn_DongY";
            this.btn_DongY.Size = new System.Drawing.Size(75, 23);
            this.btn_DongY.TabIndex = 1;
            this.btn_DongY.Text = "Đồng ý";
            this.btn_DongY.Click += new System.EventHandler(this.btn_DongY_Click);
            // 
            // btn_Huy
            // 
            this.btn_Huy.Location = new System.Drawing.Point(242, 97);
            this.btn_Huy.Name = "btn_Huy";
            this.btn_Huy.Size = new System.Drawing.Size(75, 23);
            this.btn_Huy.TabIndex = 1;
            this.btn_Huy.Text = "Hủy";
            this.btn_Huy.Click += new System.EventHandler(this.btn_Huy_Click);
            // 
            // frmTitleFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 128);
            this.Controls.Add(this.btn_Huy);
            this.Controls.Add(this.btn_DongY);
            this.Controls.Add(this.txt_Title);
            this.Name = "frmTitleFilter";
            this.Load += new System.EventHandler(this.frmTitileFilter_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTitileFilter_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.txt_Title.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit txt_Title;
        private DevExpress.XtraEditors.SimpleButton btn_DongY;
        private DevExpress.XtraEditors.SimpleButton btn_Huy;
    }
}