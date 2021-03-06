using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Win;
using ProtocolVN.Framework.Core;
using DevExpress.XtraEditors.DXErrorProvider;

namespace ProtocolVN.Framework.Win
{
    public partial class PLMoneyType : DevExpress.XtraEditors.XtraUserControl
    {
        private SelectedIndexChanged SelectedIndex_Handle;
        public delegate void SelectedIndexChanged();
        bool? IsAdd = null;
        public PLMoneyType()
        {
            InitializeComponent();
        }
        #region Khởi tạo
        public void _init(bool? IsAdd)
        {
            this.IsAdd = IsAdd;        
            ChonNgoaiTe(PLNgoaiTe,IsAdd);
        }

        public void _init(SelectedIndexChanged SelectedIndex_Handle, bool? IsAdd)
        {
            this.IsAdd = IsAdd; 
            this.SelectedIndex_Handle = SelectedIndex_Handle;
            ChonNgoaiTe(PLNgoaiTe,IsAdd);
        }
        
        public void _init(string tableNgoaiTe, string valuefieldNT, string displayNT, string tableTiGia,string displayTiGia,string fieldnameNT_ID,string fielname_visible_bit) 
        {
            string str = @"select tt." + valuefieldNT +@", tt." + displayNT+@", tg." + displayTiGia + @",tg.visible_bit
                           from "+tableNgoaiTe + @" tt
                                left join "+tableTiGia + @" tg on tt."+valuefieldNT + @"=tg." +fieldnameNT_ID + @"
                           where  tg.ngay_cap_nhat= (select max(tig.ngay_cap_nhat) from "+tableTiGia + @" tig
                                        where tig." +fieldnameNT_ID + @"= tg."+fieldnameNT_ID + @" and tig."+ fielname_visible_bit+ @"='Y')
                                    or (tt." +displayNT + @"='VND') 
                           order by lower(tt."+displayNT+ @")";
            DataSet ds = DABase.getDatabase().LoadDataSet(str);
            PLNgoaiTe.DataSource = ds.Tables[0];
            PLNgoaiTe.DisplayField = displayNT;
            PLNgoaiTe.ValueField = valuefieldNT;
            PLNgoaiTe._init();

        }
        #endregion

        #region Get thông tin
        public long _getSelectedTienTeID()
        {
            try
            {
                return PLNgoaiTe._getSelectedID();
            }
            catch { return -1; }
        }
        public decimal _getSelectedTiGia()
        {
            try
            {
                if (PLNgoaiTe.DataSource.Rows[PLNgoaiTe.imgCombo.ItemIndex]["ALLOW_EDIT_BIT"].ToString() == "N")
                    return HelpNumber.ParseDecimal(PLNgoaiTe.DataSource.Rows[PLNgoaiTe.imgCombo.ItemIndex]["TI_GIA"]);
                else
                    return HelpNumber.ParseDecimal(TiGia.EditValue);                
            }
            catch { return 0; }
        }
        #endregion

        #region Set thông tin
        public void _setSelectedTienTeID(long ID)
        {
            PLNgoaiTe._setSelectedID(ID);
        }
        public void _setSelectedTiGia(decimal value)
        {
            TiGia.Value = value;
        }
        public void _setValue(long tienTeID, decimal tiGia)
        {
            PLNgoaiTe._setSelectedID(tienTeID);
            TiGia.Value = tiGia;
        }
        /// <summary>
        /// set lại datasource cho bảng ngoại tệ khi có sự thay đổi về tỉ giá 
        /// </summary>
        /// <param name="dt">dt là 1 datatable lấy dữ liệu từ store GET_TI_GIA_NGOAI_TE </param>
       
        public void _refresh(DataTable dt)
        {
            PLNgoaiTe._refresh(dt);
        }

        #endregion

        #region Xử lý
        private void ChonNgoaiTe(PLCombobox Input, bool? IsAdd)
        {
            string str="";
            if(IsAdd!=true)
                 str= "select * from GET_TI_GIA_NGOAI_TE"; // goi store procedure 
            else str = "select * from GET_TI_GIA_NGOAI_TE where visible_bit='Y'";
            DataSet ds = DABase.getDatabase().LoadDataSet(str);
            Input.DataSource = ds.Tables[0];
            Input.DisplayField = "NAME";
            Input.ValueField = "ID";
            Input._init();
        }
        private void PLNgoaiTe_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(PLNgoaiTe.DataSource.Rows[PLNgoaiTe.imgCombo.ItemIndex]["NAME"]) == "VND")
                    TiGia.Value = 1;
                else TiGia.Value = HelpNumber.ParseDecimal(PLNgoaiTe.DataSource.Rows[PLNgoaiTe.imgCombo.ItemIndex]["TI_GIA"]);
                if (Convert.ToString(PLNgoaiTe.DataSource.Rows[PLNgoaiTe.imgCombo.ItemIndex]["NAME"]) == "VND")
                {
                    TiGia.Visible = false;
                    btnUpdateTi_Gia.Visible = false;
                    PLNgoaiTe.Dock = DockStyle.Fill;
                }
                else
                {
                    TiGia.Visible = true;
                    btnUpdateTi_Gia.Visible = true;
                    PLNgoaiTe.Dock = DockStyle.None;
                }
                if (Convert.ToString(PLNgoaiTe.DataSource.Rows[PLNgoaiTe.imgCombo.ItemIndex]["ALLOW_EDIT_BIT"]) == "Y")
                    TiGia.Properties.ReadOnly = false;
                else TiGia.Properties.ReadOnly = true;

                SelectedIndex_Handle();  
            }
            catch { }            
            
        }
        private void btnUpdateTi_Gia_Click(object sender, EventArgs e)
        {
            frmCapNhatTiGia frm = new frmCapNhatTiGia();
            frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
            ProtocolForm.ShowModalDialog((XtraForm)this.Parent, frm) ;
        }

        void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _init(this.SelectedIndex_Handle,IsAdd);
            if (!TiGia.Properties.ReadOnly || (PLNgoaiTe.imgCombo.ItemIndex>=0 &&TiGia.Properties.ReadOnly && 
                Convert.ToString(PLNgoaiTe.DataSource.Rows[PLNgoaiTe.imgCombo.ItemIndex]["ALLOW_EDIT_BIT"]) == "N"))
                TiGia.Value = HelpNumber.ParseDecimal(PLNgoaiTe.DataSource.Rows[PLNgoaiTe.imgCombo.ItemIndex]["TI_GIA"]);
        }

        #region Kiểm tra dữ liệu
        public void SetError(DXErrorProvider errorProvider, string errorName)
        {
            this.PLNgoaiTe.SetError(errorProvider, errorName);
        }
        #endregion       
        private void TiGia_EditValueChanged(object sender, EventArgs e)
        {
            if (HelpNumber.ParseDecimal(TiGia.Value) < 0)
                TiGia.EditValue = TiGia.Value * (-1);           
        }
        #endregion
    }
}
