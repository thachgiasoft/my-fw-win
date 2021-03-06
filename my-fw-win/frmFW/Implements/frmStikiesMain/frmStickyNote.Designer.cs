namespace ProtocolVN.Plugin.NoteBook
{
    partial class frmStickyNote
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
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.insertImageToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.colorsFontsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaulLooktToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.skyBlueToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.sunSetToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.nightToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.peachToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.elegantToolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.forestToolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.notepadToolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.customizeLookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.smallSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mediumSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.largeSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.picFlag = new System.Windows.Forms.PictureBox();
            this.flagContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.blueFlagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redFlagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.greenFlagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goalFlagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bulbToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.picSave = new System.Windows.Forms.PictureBox();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.pnlResize = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnlTitle.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFlag)).BeginInit();
            this.flagContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTitle
            // 
            this.pnlTitle.BackColor = System.Drawing.Color.Transparent;
            this.pnlTitle.ContextMenuStrip = this.contextMenuStrip1;
            this.pnlTitle.Controls.Add(this.txtTitle);
            this.pnlTitle.Controls.Add(this.picFlag);
            this.pnlTitle.Controls.Add(this.picSave);
            this.pnlTitle.Controls.Add(this.picClose);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Margin = new System.Windows.Forms.Padding(1);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(180, 18);
            this.pnlTitle.TabIndex = 0;
            this.pnlTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlTitle_MouseMove);
            this.pnlTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTitle_MouseDown);
            this.pnlTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlTitle_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertImageToolStripMenuItem2,
            this.colorsFontsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.sendToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(142, 114);
            // 
            // insertImageToolStripMenuItem2
            // 
            this.insertImageToolStripMenuItem2.Image = global::ProtocolVN.Framework.Win.Properties.Resources.InsertImage;
            this.insertImageToolStripMenuItem2.Name = "insertImageToolStripMenuItem2";
            this.insertImageToolStripMenuItem2.Size = new System.Drawing.Size(141, 22);
            this.insertImageToolStripMenuItem2.Text = "Chèn hình";
            this.insertImageToolStripMenuItem2.Click += new System.EventHandler(this.insertImageToolStripMenuItem2_Click);
            this.insertImageToolStripMenuItem2.Visible = false;
            // 
            // colorsFontsToolStripMenuItem
            // 
            this.colorsFontsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaulLooktToolStripMenuItem1,
            this.toolStripSeparator3,
            this.skyBlueToolStripMenuItem2,
            this.sunSetToolStripMenuItem3,
            this.nightToolStripMenuItem4,
            this.peachToolStripMenuItem5,
            this.elegantToolStripMenuItem6,
            this.forestToolStripMenuItem7,
            this.notepadToolStripMenuItem8,
            this.toolStripSeparator2,
            this.customizeLookToolStripMenuItem});
            this.colorsFontsToolStripMenuItem.Image = global::ProtocolVN.Framework.Win.Properties.Resources.LooknFeel;
            this.colorsFontsToolStripMenuItem.Name = "colorsFontsToolStripMenuItem";
            this.colorsFontsToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.colorsFontsToolStripMenuItem.Text = "Mẫu hiển thị";
            // 
            // defaulLooktToolStripMenuItem1
            // 
            this.defaulLooktToolStripMenuItem1.Name = "defaulLooktToolStripMenuItem1";
            this.defaulLooktToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.defaulLooktToolStripMenuItem1.Text = "Default";
            this.defaulLooktToolStripMenuItem1.Click += new System.EventHandler(this.defaulLooktToolStripMenuItem1_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // skyBlueToolStripMenuItem2
            // 
            this.skyBlueToolStripMenuItem2.Name = "skyBlueToolStripMenuItem2";
            this.skyBlueToolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.skyBlueToolStripMenuItem2.Text = "Sky Blue";
            this.skyBlueToolStripMenuItem2.Click += new System.EventHandler(this.skyBlueToolStripMenuItem2_Click);
            // 
            // sunSetToolStripMenuItem3
            // 
            this.sunSetToolStripMenuItem3.Name = "sunSetToolStripMenuItem3";
            this.sunSetToolStripMenuItem3.Size = new System.Drawing.Size(152, 22);
            this.sunSetToolStripMenuItem3.Text = "Sunset";
            this.sunSetToolStripMenuItem3.Click += new System.EventHandler(this.sunSetToolStripMenuItem3_Click);
            // 
            // nightToolStripMenuItem4
            // 
            this.nightToolStripMenuItem4.Name = "nightToolStripMenuItem4";
            this.nightToolStripMenuItem4.Size = new System.Drawing.Size(152, 22);
            this.nightToolStripMenuItem4.Text = "Night";
            this.nightToolStripMenuItem4.Click += new System.EventHandler(this.nightToolStripMenuItem4_Click);
            // 
            // peachToolStripMenuItem5
            // 
            this.peachToolStripMenuItem5.Name = "peachToolStripMenuItem5";
            this.peachToolStripMenuItem5.Size = new System.Drawing.Size(152, 22);
            this.peachToolStripMenuItem5.Text = "Peach";
            this.peachToolStripMenuItem5.Click += new System.EventHandler(this.peachToolStripMenuItem5_Click);
            // 
            // elegantToolStripMenuItem6
            // 
            this.elegantToolStripMenuItem6.Name = "elegantToolStripMenuItem6";
            this.elegantToolStripMenuItem6.Size = new System.Drawing.Size(152, 22);
            this.elegantToolStripMenuItem6.Text = "Elegant";
            this.elegantToolStripMenuItem6.Click += new System.EventHandler(this.elegantToolStripMenuItem6_Click);
            // 
            // forestToolStripMenuItem7
            // 
            this.forestToolStripMenuItem7.Name = "forestToolStripMenuItem7";
            this.forestToolStripMenuItem7.Size = new System.Drawing.Size(152, 22);
            this.forestToolStripMenuItem7.Text = "Forest";
            this.forestToolStripMenuItem7.Click += new System.EventHandler(this.forestToolStripMenuItem7_Click);
            // 
            // notepadToolStripMenuItem8
            // 
            this.notepadToolStripMenuItem8.Name = "notepadToolStripMenuItem8";
            this.notepadToolStripMenuItem8.Size = new System.Drawing.Size(152, 22);
            this.notepadToolStripMenuItem8.Text = "Notepad";
            this.notepadToolStripMenuItem8.Click += new System.EventHandler(this.notepadToolStripMenuItem8_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // customizeLookToolStripMenuItem
            // 
            this.customizeLookToolStripMenuItem.Name = "customizeLookToolStripMenuItem";
            this.customizeLookToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.customizeLookToolStripMenuItem.Text = "Customize";
            this.customizeLookToolStripMenuItem.Click += new System.EventHandler(this.customizeLookToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultSizeToolStripMenuItem,
            this.toolStripSeparator1,
            this.smallSizeToolStripMenuItem,
            this.mediumSizeToolStripMenuItem,
            this.largeSizeToolStripMenuItem});
            this.toolStripMenuItem1.Image = global::ProtocolVN.Framework.Win.Properties.Resources.Size;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(141, 22);
            this.toolStripMenuItem1.Text = "Kích thước";
            // 
            // defaultSizeToolStripMenuItem
            // 
            this.defaultSizeToolStripMenuItem.Name = "defaultSizeToolStripMenuItem";
            this.defaultSizeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.defaultSizeToolStripMenuItem.Text = "Mặc định";
            this.defaultSizeToolStripMenuItem.Click += new System.EventHandler(this.defaultSizeToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // smallSizeToolStripMenuItem
            // 
            this.smallSizeToolStripMenuItem.Name = "smallSizeToolStripMenuItem";
            this.smallSizeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.smallSizeToolStripMenuItem.Text = "Nhỏ";
            this.smallSizeToolStripMenuItem.Click += new System.EventHandler(this.smallSizeToolStripMenuItem_Click);
            // 
            // mediumSizeToolStripMenuItem
            // 
            this.mediumSizeToolStripMenuItem.Name = "mediumSizeToolStripMenuItem";
            this.mediumSizeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mediumSizeToolStripMenuItem.Text = "Trung bình";
            this.mediumSizeToolStripMenuItem.Click += new System.EventHandler(this.mediumSizeToolStripMenuItem_Click);
            // 
            // largeSizeToolStripMenuItem
            // 
            this.largeSizeToolStripMenuItem.Name = "largeSizeToolStripMenuItem";
            this.largeSizeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.largeSizeToolStripMenuItem.Text = "Lớn";
            this.largeSizeToolStripMenuItem.Click += new System.EventHandler(this.largeSizeToolStripMenuItem_Click);
            // 
            // sendToolStripMenuItem
            // 
            this.sendToolStripMenuItem.Image = global::ProtocolVN.Framework.Win.Properties.Resources.Send;
            this.sendToolStripMenuItem.Name = "sendToolStripMenuItem";
            this.sendToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.sendToolStripMenuItem.Text = "Gửi thư";
            this.sendToolStripMenuItem.Visible = false;
            this.sendToolStripMenuItem.Click += new System.EventHandler(this.sendToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::ProtocolVN.Framework.Win.Properties.Resources.Delete1;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.deleteToolStripMenuItem.Text = "Xóa";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // txtTitle
            // 
            this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitle.BackColor = System.Drawing.Color.Khaki;
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitle.Location = new System.Drawing.Point(24, 3);
            this.txtTitle.Margin = new System.Windows.Forms.Padding(0);
            this.txtTitle.MaxLength = 25;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(105, 14);
            this.txtTitle.TabIndex = 0;
            this.txtTitle.TabStop = false;
            this.txtTitle.WordWrap = false;
            this.txtTitle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTitle_KeyDown);
            this.txtTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtTitle_MouseDown);
            // 
            // picFlag
            // 
            this.picFlag.ContextMenuStrip = this.flagContextMenuStrip;
            this.picFlag.Dock = System.Windows.Forms.DockStyle.Left;
            this.picFlag.Image = global::ProtocolVN.Framework.Win.Properties.Resources.BlueFlag;
            this.picFlag.Location = new System.Drawing.Point(0, 0);
            this.picFlag.Margin = new System.Windows.Forms.Padding(0);
            this.picFlag.Name = "picFlag";
            this.picFlag.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.picFlag.Size = new System.Drawing.Size(21, 18);
            this.picFlag.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picFlag.TabIndex = 2;
            this.picFlag.TabStop = false;
            this.picFlag.Tag = "BlueFlag";
            this.picFlag.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlTitle_MouseMove);
            this.picFlag.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTitle_MouseDown);
            this.picFlag.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlTitle_MouseUp);
            // 
            // flagContextMenuStrip
            // 
            this.flagContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.blueFlagToolStripMenuItem,
            this.redFlagToolStripMenuItem,
            this.greenFlagToolStripMenuItem,
            this.goalFlagToolStripMenuItem,
            this.bulbToolStripMenuItem});
            this.flagContextMenuStrip.Name = "flagContextMenuStrip";
            this.flagContextMenuStrip.Size = new System.Drawing.Size(131, 114);
            // 
            // blueFlagToolStripMenuItem
            // 
            this.blueFlagToolStripMenuItem.Image = global::ProtocolVN.Framework.Win.Properties.Resources.BlueFlag;
            this.blueFlagToolStripMenuItem.Name = "blueFlagToolStripMenuItem";
            this.blueFlagToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.blueFlagToolStripMenuItem.Text = "Cờ blue";
            this.blueFlagToolStripMenuItem.Click += new System.EventHandler(this.blueFlagToolStripMenuItem_Click);
            // 
            // redFlagToolStripMenuItem
            // 
            this.redFlagToolStripMenuItem.Image = global::ProtocolVN.Framework.Win.Properties.Resources.RedFlag;
            this.redFlagToolStripMenuItem.Name = "redFlagToolStripMenuItem";
            this.redFlagToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.redFlagToolStripMenuItem.Text = "Cờ red";
            this.redFlagToolStripMenuItem.Click += new System.EventHandler(this.redFlagToolStripMenuItem_Click);
            // 
            // greenFlagToolStripMenuItem
            // 
            this.greenFlagToolStripMenuItem.Image = global::ProtocolVN.Framework.Win.Properties.Resources.GreenFlag;
            this.greenFlagToolStripMenuItem.Name = "greenFlagToolStripMenuItem";
            this.greenFlagToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.greenFlagToolStripMenuItem.Text = "Cờ green";
            this.greenFlagToolStripMenuItem.Click += new System.EventHandler(this.greenFlagToolStripMenuItem_Click);
            // 
            // goalFlagToolStripMenuItem
            // 
            this.goalFlagToolStripMenuItem.Image = global::ProtocolVN.Framework.Win.Properties.Resources.GoalFlag;
            this.goalFlagToolStripMenuItem.Name = "goalFlagToolStripMenuItem";
            this.goalFlagToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.goalFlagToolStripMenuItem.Text = "Cờ goal";
            this.goalFlagToolStripMenuItem.Click += new System.EventHandler(this.goalFlagToolStripMenuItem_Click);
            // 
            // bulbToolStripMenuItem
            // 
            this.bulbToolStripMenuItem.Image = global::ProtocolVN.Framework.Win.Properties.Resources.Bulb;
            this.bulbToolStripMenuItem.Name = "bulbToolStripMenuItem";
            this.bulbToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.bulbToolStripMenuItem.Text = "Bóng đèn";
            this.bulbToolStripMenuItem.Click += new System.EventHandler(this.bulbToolStripMenuItem_Click);
            // 
            // picSave
            // 
            this.picSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.picSave.Image = global::ProtocolVN.Framework.Win.Properties.Resources.Save1;
            this.picSave.Location = new System.Drawing.Point(134, 0);
            this.picSave.Margin = new System.Windows.Forms.Padding(0);
            this.picSave.Name = "picSave";
            this.picSave.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.picSave.Size = new System.Drawing.Size(28, 18);
            this.picSave.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picSave.TabIndex = 1;
            this.picSave.TabStop = false;
            this.picSave.Visible = false;
            this.picSave.Click += new System.EventHandler(this.picSave_Click);
            // 
            // picClose
            // 
            this.picClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.picClose.Image = global::ProtocolVN.Framework.Win.Properties.Resources.Close;
            this.picClose.Location = new System.Drawing.Point(162, 0);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(18, 18);
            this.picClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picClose.TabIndex = 0;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.AcceptsTab = true;
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.BackColor = System.Drawing.Color.Yellow;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(0, 18);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(5);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox1.Size = new System.Drawing.Size(180, 138);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            this.richTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox1_KeyDown);
            this.richTextBox1.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBox1_LinkClicked);
            this.richTextBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.richTextBox1_MouseClick);
            // 
            // pnlResize
            // 
            this.pnlResize.ContextMenuStrip = this.contextMenuStrip1;
            this.pnlResize.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.pnlResize.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlResize.Location = new System.Drawing.Point(0, 156);
            this.pnlResize.Name = "pnlResize";
            this.pnlResize.Size = new System.Drawing.Size(180, 4);
            this.pnlResize.TabIndex = 5;
            this.pnlResize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlResize_MouseMove);
            this.pnlResize.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlResize_MouseDown);
            this.pnlResize.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlResize_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmStickyNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(180, 160);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.ControlBox = false;
            this.Controls.Add(this.pnlResize);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.richTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmStickyNote";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.frmStickyNote_Load);
            this.LocationChanged += new System.EventHandler(this.frmStickyNote_LocationChanged);
            this.BackColorChanged += new System.EventHandler(this.frmStickyNote_BackColorChanged);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picFlag)).EndInit();
            this.flagContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem colorsFontsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendToolStripMenuItem;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel pnlResize;
        private System.Windows.Forms.PictureBox picClose;
        private System.Windows.Forms.ToolStripMenuItem customizeLookToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaulLooktToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem smallSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mediumSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem largeSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultSizeToolStripMenuItem;
        private System.Windows.Forms.PictureBox picSave;
        private System.Windows.Forms.ToolStripMenuItem insertImageToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.PictureBox picFlag;
        private System.Windows.Forms.ContextMenuStrip flagContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem blueFlagToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redFlagToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem greenFlagToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goalFlagToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bulbToolStripMenuItem;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem skyBlueToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem sunSetToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem nightToolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem peachToolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem elegantToolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem forestToolStripMenuItem7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem notepadToolStripMenuItem8;
        private System.Windows.Forms.Timer timer1;
    }
}