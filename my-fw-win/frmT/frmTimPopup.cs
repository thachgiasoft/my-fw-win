using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using ProtocolVN.Framework.Core;
using DevExpress.XtraGrid.Views.Grid;
using System.Windows.Forms;
namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Để xây dựng một phiếu quản lý ta làm các bước theo thứ tự sau
    /// 1.Tạo một bản sao BlankTimPopup
    /// 2.Thay tên lớp tương ứng với phiếu quản lý mình xây dựng
    /// 3.Thay mở rộng TimPopupChange thành XtraForm
    /// 4.Mở bản thiết kế của nó lên để kéo thả các control trong phần filter
    /// ( Xác định chính xác 1 lần và sẽ không trở lại design để coi nữa )
    /// 5.Mở file design của lớp Comment lại các biến trong phần biến không thay đổi
    /// 6.Thay mở rộng XtraForm về TimPopupChange
    /// 7.Tiến hành viết code phần của mình.
    /// </summary>
    
    public abstract class TimPopupChange : XtraFormPL
    {
        #region Thuộc tính ko sử dụng
        public DevExpress.XtraBars.BarStaticItem barStaticItem1;
        public DevExpress.XtraBars.BarButtonItem barButtonItem1;
        public DevExpress.XtraBars.BarButtonItem barButtonItem2;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        #endregion

        public DevExpress.XtraBars.BarManager barManager1;
        public DevExpress.XtraBars.Bar MainBar;
        public DevExpress.XtraBars.BarButtonItem barButtonItemAdd;
        public DevExpress.XtraBars.BarButtonItem barButtonItemPrint;
        public DevExpress.XtraBars.BarDockControl barDockControlTop;
        public DevExpress.XtraBars.BarDockControl barDockControlBottom;
        public DevExpress.XtraBars.BarDockControl barDockControlLeft;
        public DevExpress.XtraBars.BarDockControl barDockControlRight;
        public DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        public DevExpress.XtraGrid.GridControl gridControlMaster;
        public DevExpress.XtraGrid.Views.Grid.PLGridView gridViewMaster;
        public DevExpress.XtraTab.XtraTabControl xtraTabControlDetail;
        public DevExpress.XtraTab.XtraTabPage xtraTabPageDetail;
        public DevExpress.XtraBars.PopupControlContainer popupControlContainerFilter;
        public DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        public System.ComponentModel.IContainer components;
        public DevExpress.XtraBars.BarButtonItem barButtonItemClose;
        public DevExpress.XtraBars.PopupMenu popupMenuFilter;
        public DevExpress.XtraBars.BarCheckItem barCheckItemFilter;
        public DevExpress.XtraBars.BarButtonItem barButtonItemSearch;
        public DevExpress.XtraBars.BarButtonItem barButtonItem3;
        public DevExpress.XtraBars.PopupMenu popupMenu1;
        public DevExpress.XtraBars.BarButtonItem barButtonItem4;
        public DevExpress.XtraBars.BarButtonItem barButtonItemChon;

        public DevExpress.XtraBars.BarSubItem barSubXuatRaFile = null;
        public QueryBuilder filter = null;

        #region Tham số tùy biến
        public string IDField = "ID";       
        public bool SupportThem = false;    //Có hay không hỗ trợ thêm
        public bool SupportIn = false;      //Có hay không hỗ trợ In
        public bool SupportChon = true;     //Có hay không hỗ trợ chọn
        public ArrayList SelectedItems;     //Danh sách các ID được chọn
        public DataSet SelectedDataSet;     //DataSet gồm các dòng đã chọn
        public bool? _PLAutoWidth = false;
        public bool _UsingExportToFileItem = true;  //Gắn ExportToFile Item vào màn hình quản lý.
        #endregion

        #region Xử lý tùy biến

        /// <summary>Khởi tạo column phần master
        /// </summary>
        public abstract void InitColumnMaster();

        /// <summary>Khai báo Menu Nghiệp Vụ
        /// </summary>
        public abstract void PLLoadFilterPart();

        /// <summary>Khai báo chọn lựa Append Context Menu 
        /// </summary>
        public virtual _MenuItem GetMenuAppendGridList() { return null; }

        /// <summary>Xây dựng một QueryBuilder từ những chọn lựa trong phần filter.
        /// </summary>
        public abstract QueryBuilder PLBuildQueryFilter();

        /// <summary>Hàm xử lý khi nhấn Xem.
        /// "id" là ID của phần tử đang Focus.(int)
        /// </summary>
        public abstract void ShowViewForm(long id);

        /// <summary>Hàm xử lý khi nhấn Thêm.
        /// Kết quả trả về là mảng các ID vừa thêm.
        /// </summary>
        public virtual bool ShowAddForm() { return true; }

        /// <summary>Xử lý in các phiếu đang chọn
        /// ids là ID của các phiếu đang chọn
        /// </summary>   
        public virtual _Print InAction(long[] ids) { return null; }

        /// <summary>Hàm được gọi sau khi xử lý FocusRow
        /// Giúp có thể cán thiệp xử lý riêng.
        /// </summary>
        public virtual void HookFocusRow() { }

        /// <summary>Hàm cho phép chèn đoạn xử lý dataSet sau khi thực hiện câu truy vấn đã lưu
        /// Hiện tại giá trị trả về chưa sử dụng
        /// </summary>
        /// <param name="dataSet"></param>
        public virtual object HookAfterExecAdvQuery(DataSet dataSet) { return null; }

        #endregion
    }

    public class TimPopupFix
    {
        TimPopupChange that;
        string dataSetID = null;
        string saveQuery = null;

        public TimPopupFix(TimPopupChange phieuQL, string query):
            this(phieuQL, null, query)
        {}

        public TimPopupFix(TimPopupChange phieuQL, string dataSetID, string query)
        {
            this.that = phieuQL;
            if (dataSetID == null)
                this.dataSetID = that.GetType().FullName + that.gridViewMaster._GetPLGUI();
            else
                this.dataSetID = dataSetID + that.gridViewMaster._GetPLGUI();
            this.saveQuery = query;
            HamDung();
        }

        public TimPopupFix(TimPopupChange phieuQL)
        {
            this.that = phieuQL;
            HamDung();
        }

        /// <summary>Hàm dựng cho Form Quản Lý
        /// </summary>
        private void HamDung()
        {
            WinLaw.checkLaw(this);

            //Tùy biến không cho phép chỉnh sửa trên bar
            that.MainBar.Text = "Chức năng";
            that.MainBar.Manager.AllowCustomization = false;
            that.MainBar.OptionsBar.AllowDelete = false;
            that.MainBar.OptionsBar.DisableClose = true;
            that.MainBar.OptionsBar.DisableCustomization = true;

            //that.barButtonItemClose.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //Xử lý hình
            that.barButtonItemAdd.Glyph = FWImageDic.ADD_IMAGE20;
            that.barButtonItemPrint.Glyph = FWImageDic.PRINT_IMAGE20;
            that.barButtonItemClose.Glyph = FWImageDic.CLOSE_IMAGE20;
            that.barButtonItemSearch.Glyph = FWImageDic.FIND_IMAGE20;
            that.barCheckItemFilter.Glyph = FWImageDic.FILTER_IMAGE20;
            that.barButtonItemChon.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            that.barButtonItemChon.Glyph = FWImageDic.CHOICE_IMAGE20;
            //Xử lý cột trong lưới
            try { 
                that.InitColumnMaster(); 
            }
            catch (Exception exception) { 
                PLException.AddException(exception); 
            }
            //Xử lý in
            if (that.SupportIn)
                that.barButtonItemPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemPrint_ItemClick);                
            else
                this.that.barButtonItemPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //Xử lý thêm
            if (that.SupportThem)
                that.barButtonItemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemAdd_ItemClick);
            else
                this.that.barButtonItemAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //Xử lý chọn
            if (that.SupportChon)
                that.barButtonItemChon.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemChon_Click);            
            else
                this.that.barButtonItemChon.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //Xử lý sự kiện
            that.barButtonItem4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem4_ItemClick);
            that.barButtonItemSearch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemView_ItemClick);
            that.barCheckItemFilter.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItemFilter_CheckedChanged);
            that.barButtonItemClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemClose_ItemClick);
            that.gridViewMaster.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewMaster_FocusedRowChanged);
            that.gridViewMaster.DoubleClick += new System.EventHandler(this.gridViewMaster_DoubleClick);
            that.Load += new System.EventHandler(this.PhieuQL_Load);

            //Layout
            if (that._PLAutoWidth != null)
            {
                if (that._PLAutoWidth == true)
                {
                    ((PLGridView)that.gridViewMaster)._SetAutoLayout();
                }
                else
                {
                    //((PLGridView)that.gridViewMaster)._SetUserLayout(null);
                }
            }

            that.WindowState = FormWindowState.Maximized;
            HelpGrid.MoveGroupTextPanelToTitle(that.gridViewMaster);

            if (saveQuery != null)
            {
                if (dataSetID != null)
                {
                    //HelpControl.addSaveQueryDialog(that, that.barManager1, that.gridControlMaster,
                    //    dataSetID, saveQuery);
                    HelpControl.addSaveQueryDialog(that, that.barManager1, that.popupMenuFilter, that.gridControlMaster,
                        dataSetID, saveQuery, that.HookAfterExecAdvQuery);
                }
                else
                {
                    //HelpControl.addSaveQueryDialog(that, that.barManager1, that.gridControlMaster, 
                    //    that.gridViewMaster._GetPLGUI(), saveQuery);
                    HelpControl.addSaveQueryDialog(that, that.barManager1, that.popupMenuFilter, that.gridControlMaster,
                        that.gridViewMaster._GetPLGUI(), saveQuery, that.HookAfterExecAdvQuery);
                }
            }

            that.barCheckItemFilter.Caption = "Hiện điều &kiện tìm kiếm";
            if (that._UsingExportToFileItem == true)
                that.barSubXuatRaFile = PhieuQuanLyUtil.XuatRaFile(that.barManager1, (GridView)that.gridViewMaster);
        }

        protected void PhieuQL_Load(object sender, EventArgs e)
        {
            try
            {
                this._setContextMenuOnGrid();
                this.PLCustomView();
                PLDaChonMasterRecord(false);
                that.PLLoadFilterPart();
            }
            catch
            {
                if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
            }
        }

        #region Phần phiếu quản lý (giống hoàn toàn)
        /// <summary>Sự kiên chọn IN
        /// </summary>
        private void barButtonItemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _Print print = null;
            try
            {
                long[] ids = null;
                if (that.IDField!=null && that.IDField != "")
                    ids = PhieuQuanLyUtil.GetSelectedID(that.gridViewMaster, that.IDField);
                print = that.InAction(ids);
                print.execDirectlyPrint();                
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
                PLMessageBox.ShowErrorMessage("File " + print.ReportNameFile + " không tồn tại. Vui lòng kiểm tra lại hoặc liên hệ PROTOCOL.");
            }
        }

        /// <summary>Sự kiện xem trước khi in
        /// </summary>
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _Print print = null;
            try
            {   long[] ids = null;
                if (that.IDField!=null && that.IDField != "")
                    ids = PhieuQuanLyUtil.GetSelectedID(that.gridViewMaster, that.IDField);
                print = that.InAction(ids);
                print.execPreviewWith();                
            }
            catch(Exception ex)
            {
                PLException.AddException(ex);
                PLMessageBox.ShowErrorMessage("File "+ print.ReportNameFile +" không tồn tại. Vui lòng kiểm tra lại hoặc liên hệ PROTOCOL.");
            }
        }
        #endregion

        #region Phần phiếu tìm kiếm (1 - 0)
        /// <summary>Sự kiên chọn "Chọn"
        /// </summary>
        private void barButtonItemChon_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (that.IDField != null && that.IDField != String.Empty)
            {
                List<DataRow> data = _getSelectedDatarowsOnGrid(that.gridViewMaster);
                if (data.Count > 0)
                {
                    that.SelectedItems = new ArrayList();
                    that.SelectedDataSet = ((DataView)that.gridViewMaster.DataSource).DataViewManager.DataSet.Clone();
                    foreach (DataRow var in data)
                    {
                        that.SelectedItems.Add(var[that.IDField]);
                        that.SelectedDataSet.Tables[0].Rows.Add(var.ItemArray);
                    }
                    that.Dispose();
                }
                else
                {
                    HelpMsgBox.ShowNotificationMessage("Vui lòng chọn dữ liệu !");
                }
            }
        }
        ///// <summary>Sự kiện khi double click vào mẫu tin trong phần Master
        ///// </summary>
        private void gridViewMaster_DoubleClick(object sender, EventArgs e)
        {
            if (that.SupportChon && 
                that.gridViewMaster.SelectedRowsCount > 0 &&
                that.gridViewMaster.FocusedRowHandle >= 0
                )
            {
                this.barButtonItemChon_Click(null, null);
            }
        }
        #endregion

        /// <summary>Sự kiện khi thay đổi focus trên mẫu tin phần gridViewMaster
        /// </summary>
        private void gridViewMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (that.gridViewMaster.SelectedRowsCount > 0 &&
                that.gridViewMaster.FocusedRowHandle > -1)
            {
                PLDaChonMasterRecord(true);
                that.HookFocusRow();
            }
            else
            {
                PLDaChonMasterRecord(false);
            }
        }
        
        /// <summary>Sự kiện khi chọn Tìm kiếm
        /// </summary>
        private void barButtonItemView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PLRefresh();
        }

        /// <summary>Sự kiện khi chọn "Thêm".
        /// </summary>
        protected void barButtonItemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (that.ShowAddForm()) PLRefresh();
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
        }
        protected void PLCustomView()
        {
            that.barCheckItemFilter.Checked = true;
            that.popupControlContainerFilter.Show();

            HelpGrid.ShowNumOfRecord(that.gridControlMaster);

            that.gridViewMaster.OptionsSelection.EnableAppearanceFocusedCell = false;
            that.gridViewMaster.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            that.gridViewMaster.OptionsBehavior.Editable = false;
            that.gridViewMaster.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            that.gridViewMaster.OptionsCustomization.AllowGroup = true;            
        }

        /// <summary>Hàm cập nhật dữ liệu hiển thị trong phần master dựa vào điều kiện lọc
        /// </summary>
        public void PLLoadMasterPart(DevExpress.XtraGrid.GridControl MasterGrid)
        {
            try
            {
                DataSet data = DABase.getDatabase().LoadReadOnlyDataSet(that.filter);
                MasterGrid.DataSource = data.Tables[0];
            }
            catch (Exception ex) {
                PLException.AddException(ex);
            }
        }
        
        /// <summary> Thay đổi trạng thái các button khi chọn hay bỏ chọn một phần tử trong Master 
        /// </summary>
        /// <param name="status"></param>
        public void PLDaChonMasterRecord(bool status)
        {
            if (status)
            {
                that.barButtonItemPrint.Enabled = true;
            }
            else
            {
                that.barButtonItemPrint.Enabled = false;                
            }
        }       
        /// <summary> Sự kiên ẩn hiện filter.
        /// </summary>
        private void barCheckItemFilter_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (that.barCheckItemFilter.Checked)
                that.popupControlContainerFilter.Show();
            else
                that.popupControlContainerFilter.Hide();
        }

        /// <summary>Sự kiện khi nhấn vào nút đóng
        /// </summary>
        private void barButtonItemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            that.Close();
        }

        /// <summary>Refresh lại lưới khi Loc dữ liệu
        /// </summary>
        public void PLRefresh()
        {
            try
            {
                that.filter = that.PLBuildQueryFilter();
                if (that.filter != null && !that.filter.isEmpty())
                    PLLoadMasterPart(that.gridControlMaster);
                else
                {
                    if(that.filter != null) HelpMsgBox.ShowNotificationMessage("Vui lòng chọn điều kiện tìm !");
                }
                that.gridViewMaster.ClearSelection();
                
                if (that.gridViewMaster.RowCount > 0)
                {
                    that.gridViewMaster.SelectRow(0);
                    that.gridViewMaster.FocusedRowHandle = 0;
                }
                else
                {
                    that.gridViewMaster.SelectRow(-1);
                }
                this.gridViewMaster_FocusedRowChanged(null, null);
            }
            catch (Exception ex) {
                PLException.AddException(ex);
            }
        }

        /// <summary>Gắn menu vào grid 
        /// </summary>
        private void _setContextMenuOnGrid()
        {
            try
            {
                _MenuItem tempMenu = that.GetMenuAppendGridList();
                if (tempMenu != null)
                {
                    HelpGrid.AddMenuToGridView(that.gridControlMaster, tempMenu.FieldName, tempMenu.CaptionNames, tempMenu.ImageNames, tempMenu.Funcs);
                }
            }
            catch(Exception ex){
                PLException.AddException(ex);
            }
        }
       
        /// <summary>Lấy những dòng đã chọn trên GridView
        /// </summary>
        public List<DataRow> _getSelectedDatarowsOnGrid(DevExpress.XtraGrid.Views.Grid.PLGridView gridView)
        {
            return PhieuQuanLyUtil.GetSelectedDataRow(gridView);            
        }
    }
}
