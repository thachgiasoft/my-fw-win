namespace ProtocolVN.Framework.Win
{
    partial class frmGridInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGridInfo));
            this.gridDebug = new DevExpress.XtraGrid.GridControl();
            this.viewDebug = new DevExpress.XtraGrid.Views.Grid.PLGridView();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.cmdXoa = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridDebug)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewDebug)).BeginInit();
            this.SuspendLayout();
            // 
            // gridDebug
            // 
            this.gridDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridDebug.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gridDebug.BackgroundImage")));
            this.gridDebug.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.gridDebug.Location = new System.Drawing.Point(12, 31);
            this.gridDebug.MainView = this.viewDebug;
            this.gridDebug.Name = "gridDebug";
            this.gridDebug.Size = new System.Drawing.Size(677, 328);
            this.gridDebug.TabIndex = 0;
            this.gridDebug.ToolTipController = this.toolTipController1;
            this.gridDebug.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewDebug});
            // 
            // viewDebug
            // 
            this.viewDebug.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.viewDebug.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.viewDebug.GridControl = this.gridDebug;
            this.viewDebug.IndicatorWidth = 40;
            this.viewDebug.Name = "viewDebug";
            this.viewDebug.OptionsLayout.Columns.AddNewColumns = false;
            this.viewDebug.OptionsNavigation.AutoFocusNewRow = true;
            this.viewDebug.OptionsNavigation.EnterMoveNextColumn = true;
            this.viewDebug.OptionsView.AutoCalcPreviewLineCount = true;
            this.viewDebug.OptionsView.ColumnAutoWidth = false;
            this.viewDebug.OptionsView.EnableAppearanceEvenRow = true;
            this.viewDebug.OptionsView.EnableAppearanceOddRow = true;
            this.viewDebug.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.viewDebug.OptionsView.RowAutoHeight = true;
            this.viewDebug.OptionsView.ShowGroupedColumns = true;
            this.viewDebug.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(86, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Danh sách vấn đề";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.Location = new System.Drawing.Point(623, 365);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(66, 23);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "Đóng";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // cmdXoa
            // 
            this.cmdXoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdXoa.Location = new System.Drawing.Point(484, 365);
            this.cmdXoa.Name = "cmdXoa";
            this.cmdXoa.Size = new System.Drawing.Size(133, 23);
            this.cmdXoa.TabIndex = 3;
            this.cmdXoa.Text = "Xóa các ngoại lệ hiện tại";
            this.cmdXoa.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // frmGridInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 397);
            this.Controls.Add(this.cmdXoa);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.gridDebug);
            this.Name = "frmGridInfo";
            this.Text = "frmTrialDebug";
            ((System.ComponentModel.ISupportInitialize)(this.gridDebug)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewDebug)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraGrid.GridControl gridDebug;
        public DevExpress.XtraGrid.Views.Grid.PLGridView viewDebug;
        public DevExpress.Utils.ToolTipController toolTipController1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        public DevExpress.XtraEditors.SimpleButton cmdXoa;
    }
}