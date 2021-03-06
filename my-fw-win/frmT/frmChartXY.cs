using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraCharts;
namespace ProtocolVN.Framework.Win
{
    public partial class frmChartXY : DevExpress.XtraEditors.XtraForm
    {
        private DataSet ds = null;
        IChartXY ex;

        public frmChartXY(IChartXY ex)
        {
            InitializeComponent();
            this.ex = ex;
        }

        private void chartControl1_MouseClick(object sender, MouseEventArgs e)
        {
            ChartHitInfo hi = chartControl1.CalcHitInfo(e.X, e.Y);
            SeriesPoint point = hi.SeriesPoint;
            if (point != null)
            {
                DataSet ds = new DataSet();
                ds = ex.DetailData(point.Argument.ToString());
                gridControl1.DataSource = ds.Tables[0].DefaultView;
                gridView1.PopulateColumns();
                string[] caption = ex.DetailCaption();
                for (int i = 0; i < caption.Length; i++)
                {
                    gridView1.Columns[i].Caption = caption[i].ToString();
                }
                gridView1.Columns[0].Group();
                gridView1.OptionsBehavior.AutoExpandAllGroups = true;
            }
        }

        private void btnBarChart_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.PopulateColumns();
            string[] caption = ex.MasterCaption();
            for (int i = 0; i < caption.Length; i++)
            {
                gridView1.Columns[i].Caption = caption[i].ToString(); 
            }
            PopularChartData.ChangeBarView(chartControl1);
            chartControl1.Legend.Visible = false;
         }

        private void btnLineChart_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.PopulateColumns();
            string[] caption = ex.MasterCaption();
            for (int i = 0; i < caption.Length; i++)
            {
                gridView1.Columns[i].Caption = caption[i].ToString();
            }
            PopularChartData.ChangeLineView(chartControl1);
            chartControl1.Legend.Visible = false;
        }

        private void btnPieChart_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.PopulateColumns();
            string[] caption = ex.MasterCaption();
            for (int i = 0; i < caption.Length; i++)
            {
                gridView1.Columns[i].Caption = caption[i].ToString();
            }
            PopularChartData.ChangePieView(chartControl1);
            chartControl1.Legend.Visible = true;
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.PopulateColumns();
            string[] caption = ex.MasterCaption();
            for (int i = 0; i < caption.Length; i++)
            {
                gridView1.Columns[i].Caption = caption[i].ToString();
            }
        }

        private void frmTestChartXY_Load(object sender, EventArgs e)
        {
            ds = ex.MasterData();
            PopularChartData.DefineSeries(chartControl1, ex.GetSeriesName());
            PopularChartData.DefineXY(chartControl1, ds, ex.GetXFN(), ex.GetYFN());
            PopularChartData.DefineTitleChart(chartControl1, ex.GetTitle());
            PopularChartData.DefineCaption_X(chartControl1, ex.GetCaptionX());
            PopularChartData.DefineCaption_Y(chartControl1, ex.GetCaptionY());
            PopularChartData.SetAngleLabel_X(chartControl1, 0);
            PopularChartData.SetSmoothLabel_X(chartControl1, true);
            PopularChartData.SetSelectionRuntime(chartControl1, true);
            PopularChartData.SetScroll(chartControl1, true);
            PopularChartData.SetZoom(chartControl1, true);
            chartControl1.Legend.Visible = false;

            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.PopulateColumns();
            string[] caption = ex.MasterCaption();
            for (int i = 0; i < caption.Length; i++)
            {
                gridView1.Columns[i].Caption = caption[i].ToString();
            }
            gridView1.OptionsBehavior.Editable = false;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            PopularChartData.PrintPreview(chartControl1, gridControl1);
        }

       

    }
}