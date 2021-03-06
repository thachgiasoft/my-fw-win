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
using DevExpress.XtraGrid;

namespace ProtocolVN.Framework.Win
{
    public interface ISaveQuery
    {
        void KhongLuu(bool b);
        void getvalueFromChild(string s);
    }
    
    public partial class SaveQueryDialog : XtraForm, ISaveQuery, IPublicForm
    {
        public FilterCase ObjFilter = null;
        public GridControl gridControlQL = null;
        public delegate object HookAfterExecAdvQuery(DataSet dataSet);
        public HookAfterExecAdvQuery hook;

        public SaveQueryDialog(FilterCase _objFilter, GridControl gridControlQL)
        {
            InitializeComponent();

            this.ObjFilter = _objFilter;
            this.splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel1;
            if (gridControlQL.DataSource == null)
            {
                //String query = _objFilter.QUERYSOURCE;
                //if (!query.EndsWith("1=1"))
                //    query = query + " where 1=-1";
                //else
                //    query = query + " and 1=-1";

                string query = "select * from (" + _objFilter.QUERYSOURCE + ") SAVE_QUERY where 1 = -1";
                gridControlQL.DataSource = HelpDB.getDatabase().LoadDataSet(query).Tables[0];
            }
            this.gridControlQL = gridControlQL;
            filterControl1.SourceControl = gridControlQL;
            filterControl1.MenuManager = gridControlQL.MenuManager;
            InitOptions();
            FilterControlHelp.InitCombobox(cbbSqlFilter1, ObjFilter);
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

        private void sbApply_Click(object sender, EventArgs e)
        {
            try
            {
                //Dừng Firebird Query
                FilterControlHelper fch = new FirebirdFilterToSQLStatement(filterControl1);
                SQLDATA sqlData = fch.GetSQLFilter(true);
                DataSet ds = ObjFilter.DataSetFilterFromDatabase(sqlData);
                if (hook != null)
                {
                    hook(ds);
                }
                gridControlQL.DataSource = ds.Tables[0];
            }
            catch (Exception ex) {
                PLException.AddException(ex);
            }
        }

        private void rsFilter1_Click(object sender, EventArgs e)
        {
            filterControl1.FilterString = null;
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
    }
}