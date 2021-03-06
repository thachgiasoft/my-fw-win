using System;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    /// <summary>PREDICATE: Control này cho phép lấy dữ liệu ngày hiện tại tại database server.
    /// </summary>
    public class PLDateCurrentServerTime : DevExpress.XtraEditors.XtraUserControl
    {
        private DateEdit dateEdit1;

        private System.ComponentModel.Container components = null;

        public PLDateCurrentServerTime()
        {
            InitializeComponent();
        }

        
        /// <summary>
        /// Lấy thông tin ngày trong control.
        /// </summary>
        /// <returns></returns>
        public DateTime _getDate()
        {
            return (DateTime)this.dateEdit1.EditValue;
        }

        public void _setDate(DateTime t)
        {
            this.dateEdit1.DateTime = t;
        }

        public void _setNull()
        {
            this.dateEdit1.ResetText();
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
            this.dateEdit1 = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dateEdit1
            // 
            this.dateEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateEdit1.EditValue = null;
            this.dateEdit1.Location = new System.Drawing.Point(0, 0);
            this.dateEdit1.Name = "dateEdit1";
            this.dateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Properties.ReadOnly = true;
            this.dateEdit1.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit1.Size = new System.Drawing.Size(150, 20);
            this.dateEdit1.TabIndex = 0;
            // 
            // txtNgayHeThong
            // 
            this.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.Controls.Add(this.dateEdit1);
            this.Name = "txtNgayHeThong";
            this.Size = new System.Drawing.Size(150, 20);
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        #region IPLControl Members

        /// <summary>
        /// Đặt ngày hiện hành tại DB vào Control.
        /// </summary>
        public void _init()
        {
            this.dateEdit1.EditValue = DABase.getDatabase().GetSystemCurrentDateTime();
        }

        public void _refresh()
        {
            this._init();
        }

        public string _getValidateData()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}