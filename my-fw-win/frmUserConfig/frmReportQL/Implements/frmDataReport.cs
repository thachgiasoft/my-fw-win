using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CrystalDecisions.CrystalReports.Engine;
using DevExpress.XtraGrid.Columns;
using CrystalDecisions.ReportAppServer;
using System.Collections;

namespace ProtocolVN.Framework.Win
{
    public partial class frmDataReport : DevExpress.XtraEditors.XtraForm
    {
        public ReportDocument _report;
        public List<DataSet> _DSList;
        public PLReportDataConfig reportData;

        public frmDataReport()
        {
            InitializeComponent();
        }

        private void frmCustomReport_Load(object sender, EventArgs e)
        {            
            PLReportData dataReport = new PLReportData(_report, _DSList);

            this.reportData = null;
            if (FrameworkParams.isDataReport != null)
            {
                Object obj = FrameworkParams.isDataReport.Define(dataReport);
                
                if (obj != null && obj is PLReportDataConfig)
                {
                    reportData = (PLReportDataConfig)obj;
                }
                
                if (reportData != null)
                {
                    for (int i = 0; i < reportData.DSListName.Length; i++)
                    {
                        listBoxReportData.Items.Add(reportData.DSListName[i]);
                    }
                    LoadTableToGrid(reportData.GetTable(0));
                }
            }
        }

        private void LoadTableToGrid(PLReportDataTable table)
        {
            gridViewReportData.Columns.Clear();
            if (table == null) return;

            ArrayList listCol = table.GetColumns();

            for (int i = 0; i < listCol.Count; i++)
            {
                GridColumn col = (GridColumn)listCol[i];
                gridViewReportData.Columns.Add(col);
            }
            gridControlReportData.DataSource = table.DataSource;
        }

        private void ToTable(DataTable tb, ReportDocument report)
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

        private void listBoxReportData_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tableName = listBoxReportData.SelectedItem.ToString();
            PLReportDataTable table = reportData.GetTable(tableName);
            LoadTableToGrid(table);
        }
    }
}