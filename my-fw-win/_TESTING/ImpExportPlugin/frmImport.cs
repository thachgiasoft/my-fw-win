using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using LumenWorks.Framework.IO.Csv;
using LumenWorks.Framework.IO;
using System.IO;
using DevExpress.XtraEditors;
using ProtocolVN.Plugin;
using ProtocolVN.Framework.Win;
using ProtocolVN.Plugin.ImpExp;
using ProtocolVN.Framework.Core;
using System.Data.Common;
using System.Threading;
using System.Runtime.CompilerServices;
using DevExpress.XtraGrid.Columns;
using System.Collections;
namespace ProtocolVN.Plugin.ImpExp
{
    public partial class frmImport : XtraForm
    {
        public static frmImportIDB rdbms;

        private DataTable tableMap;
        private string ObjectName="";
        private Dictionary<string, string> lstFieldGen;
        private DatabaseFB db;
        private DataSet dsSheet;
        private string sheetObject = "";
        private int rowError ;
        private List<Array> lstRowError;
        private Dictionary<GridColumn, DataRow> ReCreateRes;
        private DataTable ReDMInfo = null;
        private string IdDM,NameDM;

        public frmImport()
        {
            InitializeComponent();
            IdDM = "ID";
            NameDM = "NAME";
            lstFieldGen = new Dictionary<string, string>();
            ReCreateRes = new Dictionary<GridColumn, DataRow>();
            db = DABase.getDatabase();
            dsSheet = new DataSet();
            lblDBName.Text = RadParams.database;
            lblServerName.Text = RadParams.server;
            gridViewMap.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridViewMap_CellValueChanged);
        }

        //Tạo tất cả các item trong danh mục chỉ định. Thể hiện cột ở dữ liệu đích có danh mục
        private void CreateComboItemDM(GridColumn column,string tenDM, string value, string display,string field)
        {
            try
            {
                if (tenDM != "")
                {
                    DataSet ds = DABase.getDatabase().LoadTable(tenDM);
                    HelpGridColumn.CotCombobox(column, ds, value, display, field);
                }
                else
                    column.ColumnEdit = null;
            }
            catch {}
        }
        private GridColumn FindColumn(string field)
        {
            foreach (GridColumn column in gridviewDataTarget.Columns)
            {
                if (column.FieldName.Equals(field))
                    return column;
            }
            return null;
        }
        //Tạo danh sách các field trong danh mục chỉ định
        private RepositoryItemComboBox CreateFieldDM(GridColumn column, string tenDM)
        {
            DataSet ds = DABase.getDatabase().LoadTable(tenDM);
            RepositoryItemComboBox resComboBox = new RepositoryItemComboBox();
            foreach (DataColumn dtColumn in ds.Tables[0].Columns)
            {
                resComboBox.Items.Add(dtColumn.ColumnName);
            }
            column.ColumnEdit = resComboBox;
            return resComboBox;
        }

        //Kiem tra field chi dinh co ton tai trong datasource cua gridview taget khong
        private bool isExistField(DataTable dtTarget,string field)
        {
            foreach (DataColumn column in dtTarget.Columns)
                if (column.ColumnName == field)
                    return true;
            return false;
        }
        void gridViewMap_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == gridColumn4)
            {
                DataRow dr = gridViewMap.GetDataRow(e.RowHandle);
                GridColumn column = FindColumn(dr[0].ToString());
                if (!ReCreateRes.ContainsKey(column))
                    ReCreateRes.Add(column, dr);
                CreateComboItemDM(column, e.Value.ToString(),IdDM, NameDM, dr[0].ToString());
                DataTable dt = ((DataView)gridviewDataTarget.DataSource).Table;
                if (e.Value.ToString() != "")
                {
                    if (!isExistField(dt, dr["FIELDNAME"].ToString() + "_SOURCE"))
                    {
                        DataColumn dc = new DataColumn(dr["FIELDNAME"].ToString() + "_SOURCE");
                        dt.Columns.Add(dc);
                    }
                }
                else
                {
                    if (isExistField(dt, dr["FIELDNAME"].ToString() + "_SOURCE"))
                    {
                        dt.Columns.Remove(dr["FIELDNAME"].ToString() + "_SOURCE");
                    }
                }

            }
        }

        #region "Read Data "
        private string OpenFileName()
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Excel files (*.xls,*.xlsx)|*.xls;*.xlsx|All files (*.*)|*.*";
                open.FilterIndex = 2;
                if (open.ShowDialog() == DialogResult.OK) 
                    return open.FileName;
                return "";
            }
            catch{}
            return "";
        }
      
        private void InsertData(string sheetName)
        {
            if (dsSheet == null) return;
            gridviewDataSource.Columns.Clear();
            gridDataSource.DataSource = dsSheet.Tables[sheetName];
            DisplayGridMap();
        }
        #endregion

        #region "Create Mapping"
        //Tạo comboBox các field ở dữ liệu nguồn và các generator có trong database. Tạo trên gridViewMap 
        private RepositoryItemComboBox CreateCombobox()
        {
            RepositoryItemComboBox combo = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            combo.Items.Add("");
            for (int i = 0; i < gridviewDataSource.Columns.Count; i++)
                combo.Items.Add(gridviewDataSource.Columns[i].Caption);

            combo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            AddGenerator(combo);
            return combo;
        }

        //Tạo danh sách danh mục có trong database
        private RepositoryItemComboBox CreatComboBoxDM()
        {
            RepositoryItemComboBox combo = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            combo.Items.Add("");
            for (int i = 0; i < comboListTable.Items.Count; i++)
            {
                string dm = comboListTable.Items[i].ToString();
                if(dm.StartsWith("DM"))
                    combo.Items.Add(dm);
            }

            combo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            //AddGenerator(combo);
            return combo;
        }
        private DataTable CreateTableMap()
        {
            DataTable table;
            table = new DataTable();
            DataColumn dcField = new DataColumn("FIELDNAME");
            table.Columns.Add(dcField);
            DataColumn dcCaption = new DataColumn("CAPTION");
            table.Columns.Add(dcCaption);
            DataColumn dcCotNguon = new DataColumn("COT_NGUON");
            table.Columns.Add(dcCotNguon);
            DataColumn dcDanhMuc = new DataColumn("DANH_MUC");
            table.Columns.Add(dcDanhMuc);
            DataColumn dcDefaule = new DataColumn("DEF_VALUE");
            table.Columns.Add(dcDefaule);
            DataColumn dcValue = new DataColumn("VALUE_FIELD");
            table.Columns.Add(dcValue);
            DataColumn dcDisplay = new DataColumn("DISPLAY_FIELD");
            table.Columns.Add(dcDisplay);
            return table;
        }

        //Tạo bảng để lưu trữ các giá trị bị conflit
        private DataTable CreatTableDMResovle()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("TEN_DM"));
            dt.Columns.Add(new DataColumn("ORG_VALUE"));
            dt.Columns.Add(new DataColumn("ID_LIKE_VALUE"));
            dt.Columns.Add(new DataColumn("LIKE_VALUE"));
            dt.Columns.Add(new DataColumn("INDEX"));
            dt.Columns.Add(new DataColumn("COLUMNFIELD"));
            return dt;
        }
        private void DisplayGridMap()
        {
            ClearText();
            gridViewMap.Columns["COT_NGUON"].ColumnEdit = CreateCombobox();                        
        }
        #endregion

        #region "Move Data"
        private void ClearRowBlank(DataTable table)
        {            
            for (int i = table.Rows.Count - 1; i >= 0; i--)
            {
                bool IsBlank = true;
                for (int j = 0; j < table.Columns.Count && IsBlank; j++)
                {
                    if (table.Rows[i][j].ToString() != "")
                        IsBlank = false;
                }
                if (IsBlank)
                {
                    table.Rows[i].Delete();
                    
                }
            }
        }

        //Lây id của một item chỉ định trong danh mục chỉ định
        private object GetIDFormDM(string TenDm, string ten)
        {
            object o = null;
            DataSet ds = DABase.getDatabase().LoadTable(TenDm);
            DataRow[] dr = ds.Tables[0].Select(NameDM +"= '" + ten + "'");
            if (dr !=null && dr.Length>0)
                o = dr[0][IdDM];
            return o;
        }
        private Dictionary<string,string> GetLikeName(string TenDm, string ten)
        {
            DataSet ds = DABase.getDatabase().LoadTable(TenDm);
           // DataRow[] dr = ds.Tables[0].Select("NAME like '%"+ten+"%'");
            //List<string> names = new List<string>();
            Dictionary<string, string> info = new Dictionary<string, string>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string id = row[IdDM].ToString();
                if (!info.ContainsKey(id))
                   info.Add(id, row[NameDM].ToString());
            }
            return info;
        }

        private void MoveData()
        {
            try
            {
                DataTable dtSource = ((DataView)gridviewDataSource.DataSource).Table;
                DataTable dtMap = ((DataView)gridViewMap.DataSource).Table;
                DataTable dtNewAddTarget = ((DataView)gridviewDataTarget.DataSource).Table;
                ReDMInfo = CreatTableDMResovle();
                long indexLastRow = dtNewAddTarget.Rows.Count - 1;
                int rowUpdate = 0;
                foreach (int i in gridviewDataSource.GetSelectedRows())
                {
                    DataRow newRowTarget = dtNewAddTarget.NewRow();
                    //DataRow rowSource = gridviewDataSource.GetDataRow(i);
                    DataRow rowSource = dtSource.Rows[i];
                    bool insSuccessful = false;
                    indexLastRow++;
                    foreach (DataRow rowMap in dtMap.Rows)
                    {
                        string fieldTarget = rowMap["FIELDNAME"].ToString();
                        string fieldSource = rowMap["COT_NGUON"].ToString();
                        if (CheckField(dtSource, fieldSource))
                            try 
                            {
                                string valueSource = rowSource[fieldSource].ToString();
                                if (valueSource != "")
                                {
                                    string TenDm = rowMap["DANH_MUC"].ToString();
                                    if(TenDm!="")
                                    {
                                        object o = GetIDFormDM(TenDm,valueSource);
                                        newRowTarget[fieldTarget + "_SOURCE"] = valueSource;
                                        if (o != null)
                                        {
                                            newRowTarget[fieldTarget] = o;
                                            insSuccessful = true;
                                        }
                                        else
                                        {
                                            Dictionary<string, string> likeValue = GetLikeName(TenDm, valueSource);
                                            foreach (string key in likeValue.Keys)
                                            {
                                                DataRow row = ReDMInfo.NewRow();
                                                row["TEN_DM"] = TenDm;
                                                row["ORG_VALUE"] = valueSource;
                                                row["ID_LIKE_VALUE"] = key;
                                                string value;
                                                likeValue.TryGetValue(key, out value);
                                                row["LIKE_VALUE"] = value;
                                                row["INDEX"] = indexLastRow;
                                                row["COLUMNFIELD"] = fieldTarget;
                                                ReDMInfo.Rows.Add(row);
                                            }
                                        }

                                    }
                                    else
                                    {
                                        newRowTarget[fieldTarget] = rowSource[fieldSource];
                                        insSuccessful = true;
                                    }
                                }
                                else
                                {
                                    newRowTarget[fieldTarget] = rowMap["DEF_VALUE"].ToString();
                                    insSuccessful = true;
                                }
                            }
                            catch
                            {
                                try { newRowTarget[fieldTarget] = rowMap["DEF_VALUE"]; insSuccessful = true; }
                                catch { }
                            }
                        else
                        {
                            string defvalue = rowMap["DEF_VALUE"].ToString();
                            if (fieldSource == "" && defvalue != "")
                            {
                                try { newRowTarget[fieldTarget] = defvalue; insSuccessful = true; }
                                catch { }
                            }
                            else 
                                AddList(fieldTarget, fieldSource);
                        }
                    }
                    if (insSuccessful && rowSource!=null)
                    {
                        rowSource.Delete();
                        newRowTarget["NEW_ROW"] = 1;
                        rowUpdate++;
                        dtNewAddTarget.Rows.Add(newRowTarget);
                    }
                }
                gridDataTarget.DataSource = dtNewAddTarget;
                ClearRowBlank(dtNewAddTarget);
                HelpMsgBox.ShowNotificationMessage("Số dòng dữ liệu chèn vào thành công: " + (rowUpdate).ToString());
            }
            catch (Exception ex) { PLException.AddException(ex); }
        }

        private void ClearText()
        {
            for (int i = 0; i < gridViewMap.RowCount; i++)
                gridViewMap.SetRowCellValue(i, gridViewMap.Columns[1], "");
        }
        private void MoveAllData()
        {
            try
            {
                DataTable dtSource = ((DataView)gridviewDataSource.DataSource).Table;
                if (dtSource != null)
                {
                    DataTable dtMap = ((DataView)gridViewMap.DataSource).Table;
                    DataTable dtNewAddTarget = ((DataView)gridviewDataTarget.DataSource).Table;
                    //startIndexAdd = dtNewAddTarget.Rows.Count;
                    ReDMInfo = CreatTableDMResovle();
                    ReDMInfo.Rows.Clear();
                    long indexLastRow = dtNewAddTarget.Rows.Count - 1;
                    int rowUpdate = 0;
                    for (int i = 0; i < dtSource.Rows.Count; i++)
                    {
                        DataRow newRowTarget = dtNewAddTarget.NewRow();
                        //DataRow rowSource = gridviewDataSource.GetDataRow(i);
                        DataRow rowSource = dtSource.Rows[i];
                        bool insSuccessful = false;
                        indexLastRow++;
                        foreach (DataRow rowMap in dtMap.Rows)
                        {
                            string fieldTarget = rowMap["FIELDNAME"].ToString();
                            string fieldSource = rowMap["COT_NGUON"].ToString();
                            if (CheckField(dtSource, fieldSource))
                                try
                                {
                                    string valueSource = rowSource[fieldSource].ToString();
                                    if (!valueSource.Equals(""))
                                    {
                                        string TenDm = rowMap["DANH_MUC"].ToString();
                                        if (!TenDm.Equals(""))
                                        {
                                            object o = GetIDFormDM(TenDm, valueSource);
                                            newRowTarget[fieldTarget + "_SOURCE"] = valueSource;
                                            if (o != null)
                                            {
                                                newRowTarget[fieldTarget] = o;
                                                insSuccessful = true;
                                            }
                                            else
                                            {
                                                Dictionary<string, string> likeValue = GetLikeName(TenDm, valueSource);
                                                foreach (string key in likeValue.Keys)
                                                {
                                                    DataRow row = ReDMInfo.NewRow();
                                                    row["TEN_DM"] = TenDm;
                                                    row["ORG_VALUE"] = valueSource;
                                                    row["ID_LIKE_VALUE"] = key;
                                                    string value;
                                                    likeValue.TryGetValue(key, out value);
                                                    row["LIKE_VALUE"] = value;
                                                    row["INDEX"] = indexLastRow;
                                                    row["COLUMNFIELD"] = fieldTarget;
                                                    ReDMInfo.Rows.Add(row);
                                                }
                                            }

                                        }
                                        else
                                        {
                                            newRowTarget[fieldTarget] = rowSource[fieldSource];
                                            insSuccessful = true;
                                        }
                                    }
                                    else
                                    {
                                        newRowTarget[fieldTarget] = rowMap["DEF_VALUE"].ToString();
                                        insSuccessful = true;
                                    }
                                }
                                catch
                                {
                                    try { newRowTarget[fieldTarget] = rowMap["DEF_VALUE"]; insSuccessful = true; }
                                    catch { }
                                }
                            else
                            {
                                string defvalue = rowMap["DEF_VALUE"].ToString();
                                if (fieldSource == "" && defvalue != "")
                                {
                                    try { newRowTarget[fieldTarget] = defvalue; insSuccessful = true; }
                                    catch { }
                                }
                                else
                                    AddList(fieldTarget, fieldSource);
                            }
                        }
                        if (insSuccessful && rowSource!=null)
                        {
                            rowSource.Delete();
                            newRowTarget["NEW_ROW"] = 1;
                            rowUpdate++;
                            dtNewAddTarget.Rows.Add(newRowTarget);
                        }
                    }
                    gridDataTarget.DataSource = dtNewAddTarget;
                    ClearRowBlank(dtNewAddTarget);
                    HelpMsgBox.ShowNotificationMessage("Số dòng dữ liệu chèn vào thành công: " + (rowUpdate).ToString());
                }
            }
            catch (Exception ex) { PLException.AddException(ex); }
        }

      
        #endregion

        #region "Events"
        private void menuMapping_Click(object sender, EventArgs e)
        {
            if (menuMapping.Checked)
                splitContainerControl2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
            else
                splitContainerControl2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
        }
        private void btnNap_Click(object sender, EventArgs e)
        {
            if (ExcelSupport.filenamepath != "")
            {
                DialogResult result = PLMessageBox.ShowConfirmMessage("Bạn có chắc chọn tập tin khác không?");
                if (result == DialogResult.Yes)
                {
                    ExcelSupport.filenamepath = OpenFileName();
                    if (ExcelSupport.filenamepath != "")
                        WaitingMsg.LongProcess(AddSheetExcel);
                }
            }
            else
            {
                ExcelSupport.filenamepath = OpenFileName();
                if (ExcelSupport.filenamepath != "")
                    WaitingMsg.LongProcess(AddSheetExcel);
            }
        }
        private void AddSheetExcel()
        {
            gridDataSource.DataSource = null;
            sheetObject = "";
            cbSelSheet.Text = "Chọn sheet";
            AddSheet(ExcelSupport.GetSheetNames(ExcelSupport.filenamepath));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateListTable();
            tableMap = CreateTableMap();
            gridMap.DataSource = tableMap;
            gridViewMap.Columns[0].OptionsColumn.AllowFocus = false;
            gridViewMap.Columns[0].OptionsColumn.AllowEdit = false;
            gridColumn4.ColumnEdit = CreatComboBoxDM();
            gridviewDataSource.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            gridviewDataTarget.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            gridViewMap.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            gridviewDataSource.DataSourceChanged += new EventHandler(gridviewDataSource_DataSourceChanged);
            gridviewDataTarget.DataSourceChanged += new EventHandler(gridviewDataTarget_DataSourceChanged);
        }

        void gridviewDataTarget_DataSourceChanged(object sender, EventArgs e)
        {
            gridviewDataTarget.BestFitColumns();
        }

        void gridviewDataSource_DataSourceChanged(object sender, EventArgs e)
        {
            gridviewDataSource.BestFitColumns();
        }

        private void menuInsertData_Click(object sender, EventArgs e)
        {            
            CommitData();
            WaitingMsg.LongProcess(MoveData);
            ProcessData(ReDMInfo);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            gridviewDataSource.DeleteSelectedRows();
            ((DataView)gridviewDataSource.DataSource).Table.AcceptChanges();
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (gridDataSource.DataSource == null || ((DataTable)gridDataSource.DataSource).Rows.Count <= 0) return;
            DialogResult result = PLMessageBox.ShowConfirmMessage("Bạn có chắc muốn xóa hết dữ liệu không?");
            if(result == DialogResult.Yes)
            {
                DataTable dt = (DataTable)gridDataSource.DataSource;
                dt.Rows.Clear();
            }
        }

        private void menuDelete_Click(object sender, EventArgs e)
        {
            gridviewDataTarget.DeleteSelectedRows();
        }

        private void menuDeleteAll_Click(object sender, EventArgs e)
        {
            gridviewDataTarget.SelectAll();
            gridviewDataTarget.DeleteSelectedRows();
        }

        //Tạo thêm cột dòng mới và dong lỗi trong gridview đích
        private void CreateGridColumn(DataTable dt, string tableName)
        {
            DataColumn colError = new DataColumn("ERROR");
            dt.Columns.Add(colError);
            DataColumn colNewRow = new DataColumn("NEW_ROW");
            dt.Columns.Add(colNewRow);

            GridColumn cotLoi = new GridColumn();
            cotLoi.Caption = "Dòng lỗi";
            cotLoi.FieldName = "ERROR";
            cotLoi.OptionsColumn.AllowEdit = false;
            RepositoryItemCheckEdit chkEditErr = new RepositoryItemCheckEdit();
            chkEditErr.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Standard;
            chkEditErr.ValueChecked = "1";
            chkEditErr.ValueUnchecked = null;
            cotLoi.ColumnEdit = chkEditErr;
            cotLoi.VisibleIndex = 0;
            cotLoi.Visible = true;
            gridviewDataTarget.Columns.Add(cotLoi);
            GridColumn cotNewRow = new GridColumn();
            cotNewRow.Caption = "Dòng mới";
            cotNewRow.FieldName = "NEW_ROW";
            cotNewRow.OptionsColumn.AllowEdit = false;
            RepositoryItemCheckEdit chkEditNew = new RepositoryItemCheckEdit();
            chkEditNew.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Standard;
            chkEditNew.ValueChecked = "1";
            chkEditNew.ValueUnchecked = null;
            cotNewRow.ColumnEdit = chkEditNew;
            cotNewRow.VisibleIndex = 1;
            cotNewRow.Visible = true;
            gridviewDataTarget.Columns.Add(cotNewRow);

            DataSet ds = DABase.getDatabase().LoadTable("FW_MAP_FIELD_CAPTION");
            DataRow[] drows = ds.Tables[0].Select("TABLE_NAME='" + tableName + "'");
            if (drows.Length > 0)
            {
                for (int i = 0; i < drows.Length; i++)
                {
                    DataRow dr = drows[i];
                    GridColumn col = new GridColumn();
                    col.Caption = dr["CAPTION"].ToString();
                    col.FieldName = dr["FIELD_NAME"].ToString();
                    col.VisibleIndex = i + 2;
                    col.Visible = true;
                    gridviewDataTarget.Columns.Add(col);
                }
            }
            else
            {
                for (int i = 0; i < dt.Columns.Count-2; i++)
                {
                    DataColumn dc = dt.Columns[i];
                    GridColumn col = new GridColumn();
                    col.Caption = dc.Caption;
                    col.FieldName = dc.ColumnName;
                    col.VisibleIndex = i + 2;
                    col.Visible = true;
                    gridviewDataTarget.Columns.Add(col);
                }
            }
           
        }

        private void comboListTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            WaitingMsg.LongProcess(SelectTable);
        }

        List<string> chkField;
        private void ReLoadDM(DataTable dtReDM)
        {
            chkField = new List<string>();
            foreach (DataRow dr in dtReDM.Rows)
            {
                if (!chkField.Contains(dr["FIELD"].ToString()))
                {
                    DataTable dt = ((DataView)gridViewMap.DataSource).Table;
                    DataRow[] lstRow = dt.Select("FIELDNAME='" + dr["FIELD"].ToString() + "'");
                    string tenDM = lstRow[0]["DANH_MUC"].ToString();
                    GridColumn column = FindColumn(dr["FIELD"].ToString());

                    //Thay Id va Name khi anh xa tu fieldname qua caption
                    CreateComboItemDM(column, tenDM, IdDM, NameDM, dr["FIELD"].ToString());
                    chkField.Add(dr["FIELD"].ToString());
                }
            }
        }
        //Gán lại dữ liệu sau khi giải quyết conflit
        private void ProcessData(DataTable dtResolve)
        {
            if (dtResolve != null & dtResolve.Rows.Count > 0)
            {
                frmResolveDM frmDM = new frmResolveDM(dtResolve);
                ProtocolForm.ShowModalDialog(this, frmDM);
                DataTable newValue = frmDM.OutputTable;
                if (newValue != null)
                {
                    ReLoadDM(newValue);
                    DataTable gridSource = ((DataView)gridviewDataTarget.DataSource).Table;
                    foreach (DataRow row in newValue.Rows)
                    {
                        string field = row["FIELD"].ToString();
                        int index = int.Parse(row["INDEX"].ToString());
                        string value = row["ID"].ToString();
                        if (index < gridSource.Rows.Count)
                        {
                            DataRow rowTarget = gridSource.Rows[index];
                            rowTarget[field] = value;
                        }
                        else
                        {
                            DataRow newRow = gridSource.NewRow();
                            newRow[field] = value;
                            newRow["NEW_ROW"] = 1;
                            gridSource.Rows.Add(newRow);
                        }
                    }
                }
            }
        }
        private void SetCaptionMap(string tableName,ref DataTable dtMap, DataTable dtClone)
        {
            DataSet ds = DABase.getDatabase().LoadTable("FW_MAP_FIELD_CAPTION");
            DataRow[] drows = ds.Tables[0].Select("TABLE_NAME='" + tableName + "'");
            if (drows.Length > 0)
            {
                foreach (DataRow dr in drows)
                {
                    DataRow newRow = dtMap.NewRow();
                    newRow[0] = dr["FIELD_NAME"];
                    newRow[1] = dr["CAPTION"];
                    dtMap.Rows.Add(newRow);
                }
            }
            else
            {
                foreach (DataColumn dc in dtClone.Columns)
                {
                    DataRow newRow = dtMap.NewRow();
                    newRow[0] = dc.ColumnName;
                    newRow[1] = dc.ColumnName;
                    dtMap.Rows.Add(newRow);
                }
            }
        }

        private void SelectTable()
        {
            if (comboListTable.SelectedIndex < 0) return;
            if (ObjectName != "" && ObjectName != comboListTable.Text && ((DataTable)gridDataTarget.DataSource).Rows.Count > 0)
            {
                if (PLMessageBox.ShowConfirmMessage("Bạn thay đổi đối tượng làm việc, dữ liệu trên bảng sẽ mất hết. Bạn có chắc chắn hay không ?") != DialogResult.Yes)
                {
                    comboListTable.SelectedItem = ObjectName;
                    return;
                }
            }
         
            ObjectName = comboListTable.Text.Trim();
            tableMap.Clear();
            if (rdbms != null)
            {
                rdbms.init(this.db, lstRowError, rowError);
            }
            DataTable tb = rdbms.LoadDataSetTarget(comboListTable.Text).Tables[0];
            //Sao chep cau truc cau table goc neu bang ko co trong bang Map caption thi gan cac field nhu trong bang
            DataTable cloneDt = tb.Clone();
            SetCaptionMap(comboListTable.Text, ref tableMap,cloneDt);
            gridMap.DataSource = tableMap;
            gridviewDataTarget.Columns.Clear();
            CreateGridColumn(tb,comboListTable.Text);
            gridDataTarget.DataSource = tb;
            MappingAuto();
        }
        private void menuClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CommitData()
        {
            try { gridviewDataSource.FocusedRowHandle = gridviewDataSource.FocusedRowHandle + 1;
            gridviewDataSource.FocusedRowHandle = gridviewDataSource.FocusedRowHandle - 1;
            }
            catch { }
            try { gridviewDataTarget.FocusedRowHandle = gridviewDataTarget.FocusedRowHandle + 1;
                gridviewDataTarget.FocusedRowHandle = gridviewDataTarget.FocusedRowHandle - 1;
            }
            catch { }
            try { gridViewMap.FocusedRowHandle = gridViewMap.FocusedRowHandle + 1;
            gridViewMap.FocusedRowHandle = gridViewMap.FocusedRowHandle - 1;
            }
            catch { }
            ((DataView)gridviewDataSource.DataSource).Table.AcceptChanges();
        }
        private void btnInsertAllData_Click(object sender, EventArgs e)
        {
            CommitData();
            WaitingMsg.LongProcess(MoveAllData);
            ProcessData(ReDMInfo);
        }
       
        #endregion

        //Load tất cả các table có trong database
        private void CreateListTable()
        {
            // them cac Table vao comboxbox
            string command = @"SELECT RDB$RELATION_NAME
                               FROM RDB$RELATIONS
                               WHERE RDB$SYSTEM_FLAG = 0
                               --AND RDB$VIEW_BLR IS NULL
                               ORDER BY RDB$RELATION_NAME";
            DbCommand sqlCommand = DABase.getDatabase().GetSQLStringCommand(command);
            DataSet dsTableName = DABase.getDatabase().LoadDataSet(sqlCommand);
            foreach (DataRow row in dsTableName.Tables[0].Rows)
                comboListTable.Items.Add(row[0].ToString().Trim());
        }
        // ham dung de loat dataset target, lay table duoc chon tren combobox
        
        private void btnSaveData_Click(object sender, EventArgs e)
        {
            WaitingMsg.LongProcess(UpdateData);
        }
        private void cbSelSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            WaitingMsg.LongProcess(SelectSheet);
        }

        private void SelectSheet()
        {
            if (sheetObject == cbSelSheet.Text) return;
            if (sheetObject != "")
            {
                DialogResult result = PLMessageBox.ShowConfirmMessage("Bạn có chắc chọn sheet khác không?");
                if (result == DialogResult.Yes)
                {
                    string sheetName = cbSelSheet.Text;
                    InsertData(sheetName);
                    MappingAuto();
                    sheetObject = sheetName;
                }
                else
                {
                    cbSelSheet.SelectedItem = sheetObject;
                    return;
                }
            }
            else
            {
                string sheetName = cbSelSheet.Text;
                InsertData(sheetName);
                MappingAuto();
                sheetObject = sheetName;
            }
        }        
        public void FilterRow(ref DataTable dt)
        {
            int indexrow = 0;
            while (indexrow < dt.Rows.Count)
            {
                DataRow row = dt.Rows[indexrow];
                int i = indexrow + 1;
                while (i < dt.Rows.Count)
                {
                    if (dt.Rows[i][0].Equals(row[0]))
                    {
                        dt.Rows.RemoveAt(i);
                        i -= 1;
                    }
                    i += 1;
                }
                indexrow += 1;
            }
        }
        private void AddGen()
        {
            DataTable dtTarget = ((DataView)gridviewDataTarget.DataSource).Table;
            foreach (string field in lstFieldGen.Keys)
            {
                string genName;
                lstFieldGen.TryGetValue(field, out genName);
                genName = genName.Substring(1, genName.Length - 2);
                for (int i = 0; i < dtTarget.Rows.Count; i++)
                {
                    DataRow row = dtTarget.Rows[i];
                    if(row.RowState!= DataRowState.Deleted && row[field].ToString()=="")
                        row[field] = db.GetID(genName);
                }
            }
            gridDataTarget.DataSource = dtTarget;
        }
        private void AddDataSheet(string sheetName)
        {
            DataTable dt = new DataTable(sheetName);
            ExcelSupport.open();
            DB xuly = new DB();
            xuly._DataSet = ExcelSupport.dataSet(sheetName);
            gridviewDataSource.Columns.Clear();
            dt = xuly.xulydataset().Tables[0].Copy();
            ExcelSupport.close();
            if ((dt.Columns.Count == 1) && (dt.Rows.Count >= 2))
                FilterRow(ref dt);
            dsSheet.Tables.Add(dt);
        }
        private void AddSheet(List<string> lstSheet)
        {
            if (lstSheet.Count > 0)
            {
                dsSheet.Tables.Clear();
                cbSelSheet.Items.Clear();
                foreach (string sheet in lstSheet)
                {
                    cbSelSheet.Items.Add(sheet.Trim());
                    AddDataSheet(sheet);
                }
                cbSelSheet.Visible = true;
                lblSheetName.Visible = true;
            }
            else
            {
                cbSelSheet.Visible = false;
                lblSheetName.Visible = false;
            }
        }
        private void MappingAuto()
        {
            for (int i = 0; i < tableMap.Rows.Count; i++)
            {
                RepositoryItemComboBox combo = CreateCombobox();
                tableMap.Rows[i][2] = "";
                if (combo.Items.IndexOf(tableMap.Rows[i][0].ToString()) >= 0)
                {
                    tableMap.Rows[i][2] = tableMap.Rows[i][0];
                }
            }
        }
        private void AddList(string field, string genName)
        {
            if(genName.StartsWith("(") && genName.EndsWith(")"))
            {
                if (!lstFieldGen.ContainsKey(field))
                    lstFieldGen.Add(field, genName);
                else
                    lstFieldGen[field] = genName;
            }
        }
        private bool CheckField(DataTable dtSource, string field)
        {
            foreach (DataColumn column in dtSource.Columns)
                if (column.ColumnName.Equals(field))
                    return true;
            return false;
        }
        private void AddGenerator(RepositoryItemComboBox comboBox)
        {
            string command = @"SELECT RDB$GENERATOR_NAME
                              FROM RDB$GENERATORS
                              WHERE RDB$SYSTEM_FLAG = 0
                              ORDER BY RDB$GENERATOR_NAME";
            DbCommand sqlCommand = DABase.getDatabase().GetSQLStringCommand(command);
            DataSet ds = DABase.getDatabase().LoadDataSet(sqlCommand);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string gen = "(" + row[0].ToString().Trim() + ")";
                comboBox.Items.Add(gen.Trim());
            }
        }
        //private DataSet LoadDataSetTarget(string tablename)
        //{
        //    return DABase.getDatabase().LoadTable(tablename);
        //}
        //private int UpdateTable(DataSet dataSet)
        //{
        //    DbCommand sQLStringCommand = db.GetSQLStringCommand("SELECT * FROM " + dataSet.Tables[0].TableName);
        //    sQLStringCommand.Connection = db.OpenConnection();
        //    FbDataAdapter adapter = new FbDataAdapter();
        //    FbCommandBuilder builder = new FbCommandBuilder(adapter);
        //    adapter.SelectCommand = (FbCommand)sQLStringCommand;
        //    adapter.DeleteCommand = builder.GetDeleteCommand();
        //    adapter.UpdateCommand = builder.GetUpdateCommand();
        //    adapter.RowUpdated += new FbRowUpdatedEventHandler(adapter_RowUpdated);
        //    return adapter.Update(dataSet.Tables[0]);
        //}
        //void adapter_RowUpdated(object sender, FbRowUpdatedEventArgs e)
        //{
        //    try
        //    {
        //        DataRow dr = e.Row;
        //        if (dr.HasErrors)
        //        {
        //            dr.ClearErrors();
        //            Object[] arrObj = new Object[dr.ItemArray.Length];
        //            dr.ItemArray.CopyTo(arrObj, 0);
        //            rowError++;
        //            lstRowError.Add(arrObj);
        //            dr.Delete();
        //        }
        //        else
        //            dr["NEW_ROW"] = null;
        //    }
        //    catch { }
        //}

        private void UpdateData()
        {
            try
            {
                gridviewDataTarget.CloseEditor();
                gridviewDataTarget.MoveNext();
                gridviewDataTarget.MovePrev();
                db = DABase.getDatabase();
                AddGen();
                DataView view = (DataView)gridviewDataTarget.DataSource;
                DataSet ds = view.Table.DataSet;
                ds.Tables[0].TableName = ds.Tables[0].TableName.Trim();
                ds.Tables[0].Columns.Remove("ERROR");
                DataColumn cotLoi = new DataColumn("ERROR");
                cotLoi.ColumnName = "ERROR";
                ds.Tables[0].Columns.Add(cotLoi);
                rowError = 0;
                lstRowError = new List<Array>();
                while (true)
                {
                    try
                    {
                        if (rdbms != null)
                        {
                            rdbms.init(db, lstRowError, rowError);
                        }
                        if (rdbms.UpdateTable(ds) != -1)
                        {
                            rowError = rdbms.getRowErrorAfterUpdateTable();
                            ds.Tables[0].AcceptChanges();
                            SetRowError(ref ds);
                            HelpMsgBox.ShowNotificationMessage("Số dòng thêm vào thành công là :" + (ds.Tables[0].Rows.Count - rowError).ToString() + "/" + ds.Tables[0].Rows.Count.ToString());
                            break;
                        }

                    }
                    catch (Exception e) { PLException.AddException(e); }
                    finally { db.Close(); }
                }
            }
            catch (Exception e)
            {
                PLException.AddException(e);
                PLMessageBox.ShowErrorMessage("Thêm dữ liệu không thành công");
            }
        }   
        private void SetRowError(ref DataSet ds)
        {
            int numCol = ds.Tables[0].Columns.Count;
            foreach(Array arr in lstRowError)
            {
                DataRow dr = ds.Tables[0].NewRow();
                for(int i=0;i<numCol;i++)
                    dr[i] = arr.GetValue(i);
                dr["ERROR"] = 1;
                ds.Tables[0].Rows.Add(dr);
            }
        }
        

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string fields = "";
            for (int i = 2; i < gridviewDataTarget.Columns.Count; i++)
            {
                GridColumn column = gridviewDataTarget.Columns[i];
                fields += column.FieldName + ";";
            }
            if (fields.Length > 0)
                fields = fields.Remove(fields.Length - 1);
            frmExportToExcel form = new frmExportToExcel(fields);
            form.ShowDialog();
        }
   
        private DataTable ReCreateConflit()
        {
            DataTable dtMap = ((DataView)gridViewMap.DataSource).Table;
            //Lay ra cac row DANH_MUC co gia tri.
            DataRow[] lstRow = dtMap.Select("DANH_MUC <>''");
            DataTable dtResolve = CreatTableDMResovle();
            foreach (DataRow dr in lstRow)
            {
                string TenDm = dr["DANH_MUC"].ToString();
                string fieldTarget = dr["FIELDNAME"].ToString();
                DataTable dtTarget = ((DataView)gridviewDataTarget.DataSource).Table;
                
                for (int i = 0; i < dtTarget.Rows.Count;i++ )
                {
                    DataRow specRow = dtTarget.Rows[i];
                    if (specRow[fieldTarget].ToString() == "")
                    {
                        string valueSource = specRow[fieldTarget + "_SOURCE"].ToString();
                        Dictionary<string, string> likeValue = GetLikeName(TenDm, valueSource);
                        foreach (string key in likeValue.Keys)
                        {
                            DataRow row = dtResolve.NewRow();
                            row["TEN_DM"] = TenDm;
                            row["ORG_VALUE"] = valueSource;
                            row["ID_LIKE_VALUE"] = key;
                            string value;
                            likeValue.TryGetValue(key, out value);
                            row["LIKE_VALUE"] = value;
                            row["INDEX"] = i;
                            row["COLUMNFIELD"] = fieldTarget;
                            dtResolve.Rows.Add(row);
                        }
                    }
                }
            }
            return dtResolve;
        }
        private void tstripBarResolve_Click(object sender, EventArgs e)
        {
            CommitData();
            DataTable dt = ReCreateConflit();
            if (dt != null && dt.Rows.Count > 0)
            {
                //ProtocolForm.ShowDialog(this, new frmResolveDM(dt));
                ProcessData(dt);
            }
            else
                HelpMsgBox.ShowNotificationMessage("Không có xung đột để giải quyết");
        }

        private void frmImport_FormClosed(object sender, FormClosedEventArgs e)
        {
            ExcelSupport.filenamepath = "";
        }


      
    }
}