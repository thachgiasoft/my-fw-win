namespace ProtocolVN.Framework.Win
{
    partial class frmPrintBarCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrintBarCode));
            this.btnInMaVach = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.PLGridView();
            this.CotSLG = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CotDVT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CotTenSanPham = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CotMaHang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CotDONGIA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDong = new DevExpress.XtraEditors.SimpleButton();
            this.btnTuSanPham = new DevExpress.XtraEditors.SimpleButton();
            this.Chon = new DevExpress.XtraEditors.DropDownButton();
            this.ctMenuChonTuExcel = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuItemXuatMauExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemImportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.ctMenuChonTuExcel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInMaVach
            // 
            this.btnInMaVach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInMaVach.Location = new System.Drawing.Point(622, 413);
            this.btnInMaVach.Name = "btnInMaVach";
            this.btnInMaVach.Size = new System.Drawing.Size(104, 23);
            this.btnInMaVach.TabIndex = 0;
            this.btnInMaVach.Text = "In mã vạch...";
            this.btnInMaVach.Click += new System.EventHandler(this.btnInMaVach_Click);
            // 
            // gridControl
            // 
            this.gridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gridControl.BackgroundImage")));
            this.gridControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.gridControl.Location = new System.Drawing.Point(12, 31);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(795, 376);
            this.gridControl.TabIndex = 1;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.CotSLG,
            this.CotDVT,
            this.CotTenSanPham,
            this.CotMaHang,
            this.CotDONGIA});
            this.gridView.GridControl = this.gridControl;
            this.gridView.GroupPanelText = "Danh sách các sản phẩm cần in mã vạch";
            this.gridView.IndicatorWidth = 40;
            this.gridView.Name = "gridView";
            this.gridView.OptionsLayout.Columns.AddNewColumns = false;
            this.gridView.OptionsNavigation.AutoFocusNewRow = true;
            this.gridView.OptionsNavigation.EnterMoveNextColumn = true;
            this.gridView.OptionsPrint.UsePrintStyles = true;
            this.gridView.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView.OptionsView.EnableAppearanceOddRow = true;
            this.gridView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gridView.OptionsView.ShowGroupedColumns = true;
            // 
            // CotSLG
            // 
            this.CotSLG.Caption = "Số lượng";
            this.CotSLG.FieldName = "QUANTITY";
            this.CotSLG.Name = "CotSLG";
            this.CotSLG.Visible = true;
            this.CotSLG.VisibleIndex = 4;
            this.CotSLG.Width = 54;
            // 
            // CotDVT
            // 
            this.CotDVT.Caption = "Đơn vị tính";
            this.CotDVT.FieldName = "UNIT";
            this.CotDVT.Name = "CotDVT";
            this.CotDVT.Visible = true;
            this.CotDVT.VisibleIndex = 2;
            this.CotDVT.Width = 64;
            // 
            // CotTenSanPham
            // 
            this.CotTenSanPham.Caption = "Tên sản phẩm";
            this.CotTenSanPham.FieldName = "NAME";
            this.CotTenSanPham.Name = "CotTenSanPham";
            this.CotTenSanPham.Visible = true;
            this.CotTenSanPham.VisibleIndex = 1;
            this.CotTenSanPham.Width = 79;
            // 
            // CotMaHang
            // 
            this.CotMaHang.Caption = "Mã sản phẩm";
            this.CotMaHang.FieldName = "ID";
            this.CotMaHang.Name = "CotMaHang";
            this.CotMaHang.Visible = true;
            this.CotMaHang.VisibleIndex = 0;
            // 
            // CotDONGIA
            // 
            this.CotDONGIA.Caption = "Đơn giá";
            this.CotDONGIA.Name = "CotDONGIA";
            this.CotDONGIA.Visible = true;
            this.CotDONGIA.VisibleIndex = 3;
            this.CotDONGIA.Width = 49;
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.Location = new System.Drawing.Point(732, 413);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(75, 23);
            this.btnDong.TabIndex = 2;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnTuSanPham
            // 
            this.btnTuSanPham.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTuSanPham.Location = new System.Drawing.Point(3, 3);
            this.btnTuSanPham.Name = "btnTuSanPham";
            this.btnTuSanPham.Size = new System.Drawing.Size(117, 23);
            this.btnTuSanPham.TabIndex = 3;
            this.btnTuSanPham.Text = "Chọn sản phẩm...";
            this.btnTuSanPham.Click += new System.EventHandler(this.btnTuSanPham_Click);
            // 
            // Chon
            // 
            this.Chon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Chon.ContextMenuStrip = this.ctMenuChonTuExcel;
            this.Chon.Location = new System.Drawing.Point(126, 3);
            this.Chon.Name = "Chon";
            this.Chon.Size = new System.Drawing.Size(129, 23);
            this.Chon.TabIndex = 14;
            this.Chon.Text = "Chọn từ Excel";
            this.Chon.ArrowButtonClick += new System.EventHandler(this.Chon_ArrowButtonClick);
            // 
            // ctMenuChonTuExcel
            // 
            this.ctMenuChonTuExcel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemXuatMauExcel,
            this.mnuItemImportExcel});
            this.ctMenuChonTuExcel.Name = "cnMenuChonTuExcel";
            this.ctMenuChonTuExcel.Size = new System.Drawing.Size(149, 48);
            // 
            // mnuItemXuatMauExcel
            // 
            this.mnuItemXuatMauExcel.Name = "mnuItemXuatMauExcel";
            this.mnuItemXuatMauExcel.Size = new System.Drawing.Size(148, 22);
            this.mnuItemXuatMauExcel.Text = "Tạo mẫu Excel";
            this.mnuItemXuatMauExcel.Click += new System.EventHandler(this.mnuItemXuatMauExcel_Click);
            // 
            // mnuItemImportExcel
            // 
            this.mnuItemImportExcel.Name = "mnuItemImportExcel";
            this.mnuItemImportExcel.Size = new System.Drawing.Size(148, 22);
            this.mnuItemImportExcel.Text = "Import từ Excel";
            this.mnuItemImportExcel.Click += new System.EventHandler(this.mnuItemImportExcel_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(124, 13);
            this.labelControl1.TabIndex = 15;
            this.labelControl1.Text = "Danh sách mã vạch cần in";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.flowLayoutPanel1.Controls.Add(this.btnTuSanPham);
            this.flowLayoutPanel1.Controls.Add(this.Chon);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 413);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(268, 29);
            this.flowLayoutPanel1.TabIndex = 16;
            // 
            // frmPrintBarCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 444);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.gridControl);
            this.Controls.Add(this.btnInMaVach);
            this.Name = "frmPrintBarCode";
            this.Text = "In mã vạch";
            this.Load += new System.EventHandler(this.frmPrintBarCode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ctMenuChonTuExcel.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnInMaVach;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.PLGridView gridView;
        private DevExpress.XtraEditors.SimpleButton btnDong;
        private DevExpress.XtraEditors.SimpleButton btnTuSanPham;
        private DevExpress.XtraGrid.Columns.GridColumn CotDVT;
        private DevExpress.XtraGrid.Columns.GridColumn CotTenSanPham;
        private DevExpress.XtraGrid.Columns.GridColumn CotMaHang;
        private DevExpress.XtraGrid.Columns.GridColumn CotSLG;
        private DevExpress.XtraGrid.Columns.GridColumn CotDONGIA;
        private DevExpress.XtraEditors.DropDownButton Chon;
        private System.Windows.Forms.ContextMenuStrip ctMenuChonTuExcel;
        private System.Windows.Forms.ToolStripMenuItem mnuItemXuatMauExcel;
        private System.Windows.Forms.ToolStripMenuItem mnuItemImportExcel;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}