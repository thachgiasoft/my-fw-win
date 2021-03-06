using System;
using DevExpress.XtraTreeList.Nodes;
using ProtocolVN.Framework.Core;
using DevExpress.XtraTreeList;
using System.Windows.Forms;
namespace ProtocolVN.Framework.Win
{
    sealed public partial class PLDMTreeGroup : DevExpress.XtraEditors.XtraUserControl, IIDValidation
    {
        private string ValueField;
        private bool isActive = false;
        private long selectId = -1;
        private System.EventHandler text;
        public PLDMTreeGroup()
        {
            InitializeComponent();
            this.popupContainerControl1.PopupContainerProperties.CloseOnOuterMouseClick = false;            
            this.popupContainerEdit1.CloseUp += new DevExpress.XtraEditors.Controls.CloseUpEventHandler(popupContainerEdit1_CloseUp);
            this.popupContainerEdit1.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.popupContainerEdit1_Closed);
            this.popupContainerEdit1.Properties.KeyDown += new System.Windows.Forms.KeyEventHandler(this.popupContainerEdit1_Properties_KeyDown);
        }

        public void InitReadOnly(   string ValueField, String GroupTableName, int[] RootID, String IDField, String ParentIDField,
                                    string[] VisibleFields, string[] Captions )
        {
            plGroupCatNew1.setPopupControl(popupContainerEdit1);
            popupContainerEdit1.Text = popupContainerEdit1.Properties.NullText;
            plGroupCatNew1.Init(GroupElementType.ONLY_CHOICE, GroupTableName, RootID, IDField, 
                                ParentIDField, VisibleFields, Captions, HelpGen.G_FW_DM_ID, null, null);
            this.ValueField = ValueField;
        }

        public void InitUpdate( string ValueField, String GroupTableName, int[] RootID, String IDField, String ParentIDField,
                                string[] VisibleFields, string[] Captions, string GenName,
                                object[] InputColumnType, FieldNameCheck[] Rules)
        {
            plGroupCatNew1.setPopupControl(popupContainerEdit1);
            plGroupCatNew1.Init(GroupElementType.CHOICE_N_ADD, GroupTableName, RootID, IDField, 
                                ParentIDField, VisibleFields, Captions, GenName, InputColumnType, Rules);
            this.ValueField = ValueField;
        }

        public void SetOtherInfo(bool IsShowFilter, int W, int H)
        {
            this.popupContainerControl1.Width = W;
            this.popupContainerControl1.Height = H;
            if (IsShowFilter && plGroupCatNew1.TreeList_1.FilterConditions.Count == 0)
            {
                HelpTree.ShowFilter(plGroupCatNew1.TreeList_1, true, plGroupCatNew1.IDField, plGroupCatNew1.ParentIDField, FilterConditionEnum.NotContains);
            }
        }

        public void ShowImmediatePopup()
        {
            //Xử lý Filter
            this.popupContainerEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            text = new System.EventHandler(this.popupContainerEdit1_TextChanged);
            this.popupContainerEdit1.TextChanged += text;
            this.popupContainerEdit1.Properties.NullText = "";
            this.popupContainerEdit1.Popup += new EventHandler(popupContainerEdit1_Popup);
            if (plGroupCatNew1.TreeList_1.FilterConditions.Count == 0)
                HelpTree.ShowFilter(plGroupCatNew1.TreeList_1, true, plGroupCatNew1.IDField, plGroupCatNew1.ParentIDField, FilterConditionEnum.NotContains);
            this.AddCondition();
        }

        #region Hàm liên quan đến filter        
        private void AddCondition()
        {
            plGroupCatNew1.TreeList_1.FilterConditions.Add(new FilterCondition(FilterConditionEnum.NotContains, plGroupCatNew1.TreeList_1.Columns.ColumnByFieldName(ValueField), null, null, true));
        }
        private void popupContainerEdit1_TextChanged(object sender, EventArgs e)
        {
            object prevalue = null;

            plGroupCatNew1.TreeList_1.OptionsBehavior.EnableFiltering = true;
            plGroupCatNew1.TreeList_1.OptionsBehavior.AutoSelectAllInEditor = false;

            plGroupCatNew1.TreeList_1.FilterConditions[plGroupCatNew1.TreeList_1.FilterConditions.Count - 1].Value1 = prevalue;
            plGroupCatNew1.TreeList_1.FilterConditions[plGroupCatNew1.TreeList_1.FilterConditions.Count - 1].Visible = true;

            plGroupCatNew1.TreeList_1.FilterConditions[plGroupCatNew1.TreeList_1.FilterConditions.Count - 1].Value1 = popupContainerEdit1.Text;
            plGroupCatNew1.TreeList_1.FilterConditions[plGroupCatNew1.TreeList_1.FilterConditions.Count - 1].Visible = false;

            prevalue = popupContainerEdit1.Text;
            popupContainerEdit1.ShowPopup();
            popupContainerEdit1.Focus();
            isActive = true;
        }
        private void popupContainerEdit1_Properties_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //PHUOCNC Kiểm tra nếu có phần tử thỏa mạn thì mới hỗ trợ KeyDown
            if (e.KeyCode == System.Windows.Forms.Keys.Down || e.KeyCode == System.Windows.Forms.Keys.Up)
            {
                if (isActive)
                {
                    plGroupCatNew1.TreeList_1.Focus();
                    try
                    {
                        TreeListNode node = plGroupCatNew1.TreeList_1.Selection[0];
                        plGroupCatNew1.TreeList_1.FocusedNode = (e.KeyCode == System.Windows.Forms.Keys.Down) ? node.NextVisibleNode : node.PrevVisibleNode;
                    }
                    catch { }
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                plGroupCatNew1.btnSelect_Click(null, null);
            }
        }
        #endregion

        #region GET & SET
        public long _getSelectedID()
        {
            return plGroupCatNew1.getSelectedID();
        }
        public void _setSelectedID(long id)
        {
            plGroupCatNew1.setSelectedID(id);
            if (id != -1)
            {
                if (plGroupCatNew1.SelectedNode != null)
                {
                    popupContainerEdit1.Text = plGroupCatNew1.SelectedNode[ValueField].ToString();
                    return;
                }
            }
            popupContainerEdit1.Text = popupContainerEdit1.Properties.NullText;
        }
        #endregion

        #region Kiểm tra dữ liệu
        public void SetError(DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider, string errorName)
        {
            errorProvider.SetError(this.popupContainerEdit1, errorName);
        }
        #endregion

        #region Sự kiện trên Popup
        private void popupContainerEdit1_Popup(object sender, EventArgs e)
        {
            plGroupCatNew1.setSelectedID(selectId);
        }
        private void popupContainerEdit1_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            if (plGroupCatNew1.TreeList_1.OptionsBehavior.Editable == true)
            {
                TreeListNode node = plGroupCatNew1.TreeList_1.FocusedNode;
                //HUYNC : Thêm điều kiện kiểm tra có sử dụng filter hay không?
                if (node.Id != 0) this.popupContainerEdit1.ShowPopup();
            }
        }

        private void popupContainerEdit1_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            TreeListNode selectNode = plGroupCatNew1.SelectedNode;
            if (selectNode == null)
            {
                //Thông báo không cho chọn nút trên cùng
                popupContainerEdit1.Text = popupContainerEdit1.Properties.NullText;
                return;
            }
            popupContainerEdit1.TextChanged -= text;
            if (selectNode.GetValue(ValueField) != DBNull.Value && selectNode.GetValue(ValueField) != null)
                popupContainerEdit1.Text = selectNode.GetValue(ValueField).ToString();
            else
                popupContainerEdit1.Text = popupContainerEdit1.Properties.NullText;
            popupContainerEdit1.TextChanged += text;

            try
            {
                plGroupCatNew1.TreeList_1.FilterConditions[plGroupCatNew1.TreeList_1.FilterConditions.Count - 1].Value1 = null;
                plGroupCatNew1.TreeList_1.FilterConditions[plGroupCatNew1.TreeList_1.FilterConditions.Count - 1].Visible = true;
                selectId = plGroupCatNew1.getSelectedID();
            }
            catch { }
        }
        #endregion
    }
}
