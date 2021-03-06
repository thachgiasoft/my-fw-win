using System.ComponentModel;
using System.Data;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
using System;
using ProtocolVN.Framework.Win;

namespace ProtocolVN.Framework.Win
{
    public class PLLookupEdit : XtraUserControl, ISelectionControl, IIDValidation
    {
        #region Biến
        public LookUpEdit _lookUpEdit;
        private string _ValueField;
        public bool _IgnoreCase = true;
        public string _TableName = null;
        public string[] _DisplayFields = null;
        #endregion

        #region Thuộc tính
        //[Browsable(true), Category("_PROTOCOL"), Description("Tên FIELD sẽ lấy dữ liệu khi chọn")]
        public string ValueField
        {
            get
            {
                return _ValueField;
            }
            set
            {
                _ValueField = value;
            }
        }
        public LookUpEdit MainCtrl
        {
            get
            {
                return _lookUpEdit;
            }
        }
        #endregion

        #region Khởi tạo        
        public PLLookupEdit()
        {
            InitializeComponent();
        }
        private void _init(DataSet ds, string DisplayField, string ValueField)
        {
           _init(ds,DisplayField,ValueField,true);
        }
        private void _init(DataSet ds, string DisplayField, string ValueField, bool AllowBlank)
        {
            DataTable dt = ds.Tables[0];
            _lookUpEdit.Properties.DataSource = dt;
            _lookUpEdit.Properties.DisplayMember = DisplayField;
            _lookUpEdit.Properties.ValueMember = ValueField;
            this._ValueField = ValueField;
            if (AllowBlank)
            {
                DataRow row = dt.NewRow();
                row[_lookUpEdit.Properties.ValueMember] = -1;
                row[_lookUpEdit.Properties.DisplayMember] = "";
                dt.Rows.InsertAt(row, 0);
            }
        }
        private void _init(DataSet ds, string[] DisplayFields, string ValueField)
        {
            DataTable dt = ds.Tables[0];
            this._DisplayFields = DisplayFields;
            _init(ds, HelpDataSetExt.AddNewField(dt, this._DisplayFields), ValueField);    
        }
        /// <summary>
        /// Cho phép khởi tạo control chọn lựa
        /// </summary>
        /// <param name="TableName">Tên table name chứa dữ liệu</param>
        /// <param name="DisplayField">Tên field sẽ lấy giá trị khi chọn (field có kiểu int)</param>
        /// <param name="ValueField">Giá trị khi chọn</param>
        /// <param name="NullText">Giá trị hiển thị khi không chọn</param>
        /// <param name="fieldNames">Danh sách các tên fields sẽ hiển thị trong lưới</param>
        /// <param name="titles">Danh sách các tiêu đề sẽ hiển thị trong lưới</param>
        public void _init(string TableName, string DisplayField, string ValueField, string NullText, string[] fieldNames, string[] titles, int[] widths)
        {
            updateLookUp(fieldNames, titles, NullText, widths);
            this._TableName = TableName;
            DataSet ds = DABase.getDatabase().LoadDataSet(HelpSQL.SelectAll(TableName, DisplayField, _IgnoreCase), TableName);
            _init(ds, DisplayField, ValueField);
        }
        public void _init(string TableName, string[] DisplayFields, string ValueField, string NullText, string[] fieldNames, string[] titles, int[] widths)
        {
            updateLookUp(fieldNames, titles, NullText, widths);
            this._TableName = TableName;
            DataSet ds = DABase.getDatabase().LoadDataSet(HelpSQL.SelectAll(TableName, DisplayFields), TableName);
            _init(ds, DisplayFields, ValueField);
        }
        public void _init(string TableName, string DisplayField, string ValueField, string NullText, string fieldNames, string titles, int widths)
        {
            updateLookUp(new string[] {fieldNames}, new string[]{titles}, NullText, new int[]{widths});
            this._TableName = TableName;
            DataSet ds = DABase.getDatabase().LoadDataSet(HelpSQL.SelectAll(TableName, DisplayField, _IgnoreCase), TableName);
            _init(ds, DisplayField, ValueField);
        }
        public void _init(string TableName, string[] DisplayFields, string ValueField, string NullText, string fieldName, string titles, int widths)
        {
            updateLookUp(new string[] { fieldName }, new string[] { titles }, NullText, new int[] { widths });
            this._TableName = TableName;
            DataSet ds = DABase.getDatabase().LoadDataSet(HelpSQL.SelectAll(TableName, DisplayFields), TableName);
            _init(ds, DisplayFields, ValueField);
        }

        /// <summary>
        /// Cho phép khởi tạo control chọn lựa
        /// </summary>
        /// <param name="dt">DataTable chứa dữ liệu hiển thị trong chọn lựa</param>
        /// <param name="DisplayField">Tên field sẽ lấy giá trị khi chọn (field có kiểu int)</param>
        /// <param name="ValueField">Tên field lấy giá trị dữ liệu</param>
        /// <param name="NullText">Giá trị hiển thị khi không chọn</param>
        /// <param name="fieldNames">Danh sách các tên fields sẽ hiển thị trong lưới</param>
        /// <param name="titles">Danh sách các tiêu đề sẽ hiển thị trong lưới</param>
        public void _init(DataTable dt, string DisplayField, string ValueField, string NullText, string[] fieldNames, string[] titles, int[] widths)
        {
            updateLookUp(fieldNames, titles, NullText, widths);
            _init(dt.DataSet, DisplayField, ValueField);
        }
        public void _init(DataTable dt, string DisplayField, string ValueField, string NullText, string[] fieldNames, string[] titles, int[] widths,bool AllowBlank)
        {
            updateLookUp(fieldNames, titles, NullText, widths);
            _init(dt.DataSet, DisplayField, ValueField,AllowBlank);
        }
        public void _init(DataTable dt, string[] DisplayFields, string ValueField, string NullText, string[] fieldNames, string[] titles, int[] widths)
        {
            updateLookUp(fieldNames, titles, NullText, widths);
            _init(dt.DataSet, DisplayFields, ValueField);
        }
        public void _init(DataTable dt, string DisplayField, string ValueField, string NullText, string fieldNames, string titles, int widths)
        {
            updateLookUp(new string[]{fieldNames}, new string[]{titles}, NullText, new int[]{widths});
            _init(dt.DataSet, DisplayField, ValueField);
        }
        public void _init(DataTable dt, string[] DisplayFields, string ValueField, string NullText, string fieldNames, string titles, int widths)
        {
            updateLookUp(new string[] { fieldNames }, new string[] { titles }, NullText, new int[] { widths });
            _init(dt.DataSet, DisplayFields, ValueField);
        }
        #endregion
        
        #region Component Designer generated code
        private System.ComponentModel.Container components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._lookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this._lookUpEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // _lookUpEdit
            // 
            this._lookUpEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lookUpEdit.Location = new System.Drawing.Point(0, 0);
            this._lookUpEdit.Name = "_lookUpEdit";
            this._lookUpEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this._lookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._lookUpEdit.Properties.ImmediatePopup = true;
            this._lookUpEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this._lookUpEdit.Size = new System.Drawing.Size(100, 20);
            this._lookUpEdit.TabIndex = 0;
            // 
            // PLLookupEdit
            // 
            this.Controls.Add(this._lookUpEdit);
            this.Name = "PLLookupEdit";
            this.Size = new System.Drawing.Size(100, 20);
            ((System.ComponentModel.ISupportInitialize)(this._lookUpEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private void updateLookUp(string[] fields, string[] titles, string NullText, int[] widths)
        {
            this.SuspendLayout();
            DevExpress.XtraEditors.Controls.LookUpColumnInfo[] cols = new DevExpress.XtraEditors.Controls.LookUpColumnInfo[titles.Length];
            int totalWidth = widths[0];
            cols[0] = new DevExpress.XtraEditors.Controls.LookUpColumnInfo(fields[0], titles[0], widths[0], DevExpress.Utils.FormatType.None,
                                    "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.Ascending);
            for (int i = 1; i < titles.Length; i++)
            {
                cols[i] = new DevExpress.XtraEditors.Controls.LookUpColumnInfo(fields[i], titles[i], widths[i], DevExpress.Utils.FormatType.None,
                                    "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None);
                totalWidth += widths[i];
            }
            this._lookUpEdit.Properties.Columns.Clear();
            this._lookUpEdit.Properties.Columns.AddRange(cols);
            this._lookUpEdit.Properties.NullText = NullText;
            this._lookUpEdit.Size = new System.Drawing.Size(totalWidth + 5, 30);
            this.ResumeLayout(false);
        }

        #region ISelectionControl Members

        public long _getSelectedID()
        {
            //object o = _lookUpEdit.Properties.GetDataSourceValue(_ValueField, _lookUpEdit.ItemIndex);

            //if (o == null) return -1;
            //return HelpNumber.ParseInt64(o);

            if (_lookUpEdit.EditValue != null)
            {
                if (HelpNumber.ParseInt64(_lookUpEdit.EditValue) != Int64.MinValue)
                    return HelpNumber.ParseInt64(_lookUpEdit.EditValue);
                else
                    return -1;
            }
            else return -1;
        }

        public void _setSelectedID(long id)
        {
            if (this._TableName != null)
            {
                if (((DataTable)_lookUpEdit.Properties.DataSource).Select(this._ValueField + "=" + id).Length == 0)
                {
                    DataRow row = ((DataTable)_lookUpEdit.Properties.DataSource).NewRow();
                    HelpDataSetExt.GetDataRow(ref row, this._TableName, this._ValueField, id, this._DisplayFields);
                    if (row != null) ((DataTable)_lookUpEdit.Properties.DataSource).Rows.Add(row);
                }
            }

            _lookUpEdit.EditValue = id;                

            //DataTable dt = (DataTable)_lookUpEdit.Properties.DataSource;
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    if (dt.Rows[i][0].ToString() == id.ToString())
            //    {                    
            //        _lookUpEdit.EditValue = id;
            //        _lookUpEdit.ItemIndex = i;
            //        break;
            //    }
            //}
        }

        public void _refresh(object _DataTable)
        {
            _lookUpEdit.Properties.DataSource = (DataTable)_DataTable;
            _lookUpEdit.Update();
        }

        #endregion

        #region IValidation Members

        public void SetError(DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider, string errorMsg)
        {
            errorProvider.SetError(this._lookUpEdit, errorMsg);
        }

        #endregion
    }
}