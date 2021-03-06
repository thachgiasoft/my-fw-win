using System.Windows.Forms;
using ProtocolVN.Framework.Core;
using DevExpress.XtraEditors;
namespace ProtocolVN.Framework.Win
{
    public class FWMsgBox
    {
        public static DialogResult showMissingFile(string fileMissing)
        {
            return PLMessageBox.ShowSystemErrorMessage("Không tồn tại dữ liệu cần thiết để chạy chương trình :\n" + fileMissing);
        }

        public static DialogResult showDeleteUsing()
        {
            return HelpMsgBox.ShowNotificationMessage("Dữ liệu đang được sử dụng bởi chương trình.");
        }

        public static DialogResult showValidConnect()
        {
            return HelpMsgBox.ShowNotificationMessage("Kết nối thành công với máy chủ dữ liệu.");
        }

        public static DialogResult showInvalidConnectServer(XtraForm form)
        {
            return HelpMsgBox.ShowSystemErrorMessage("Không thể kết nối được với máy chủ dữ liệu.\nVui lòng kiểm tra lại thông tin kết nối với máy chủ dữ liệu.", form);
        }

        public static DialogResult showErrorImage()
        {
            return HelpMsgBox.ShowErrorMessage("Định dạng tập tin hình ảnh không hợp lệ.\nVui lòng chọn những tập tin có định dạng ...");
        }

        public static DialogResult showBackupError()
        {
            return HelpMsgBox.ShowErrorMessage("Sao lưu dữ liệu không thành công.\nThao tác này chỉ thực hiện được tại máy chủ.\nVui lòng thực hiện sao lưu lại.");
        }

        public static DialogResult showRestoreError()
        {
            return HelpMsgBox.ShowErrorMessage("Phục hồi dữ liệu không thành công.\nVui lòng thực hiện phục hồi lại.");
        }

        public static DialogResult showInvalidUser()
        {
            return HelpMsgBox.ShowErrorMessage("Tên đăng nhập và mật khẩu sai.\nVui lòng nhập lại thông tin đăng nhập.");            
        }

        //public static DialogResult showInvalidUser(Control control)
        //{
        //    return HelpMsgBox.ShowErrorMessage("Tên đăng nhập và mật khẩu sai.\nVui lòng nhập lại thông tin đăng nhập.", control);
        //}

        public static DialogResult showValidChangePassword()
        {
            return HelpMsgBox.ShowNotificationMessage("Đổi mật khẩu thành công.");
        }

        public static DialogResult showInvalidChangePassword()
        {
            return HelpMsgBox.ShowNotificationMessage("Đổi mật khẩu không thành công.");
        }

        public static DialogResult showInvalidOldPassword()
        {
            return HelpMsgBox.ShowErrorMessage("Mật khẩu cũ không hợp lệ.\nVui lòng nhập lại.");
        }

        public static DialogResult showConfirmRemoveCatelogy()
        {
            return HelpMsgBox.ShowConfirmMessage("Bạn có muốn xóa dữ liệu này?");
        }

        public static DialogResult questionGroupDelete(string GroupName)
        {
            return HelpMsgBox.ShowConfirmMessage("Bạn có muốn xóa nhóm người dùng '" + GroupName + "' này không?");
        }

        public static DialogResult questionUserDelete(string UserName)
        {
            return HelpMsgBox.ShowConfirmMessage("Bạn có muốn xóa người dùng '" + UserName + "' này không?");
        }

        public static DialogResult showGroupNameExist()
        {
            return HelpMsgBox.ShowErrorMessage("Tên nhóm người dùng này đã tồn tại.");
        }

        public static DialogResult showInvalidNotNull()
        {
            return HelpMsgBox.ShowErrorMessage("Bạn không vào dữ liệu.\nVui lòng nhập vào dữ liệu.");
        }

        public static DialogResult showInvalidInputGrid()
        {
            return HelpMsgBox.ShowErrorMessage("Dữ liệu này đã tồn tại.\nXin vui lòng nhập dữ liệu khác");
        }
    }
}