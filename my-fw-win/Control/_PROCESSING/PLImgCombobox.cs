using System;
using System.ComponentModel;
using System.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Control cho phép tạo một ImageCombobox từ DataTable
    /// Chú ý dữ liệu tại _ValueField phải là một con số nguyên
    /// </summary>
    public class PLImgCombobox : XtraUserControl, ISelectionControl, IIDValidation
    {
        #region Danh sách các biến
        private ImageComboBoxEdit _imgCombo;
        private string _DisplayField;
        private string _ValueField;
        private DataTable _DataSource;
        #endregion

        #region Danh sách các thuộc tính
        public DataTable DataSource
        {
            set
            {
                _DataSource = value;
            }
            get
            {
                return _DataSource;
            }
        }
        public ImageComboBoxEdit imgCombo
        {
            set
            {
                _imgCombo = value;
            }
            get
            {
                return _imgCombo;
            }
        }
        public string DisplayField
        {
            set
            {
                _DisplayField = value;
            }
            get
            {
                return _DisplayField;
            }
        }
        public string ValueField
        {
            set
            {
                _ValueField = value;
            }
            get
            {
                return _ValueField;
            }
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
            this._imgCombo = new DevExpress.XtraEditors.ImageComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this._imgCombo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // imgComboBox
            // 
            this._imgCombo.Dock = System.Windows.Forms.DockStyle.Fill;
            this._imgCombo.Location = new System.Drawing.Point(0, 0);
            this._imgCombo.Name = "imgComboBox";
            this._imgCombo.Properties.NullText = GlobalConst.NULL_TEXT;
            this._imgCombo.Properties.AppearanceDropDown.BackColor = System.Drawing.Color.White;
            this._imgCombo.Properties.AppearanceDropDown.BackColor2 = System.Drawing.Color.White;
            this._imgCombo.Properties.AppearanceDropDown.BorderColor = System.Drawing.Color.Black;
            this._imgCombo.Properties.AppearanceDropDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._imgCombo.Properties.AppearanceDropDown.Options.UseBackColor = true;
            this._imgCombo.Properties.AppearanceDropDown.Options.UseBorderColor = true;
            this._imgCombo.Properties.AppearanceDropDown.Options.UseForeColor = true;
            this._imgCombo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._imgCombo.Properties.ImmediatePopup = true;
            this._imgCombo.Size = new System.Drawing.Size(161, 20);
            this._imgCombo.TabIndex = 0;
            this._imgCombo.SelectedIndexChanged += new System.EventHandler(this.imgComboBox_SelectedIndexChanged);
            // 
            // PLCombobox
            // 
            this.Controls.Add(this._imgCombo);
            this.Name = "PLCombobox";
            this.Size = new System.Drawing.Size(161, 20);
            ((System.ComponentModel.ISupportInitialize)(this._imgCombo.Properties)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        public PLImgCombobox()
        {
            InitializeComponent();
        }

        #region Hàm khởi tạo Control
        public bool _IgnoreCase = true;
        /// <summary>Predicate: Phải khởi tạo DataSource, DisplayField, ValueField trước khi gọi
        /// </summary>
        public void _init()
        {
            _imgCombo.Properties.Items.Clear();
            _imgCombo.Properties.Items.Add(new ImageComboBoxItem(GlobalConst.NULL_TEXT, "-1"));
            if (_DataSource != null)
            {
                for (int i = 0; i < _DataSource.Rows.Count; i++)
                {
                    ImageComboBoxItem item = new ImageComboBoxItem();
                    item.Value = HelpNumber.ParseInt64(_DataSource.Rows[i][_ValueField]);
                    item.Description = _DataSource.Rows[i][_DisplayField].ToString();
                    _imgCombo.Properties.Items.Add(item);
                }
            }
            this._imgCombo.EditValue = "-1";
        }
        /// <summary>Predicate: Phải khởi tạo DisplayField, ValueField
        /// </summary>
        public void _init(string TableName)
        {
            //DataSet ds = DABase.getDatabase().LoadTable(TableName);
            DataSet ds = DABase.getDatabase().LoadDataSet(HelpSQL.SelectAll(TableName, _DisplayField, _IgnoreCase), TableName);
            this._DataSource = ds.Tables[0];
            _init();
        }

        public void _init(DataTable Src, string DisplayFN, string ValueFN)
        {
            this._DataSource = Src;
            this._DisplayField = DisplayFN;
            this._ValueField = ValueFN;
            _init();
        }
        
        public void _init(string TableName, string DisplayFN, string ValueFN)
        {
            //DataSet ds = DABase.getDatabase().LoadTable(TableName);
            DataSet ds = DABase.getDatabase().LoadDataSet(HelpSQL.SelectAll(TableName, DisplayFN, _IgnoreCase), TableName);
            this._DataSource = ds.Tables[0];
            this._DisplayField = DisplayFN;
            this._ValueField = ValueFN;
            _init();
        }

        #endregion

        #region IValidation Members
        public void SetError(DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider, string errorMsg)
        {
            errorProvider.SetError(this._imgCombo, errorMsg);
        }
        #endregion

        #region ISelectionControl Members
        public long _getSelectedID()
        {
            return HelpNumber.ParseInt64(this._imgCombo.EditValue);
        }
        public void _setSelectedID(long id)
        {
            if (id > -1) this._imgCombo.EditValue = id;
            else this._imgCombo.EditValue = "-1";
        }

        /// <summary>Làm tươi control
        /// </summary>
        /// <param name="NewSrc">Phải là 1 DataTable</param>
        public void _refresh(object NewSrc)
        {
            this._DataSource = (DataTable)NewSrc;
            _init();
        }
        #endregion

        #region Đưa sự kiện ra ngoài
        public event EventHandler SelectedIndexChanged = null;
        private void imgComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null)
            {
                SelectedIndexChanged(sender, e);
            }
        }
        #endregion
    }
}