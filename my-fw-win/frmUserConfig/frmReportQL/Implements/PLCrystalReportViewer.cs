using System;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
using System.Collections.Generic;
using System.Data;

namespace ProtocolVN.Framework.Win
{
    //Hoạt động từ 10.5 trở xuống
    public class PLCrystalReportViewer : CrystalReportViewer
    {
        public XtraForm frmFax;
        public List<DataSet> _DSList;

        int pStatusBar;
        int pToolStrip;
        int pPageView;
        
        string s1 = "Trang hiện tại: ";
        string s2 = "Tổng số trang: ";
        string s3 = "Kích cỡ: ";
        int zoom = 100;        

        public PLCrystalReportViewer() : base()
        {
            InitializeComponent();
            this.DisplayGroupTree = false;

            for(int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i].GetType() == typeof(StatusBar))
                    pStatusBar = i;

                if (this.Controls[i].GetType() == typeof(ToolStrip))
                    pToolStrip = i;

                if (this.Controls[i].GetType() == typeof(PageView))
                    pPageView = i;
            }
                                
            this.ViewZoom += new CrystalDecisions.Windows.Forms.ZoomEventHandler(CRView_ViewZoom);            
            this.Paint += new PaintEventHandler(PLCrystalReportViewer_Paint);
            
            CustomizeStatusBar();
            CustomizeToolStrip();
        }


        private void CustomizeToolStrip()
        {
            ((ToolStrip)this.Controls[pToolStrip]).BackColor = Color.Transparent;

            
            foreach (ToolStripItem item in ((ToolStrip)this.Controls[pToolStrip]).Items)
            {
                if (item.ToolTipText == "Export Report")
                    item.ToolTipText = "Xuất Báo Cáo Ra Tập Tin";
                else if (item.ToolTipText == "Print Report")
                    item.ToolTipText = "In Báo Cáo";
                else if (item.ToolTipText == "Go to First Page")
                    item.ToolTipText = "Đến Trang Đầu Tiên";
                else if (item.ToolTipText == "Go to Previous Page")
                    item.ToolTipText = "Đến Trang Trước";
                else if (item.ToolTipText == "Go to Next Page")
                    item.ToolTipText = "Đến Trang Tiếp Theo";
                else if (item.ToolTipText == "Go to Last Page")
                    item.ToolTipText = "Đến Trang Cuối";
                else if (item.ToolTipText == "Find Text")
                    item.ToolTipText = "Tìm Kiếm";
                else if (item.ToolTipText == "Go to Page")
                    item.ToolTipText = "Chuyển trang";
                else if (item.ToolTipText == "Refresh")
                    item.ToolTipText = "Làm tươi";
                else if (item.ToolTipText == "Zoom")
                {
                    item.ToolTipText = "Kích Cỡ Xem";

                    foreach (ToolStripMenuItem mnitem in ((ToolStripDropDownButton)item).DropDownItems)
                    {
                        if (mnitem.Text == "Page Width")
                            mnitem.Text = "Bề Rộng Trang";
                        else if (mnitem.Text == "Whole Page")
                            mnitem.Text = "Cả Trang";
                        else if (mnitem.Text == "Customize...")
                            mnitem.Text = "Tùy Chọn...";
                    }
                }
            }

            if (FrameworkParams.isPrintCustom == true)
            {
                ToolStripItem printCustom = ((ToolStrip)this.Controls[pToolStrip]).Items.Add(((ToolStrip)this.Controls[pToolStrip]).Items[1].Image);
                printCustom.ToolTipText = "In báo cáo";
                printCustom.Click += new EventHandler(printCustom_Click);
                ((ToolStrip)this.Controls[pToolStrip]).Items[1].Visible = false;
                ((ToolStrip)this.Controls[pToolStrip]).Items.Insert(2, printCustom);
            }

            if (FrameworkParams.isFax == true)
            {
                ToolStripItem faxitem = ((ToolStrip)this.Controls[pToolStrip]).Items.Add(FWImageDic.FAX_16.ToBitmap());
                faxitem.ToolTipText = "Gửi fax...";
                faxitem.Click += new EventHandler(faxitem_Click);
                ((ToolStrip)this.Controls[pToolStrip]).Items.Insert(2, faxitem);
            }            
            if (FrameworkParams.isCustomReport != null)
            {
                ToolStripItem cusCol = ((ToolStrip)this.Controls[pToolStrip]).Items.Add(FWImageDic.CONFIG_REPORT_16.ToBitmap());
                cusCol.ToolTipText = "Tùy biến báo biểu...";
                cusCol.Click += new EventHandler(cusCol_Click);
                ((ToolStrip)this.Controls[pToolStrip]).Items.Insert(2, cusCol);
            }
            if (FrameworkParams.isDataReport != null)
            {
                ToolStripItem data = ((ToolStrip)this.Controls[pToolStrip]).Items.Add(FWImageDic.DATA_REPORT_16.ToBitmap());
                data.ToolTipText = "Dữ liệu báo biểu...";
                data.Click += new EventHandler(data_Click);
                ((ToolStrip)this.Controls[pToolStrip]).Items.Insert(2, data);
            }
        }

        void printCustom_Click(object sender, EventArgs e)
        {
            ((ReportDocument)this.ReportSource).PrintToPrinter(1, false, 0, 0);
        }

        void data_Click(object sender, EventArgs e)
        {
            //PHUOCNC Chưa xây dựng chức năng FAX
            if ((this.ReportSource != null) && (this.ReportSource is ReportDocument))
            {
                ReportDocument report = (ReportDocument)this.ReportSource;
                frmDataReport custom = new frmDataReport();
                custom._report = report;
                custom._DSList = _DSList;
                ProtocolForm.ShowModalDialog(FrameworkParams.MainForm, custom);
            }
        }

        void cusCol_Click(object sender, EventArgs e)
        {
            //PHUOCNC Chưa xây dựng chức năng FAX
            if ((this.ReportSource != null) && (this.ReportSource is ReportDocument))
            {
                ReportDocument report = (ReportDocument)this.ReportSource;
                frmCustomReport custom = new frmCustomReport();
                custom._report = report;
                ProtocolForm.ShowModalDialog(FrameworkParams.MainForm, custom);
            }
        }

        void faxitem_Click(object sender, EventArgs e)
        {
            //PHUOCNC Chưa xây dựng chức năng FAX
            if ((this.ReportSource != null) && (this.ReportSource is ReportDocument))
            {
                ReportDocument report = (ReportDocument)this.ReportSource;
                try{
                    System.IO.File.Delete(FrameworkParams.TEMP_FOLDER + @"\" + "$faxdoc.pdf");
                    report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, FrameworkParams.TEMP_FOLDER + @"\" + "$faxdoc.pdf");
                    if(this.frmFax == null) 
                        frmFax = new frmFax("", FrameworkParams.TEMP_FOLDER + @"\" + "$faxdoc.pdf", "", "");
                    this.frmFax.ShowDialog();
                }
                catch (Exception ex)
                {
                    //PLMessageBox.ShowErrorMessage("Chưa cài đặt máy FAX");
                    PLException.AddException(ex);
                }
            }
        }

        private void CustomizeStatusBar()
        {
            ((StatusBar)this.Controls[pStatusBar]).Panels[0].Text = s1;
            ((StatusBar)this.Controls[pStatusBar]).Panels[1].Text = s2;
            ((StatusBar)this.Controls[pStatusBar]).Panels[2].Text = s3;
        }

        void PLCrystalReportViewer_Paint(object sender, PaintEventArgs e)
        {
            ((StatusBar)this.Controls[pStatusBar]).Panels[0].Text = s1 + ((PageView)this.Controls[pPageView]).GetCurrentPageNumber();
            ((StatusBar)this.Controls[pStatusBar]).Panels[1].Text = s2 + ((PageView)this.Controls[pPageView]).GetLastPageNumber();
            ((StatusBar)this.Controls[pStatusBar]).Panels[2].Text = s3 + zoom.ToString() + "%";

            if (((PageView)this.Controls[pPageView]).Controls.Count > 0)
            {
                if (((PageView)this.Controls[pPageView]).Controls[0] is TabControl)
                {
                    if (((TabControl)((PageView)this.Controls[pPageView]).Controls[0]).TabCount > 0)
                    {
                        foreach (TabPage page in ((TabControl)((PageView)this.Controls[pPageView]).Controls[0]).TabPages)
                        {
                            page.Text = "Báo Cáo";
                        }
                    }
                }
            }
        }                

        void CRView_ViewZoom(object source, CrystalDecisions.Windows.Forms.ZoomEventArgs e)
        {
            zoom = e.NewZoomFactor;
        }

        public void _SetSupportRefresh(bool _SupportRefresh)
        {
            this.ShowRefreshButton = _SupportRefresh;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // PLCrystalReportViewer
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Name = "PLCrystalReportViewer";
            this.ShowCloseButton = false;
            this.ShowGotoPageButton = true;
            //this.ShowGotoPageButton = false;
            this.ShowPageNavigateButtons = true;
            this.ShowZoomButton = true;
            this.ShowRefreshButton = false;

            this.ShowGroupTreeButton = false;
            this.Size = new System.Drawing.Size(363, 337);
            this.ResumeLayout(false);
            this.PerformLayout();      
            
        }


    }
}
