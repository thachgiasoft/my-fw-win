using System.Collections.Generic;
using System.Windows.Forms;
using System;
using DevExpress.XtraEditors;
namespace ProtocolVN.Framework.Win
{
    partial class frmSimpleCategory
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.addButton = new System.Windows.Forms.ToolStripButton();
            this.removeButton = new System.Windows.Forms.ToolStripButton();
            this.editButton = new System.Windows.Forms.ToolStripButton();
            this.LuuSep = new System.Windows.Forms.ToolStripSeparator();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnNoSave = new System.Windows.Forms.ToolStripButton();
            this.DongSep = new System.Windows.Forms.ToolStripSeparator();
            this.closeButton = new System.Windows.Forms.ToolStripButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblCat = new System.Windows.Forms.Label();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
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
            this.editButton,
            this.LuuSep,
            this.btnSave,
            this.btnNoSave,
            this.DongSep,
            this.closeButton});
            this.toolStrip1.Location = new System.Drawing.Point(203, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(436, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // addButton
            // 
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(43, 22);
            this.addButton.Tag = "";
            this.addButton.Text = " &Thêm ";
            this.addButton.Click += new System.EventHandler(this.addButton_Click1);
            // 
            // removeButton
            // 
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(35, 22);
            this.removeButton.Tag = "";
            this.removeButton.Text = " &Xóa ";
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click1);
            // 
            // editButton
            // 
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(36, 22);
            this.editButton.Tag = "";
            this.editButton.Text = " &Sửa ";
            this.editButton.Click += new System.EventHandler(this.editButton_Click1);
            // 
            // LuuSep
            // 
            this.LuuSep.Name = "LuuSep";
            this.LuuSep.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(29, 22);
            this.btnSave.Text = "&Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click1);
            // 
            // btnNoSave
            // 
            this.btnNoSave.Enabled = false;
            this.btnNoSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNoSave.Name = "btnNoSave";
            this.btnNoSave.Size = new System.Drawing.Size(59, 22);
            this.btnNoSave.Text = "&Không lưu";
            this.btnNoSave.Click += new System.EventHandler(this.btnNoSave_Click1);
            // 
            // DongSep
            // 
            this.DongSep.Name = "DongSep";
            this.DongSep.Size = new System.Drawing.Size(6, 25);
            // 
            // closeButton
            // 
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(43, 22);
            this.closeButton.Text = " Đón&g ";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click1);
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.lblCat);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(3, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(636, 37);
            this.panelControl1.TabIndex = 3;
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
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "System.Windows.Forms.StatusBar"});
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(636, 326);
            this.panel1.TabIndex = 7;
            // 
            // frmSimpleCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 369);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelControl1);
            this.Name = "frmSimpleCategory";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowInTaskbar = false;
            this.Text = "Quản lý danh mục";
            this.Load += new System.EventHandler(this.frmSimpleCategory_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.ToolStripButton removeButton;
        private System.Windows.Forms.ToolStripSeparator DongSep;
        private System.Windows.Forms.ToolStripButton closeButton;        
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Label lblCat;
        public System.Windows.Forms.ToolStripButton addButton;
        public System.Windows.Forms.ToolStripButton editButton;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private Panel panel1;
        private XtraUserControl control;
        private ToolStripSeparator LuuSep;
        public ToolStripButton btnSave;
        public ToolStripButton btnNoSave;

        
    }
}

