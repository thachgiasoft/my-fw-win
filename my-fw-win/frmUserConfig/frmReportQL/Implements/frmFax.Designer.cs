namespace ProtocolVN.Framework.Win
{
    partial class frmFax
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textEditDoc = new DevExpress.XtraEditors.TextEdit();
            this.comboBoxEditRecipient = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBoxEditFaxNumber = new DevExpress.XtraEditors.ComboBoxEdit();
            this.simpleButtonSend = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditRecipient.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditFaxNumber.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên tài liệu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tên người nhận";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Số fax người nhận";
            // 
            // textEditDoc
            // 
            this.textEditDoc.Location = new System.Drawing.Point(109, 8);
            this.textEditDoc.Name = "textEditDoc";
            this.textEditDoc.Size = new System.Drawing.Size(177, 20);
            this.textEditDoc.TabIndex = 1;
            // 
            // comboBoxEditRecipient
            // 
            this.comboBoxEditRecipient.Location = new System.Drawing.Point(109, 34);
            this.comboBoxEditRecipient.Name = "comboBoxEditRecipient";
            this.comboBoxEditRecipient.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditRecipient.Size = new System.Drawing.Size(177, 20);
            this.comboBoxEditRecipient.TabIndex = 2;
            // 
            // comboBoxEditFaxNumber
            // 
            this.comboBoxEditFaxNumber.Location = new System.Drawing.Point(109, 60);
            this.comboBoxEditFaxNumber.Name = "comboBoxEditFaxNumber";
            this.comboBoxEditFaxNumber.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditFaxNumber.Size = new System.Drawing.Size(177, 20);
            this.comboBoxEditFaxNumber.TabIndex = 2;
            // 
            // simpleButtonSend
            // 
            this.simpleButtonSend.Location = new System.Drawing.Point(168, 86);
            this.simpleButtonSend.Name = "simpleButtonSend";
            this.simpleButtonSend.Size = new System.Drawing.Size(56, 23);
            this.simpleButtonSend.TabIndex = 3;
            this.simpleButtonSend.Text = "Gửi";
            this.simpleButtonSend.Click += new System.EventHandler(this.simpleButtonSend_Click);
            // 
            // simpleButtonClose
            // 
            this.simpleButtonClose.Location = new System.Drawing.Point(230, 86);
            this.simpleButtonClose.Name = "simpleButtonClose";
            this.simpleButtonClose.Size = new System.Drawing.Size(56, 23);
            this.simpleButtonClose.TabIndex = 3;
            this.simpleButtonClose.Text = "Đóng";
            this.simpleButtonClose.Click += new System.EventHandler(this.simpleButtonClose_Click);
            // 
            // frmFax
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 115);
            this.Controls.Add(this.simpleButtonClose);
            this.Controls.Add(this.simpleButtonSend);
            this.Controls.Add(this.comboBoxEditFaxNumber);
            this.Controls.Add(this.comboBoxEditRecipient);
            this.Controls.Add(this.textEditDoc);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmFax";
            this.Text = "Thông tin FAX";
            this.Load += new System.EventHandler(this.frmFax_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textEditDoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditRecipient.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditFaxNumber.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit textEditDoc;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditRecipient;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditFaxNumber;
        private DevExpress.XtraEditors.SimpleButton simpleButtonSend;
        private DevExpress.XtraEditors.SimpleButton simpleButtonClose;
    }
}