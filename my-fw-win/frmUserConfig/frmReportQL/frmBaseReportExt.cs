using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using ProtocolVN.Framework.Core;
using ProtocolVN.Framework.Win;
using CrystalDecisions.Shared;
using DevExpress.XtraTab;
using DevExpress.XtraReports.UI;
using ProtocolVN.Framework.Win.Report.TemplateReport;

namespace ProtocolVN.Framework.Win
{
    public partial class frmBaseReportExt : XtraForm, IParamForm
    {
        private string ReportXMLStr = "";
        private DXErrorProvider Error;
        public frmBaseReportExt()
        {
            InitializeComponent();

            ReportXMLStr = FrameworkParams.Report;
            //this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            //this.pictureEdit1.EditValue = global::warehouse.Properties.Resources.pictureEdit1_EditValue;
            //Hàm dùng để khởi động hệ thống report
            //new PLCrystalReportViewer();

            this.treeList1.OptionsView.AutoWidth = true;
            this.treeList1.OptionsView.ShowIndicator = false;
            this.treeList1.OptionsView.ShowHorzLines = false;
            this.treeList1.OptionsView.ShowVertLines = false;
            this.treeList1.OptionsView.ShowColumns = false;
            //Màu chọn lựa
            this.treeList1.Appearance.FocusedCell.BackColor = System.Drawing.Color.LightSkyBlue;
        }

        public frmBaseReportExt(string ReportXMLStr)
            : this()
        {
            this.ReportXMLStr = ReportXMLStr;                        
        }

        private bool FocusParam(string param, TreeList tree)
        {
            foreach (TreeListNode node in tree.Nodes)
            {
                if (node.Tag != null)
                {
                    //lap qua node cha
                    ReportInfo info = (ReportInfo)node.Tag;
                    if (info.ID == param)
                    {
                        this.treeList1.FocusedNode = node;
                        return true;
                    }

                    //lap qua node con
                    foreach (TreeListNode nodeChild in node.Nodes)
                    {
                        if (node.Tag != null)
                        {
                            ReportInfo infoChile = (ReportInfo)nodeChild.Tag;
                            if (infoChile.ID == param)
                            {
                                this.treeList1.FocusedNode = nodeChild;
                                return true;
                            }
                        }
                    }
                }
            }
            new frmPermissionFail().ShowDialog();
            //HelpMsgBox.ShowNotificationMessage("Không tồn tại ID = " + param + " trong hệ thống report. Do cấu hình sai.");            
            return false;
            
        }
        private void frmBaseReport_Load(object sender, EventArgs e)
        {
            this.splitContainerControl1.Panel2.Controls.Clear();
            this.splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel1;
            this.panelControl1.Visible = false;

            BuildTreeList();

            this.treeList1.Selection.Clear();
            this.xtraTabControl1.HeaderButtonsShowMode = DevExpress.XtraTab.TabButtonShowMode.Never;


            // focus node
            if (this.Tag != null && TagPropertyMan.Get(this.Tag, "FORM_PARAM") != null)
            {
                if (TagPropertyMan.Get(this.Tag, "FORM_PARAM").ToString() != "")
                {
                    if (FocusParam(TagPropertyMan.Get(this.Tag, "FORM_PARAM").ToString(), this.treeList1) == false)
                    {
                        ////PHUOCNC : Hủy không mở form
                        //this.Dispose();
                        //return;
                    }
                }
            }
            this.Error = GUIValidation.GetErrorProvider(this);
        }
      
        private void BuildTreeList()
        {
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            //System.IO.StringReader sr = new System.IO.StringReader(FrameworkParams.Report);
            System.IO.StringReader sr = new System.IO.StringReader(this.ReportXMLStr);
            doc.Load(sr);

            this.treeList1.Nodes.Clear();

            foreach (System.Xml.XmlNode gnode in doc.DocumentElement.ChildNodes)
            {
                DevExpress.XtraTreeList.Nodes.TreeListNode tnode = this.treeList1.AppendNode(null, -1);
                
                tnode.SetValue(0, gnode.Attributes["vn"].Value);
                ////Tiếng Anh
                //tnode.SetValue(0, gnode.Attributes["en"].Value);

                //them info vao node cha
                //HUNG
                ReportInfo ginfo = new ReportInfo();
                ginfo.ID = gnode.Attributes["id"].Value;
                tnode.Tag = ginfo;
                //----

                //TODO : PHUOC
                foreach (System.Xml.XmlNode inode in gnode.ChildNodes)
                {
                    BaoBieu report = null;
                    try
                    {
                        //if (!DAReport.Instance.IsPublicReport(inode.Attributes["filtercontrol"].Value.Trim(), 
                        //    FrameworkParams.currentUser.username))
                        //{
                        //    report = BaoBieu.loadReport2(inode.Attributes["filtercontrol"].Value.Trim(), FrameworkParams.currentUser.username);
                        //}

                        //Kiem tra phan quyen report
                        if (User.isAdmin() == false&& FrameworkParams.isPermision != null)
                        {
                            //Kiem tra public report
                            if (inode.Attributes["en"].Value.Trim().Equals("PUBLIC") == false)
                            {
                                report = BaoBieu.loadReport2(inode.Attributes["filtercontrol"].Value.Trim(), FrameworkParams.currentUser.username);
                                //Kiểm tra quyền chỉ đọc trên báo cáo
                                if (report != null)
                                {
                                    if (report.id == -1 || report.isRead == false)
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }
                    catch
                    {
                        frmDangXayDung.Show(this);
                        if (FrameworkParams.wait != null) { FrameworkParams.wait.Finish(); }
                    }

                    ReportInfo info = new ReportInfo();
                    info.ID = inode.Attributes["id"].Value;
                    info.Caption = inode.Attributes["caption"].Value;
                    info.FilterControl = inode.Attributes["filtercontrol"].Value;
                    info.ReportFile = inode.Attributes["reportfile"].Value;
                    
                    DevExpress.XtraTreeList.Nodes.TreeListNode ttnode = this.treeList1.AppendNode(null, tnode);
                    
                    ttnode.SetValue(0, inode.Attributes["vn"].Value);
                    ////Tiếng Anh
                    //ttnode.SetValue(0, inode.Attributes["en"].Value);
                    
                    ttnode.Tag = info;
                }
                if (tnode.Nodes.Count == 0)
                {
                   // treeList1.DeleteNode(tnode);
                    tnode.Visible = false;

                }
            }

            this.treeList1.ExpandAll();
        }

        private void treeList1_FocusedNodeChanged_1(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (this.treeList1.Selection.Count > 0 && NoRunFocusChange == false)
            {
                if (this.treeList1.Selection[0] == null) return;
                if (this.treeList1.Selection[0].ParentNode != null)
                {
                    if (this.treeList1.Selection[0].Tag != null)
                    {
                        this.splitContainerControl1.PanelVisibility = SplitPanelVisibility.Both;
                        ReportInfo info = (ReportInfo)this.treeList1.Selection[0].Tag;
                        //PHUOCNC
                        XtraUserControl ctrl = LoadFilterControl(info);
                        ctrl.Tag = this.treeList1.FocusedNode;

                        this.xtraTabControl1.SelectedTabPage = this.xtraTabPageIntro;
                    }
                    else
                    {
                        this.splitContainerControl1.Panel2.Controls.Clear();
                        this.splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel1;
                        this.panelControl1.Visible = false;
                        HelpMsgBox.ShowNotificationMessage("Bạn không có quyền đối với báo cáo này!");
                    }
                }
                else
                {
                    this.splitContainerControl1.Panel2.Controls.Clear();
                    this.splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel1;
                    this.panelControl1.Visible = false;
                }
                AddPageFlag.Value = true;
            }
        }

        private XtraUserControl LoadFilterControl(ReportInfo info)
        {
            try
            {
                XtraUserControl ctl = (XtraUserControl)GenerateClass.initObject(info.FilterControl);

                //HUNG, an di neu nhu chua co report
                if (ctl == null)
                {
                    this.splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel1;
                    this.splitContainerControl1.Panel2.Controls.Clear();
                    this.panelControl1.Visible = false;
                    return null;
                }
                UpdateSearchControl(ctl);

                return ctl;

                //this.splitContainerControl1.Panel2.Controls.Clear();
                //this.splitContainerControl1.Panel2.Controls.Add(ctl);
                //this.dockPanel1.Width = ctl.Width + 12;
                //this.splitContainerControl1.SplitterPosition = splitContainerControl1.Height - ctl.Height - 6;

                //this.panelControl1.Visible = true;
                //ProtocolVN.Framework.Win.ProtocolForm.pl_ctrl_wrapper(this, ctl, true);
                //this.xtraTabControl1.SelectedTabPage = this.xtraTabPageIntro;
                //ctl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                //        | System.Windows.Forms.AnchorStyles.Right)));
            }
            catch (Exception ex)
            {
                frmDangXayDung.Show(this);
                PLException.AddException(ex);
                return null;
            }
        }

        //PHUOCNC Hiện tại thực hiện 2 lần khi chuyển TabPage
        private void UpdateSearchControl(XtraUserControl ctl)
        {
            this.splitContainerControl1.Panel2.Controls.Clear();
            this.splitContainerControl1.Panel2.Controls.Add(ctl);
            this.dockPanel1.Width = ctl.Width + 12;
            this.splitContainerControl1.SplitterPosition = splitContainerControl1.Height - ctl.Height - 6;
            this.panelControl1.Visible = true;

            ProtocolVN.Framework.Win.ProtocolForm.pl_ctrl_wrapper(this, ctl, true);
            
            ctl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));            
        }
        private BandCollection getBands(XtraReport xtraReport)
        {
            BandCollection collection = new BandCollection(new XtraReport());
            foreach (Band band in xtraReport.Bands)
            {
                if ((band.Name != "PageHeader") && (band.Name != "PageFooter"))
                {
                    collection.Add(band);
                }
            }
            return collection;
        }
        LOCKBOOL AddPageFlag = new LOCKBOOL(true);
        public bool showEmptyReport = false;
        private void preview()
        {
            string reportFile = "";
            try
            {
                if (this.splitContainerControl1.Panel2.Controls.Count > 0 && this.treeList1.Selection[0].Tag != null)
                {
                    IReportFilter filter = (IReportFilter)this.splitContainerControl1.Panel2.Controls[0];

                    //Kiểm tra lỗi
                    Error.ClearErrors();
                    filter.ValidateFilter(Error);
                    if (Error.HasErrors) return;

                    #region Khởi tạo Main DataSet
                    DataSet ds = null;
                    ds = filter.getDataSet();
                    if (showEmptyReport==false)
                    {
                        if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                        {
                            HelpMsgBox.ShowNotificationMessage("Không tìm thấy dữ liệu báo cáo.");
                            return;
                        }
                    }
                    ReportInfo info = (ReportInfo)this.treeList1.Selection[0].Tag;
                    ParameterFields Params = null;
                    ReportClass Report = null;
                    if (this.splitContainerControl1.Panel2.Controls[0] is IDynSheetReportFilter)
                    {
                        string Title = null;
                        string SubTitle = null;
                        string[] FieldNames = null;
                        string[] Captions = null;
                        int[] Widths = null;
                        bool IsVertical = false;
                        //Alignment[] aligns = null;

                        ((IDynSheetReportFilter)this.splitContainerControl1.Panel2.Controls[0]).GetParam(
                            out Title, out SubTitle, out FieldNames,
                            out Captions, out Widths, out IsVertical);

                        if (IsVertical)
                        {
                            Report = new VSheetReport();
                        }
                        else
                        {
                            Report = new HSheetReport();
                        }

                        DynamicSheetReport.ToSheetReport(Report, out Params, ds,
                            FieldNames,
                            Captions,
                            Widths, Title, SubTitle);
                        
                    }
                    #endregion
                    FrameworkParams.wait = new WaitingMsg();

                    #region Thêm 1 Viewer
                    DevExpress.XtraTab.XtraTabPage page = null;
                    bool usingXtraReport = info.ReportFile.StartsWith("EMBXTRA");
                    PLCrystalReportViewer view = null;
                    PLXtraPreview xtraview = null;
                    Control viewCtrl = null;
                    if (usingXtraReport)
                    {
                        xtraview = new PLXtraPreview();
                        viewCtrl = xtraview;
                    }
                    else
                    {
                        view = new PLCrystalReportViewer();
                        view._SetSupportRefresh(false);
                        viewCtrl = view;
                    }
                    lock(AddPageFlag){
                        if (AddPageFlag.Value == true)
                        {
                            page = new DevExpress.XtraTab.XtraTabPage();
                            page.Text = this.treeList1.Selection[0].GetValue(0).ToString();                            
                            page.Controls.Add(viewCtrl);
                            AddPageFlag.Value = false;

                            this.xtraTabControl1.TabPages.Add(page);
                            page.Tag = this.splitContainerControl1.Panel2.Controls[0];
                            this.xtraTabControl1.SelectedTabPage = page; 
                        }
                        else
                        {
                            page = this.xtraTabControl1.SelectedTabPage;
                            page.Controls.Clear();
                            page.Controls.Add(viewCtrl);
                        }
                    }
                    viewCtrl.Dock = DockStyle.Fill;
                    //DevExpress.XtraTab.XtraTabPage page = new DevExpress.XtraTab.XtraTabPage();
                    //page.Text = this.treeList1.Selection[0].GetValue(0).ToString();
                    //PLCrystalReportViewer view = new PLCrystalReportViewer();
                    //page.Controls.Add(view);
                    
                    //view.Dock = DockStyle.Fill;
                    #endregion
                    XtraReport xtraReport = null;
                    if (this.splitContainerControl1.Panel2.Controls[0] is IDynSheetReportFilter)
                    {
                        #region Phiên bản 14
                        //view._I.ParameterFieldInfo = Params;
                        //view._SetSupportRefresh(false);
                        ////EnableParameterPrompting
                        //view._I.ReportSource = Report;
                        #endregion
                        #region Phiên bản 10
                        view.ParameterFieldInfo = Params;
                        view._SetSupportRefresh(false);
                        //EnableParameterPrompting
                        view.ReportSource = Report;
                        #endregion
                    }
                    else {
                        #region Lấy 1 ReportDocument
                        
                        if (this.splitContainerControl1.Panel2.Controls[0] is IAdvanceReportFilter)
                            info.ReportFile = ((IAdvanceReportFilter)this.splitContainerControl1.Panel2.Controls[0]).getReportFile();
                        reportFile = info.ReportFile;
                        ReportDocument report = new ReportDocument();

                        if (reportFile.StartsWith("EMBXTRA"))
                        {
                            reportFile = reportFile.Substring(7);
                            try { xtraReport = (DevExpress.XtraReports.UI.XtraReport)GenerateClass.initObject(reportFile); }
                            catch { }
                        }
                        else if (reportFile.StartsWith("EMB"))
                        {
                            reportFile = reportFile.Substring(3);
                            try { report = (ReportDocument)GenerateClass.initObject(reportFile); }
                            catch { }
                        }
                        else if (reportFile.StartsWith("PATH"))
                        {
                            reportFile = reportFile.Substring(4);
                            report.Load(reportFile);
                        }
                        else //if (reportFile.StartsWith("\\report"))
                        { //Load theo dạng file
                            reportFile = RadParams.RUNTIME_PATH + reportFile;
                            report.Load(reportFile);
                        }

                        #endregion

                        #region Gán DataSet
                        if (usingXtraReport == false)
                        {
                            view._DSList = new List<DataSet>();
                            //Set DataSet cho Main Report
                            view._DSList.Add(ds);
                            for (int i = 0; i < ds.Tables.Count; i++)
                            {
                                report.Database.Tables[i].SetDataSource(ds.Tables[i]);
                            }
                            //Set DataSet cho SubReport
                            DataSet[] dsArray = filter.getSubReports();
                            if (dsArray != null)
                            {
                                for (int i = 0; i < dsArray.Length; i++)
                                {
                                    report.Subreports[i].SetDataSource(dsArray[i]);
                                    view._DSList.Add(dsArray[i]);
                                }
                            }
                            #region Parameter
                            Dictionary<string, object> dic = filter.GetParamFieldValue();
                            foreach (CrystalDecisions.Shared.ParameterField pa in report.ParameterFields)
                            {
                                if (dic.ContainsKey(pa.Name))
                                {
                                    report.SetParameterValue(pa.Name, dic[pa.Name]);
                                }
                            }
                            if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
                            //view._SetSupportRefresh(true);
                            //view._I.ReportSource = report;//phiên bản 12
                            view.ReportSource = report;//phiên bản 10
                            #endregion

                            report.RefreshReport += new EventHandler(report_RefreshReport);
                        }
                        else//XtraReport
                        {
                            xtraReport.DataSource = ds;
                            //Dạng có Main Report + Sub Report
                            DataSet[] dsArray = filter.getSubReports();
                            if (dsArray != null)
                            {
                                List<XRSubreport> subreport = new List<XRSubreport>();
                                foreach (Band band in xtraReport.Bands)//getBands(xtraReport))
                                {
                                    foreach (XRControl xrControl in band.Controls)
                                    {
                                        if (xrControl is XRSubreport)
                                        {
                                            subreport.Add((XRSubreport)xrControl);
                                        }
                                    }
                                }

                                //for (int i = 0; i < dsArray.Length; i++)
                                //{
                                //    subreport[i].ReportSource.DataSource = dsArray[i];
                                //}
                                //duchs: gan dataset cho datasource cua subreport dua vao ten bang
                                for (int i = 0; i < dsArray.Length; i++)
                                {
                                    foreach (XRSubreport itemreport in subreport)
                                    {
                                        try
                                        {
                                            //DataSet ds=(DataSet)itemreport.DataSource;
                                            if (itemreport.ReportSource.DataMember.ToUpper().Equals(dsArray[i].Tables[0].TableName))
                                            {
                                                itemreport.ReportSource.DataSource = dsArray[i].Tables[0];
                                                break;
                                            }
                                        }
                                        catch { }
                                    }
                                }
                            }
                            #region Parameter
                            Dictionary<string, object> dic = filter.GetParamFieldValue();
                            foreach (DevExpress.XtraReports.Parameters.Parameter par in xtraReport.Parameters)
                            {
                                if (dic.ContainsKey(par.Name))
                                {
                                    xtraReport.Parameters[par.Name].Value = dic[par.Name];
                                }
                            }
                            xtraReport.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.ClosePreview, DevExpress.XtraPrinting.CommandVisibility.None);
                            //ẩn phần nhập Parameters
                            xtraReport.RequestParameters = false;
                            xtraview.PrintingSystem = xtraReport.PrintingSystem;
                            xtraReport.CreateDocument();
                            //ẩn nút Parameters trên toolbar
                            xtraReport.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.Parameters, DevExpress.XtraPrinting.CommandVisibility.None);
                            //MessageBox.Show(view.PrintingSystem.Pages.Count.ToString());
                            #endregion
                        }
                        #endregion
                        
                        
                    }

                    if (FrameworkParams.isFax == true)
                    {
                        frmFax frmFax = new frmFax(info.Caption, FrameworkParams.TEMP_FOLDER + @"\" + "$faxdoc.pdf", "Thông tin người nhận", "Điện thoại");
                        view.frmFax = frmFax;
                    }

                    //this.xtraTabControl1.TabPages.Add(page);
                    //page.Tag = this.splitContainerControl1.Panel2.Controls[0];

                    //this.xtraTabControl1.SelectedTabPage = page; 
                    
                }
                //if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
            }
            catch(Exception e){
                if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
                HelpMsgBox.ShowNotificationMessage("Tập tin " + reportFile + " không tồn tại.\nVui lòng liên hệ Công ty P R O T O C O L.");
                PLException.AddException(e);
            }
            finally {
                if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
            }
        }


        //Cờ tránh việc gọi liên tục khi có cập nhật dữ liệu trên report
        private LOCKBOOL flagRun = new LOCKBOOL();
        void report_RefreshReport(object sender, EventArgs e)
        {
            try
            {
                if (flagRun.Value == false)
                {
                    return;
                }
                IReportFilter filter = (IReportFilter)this.splitContainerControl1.Panel2.Controls[0];
                lock (flagRun)
                {
                    flagRun.Value = false;
                    GetDataSetForReport(filter, (ReportDocument)sender);
                    flagRun.Value = true;
                }
            }
            catch { }
        }

        private List<DataSet> GetDataSetForReport(IReportFilter filter, ReportDocument report)
        {
            List<DataSet> data = new List<DataSet>();
            //Set DataSet cho Main Report
            DataSet ds = filter.getDataSet();
            if (showEmptyReport == false)
            {
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    HelpMsgBox.ShowNotificationMessage("Không tìm thấy dữ liệu báo cáo.");
                    return null;
                }
            }
            data.Add(ds);
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                report.Database.Tables[i].SetDataSource(ds.Tables[i]);
            }
            //Set DataSet cho SubReport
            DataSet[] dsArray = filter.getSubReports();
            if (dsArray != null)
            {
                for (int i = 0; i < dsArray.Length; i++)
                {
                    report.Subreports[i].SetDataSource(dsArray[i]);
                    data.Add(dsArray[i]);
                }
            }
            return data;
        }
        
        private void sbPreview_Click(object sender, EventArgs e)
        {
            preview();
        }

        private void sbNewPreview_Click(object sender, EventArgs e)
        {
            treeList1_FocusedNodeChanged_1(null, null);            
        }

        private void splitContainerControl1_SizeChanged(object sender, EventArgs e)
        {
            if (this.splitContainerControl1.Panel2.Controls.Count > 0)
            {
                XtraUserControl filter = (XtraUserControl)this.splitContainerControl1.Panel2.Controls[0];
                if(Math.Abs(this.splitContainerControl1.SplitterPosition - splitContainerControl1.Height + filter.Height + 6) > 10){
                    this.splitContainerControl1.SplitterPosition = splitContainerControl1.Height - filter.Height - 6;
                }
                
            }
        }

        private void xtraTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            if (this.xtraTabControl1.SelectedTabPage != null){
                int newIndex = 0;
                if (this.xtraTabControl1.TabPages.IndexOf(this.xtraTabControl1.SelectedTabPage) > 0)
                    newIndex = this.xtraTabControl1.TabPages.IndexOf(this.xtraTabControl1.SelectedTabPage) - 1;
                this.xtraTabControl1.TabPages.Remove(this.xtraTabControl1.SelectedTabPage);

                this.xtraTabControl1.SelectedTabPage = this.xtraTabControl1.TabPages[newIndex];
            }
        }

        private bool NoRunFocusChange = false;
        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            AddPageFlag.Value = false;
            if (e.Page == this.xtraTabPageIntro){
                // Ẩn hiện nút đóng trên Tab
                this.xtraTabControl1.HeaderButtonsShowMode = DevExpress.XtraTab.TabButtonShowMode.Never;
            }
            else{
                this.xtraTabControl1.HeaderButtonsShowMode = DevExpress.XtraTab.TabButtonShowMode.Always;
                //Cập nhật lại control tìm kiếm theo chọn lựa
                UpdateSearchControl((XtraUserControl)e.Page.Tag);
                //Cập nhật lại Chọn lựa trên cây
                NoRunFocusChange = true;
                this.treeList1.FocusedNode = (TreeListNode)((XtraUserControl)e.Page.Tag).Tag;
                NoRunFocusChange = false;
            }
        }
        #region IParamForm Members

        void IParamForm.Activate()
        {
            if (TagPropertyMan.Get(this.Tag, "FORM_PARAM").ToString() != "")
            {
                FocusParam(TagPropertyMan.Get(this.Tag, "FORM_PARAM").ToString(), this.treeList1);
            }
        }

        #endregion
    }
}