using System.Windows.Forms;
using DevExpress.XtraEditors;
using System;

namespace ProtocolVN.Framework.Win
{
    //Đã dùng giải pháp khác để thay thế.
    [Obsolete("Không sử dụng")]
    public interface SplashForm
    {
    }
    [Obsolete("Không sử dụng")]
    public partial class frmFWSplash : XtraForm, SplashForm
    {
        public frmFWSplash()
        {
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();
            Cursor.Current = Cursors.Default;
        }
    }
}