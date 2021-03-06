namespace ProtocolVN.Framework.Win
{
    partial class frmBackupRestore
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBackupRestore));
            this.lblBackupFileName = new System.Windows.Forms.Label();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnExec = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPageBackup = new DevExpress.XtraTab.XtraTabPage();
            this.txtDate = new DevExpress.XtraEditors.TextEdit();
            this.txtUser = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mmeDesBackup = new DevExpress.XtraEditors.MemoEdit();
            this.bteFileBackup = new DevExpress.XtraEditors.ButtonEdit();
            this.lblBackupDescription = new System.Windows.Forms.Label();
            this.xtraTabPageRestore = new DevExpress.XtraTab.XtraTabPage();
            this.txtDate2 = new DevExpress.XtraEditors.TextEdit();
            this.txtUser2 = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.mmeDesRestore = new DevExpress.XtraEditors.MemoEdit();
            this.bteFileRestore = new DevExpress.XtraEditors.ButtonEdit();
            this.lblRestoreDescription = new System.Windows.Forms.Label();
            this.lblRestoreFileName = new System.Windows.Forms.Label();
            this.xtraTabPageHistory = new DevExpress.XtraTab.XtraTabPage();
            this.gridControlHistory = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.PLGridView();
            this.gcIsBackup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcFilePath = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPageBackup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmeDesBackup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteFileBackup.Properties)).BeginInit();
            this.xtraTabPageRestore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUser2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmeDesRestore.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteFileRestore.Properties)).BeginInit();
            this.xtraTabPageHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblBackupFileName
            // 
            this.lblBackupFileName.AutoSize = true;
            this.lblBackupFileName.Location = new System.Drawing.Point(10, 62);
            this.lblBackupFileName.Name = "lblBackupFileName";
            this.lblBackupFileName.Size = new System.Drawing.Size(75, 13);
            this.lblBackupFileName.TabIndex = 6;
            this.lblBackupFileName.Text = "Tập tin dữ liệu";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(405, 243);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Đón&g";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnExec
            // 
            this.btnExec.Location = new System.Drawing.Point(324, 243);
            this.btnExec.Name = "btnExec";
            this.btnExec.Size = new System.Drawing.Size(75, 23);
            this.btnExec.TabIndex = 8;
            this.btnExec.Text = "&Thực hiện";
            this.btnExec.Click += new System.EventHandler(this.btnExec_Click);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(4, 10);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPageBackup;
            this.xtraTabControl1.Size = new System.Drawing.Size(479, 227);
            this.xtraTabControl1.TabIndex = 9;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageBackup,
            this.xtraTabPageRestore,
            this.xtraTabPageHistory});
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            // 
            // xtraTabPageBackup
            // 
            this.xtraTabPageBackup.Appearance.PageClient.BackColor = System.Drawing.Color.White;
            this.xtraTabPageBackup.Appearance.PageClient.Options.UseBackColor = true;
            this.xtraTabPageBackup.Controls.Add(this.txtDate);
            this.xtraTabPageBackup.Controls.Add(this.txtUser);
            this.xtraTabPageBackup.Controls.Add(this.label2);
            this.xtraTabPageBackup.Controls.Add(this.label1);
            this.xtraTabPageBackup.Controls.Add(this.mmeDesBackup);
            this.xtraTabPageBackup.Controls.Add(this.bteFileBackup);
            this.xtraTabPageBackup.Controls.Add(this.lblBackupDescription);
            this.xtraTabPageBackup.Controls.Add(this.lblBackupFileName);
            this.xtraTabPageBackup.Name = "xtraTabPageBackup";
            this.xtraTabPageBackup.Size = new System.Drawing.Size(472, 198);
            this.xtraTabPageBackup.Text = "Sao lưu dữ liệu";
            // 
            // txtDate
            // 
            this.txtDate.AllowDrop = true;
            this.txtDate.Enabled = false;
            this.txtDate.Location = new System.Drawing.Point(98, 33);
            this.txtDate.Name = "txtDate";
            this.txtDate.Properties.DisplayFormat.FormatString = "f";
            this.txtDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtDate.Size = new System.Drawing.Size(359, 20);
            this.txtDate.TabIndex = 12;
            // 
            // txtUser
            // 
            this.txtUser.Enabled = false;
            this.txtUser.Location = new System.Drawing.Point(98, 7);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(359, 20);
            this.txtUser.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Ngày thực hiện";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Người thực hiện";
            // 
            // mmeDesBackup
            // 
            this.mmeDesBackup.Location = new System.Drawing.Point(98, 86);
            this.mmeDesBackup.Name = "mmeDesBackup";
            this.mmeDesBackup.Size = new System.Drawing.Size(359, 100);
            this.mmeDesBackup.TabIndex = 7;
            // 
            // bteFileBackup
            // 
            this.bteFileBackup.Location = new System.Drawing.Point(98, 59);
            this.bteFileBackup.Name = "bteFileBackup";
            this.bteFileBackup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteFileBackup.Properties.ReadOnly = true;
            this.bteFileBackup.Size = new System.Drawing.Size(359, 20);
            this.bteFileBackup.TabIndex = 0;
            this.bteFileBackup.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteFileBackup_ButtonClick);
            // 
            // lblBackupDescription
            // 
            this.lblBackupDescription.AutoSize = true;
            this.lblBackupDescription.Location = new System.Drawing.Point(10, 86);
            this.lblBackupDescription.Name = "lblBackupDescription";
            this.lblBackupDescription.Size = new System.Drawing.Size(42, 13);
            this.lblBackupDescription.TabIndex = 6;
            this.lblBackupDescription.Text = "Ghi chú";
            // 
            // xtraTabPageRestore
            // 
            this.xtraTabPageRestore.Appearance.PageClient.BackColor = System.Drawing.Color.White;
            this.xtraTabPageRestore.Appearance.PageClient.Options.UseBackColor = true;
            this.xtraTabPageRestore.Controls.Add(this.txtDate2);
            this.xtraTabPageRestore.Controls.Add(this.txtUser2);
            this.xtraTabPageRestore.Controls.Add(this.label3);
            this.xtraTabPageRestore.Controls.Add(this.label4);
            this.xtraTabPageRestore.Controls.Add(this.mmeDesRestore);
            this.xtraTabPageRestore.Controls.Add(this.bteFileRestore);
            this.xtraTabPageRestore.Controls.Add(this.lblRestoreDescription);
            this.xtraTabPageRestore.Controls.Add(this.lblRestoreFileName);
            this.xtraTabPageRestore.Name = "xtraTabPageRestore";
            this.xtraTabPageRestore.Size = new System.Drawing.Size(472, 198);
            this.xtraTabPageRestore.Text = "Phục hồi dữ liệu";
            // 
            // txtDate2
            // 
            this.txtDate2.AllowDrop = true;
            this.txtDate2.Enabled = false;
            this.txtDate2.Location = new System.Drawing.Point(99, 34);
            this.txtDate2.Name = "txtDate2";
            this.txtDate2.Properties.DisplayFormat.FormatString = "f";
            this.txtDate2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtDate2.Size = new System.Drawing.Size(357, 20);
            this.txtDate2.TabIndex = 18;
            // 
            // txtUser2
            // 
            this.txtUser2.Enabled = false;
            this.txtUser2.Location = new System.Drawing.Point(99, 8);
            this.txtUser2.Name = "txtUser2";
            this.txtUser2.Size = new System.Drawing.Size(357, 20);
            this.txtUser2.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Ngày thực hiện";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Người thực hiện";
            // 
            // mmeDesRestore
            // 
            this.mmeDesRestore.Location = new System.Drawing.Point(99, 92);
            this.mmeDesRestore.Name = "mmeDesRestore";
            this.mmeDesRestore.Size = new System.Drawing.Size(357, 100);
            this.mmeDesRestore.TabIndex = 13;
            // 
            // bteFileRestore
            // 
            this.bteFileRestore.Location = new System.Drawing.Point(99, 63);
            this.bteFileRestore.Name = "bteFileRestore";
            this.bteFileRestore.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteFileRestore.Properties.ReadOnly = true;
            this.bteFileRestore.Size = new System.Drawing.Size(357, 20);
            this.bteFileRestore.TabIndex = 9;
            this.bteFileRestore.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteFileRestore_ButtonClick);
            // 
            // lblRestoreDescription
            // 
            this.lblRestoreDescription.AutoSize = true;
            this.lblRestoreDescription.Location = new System.Drawing.Point(11, 92);
            this.lblRestoreDescription.Name = "lblRestoreDescription";
            this.lblRestoreDescription.Size = new System.Drawing.Size(42, 13);
            this.lblRestoreDescription.TabIndex = 12;
            this.lblRestoreDescription.Text = "Ghi chú";
            // 
            // lblRestoreFileName
            // 
            this.lblRestoreFileName.AutoSize = true;
            this.lblRestoreFileName.Location = new System.Drawing.Point(11, 63);
            this.lblRestoreFileName.Name = "lblRestoreFileName";
            this.lblRestoreFileName.Size = new System.Drawing.Size(75, 13);
            this.lblRestoreFileName.TabIndex = 10;
            this.lblRestoreFileName.Text = "Tập tin dữ liệu";
            // 
            // xtraTabPageHistory
            // 
            this.xtraTabPageHistory.Controls.Add(this.gridControlHistory);
            this.xtraTabPageHistory.Name = "xtraTabPageHistory";
            this.xtraTabPageHistory.Size = new System.Drawing.Size(472, 198);
            this.xtraTabPageHistory.Text = "Lịch sử sao lưu";
            // 
            // gridControlHistory
            // 
            this.gridControlHistory.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gridControlHistory.BackgroundImage")));
            this.gridControlHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.gridControlHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlHistory.Location = new System.Drawing.Point(0, 0);
            this.gridControlHistory.MainView = this.gridView1;
            this.gridControlHistory.Name = "gridControlHistory";
            this.gridControlHistory.Size = new System.Drawing.Size(472, 198);
            this.gridControlHistory.TabIndex = 0;
            this.gridControlHistory.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcIsBackup,
            this.gcUserName,
            this.gcCDate,
            this.gcFilePath,
            this.gcDescription});
            this.gridView1.GridControl = this.gridControlHistory;
            this.gridView1.GroupPanelText = "Các sao lưu trước";
            this.gridView1.IndicatorWidth = 40;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsLayout.Columns.AddNewColumns = false;
            this.gridView1.OptionsNavigation.AutoFocusNewRow = true;
            this.gridView1.OptionsNavigation.EnterMoveNextColumn = true;
            this.gridView1.OptionsPrint.UsePrintStyles = true;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gridView1.OptionsView.ShowGroupedColumns = true;
            // 
            // gcIsBackup
            // 
            this.gcIsBackup.AppearanceCell.Options.UseTextOptions = true;
            this.gcIsBackup.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcIsBackup.AppearanceHeader.Options.UseTextOptions = true;
            this.gcIsBackup.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcIsBackup.Caption = "Thao tác";
            this.gcIsBackup.FieldName = "ISB";
            this.gcIsBackup.Name = "gcIsBackup";
            this.gcIsBackup.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.gcIsBackup.Visible = true;
            this.gcIsBackup.VisibleIndex = 4;
            this.gcIsBackup.Width = 54;
            // 
            // gcUserName
            // 
            this.gcUserName.AppearanceHeader.Options.UseTextOptions = true;
            this.gcUserName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcUserName.Caption = "Người dùng";
            this.gcUserName.FieldName = "USERNAME";
            this.gcUserName.Name = "gcUserName";
            this.gcUserName.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.gcUserName.Visible = true;
            this.gcUserName.VisibleIndex = 0;
            this.gcUserName.Width = 67;
            // 
            // gcCDate
            // 
            this.gcCDate.AppearanceCell.Options.UseTextOptions = true;
            this.gcCDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcCDate.AppearanceHeader.Options.UseTextOptions = true;
            this.gcCDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcCDate.Caption = "Ngày thực hiện";
            this.gcCDate.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";
            this.gcCDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gcCDate.FieldName = "CDATE";
            this.gcCDate.Name = "gcCDate";
            this.gcCDate.OptionsColumn.FixedWidth = true;
            this.gcCDate.UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
            this.gcCDate.Visible = true;
            this.gcCDate.VisibleIndex = 1;
            this.gcCDate.Width = 94;
            // 
            // gcFilePath
            // 
            this.gcFilePath.AppearanceHeader.Options.UseTextOptions = true;
            this.gcFilePath.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcFilePath.Caption = "Tập tin dữ liệu";
            this.gcFilePath.FieldName = "FILEPATH";
            this.gcFilePath.Name = "gcFilePath";
            this.gcFilePath.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.gcFilePath.Visible = true;
            this.gcFilePath.VisibleIndex = 2;
            this.gcFilePath.Width = 80;
            // 
            // gcDescription
            // 
            this.gcDescription.AppearanceHeader.Options.UseTextOptions = true;
            this.gcDescription.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcDescription.Caption = "Ghi chú";
            this.gcDescription.FieldName = "DESCRIPTION";
            this.gcDescription.Name = "gcDescription";
            this.gcDescription.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.gcDescription.Visible = true;
            this.gcDescription.VisibleIndex = 3;
            this.gcDescription.Width = 47;
            // 
            // frmBackupRestore
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 275);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.btnExec);
            this.Controls.Add(this.btnClose);
            this.Name = "frmBackupRestore";
            this.Text = "Sao lưu - Phục hồi dữ liệu";
            this.Load += new System.EventHandler(this.frmBackupRestore_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPageBackup.ResumeLayout(false);
            this.xtraTabPageBackup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmeDesBackup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteFileBackup.Properties)).EndInit();
            this.xtraTabPageRestore.ResumeLayout(false);
            this.xtraTabPageRestore.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUser2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmeDesRestore.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteFileRestore.Properties)).EndInit();
            this.xtraTabPageHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblBackupFileName;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnExec;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageBackup;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageRestore;
        private DevExpress.XtraEditors.ButtonEdit bteFileBackup;
        private DevExpress.XtraEditors.MemoEdit mmeDesBackup;
        private System.Windows.Forms.Label lblBackupDescription;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageHistory;
        private DevExpress.XtraGrid.GridControl gridControlHistory;
        private DevExpress.XtraGrid.Views.Grid.PLGridView gridView1;
        private DevExpress.XtraEditors.MemoEdit mmeDesRestore;
        private DevExpress.XtraEditors.ButtonEdit bteFileRestore;
        private System.Windows.Forms.Label lblRestoreDescription;
        private System.Windows.Forms.Label lblRestoreFileName;
        private DevExpress.XtraGrid.Columns.GridColumn gcCDate;
        private DevExpress.XtraGrid.Columns.GridColumn gcFilePath;
        private DevExpress.XtraGrid.Columns.GridColumn gcDescription;
        private DevExpress.XtraGrid.Columns.GridColumn gcIsBackup;
        private DevExpress.XtraGrid.Columns.GridColumn gcUserName;
        private DevExpress.XtraEditors.TextEdit txtUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtDate;
        private DevExpress.XtraEditors.TextEdit txtDate2;
        private DevExpress.XtraEditors.TextEdit txtUser2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}