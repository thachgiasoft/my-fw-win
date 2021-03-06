using System;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    /// <summary>Control cho phép chọn tình trạng duyệt của phiếu theo dạng checkbox
    /// Cho phép nhiều tình trạng cùng một lúc nhưng tối thiểu phải chọn 1 tình trạng
    /// </summary>
    public partial class PLDuyetCheckbox : DevExpress.XtraEditors.XtraUserControl
    {
        public PLDuyetCheckbox()
        {
            InitializeComponent();
        }

        public void _initRedCheckEdit()
        {
            HelpControl.RedCheckEdit(checkDuyet, false);
            HelpControl.RedCheckEdit(checkChoDuyet, false);
            HelpControl.RedCheckEdit(checkKhongDuyet, false);
        }

        //========ID cua cac check box
        public static int IDChkDuyet  = 1;
        public static int IDChkKDuyet = 2;
        public static int IDChkCDuyet = 3;

        //========Ham xu ly su kien khi click vao cac checkbox
        private void checkDuyet_CheckedChanged(object sender, EventArgs e)
        {
            kiemTraChon(IDChkDuyet);
        }

        private void checkKhongDuyet_CheckedChanged(object sender, EventArgs e)
        {
            kiemTraChon(IDChkKDuyet);
        }

        private void checkChoDuyet_CheckedChanged(object sender, EventArgs e)
        {
            kiemTraChon(IDChkCDuyet);
        }

        public void kiemTraChon(int n)
        {
            TRANG_THAI_DUYET tt = layTrangThai();
            if (tt == TRANG_THAI_DUYET.KHONG_CHECK)
            {
                //ToolTip t = new ToolTip();
                //t.SetToolTip(this, "Thông báo");
                //t.Show("Bạn phải check ít nhất một ô", this, 100, 100, 1000);
                check(n);
            }
        }  

        //========Ham tra ve trang  thai cac checkbox cua usercontrol
        public TRANG_THAI_DUYET layTrangThai()
        {
            if (checkDuyet.Checked)
            {
                if (checkChoDuyet.Checked && checkKhongDuyet.Checked)
                    return TRANG_THAI_DUYET.DUYET_KDUYET_CDUYET;

                if (!checkChoDuyet.Checked && checkKhongDuyet.Checked)
                    return TRANG_THAI_DUYET.DUYET_KDUYET;

                if (checkChoDuyet.Checked && !checkKhongDuyet.Checked)
                    return TRANG_THAI_DUYET.DUYET_CDUYET;

                return TRANG_THAI_DUYET.DUYET;
            }
            else
            {
                if (checkChoDuyet.Checked && checkKhongDuyet.Checked)
                    return TRANG_THAI_DUYET.CDUYET_KDUYET;

                if (!checkChoDuyet.Checked && checkKhongDuyet.Checked)
                    return TRANG_THAI_DUYET.KHONG_DUYET;

                if (checkChoDuyet.Checked && !checkKhongDuyet.Checked)
                    return TRANG_THAI_DUYET.CHO_DUYET;

                return TRANG_THAI_DUYET.KHONG_CHECK;
            }
        }

        //========Check mot checkbox
        public void check(int n)
        {
            if (n == IDChkDuyet)
                checkDuyet.Checked = true;

            if (n == IDChkKDuyet)
                checkKhongDuyet.Checked = true;

            if (n == IDChkCDuyet)
                checkChoDuyet.Checked = true;
        }

        //========Uncheck mot checkbox
        public void unCheck(int n)
        {
            if (n == IDChkDuyet)
                checkDuyet.Checked = false;

            if (n == IDChkKDuyet)
                checkKhongDuyet.Checked = false;

            if (n == IDChkCDuyet)
                checkChoDuyet.Checked = false;
        }
    }
}
