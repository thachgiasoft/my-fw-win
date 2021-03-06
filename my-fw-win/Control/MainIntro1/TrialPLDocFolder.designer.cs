namespace ProtocolVN.Framework.Win
{
    partial class PLDocFolder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PLDocFolder));
            this.repositoryItemButtonDownload = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridFTPControl = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonUp = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridColumnDescript = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colDownload = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.btnUpload = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonDownload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridFTPControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonDelete)).BeginInit();
            this.SuspendLayout();
            // 
            // repositoryItemButtonDownload
            // 
            this.repositoryItemButtonDownload.AutoHeight = false;
            this.repositoryItemButtonDownload.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonDownload.Buttons"))), null)});
            this.repositoryItemButtonDownload.Name = "repositoryItemButtonDownload";
            this.repositoryItemButtonDownload.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repositoryItemButtonDownload.Click += new System.EventHandler(this.repositoryItemButtonDownload_Click);
            // 
            // gridFTPControl
            // 
            this.gridFTPControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridFTPControl.EmbeddedNavigator.Name = "";
            this.gridFTPControl.FormsUseDefaultLookAndFeel = false;
            this.gridFTPControl.Location = new System.Drawing.Point(0, 0);
            this.gridFTPControl.MainView = this.gridView1;
            this.gridFTPControl.Name = "gridFTPControl";
            this.gridFTPControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonDelete,
            this.repositoryItemButtonDownload,
            this.repositoryItemButtonUp,
            this.repositoryItemTextEdit2});
            this.gridFTPControl.Size = new System.Drawing.Size(327, 103);
            this.gridFTPControl.TabIndex = 10;
            this.gridFTPControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumnDescript,
            this.colDownload,
            this.colDelete});
            this.gridView1.GridControl = this.gridFTPControl;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsMenu.EnableColumnMenu = false;
            this.gridView1.OptionsMenu.EnableFooterMenu = false;
            this.gridView1.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridView1.OptionsNavigation.AutoMoveRowFocus = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.None;
            this.gridView1.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.Default;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceHeader.Image = ((System.Drawing.Image)(resources.GetObject("gridColumn2.AppearanceHeader.Image")));
            this.gridColumn2.AppearanceHeader.Options.UseImage = true;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "Tên tập tin";
            this.gridColumn2.ColumnEdit = this.repositoryItemButtonUp;
            this.gridColumn2.FieldName = "FILE_NAME";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 231;
            // 
            // repositoryItemButtonUp
            // 
            this.repositoryItemButtonUp.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.Utils.HorzAlignment.Center, ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonUp.Buttons"))), new DevExpress.Utils.KeyShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)))});
            this.repositoryItemButtonUp.Name = "repositoryItemButtonUp";
            // 
            // gridColumnDescript
            // 
            this.gridColumnDescript.Caption = "Mô tả";
            this.gridColumnDescript.ColumnEdit = this.repositoryItemTextEdit2;
            this.gridColumnDescript.FieldName = "FILE_DESCRIPT";
            this.gridColumnDescript.Name = "gridColumnDescript";
            this.gridColumnDescript.Width = 106;
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // colDownload
            // 
            this.colDownload.AppearanceCell.Options.UseTextOptions = true;
            this.colDownload.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDownload.AppearanceHeader.Options.UseTextOptions = true;
            this.colDownload.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDownload.Caption = "Mở";
            this.colDownload.ColumnEdit = this.repositoryItemButtonDownload;
            this.colDownload.MinWidth = 15;
            this.colDownload.Name = "colDownload";
            this.colDownload.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colDownload.OptionsColumn.AllowMove = false;
            this.colDownload.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDownload.OptionsColumn.FixedWidth = true;
            this.colDownload.Visible = true;
            this.colDownload.VisibleIndex = 1;
            this.colDownload.Width = 32;
            // 
            // colDelete
            // 
            this.colDelete.AppearanceCell.Options.UseTextOptions = true;
            this.colDelete.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDelete.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDelete.AppearanceHeader.Options.UseTextOptions = true;
            this.colDelete.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDelete.Caption = "Xóa";
            this.colDelete.ColumnEdit = this.repositoryItemButtonDelete;
            this.colDelete.MinWidth = 15;
            this.colDelete.Name = "colDelete";
            this.colDelete.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colDelete.OptionsColumn.AllowMove = false;
            this.colDelete.OptionsColumn.AllowSize = false;
            this.colDelete.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDelete.OptionsColumn.FixedWidth = true;
            this.colDelete.Visible = true;
            this.colDelete.VisibleIndex = 2;
            this.colDelete.Width = 31;
            // 
            // repositoryItemButtonDelete
            // 
            this.repositoryItemButtonDelete.AutoHeight = false;
            this.repositoryItemButtonDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonDelete.Buttons"))), null)});
            this.repositoryItemButtonDelete.Name = "repositoryItemButtonDelete";
            this.repositoryItemButtonDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repositoryItemButtonDelete.Click += new System.EventHandler(this.repositoryItemButtonDelete_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.btnUpload.Appearance.BackColor2 = System.Drawing.Color.Transparent;
            this.btnUpload.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.btnUpload.Appearance.ForeColor = System.Drawing.Color.Transparent;
            this.btnUpload.Appearance.Options.UseBackColor = true;
            this.btnUpload.Appearance.Options.UseBorderColor = true;
            this.btnUpload.Appearance.Options.UseForeColor = true;
            this.btnUpload.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnUpload.Image = ((System.Drawing.Image)(resources.GetObject("btnUpload.Image")));
            this.btnUpload.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnUpload.Location = new System.Drawing.Point(4, 4);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(16, 15);
            this.btnUpload.TabIndex = 11;
            this.btnUpload.ToolTip = "Thêm tập tin";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // PLDocFolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.gridFTPControl);
            this.Name = "PLDocFolder";
            this.Size = new System.Drawing.Size(327, 103);
            this.Load += new System.EventHandler(this.FTPControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonDownload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridFTPControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonDelete)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridFTPControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonDelete;
        //private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnUpload;
        //private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnDownload;
        //private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnDelete;
       
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonDownload;
        private DevExpress.XtraGrid.Columns.GridColumn colDownload;
        private DevExpress.XtraGrid.Columns.GridColumn colDelete;
       
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;

        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonUp;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDescript;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraEditors.SimpleButton btnUpload;
    }
}
