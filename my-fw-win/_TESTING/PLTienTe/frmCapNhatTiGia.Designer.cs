using DevExpress.XtraGrid.Views.Grid;
namespace ProtocolVN.Framework.Win
{
    partial class frmCapNhatTiGia
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCapNhatTiGia));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.gridUpdateTiGia = new DevExpress.XtraGrid.GridControl();
            this.gridViewUpdateTiGia = new DevExpress.XtraGrid.Views.Grid.PLGridView();
            this.CotTienTe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CotTiGiaHienTai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CotTiGiaMoi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CotNGuoiCapNhat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CotNgayCapNhat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CotDong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnAddNew = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnXem = new DevExpress.XtraEditors.SimpleButton();
            this.btnDMTienTe = new DevExpress.XtraEditors.SimpleButton();
            this.Ngay = new DevExpress.XtraEditors.DateEdit();
            this.plMoneyType1 = new PLMoneyType();
            ((System.ComponentModel.ISupportInitialize)(this.gridUpdateTiGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewUpdateTiGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ngay.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ngay.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(466, 364);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "&Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(547, 364);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // gridUpdateTiGia
            // 
            this.gridUpdateTiGia.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gridUpdateTiGia.BackgroundImage")));
            this.gridUpdateTiGia.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.gridUpdateTiGia.Location = new System.Drawing.Point(11, 40);
            this.gridUpdateTiGia.MainView = this.gridViewUpdateTiGia;
            this.gridUpdateTiGia.Name = "gridUpdateTiGia";
            this.gridUpdateTiGia.Size = new System.Drawing.Size(611, 316);
            this.gridUpdateTiGia.TabIndex = 11;
            this.gridUpdateTiGia.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewUpdateTiGia});
            // 
            // gridViewUpdateTiGia
            // 
            this.gridViewUpdateTiGia.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewUpdateTiGia.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewUpdateTiGia.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.CotTienTe,
            this.CotTiGiaHienTai,
            this.CotTiGiaMoi,
            this.CotNGuoiCapNhat,
            this.CotNgayCapNhat});
            this.gridViewUpdateTiGia.GridControl = this.gridUpdateTiGia;
            this.gridViewUpdateTiGia.GroupPanelText = "Bảng tỷ giá ngoại tệ";
            this.gridViewUpdateTiGia.IndicatorWidth = 40;
            this.gridViewUpdateTiGia.Name = "gridViewUpdateTiGia";
            this.gridViewUpdateTiGia.OptionsLayout.Columns.AddNewColumns = false;
            this.gridViewUpdateTiGia.OptionsNavigation.AutoFocusNewRow = true;
            this.gridViewUpdateTiGia.OptionsNavigation.EnterMoveNextColumn = true;
            this.gridViewUpdateTiGia.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewUpdateTiGia.OptionsView.EnableAppearanceOddRow = true;
            this.gridViewUpdateTiGia.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gridViewUpdateTiGia.OptionsView.ShowGroupedColumns = true;
            this.gridViewUpdateTiGia.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewUpdateTiGia_FocusedRowChanged);
            this.gridViewUpdateTiGia.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewUpdateTiGia_CellValueChanged);
            this.gridViewUpdateTiGia.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewUpdateTiGia_CellValueChanging);
            this.gridViewUpdateTiGia.RowCountChanged += new System.EventHandler(this.gridViewUpdateTiGia_RowCountChanged);
            this.gridViewUpdateTiGia.DoubleClick += new System.EventHandler(this.gridViewUpdateTiGia_DoubleClick);
            this.gridViewUpdateTiGia.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gridViewUpdateTiGia_ValidateRow);
            // 
            // CotTienTe
            // 
            this.CotTienTe.Caption = "Tiền tệ";
            this.CotTienTe.Name = "CotTienTe";
            this.CotTienTe.Visible = true;
            this.CotTienTe.VisibleIndex = 2;
            this.CotTienTe.Width = 45;
            // 
            // CotTiGiaHienTai
            // 
            this.CotTiGiaHienTai.Caption = "Tỉ giá hiện tại";
            this.CotTiGiaHienTai.Name = "CotTiGiaHienTai";
            this.CotTiGiaHienTai.OptionsColumn.AllowEdit = false;
            this.CotTiGiaHienTai.OptionsColumn.AllowFocus = false;
            this.CotTiGiaHienTai.OptionsColumn.ReadOnly = true;
            this.CotTiGiaHienTai.Visible = true;
            this.CotTiGiaHienTai.VisibleIndex = 3;
            // 
            // CotTiGiaMoi
            // 
            this.CotTiGiaMoi.Caption = "Tỉ giá mới";
            this.CotTiGiaMoi.Name = "CotTiGiaMoi";
            this.CotTiGiaMoi.OptionsColumn.AllowEdit = false;
            this.CotTiGiaMoi.Visible = true;
            this.CotTiGiaMoi.VisibleIndex = 4;
            this.CotTiGiaMoi.Width = 56;
            // 
            // CotNGuoiCapNhat
            // 
            this.CotNGuoiCapNhat.Caption = "Người cập nhật";
            this.CotNGuoiCapNhat.Name = "CotNGuoiCapNhat";
            this.CotNGuoiCapNhat.OptionsColumn.AllowEdit = false;
            this.CotNGuoiCapNhat.OptionsColumn.AllowFocus = false;
            this.CotNGuoiCapNhat.OptionsColumn.ReadOnly = true;
            this.CotNGuoiCapNhat.Visible = true;
            this.CotNGuoiCapNhat.VisibleIndex = 0;
            this.CotNGuoiCapNhat.Width = 85;
            // 
            // CotNgayCapNhat
            // 
            this.CotNgayCapNhat.Caption = "Ngày cập nhật";
            this.CotNgayCapNhat.Name = "CotNgayCapNhat";
            this.CotNgayCapNhat.OptionsColumn.AllowEdit = false;
            this.CotNgayCapNhat.OptionsColumn.AllowFocus = false;
            this.CotNgayCapNhat.OptionsColumn.ReadOnly = true;
            this.CotNgayCapNhat.Visible = true;
            this.CotNgayCapNhat.VisibleIndex = 1;
            this.CotNgayCapNhat.Width = 82;
            // 
            // CotDong
            // 
            this.CotDong.Name = "CotDong";
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNew.Location = new System.Drawing.Point(362, 364);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(98, 23);
            this.btnAddNew.TabIndex = 12;
            this.btnAddNew.Text = "&Cập nhật tỉ giá";
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 369);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Kiểm tra tỉ giá hiện tại";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Xem tỉ giá ngày";
            // 
            // btnXem
            // 
            this.btnXem.Location = new System.Drawing.Point(215, 10);
            this.btnXem.Name = "btnXem";
            this.btnXem.Size = new System.Drawing.Size(50, 23);
            this.btnXem.TabIndex = 12;
            this.btnXem.Text = "Xem";
            this.btnXem.Click += new System.EventHandler(this.btnXem_Click);
            // 
            // btnDMTienTe
            // 
            this.btnDMTienTe.Location = new System.Drawing.Point(271, 10);
            this.btnDMTienTe.Name = "btnDMTienTe";
            this.btnDMTienTe.Size = new System.Drawing.Size(122, 23);
            this.btnDMTienTe.TabIndex = 12;
            this.btnDMTienTe.Text = "Danh sách loại tiền tệ";
            this.btnDMTienTe.Visible = false;
            this.btnDMTienTe.Click += new System.EventHandler(this.btnDMTienTe_Click);
            // 
            // Ngay
            // 
            this.Ngay.EditValue = null;
            this.Ngay.Location = new System.Drawing.Point(94, 13);
            this.Ngay.Name = "Ngay";
            this.Ngay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Ngay.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.Ngay.Size = new System.Drawing.Size(104, 20);
            this.Ngay.TabIndex = 16;
            // 
            // plMoneyType1
            // 
            this.plMoneyType1.Location = new System.Drawing.Point(127, 367);
            this.plMoneyType1.Name = "plMoneyType1";
            this.plMoneyType1.Size = new System.Drawing.Size(209, 20);
            this.plMoneyType1.TabIndex = 17;
            this.plMoneyType1.Visible = false;
            // 
            // frmCapNhatTiGia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 398);
            this.Controls.Add(this.plMoneyType1);
            this.Controls.Add(this.Ngay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.btnXem);
            this.Controls.Add(this.gridUpdateTiGia);
            this.Controls.Add(this.btnDMTienTe);
            this.Name = "frmCapNhatTiGia";
            this.Text = "Quản lý tiền tệ";
            this.Load += new System.EventHandler(this.frmCapNhatTiGia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridUpdateTiGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewUpdateTiGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ngay.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ngay.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraGrid.GridControl gridUpdateTiGia;
        private DevExpress.XtraGrid.Columns.GridColumn CotTienTe;
        private DevExpress.XtraGrid.Columns.GridColumn CotTiGiaHienTai;
        public DevExpress.XtraGrid.Columns.GridColumn CotTiGiaMoi;
        public PLGridView gridViewUpdateTiGia;
        private DevExpress.XtraGrid.Columns.GridColumn CotNGuoiCapNhat;
        private DevExpress.XtraGrid.Columns.GridColumn CotNgayCapNhat;
        public DevExpress.XtraEditors.SimpleButton btnAddNew;
        public DevExpress.XtraEditors.SimpleButton btnDMTienTe;
        private System.Windows.Forms.Label label2;
        public DevExpress.XtraEditors.SimpleButton btnXem;
        private System.Windows.Forms.Label label1;
        
        private DevExpress.XtraEditors.DateEdit Ngay;
        private PLMoneyType plMoneyType1;
        public  DevExpress.XtraGrid.Columns.GridColumn CotDong;
    }
}