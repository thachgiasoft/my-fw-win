namespace ProtocolVN.Framework.Win
{
    partial class frmDataReport
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
            this.gridControlReportData = new DevExpress.XtraGrid.GridControl();
            this.gridViewReportData = new DevExpress.XtraGrid.Views.Grid.PLGridView();
            this.listBoxReportData = new DevExpress.XtraEditors.ListBoxControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlReportData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewReportData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxReportData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControlReportData
            // 
            this.gridControlReportData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlReportData.Location = new System.Drawing.Point(0, 0);
            this.gridControlReportData.MainView = this.gridViewReportData;
            this.gridControlReportData.Name = "gridControlReportData";
            this.gridControlReportData.Size = new System.Drawing.Size(505, 535);
            this.gridControlReportData.TabIndex = 0;
            this.gridControlReportData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewReportData});
            // 
            // gridViewReportData
            // 
            this.gridViewReportData.GridControl = this.gridControlReportData;
            this.gridViewReportData.Name = "gridViewReportData";
            this.gridViewReportData.OptionsBehavior.Editable = false;
            // 
            // listBoxReportData
            // 
            this.listBoxReportData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxReportData.Location = new System.Drawing.Point(0, 0);
            this.listBoxReportData.Name = "listBoxReportData";
            this.listBoxReportData.Size = new System.Drawing.Size(215, 535);
            this.listBoxReportData.TabIndex = 1;
            this.listBoxReportData.SelectedIndexChanged += new System.EventHandler(this.listBoxReportData_SelectedIndexChanged);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.listBoxReportData);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControlReportData);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(734, 539);
            this.splitContainerControl1.SplitterPosition = 219;
            this.splitContainerControl1.TabIndex = 3;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // frmDataReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 539);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "frmDataReport";
            this.Text = "Dữ liệu báo cáo";
            this.Load += new System.EventHandler(this.frmCustomReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlReportData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewReportData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxReportData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlReportData;
        private DevExpress.XtraGrid.Views.Grid.PLGridView gridViewReportData;
        private DevExpress.XtraEditors.ListBoxControl listBoxReportData;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
    }
}