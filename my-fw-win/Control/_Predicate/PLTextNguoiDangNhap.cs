using System.Data;
using System.Drawing;
using DevExpress.XtraEditors;
namespace ProtocolVN.Framework.Win
{
    /// <summary>PREDICATE: Hiển thị thông tin người đăng nhập hiện tại
    /// </summary>
    public class PLTextNguoiDangNhap : DevExpress.XtraEditors.XtraUserControl
    {
        private TextEdit textEdit1;     //Display Value
        private long id;                 //Value

        #region Các thuộc tính bắt buộc
        public DataSet _DataSource;     //Nguồn dữ liệu
        public string _LinkField;       //Field liên kết
        public string _DisplayField;    //Field trình bày dữ liệu
        #endregion

        private System.ComponentModel.Container components = null;
        
        public override string Text
        {
            get { return _getValidateData()  ;  }            

        }

        public PLTextNguoiDangNhap()
        {
            InitializeComponent();
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
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // textEdit1
            // 
            this.textEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEdit1.Enabled = false;
            this.textEdit1.Location = new System.Drawing.Point(0, 0);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.ReadOnly = true;
            this.textEdit1.Size = new System.Drawing.Size(150, 20);
            this.textEdit1.TabIndex = 0;
            // 
            // txtNguoiDangNhap
            // 
            this.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.Controls.Add(this.textEdit1);
            this.Name = "txtNguoiDangNhap";
            this.Size = new System.Drawing.Size(150, 20);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

            this.textEdit1.Enabled = false;
            this.textEdit1.ForeColor = Color.Red;
        }
        #endregion

        public void _setNull(){
            this.textEdit1.ResetText();
        }
        #region IPLControl Members

        public void _init()
        {
            if(_DataSource==null){
                if (FrameworkParams.currentUser.fullname == null){
                    this.textEdit1.Text = FrameworkParams.currentUser.username;
                }
                else{
                    this.textEdit1.Text = FrameworkParams.currentUser.fullname;
                }
                this.id = FrameworkParams.currentUser.id;
            }
            else{
                //TODO PHUOC : Phước Quan trọng  
                //Dựa vào DataSource, LinkField và Display Field để => this.textEdit1.Text.
                
            }
        }

        public string _getValidateData()
        {
            return textEdit1.Text;
        }

        public void _refresh()
        {
            _init();
        }
        
        public long _getID(){
            return this.id;
        }        

        public bool _setValue(int id){
            User user = DAUser.Instance.findById(id);
            if (user != null)
            {
                this.id = id;
                if(user.fullname == null)
                    this.textEdit1.Text = user.username;
                else
                    this.textEdit1.Text = user.fullname;
                return true;
            }
            return false;
        }

        #endregion

        public void SetError(DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider, string errorName)
        {
            errorProvider.SetError(this.textEdit1, errorName);
        }
    }
}