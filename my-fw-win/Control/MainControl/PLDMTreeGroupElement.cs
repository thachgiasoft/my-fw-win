using System;
using System.Data;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
using DevExpress.XtraGrid.Views.Grid;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
namespace ProtocolVN.Framework.Win
{
    sealed public class PLDMTreeGroupElement : XtraUserControl, IIDValidation
    {
        #region Biến
        #region Dùng cho tùy biến
        public delegate XtraForm Add_Delegate();
        public delegate XtraForm Edit_Delegate(string id);
        public delegate XtraForm View_Delegate(string id);

        public Add_Delegate add;
        public Edit_Delegate edit;
        public View_Delegate view;
        #endregion
        public DMTreeGroupElement plGroupElement1;
        public PopupContainerControl popupContainerControl1;
        /// <summary>Cho phải tự tính kích thước của popup hoặc cố định
        /// </summary>
        public bool isFixPopupContainer = false;

        private PopupContainerEdit popupContainerEdit1;
        private string DisplayField;
        private bool IsUpDownKey = false;
        private bool IsFilter = false;
        private long selectId = -1;
        private System.EventHandler text;

        #endregion

        #region Thuộc tính
        public void DefinePermission(DelegationLib.DefinePermission permission)
        {
            this.plGroupElement1.DefinePermission(permission);
            
            if (HelpPermission.CheckCtrl(plGroupElement1) == false)
            {
                plGroupElement1.Visible = false;
                //Nên thay thành dòng hiện bạn chưa có quyền ngay vị trí của control đó
                return;
            }
        }
        #endregion

        #region Size
        [Browsable(true), Category("_PROTOCOL"), Description("Kích thước Popup gấp bao nhiêu so với Control")]
        public float ZZZWidthFactor
        {
            get { return _WidthFactor; }
            set{ _WidthFactor = value; }
        }
        private float _WidthFactor = 2;//Chiều rộng của popup tùy vào số này
        void PLDMGrid_Resize(object sender, EventArgs e)
        {
            if(isFixPopupContainer == false)
                _CalcSize();
        }
        private void _CalcSize()
        {
            if (this.popupContainerControl1.Size.Width != (int)(this.Size.Width * _WidthFactor))
            {
                this.popupContainerControl1.Size = new Size((int)(this.Size.Width * _WidthFactor), this.popupContainerControl1.Size.Height);
            }
        }
        void PLDMTreeGroupElement_Load(object sender, EventArgs e)
        {
            if (isFixPopupContainer == false)
                _CalcSize();
            this.Load -= PLDMTreeGroupElement_Load;
        }
        #endregion

        #region Khởi tạo
        public PLDMTreeGroupElement()
        {
            InitializeComponent();
            //Sự kiện đưa ra bên ngoài
            this.popupContainerEdit1.EditValueChanged += new System.EventHandler(this.popupContainerEdit1_EditValueChanged);

            popupContainerEdit1.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.DoubleClick;
            this.popupContainerControl1.PopupContainerProperties.CloseOnOuterMouseClick = false;

            this.popupContainerEdit1.CloseUp += new DevExpress.XtraEditors.Controls.CloseUpEventHandler(popupContainerEdit1_CloseUp);
            this.popupContainerEdit1.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.popupContainerEdit1_Closed);
            this.popupContainerEdit1.Popup += new EventHandler(popupContainerEdit1_Popup);
            this.Resize += PLDMGrid_Resize;
            this.Load += new EventHandler(PLDMTreeGroupElement_Load);

            HelpControl.SetToolTip(popupContainerEdit1, "Trợ giúp", @"<size=8><b>Phím tắt</b> <br><size=8>+ F4: Ẩn/Hiện hộp thoại chọn<br><size=8>+ Alt-1: Chọn phần tử<br><size=8>+ Alt-2: Chọn nhóm<br><size=8>+ Alt-?: Di chuyển vào dòng lọc dữ liệu"
                , null);
        }

        public void InitReadOnly(string GroupTableName, int[] RootID, string GroupIDField,
                            string GroupParentIDField, string[] GroupVisibleFields, string[] GroupCaptions,
                            String tableName, string IDField, string LinkField, string DisplayField, string GenName,
                            ProtocolVN.Framework.Win.DMTreeGroupElement.InitGridColumns InitGridCol, bool IsTreeVisible, bool IsGridVisible)
        {
            this.DisplayField = DisplayField;
            plGroupElement1.Init(GroupElementType.ONLY_CHOICE, GroupTableName, RootID, GroupIDField, GroupParentIDField,
                            GroupVisibleFields, GroupCaptions, tableName, IDField, LinkField, DisplayField, GenName
                            , InitGridCol, null, IsTreeVisible, IsGridVisible);
            plGroupElement1.setPopUp(popupContainerEdit1);
            if (this.add != null) plGroupElement1.add = new DMTreeGroupElement.Add_Delegate(this.add);
            if (this.edit != null) plGroupElement1.edit = new DMTreeGroupElement.Edit_Delegate(this.edit);
            if (this.view != null) plGroupElement1.view = new DMTreeGroupElement.View_Delegate(this.view);
        }


        public void InitReadOnly(string GroupTableName, int[] RootID, string GroupIDField,
                            string GroupParentIDField, string[] GroupVisibleFields, string[] GroupCaptions,
                            DataSet dtGrid, string IDField, string LinkField, string DisplayField, string GenName,
                            ProtocolVN.Framework.Win.DMTreeGroupElement.InitGridColumns InitGridCol, bool IsTreeVisible, bool IsGridVisible)
        {
            this.DisplayField = DisplayField;
            plGroupElement1.Init(GroupElementType.ONLY_CHOICE, GroupTableName, RootID, GroupIDField, GroupParentIDField,
                            GroupVisibleFields, GroupCaptions, dtGrid, IDField, LinkField, DisplayField, GenName
                            , InitGridCol, null, IsTreeVisible, IsGridVisible);
            plGroupElement1.setPopUp(popupContainerEdit1);
            if (this.add != null) plGroupElement1.add = new DMTreeGroupElement.Add_Delegate(this.add);
            if (this.edit != null) plGroupElement1.edit = new DMTreeGroupElement.Edit_Delegate(this.edit);
            if (this.view != null) plGroupElement1.view = new DMTreeGroupElement.View_Delegate(this.view);
        }
        public void InitReadOnly(string GroupTableName, int[] RootID, string GroupIDField,
                            string GroupParentIDField, string[] GroupVisibleFields, string[] GroupCaptions,
                            DataSet dtGrid, string IDField, string LinkField, string DisplayField, string GenName,
                            ProtocolVN.Framework.Win.DMTreeGroupElement.InitGridColumns InitGridCol)
        {
            InitReadOnly(GroupTableName, RootID, GroupIDField,
                             GroupParentIDField, GroupVisibleFields, GroupCaptions,
                             dtGrid, IDField, LinkField, DisplayField, GenName,
                              InitGridCol, false, false);
        }
        public void InitUpdate(string GroupTableName, int[] RootID, string GroupIDField,
                            string GroupParentIDField, string[] GroupVisibleFields, string[] GroupCaptions,
                            DataSet dtGrid, string IDField, string LinkField, string DisplayField, string GenName,
                            DMTreeGroupElement.InitGridColumns InitGridCol,
                            DMTreeGroupElement.GetRule Rule)
        {
            InitUpdate(GroupTableName, RootID, GroupIDField,
                             GroupParentIDField, GroupVisibleFields, GroupCaptions,
                             dtGrid, IDField, LinkField, DisplayField, GenName,
                             InitGridCol, Rule, false, false);
        }
        
        public void InitUpdate(string GroupTableName, int[] RootID, string GroupIDField,
                            string GroupParentIDField, string[] GroupVisibleFields, string[] GroupCaptions,
                            DataSet dtGrid, string IDField, string LinkField, string DisplayField, string GenName,
                            DMTreeGroupElement.InitGridColumns InitGridCol,
                            DMTreeGroupElement.GetRule Rule, bool IsTreeVisible, bool IsGridVisible)
        {
            this.DisplayField = DisplayField;
            plGroupElement1.Init(GroupElementType.CHOICE_N_ADD, GroupTableName, RootID, GroupIDField, GroupParentIDField,
                            GroupVisibleFields, GroupCaptions, dtGrid, IDField, LinkField, DisplayField, GenName, InitGridCol
                            , Rule, IsTreeVisible, IsGridVisible);
            plGroupElement1.setPopUp(popupContainerEdit1);
            if (this.add != null) plGroupElement1.add = new DMTreeGroupElement.Add_Delegate(this.add);
            if (this.edit != null) plGroupElement1.edit = new DMTreeGroupElement.Edit_Delegate(this.edit);
            if (this.view != null) plGroupElement1.view = new DMTreeGroupElement.View_Delegate(this.view);
        }

        public void InitUpdate(string GroupTableName, int[] RootID, string GroupIDField,
                            string GroupParentIDField, string[] GroupVisibleFields, string[] GroupCaptions,
                            String elementTable, string IDField, string LinkField, string DisplayField, string GenName,
                            DMTreeGroupElement.InitGridColumns InitGridCol,
                            DMTreeGroupElement.GetRule Rule, bool IsTreeVisible, bool IsGridVisible)
        {
            this.DisplayField = DisplayField;
            plGroupElement1.Init(GroupElementType.CHOICE_N_ADD, GroupTableName, RootID, GroupIDField, GroupParentIDField,
                            GroupVisibleFields, GroupCaptions, elementTable, IDField, LinkField, DisplayField, GenName, InitGridCol
                            , Rule, IsTreeVisible, IsGridVisible);
            plGroupElement1.setPopUp(popupContainerEdit1);
            if (this.add != null) plGroupElement1.add = new DMTreeGroupElement.Add_Delegate(this.add);
            if (this.edit != null) plGroupElement1.edit = new DMTreeGroupElement.Edit_Delegate(this.edit);
            if (this.view != null) plGroupElement1.view = new DMTreeGroupElement.View_Delegate(this.view);
        }

        public void InitUpdate(string GroupTableName, int[] RootID, string GroupIDField,
                            string GroupParentIDField, string[] GroupVisibleFields, string[] GroupCaptions,
                            String elementTable, string IDField, string LinkField, string DisplayField, string GenName,
                            DMTreeGroupElement.InitGridColumns InitGridCol,
                            DMTreeGroupElement.GetRule Rule)
        {
            InitUpdate(GroupTableName, RootID, GroupIDField,
                             GroupParentIDField, GroupVisibleFields, GroupCaptions,
                             elementTable, IDField, LinkField, DisplayField, GenName,
                             InitGridCol, Rule, false, false);
        }

        #endregion

        #region Sử dụng
        public void _setShowImmediatePopup()
        {
            ShowImmediatePopup();
        }
        public void ShowImmediatePopup()
        {
            this.popupContainerEdit1.KeyDown += new KeyEventHandler(popupContainerEdit1_KeyDown);
            this.popupContainerEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            text = new System.EventHandler(this.popupContainerEdit1_TextChanged);
            this.popupContainerEdit1.TextChanged += text;
            this.popupContainerEdit1.Properties.NullText = GlobalConst.NULL_TEXT;
        }
        public void _setOtherInfo(bool IsShowFilter, int W, int H)
        {
            SetOtherInfo(IsShowFilter, W, H);
        }
        public void SetOtherInfo(bool IsShowFilter, int W, int H)
        {
            this.plGroupElement1.gridView_1.OptionsView.ShowAutoFilterRow = IsShowFilter;
            this.popupContainerControl1.Width = W;
            this.popupContainerControl1.Height = H;
        }
        #region GET & SET
        public void SetError(DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider, string errorName)
        {
            errorProvider.SetError(popupContainerEdit1, errorName);
        }
        public long _getSelectedID()
        {
            return plGroupElement1._getSelectedID();
        }
        public void _setSelectedID(long id)
        {
            if (id != -1)
            {
                this.selectId = id;
                DataRow SelectedRow = plGroupElement1._setSelectedID(id);

                if (SelectedRow == null)
                {
                    popupContainerEdit1.Text = popupContainerEdit1.Properties.NullText;
                }
                else
                {
                    popupContainerEdit1.Text = SelectedRow[DisplayField].ToString();
                }
            }
        }
        #endregion
        #endregion

        #region Xử lý Filter
        private void popupContainerEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (plGroupElement1.gridView_1.RowCount > 0)
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
                {
                    IsFilter = false;
                    if (IsUpDownKey)
                    {
                        plGroupElement1.gridView_1.Focus();
                        try
                        {
                            int index = (plGroupElement1.gridView_1.GetSelectedRows())[0];
                            plGroupElement1.gridView_1.FocusedRowHandle = (e.KeyCode == Keys.Down) ? index + 1 : index - 1;
                        }
                        catch { }
                    }
                }
                //else if (e.KeyCode == Keys.Enter)
                //{
                //    plGroupElement1.btnSelect_Click(null, null);
                //}
                else
                {
                    IsFilter = true;
                }
            }
            else
            {
                IsFilter = true;
            }
        }
        private void popupContainerEdit1_TextChanged(object sender, EventArgs e)
        {            
           if (IsFilter && popupContainerEdit1.EditorContainsFocus && 
                plGroupElement1.getDislayText() != popupContainerEdit1.Text) 
            {
                if (popupContainerEdit1.Text != "" && popupContainerEdit1.Text != GlobalConst.NULL_TEXT)
                {
                    plGroupElement1.gridView_1.ActiveFilterString = "[" + DisplayField + "]" + " Like " + "'%" + popupContainerEdit1.Text + "%'";
                    if (plGroupElement1.gridView_1.RowCount >= 0)
                    {
                        if(plGroupElement1.forceExitCtrl==false){
                            popupContainerEdit1.ShowPopup();
                            popupContainerEdit1.Focus();
                            IsUpDownKey = true;
                        }
                        else{
                            popupContainerEdit1.Focus();
                            IsUpDownKey = false;
                            plGroupElement1.forceExitCtrl = false;
                        }
                    }
                }
                else
                {
                    plGroupElement1.gridView_1.ActiveFilterString = "";
                    popupContainerEdit1.Text = null;
                }
            }
        }
       
        #endregion

        #region Component Designer generated code
        private System.ComponentModel.Container components = null;
        protected override void Dispose(bool disposing)
        {
            try
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
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
        }

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.popupContainerEdit1 = new DevExpress.XtraEditors.PopupContainerEdit();
            this.popupContainerControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            this.plGroupElement1 = new ProtocolVN.Framework.Win.DMTreeGroupElement();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
            this.popupContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // popupContainerEdit1
            // 
            this.popupContainerEdit1.Dock = System.Windows.Forms.DockStyle.Top;
            this.popupContainerEdit1.Location = new System.Drawing.Point(0, 0);
            this.popupContainerEdit1.Name = "popupContainerEdit1";
            this.popupContainerEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.popupContainerEdit1.Properties.NullText = "Chọn giá trị";
            this.popupContainerEdit1.Properties.PopupControl = this.popupContainerControl1;
            this.popupContainerEdit1.Properties.ShowPopupCloseButton = false;
            this.popupContainerEdit1.Size = new System.Drawing.Size(623, 20);
            this.popupContainerEdit1.TabIndex = 1;
            // 
            // popupContainerControl1
            // 
            this.popupContainerControl1.Controls.Add(this.plGroupElement1);
            this.popupContainerControl1.Location = new System.Drawing.Point(0, 20);
            this.popupContainerControl1.Name = "popupContainerControl1";
            this.popupContainerControl1.Size = new System.Drawing.Size(623, 326);
            this.popupContainerControl1.TabIndex = 1;
            // 
            // plGroupElement1
            // 
            this.plGroupElement1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plGroupElement1.Location = new System.Drawing.Point(0, 0);
            this.plGroupElement1.Name = "plGroupElement1";
            this.plGroupElement1.Size = new System.Drawing.Size(623, 326);
            this.plGroupElement1.TabIndex = 0;
            // 
            // PLDMTreeGroupElement
            // 
            this.Controls.Add(this.popupContainerControl1);
            this.Controls.Add(this.popupContainerEdit1);
            this.Name = "PLDMTreeGroupElement";
            this.Size = new System.Drawing.Size(623, 346);
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
            this.popupContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        
        #endregion

        #region Xử lý Popup
        private void popupContainerEdit1_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            if (plGroupElement1.gridView_1.Editable == true)
            {
                this.popupContainerEdit1.ShowPopup();
            }
        }
        private void popupContainerEdit1_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            selectId = plGroupElement1._getSelectedID();
            popupContainerEdit1.Text = plGroupElement1.getDislayText();
            if (selectId == -1) popupContainerEdit1.Text = null;
            plGroupElement1.gridView_1.ActiveFilterString = "";
        }

        private void popupContainerEdit1_Popup(object sender, EventArgs e)
        {
            plGroupElement1._setSelectedID(selectId);
        }

        #endregion

        #region Đưa sự kiện ra bên ngoài
        public event EventHandler EditValueChanged = null;
        private void popupContainerEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (EditValueChanged != null)
            {
                EditValueChanged(sender, e);
            }
        }
        #endregion        
    }
}