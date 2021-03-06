using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public abstract class PhieuQuanLyChange : XtraFormPL
    {
        #region Thuộc tính ko sử dụng
        public DevExpress.XtraBars.BarButtonItem barButtonItem1;
        public DevExpress.XtraBars.BarButtonItem barButtonItem2;
        public DevExpress.XtraBars.BarStaticItem barStaticItem1;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        #endregion

        public DevExpress.XtraBars.BarManager barManager1;
        public DevExpress.XtraBars.Bar MainBar;
        public DevExpress.XtraBars.BarButtonItem barButtonItemAdd;
        public DevExpress.XtraBars.BarButtonItem barButtonItemDelete;
        public DevExpress.XtraBars.BarButtonItem barButtonItemUpdate;
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
        public DevExpress.XtraGrid.GridControl gridControlDetail;
        public DevExpress.XtraGrid.Views.Grid.PLGridView gridViewDetail;
        public DevExpress.XtraBars.BarButtonItem barButtonItemCommit;
        public DevExpress.XtraBars.BarButtonItem barButtonItemNoCommit;
        public DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        public System.ComponentModel.IContainer components;
        public DevExpress.XtraBars.BarSubItem barSubItem1;
        public DevExpress.XtraBars.BarButtonItem barButtonItemXem;
        public DevExpress.XtraBars.BarButtonItem barButtonItemClose;
        public DevExpress.XtraBars.PopupMenu popupMenuFilter;
        public DevExpress.XtraBars.BarCheckItem barCheckItemFilter;
        public DevExpress.XtraBars.BarButtonItem barButtonItemSearch;
        public DevExpress.XtraBars.BarButtonItem barButtonItem3;
        public DevExpress.XtraBars.PopupMenu popupMenu1;
        public DevExpress.XtraBars.BarButtonItem barButtonItem4;
        public DevExpress.XtraBars.BarSubItem barSubItemExport;
        public QueryBuilder filter = null;          //Chứa dữ liệu của điều kiện lọc

        #region Tham số tùy biến
        public string GroupByClause = "";
        public string ASCSortClause = "";
        public string DESCSortClause = "";
        public bool IncludeNghiepVu = true;         //Có hay không hỗ trợ nghiệp vụ
        public bool AllowUpdatePhieuDuyet = true;   //Cho phép không duyệt phiếu đã duyệt
        public string IDField = "";                 //Field ID của phiếu
        public string DisplayField = "";            //Field đại diện cho phép thường mã phiếu
        public bool IsContextMenu = false;          //Có hay không hỗ trợ right click
        public bool InSupport = true;               //Có hay không hỗ trợ in
        public bool? _PLAutoWidth = false;
        public bool _UsingExportToFileItem = true;  //Gắn ExportToFile Item vào màn hình quản lý.

        public string _msgConfirmBeforeDelete = "Bạn có chắc muốn 'Xóa' phiếu \"{0}\" ?";
        public string _msgConfirmBeforeDuyet = "Bạn có chắc muốn 'Duyệt' phiếu \"{0}\" ?";
        public string _msgConfirmBeforeKhongDuyet = "Bạn có chắc muốn 'Không duyệt' phiếu \"{0}\" ?";
        //public string _msgNotifyDeleteOK = "";
        public string _msgNotifyDeleteFail = "Thao tác 'Xóa' phiếu \"{0}\" thực hiện không thành công. Vui lòng kiểm tra lại dữ liệu.";
        //public string _msgNotifyDuyetOK = "";
        public string _msgNotifyDuyetFail = "Thao tác 'Duyệt' phiếu \"{0}\" thực hiện không thành công.";
        //public string _msgNotifyKhongDuyetOK = "";
        public string _msgNotifyKhongDuyetFail = "Thao tác 'Không duyệt' phiếu \"{0}\" thực hiện không thành công.";
        public string _msgChuaChonDuLieu = "Vui lòng chọn dữ liệu !";
        public bool _UseSplitControlEvent = true;

        public bool _UseParentDeleteEvent = true;
        #endregion

        #region Xử lý tùy biến
        /// <summary>Khởi tạo column phần master
        /// </summary>
        public abstract void InitColumnMaster();

        /// <summary>Khởi tạo column phần detail
        /// </summary>
        public abstract void InitColumDetail();

        /// <summary>Khởi tạo ctrl trong phần filter
        /// </summary>
        public abstract void PLLoadFilterPart();

        /// <summary>Khai báo Menu Nghiệp Vụ
        /// </summary>
        public virtual _MenuItem GetBusinessMenuList() { return null; }

        /// <summary>Khai báo chọn lựa Append Context Menu 
        /// </summary>
        public virtual _MenuItem GetMenuAppendGridList() { return null; }

        /// <summary>Xây dựng một QueryBuilder từ những chọn lựa trong phần filter.
        /// </summary>
        public abstract QueryBuilder PLBuildQueryFilter();

        /// <summary>Hàm lấy dữ liệu phần detail
        /// "MasterID" là ID của phần tử đang Focus.
        /// </summary>
        public abstract DataTable PLLoadDataDetailPart(long MasterID);
        
        /// <summary>Hàm xử lý khi nhấn Xem.
        /// "id" là ID của phần tử đang Focus.(int)
        /// </summary>
        public abstract void ShowViewForm(long id);

        /// <summary>Hàm xử lý khi nhấn Cập nhật.
        /// "id" là ID của phần tử đang Focus.(int)
        /// </summary>
        public abstract void ShowUpdateForm(long id);

        /// <summary>Hàm xử lý khi nhấn Thêm.
        /// Kết quả trả về là mảng các ID vừa thêm.
        /// </summary>
        public abstract long[] ShowAddForm();

        /// <summary>Hàm xử lý khi nhấn Xóa.
        /// "id" là ID của phần tử đang Focus.(int)
        /// </summary>
        public abstract bool XoaAction(long id);

        /// <summary>Xử lý in các phiếu đang chọn
        /// ids là ID của các phiếu đang chọn
        /// </summary>   
        public virtual _Print InAction(long[] ids) { return null; }
        
        /// <summary>Câu truy vấn dùng để update lại dòng đang chọn
        /// sau khi cập nhật hoặc thêm.
        /// </summary>
        public virtual string UpdateRow() { return null; }
        
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

    public class PhieuQuanLyFix:IPhieuFix
    {
        PhieuQuanLyChange that;
        bool find = false;          //Đã thực hiện tìm kiếm
        bool DuyetSupport = false;  //Có hỗ trợ duyệt không
        string dataSetID = null;
        string saveQuery = null;

        public PhieuQuanLyFix(PhieuQuanLyChange phieuQL, string fullQuery)
            : this(phieuQL, null, fullQuery) { }

        public PhieuQuanLyFix(PhieuQuanLyChange phieuQL, string dataSetID, string query)
        {
            this.that = phieuQL;
            if ((phieuQL as IDuyetSupport) != null) this.DuyetSupport = true;
            if (dataSetID == null)
                this.dataSetID = this.that.GetType().FullName + that.gridViewMaster._GetPLGUI();
            else
                this.dataSetID = dataSetID + that.gridViewMaster._GetPLGUI();
            this.saveQuery = query;
            
            HamDung();
        }

        public PhieuQuanLyFix(PhieuQuanLyChange phieuQL)
        {
            this.that = phieuQL;
            if ((phieuQL as IDuyetSupport) != null) this.DuyetSupport = true;
            HamDung();
        }
        
        #region Phần phiếu quản lý ( giống hoàn toàn )
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

            that.barButtonItemClose.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //Xử lý hình
            that.barButtonItemAdd.Glyph = FWImageDic.ADD_IMAGE20;
            that.barButtonItemXem.Glyph = FWImageDic.VIEW_IMAGE20;
            that.barButtonItemDelete.Glyph = FWImageDic.DELETE_IMAGE20;
            that.barButtonItemUpdate.Glyph = FWImageDic.EDIT_IMAGE20;
            that.barButtonItemPrint.Glyph = FWImageDic.PRINT_IMAGE20;
            that.barButtonItemClose.Glyph = FWImageDic.CLOSE_IMAGE20;
            that.barButtonItemCommit.Glyph = FWImageDic.COMMIT_IMAGE20;
            that.barButtonItemNoCommit.Glyph = FWImageDic.UNCOMMIT_IMAGE20;
            that.barSubItem1.Glyph = FWImageDic.BUSINESS_IMAGE20;
            that.barButtonItemSearch.Glyph = FWImageDic.FIND_IMAGE20;
            that.barCheckItemFilter.Glyph = FWImageDic.FILTER_IMAGE20;
            //Xử lý cột trong lưới
            try
            {
                that.InitColumnMaster();
                that.InitColumDetail();
            }
            catch (Exception exception)
            {
                PLException.AddException(exception);
                PLMessageBoxDev.ShowMessage(this, exception.Message);                
            }
            //Xử lý duyệt
            if (this.DuyetSupport)
            {
                GridColumn column = new GridColumn();
                XtraGridSupportExt.CreateDuyetGridColumn(column);
                that.gridViewMaster.Columns.Add(column);
            }
            else
            {
                that.barButtonItemCommit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                that.barButtonItemNoCommit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            //Xử lý in
            if (that.InSupport == false)
            {
                that.barButtonItemPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            else
            {
                that.barButtonItemPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemPrint_ItemClick);
            }
            //Xử lý sự kiện            
            that.barButtonItem4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem4_ItemClick);
            that.barButtonItemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemAdd_ItemClick);
            that.barButtonItemXem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemXem_ItemClick);
            if (that._UseParentDeleteEvent)
                that.barButtonItemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemDelete_ItemClick);
            that.barButtonItemUpdate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemUpdate_ItemClick);
            that.barButtonItemCommit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemCommit_ItemClick);
            that.barButtonItemNoCommit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemNoCommit_ItemClick);
            that.barButtonItemSearch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemView_ItemClick);
            that.barCheckItemFilter.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItemFilter_CheckedChanged);
            that.barButtonItemClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemClose_ItemClick);
            if (that._UseSplitControlEvent)
                that.splitContainerControl1.Resize += new System.EventHandler(this.splitContainerControl1_Resize);
            that.gridViewMaster.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewMaster_FocusedRowChanged);
            that.gridViewMaster.DoubleClick += new System.EventHandler(this.gridViewMaster_DoubleClick);
            that.Load += new System.EventHandler(this.PhieuQL_Load);
            //Layout
            if(that._PLAutoWidth != null){
                if (that._PLAutoWidth == true)
                {
                    ((PLGridView)that.gridViewDetail)._SetAutoLayout();
                    ((PLGridView)that.gridViewMaster)._SetAutoLayout();
                }
                else
                {
                    //((PLGridView)that.gridViewDetail)._SetUserLayout(null);
                    //((PLGridView)that.gridViewMaster)._SetUserLayout(null);
                }
            }
            that.WindowState = FormWindowState.Maximized;
            HelpGrid.MoveGroupTextPanelToTitle(that.gridViewMaster);

            if (saveQuery != null)
            {
                if (dataSetID != null)
                {
                    HelpControl.addSaveQueryDialog(that, that.barManager1, that.popupMenuFilter, that.gridControlMaster,
                        dataSetID, saveQuery, that.HookAfterExecAdvQuery);
                }
                else
                {
                    HelpControl.addSaveQueryDialog(that, that.barManager1, that.popupMenuFilter, that.gridControlMaster,
                        that.gridViewMaster._GetPLGUI(), saveQuery, that.HookAfterExecAdvQuery);
                }
            }

            that.barCheckItemFilter.Caption = "Hiện điều &kiện tìm kiếm";
            if (that._UsingExportToFileItem == true)
                that.barSubItemExport = PhieuQuanLyUtil.XuatRaFile(that.barManager1, that.gridViewMaster);
            else that.barSubItemExport = new DevExpress.XtraBars.BarSubItem();
        }

        protected void PhieuQL_Load(object sender, EventArgs e)
        {
            try
            {
                this._setBusinessMenu();
                if (that.IsContextMenu) this._setContextMenuOnGrid();
                this.PLCustomView();
                PLDaChonMasterRecord(false);
                that.PLLoadFilterPart();
                //((PLGridView)that.gridViewMaster)._PLAutoWidth = that._PLAutoWidth;
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
                if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
            }
        }
        /// <summary>Sự kiên chọn IN
        /// </summary>
        private void barButtonItemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                long[] ids = PhieuQuanLyUtil.GetSelectedID(that.gridViewMaster, that.IDField);
                if (ids != null) that.InAction(ids).execDirectlyPrint();
            }
            catch (Exception ex){
                PLException.AddException(ex);
            }
        }
        /// <summary>Sự kiện xem trước khi in
        /// </summary>
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                long[] ids = PhieuQuanLyUtil.GetSelectedID(that.gridViewMaster, that.IDField);
                if (ids != null) that.InAction(ids).execPreviewWith();
            }
            catch(Exception ex)
            {
                PLException.AddException(ex);
                PLMessageBox.ShowErrorMessage("File RPT không tồn tại. Vui lòng kiểm tra lại hoặc liên hệ PROTOCOL.");
            }
        }
        /// <summary>Sự kiện chọn Không Duyệt.
        /// </summary>
        private void barButtonItemNoCommit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (that.gridViewMaster.SelectedRowsCount < 1)
            {
                HelpMsgBox.ShowNotificationMessage(that._msgChuaChonDuLieu);
                return;
            }
            foreach (int i in that.gridViewMaster.GetSelectedRows())
            {
                long id = HelpNumber.ParseInt64(that.gridViewMaster.GetRowCellValue(i, that.IDField));
                string title = "";
                if (that.DisplayField != "") title = "" + that.gridViewMaster.GetRowCellValue(i, that.DisplayField);
                if (PLMessageBox.ShowConfirmMessage(string.Format(that._msgConfirmBeforeKhongDuyet, title)) == DialogResult.Yes)
                {
                    try
                    {
                        if (((IDuyetSupport)that).KhongDuyetAction(id, FrameworkParams.currentUser.employee_id, DABase.getDatabase().GetSystemCurrentDateTime()))
                        {
                            that.gridViewMaster.SetRowCellValue(i, "DUYET", "3");
                            gridViewMaster_FocusedRowChanged(null, null);
                        }
                        else
                            HelpMsgBox.ShowNotificationMessage(String.Format(that._msgNotifyKhongDuyetFail, title));
                            
                    }
                    catch (Exception ex)
                    {
                        PLException.AddException(ex);
                    }
                }
            }

        }
        /// <summary>Sự kiện chọn "Duyêt".
        /// </summary>
        private void barButtonItemCommit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (that.gridViewMaster.SelectedRowsCount < 1)
            {
                HelpMsgBox.ShowNotificationMessage(that._msgChuaChonDuLieu);
                return;
            }

            foreach (int i in that.gridViewMaster.GetSelectedRows())
            {
                long id = HelpNumber.ParseInt64(that.gridViewMaster.GetRowCellValue(i, that.IDField));
                string title = "";
                if (that.DisplayField != "") title = "" + that.gridViewMaster.GetRowCellValue(i, that.DisplayField);
                if (PLMessageBox.ShowConfirmMessage(string.Format(that._msgConfirmBeforeDuyet, title)) == DialogResult.Yes)
                {
                    try
                    {
                        if (((IDuyetSupport)that).DuyetAction(id, FrameworkParams.currentUser.employee_id, DABase.getDatabase().GetSystemCurrentDateTime()))
                        {
                            that.gridViewMaster.SetRowCellValue(i, "DUYET", "2");
                            gridViewMaster_FocusedRowChanged(null, null);
                        }
                        else
                        {
                            HelpMsgBox.ShowNotificationMessage(String.Format(that._msgNotifyDuyetFail, title));
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        PLException.AddException(ex);
                    }
                }
            }
        }
        /// <summary>Sự kiện khi double click vào mẫu tin trong phần Master
        /// </summary>
        private void gridViewMaster_DoubleClick(object sender, EventArgs e)
        {
            if (that.gridViewMaster.SelectedRowsCount > 0 &&
               that.gridViewMaster.FocusedRowHandle > -1)
            {
                try
                {
                    long id = HelpNumber.ParseInt64(that.gridViewMaster.GetFocusedRowCellValue(that.IDField));
                    that.ShowViewForm(id);
                }
                catch (Exception ex)
                {
                    PLException.AddException(ex);
                }
            }
        }
        /// <summary>Sự kiện khi chọn Tìm kiếm
        /// </summary>
        public void barButtonItemView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PLRefresh();
            find = true;
        }
        /// <summary>Sự kiện khi chọn "Sửa"
        /// </summary>
        private void barButtonItemUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (that.gridViewMaster.SelectedRowsCount > 0 &&
                    that.gridViewMaster.FocusedRowHandle > -1)
                {
                    long id = HelpNumber.ParseInt64(that.gridViewMaster.GetFocusedRowCellValue(that.IDField));
                    that.ShowUpdateForm(id);
                    if (!this.UpdateRow())
                    {
                        if (that.filter != null && !that.filter.isEmpty() && find)
                            PLRefresh();
                    }
                }
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
        }
        /// <summary>Sự kiện khi chọn nút "Xem"
        /// </summary>
        private void barButtonItemXem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (that.gridViewMaster.GetDataRow(that.gridViewMaster.FocusedRowHandle) != null)
                {
                    that.ShowViewForm(HelpNumber.ParseInt64(that.gridViewMaster.GetDataRow(that.gridViewMaster.FocusedRowHandle)[that.IDField]));
                }
                else
                {
                    HelpMsgBox.ShowNotificationMessage(that._msgChuaChonDuLieu);
                }
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
        }
        /// <summary>Sự kiện khi chọn "Thêm".
        /// </summary>
        protected void barButtonItemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                long[] ids = that.ShowAddForm();
                if (ids != null)
                {
                    string query = that.UpdateRow();
                    if (query != null && query != "")
                    {
                        DataTable table = ((DataTable)(that.gridControlMaster.DataSource)).DataSet.Tables[0];
                        for (int i = 0; i < ids.Length; i++)
                        {
                            QueryBuilder filter = new QueryBuilder(query);
                            filter.addID(that.IDField, ids[i]);
                            DataSet ds = DABase.getDatabase().LoadReadOnlyDataSet(filter);
                            if (ds.Tables[0].Rows.Count == 1)
                            {
                                DataRow newRow = table.NewRow();
                                newRow.ItemArray = ds.Tables[0].Rows[0].ItemArray;
                                table.Rows.Add(newRow);
                            }
                            else
                            {
                            }
                        }
                    }
                }
                else
                {
                    //PLRefresh();
                }
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
        }
        /// <summary>Sự kiện khi chọn "Xóa".
        /// </summary>
        private void barButtonItemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string title = "";
            long id = -1;
            int j = 0;

            if (that.gridViewMaster.SelectedRowsCount > 0)
            {
                int[] DeleteIds = that.gridViewMaster.GetSelectedRows();
                if (DeleteIds == null) return;

                DataRow[] rows = new DataRow[DeleteIds.Length];
                foreach (int i in DeleteIds)
                {
                    if (i < 0) continue;
                    rows[j++] = that.gridViewMaster.GetDataRow(i);
                }

                foreach (DataRow row in rows)
                {
                    if (row == null) continue;
                    id = HelpNumber.ParseInt64(row[that.IDField]);
                    title = row[that.DisplayField].ToString();

                    if (PLMessageBox.ShowConfirmMessage(string.Format(that._msgConfirmBeforeDelete, title)) == DialogResult.Yes)
                    {
                        if (this.DuyetSupport)
                        {
                            if (row["DUYET"].ToString() == "2")
                            {
                                HelpMsgBox.ShowNotificationMessage(String.Format(that._msgNotifyDeleteFail, title));                                
                                continue;
                            }
                        }
                        try
                        {
                            if (!that.XoaAction(id))
                            {
                                HelpMsgBox.ShowNotificationMessage(String.Format(that._msgNotifyDeleteFail, title));                                
                                //HelpMsgBox.ShowNotificationMessage("Thực hiện không thành công vì " + title + " còn đang sử dụng.");
                            }
                            else
                            {
                                ((DataTable)(that.gridControlMaster.DataSource)).DataSet.Tables[0].Rows.Remove(row);
                            }
                        }
                        catch { }
                    }
                }
                try { that.gridViewMaster.SelectRow(that.gridViewMaster.FocusedRowHandle); }
                catch { }
                gridViewMaster_FocusedRowChanged(null, null);
            }
            else
                HelpMsgBox.ShowNotificationMessage(that._msgChuaChonDuLieu);
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
            catch (Exception ex)
            {
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
                that.barButtonItemUpdate.Enabled = true;
                that.barButtonItemDelete.Enabled = true;
                that.barButtonItemPrint.Enabled = true;
                that.barButtonItemXem.Enabled = true;
                _setEnableMenu(true, SplitPanelVisibility.Both);

                UpdateDuyetState();
            }
            else
            {
                that.barButtonItemUpdate.Enabled = false;
                that.barButtonItemDelete.Enabled = false;
                that.barButtonItemPrint.Enabled = false;
                that.barButtonItemXem.Enabled = false;

                that.barButtonItemCommit.Enabled = false;
                that.barButtonItemNoCommit.Enabled = false;

                _setEnableMenu(false, SplitPanelVisibility.Panel1);
            }
        }
        /// <summary>
        /// Sự kiện khi split container thay đổi kích thước
        /// </summary>
        private void splitContainerControl1_Resize(object sender, EventArgs e)
        {
            that.splitContainerControl1.SplitterPosition = 3 * that.splitContainerControl1.Height / 4;
        }
        /// <summary> Sự kiên ẩn hiện filter.
        /// </summary>
        private void barCheckItemFilter_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (that.barCheckItemFilter.Checked)
            {
                that.popupControlContainerFilter.Show();

            }
            else
            {
                that.popupControlContainerFilter.Hide();
            }
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
                if (that.filter != null){
                    if(that.filter.isEmpty())
                        ((DataTable)that.gridControlMaster.DataSource).Clear();
                    else
                        PLLoadMasterPart(that.gridControlMaster);                    
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
                gridViewMaster_FocusedRowChanged(null, null);
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
        }

        private void UpdateDuyetState()
        {
            PhieuQuanLyUtil.UpdateDuyetState(
                this.DuyetSupport,
                that.AllowUpdatePhieuDuyet,
                that.gridViewMaster,
                that.barButtonItemCommit,
                that.barButtonItemNoCommit,
                that.barButtonItemDelete,
                that.barButtonItemUpdate
            );
            #region Hàm cũ - Xóa
            //if (this.DuyetSupport)
            //{
            //    DataRow row = that.gridViewMaster.GetDataRow(that.gridViewMaster.FocusedRowHandle);
            //    if (row["DUYET"] == null) row["DUYET"] = "1";
            //    if (row["DUYET"].ToString() == "1")
            //    {
            //        that.barButtonItemCommit.Enabled = true;
            //        that.barButtonItemNoCommit.Enabled = true;
            //        that.barButtonItemDelete.Enabled = true;
            //        that.barButtonItemUpdate.Enabled = true;
            //    }
            //    else if (row["DUYET"].ToString() == "3")
            //    {
            //        that.barButtonItemCommit.Enabled = true;
            //        that.barButtonItemNoCommit.Enabled = false;
            //        that.barButtonItemDelete.Enabled = true;
            //        that.barButtonItemUpdate.Enabled = true;
            //    }
            //    else if (row["DUYET"].ToString() == "2")
            //    {
            //        if (that.AllowUpdatePhieuDuyet)
            //        {
            //            that.barButtonItemCommit.Enabled = false;
            //            that.barButtonItemNoCommit.Enabled = true;
            //        }
            //        else
            //        {
            //            that.barButtonItemCommit.Enabled = false;
            //            that.barButtonItemNoCommit.Enabled = false;
            //        }
            //        that.barButtonItemUpdate.Enabled = false;
            //        that.barButtonItemDelete.Enabled = false;
            //    }
            //}
            #endregion
        }

        /// <summary>Cập nhật hiển thị menu và phần detail
        /// </summary>
        public void _setEnableMenu(bool _status, SplitPanelVisibility _type)
        {
            PhieuQuanLyUtil.ShowMenu(
                that.barSubItem1, _status,
                that.gridControlMaster, that.splitContainerControl1, _type);

            #region Hàm cũ - Xóa
            //that.barSubItem1.Enabled = _status;//ẩn đi menu trên menu chính            
            //if (that.gridControlMaster.ContextMenuStrip != null)
            //{
            //    foreach (ToolStripItem var in that.gridControlMaster.ContextMenuStrip.Items)//ẩn đi menu trên grid           
            //    {
            //        var.Enabled = _status;
            //    }
            //}
            //that.splitContainerControl1.PanelVisibility = _type;
            #endregion
        }

        /// <summary>Lấy những dòng đã chọn trên GridView
        /// </summary>
        public List<DataRow> _getSelectedDatarowsOnGrid(DevExpress.XtraGrid.Views.Grid.PLGridView gridView)
        {
            return PhieuQuanLyUtil.GetSelectedDataRow(gridView);
            #region Hàm cũ - Xóa
            //List<DataRow> rowList = new List<DataRow>();
            //foreach (int index in gridView.GetSelectedRows())
            //{
            //    DataRow temp = gridView.GetDataRow(index);
            //    rowList.Add(temp);
            //}
            //return rowList;
            #endregion
        }

        /// <summary>Gắn menu vào lưới 
        /// </summary>
        private void _setContextMenuOnGrid()
        {
            PhieuQuanLyUtil.SetContextMenuOnGrid(
                that.gridControlMaster,
                that.GetMenuAppendGridList(),
                that.GetBusinessMenuList(),
                that.IncludeNghiepVu);
            #region Hàm cũ - Xóa
            //try
            //{
            //    _MenuItem BothMenu = null;
            //    _MenuItem AppendMenu = that.GetMenuAppendGridList();
            //    if (that.IncludeNghiepVu)
            //    {
            //        _MenuItem NghiepVuMenu = that.GetBusinessMenuList();
            //        if (NghiepVuMenu == null && AppendMenu != null) BothMenu = AppendMenu;
            //        else if (NghiepVuMenu != null && AppendMenu == null) BothMenu = NghiepVuMenu;
            //        else if (NghiepVuMenu == null && AppendMenu == null) BothMenu = null;
            //        else
            //        {
            //            //Merger 2 thang : Chua xay dung
            //        }
            //    }
            //    else
            //    {
            //        BothMenu = AppendMenu;
            //    }

            //    if (BothMenu != null) XtraGridSupportExt.AddMenuToGridView(that.gridControlMaster, BothMenu.FieldName, BothMenu.CaptionNames, BothMenu.ImageNames, BothMenu.Funcs, BothMenu.Permissions);
            //}
            //catch (Exception ex)
            //{
            //    PLException.AddException(ex);
            //}
            #endregion
        }

        #region Xử lý menu nghiệp vụ
        /// <summary>Xây dựng menu nghiệp vụ
        /// </summary>
        /// <param name="captions">Mảng (Array) tên các chọn lựa</param>
        /// <param name="formFullNames">
        ///     Mảng (Array) tên kiểu đầy đủ của form, bao gồm cả namespace
        ///     Form này phải có hàm dựng truyền vào một mảng object.
        /// </param>
        /// <param name="images">Mảng (Array) tên hình gắn vào các chọn lựa</param>
        private void createBusinessMenu(string[] captions, string fieldName, string[] ImageNames, DelegationLib.CallFunction_MulIn_NoOut[] delegates, PermissionItem[] pers)
        {
            PhieuQuanLyUtil.CreateBusinessMenu(that.gridViewMaster, that.barSubItem1, fieldName,
                captions, ImageNames, delegates, pers);
            #region Hàm cũ - Xóa
            //if (captions == null)
            //{
            //    //that.barSubItem1.Enabled = false;
            //    that.barSubItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //    return;
            //}

            //int index = 0;
            //foreach (string s in captions)
            //{
            //    //Start Check Permission
            //    if (pers != null)
            //    {
            //        if (pers[index] != null)
            //        {
            //            if (ApplyPermissionAction.checkPermission(pers[index])==null ||
            //                ApplyPermissionAction.checkPermission(pers[index])==false)
            //            {
            //                index++;
            //                continue;
            //            }
            //        }
            //    }
            //    //End Check Permission

            //    DevExpress.XtraBars.BarButtonItem temp = new DevExpress.XtraBars.BarButtonItem();
            //    temp.Caption = captions[index];
            //    temp.Name = index.ToString();
            //    if (!ImageNames[index].Equals(""))
            //    {
            //        temp.Glyph = ResourceMan.getImage16(ImageNames[index]);
            //    }
            //    temp.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            //    temp.ItemClick += delegate(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
            //    {
            //        if (that.gridViewMaster.SelectedRowsCount < 1)
            //        {
            //            HelpMsgBox.ShowNotificationMessage(that._msgChuaChonDuLieu);
            //            return;
            //        }
            //        //Lấy danh sách các giá trị (row[fieldName]) đang chọn 
            //        List<object> objs = new List<object>();
            //        foreach (int i in that.gridViewMaster.GetSelectedRows())
            //        {
            //            DataRow row = that.gridViewMaster.GetDataRow(i);
            //            objs.Add(row[fieldName]);
            //        }

            //        //Chọn xử lý tương ứng với chọn lựa
            //        delegates[HelpNumber.ParseInt32(e.Item.Name)](objs);
            //    };

            //    that.barSubItem1.ItemLinks.Add(temp);
            //    index++;
            //}
            ////Không có chọn lựa ẩn luôn.
            //if (that.barSubItem1.ItemLinks == null || that.barSubItem1.ItemLinks.Count == 0)
            //    that.barSubItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            #endregion
        }
        /// <summary>Gắn menu nghiệp vụ  vào menu chính
        /// </summary>
        private void _setBusinessMenu()
        {
            try
            {
                _MenuItem tempMenu = that.GetBusinessMenuList();
                if (tempMenu != null)
                {
                    createBusinessMenu(tempMenu.CaptionNames, tempMenu.FieldName,
                        tempMenu.ImageNames, tempMenu.Funcs, tempMenu.Permissions);
                }
                else
                {
                    that.barSubItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
        }
        #endregion
        #endregion

        #region Phần phiếu quản lý ( 1 - 1)

        /// <summary>Sự kiện khi thay đổi focus trên mẫu tin phần gridViewMaster
        /// PHUOCNC : Sao nó gọi 2 lần khi thực hiện tìm kiếm tìm cách cho nó gọi một lần.
        /// </summary>
        public void gridViewMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (that.gridViewMaster.SelectedRowsCount > 0 &&
                that.gridViewMaster.FocusedRowHandle > -1)
            {
                PLDaChonMasterRecord(true);
                DataRow row = that.gridViewMaster.GetDataRow(that.gridViewMaster.FocusedRowHandle);
                that.gridControlDetail.DataSource = that.PLLoadDataDetailPart(HelpNumber.ParseInt64(row[that.IDField].ToString()));
                that.HookFocusRow();
            }
            else
            {
                PLDaChonMasterRecord(false);
                that.gridControlDetail.DataSource = null;
            }
        }
        
        public bool UpdateRow()
        {
            string query = that.UpdateRow();
            try
            {
                if (query != null && query != "" && that.gridViewMaster.FocusedRowHandle >= 0)
                {
                    DataRow row = that.gridViewMaster.GetDataRow(that.gridViewMaster.FocusedRowHandle);
                    long id = HelpNumber.ParseInt64(row[that.IDField].ToString());
                    QueryBuilder filter = new QueryBuilder(query);
                    filter.addID(that.IDField, id);
                    DataSet ds = DABase.getDatabase().LoadReadOnlyDataSet(filter);
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        row.ItemArray = ds.Tables[0].Rows[0].ItemArray;
                        PLDaChonMasterRecord(true);
                        that.gridControlDetail.DataSource = that.PLLoadDataDetailPart(HelpNumber.ParseInt64(row[that.IDField].ToString()));
                        that.HookFocusRow();
                    }
                    else
                    {
                        that.gridViewMaster.DeleteRow(that.gridViewMaster.FocusedRowHandle);
                    }
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                PLMessageBoxDev.ShowMessage("Câu truy vấn này bị sai " + query);
                return false;
            }
        }
        /// <summary> Tùy biến xử lý grid sao khi bị wrapper
        /// Do bị FW wrapper làm form của mình có thể không đạt được giao diện
        /// mình mong đợi. Do đó để thay đổi sau wrapper có thể đặt xử lý
        /// trong hàm này.
        /// </summary>
        protected void PLCustomView()
        {
            that.barCheckItemFilter.Checked = true;
            that.popupControlContainerFilter.Show();

            that.gridViewMaster.OptionsSelection.EnableAppearanceFocusedCell = false;
            that.gridViewMaster.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            that.gridViewMaster.OptionsView.ShowFooter = false;
            that.gridViewMaster.OptionsBehavior.Editable = false;
            that.gridViewMaster.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            that.gridViewMaster.OptionsCustomization.AllowGroup = true;
            HelpGrid.ShowNumOfRecord(that.gridControlMaster);

            that.gridViewDetail.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            that.gridViewDetail.OptionsBehavior.Editable = false;
            that.gridViewDetail.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }
        #endregion
    }
}
