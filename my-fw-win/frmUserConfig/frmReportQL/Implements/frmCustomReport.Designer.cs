namespace ProtocolVN.Framework.Win
{
    partial class frmCustomReport
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
            this.gridTBDRCot = new DevExpress.XtraGrid.GridControl();
            this.gridTBDRCotView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gridTBTieuDe = new DevExpress.XtraGrid.GridControl();
            this.gridTBTieuDeView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColGiatriHienTai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColGiaTriMoi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridTBDRCot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTBDRCotView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTBTieuDe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTBTieuDeView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridTBDRCot
            // 
            this.gridTBDRCot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTBDRCot.Location = new System.Drawing.Point(2, 20);
            this.gridTBDRCot.MainView = this.gridTBDRCotView;
            this.gridTBDRCot.Name = "gridTBDRCot";
            this.gridTBDRCot.Size = new System.Drawing.Size(622, 74);
            this.gridTBDRCot.TabIndex = 0;
            this.gridTBDRCot.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridTBDRCotView});
            // 
            // gridTBDRCotView
            // 
            this.gridTBDRCotView.GridControl = this.gridTBDRCot;
            this.gridTBDRCotView.Name = "gridTBDRCotView";
            this.gridTBDRCotView.OptionsView.AllowCellMerge = true;
            this.gridTBDRCotView.OptionsView.ColumnAutoWidth = false;
            this.gridTBDRCotView.OptionsView.ShowGroupPanel = false;
            this.gridTBDRCotView.OptionsView.ShowPreview = true;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 312);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(630, 43);
            this.panelControl1.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(543, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(462, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.groupControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(630, 312);
            this.splitContainerControl1.SplitterPosition = 206;
            this.splitContainerControl1.TabIndex = 2;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.gridTBTieuDe);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(626, 202);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Tùy biến tiêu đề";
            // 
            // gridTBTieuDe
            // 
            this.gridTBTieuDe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTBTieuDe.Location = new System.Drawing.Point(2, 20);
            this.gridTBTieuDe.MainView = this.gridTBTieuDeView;
            this.gridTBTieuDe.Name = "gridTBTieuDe";
            this.gridTBTieuDe.Size = new System.Drawing.Size(622, 180);
            this.gridTBTieuDe.TabIndex = 0;
            this.gridTBTieuDe.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridTBTieuDeView});
            // 
            // gridTBTieuDeView
            // 
            this.gridTBTieuDeView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColGiatriHienTai,
            this.gridColGiaTriMoi});
            this.gridTBTieuDeView.GridControl = this.gridTBTieuDe;
            this.gridTBTieuDeView.Name = "gridTBTieuDeView";
            this.gridTBTieuDeView.OptionsView.ShowGroupPanel = false;
            // 
            // gridColGiatriHienTai
            // 
            this.gridColGiatriHienTai.Caption = "Giá trị hiện tại";
            this.gridColGiatriHienTai.FieldName = "CurrentValue";
            this.gridColGiatriHienTai.Name = "gridColGiatriHienTai";
            this.gridColGiatriHienTai.OptionsColumn.ReadOnly = true;
            this.gridColGiatriHienTai.Visible = true;
            this.gridColGiatriHienTai.VisibleIndex = 0;
            // 
            // gridColGiaTriMoi
            // 
            this.gridColGiaTriMoi.Caption = "Giá trị mới";
            this.gridColGiaTriMoi.FieldName = "NewValue";
            this.gridColGiaTriMoi.Name = "gridColGiaTriMoi";
            this.gridColGiaTriMoi.Visible = true;
            this.gridColGiaTriMoi.VisibleIndex = 1;
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.Controls.Add(this.gridTBDRCot);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(626, 96);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "Tùy biến độ rộng cột";
            // 
            // frmCustomReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 355);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "frmCustomReport";
            this.Text = "Tùy biến báo biểu";
            this.Load += new System.EventHandler(this.frmCustomReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridTBDRCot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTBDRCotView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTBTieuDe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTBTieuDeView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridTBDRCot;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraGrid.Views.Grid.GridView gridTBDRCotView;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gridTBTieuDe;
        private DevExpress.XtraGrid.Views.Grid.GridView gridTBTieuDeView;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColGiatriHienTai;
        private DevExpress.XtraGrid.Columns.GridColumn gridColGiaTriMoi;
    }
}