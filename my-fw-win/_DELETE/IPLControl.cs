namespace ProtocolVN.Framework.Win
{
    /// <summary>Các thao tác trên control của PL. 
    /// Control của PL phải thừa kế từ Interface này.
    /// </summary>
    public interface IPLControl
    {
        /// <summary>Khởi tạo giá trị ban đầu cho control
        /// </summary>
        void _init();

        /// <summary>Làm mới dữ liệu
        /// </summary>
        void _refresh();        
    }

    /// <summary>Các thao tác trên control chọn dữ liệu từ danh mục    
    /// </summary>
    public interface ISelectionControl : IPLControl {
        /// <summary>Trả về ID tương ứng với phần từ đang chọn        
        /// ID = -1 : chưa chọn phần tử nào
        /// </summary>
        long _getSelectedID();

        /// <summary>Chọn phần tử có id bằng với id chỉ định
        /// ID = -1 : không chọn phần tử nào
        /// </summary>
        /// <param name="id"></param>
        void _setSelectedID(long id);
    }

    public interface IValueControl : IPLControl
    {
        
    }
}
