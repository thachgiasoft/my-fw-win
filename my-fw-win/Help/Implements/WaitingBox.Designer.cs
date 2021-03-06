namespace ProtocolVN.Framework.Win
{
    partial class TrialWaitingBox
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
            this.pbcWait = new DevExpress.XtraEditors.ProgressBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.pbcWait.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pbcWait
            // 
            this.pbcWait.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbcWait.Location = new System.Drawing.Point(0, 0);
            this.pbcWait.Name = "pbcWait";
            this.pbcWait.Properties.ShowTitle = true;
            this.pbcWait.Size = new System.Drawing.Size(391, 11);
            this.pbcWait.TabIndex = 9;
            this.pbcWait.TabStop = false;
            // 
            // WaitingBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 11);
            this.Controls.Add(this.pbcWait);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WaitingBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.Shown += new System.EventHandler(this.frmWaiting_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pbcWait.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ProgressBarControl pbcWait;
    }
}