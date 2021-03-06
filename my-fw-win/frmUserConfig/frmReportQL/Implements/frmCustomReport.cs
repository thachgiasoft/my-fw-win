using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CrystalDecisions.CrystalReports.Engine;
using ProtocolVN.Framework.Core;
using System.Collections;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;

namespace ProtocolVN.Framework.Win
{
    public partial class frmCustomReport : DevExpress.XtraEditors.XtraForm
    {
      
        public ReportDocument _report;
        public ReportConfig reportConfig;

        public frmCustomReport()
        {
            InitializeComponent();
            
        }

        public void Init()
        {
            InitCaption();
            InitCotDuLieu();
       
        }

        private void InitCaption()
        {
            if (reportConfig != null)
            {
                DataTable table = new DataTable();

                table.Columns.Add(ReportConfig.Caption_CurrentValue);
                table.Columns.Add(ReportConfig.Caption_NewValue);

                if (reportConfig.Captions == null || reportConfig.Captions.Count == 0)
                    reportConfig.LoadListAllCaptions();

                for (int i = 0; i < reportConfig.Captions.Count; i++)
                {
                    PLReportCaption caption = (PLReportCaption)reportConfig.GetPLReportCaption(i);
                    if (caption != null)
                    {
                        DataRow newRow = table.NewRow();
                        newRow[ReportConfig.Caption_CurrentValue] = caption.Caption;
                        newRow[ReportConfig.Caption_NewValue] = caption.Caption;
                        table.Rows.Add(newRow);
                    }

                }
                gridTBTieuDe.DataSource = table;
            }
        }

        private void InitCotDuLieu()
        {
            if (reportConfig != null)
            {
                gridTBDRCotView.Columns.Clear();
                int count = 1;
                for (int i = 0; i < reportConfig.Rows.Count; i++)
                {
                    PLReportRow RowCol = (PLReportRow)reportConfig.Rows[i];
                    DataTable table;
                    if (RowCol != null)
                    {
                        ArrayList Col = RowCol.GetPLReportColumns();
                        table = new DataTable();
                        DataRow newRow = table.NewRow();

                        for (int j = 0; j < Col.Count; j++)
                        {
                            GridColumn gridcol = (GridColumn)Col[j];

                            table.Columns.Add(gridcol.Name);
                            newRow[gridcol.Name] = gridcol.Caption;

                            gridcol.Caption = "Cột " + count;

                            gridTBDRCotView.Columns.Add(gridcol);
                            count++;
                        }
                        table.Rows.Add(newRow);
                        gridTBDRCot.DataSource = table;
                    }
                }
            }
        }

        private void frmCustomReport_Load(object sender, EventArgs e)
        {
            reportConfig = null;
            if (FrameworkParams.isCustomReport != null)
            {
                Object obj = FrameworkParams.isCustomReport.Define(this._report);
                if (obj != null && obj is ReportConfig)
                {
                    reportConfig = (ReportConfig)obj;
                }
                if (reportConfig != null)
                {
                    Init();
                }
                else
                    splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel1;
            }
        }

        

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (reportConfig != null)
            {
                DataTable table = (DataTable)gridTBTieuDe.DataSource;
                reportConfig.ChangeCaption(table);

                // cập nhật độ rộng cột
                if (splitContainerControl1.PanelVisibility == SplitPanelVisibility.Both)
                {
                    reportConfig.ApplyChanges(gridTBDRCotView.Columns);
                    DataTable headerTable = (DataTable)gridTBDRCot.DataSource;
                    reportConfig.ChangeHeaderName(headerTable);
                }
                reportConfig.SaveChanges();
                HelpMsgBox.ShowNotificationMessage("Lưu xong");
                this.Close();
            }
        }



        #region Hàm cho phép lấy các thông tin trong 1 report
        public static void ToTable(DataTable tb, ReportDocument report)
        {
            if (report != null)
            {
                for (int i = 0; i < report.ReportDefinition.ReportObjects.Count; i++)
                {
                    DataRow row = tb.NewRow();
                    row["REPORTNAME"] = report.Name;
                    row["NAME"] = report.ReportDefinition.ReportObjects[i].Name;
                    row["LEFT"] = report.ReportDefinition.ReportObjects[i].Left;
                    row["TOP"] = report.ReportDefinition.ReportObjects[i].Top;
                    row["HEIGHT"] = report.ReportDefinition.ReportObjects[i].Height;
                    row["WIDTH"] = report.ReportDefinition.ReportObjects[i].Width;
                    row["TYPE"] = report.ReportDefinition.ReportObjects[i].GetType().Name;


                    if (report.ReportDefinition.ReportObjects[i].GetType().Name.Equals("FieldHeadingObject"))
                    {
                        row["Header"] = ((FieldHeadingObject)report.ReportDefinition.ReportObjects[i]).Text;
                    }
                    else row["Header"] = "";

                    tb.Rows.Add(row);
                }

                if (report.IsSubreport == false && report.Subreports != null)
                {
                    for (int i = 0; i < report.Subreports.Count; i++)
                    {
                        ToTable(tb, report.Subreports[i]);
                    }
                }
            }
        }
        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}