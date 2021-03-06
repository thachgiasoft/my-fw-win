using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.FilterEditor;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using ProtocolVN.Framework.Win;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    //PHUOCNC:
    /*
     * - Khi nhấn Right-Click có menu cho phép chọn đối tượng để xem
     * - Phân quyền để mở cái đó lên nữa ( tạm thời dừng mức chỉ xem )
     * - Ẩn hiện các cột
     * - Khái niệm nguồn dữ liệu 
     * - Thông tin liên kết tìm kiếm giữa các đối tượng trong 1 nguồi dữ liệu     
     */

    /// <summary>
    /// Hiện tại lớp này ko còn được dùng nữa nó phải chỉnh sửa lại dể dùng cho 1 tình huống khác.
    /// </summary>
    public partial class PLFilter : XtraForm, ISaveQuery
    {
        FilterCase ObjFilter = null;

        public PLFilter()
        {
            InitializeComponent();
        }
        
        public PLFilter(FilterCase _objFilter)
        {
            this.ObjFilter = _objFilter;
            InitializeComponent();
            filterControl1.MenuManager = gridControl1.MenuManager;
            InitOptions();

            FilterControlHelp.InitCombobox(cbbSqlFilter1, ObjFilter);
            gridViewVN1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            try
            {
                string query = "";
                if (_objFilter.QUERYSOURCE.EndsWith("1=1"))
                {
                    query = _objFilter.QUERYSOURCE + " and 1=-1";
                }
                else
                {
                    query = _objFilter.QUERYSOURCE + " where 1=-1";
                }
                DataSet ds = DABase.getDatabase().LoadDataSet(query);
                gridControl1.DataSource = ds.Tables[0];
                for (int i = 0; i < ObjFilter.FILEDS.Length; i++)
                {
                    GridColumn column = gridViewVN1.Columns[ObjFilter.FILEDS[i]];
                    if(column!=null) column.Caption = ObjFilter.CAPTION[i];
                }

                for (int j = 0; j < gridViewVN1.Columns.Count; j++)
                {
                    if (ObjFilter.CheckFieldExist(gridViewVN1.Columns[j].FieldName) == false)
                        gridViewVN1.Columns[j].Visible = false;
                }

            }
            catch (Exception ex) { PLException.AddException(ex); }            
        }

        #region PHUOC REFACTOR
        

        private void SetFilterString(string data)
        {
            try {
                filterControl1.FilterString = data;
            }
            catch (Exception ex) { PLException.AddException(ex); }
        }

        private void btnLoadFilter1_Click(object sender, EventArgs e)
        {
            if (cbbSqlFilter1._getSelectedID() != -1)
            {
                SetFilterString(FilterControlHelp.GetQueryFromID(cbbSqlFilter1._getSelectedID()));
            }
        }

        private void btnXoaCauTruyVan_Click(object sender, EventArgs e)
        {
            if (FilterControlHelp.Delete(cbbSqlFilter1._getSelectedID()))
            {
                FilterControlHelp.InitCombobox(cbbSqlFilter1, ObjFilter);
            }
        }

        /// <summary>
        /// Tiến hành xử lý lại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbApply_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlWhere = FilterControlHelp.ConvertQueryFilterToWhereSql(filterControl1.FilterString);
                //PHUOCNC Khong dung co dinh vao Nhan Vien duoc
                DataSet ds = ObjFilter.DataSetFilterFromDatabase(sqlWhere);
                gridControl1.DataSource = ds.Tables[0];
            }
            catch (Exception ex) {
                PLException.AddException(ex);
            }
        }

        private void rsFilter1_Click(object sender, EventArgs e)
        {
            try { filterControl1.FilterString = gridViewVN1.ActiveFilterString; }
            catch { }
        }

        private void btnSave1_Click(object sender, EventArgs e)
        {
            ObjFilter.TITLE = filterControl1.FilterString.Trim(); //Mac dinh lay title chinh la query
            if (ObjFilter.TITLE.Length == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn điều kiện lọc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmTitleFilter frm = new frmTitleFilter(ObjFilter.TITLE, this);
            ProtocolForm.ShowModalDialog(this, frm);
            if (Luu)
            {
                string query = filterControl1.FilterString;
                if (query != string.Empty)
                    if (FilterControlHelp.Save(query, ObjFilter.USERID, ObjFilter.DATASETID, ObjFilter.TITLE))
                        FilterControlHelp.InitCombobox(cbbSqlFilter1, ObjFilter);
            }
        }
        #region 3.Danh sách hàm truyền dữ liệu hai form
        bool Luu = false;
        public void KhongLuu(bool b)
        {
            Luu = b;
        }
        public void getvalueFromChild(string s)
        {
            ObjFilter.TITLE = s;
        }
        #endregion
        #endregion

        //PHUOCNC: Nạp dữ liệu thuộc tính trình bày của FilterControl
        private void InitOptions()
        {
            initProperties = true;
            ceEmptyValue.Color = filterControl1.AppearanceEmptyValueColor;
            ceFieldName.Color = filterControl1.AppearanceFieldNameColor;
            ceGroupOperator.Color = filterControl1.AppearanceGroupOperatorColor;
            ceOperator.Color = filterControl1.AppearanceOperatorColor;
            ceValue.Color = filterControl1.AppearanceValueColor;
            seLevelIndent.Value = filterControl1.LevelIndent;
            seSeparatorHeight.Value = filterControl1.NodeSeparatorHeight;
            ceGroupCommandsIcon.Checked = filterControl1.ShowGroupCommandsIcon;
            ceOperandTypeIcon.Checked = filterControl1.ShowOperandTypeIcon;
            ceToolTips.Checked = filterControl1.ShowToolTips;
            initProperties = false;
        }
        
        #region Danh sách các sự kiện làm thay đổi thuộc tính của FilterControl
        bool initProperties = false;
        private void ceGroupCommandsIcon_CheckedChanged(object sender, EventArgs e)
        {
            if (initProperties) return;
            filterControl1.ShowGroupCommandsIcon = ceGroupCommandsIcon.Checked;
        }
        private void ceOperandTypeIcon_CheckedChanged(object sender, EventArgs e)
        {
            if (initProperties) return;
            filterControl1.ShowOperandTypeIcon = ceOperandTypeIcon.Checked;
        }
        private void ceToolTips_CheckedChanged(object sender, EventArgs e)
        {
            if (initProperties) return;
            filterControl1.ShowToolTips = ceToolTips.Checked;
        }
        private void seLevelIndent_EditValueChanged(object sender, EventArgs e)
        {
            if (initProperties) return;
            filterControl1.LevelIndent = Convert.ToInt32(seLevelIndent.Value);
        }
        private void seSeparatorHeight_EditValueChanged(object sender, EventArgs e)
        {
            if (initProperties) return;
            filterControl1.NodeSeparatorHeight = Convert.ToInt32(seSeparatorHeight.Value);
        }
        private void ceEmptyValue_EditValueChanged(object sender, System.EventArgs e)
        {
            if (initProperties) return;
            filterControl1.AppearanceEmptyValueColor = ceEmptyValue.Color;
        }
        private void ceFieldName_EditValueChanged(object sender, System.EventArgs e)
        {
            if (initProperties) return;
            filterControl1.AppearanceFieldNameColor = ceFieldName.Color;
        }
        private void ceGroupOperator_EditValueChanged(object sender, System.EventArgs e)
        {
            if (initProperties) return;
            filterControl1.AppearanceGroupOperatorColor = ceGroupOperator.Color;
        }
        private void ceOperator_EditValueChanged(object sender, System.EventArgs e)
        {
            if (initProperties) return;
            filterControl1.AppearanceOperatorColor = ceOperator.Color;
        }
        private void ceValue_EditValueChanged(object sender, System.EventArgs e)
        {
            if (initProperties) return;
            filterControl1.AppearanceValueColor = ceValue.Color;
        }
        #endregion

        #region Tab 2 Chưa xử lý - Chưa kiểm tra
        private void sbApplyFilter2_Click(object sender, EventArgs e)
        {
            //gridView2.ActiveFilterString = filterControl2.FilterString;
        }
        private void sbReset_Click(object sender, EventArgs e)
        {
            //ResetCustomFilter();
        }
        private void btnSave2_Click(object sender, EventArgs e)
        {
            //ObjFilter.TITLE = filterControl2.FilterString.Trim();
            //if (ObjFilter.TITLE.Length == 0)
            //{
            //    XtraMessageBox.Show("Vui lòng chọn điều kiện lọc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //frmTitileFilter frm = new frmTitileFilter(ObjFilter.TITLE, this);
            //ProtocolForm.ShowModalDialog(this, frm);
            //if (Luu)
            //{
            //    string sql = filterControl2.FilterString;
            //    if (sql.Trim() != string.Empty && cbbSqlFilter2.Properties.Items.Contains(sql) == false)
            //        cbbSqlFilter2.Properties.Items.Add(sql);

            //    this.ObjFilter.Save(query);
            //}
        }
        private void btnLoadFilter2_Click(object sender, EventArgs e)
        {
            //if (cbbSqlFilter2.SelectedIndex > 0)
            //    gridView2.ActiveFilterString = cbbSqlFilter2.Text;
            //else
            //    gridControl2.DataSource = "";// ObjFilter.DATA.Tables[0];
        }
        #endregion
    }
}