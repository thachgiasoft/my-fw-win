using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraTreeList.Nodes;


namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Control cho phép dựng một cây đặt vào XtraTreeList
    /// TODO PHUOC : Phước Xây dựng thêm hỗ trợ cho phép ẩn các field không cần thiết
    /// </summary>
    public partial class TrialPLTreeCombobox : DevExpress.XtraEditors.XtraUserControl, ISelectionControl
    {
        #region Thuộc tính bắt buộc khởi tạo
        public string _tablename;           //Tên table chứa dữ liệu cây
        [Browsable(true), Category("_PROTOCOL"), Description("Tên TableName sẽ lấy dữ liệu khi chọn")]
        public string TableName{
            get{ return _tablename; }
            set{ TableName = value; }
        }
        public string _fieldid;             //ID Field
        [Browsable(true), Category("_PROTOCOL"), Description("Tên ID FIELD")]
        public string IDField{
            get { return _fieldid; }
            set { _fieldid = value; }
        }
        public string _fieldparentid;       //Parent Field
        [Browsable(true), Category("_PROTOCOL"), Description("Tên PARENT FIELD")]
        public string ParentField{
            get { return _fieldparentid; }
            set { _fieldparentid = value; }
        }

        public string _fielddisplay;        //Display Field field được trình bày khi chọn
        [Browsable(true), Category("_PROTOCOL"), Description("Tên DISPLAY FIELD")]
        public string DisplayField
        {
            get { return _fielddisplay; }
            set { _fielddisplay = value; }
        }
        public string[] _visibledfields;    //Danh sách các field muốn trình bày
        public string[] _captions;          //Danh sách các caption tương ứng với field trình bày
        
        #endregion
        
        private string _DisplayText;         
        private object _ValueText;
        //private int visibleIndex;

        //public string Text{
        //    get{
        //        return _getValidateData();
        //    }
        //}                                       
        public TrialPLTreeCombobox()
        {
            InitializeComponent();            
        }                                      
        
        #region Sự kiện trên control

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.EditText.CancelPopup();
            this._DisplayText = GlobalConst.NULL_TEXT;
            this._ValueText = "-1";
            this.EditText.Text = this._DisplayText;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            this.EditText.ClosePopup();
            _getSelectedValue();
            this.EditText.Text = _DisplayText;
        }

        private void dataTree_DoubleClick(object sender, EventArgs e)
        {
            EditText.ClosePopup();
            _getSelectedValue();
            EditText.Text = _DisplayText;
        }

        private void plDataTree1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditText.ClosePopup();
                _getSelectedValue();
                EditText.Text = _DisplayText;
            }
            else if (e.KeyCode == Keys.Tab)
                btnSelect.Focus();
        }

        private void EditText_Popup(object sender, EventArgs e)
        {
            foreach (TreeListNode tn in plDataTree1.Nodes)
            {
                if (tn[this.plDataTree1.Columns[_fieldid].AbsoluteIndex].ToString() == _ValueText.ToString())
                {
                    plDataTree1.FocusedNode = tn;
                    break;
                }
            }
        }

        public event EventHandler EditValueChanged = null;
        private void EditText_EditValueChanged_1(object sender, EventArgs e)
        {
            if (EditValueChanged != null)
            {
                EditValueChanged(sender, e);
            }
        }
        #endregion
        
        #region ISelectionControl Members

        public long _getSelectedID()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void _setSelectedID(long id)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public object _getSelectedValue()
        {
            if (this.plDataTree1.FocusedNode != null)
            {
                _DisplayText = this.plDataTree1.FocusedNode
                    [this.plDataTree1.Columns[_fielddisplay].AbsoluteIndex].ToString();
                _ValueText = this.plDataTree1.FocusedNode
                    [this.plDataTree1.Columns[_fieldid].AbsoluteIndex];
                //visibleIndex = plDataTree1.GetNodeIndex(plDataTree1.FocusedNode);
            }
            else
            {
                _DisplayText = GlobalConst.NULL_TEXT;
                _ValueText = "-1";
            }
            return _ValueText;
        }

        //TODO PHUOC : Phước Chưa xử lý được focus đến phần tử mình chọn
        public void _setSelectedValue(object data)
        {
            //this.plDataTree1.FocusedNode[this.plDataTree1.Columns[valueMemberField].AbsoluteIndex];
            //int i = 0;
            foreach (TreeListNode tn in plDataTree1.Nodes)
            {
                if (tn[this.plDataTree1.Columns[_fieldid].AbsoluteIndex].ToString() == data.ToString())
                {
                    _ValueText = data;
                    _DisplayText = tn[this.plDataTree1.Columns[_fielddisplay].AbsoluteIndex].ToString();
                    //visibleIndex = i;
                    EditText.Text = _DisplayText;
                    return;
                }

            }
        }

        #endregion

        #region IPLControl Members

        /// <summary>
        /// Tạo cây dữ liệu đặt vào XtraTreeList        
        /// </summary>
        /// <param name="tablename">Tên table có cấu trúc dạng cây</param>
        /// <param name="fieldid">Tên field ID</param>
        /// <param name="fieldparentid">Ten field ParentID</param>
        /// <param name="visibledfields">Danh sách field hiển thị</param>
        /// <param name="captions">Danh sách caption cho field hiển thị</param>
        /// <param name="fielddisplay">Field dùng hiển thị khi chọn</param>
        public void _init()
        {
            this.plDataTree1._BuildTree(_tablename, _fieldid, _fieldparentid, _visibledfields, _captions);
            _DisplayText = GlobalConst.NULL_TEXT;
            _ValueText = "-1";
            this.EditText.Text = _DisplayText;
        }

        public void _refresh()
        {
            _init();
        }

        public string _getValidateData()
        {
            return _getSelectedValue().ToString();
        }

        #endregion

        public void SetError(DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider, string errorName)
        {
            errorProvider.SetError(this.EditText, errorName);
        }


        #region ISelectionControl Members


        public void _refresh(object NewSrc)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}