using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections.Generic;
using ProtocolVN.Framework.Core;
using ProtocolVN.Framework.Win;


namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// TODO PHUOC : Phụng sẽ thực hiện
    /// </summary>
    public class TrialPLListSelectUpdate : DevExpress.XtraEditors.XtraUserControl
    {
        public LookUpEdit lookUpEdit1;

        private DataSet ds;
        private string[] captions;
        private string ValueField;
        private System.ComponentModel.Container components = null;

        public TrialPLListSelectUpdate()
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
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.lookUpEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lookUpEdit1.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.lookUpEdit1_Properties_ButtonClick);
            this.lookUpEdit1.Size = new System.Drawing.Size(150, 20);
            this.lookUpEdit1.TabIndex = 0;

            // 
            // PLListSelectUpdate
            // 
            this.Controls.Add(this.lookUpEdit1);
            this.Name = "PLListSelectUpdate";
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

        
        private void updateLookUp(string DisplayField, string Caption, string NullText){
            this.SuspendLayout();
            this.lookUpEdit1.Properties.NullText = NullText;
            this.lookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(DisplayField, Caption, 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.ResumeLayout(false);
        }

        public void _init(string TableName, string DisplayField, string Caption, string ValueField, string NullText, string[] captions )
        {
            updateLookUp(DisplayField, Caption, NullText);
            this.ds = DABase.getDatabase().LoadTable(TableName);
            lookUpEdit1.Properties.DataSource = ds.Tables[0];
            lookUpEdit1.Properties.DisplayMember = DisplayField;
            this.ValueField = ValueField;
            this.captions = captions;
        }

        public void _init(DataTable dt, string DisplayField, string Caption, string ValueField, string NullText, string[] captions)
        {
            updateLookUp(DisplayField, Caption, NullText);
            lookUpEdit1.Properties.DataSource = dt;
            lookUpEdit1.Properties.DisplayMember = DisplayField;
            this.ValueField = ValueField;
            this.captions = captions;
        }

        public long _getSelectedID()
        {
            object o = lookUpEdit1.Properties.GetDataSourceValue(ValueField, lookUpEdit1.ItemIndex);

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
            this.ds = DABase.getDatabase().LoadTable(TableName);
            lookUpEdit1.Properties.DataSource = ds.Tables[0];           
        }

        public void _refresh(DataTable dt){
            lookUpEdit1.Properties.DataSource = dt;
        }

        private void lookUpEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind.Equals(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)){
                XtraForm form = new TrialfrmSimpleCategory(this.ds, this.captions);
                form.ShowDialog();
            }
            
        }
        public void SetError(DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider, string errorName)
        {
            errorProvider.SetError(this.lookUpEdit1, errorName);
        }

    }
}