using System.Collections.Generic;
using System.Windows.Forms;
using System;
using DevExpress.XtraEditors;
namespace ProtocolVN.Framework.Win
{
    partial class frmClipboardMan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClipboardMan));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.addButton = new System.Windows.Forms.ToolStripButton();
            this.removeButton = new System.Windows.Forms.ToolStripButton();
            this.LuuSep = new System.Windows.Forms.ToolStripSeparator();
            this.closeButton = new System.Windows.Forms.ToolStripButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lbl_doituongClipboard = new DevExpress.XtraEditors.LabelControl();
            this.lblCat = new System.Windows.Forms.Label();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.navBarControl3 = new DevExpress.XtraNavBar.NavBarControl();
            this.groupClipboard = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItem10 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarControl2 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarItem1 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem2 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem3 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem4 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem5 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem6 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem7 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem8 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem9 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgc_details = new DevExpress.XtraGrid.GridControl();
            this.dgv_details = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgc_details)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_details)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(23, 23);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addButton,
            this.removeButton,
            this.LuuSep,
            this.closeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(580, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // addButton
            // 
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(98, 22);
            this.addButton.Tag = "";
            this.addButton.Text = " &Xóa tất cả dữ liệu";
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(101, 22);
            this.removeButton.Tag = "";
            this.removeButton.Text = "Xóa các &dòng chọn";
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // LuuSep
            // 
            this.LuuSep.Name = "LuuSep";
            this.LuuSep.Size = new System.Drawing.Size(6, 25);
            this.LuuSep.Visible = false;
            // 
            // closeButton
            // 
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(43, 22);
            this.closeButton.Text = " Đón&g ";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.lbl_doituongClipboard);
            this.panelControl1.Controls.Add(this.lblCat);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(176, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(580, 37);
            this.panelControl1.TabIndex = 3;
            // 
            // lbl_doituongClipboard
            // 
            this.lbl_doituongClipboard.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_doituongClipboard.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lbl_doituongClipboard.Appearance.Options.UseFont = true;
            this.lbl_doituongClipboard.Appearance.Options.UseForeColor = true;
            this.lbl_doituongClipboard.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal;
            this.lbl_doituongClipboard.Location = new System.Drawing.Point(6, 8);
            this.lbl_doituongClipboard.Name = "lbl_doituongClipboard";
            this.lbl_doituongClipboard.Size = new System.Drawing.Size(168, 23);
            this.lbl_doituongClipboard.TabIndex = 1;
            this.lbl_doituongClipboard.Text = "Đối tượng Clipboard";
            // 
            // lblCat
            // 
            this.lblCat.AutoSize = true;
            this.lblCat.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCat.Location = new System.Drawing.Point(7, 5);
            this.lblCat.Name = "lblCat";
            this.lblCat.Size = new System.Drawing.Size(0, 25);
            this.lblCat.TabIndex = 0;
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "System.Windows.Forms.StatusBar"});
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel1.ID = new System.Guid("47482ff7-7f88-4274-9ba9-35bc499857c9");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Options.AllowDockBottom = false;
            this.dockPanel1.Options.AllowDockFill = false;
            this.dockPanel1.Options.AllowDockRight = false;
            this.dockPanel1.Options.AllowDockTop = false;
            this.dockPanel1.Options.AllowFloating = false;
            this.dockPanel1.Options.FloatOnDblClick = false;
            this.dockPanel1.Options.ShowCloseButton = false;
            this.dockPanel1.Size = new System.Drawing.Size(173, 485);
            this.dockPanel1.Text = "Clipboard";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.panelControl2);
            this.dockPanel1_Container.Controls.Add(this.navBarControl1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(167, 457);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // panelControl2
            // 
            this.panelControl2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.panelControl3);
            this.panelControl2.Controls.Add(this.navBarControl2);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(167, 285);
            this.panelControl2.TabIndex = 2;
            // 
            // panelControl3
            // 
            this.panelControl3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.navBarControl3);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(167, 387);
            this.panelControl3.TabIndex = 1;
            // 
            // navBarControl3
            // 
            this.navBarControl3.ActiveGroup = this.groupClipboard;
            this.navBarControl3.ContentButtonHint = null;
            this.navBarControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl3.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.groupClipboard});
            this.navBarControl3.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.navBarItem10});
            this.navBarControl3.Location = new System.Drawing.Point(0, 0);
            this.navBarControl3.Name = "navBarControl3";
            this.navBarControl3.OptionsNavPane.ExpandedWidth = 194;
            this.navBarControl3.Size = new System.Drawing.Size(167, 387);
            this.navBarControl3.TabIndex = 0;
            this.navBarControl3.Text = "navBarControl3";
            // 
            // groupClipboard
            // 
            this.groupClipboard.Caption = "Đối tượng";
            this.groupClipboard.Expanded = true;
            this.groupClipboard.LargeImage = ((System.Drawing.Image)(resources.GetObject("groupClipboard.LargeImage")));
            this.groupClipboard.Name = "groupClipboard";
            // 
            // navBarItem10
            // 
            this.navBarItem10.Caption = "navBarItem10";
            this.navBarItem10.LargeImage = ((System.Drawing.Image)(resources.GetObject("navBarItem10.LargeImage")));
            this.navBarItem10.Name = "navBarItem10";
            // 
            // navBarControl2
            // 
            this.navBarControl2.ActiveGroup = null;
            this.navBarControl2.ContentButtonHint = null;
            this.navBarControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl2.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.navBarItem1,
            this.navBarItem2,
            this.navBarItem3,
            this.navBarItem4,
            this.navBarItem5,
            this.navBarItem6,
            this.navBarItem7,
            this.navBarItem8,
            this.navBarItem9});
            this.navBarControl2.Location = new System.Drawing.Point(0, 0);
            this.navBarControl2.Name = "navBarControl2";
            this.navBarControl2.OptionsNavPane.ExpandedWidth = 194;
            this.navBarControl2.Size = new System.Drawing.Size(167, 285);
            this.navBarControl2.TabIndex = 0;
            this.navBarControl2.Text = "navBarControl2";
            // 
            // navBarItem1
            // 
            this.navBarItem1.Caption = "navBarItem1";
            this.navBarItem1.Name = "navBarItem1";
            // 
            // navBarItem2
            // 
            this.navBarItem2.Caption = "navBarItem2";
            this.navBarItem2.Name = "navBarItem2";
            // 
            // navBarItem3
            // 
            this.navBarItem3.Caption = "navBarItem3";
            this.navBarItem3.Name = "navBarItem3";
            // 
            // navBarItem4
            // 
            this.navBarItem4.Caption = "navBarItem4";
            this.navBarItem4.Name = "navBarItem4";
            // 
            // navBarItem5
            // 
            this.navBarItem5.Caption = "navBarItem5";
            this.navBarItem5.Name = "navBarItem5";
            // 
            // navBarItem6
            // 
            this.navBarItem6.Caption = "navBarItem6";
            this.navBarItem6.Name = "navBarItem6";
            // 
            // navBarItem7
            // 
            this.navBarItem7.Caption = "navBarItem7";
            this.navBarItem7.Name = "navBarItem7";
            // 
            // navBarItem8
            // 
            this.navBarItem8.Caption = "navBarItem8";
            this.navBarItem8.Name = "navBarItem8";
            // 
            // navBarItem9
            // 
            this.navBarItem9.Caption = "navBarItem9";
            this.navBarItem9.Name = "navBarItem9";
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = null;
            this.navBarControl1.Appearance.Background.Options.UseTextOptions = true;
            this.navBarControl1.Appearance.Background.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.navBarControl1.ContentButtonHint = null;
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 194;
            this.navBarControl1.Size = new System.Drawing.Size(167, 457);
            this.navBarControl1.TabIndex = 1;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgc_details);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(176, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(580, 442);
            this.panel1.TabIndex = 7;
            // 
            // dgc_details
            // 
            this.dgc_details.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgc_details.EmbeddedNavigator.Name = "";
            this.dgc_details.FormsUseDefaultLookAndFeel = false;
            this.dgc_details.Location = new System.Drawing.Point(0, 25);
            this.dgc_details.MainView = this.dgv_details;
            this.dgc_details.Name = "dgc_details";
            this.dgc_details.Size = new System.Drawing.Size(580, 417);
            this.dgc_details.TabIndex = 2;
            this.dgc_details.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgv_details});
            // 
            // dgv_details
            // 
            this.dgv_details.GridControl = this.dgc_details;
            this.dgv_details.Name = "dgv_details";
            this.dgv_details.OptionsBehavior.Editable = false;
            this.dgv_details.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgv_details.OptionsSelection.MultiSelect = true;
            this.dgv_details.OptionsView.ShowGroupPanel = false;
            this.dgv_details.RowCountChanged += new System.EventHandler(this.dgv_details_RowCountChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 25);
            this.label1.TabIndex = 0;
            // 
            // TrialfrmClipboardMan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 485);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.dockPanel1);
            this.Name = "TrialfrmClipboardMan";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowInTaskbar = false;
            this.Text = "Quản lý Clipboard";
            this.Load += new System.EventHandler(this.frmClipboardMan_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgc_details)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_details)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.ToolStripButton removeButton;
        private System.Windows.Forms.ToolStripButton closeButton;        
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Label lblCat;
        public System.Windows.Forms.ToolStripButton addButton;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private Panel panel1;
        private ToolStripSeparator LuuSep;
        private Label label1;
        private PanelControl panelControl2;
        private DevExpress.XtraNavBar.NavBarControl navBarControl2;
        private DevExpress.XtraNavBar.NavBarItem navBarItem1;
        private DevExpress.XtraNavBar.NavBarItem navBarItem2;
        private DevExpress.XtraNavBar.NavBarItem navBarItem3;
        private DevExpress.XtraNavBar.NavBarItem navBarItem4;
        private DevExpress.XtraNavBar.NavBarItem navBarItem5;
        private DevExpress.XtraNavBar.NavBarItem navBarItem6;
        private DevExpress.XtraNavBar.NavBarItem navBarItem7;
        private DevExpress.XtraNavBar.NavBarItem navBarItem8;
        private DevExpress.XtraNavBar.NavBarItem navBarItem9;
        private DevExpress.XtraGrid.GridControl dgc_details;
        private DevExpress.XtraGrid.Views.Grid.GridView dgv_details;
        private PanelControl panelControl3;
        private DevExpress.XtraNavBar.NavBarControl navBarControl3;
        private DevExpress.XtraNavBar.NavBarGroup groupClipboard;
        private LabelControl lbl_doituongClipboard;
        private DevExpress.XtraNavBar.NavBarItem navBarItem10;

        
    }
}

