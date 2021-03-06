using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ProtocolVN.Framework.Core;
namespace ProtocolVN.Framework.Win
{
    /// <summary> PREDICATE: PHUOCNC Cho phép chọn dạng combobox nhiều cột, đồng thời cho phép thêm nếu không tồn tại.
    /// Hiện tại không dùng, vì đã dùng XtraGridLookup của Dev ( nhưng cái này hiện tại không cho thêm phần tử mới )
    /// </summary>
    public partial class PLGridCombobox : DevExpress.XtraEditors.XtraUserControl
    {
        private string displayField;    //Tên field hiển thị khi chọn   
        private string valueField;      //Tên field lấy phần giá trị 
        private object selectedValue;   //Giá trị của mẫu tin người dùng chọn

        private DataSet ds;             //Dữ liệu của lưới
        private XtraForm frmUpdateCat;  //Form dùng để cập nhật dữ liệu trong danh mục, phải được wrapper trước 
        private int iSelecIndex;        //Ví trị để forcus lại
        bool isUpdate = false;          //Có cập nhật lưới từ form danh mục
        private string displayText;

        private DevExpress.XtraGrid.Views.Grid.PLGridView viewData;
        public PLDynamicGrid plGrid;
        public event EventHandler EditValueChanged;

        public PLGridCombobox()
        {
            InitializeComponent();
            this.btnDM.Visible = false;
            setKhongChon();
            this.EditText.Text = displayText;
        }

        #region Thuộc tính bắt buộc khởi tạo
        //1. Dữ liệu cho trình bày
        public DataSet DataSource
        {
            get { return ds; }
            set
            {
                this.ds = value;
                if (value == null) return;
                
                this.gridData.DataSource = ((DataSet)value).Tables[0] ;
                this.plGrid = new PLDynamicGrid(gridData, value, false);
                this.gridView1 = plGrid.gv;
                this.gridData.MainView = gridView1;
                this.gridView1.OptionsView.ShowFooter = false;
                this.gridView1.OptionsView.ShowGroupPanel = false;
                this.gridView1.DoubleClick+= new System.EventHandler(this.gridView1_DoubleClick);

                this.EditText.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                //PHUOCNC : Phước Chỉnh sửa lại chiều rộng của popUp cho phù hợp
            }
        }
        //2. Field Chua du lieu khi trinh bay
        public string DisplayField
        {
            get { return displayField; }
            set
            {
                displayField = value;
                gcDisplayMember.FieldName = displayField;
                gcDisplayMember.Caption = displayField;
            }
        }
        
        //3.Field se lay du lieu
        public string ValueField
        {
            get { return valueField; }
            set
            {
                valueField = value;
                gcValueMember.FieldName = valueField;
                gcValueMember.Caption = valueField;
            }
        }

        //4. Chọn các caption hiển thị
        private string[] _captions;
        public string[] Captions
        {
            set
            {
                _captions = value;
            }
        }

        //5. Chọn các field hiển thị
        public string[] VisibleFieldName
        {
            set
            {
                for (int i = 0; i < plGrid.gv.Columns.Count; i++)
                {
                    plGrid.ChangeProperty("Visible", false, i);
                }
                for (int i = 0; i < value.Length; i++)
                {
                    gridView1.Columns.ColumnByFieldName(value[i]).Visible = true;
                    gridView1.Columns.ColumnByFieldName(value[i]).Caption = _captions[i];
                }
            }
        }

        /// <summary> Khi muốn cho phép cập nhật dữ liệu trên lưới
        /// phải truyền vào FormCategory ( chú ý phải được wrapper trước khi truyền vào trong).
        /// </summary>
        public DevExpress.XtraEditors.XtraForm FormCategory
        {
            get
            {
                return frmUpdateCat;
            }
            set
            {
                frmUpdateCat = value;
                if (frmUpdateCat != null) this.btnDM.Visible = true;
            }
        }

        public string DisplayText
        {
            get { return displayText; }
        }

        #endregion
                
        private void setKhongChon()
        {
            iSelecIndex = -1;
            //displayText = "[Chọn giá trị]";
            displayText = GlobalConst.NULL_TEXT;
            selectedValue = "-1";
        }

        private string GetSelectedData()
        {
            if (gridView1.GetSelectedRows() != null){
                iSelecIndex = gridView1.GetSelectedRows()[0];
                DataTable dv = (DataTable)gridData.DataSource;
                displayText = dv.Rows[iSelecIndex][displayField].ToString();
                selectedValue = dv.Rows[iSelecIndex][valueField];
                return displayText;
            }
            else
            {
                setKhongChon();
                return "-1";
            }
        }

        public int _getSelectedID()
        {
            return HelpNumber.ParseInt32(GetSelectedData());
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            EditText.ClosePopup();
            EditText.Text = GetSelectedData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {            
            EditText.CancelPopup();
            setKhongChon();
            EditText.Text = displayText;
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            EditText.ClosePopup();
            EditText.Text = GetSelectedData();
        }
        
        private void btnDM_Click(object sender, EventArgs e)
        {
            if (frmUpdateCat != null)
            {
                //frmUpdateCat.TopMost = true;                
                frmUpdateCat.TopLevel = true;
                ProtocolForm.pl_wrapper(ref frmUpdateCat);
                frmUpdateCat.ShowDialog(this);
            }            
            isUpdate = true;
            EditText.Invalidate();
        }

        private void EditText_EditValueChanged_1(object sender, EventArgs e)
        {
            if (EditValueChanged != null)
            {
                EditValueChanged(sender, e);
            }
        }

        private void gridData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditText.ClosePopup();
                EditText.Text = GetSelectedData();
            }
            else if (e.KeyCode == Keys.Tab)
                btnSelect.Focus();
        }

        private void EditText_Popup(object sender, EventArgs e)
        {
            if (!isUpdate)
                gridView1.FocusedRowHandle = iSelecIndex;
            else
            {
                isUpdate = false;

                if (ds != null)
                {
                    int i = 0;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr[valueField] != null && dr[valueField].ToString().Equals(selectedValue.ToString()))
                        {
                            this.iSelecIndex = i;
                            if (dr[displayField] != null)
                                this.displayText = dr[displayField].ToString();
                            else
                                this.displayText = "";
                            this.selectedValue = dr[valueField];

                            EditText.Text = displayText;

                            break;
                        }
                        i++;
                    }
                }
                gridView1.FocusedRowHandle = iSelecIndex;
            }
        }

        public void SetError(DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider, string errorName)
        {
            errorProvider.SetError(this.EditText, errorName);
        }

        public void _setSelectedValue(object value)
        {
            if (ds != null)
            {
                int i = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr[valueField] != null && dr[valueField].ToString().Equals(value.ToString()))
                    {
                        this.selectedValue = value;
                        this.iSelecIndex = i;
                        if (dr[displayField] != null)
                            this.displayText = dr[displayField].ToString();
                        else
                            this.displayText = "";
                        this.selectedValue = dr[valueField];

                        EditText.Text = displayText;
                        if (plGrid != null) plGrid.gv.FocusedRowHandle = i;
                        break;
                    }
                    i++;
                }
            }
        }

        #region Không dùng những hàm này
        public DevExpress.XtraGrid.Views.Grid.PLGridView ViewData
        {
            get { return viewData; }
            set
            {
                viewData = value;
                if (viewData != null) gridData.MainView = viewData;
            }
        }
        public int[] InvisibleCol
        {
            set
            {
                SetInVisibleColumns(value);
            }
        }

        private void SetInVisibleColumns(int[] Columns)
        {
            for (int i = 0; i < Columns.Length; i++)
            {
                plGrid.ChangeProperty("Visible", false, Columns[i]);
            }
        }
        #endregion
    }
}
