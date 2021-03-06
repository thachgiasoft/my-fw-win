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
namespace ProtocolVN.Plugin.OldImpExp
{
    public partial class frmOldImport : XtraForm
    {        
        private DataTable tableMap;
        private string ObjectName="";
        private Dictionary<string, string> lstFieldGen;
        private DatabaseFB db;
        public frmOldImport()
        {
            InitializeComponent();
            lstFieldGen = new Dictionary<string, string>();
            db = DABase.getDatabase();
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
            catch
            {
                
            }
            return "";
        }
      
        private void InsertData(DataSet ds)
        {
            if (ds == null) return;
            gridviewDataSource.Columns.Clear();
            gridDataSource.DataSource = ds.Tables[0];
            DisplayGridMap();
        }
        #endregion

        #region "Create Mapping"
      
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
        private DataTable CreateTableMap()
        {
            DataTable table;
            table = new DataTable();
            table.Columns.Add("Cột đích",Type.GetType("System.String"));
            table.Columns.Add("Dữ liệu nguồn",Type.GetType("System.String"));            
            return table;
        }
        private void DisplayGridMap()
        {
            ClearText();
            gridViewMap.Columns[1].ColumnEdit = CreateCombobox();                        
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
      
        private bool MoveData()
        {
            try
            {
                DataTable dtSource = ((DataView)gridviewDataSource.DataSource).Table;
                DataTable dtMap = ((DataView)gridViewMap.DataSource).Table;
                DataTable dtNewAddTarget = ((DataView)gridviewDataTarget.DataSource).Table;
                int rowUpdate = 0;
                foreach (int i in gridviewDataSource.GetSelectedRows())
                {
                    DataRow newRowTarget = dtNewAddTarget.NewRow();
                    DataRow rowSource = gridviewDataSource.GetDataRow(i);
                    bool insSuccessful = false;
                    foreach (DataRow rowMap in dtMap.Rows)
                    {
                        string fieldTarget = rowMap[0].ToString();
                        string fieldSource = rowMap[1].ToString();
                        if (CheckField(dtSource, fieldSource))
                            try { newRowTarget[fieldTarget] = rowSource[fieldSource]; insSuccessful = true; }
                            catch { }
                        else
                            AddList(fieldTarget, fieldSource);
                    }
                    if (insSuccessful)
                    {
                        rowSource.Delete();
                        rowUpdate++;
                        dtNewAddTarget.Rows.Add(newRowTarget);
                    }
                }
                gridDataTarget.DataSource = dtNewAddTarget;
                ClearRowBlank(dtNewAddTarget);
                HelpMsgBox.ShowNotificationMessage("Số dòng dữ liệu chèn vào thành công: " + (rowUpdate).ToString());
                return true;
            }
            catch { return false; }
            return false;
        }

        private void ClearText()
        {
            for (int i = 0; i < gridViewMap.RowCount; i++)
                gridViewMap.SetRowCellValue(i, gridViewMap.Columns[1], "");
        }
        private bool MoveAllData()
        {
            try
            {
                DataTable dtSource = ((DataView)gridviewDataSource.DataSource).Table;
                if (dtSource != null)
                {
                    DataTable dtMap = ((DataView)gridViewMap.DataSource).Table;
                    DataTable dtNewAddTarget = ((DataView)gridviewDataTarget.DataSource).Table;
                    //startIndexAdd = dtNewAddTarget.Rows.Count;
                    int rowUpdate = 0;
                    for (int i = 0; i < dtSource.Rows.Count; i++)
                    {
                        DataRow rowSource = dtSource.Rows[i];
                        DataRow newRowTarget = dtNewAddTarget.NewRow();
                        bool insSuccessful = false;
                        foreach (DataRow rowMap in dtMap.Rows)
                        {
                            string fieldTarget = rowMap[0].ToString();
                            string fieldSource = rowMap[1].ToString();
                            if (CheckField(dtSource, fieldSource))
                                try { newRowTarget[fieldTarget] = rowSource[fieldSource]; insSuccessful = true; }
                                catch { }
                            else
                                AddList(fieldTarget, fieldSource);
                        }
                        if (insSuccessful)
                        {
                            dtNewAddTarget.Rows.Add(newRowTarget);
                            rowSource.Delete();
                            rowUpdate++;
                        }
                    }
                    gridDataTarget.DataSource = dtNewAddTarget;
                    ClearRowBlank(dtNewAddTarget);
                    HelpMsgBox.ShowNotificationMessage("Số dòng dữ liệu chèn vào thành công: " + (rowUpdate).ToString());
                    return true;
                }
            }
            catch { return false; }
            return false;
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
            ExcelSupport.filenamepath = OpenFileName();
            AddSheet(ExcelSupport.GetSheetNames(ExcelSupport.filenamepath));
            
        }      
        private void Form1_Load(object sender, EventArgs e)
        {
            CreateListTable();
            tableMap = CreateTableMap();
            gridMap.DataSource = tableMap;
            gridViewMap.Columns[0].OptionsColumn.AllowFocus = false;
            gridViewMap.Columns[0].OptionsColumn.AllowEdit = false;            
        }

        private void menuInsertData_Click(object sender, EventArgs e)
        {            
            MoveData();            
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
        private void comboListTable_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if (comboListTable.SelectedIndex < 0) return;
            if (ObjectName == comboListTable.Text.Trim()) return;
            if (ObjectName != "" && ObjectName != comboListTable.Text.Trim() 
                && ((DataTable)gridDataTarget.DataSource).Rows.Count>0)
            {
                if (PLMessageBox.ShowConfirmMessage("Bạn thay đổi đối tượng làm việc, dữ liệu trên bảng sẽ mất hết. Bạn có chắc chắn hay không ?") != DialogResult.Yes)
                    return;
            }            
            ObjectName = comboListTable.Text.Trim();            
            DataTable tb = LoadDataSetTarget(comboListTable.Text.Trim()).Tables[0];
            gridviewDataTarget.Columns.Clear();
            gridDataTarget.DataSource = tb;            
            tableMap.Clear();
            for (int i = 0; i < tb.Columns.Count; i++)
            {
                DataRow dr = tableMap.NewRow();
                dr[0] = tb.Columns[i].Caption;
                tableMap.Rows.Add(dr);
                dr.EndEdit();
                RepositoryItemComboBox combo = CreateCombobox();
                //if (combo.Items.IndexOf(dr[0].ToString()) >= 0)
                {
                    dr[1] = dr[0];
                }
            }
            MappingAuto();
        }
       
        private void menuClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnInsertAllData_Click(object sender, EventArgs e)
        {
            MoveAllData();
        }
        private void gridViewMap_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            tableMap.Rows[e.RowHandle][1] = e.Value.ToString();
        }
        #endregion
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
                comboListTable.Items.Add(row[0].ToString());
        }
        // ham dung de loat dataset target, lay table duoc chon tren combobox
        
        private void btnSaveData_Click(object sender, EventArgs e)
        {
            WaitingMsg.LongProcess(UpdateData);
        }
        private void cbSelSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sheetName = cbSelSheet.Text;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ExcelSupport.open();
            DB xuly = new DB();
            xuly._DataSet = ExcelSupport.dataSet(sheetName);
            gridviewDataSource.Columns.Clear();
            ds = xuly.xulydataset();
            ExcelSupport.close();
            if ((ds.Tables[0].Columns.Count == 1) && (ds.Tables[0].Rows.Count >= 2))
                FilterRow(ref ds);
            InsertData(ds);
            MappingAuto();
        }

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
                if (db.UpdateTable(ds)!=-1)
                {
                    ds.Tables[0].AcceptChanges();
                    HelpMsgBox.ShowNotificationMessage("Dữ liệu được cập nhật thành công");
                }
                else
                    PLMessageBox.ShowErrorMessage("Thêm dữ liệu không thành công");
            }
            catch(Exception e) {
                PLException.AddException(e);
                PLMessageBox.ShowErrorMessage("Thêm dữ liệu không thành công"); }
        }
        public void FilterRow(ref DataSet ds)
        {
            int indexrow = 0;
            while (indexrow < ds.Tables[0].Rows.Count)
            {
                DataRow row = ds.Tables[0].Rows[indexrow];
                int i = indexrow + 1;
                while (i < ds.Tables[0].Rows.Count)
                {
                    if (ds.Tables[0].Rows[i][0].Equals(row[0]))
                    {
                        ds.Tables[0].Rows.RemoveAt(i);
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
        private void AddSheet(List<string> lstSheet)
        {
            if (lstSheet.Count > 0)
            {
                cbSelSheet.Items.Clear();
                foreach (string sheet in lstSheet)
                    cbSelSheet.Items.Add(sheet);
                cbSelSheet.Visible = true;
            }
            else
                cbSelSheet.Visible = false;
        }
        private void MappingAuto()
        {
            for (int i = 0; i < tableMap.Rows.Count; i++)
            {
                RepositoryItemComboBox combo = CreateCombobox();
                tableMap.Rows[i][1] = "";
                //if (combo.Items.IndexOf(tableMap.Rows[i][0].ToString()) >= 0)
                {
                    tableMap.Rows[i][1] = tableMap.Rows[i][0];
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
                comboBox.Items.Add(gen);
            }
        }
        private DataSet LoadDataSetTarget(string tablename)
        {
            return DABase.getDatabase().LoadTable(tablename);
        }              
    }
}