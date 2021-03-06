using DevExpress.XtraGrid.Columns;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win.Demo
{
    //public partial class frmTimPopupTemplateVertical : XtraFormPL
    public partial class frmTimPopupTemplateVertical : TimPopupChange
    {
        #region Các biến của form Quan Ly Tuyệt đối không thay đổi
        //public DevExpress.XtraBars.BarManager barManager1;
        //public DevExpress.XtraBars.Bar MainBar;
        //public DevExpress.XtraBars.BarButtonItem barButtonItemAdd;
        //public DevExpress.XtraBars.BarButtonItem barButtonItemPrint;
        //public DevExpress.XtraBars.BarDockControl barDockControlTop;
        //public DevExpress.XtraBars.BarDockControl barDockControlBottom;
        //public DevExpress.XtraBars.BarDockControl barDockControlLeft;
        //public DevExpress.XtraBars.BarDockControl barDockControlRight;
        //public DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        //public DevExpress.XtraGrid.GridControl gridControlMaster;
        //public DevExpress.XtraGrid.Views.Grid.PLGridView gridViewMaster;
        //public DevExpress.XtraTab.XtraTabControl xtraTabControlDetail;
        //public DevExpress.XtraTab.XtraTabPage xtraTabPageDetail;
        //public DevExpress.XtraBars.PopupControlContainer popupControlContainerFilter;
        //public DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        //public DevExpress.XtraBars.BarButtonItem barButtonItemClose;
        //public DevExpress.XtraBars.PopupMenu popupMenuFilter;
        //public DevExpress.XtraBars.BarCheckItem barCheckItemFilter;
        //public DevExpress.XtraBars.BarButtonItem barButtonItemSearch;
        //public DevExpress.XtraBars.BarButtonItem barButtonItem3;
        //public DevExpress.XtraBars.PopupMenu popupMenu1;
        //public DevExpress.XtraBars.BarButtonItem barButtonItem4;
        //public DevExpress.XtraBars.BarButtonItem barButtonItemChon;
        //public System.ComponentModel.IContainer components;
        #endregion

        public frmTimPopupTemplateVertical()
        {
            InitializeComponent();
            IDField = "ID";
            new TimPopupFix(this);
        }

        #region Phần Thay đổi ( Chú ý chỉ thay đổi phần nội dung không thay đổi tên phương thức và tham số của phương thức )

        public override void InitColumnMaster()
        {
            GridColumn Cot_ID = new GridColumn();
            Cot_ID.Caption = "ID";
            Cot_ID.VisibleIndex = -1;
            Cot_ID.FieldName = "ID";
            this.gridViewMaster.Columns.Add(Cot_ID);

            GridColumn Cot_TenNhom = new GridColumn();
            Cot_TenNhom.Caption = "Tên nhóm";
            Cot_TenNhom.VisibleIndex = 0;
            Cot_TenNhom.FieldName = "NAME";
            this.gridViewMaster.Columns.Add(Cot_TenNhom);
            //XtraGridSupportExt.IntegerGridColum(Cot_MGrid_SoLuong, SO_LUONG);            
        }

        public override void PLLoadFilterPart()
        {

        }
        public override _MenuItem GetMenuAppendGridList()
        {
            return null;
        }
        #endregion

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

        public override void ShowViewForm(long id)
        {
            HelpMsgBox.ShowNotificationMessage("Xem phiếu có ID : " + id);
        }

        public override bool ShowAddForm()
        {
            HelpMsgBox.ShowNotificationMessage("Thêm phiếu");
            return true;
        }

        public override _Print InAction(long[] ids)
        {
            return null;
        }
        
    }
}