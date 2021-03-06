using System.ComponentModel;
using System.Data;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public class TrialPLListSelect : DevExpress.XtraEditors.XtraUserControl
    {
        public LookUpEdit lookUpEdit1;

        private string _ValueField;
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
        private System.ComponentModel.Container components = null;

        public TrialPLListSelect()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
            // TODO: Add any initialization after the InitForm call
        }

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

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lookUpEdit1.Location = new System.Drawing.Point(0, 0);
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit1.Properties.ImmediatePopup = true;
            this.lookUpEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lookUpEdit1.Size = new System.Drawing.Size(150, 20);
            this.lookUpEdit1.TabIndex = 0;
            // 
            // PLListSelect
            // 
            this.Controls.Add(this.lookUpEdit1);
            this.Name = "PLListSelect";
            this.Size = new System.Drawing.Size(150, 20);
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        public override string Text
        {
            get
            {
                return "" + this._getSelectedID();
            }
        }

        private void updateLookUp(string DisplayField, string Caption, string NullText, int width)
        {
            this.SuspendLayout();
            this.lookUpEdit1.Properties.NullText = NullText;
            this.lookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(DisplayField, Caption, width, 
                    DevExpress.Utils.FormatType.None, "", true, 
                    DevExpress.Utils.HorzAlignment.Default, 
                    DevExpress.Data.ColumnSortOrder.Ascending)});
            this.ResumeLayout(false);
        }


        public void _init(string TableName, string DisplayField, string Caption, string ValueField, string NullText, int width)
        {
            updateLookUp(DisplayField, Caption, NullText, width);
            DataSet ds = DABase.getDatabase().LoadTable(TableName);
            lookUpEdit1.Properties.DataSource = ds.Tables[0];
            lookUpEdit1.Properties.DisplayMember = DisplayField;
            this._ValueField = ValueField;
        }

        public void _init(string TableName, string DisplayField, string Caption, string ValueField, string NullText)
        {
            _init(TableName, DisplayField, Caption, ValueField, NullText, 30);
        }

        public void _init(DataTable dt, string DisplayField, string Caption, string ValueField, string NullText, int width)
        {
            updateLookUp(DisplayField, Caption, NullText, width);
            lookUpEdit1.Properties.DataSource = dt;
            lookUpEdit1.Properties.DisplayMember = DisplayField;
            this._ValueField = ValueField;
        }

        public void _init(DataTable dt, string DisplayField, string Caption, string ValueField, string NullText)
        {
            _init(dt, DisplayField, Caption, ValueField, NullText, 30);
        }

        public long _getSelectedID()
        {
            object o = lookUpEdit1.Properties.GetDataSourceValue(_ValueField, lookUpEdit1.ItemIndex);

            if (o == null) return -1;
            return (int)o;
        }

        public void _setSelectedID(long id)
        {
            DataTable dt = (DataTable)lookUpEdit1.Properties.DataSource;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() == id.ToString()){
                    lookUpEdit1.ItemIndex = i;
                    break;
                }
            }                           
        }                                                                                   

        public void _refresh(string TableName)
        {
            DataSet ds = DABase.getDatabase().LoadTable(TableName);
            lookUpEdit1.Properties.DataSource = ds.Tables[0];           
        }

        public void _refresh(DataTable dt){
            lookUpEdit1.Properties.DataSource = dt;
        }

        public void SetError(DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider, string errorName)
        {
            errorProvider.SetError(this.lookUpEdit1, errorName);
        }

    }
}