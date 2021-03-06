using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraBars;

namespace ProtocolVN.Framework.Win
{
    [Obsolete("Ít dùng. Được dùng tương thích menu cũ")]
    public interface IMainForm
    {
        BarManager GetBarManager();
        void logout_Click(object sender, System.EventArgs e);
    }

    /// <summary>
    /// Form hỗ trợ tham số tùy vào tham số mà focus đến phần nào trên form
    /// Ví dụ : như form danh mục hệ thống, hay form hệ thống report
    /// </summary>
    public interface IParamForm
    {
        /// <summary>
        /// Hàm xử lý việc kích hoạt lại với tham số chọn khác
        /// </summary>
        void Activate();
    }
}
