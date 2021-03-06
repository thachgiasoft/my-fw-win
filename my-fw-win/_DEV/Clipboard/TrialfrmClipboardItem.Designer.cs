namespace ProtocolVN.Framework.Win
{
    partial class frmClipboardItem
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
            this.gridControlDetails = new DevExpress.XtraGrid.GridControl();
            this.gridViewDetails = new DevExpress.XtraGrid.Views.Grid.PLGridView();
            this.rep_Xoa = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.btn_Chon = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Dong = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_Xoa)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlDetails
            // 
            this.gridControlDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControlDetails.EmbeddedNavigator.Name = "";
            this.gridControlDetails.FormsUseDefaultLookAndFeel = false;
            this.gridControlDetails.Location = new System.Drawing.Point(12, 12);
            this.gridControlDetails.MainView = this.gridViewDetails;
            this.gridControlDetails.Name = "gridControlDetails";
            this.gridControlDetails.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rep_Xoa});
            this.gridControlDetails.Size = new System.Drawing.Size(513, 217);
            this.gridControlDetails.TabIndex = 0;
            this.gridControlDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDetails});
            // 
            // gridViewDetails
            // 
            this.gridViewDetails.GridControl = this.gridControlDetails;
            this.gridViewDetails.Name = "gridViewDetails";
            this.gridViewDetails.OptionsBehavior.Editable = false;
            this.gridViewDetails.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewDetails.OptionsView.ShowGroupPanel = false;
            // 
            // rep_Xoa
            // 
            this.rep_Xoa.AutoHeight = false;
            this.rep_Xoa.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.rep_Xoa.Name = "rep_Xoa";
            this.rep_Xoa.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // btn_Chon
            // 
            this.btn_Chon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Chon.Location = new System.Drawing.Point(369, 235);
            this.btn_Chon.Name = "btn_Chon";
            this.btn_Chon.Size = new System.Drawing.Size(75, 23);
            this.btn_Chon.TabIndex = 1;
            this.btn_Chon.Text = "Chọn";
            this.btn_Chon.Click += new System.EventHandler(this.btn_Chon_Click);
            // 
            // btn_Dong
            // 
            this.btn_Dong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Dong.Location = new System.Drawing.Point(450, 235);
            this.btn_Dong.Name = "btn_Dong";
            this.btn_Dong.Size = new System.Drawing.Size(75, 23);
            this.btn_Dong.TabIndex = 2;
            this.btn_Dong.Text = "Đóng";
            this.btn_Dong.Click += new System.EventHandler(this.btn_Dong_Click);
            // 
            // frmClipboardItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 263);
            this.Controls.Add(this.btn_Dong);
            this.Controls.Add(this.btn_Chon);
            this.Controls.Add(this.gridControlDetails);
            this.Name = "frmClipboardItem";
            this.Text = "Dữ liệu từ Clipboard";
            this.Load += new System.EventHandler(this.frmClipboardItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_Xoa)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlDetails;
        private DevExpress.XtraGrid.Views.Grid.PLGridView gridViewDetails;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rep_Xoa;
        private DevExpress.XtraEditors.SimpleButton btn_Chon;
        private DevExpress.XtraEditors.SimpleButton btn_Dong;
    }
}