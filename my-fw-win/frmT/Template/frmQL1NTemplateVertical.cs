using System.Data;
using DevExpress.XtraGrid.Columns;
using ProtocolVN.Framework.Core;
using DevExpress.XtraEditors;
namespace ProtocolVN.Framework.Win.Demo
{
    //public partial class frmQL1NTemplateVertical : XtraFormPL
    public partial class frmQL1NTemplateVertical : PhieuQuanLyChange1N
    {
        #region Các biến của form Quan Ly Tuyệt đối không thay đổi
        //public DevExpress.XtraBars.BarManager barManager1;
        //public DevExpress.XtraBars.Bar MainBar;
        //public DevExpress.XtraBars.BarButtonItem barButtonItemAdd;
        //public DevExpress.XtraBars.BarButtonItem barButtonItemDelete;
        //public DevExpress.XtraBars.BarButtonItem barButtonItemUpdate;
        //public DevExpress.XtraBars.BarButtonItem barButtonItemPrint;
        //public DevExpress.XtraBars.BarDockControl barDockControlTop;
        //public DevExpress.XtraBars.BarDockControl barDockControlBottom;
        //public DevExpress.XtraBars.BarDockControl barDockControlLeft;
        //public DevExpress.XtraBars.BarDockControl barDockControlRight;
        //public DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        //public DevExpress.XtraGrid.GridControl gridControlMaster;
        //public DevExpress.XtraGrid.Views.Grid.PLGridView gridViewMaster;
        //public DevExpress.XtraTab.XtraTabControl xtraTabControlDetail;
        //public DevExpress.XtraBars.PopupControlContainer popupControlContainerFilter;
        //public DevExpress.XtraBars.BarButtonItem barButtonItemCommit;
        //public DevExpress.XtraBars.BarButtonItem barButtonItemNoCommit;
        //public DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        //public DevExpress.XtraBars.BarSubItem barSubItem1;
        //public DevExpress.XtraBars.BarButtonItem barButtonItemXem;
        //public DevExpress.XtraBars.BarButtonItem barButtonItemClose;
        //public DevExpress.XtraBars.PopupMenu popupMenuFilter;
        //public DevExpress.XtraBars.BarCheckItem barCheckItemFilter;
        //public DevExpress.XtraBars.BarButtonItem barButtonItemSearch;
        //public DevExpress.XtraBars.BarButtonItem barButtonItem3;
        //public DevExpress.XtraBars.PopupMenu popupMenu1;
        //public DevExpress.XtraBars.BarButtonItem barButtonItem4;
        //public System.ComponentModel.IContainer components;
        #endregion

        #region Vùng dữ liệu cho demo
        DevExpress.XtraTab.XtraTabPage xtraTabPageDetail;
        DevExpress.XtraGrid.GridControl gridControlDetail;
        DevExpress.XtraGrid.Views.Grid.PLGridView gridViewDetail;

        private void Create1GridForDemo()
        {
            xtraTabPageDetail = new DevExpress.XtraTab.XtraTabPage();
            gridControlDetail = new DevExpress.XtraGrid.GridControl();
            gridViewDetail = new DevExpress.XtraGrid.Views.Grid.PLGridView();
            gridControlDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            gridControlDetail.EmbeddedNavigator.Name = "";
            gridControlDetail.Location = new System.Drawing.Point(0, 0);
            gridControlDetail.MainView = gridViewDetail;
            gridControlDetail.Name = "gridControlDetail";
            gridControlDetail.Size = new System.Drawing.Size(776, 165);
            gridControlDetail.TabIndex = 9;
            gridControlDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridViewDetail});
            gridViewDetail.GridControl = gridControlDetail;
            gridViewDetail.Name = "gridViewDetail";
            gridViewDetail.OptionsView.ShowGroupPanel = false;
            gridViewDetail.OptionsBehavior.Editable = false;
            gridViewDetail.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            
            xtraTabPageDetail.Controls.Add(gridControlDetail);
            xtraTabPageDetail.Name = "xtraTabPageDetail";
            xtraTabPageDetail.Size = new System.Drawing.Size(776, 165);
            xtraTabPageDetail.Text = "Chi tiết";
            xtraTabControlDetail.Controls.Add(xtraTabPageDetail);
            xtraTabControlDetail.SelectedTabPage = xtraTabPageDetail;
        }
        #endregion

        public frmQL1NTemplateVertical()
        {
            InitializeComponent();
            Create1GridForDemo();
            IDField = "ID";
            new PhieuQuanLyFix1N(this);
            
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
        public override bool XoaAction(long id)
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

        public override DataTable[] PLLoadDataDetailParts(long MasterID)
        {
            QueryBuilder query = null;
            query = new QueryBuilder(
                "SELECT * " +
                "FROM TEST_PRODUCT " +
                "WHERE 1=1"
            );
            query.addID("CAT_ID", MasterID);
            if (query != null)
                return new DataTable[] { DABase.getDatabase().LoadDataSet(query, "DETAIL").Tables[0] };
            return null;
        }

        public override string UpdateRow()
        {
            return null;
        }
    }
}