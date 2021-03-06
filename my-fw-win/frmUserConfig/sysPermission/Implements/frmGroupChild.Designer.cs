using DevExpress.XtraEditors.DXErrorProvider;
namespace ProtocolVN.Framework.Win
{
    partial class frmGroupChild
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGroupChild));
            this.lblGroupName = new System.Windows.Forms.Label();
            this.txtGroupName = new DevExpress.XtraEditors.TextEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.gridControlThanhPhanUser = new DevExpress.XtraGrid.GridControl();
            this.gridViewThanhPhanUser = new DevExpress.XtraGrid.Views.Grid.PLGridView();
            this.colUSERNAME_TP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFULLNAME_TP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDEPARTMENTNAME_TP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtGroupName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlThanhPhanUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewThanhPhanUser)).BeginInit();
            this.SuspendLayout();
            // 
            // lblGroupName
            // 
            this.lblGroupName.AutoSize = true;
            this.lblGroupName.Location = new System.Drawing.Point(7, 13);
            this.lblGroupName.Name = "lblGroupName";
            this.lblGroupName.Size = new System.Drawing.Size(111, 13);
            this.lblGroupName.TabIndex = 0;
            this.lblGroupName.Text = "Tên nhóm người dùng";
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(119, 9);
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Properties.MaxLength = 63;
            this.txtGroupName.Size = new System.Drawing.Size(194, 20);
            this.txtGroupName.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(253, 255);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Đón&g";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(187, 255);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "&Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // gridControlThanhPhanUser
            // 
            this.gridControlThanhPhanUser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gridControlThanhPhanUser.BackgroundImage")));
            this.gridControlThanhPhanUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.gridControlThanhPhanUser.Location = new System.Drawing.Point(8, 65);
            this.gridControlThanhPhanUser.MainView = this.gridViewThanhPhanUser;
            this.gridControlThanhPhanUser.Margin = new System.Windows.Forms.Padding(0);
            this.gridControlThanhPhanUser.Name = "gridControlThanhPhanUser";
            this.gridControlThanhPhanUser.Size = new System.Drawing.Size(305, 181);
            this.gridControlThanhPhanUser.TabIndex = 3;
            this.gridControlThanhPhanUser.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewThanhPhanUser});
            // 
            // gridViewThanhPhanUser
            // 
            this.gridViewThanhPhanUser.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewThanhPhanUser.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewThanhPhanUser.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUSERNAME_TP,
            this.colFULLNAME_TP,
            this.colDEPARTMENTNAME_TP});
            this.gridViewThanhPhanUser.GridControl = this.gridControlThanhPhanUser;
            this.gridViewThanhPhanUser.GroupPanelText = "Danh sách người dùng trong nhóm";
            this.gridViewThanhPhanUser.IndicatorWidth = 40;
            this.gridViewThanhPhanUser.Name = "gridViewThanhPhanUser";
            this.gridViewThanhPhanUser.OptionsBehavior.Editable = false;
            this.gridViewThanhPhanUser.OptionsCustomization.AllowFilter = false;
            this.gridViewThanhPhanUser.OptionsCustomization.AllowGroup = false;
            this.gridViewThanhPhanUser.OptionsLayout.Columns.AddNewColumns = false;
            this.gridViewThanhPhanUser.OptionsNavigation.AutoFocusNewRow = true;
            this.gridViewThanhPhanUser.OptionsNavigation.EnterMoveNextColumn = true;
            this.gridViewThanhPhanUser.OptionsPrint.UsePrintStyles = true;
            this.gridViewThanhPhanUser.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewThanhPhanUser.OptionsView.EnableAppearanceOddRow = true;
            this.gridViewThanhPhanUser.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gridViewThanhPhanUser.OptionsView.ShowGroupedColumns = true;
            // 
            // colUSERNAME_TP
            // 
            this.colUSERNAME_TP.AppearanceHeader.Options.UseTextOptions = true;
            this.colUSERNAME_TP.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUSERNAME_TP.Caption = "Tên truy cập";
            this.colUSERNAME_TP.FieldName = "USERNAME";
            this.colUSERNAME_TP.Name = "colUSERNAME_TP";
            this.colUSERNAME_TP.Visible = true;
            this.colUSERNAME_TP.VisibleIndex = 0;
            this.colUSERNAME_TP.Width = 73;
            // 
            // colFULLNAME_TP
            // 
            this.colFULLNAME_TP.AppearanceHeader.Options.UseTextOptions = true;
            this.colFULLNAME_TP.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFULLNAME_TP.Caption = "Họ tên nhân viên";
            this.colFULLNAME_TP.FieldName = "EMPLOYEE_NAME";
            this.colFULLNAME_TP.Name = "colFULLNAME_TP";
            this.colFULLNAME_TP.Visible = true;
            this.colFULLNAME_TP.VisibleIndex = 1;
            this.colFULLNAME_TP.Width = 94;
            // 
            // colDEPARTMENTNAME_TP
            // 
            this.colDEPARTMENTNAME_TP.AppearanceHeader.Options.UseTextOptions = true;
            this.colDEPARTMENTNAME_TP.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDEPARTMENTNAME_TP.Caption = "Phòng ban";
            this.colDEPARTMENTNAME_TP.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colDEPARTMENTNAME_TP.FieldName = "DEPARTMENT_NAME";
            this.colDEPARTMENTNAME_TP.Name = "colDEPARTMENTNAME_TP";
            this.colDEPARTMENTNAME_TP.Visible = true;
            this.colDEPARTMENTNAME_TP.VisibleIndex = 2;
            this.colDEPARTMENTNAME_TP.Width = 63;
            // 
            // btnDelete
            // 
            this.btnDelete.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnDelete.Location = new System.Drawing.Point(253, 35);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.Click += new System.EventHandler(this.simpleButtonXoaUser_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnSelect.Location = new System.Drawing.Point(224, 35);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(23, 23);
            this.btnSelect.TabIndex = 1;
            // 
            // frmGroupChild
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 285);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.gridControlThanhPhanUser);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtGroupName);
            this.Controls.Add(this.lblGroupName);
            this.MaximizeBox = false;
            this.Name = "frmGroupChild";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nhóm người dùng";
            this.Load += new System.EventHandler(this.frmGroupChild_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtGroupName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlThanhPhanUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewThanhPhanUser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGroupName;
        private DevExpress.XtraEditors.TextEdit txtGroupName;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        public DevExpress.XtraEditors.SimpleButton btnSave;
        private DXErrorProvider errorProvider;
        private DevExpress.XtraGrid.GridControl gridControlThanhPhanUser;
        private DevExpress.XtraGrid.Views.Grid.PLGridView gridViewThanhPhanUser;
        private DevExpress.XtraGrid.Columns.GridColumn colUSERNAME_TP;
        private DevExpress.XtraGrid.Columns.GridColumn colFULLNAME_TP;
        private DevExpress.XtraGrid.Columns.GridColumn colDEPARTMENTNAME_TP;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnSelect;
    }
}