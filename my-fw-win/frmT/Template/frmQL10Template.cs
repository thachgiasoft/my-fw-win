using DevExpress.XtraGrid.Columns;
using ProtocolVN.Framework.Core;
using DevExpress.XtraEditors;
namespace ProtocolVN.Framework.Win.Demo
{
    //public partial class frmQL10Template : XtraFormPL
    public partial class frmQL10Template : PhieuQuanLy10Change
    {
        //protected DevExpress.XtraBars.BarButtonItem barButtonItem1;
        //protected DevExpress.XtraBars.BarButtonItem barButtonItem2;
        //protected DevExpress.XtraBars.BarStaticItem barStaticItem1;

        #region Các biến của form Quan Ly Tuyệt đối không thay đổi
        //protected DevExpress.XtraBars.BarManager barManager1;
        //protected DevExpress.XtraBars.Bar MainBar;
        //protected DevExpress.XtraBars.BarButtonItem barButtonItemAdd;
        //protected DevExpress.XtraBars.BarButtonItem barButtonItemDelete;
        //protected DevExpress.XtraBars.BarButtonItem barButtonItemUpdate;
        //protected DevExpress.XtraBars.BarButtonItem barButtonItemPrint;
        //protected DevExpress.XtraBars.BarDockControl barDockControlTop;
        //protected DevExpress.XtraBars.BarDockControl barDockControlBottom;
        //protected DevExpress.XtraBars.BarDockControl barDockControlLeft;
        //protected DevExpress.XtraBars.BarDockControl barDockControlRight;
        //protected DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        //protected DevExpress.XtraGrid.GridControl gridControlMaster;
        //protected DevExpress.XtraGrid.Views.Grid.PLGridView gridViewMaster;
        //protected DevExpress.XtraTab.XtraTabControl xtraTabControlDetail;
        //protected DevExpress.XtraTab.XtraTabPage xtraTabPageDetail;
        //protected DevExpress.XtraBars.PopupControlContainer popupControlContainerFilter;
        //protected DevExpress.XtraGrid.GridControl gridControlDetail;
        //protected DevExpress.XtraGrid.Views.Grid.PLGridView gridViewDetail;
        //protected DevExpress.XtraBars.BarButtonItem barButtonItemCommit;
        //protected DevExpress.XtraBars.BarButtonItem barButtonItemNoCommit;
        //protected DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        //private System.ComponentModel.IContainer components;
        //private DevExpress.XtraBars.BarSubItem barSubItem1;
        //private DevExpress.XtraBars.BarButtonItem barButtonItemXem;
        //protected DevExpress.XtraBars.BarButtonItem barButtonItemClose;
        //private DevExpress.XtraBars.PopupMenu popupMenuFilter;
        //protected DevExpress.XtraBars.BarCheckItem barCheckItemFilter;
        //private DevExpress.XtraBars.BarButtonItem barButtonItemSearch;
        //private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        //private DevExpress.XtraBars.PopupMenu popupMenu1;
        //private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        #endregion

        public frmQL10Template()
        {
            InitializeComponent();
            IDField = "ID";
            new PhieuQuanLy10Fix(this);
        }

        /// <summary>Step 1: Xác định các cột sẽ hiển thị trong phần master gridView 
        /// Chú ý không được khởi tạo qua phần giao diện kéo thả ( Chỉ Viết Code )
        /// </summary>
        public override void InitColumnMaster()
        {
            GridColumn Cot_ID = new GridColumn();
            Cot_ID.Caption = "ID";
            Cot_ID.VisibleIndex = - 1;
            Cot_ID.FieldName = "ID";
            this.gridViewMaster.Columns.Add(Cot_ID);

            GridColumn Cot_TenNhom = new GridColumn();
            Cot_TenNhom.Caption = "Tên nhóm";
            Cot_TenNhom.VisibleIndex = 0;
            Cot_TenNhom.FieldName = "NAME";
            this.gridViewMaster.Columns.Add(Cot_TenNhom);

            //XtraGridSupportExt.IntegerGridColum(Cot_MGrid_SoLuong, SO_LUONG);            
        }

        /// <summary>Step 2: Xác định các cột sẽ hiển thị trong phần detail  
        /// Chú ý không được khởi tạo qua phần giao diện kéo thả ( Chỉ Viết Code )
        /// </summary>
        public override void InitColumDetail()
        {
            GridColumn Cot_ID = new GridColumn();
            Cot_ID.Caption = "ID";
            Cot_ID.VisibleIndex = - 1;
            Cot_ID.FieldName = "ID";
            this.gridViewDetail.Columns.Add(Cot_ID);

            GridColumn Cot_TenNhom = new GridColumn();
            Cot_TenNhom.Caption = "Tên sản phẩm";
            Cot_TenNhom.VisibleIndex = 0;
            Cot_TenNhom.FieldName = "NAME";
            this.gridViewDetail.Columns.Add(Cot_TenNhom);

            //XtraGridSupportExt.IntegerGridColum(Cot_DGrid_SoLuong, SO_LUONG);            
        }

        /// <summary>Step 3: Xác định các control trong filter part và Khởi tạo control trong phần filter.
        /// Các control trong phần filter này là những control chỉ chọn
        /// </summary>
        public override void PLLoadFilterPart()
        {

        }

        #region Step 4 - Cài đặt menu
        public override _MenuItem GetBusinessMenuList()
        {
            return null;
        }

        public override _MenuItem GetMenuAppendGridList()
        {
            return null;
        }
        #endregion

        /// <summary>Step 5: Xây dựng Query Buider cho việc tìm kiếm
        /// Xây dựng một QueryBuilder từ những chọn lựa trong phần filter.
        /// Từ QueryBuilder này ta có thể lấy được dữ liệu thỏa điều kiện.
        /// Nếu hỗ trợ duyệt thì trong câu truy vấn trả về 
        /// phải có cột là DUYET_BIT
        /// </summary>
        public override QueryBuilder PLBuildQueryFilter()
        {
            QueryBuilder query = null;
            query = new QueryBuilder(
                "SELECT * " + 
                "FROM TEST_PRODUCT_GROUP " + 
                "WHERE 1=1"         
            );
            query.addID("NOOP", -1);
            return query;
        }

        #region Step 7: Xác định các form xử lý khi chọn Thêm, Xem , Sửa

        public override void ShowViewForm(long id)
        {
            HelpMsgBox.ShowNotificationMessage("Xem phiếu có ID : " + id);
        }

        public override void ShowUpdateForm(long id)
        {
            HelpMsgBox.ShowNotificationMessage("Cập nhật phiếu có ID : " + id);
        }

        public override long[] ShowAddForm()
        {
            HelpMsgBox.ShowNotificationMessage("Thêm phiếu");
            return null;
        }

        #endregion

        #region Step 8 : Xác định các hành động trên form
        /// <summary>Thực hiện câu lệnh để xóa phiếu có id = id 
        /// </summary>        
        public override bool? XoaAction(long id)
        {
            return true;
        }

        /// <summary>Cấu hình thông tin In
        /// </summary>        
        public override _Print InAction(long []ids)
        {
            return null;
        }
        #endregion

        public override string UpdateRow()
        {
            return "";
        }
    }
}