using DevExpress.XtraGrid;
using ProtocolVN.Framework.Win;
namespace ProtocolVN.Framework.Win
{
    partial class SaveQueryDialog
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
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.filterControl1 = new DevExpress.XtraEditors.FilterControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.xtraTabControl2 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.ceToolTips = new DevExpress.XtraEditors.CheckEdit();
            this.ceOperandTypeIcon = new DevExpress.XtraEditors.CheckEdit();
            this.ceGroupCommandsIcon = new DevExpress.XtraEditors.CheckEdit();
            this.seSeparatorHeight = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.seLevelIndent = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
            this.ceValue = new DevExpress.XtraEditors.ColorEdit();
            this.ceOperator = new DevExpress.XtraEditors.ColorEdit();
            this.ceGroupOperator = new DevExpress.XtraEditors.ColorEdit();
            this.ceFieldName = new DevExpress.XtraEditors.ColorEdit();
            this.ceEmptyValue = new DevExpress.XtraEditors.ColorEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.btnXoaCauTruyVan = new DevExpress.XtraEditors.SimpleButton();
            this.cbbSqlFilter1 = new ProtocolVN.Framework.Win.PLCombobox();
            this.rsFilter1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnLoadFilter1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave1 = new DevExpress.XtraEditors.SimpleButton();
            this.sbApply = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl2)).BeginInit();
            this.xtraTabControl2.SuspendLayout();
            this.xtraTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceToolTips.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceOperandTypeIcon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceGroupCommandsIcon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seSeparatorHeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seLevelIndent.Properties)).BeginInit();
            this.xtraTabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceOperator.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceGroupOperator.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceFieldName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceEmptyValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(710, 249);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.splitContainerControl1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(703, 220);
            this.xtraTabPage1.Text = "Điều kiện tìm";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.filterControl1);
            this.splitContainerControl1.Panel1.Controls.Add(this.panelControl2);
            this.splitContainerControl1.Panel1.Controls.Add(this.xtraTabControl2);
            this.splitContainerControl1.Panel1.Controls.Add(this.panelControl1);
            this.splitContainerControl1.Panel1.Padding = new System.Windows.Forms.Padding(4);
            this.splitContainerControl1.Panel1.Text = "splitContainerControl1_Panel1";
            this.splitContainerControl1.Panel2.Padding = new System.Windows.Forms.Padding(4);
            this.splitContainerControl1.Panel2.Text = "splitContainerControl1_Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(703, 220);
            this.splitContainerControl1.SplitterPosition = 220;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // filterControl1
            // 
            this.filterControl1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.filterControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterControl1.Location = new System.Drawing.Point(4, 4);
            this.filterControl1.Name = "filterControl1";
            this.filterControl1.Size = new System.Drawing.Size(432, 178);
            this.filterControl1.TabIndex = 0;
            this.filterControl1.Text = "filterControl1";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl2.Location = new System.Drawing.Point(436, 4);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(10, 178);
            this.panelControl2.TabIndex = 3;
            // 
            // xtraTabControl2
            // 
            this.xtraTabControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.xtraTabControl2.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.xtraTabControl2.Location = new System.Drawing.Point(446, 4);
            this.xtraTabControl2.Name = "xtraTabControl2";
            this.xtraTabControl2.SelectedTabPage = this.xtraTabPage3;
            this.xtraTabControl2.Size = new System.Drawing.Size(253, 178);
            this.xtraTabControl2.TabIndex = 2;
            this.xtraTabControl2.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage3,
            this.xtraTabPage4});
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.ceToolTips);
            this.xtraTabPage3.Controls.Add(this.ceOperandTypeIcon);
            this.xtraTabPage3.Controls.Add(this.ceGroupCommandsIcon);
            this.xtraTabPage3.Controls.Add(this.seSeparatorHeight);
            this.xtraTabPage3.Controls.Add(this.labelControl7);
            this.xtraTabPage3.Controls.Add(this.seLevelIndent);
            this.xtraTabPage3.Controls.Add(this.labelControl6);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(224, 171);
            this.xtraTabPage3.Text = "Mở rộng truy vấn";
            // 
            // ceToolTips
            // 
            this.ceToolTips.Location = new System.Drawing.Point(8, 126);
            this.ceToolTips.Name = "ceToolTips";
            this.ceToolTips.Properties.Caption = "Hiện Tool Tips";
            this.ceToolTips.Size = new System.Drawing.Size(133, 19);
            this.ceToolTips.TabIndex = 12;
            this.ceToolTips.CheckedChanged += new System.EventHandler(this.ceToolTips_CheckedChanged);
            // 
            // ceOperandTypeIcon
            // 
            this.ceOperandTypeIcon.Location = new System.Drawing.Point(8, 102);
            this.ceOperandTypeIcon.Name = "ceOperandTypeIcon";
            this.ceOperandTypeIcon.Properties.Caption = "Hiện kiểu Icon";
            this.ceOperandTypeIcon.Size = new System.Drawing.Size(133, 19);
            this.ceOperandTypeIcon.TabIndex = 11;
            this.ceOperandTypeIcon.CheckedChanged += new System.EventHandler(this.ceOperandTypeIcon_CheckedChanged);
            // 
            // ceGroupCommandsIcon
            // 
            this.ceGroupCommandsIcon.Location = new System.Drawing.Point(8, 78);
            this.ceGroupCommandsIcon.Name = "ceGroupCommandsIcon";
            this.ceGroupCommandsIcon.Properties.Caption = "Hiện nhóm truy vấn";
            this.ceGroupCommandsIcon.Size = new System.Drawing.Size(133, 19);
            this.ceGroupCommandsIcon.TabIndex = 10;
            this.ceGroupCommandsIcon.CheckedChanged += new System.EventHandler(this.ceGroupCommandsIcon_CheckedChanged);
            // 
            // seSeparatorHeight
            // 
            this.seSeparatorHeight.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seSeparatorHeight.Location = new System.Drawing.Point(167, 46);
            this.seSeparatorHeight.Name = "seSeparatorHeight";
            this.seSeparatorHeight.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seSeparatorHeight.Properties.IsFloatValue = false;
            this.seSeparatorHeight.Properties.Mask.EditMask = "N00";
            this.seSeparatorHeight.Properties.MaxValue = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.seSeparatorHeight.Size = new System.Drawing.Size(49, 20);
            this.seSeparatorHeight.TabIndex = 9;
            this.seSeparatorHeight.EditValueChanged += new System.EventHandler(this.seSeparatorHeight_EditValueChanged);
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Options.UseTextOptions = true;
            this.labelControl7.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl7.Location = new System.Drawing.Point(10, 52);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(151, 13);
            this.labelControl7.TabIndex = 8;
            this.labelControl7.Text = "Khoảng cách các dòng truy vấn";
            // 
            // seLevelIndent
            // 
            this.seLevelIndent.EditValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.seLevelIndent.Location = new System.Drawing.Point(167, 22);
            this.seLevelIndent.Name = "seLevelIndent";
            this.seLevelIndent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seLevelIndent.Properties.IsFloatValue = false;
            this.seLevelIndent.Properties.Mask.EditMask = "N00";
            this.seLevelIndent.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.seLevelIndent.Properties.MinValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.seLevelIndent.Size = new System.Drawing.Size(49, 20);
            this.seLevelIndent.TabIndex = 7;
            this.seLevelIndent.EditValueChanged += new System.EventHandler(this.seLevelIndent_EditValueChanged);
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Options.UseTextOptions = true;
            this.labelControl6.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl6.Location = new System.Drawing.Point(10, 25);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(122, 13);
            this.labelControl6.TabIndex = 6;
            this.labelControl6.Text = "Khoảng cách so với lề trái";
            // 
            // xtraTabPage4
            // 
            this.xtraTabPage4.Controls.Add(this.ceValue);
            this.xtraTabPage4.Controls.Add(this.ceOperator);
            this.xtraTabPage4.Controls.Add(this.ceGroupOperator);
            this.xtraTabPage4.Controls.Add(this.ceFieldName);
            this.xtraTabPage4.Controls.Add(this.ceEmptyValue);
            this.xtraTabPage4.Controls.Add(this.labelControl5);
            this.xtraTabPage4.Controls.Add(this.labelControl4);
            this.xtraTabPage4.Controls.Add(this.labelControl3);
            this.xtraTabPage4.Controls.Add(this.labelControl2);
            this.xtraTabPage4.Controls.Add(this.labelControl1);
            this.xtraTabPage4.Name = "xtraTabPage4";
            this.xtraTabPage4.Size = new System.Drawing.Size(224, 158);
            this.xtraTabPage4.Text = "Màu";
            // 
            // ceValue
            // 
            this.ceValue.EditValue = System.Drawing.Color.Empty;
            this.ceValue.Location = new System.Drawing.Point(103, 121);
            this.ceValue.Name = "ceValue";
            this.ceValue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ceValue.Size = new System.Drawing.Size(112, 20);
            this.ceValue.TabIndex = 4;
            this.ceValue.EditValueChanged += new System.EventHandler(this.ceValue_EditValueChanged);
            // 
            // ceOperator
            // 
            this.ceOperator.EditValue = System.Drawing.Color.Empty;
            this.ceOperator.Location = new System.Drawing.Point(103, 97);
            this.ceOperator.Name = "ceOperator";
            this.ceOperator.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ceOperator.Size = new System.Drawing.Size(112, 20);
            this.ceOperator.TabIndex = 3;
            this.ceOperator.EditValueChanged += new System.EventHandler(this.ceOperator_EditValueChanged);
            // 
            // ceGroupOperator
            // 
            this.ceGroupOperator.EditValue = System.Drawing.Color.Empty;
            this.ceGroupOperator.Location = new System.Drawing.Point(103, 73);
            this.ceGroupOperator.Name = "ceGroupOperator";
            this.ceGroupOperator.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ceGroupOperator.Size = new System.Drawing.Size(112, 20);
            this.ceGroupOperator.TabIndex = 2;
            this.ceGroupOperator.EditValueChanged += new System.EventHandler(this.ceGroupOperator_EditValueChanged);
            // 
            // ceFieldName
            // 
            this.ceFieldName.EditValue = System.Drawing.Color.Empty;
            this.ceFieldName.Location = new System.Drawing.Point(103, 49);
            this.ceFieldName.Name = "ceFieldName";
            this.ceFieldName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ceFieldName.Size = new System.Drawing.Size(112, 20);
            this.ceFieldName.TabIndex = 1;
            this.ceFieldName.EditValueChanged += new System.EventHandler(this.ceFieldName_EditValueChanged);
            // 
            // ceEmptyValue
            // 
            this.ceEmptyValue.EditValue = System.Drawing.Color.Empty;
            this.ceEmptyValue.Location = new System.Drawing.Point(103, 25);
            this.ceEmptyValue.Name = "ceEmptyValue";
            this.ceEmptyValue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ceEmptyValue.Size = new System.Drawing.Size(112, 20);
            this.ceEmptyValue.TabIndex = 0;
            this.ceEmptyValue.EditValueChanged += new System.EventHandler(this.ceEmptyValue_EditValueChanged);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Options.UseTextOptions = true;
            this.labelControl5.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl5.Location = new System.Drawing.Point(11, 124);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(28, 13);
            this.labelControl5.TabIndex = 9;
            this.labelControl5.Text = "Giá trị";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Options.UseTextOptions = true;
            this.labelControl4.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl4.Location = new System.Drawing.Point(11, 100);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(41, 13);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "Toán tử ";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl3.Location = new System.Drawing.Point(11, 76);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(69, 13);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "Nhóm toán tử ";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Options.UseTextOptions = true;
            this.labelControl2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl2.Location = new System.Drawing.Point(11, 52);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(62, 13);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "Tên cột chọn";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl1.Location = new System.Drawing.Point(11, 28);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(86, 13);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "Chưa nhập giá trị ";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.labelControl8);
            this.panelControl1.Controls.Add(this.btnXoaCauTruyVan);
            this.panelControl1.Controls.Add(this.cbbSqlFilter1);
            this.panelControl1.Controls.Add(this.rsFilter1);
            this.panelControl1.Controls.Add(this.btnLoadFilter1);
            this.panelControl1.Controls.Add(this.btnSave1);
            this.panelControl1.Controls.Add(this.sbApply);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(4, 182);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(695, 28);
            this.panelControl1.TabIndex = 1;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(6, 8);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(94, 13);
            this.labelControl8.TabIndex = 45;
            this.labelControl8.Text = "Điều kiện tìm đã lưu";
            // 
            // btnXoaCauTruyVan
            // 
            this.btnXoaCauTruyVan.Location = new System.Drawing.Point(425, 3);
            this.btnXoaCauTruyVan.Name = "btnXoaCauTruyVan";
            this.btnXoaCauTruyVan.Size = new System.Drawing.Size(37, 22);
            this.btnXoaCauTruyVan.TabIndex = 44;
            this.btnXoaCauTruyVan.Text = "&Xóa";
            this.btnXoaCauTruyVan.Click += new System.EventHandler(this.btnXoaCauTruyVan_Click);
            // 
            // cbbSqlFilter1
            // 
            this.cbbSqlFilter1.DataSource = null;
            this.cbbSqlFilter1.DisplayField = null;
            this.cbbSqlFilter1.Location = new System.Drawing.Point(106, 4);
            this.cbbSqlFilter1.Name = "cbbSqlFilter1";
            this.cbbSqlFilter1.Size = new System.Drawing.Size(274, 20);
            this.cbbSqlFilter1.TabIndex = 43;
            this.cbbSqlFilter1.ValueField = null;
            // 
            // rsFilter1
            // 
            this.rsFilter1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rsFilter1.Location = new System.Drawing.Point(557, 3);
            this.rsFilter1.Name = "rsFilter1";
            this.rsFilter1.Size = new System.Drawing.Size(63, 22);
            this.rsFilter1.TabIndex = 4;
            this.rsFilter1.Text = "Làm &mới";
            this.rsFilter1.Click += new System.EventHandler(this.rsFilter1_Click);
            // 
            // btnLoadFilter1
            // 
            this.btnLoadFilter1.Location = new System.Drawing.Point(385, 3);
            this.btnLoadFilter1.Name = "btnLoadFilter1";
            this.btnLoadFilter1.Size = new System.Drawing.Size(37, 22);
            this.btnLoadFilter1.TabIndex = 2;
            this.btnLoadFilter1.Text = "&Nạp";
            this.btnLoadFilter1.Click += new System.EventHandler(this.btnLoadFilter1_Click);
            // 
            // btnSave1
            // 
            this.btnSave1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave1.Location = new System.Drawing.Point(626, 3);
            this.btnSave1.Name = "btnSave1";
            this.btnSave1.Size = new System.Drawing.Size(63, 22);
            this.btnSave1.TabIndex = 1;
            this.btnSave1.Text = "&Lưu";
            this.btnSave1.Click += new System.EventHandler(this.btnSave1_Click);
            // 
            // sbApply
            // 
            this.sbApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sbApply.Location = new System.Drawing.Point(488, 3);
            this.sbApply.Name = "sbApply";
            this.sbApply.Size = new System.Drawing.Size(63, 22);
            this.sbApply.TabIndex = 0;
            this.sbApply.Text = "&Thực hiện";
            this.sbApply.Click += new System.EventHandler(this.sbApply_Click);
            // 
            // SaveQueryDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 249);
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "SaveQueryDialog";
            this.Text = "Công cụ tìm kiếm";
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl2)).EndInit();
            this.xtraTabControl2.ResumeLayout(false);
            this.xtraTabPage3.ResumeLayout(false);
            this.xtraTabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceToolTips.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceOperandTypeIcon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceGroupCommandsIcon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seSeparatorHeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seLevelIndent.Properties)).EndInit();
            this.xtraTabPage4.ResumeLayout(false);
            this.xtraTabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceOperator.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceGroupOperator.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceFieldName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceEmptyValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        public DevExpress.XtraEditors.FilterControl filterControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private DevExpress.XtraEditors.CheckEdit ceToolTips;
        private DevExpress.XtraEditors.CheckEdit ceOperandTypeIcon;
        private DevExpress.XtraEditors.CheckEdit ceGroupCommandsIcon;
        private DevExpress.XtraEditors.SpinEdit seSeparatorHeight;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.SpinEdit seLevelIndent;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage4;
        private DevExpress.XtraEditors.ColorEdit ceValue;
        private DevExpress.XtraEditors.ColorEdit ceOperator;
        private DevExpress.XtraEditors.ColorEdit ceGroupOperator;
        private DevExpress.XtraEditors.ColorEdit ceFieldName;
        private DevExpress.XtraEditors.ColorEdit ceEmptyValue;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton sbApply;
        private DevExpress.XtraEditors.SimpleButton btnLoadFilter1;
        private DevExpress.XtraEditors.SimpleButton btnSave1;
        private DevExpress.XtraEditors.SimpleButton rsFilter1;
        private PLCombobox cbbSqlFilter1;
        private DevExpress.XtraEditors.SimpleButton btnXoaCauTruyVan;
        private DevExpress.XtraEditors.LabelControl labelControl8;
    }
}