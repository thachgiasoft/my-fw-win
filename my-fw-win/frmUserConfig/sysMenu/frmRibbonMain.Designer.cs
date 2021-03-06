namespace ProtocolVN.Framework.Win
{
    partial class frmRibbonMain
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
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.RibbonCtrl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.ribbonGallerySkins = new DevExpress.XtraBars.RibbonGalleryBarItem();
            this.SkinRibbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            this.SuspendLayout();
            this.xtraTabbedMdiManager1.MdiParent = null;
            // 
            // RibbonCtrl
            // 
            this.RibbonCtrl.ApplicationButtonKeyTip = "";
            this.RibbonCtrl.ApplicationIcon = null;
            this.RibbonCtrl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonGallerySkins});
            this.RibbonCtrl.Location = new System.Drawing.Point(0, 0);
            this.RibbonCtrl.MaxItemId = 1;
            this.RibbonCtrl.Name = "RibbonCtrl";
            //this.RibbonCtrl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            //this.SkinRibbonPage});
            this.RibbonCtrl.SelectedPage = this.SkinRibbonPage;
            this.RibbonCtrl.Size = new System.Drawing.Size(442, 143);
            // 
            // ribbonGallerySkins
            // 
            //this.ribbonGallerySkins.Caption = "Chọn Giao diện";
            //this.ribbonGallerySkins.Id = 0;
            //this.ribbonGallerySkins.Name = "ribbonGallerySkins";
            // 
            // ribbonPage1
            // 
            this.SkinRibbonPage.KeyTip = "";
            this.SkinRibbonPage.Name = "ribbonPage1";
            this.SkinRibbonPage.Text = "Chọn giao diện";

            // 
            // RibbonForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 364);
            this.Controls.Add(this.RibbonCtrl);
            this.IsMdiContainer = true;
            this.DoubleBuffered = true;
            this.Name = "frmMain";
            this.Ribbon = this.RibbonCtrl;
            
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            this.ResumeLayout(false);
        }

        

        #endregion

        //public DevExpress.XtraBars.Ribbon.RibbonControl RibbonCtrl;
        internal DevExpress.XtraBars.BarManager barManager1;
        internal DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        internal DevExpress.XtraBars.Ribbon.RibbonPage SkinRibbonPage;
        internal DevExpress.XtraBars.RibbonGalleryBarItem ribbonGallerySkins;
    }
}