using System;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    /// <summary>Control cho phép chọn một giá trị từ table 
    /// nhưng chỉ lấy phần giá trị chứ ko lấy phần ID ( nên 
    /// tạm gọi là danh mục tham trị )
    /// - Cho phép thêm 1 mục mới nhấn Ctrl-Insert
    /// - Xóa 1 mục cũ bằng cách nhấn Ctrl-Delete
    /// Table chứa các danh mục tham trị này phải có cấu trúc như bên dưới
    /// Ví dụ
    ///|-------------|------------|
    ///|TEN_DANH_MUC |NOI_DUNG_DM |
    ///|-------------|------------|
    ///|   TDHV      |  Dai Hoc   |
    ///|-------------|------------|
    ///|   HTTT      |Chuyen khoan|
    ///|-------------|------------|
    /// </summary>    
    public partial class PLDMThamTri : DevExpress.XtraEditors.XtraUserControl
    {
        private string tableName;
        private string catalogName;

        #region Thuộc tính bắt buộc khởi tạo khi gọi _init
        public string TableName
        {
            set
            {
                tableName = value;
            }
        }
        public string CatalogName
        {
            set
            {
                catalogName = value;
            }
        }
        #endregion

        public PLDMThamTri()
        {
            InitializeComponent();
        }
        
        public PLDMThamTri(string TableName, string TenDanhMuc)
        {
            InitializeComponent();
            this.tableName = TableName;
            this.catalogName = TenDanhMuc;
        }

        private void comboBoxEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Insert | Keys.Control))
            {
                if (!Exist())
                {
                    if (InsertItem(this.tableName, this.catalogName, this.comboBoxEdit1.Text))
                        this.comboBoxEdit1.Properties.Items.Add(this.comboBoxEdit1.Text);
                }
            }
            else if (e.KeyData == (Keys.Delete | Keys.Control))
            {
                if (PLDMThamTri.DeleteItem(this.tableName, this.catalogName, this.comboBoxEdit1.Text))
                {
                    this.comboBoxEdit1.Properties.Items.Remove(this.comboBoxEdit1.Text);
                    comboBoxEdit1.Text = String.Empty;
                }
            }
        }
        private bool Exist()
        {
            foreach (String item in this.comboBoxEdit1.Properties.Items)
            {
                if (item.Equals(comboBoxEdit1.Text) || (comboBoxEdit1.Text.Trim().Equals(string.Empty)))
                    return true;
            }
            return false;
        }

        #region IPLControl Members

        public void _init()
        {
            DataTable dt = LoadItems(this.tableName, this.catalogName);

            comboBoxEdit1.Properties.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                comboBoxEdit1.Properties.Items.Add(dr[0].ToString());
            }
        }

        public void _refresh()
        {
            _init();
        }

        #endregion


        public void SetError(DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider, string errorName)
        {
            errorProvider.SetError(this.comboBoxEdit1, errorName);
        }        
        public static DataTable LoadItems(string tableName, string catalogName)
        {
            //DatabaseFB db = DABase.getDatabase();
            //DbCommand command = db.GetSQLStringCommand("SELECT NOI_DUNG_DM FROM " + tableName +
            //                                          " WHERE TEN_DANH_MUC = '"+ catalogName+ "'");
            //IDataReader reader = db.ExecuteReader(command);
            //DataTable tempTable = new DataTable("TEMPTABLE");
            //tempTable.Columns.Add("NOIDUNG");

            //while (reader.Read())
            //{
            //    DataRow dr = tempTable.NewRow();
            //    dr[0] = reader[0];
            //    tempTable.Rows.Add(dr);
            //}
            //reader.Close();
            //return tempTable;

            QueryBuilder query = new QueryBuilder("SELECT NOI_DUNG_DM FROM " + tableName + " WHERE 1=1");
            query.add("TEN_DANH_MUC", Operator.Equal, catalogName, DbType.String);
            DataSet ds = DABase.getDatabase().LoadDataSet(query, "TEMPTABLE");
            return ds.Tables[0];
        }
        public static bool DeleteItem(string tableName, string catalogName, string value)
        {
            try
            {
                DatabaseFB db = DABase.getDatabase();
                DbCommand dbDelete = db.GetSQLStringCommand("DELETE FROM " + tableName + " WHERE TEN_DANH_MUC = @TEN_DANH_MUC " +
                                                            " AND NOI_DUNG_DM = @NOI_DUNG_DM");
                db.AddInParameter(dbDelete, "@TEN_DANH_MUC", DbType.String, catalogName);
                db.AddInParameter(dbDelete, "@NOI_DUNG_DM", DbType.String, value);
                db.ExecuteNonQuery(dbDelete);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool InsertItem(string tableName, string catalogName, string value)
        {
            try
            {
                DatabaseFB db = DABase.getDatabase();
                DbCommand dbInsert = db.GetSQLStringCommand("INSERT INTO " + tableName + " VALUES (" +
                                                        "@TEN_DANH_MUC, @NOI_DUNG_DM)");

                db.AddInParameter(dbInsert, "@TEN_DANH_MUC", DbType.String, catalogName);
                db.AddInParameter(dbInsert, "@NOI_DUNG_DM", DbType.String, value);
                db.ExecuteNonQuery(dbInsert);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
