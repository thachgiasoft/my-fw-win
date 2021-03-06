using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.Office.Interop.Excel;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Màn hình in barcode từ Excel hoặc từ một màn hình chọn lựa theo chuẩn cài đặt IBARCODE
    /// </summary>
    public partial class frmPrintBarCode : DevExpress.XtraEditors.XtraForm
    {
        private DataSet GridDataSet;
        public delegate IBarCode _ChonTuHeThong(frmPrintBarCode frm);
        private _ChonTuHeThong chonTuHeThong;
        public frmPrintBarCode()
        {
            InitializeComponent();
            this.btnTuSanPham.Visible = false;
        }

        public frmPrintBarCode(_ChonTuHeThong chonTuHeThong)
        {
            InitializeComponent();
            if (chonTuHeThong != null)
            {
                this.chonTuHeThong = chonTuHeThong;
                this.btnTuSanPham.Visible = true;
            }
        }

        private void frmPrintBarCode_Load(object sender, EventArgs e)
        {
            InitGrid();
            InitGridData();
        }

        private void InitGrid()
        {
            GridColumn CotDong = XtraGridSupportExt.CloseButton(gridControl, gridView);

            XtraGridSupportExt.TextLeftColumn(CotMaHang, "ID");
            XtraGridSupportExt.TextLeftColumn(CotTenSanPham, "NAME");
            XtraGridSupportExt.TextLeftColumn(CotDVT, "UNIT");
            XtraGridSupportExt.DecimalGridColumn(CotDONGIA, "PRICE");
            XtraGridSupportExt.DecimalGridColumn(CotSLG, "QUANTITY");
        }

        private void InitGridData()
        {
            System.Data.DataTable tab = new System.Data.DataTable();
            tab.Columns.Add("ID");
            tab.Columns.Add("NAME");
            tab.Columns.Add("UNIT");
            tab.Columns.Add("PRICE");
            tab.Columns.Add("QUANTITY");
            GridDataSet = new DataSet();
            GridDataSet.Tables.Add(tab);
            if (GridDataSet != null)
            {
                this.gridControl.DataSource = this.GridDataSet.Tables[0];
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            //DataView ds = (DataView)gridView.DataSource;
            this.Close();
        }

        /// <summary>
        /// In barcode tương ứng với dữ liệu có trên lưới 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInMaVach_Click(object sender, EventArgs e)
        {
            frmPrintBarCodeConfig frm = new frmPrintBarCodeConfig();
            frm.data = (System.Data.DataTable)gridControl.DataSource;
            ProtocolForm.ShowModalDialog(this, frm);
        }

        #region Chọn Từ nút Chọn Sản Phẩm
        private void btnTuSanPham_Click(object sender, EventArgs e)
        {
            //#region Nạp động từ tập tin
            //string ConfigFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
            //                        + "\\config.txt";
            //if (!File.Exists(ConfigFile))
            //{
            //    HelpMsgBox.ShowNotificationMessage("Chưa cấu hình Barcode Plugin");
            //    File.Create(ConfigFile);
            //    return;
            //}

            //StreamReader file = new StreamReader(ConfigFile);
            //string className = file.ReadToEnd();

            //Object obj = GenerateClass.initObject(className);

            //if (obj is IBarCode)
            //{
            //    DataSet ds = null;
            //    IBarCode code = (IBarCode)obj;
            //    if (code != null) ds = code.GetDataSource();
            //    if (ds != null && ds.Tables.Count > 0)
            //    {
            //        gridControl.DataSource = AppendData(ds.Tables[0], code.GetFieldMap());
            //    }
            //}
            //CODE dùng cho khởi tạo trực tiếp sẽ bỏ
            //DataSet ds = new DataSet();
            //Dictionary<string, string> DicMap = new Dictionary<string, string>();
            //khoitaodulieu(ref ds, ref DicMap);
            //gridControl.DataSource = AppendData(ds.Tables[0], DicMap);
            //#endregion

            IBarCode code = chonTuHeThong(this);
            DataSet ds = null;
            if (code != null) ds = code.GetDataSource();
            if (ds != null && ds.Tables.Count > 0)
            {
                gridControl.DataSource = AppendData(ds.Tables[0], code.GetFieldMap());
            }
        }
        #endregion


        #region Chọn các sản phẩm cần in mã vạch để tiến hình in
        //PHUOCNC: em phải chuyển dữ liệu nó vào DataTable theo cấu trúc của mình
        //Điều này đòi hỏi em phải có bảng map từ bên Client
        private System.Data.DataTable AppendData(System.Data.DataTable other, Dictionary<string, string> map)
        {
            System.Data.DataTable dt = (System.Data.DataTable)this.gridControl.DataSource;
            for (int i = 0; i < other.Rows.Count; i++)
            {
                DataRow row = dt.NewRow();
                row["ID"] = other.Rows[i][map["ID"]];
                row["NAME"] = other.Rows[i][map["NAME"]];
                row["UNIT"] = other.Rows[i][map["UNIT"]];
                row["PRICE"] = other.Rows[i][map["PRICE"]];
                row["QUANTITY"] = other.Rows[i][map["QUANTITY"]];
                dt.Rows.Add(row);
            }
            return dt;
        }
        #endregion


        /// <summary>
        /// EXPORT EXCEL TEMPLATE FILE TO IMPORT PRODUCTS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuItemXuatMauExcel_Click(object sender, EventArgs e)
        {
            ProcessExcel.ExportToGrid(gridView, "DM_HANG_HOA");
        }

        /// <summary>
        /// IMPORT EXCEL FROM TEMPLATE FILE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuItemImportExcel_Click(object sender, EventArgs e)
        {

            OpenFileDialog open = new OpenFileDialog();
            open.Filter = ProcessExcel.FILTER_FILE;
            open.Title = "Import From Excel File";
            DialogResult dr = open.ShowDialog();

            if (dr == DialogResult.Cancel) return;
            string fileNameImport = open.FileName;
            ProcessExcel.ImportToGrid(fileNameImport, gridControl, "DM_HANG_HOA", 
                new string[] { "ID", "NAME", "UNIT", "PRICE", "QUANTITY" });

        }


        private void Chon_ArrowButtonClick(object sender, EventArgs e)
        {
            ctMenuChonTuExcel.Show(Chon, new System.Drawing.Point(0, 23));
        }

    }


    class ProcessExcel
    {
        private static string constring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0;HDR=Yes;IMEX=1""";
        public const string FILTER_FILE = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx|All files (*.*)|*.*";
        private static OleDbConnection Con;
        public static void ExcelConn(String FileName)
        {
            constring = string.Format(constring, FileName);
        }
        public static OleDbConnection Open()
        {
            Con = new OleDbConnection(constring);
            try
            {
                Con.Open();
            }
            catch (OleDbException e)
            {
                Con.Close();
                return null;
            }
            catch (Exception)
            {
                Con.Close();
                return null;
            }
            return Con;
        }
        public static bool ExportToGrid(GridView gridView, String SheetName)
        {

            DataSet ds = new DataSet();
            ds.Tables.Add(ProcessExcel.CreatDataTableToGrid(SheetName, gridView));
            SaveFileDialog save = new SaveFileDialog();
            save.InitialDirectory = "My Documents://";
            save.Filter = FILTER_FILE;
            save.Title = "Save an Excel File";
            if (save.ShowDialog() == DialogResult.OK)
            {
                System.Windows.Forms.Application.DoEvents();
                if (ds != null)
                {
                    if (File.Exists(save.FileName))
                    {
                        try
                        {
                            File.Delete(save.FileName);
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.Contains("The process cannot access the file"))
                            {
                                PLMessageBox.ShowErrorMessage("Tập tin \"" + save.FileName + "\" đang mở \nhoặc đang dược sử dụng bởi chương trình khác, không thể ghi chồng lên được!");

                            }
                            else
                            {
                                HelpMsgBox.ShowNotificationMessage("Tạo file excel bị lỗi!");
                            }
                            return false;
                        }
                    }
                    string fileName = save.Filter.Split('|')[save.FilterIndex * 2 - 1].Replace("*.*", "").TrimStart('*');
                    if (Path.GetExtension(save.FileName) == fileName)
                        fileName = save.FileName;
                    else
                        fileName = save.FileName + fileName;
                    if (ProcessExcel.CreateExcelFile(ds, fileName))
                    { // HelpProcess.OpenFile(HelpProcess.ProcessName.EXCEL, fileName);
                        try
                        {
                            Process.Start("excel.exe", "\"" + System.IO.Path.GetFileName(fileName) + "\"");
                            return true;
                        }
                        catch (Exception) { HelpMsgBox.ShowNotificationMessage("Mở tập tin không thành công"); }
                    }
                }
            }
            return true;
        }
        public static bool ImportToGrid(string filenamepath, GridControl gridControl, string SheetName, string[] FieldName)
        {
            if (filenamepath == "" || filenamepath == null || !File.Exists(filenamepath)) return false;
            //Hổ trợ cho kiểm tra              

            DataSet ds = new DataSet();
            //int soRecordImported = 0;            
            string tempFilePath = Path.GetTempFileName();
            File.Copy(filenamepath, tempFilePath, true);
            FileInfo inf = new FileInfo(tempFilePath);
            if (inf.IsReadOnly) inf.IsReadOnly = false;
            ExcelConn(filenamepath);
            if (Open() != null)
            {
                gridControl.DataSource = LoadDataSet(SheetName, FieldName).Tables[0];
                return true;
            }
            return false;
        }
        private static DataSet LoadDataSet(string sheetname, object[] NameData)
        {
            OleDbDataAdapter Adapter;
            DataSet DS;
            string sql = "select * from [" + sheetname + "$]";
            if (NameData != null && NameData.Length > 0)
            {
                string filterNotNull = " where ";
                for (int i = 0; i < NameData.Length - 1; i++)
                {
                    filterNotNull += NameData[i].ToString() + " Is Not Null or ";
                }
                filterNotNull += NameData[NameData.Length - 1] + " Is Not Null";
                sql += filterNotNull;
            }
            Adapter = new OleDbDataAdapter(sql, Con);
            DS = new DataSet();
            try
            {
                Adapter.Fill(DS, sheetname);
                DS.Tables[0].Rows.RemoveAt(0);// xoa dòng đầu tiên có chứa tiêu đề cột
                return DS;
            }
            catch (Exception)
            {
            }
            return null;
        }
        private static System.Data.DataTable CreatDataTableToGrid(string NAME, GridView gridView1)
        {
            System.Data.DataTable dt = new System.Data.DataTable(NAME);
            DataRow dr;
            dr = dt.NewRow();
            for (int item = 0; item < gridView1.Columns.Count - 1; item++)
            {
                dt.Columns.Add(gridView1.Columns[item].FieldName);
                dr[gridView1.Columns[item].FieldName] = gridView1.Columns[item].Caption;
            }
            dt.Rows.Add(dr);
            return dt;
        }
        public static bool CreateExcelFile(DataSet ds, string FullFileName)
        {
            Stopwatch stopwatch = new Stopwatch();
            try
            {
                stopwatch.Start();
                DataSet demoDataSet = ds;
                ProcessExcel.ExportToExcel(demoDataSet, FullFileName);
                stopwatch.Stop();
                stopwatch.Reset();
                return true;
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
                if (ex.Message.Contains("00024500-0000-0000-C000-000000000046"))
                {
                    HelpMsgBox.ShowNotificationMessage("Máy chưa cài Microsoft Excel!");
                }
                else
                    HelpMsgBox.ShowNotificationMessage("Tạo file Excel bị lỗi");
                if (System.IO.File.Exists(FullFileName))
                {
                    System.IO.File.Delete(FullFileName);
                }
                return false;
            }
        }
        private static void ExportToExcel(DataSet dataSet, string FullFileName)
        {
            int sheetIndex = 0;
            ApplicationClass excelApp = new ApplicationClass();
            Workbook excelWorkbook = excelApp.Workbooks.Add(Type.Missing);
            Worksheet excelSheet = null;
            foreach (System.Data.DataTable dt in dataSet.Tables)
            {
                excelSheet = (Worksheet)excelWorkbook.Sheets.Add(
                    excelWorkbook.Sheets.get_Item(++sheetIndex),
                    Type.Missing, 1, XlSheetType.xlWorksheet);

                excelSheet.Name = dt.TableName;
                ((Range)excelSheet.Rows[2, Type.Missing]).Font.Bold = true;
                ((Range)excelSheet.Rows[2, Type.Missing]).Font.FontStyle = new System.Drawing.Font("Tahoma", 8.25F);
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    excelSheet.Cells[1, col + 1] = dt.Columns[col].ColumnName;
                    excelSheet.Cells[2, col + 1] = dt.Rows[0][col].ToString();
                }
                ((Range)excelSheet.Rows[1, Type.Missing]).Hidden = true;
                dt.Rows[0].Delete();
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        excelSheet.Cells[row + 3, col + 1] = dt.Rows[row].ItemArray[col];
                        ((Range)excelSheet.Rows[row + 3, Type.Missing]).Columns.EntireColumn.AutoFit();
                    }
                }

            }
            XlFileFormat xlFormat = XlFileFormat.xlWorkbookNormal;

            System.IO.FileInfo fi = new System.IO.FileInfo(FullFileName);
            if (fi.Extension.ToLower() == ".xlsx") xlFormat = XlFileFormat.xlXMLSpreadsheet;
            excelWorkbook.SaveAs(FullFileName, xlFormat, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive,
               true, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            excelWorkbook.Close(true, Type.Missing, Type.Missing);
            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorkbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            excelSheet = null;
            excelWorkbook = null;
            excelApp = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}