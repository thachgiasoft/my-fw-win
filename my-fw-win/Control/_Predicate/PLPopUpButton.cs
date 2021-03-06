using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    /// <summary>PREDICATE: Hiển thị button cho phép chọn menu sổ xuống. 
    /// Không dùng nữa thay bằng DropDownButton
    /// </summary>
    public partial class PLPopUpButton : XtraUserControl
    {
        #region Thuộc tính bắt buộc của control
        /// <summary>
        /// Menu hiện ra khi chọn vào popup button
        /// Các sự kiện trên menu sẽ được xử lý tại nơi khởi tạo menu.
        /// </summary>
        public System.Windows.Forms.ContextMenuStrip _menu;
        /// <summary>
        /// Tiêu đề trên caption.
        /// </summary>
        public string _buttonTitle;
        [Browsable(true), Category("_PROTOCOL"), Description("Caption cho nút")]
        public string PLButtonTitle
        {
            get{
                return _buttonTitle;
            }
            set{
                _buttonTitle = value;
            }
        }
        #endregion

        public PLPopUpButton()
        {
            InitializeComponent();
            
        }
        private bool show = true;
        private void popup_Click(object sender, EventArgs e)
        {
            
            if (_menu == null) { return; }
            Point p = new Point(but.Left, but.Bottom);            
            if(show){
                _menu.Show(this, p, ToolStripDropDownDirection.Default);
                show = false;
            }
            else{
                _menu.Hide();
                show = true;
            }
        }

        private void popup_Leave(object sender, EventArgs e)
        {
            show = true;
        }
        private void popup_MouseLeave(object sender, EventArgs e)
        {
            show = true;
        }

        #region IPLControl Members

        public void _init()
        {
            this.but.Text = _buttonTitle;
        }

        public void _refresh()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public string _getValidateData()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        public ProtocolVN.Framework.Core.DelegationLib.CallFunction_NoIn_NoOut Func;
        private void but_Click(object sender, EventArgs e)
        {
            if (Func != null)
                Func();
        }
    }
}
