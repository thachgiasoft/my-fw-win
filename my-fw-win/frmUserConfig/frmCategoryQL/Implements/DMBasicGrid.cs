using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using ProtocolVN.Framework.Core;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Win;
using ProtocolVN.DanhMuc;
namespace ProtocolVN.Framework.Win
{
    public partial class DMBasicGrid : XtraUserControl, IPluginCategory
    {
        private string TableName;
        private DataTable DataSource;

        private string IDField;
        private string[] NameFields;
        private string[] Subjects;        
        private InitGridColumns InitGridCol;
        private GetRule Rule;
        //private frmCategory owner;
        private IFormCategory owner;
        private DMGrid ownerNew;
        private DelegationLib.DefinePermission permission;
        public delegate GridColumn[] InitGridColumns(GridControl gridControl, GridView gridView);   //Định nghĩa các column
        public delegate FieldNameCheck[] GetRule(object param); //Định nghĩa các luật kiểm tra

        private PLDelegation.ProcessDataRow UpdateFunc;
        private PLDelegation.ProcessDataRow DeleteFunc;
        private PLDelegation.ProcessDataRow InsertFunc;

        public bool IsAllField = false;
        public bool _SupportDoubleClick = true;//Có hỗ trợ DoubleClick là Update không        
        public bool SupportDoubleClick {
            get{ return _SupportDoubleClick; }
            set{ _SupportDoubleClick = value; }
        }
        public List<long> NotDeleteIDs = new List<long>();//Những ID không được xóa

        public GridView Grid
        {
            get
            {
                return this.gridView;
            }
        }

        #region Danh sách sự kiện
        public delegate void BeforeSave(DMBasicGrid sender);
        public event BeforeSave _BeforeSaveEvent;

        public delegate void AfterSaveSucc(DMBasicGrid sender);
        public event AfterSaveSucc _AfterSaveSuccEvent;

        public delegate void AfterSaveFail(DMBasicGrid sender);
        public event AfterSaveFail _AfterSaveFailEvent;

        public delegate void BeforeDel(DMBasicGrid sender);
        public event BeforeDel _BeforeDelEvent;

        public delegate void AfterDelSucc(DMBasicGrid sender);
        public event AfterDelSucc _AfterDelSuccEvent;

        public delegate void AfterDelFail(DMBasicGrid sender);
        public event AfterDelFail _AfterDelFailEvent;
        #endregion

        #region Dùng như control chọn lựa
        public DMBasicGrid()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Unique:         N Field
        /// Phân quyền:     Có (Chưa xây dựng)
        /// Không sử dụng trực tiếp trong dự án
        /// </summary>
        public void _init(string TableName, string IDField, string[] NameFields, string[] Subjects,
            InitGridColumns InitGridCol, GetRule Rule,
            DelegationLib.DefinePermission permission)
        {
            this.TableName = TableName;
            this.DataSource = null;
            this.InsertFunc = null;
            this.DeleteFunc = null;
            this.UpdateFunc = null;

            this.IDField = IDField;
            this.NameFields = NameFields;
            this.Subjects = Subjects;
            this.InitGridCol = InitGridCol;
            this.Rule = Rule;
            InitGrid();
            InitDataGrid();
            InitEventGrid();
            this.permission = permission;
            this.gridView.DoubleClick += new EventHandler(gridView_DoubleClick);
        }

        public void _init(DataTable DataSource, string IDField, string[] NameFields, string[] Subjects,
            InitGridColumns InitGridCol, GetRule Rule,
            DelegationLib.DefinePermission permission,
            PLDelegation.ProcessDataRow InsertFunc, PLDelegation.ProcessDataRow DeleteFunc, PLDelegation.ProcessDataRow UpdateFunc)
        {
            this.TableName = null;            
            this.DataSource = DataSource;
            this.InsertFunc = InsertFunc;
            this.DeleteFunc = DeleteFunc;
            this.UpdateFunc = UpdateFunc;

            this.IDField = IDField;
            this.NameFields = NameFields;
            this.Subjects = Subjects;
            this.InitGridCol = InitGridCol;
            this.Rule = Rule;
            InitGrid();
            InitDataGrid();
            InitEventGrid();
            this.permission = permission;
            this.gridView.DoubleClick += new EventHandler(gridView_DoubleClick);
        }
        #endregion

        #region Dùng như control input trong form Category - ItemCategory
        /// <summary>Unique: N Field, Phân quyền: Có (Chưa xây dựng)
        /// </summary>
        public DMBasicGrid(DataTable DataSource, string IDField, string[] NameFields, string[] Subjects,
            InitGridColumns InitGridCol, GetRule Rule, 
            DelegationLib.DefinePermission permission,
            PLDelegation.ProcessDataRow InsertFunc, 
            PLDelegation.ProcessDataRow DeleteFunc, 
            PLDelegation.ProcessDataRow UpdateFunc)
        {
            InitializeComponent();
            _init(DataSource, IDField, NameFields, Subjects, InitGridCol, Rule, permission,
                 InsertFunc,  DeleteFunc,  UpdateFunc);
        }

        /// <summary>Unique: N Field, Phân quyền: Có (Chưa xây dựng)
        /// </summary>
        public DMBasicGrid(string TableName, string IDField, string[] NameFields, string[] Subjects, 
            InitGridColumns InitGridCol, GetRule Rule, DelegationLib.DefinePermission permission)
        {
            InitializeComponent();
            _init(TableName, IDField, NameFields, Subjects, InitGridCol, Rule, permission);
        }

        /// <summary>Unique: N Field, Phân quyền: Không
        /// </summary>
        public DMBasicGrid(string TableName, string IDField, string[] NameFields, string[] Subjects,
            InitGridColumns InitGridCol, GetRule Rule)
            : this(TableName, IDField, NameFields, Subjects, InitGridCol, Rule, null) 
        { 
        }
        /// <summary>Unique: 1 Field, Phân quyền: Không
        /// </summary>
        public DMBasicGrid(string TableName, string IDField, string NameField, string Subject, 
            InitGridColumns InitGridCol, GetRule Rule)
            : this(TableName, IDField, new string[] { NameField }, new string[] { Subject }, InitGridCol, Rule, null) 
        { 
        }
        #endregion

        #region Suy biến về DMBasic ( ID, NAME, VISIBLE )
        private static void DefaultRow(DataRow dr)
        {
            dr["VISIBLE_BIT"] = "Y";
        }

        public static GridColumn[] CreateDM_BASIC_V(GridControl gridControl, GridView gridView)
        {
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "ID", -1, -1), "ID");
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Tên", 0, 250), "NAME");
            GridColumn cotHienThi = XtraGridSupportExt.CreateGridColumn(gridView, GlobalConst.VISIBLE_TITLE, 100, 100);
            HelpGridColumn.CotPLHienThi(cotHienThi, "VISIBLE_BIT");
            ((PLGridView)gridView).DefaultNewRow = DefaultRow;
            ((PLGridView)gridView)._SetDesignLayout();
            return null;
        }
        public static GridColumn[] CreateDM_BASIC_No_V(GridControl gridControl, GridView gridView)
        {
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "ID", -1, -1), "ID");
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Tên", 0, 250), "NAME");
            GridColumn cotHienThi = XtraGridSupportExt.CreateGridColumn(gridView, GlobalConst.VISIBLE_TITLE, 100, 100);
            HelpGridColumn.CotPLHienThi( cotHienThi, "VISIBLE_BIT");
            ((PLGridView)gridView).DefaultNewRow = DefaultRow;
            ((PLGridView)gridView)._SetDesignLayout();
            cotHienThi.Visible = false;
            cotHienThi.OptionsColumn.ShowInCustomizationForm = false;
          
            return null;
        }
        public static FieldNameCheck[] GetRuleDM_BASIC(object param)
        {
            return new FieldNameCheck[] { 
                        new FieldNameCheck("NAME",
                            new CheckType[]{ CheckType.RequireMaxLength },
                            "Tên", 
                            new object[]{ 200 })
            };
        }
        
        public DMBasicGrid(string TableName, DelegationLib.DefinePermission permission, bool IsVisible)
        {
            InitializeComponent();
            if (IsVisible)
            {
                _init(TableName, "ID", new string[]{ "NAME" }, new string[] { "Tên" }
                    , CreateDM_BASIC_V, GetRuleDM_BASIC, null);
            }
            else
            {
                _init(TableName, "ID", new string[] { "NAME" }, new string[] { "Tên" }
                    , CreateDM_BASIC_No_V, GetRuleDM_BASIC, null);
            }
        }
        public DMBasicGrid(string TableName, bool IsVisible) : this(TableName, null, IsVisible) { }
        public DMBasicGrid(string TableName) : this(TableName, null, false) { }
        #endregion

        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            if (SupportDoubleClick) EditAction(null);
        }

        private void InitGrid()
        {
            if(InitGridCol!=null)
                InitGridCol(this.gridControl, this.gridView);
            ShowGrid(true);            
        }

        public void InitDataGrid()
        {
            DataTable tb = GetDataSource();
            if (tb == null) PLMessageBoxDev.ShowMessage("Thiếu bảng " + TableName);
            this.gridControl.DataSource = tb;
            ((PLGridView)this.Grid).BestFitColumns();            
        }

        public DataTable GetDataSource()
        {
            if (TableName != null)
            {
                //DataSet ds = DABase.getDatabase().LoadTable(TableName);
                DataSet ds = DABase.getDatabase().LoadDataSet(HelpSQL.SelectAll(TableName, NameFields), TableName);
                return HelpDataSetExt.GetTable0(ds);
            }
            else if (this.DataSource != null)
            {
                try {
                    return (DataTable)this.DataSource;
                }
                catch { }
            }
            return null;
        }

        private void InitEventGrid()
        {
            this.gridView.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gridView_ValidateRow);
            this.gridView.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView_RowUpdated);
            this.gridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridView_FocusedRowChanged);
            //this.gridView.InvalidRowException -= new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(XtraGridSupport.AllowValidateGrid_Event);
            GridValidation.NotAllowValidateGrid(this.gridView);
            this.gridView.InvalidRowException += delegate(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
            {
                if (e.ExceptionMode == DevExpress.XtraEditors.Controls.ExceptionMode.DisplayError)
                {
                    if (HelpMsgBox.ShowConfirmMessage("Thông tin bạn vào không hợp lệ. Bạn có muốn vào lại thông tin ?", this) == DialogResult.Yes)
                    {
                        e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
                        ignoreFocusRow = true;
                    }
                    else//Có thể ko dùng
                    {
                        e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.Ignore;
                        ((DataRowView)e.Row).Row.ClearErrors();
                        ShowGrid(true);
                    }
                }
                else if (e.ExceptionMode == DevExpress.XtraEditors.Controls.ExceptionMode.Ignore)
                {
                    ShowGrid(true);
                }
                else
                {
                    e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.Ignore;
                }
            };
        }
        
        private void gridView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                if (e.Row == null) { return; }
                DataRow row = ((DataRowView)e.Row).Row;
                row.ClearErrors();
                if (Rule != null)
                {
                    HelpInputData.TrimAllData(row);

                    if (!GUIValidation.ValidateRow(this.gridView, row, Rule(null)))
                    {
                        e.Valid = false;
                        return;
                    };
                }
                if (IsAllField == false)
                {
                    if (!GUIValidation.CheckDuplicateField(gridView,
                        ((DataView)gridView.DataSource).DataViewManager.DataSet, e, this.NameFields, this.Subjects))
                    {
                        e.Valid = false;
                        return;
                    }
                }
                else
                {
                    if (!GridValidation.CheckDuplicateAllField(gridView,
                        ((DataView)gridView.DataSource).DataViewManager.DataSet, e, this.NameFields, this.Subjects))
                    {
                        e.Valid = false;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
        }

        private void gridView_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            if (e.Row != null)
            {
                DataRow row = ((DataRowView)e.Row).Row;
                if (!this.saveOrUpdate(row))
                {
                    HelpMsgBox.ShowNotificationMessage("Lưu không thành công");
                    HelpGrid.ClearAllError(this.gridView);
                    return;
                }
            }
        }

        //PHUOCNC: Refactor lại 
        private bool saveOrUpdate(DataRow row)
        {
            if (_BeforeSaveEvent != null) _BeforeSaveEvent(this);
            try
            {
                #region PHUOCNT TODO Đoạn code này dự định bỏ vì kiểm lần này là 2 cho chắc.
                HelpInputData.TrimAllData(row);
                //Gần như chắc chắn thành công.
                if (Rule != null)
                {
                    if (!GUIValidation.ValidateRow(this.gridView, row, Rule(null)))
                    {
                        return false;
                    };
                }
                #endregion 

                if (row[IDField] == null || row[IDField].ToString().Equals(""))
                {
                    if (this.TableName != null)//Dùng Table
                    {
                        if (HelpDanhMucDB.InsertDMNCot(this.TableName, row) == false)
                        {
                            ((DataView)gridView.DataSource).DataViewManager.DataSet.Tables[0].Rows.Remove(row);
                            if (_AfterSaveFailEvent != null) _AfterSaveFailEvent(this);
                            return false;
                        }
                        else
                        {
                            if (_AfterSaveSuccEvent != null) _AfterSaveSuccEvent(this);                            
                            return true;
                        }
                    }
                    else if (this.DataSource != null)//Dùng DataSource
                    {
                        if (InsertFunc != null)
                        {
                            if (InsertFunc(row) == false)
                            {
                                ((DataView)gridView.DataSource).Table.Rows.Remove(row);
                                if (_AfterSaveFailEvent != null) _AfterSaveFailEvent(this);
                                return false;
                            }
                            else
                            {
                                try{
                                    if (DataSource.Rows.IndexOf(row) == -1)
                                        DataSource.ImportRow(row);
                                }
                                catch { }
                                if (_AfterSaveSuccEvent != null) _AfterSaveSuccEvent(this);
                            }
                            return true;
                        }
                        else
                        {
                            if (HelpDanhMucDB.InsertDMNCot(this.DataSource.TableName, row) == false)
                            {
                                ((DataView)gridView.DataSource).Table.Rows.Remove(row);
                                if (_AfterSaveFailEvent != null) _AfterSaveFailEvent(this);
                                return false;
                            }
                            else
                            {
                                try{
                                    if (DataSource.Rows.IndexOf(row) == -1)
                                        DataSource.ImportRow(row);
                                }
                                catch { }
                                if (_AfterSaveSuccEvent != null) _AfterSaveSuccEvent(this);
                            }
                            return true;
                        }
                    }
                    else //KHÔNG Xảy ra
                        return false;
                }
                else
                {
                    if (this.TableName != null)//Dùng Table
                    {
                        if (HelpDanhMucDB.UpdateDMNCot(this.TableName, row) == false)
                        {
                            if (_AfterSaveFailEvent != null) _AfterSaveFailEvent(this);
                            return false;
                        }
                        else
                        {
                            if (_AfterSaveSuccEvent != null) _AfterSaveSuccEvent(this);
                            return true;
                        }
                    }
                    else if (this.DataSource != null)//Dùng DataSource
                    {
                        bool flag ;
                        if (UpdateFunc != null)
                        {
                            flag = UpdateFunc(row);
                        }
                        else
                        {
                            flag = HelpDanhMucDB.UpdateDMNCot(this.DataSource.TableName, row);
                        }
                        if (flag == true){
                            if (_AfterSaveSuccEvent != null) _AfterSaveSuccEvent(this);
                        }
                        else{
                            if (_AfterSaveFailEvent != null) _AfterSaveFailEvent(this);
                        }
                        return flag;
                    }
                    else // KHÔNG Xảy ra
                        return false;
                }
            }
            catch (Exception ex) { PLException.AddException(ex); }
            return false;
        }

        private void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (ignoreFocusRow == true)
            {
                ignoreFocusRow = false;
                return;
            }
            if (this.gridView.Editable == true && isEdit == true)
            {
                ShowGrid(true);
            }
        }

        private void ShowGrid(bool ReadOnly)
        {
            isEdit = !ReadOnly;
            if (ReadOnly)
            {
                this.gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
                this.gridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
                this.gridView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
                this.gridView.OptionsBehavior.Editable = false;
                if (owner != null)
                {
                    owner.GetRemoveBtn().Enabled = true;
                    owner.GetAddBtn().Enabled = true;
                    owner.GetEditBtn().Enabled = true;
                    owner.GetSaveBtn().Enabled = false;
                    owner.GetNoSaveBtn().Enabled = false;
                }
                else if (ownerNew != null)
                {
                    ownerNew.btnDel.Enabled = true;
                    ownerNew.btnAdd.Enabled = true;
                    ownerNew.btnUpdate.Enabled = true;
                    ownerNew.btnSave.Enabled = false;
                    ownerNew.btnNoSave.Enabled = false;
                }
            }
            else
            {
                this.gridView.OptionsSelection.EnableAppearanceFocusedCell = true;
                this.gridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
                this.gridView.OptionsBehavior.Editable = true;
                if (owner != null)
                {
                    owner.GetRemoveBtn().Enabled = false;
                    owner.GetAddBtn().Enabled = false;
                    owner.GetEditBtn().Enabled = false;
                    owner.GetSaveBtn().Enabled = true;
                    owner.GetNoSaveBtn().Enabled = true;
                }
                else if (ownerNew != null)
                {
                    ownerNew.btnDel.Enabled = false;
                    ownerNew.btnAdd.Enabled = false;
                    ownerNew.btnUpdate.Enabled = false;
                    ownerNew.btnSave.Enabled = true;
                    ownerNew.btnNoSave.Enabled = true;
                }
            }
        }

        #region ICategory Members

        public void SetOwner(IFormCategory owner)
        {
            this.owner = owner;
        }

        public void SetDMGridOwner(DMGrid ownerNew)
        {
            this.ownerNew = ownerNew;
        }
        public void UpdateGUI()
        {
            return;
        }

        public GridView GetGridView()
        {
            return this.gridView;
        }
        #endregion        

        #region IDanhMuc Members
        public object AddAction(object param)
        {
            if (this.gridView.Editable == false) isEdit = false;
            if (isEdit == false)
            {
                ShowGrid(false);
                this.ignoreFocusRow = true;
                this.gridView.AddNewRow();
                this.gridView.FocusedColumn = this.gridView.VisibleColumns[0];
                this.gridView.ShowEditor();
            }

            return "NOOP";
        }
        private bool isEdit = false;
        private bool ignoreFocusRow = false;
        public object EditAction(object param)
        {
            if (gridView.FocusedRowHandle == GridControl.AutoFilterRowHandle ||
                gridView.FocusedRowHandle <= -1)
            {
                HelpMsgBox.ShowNotificationMessage("Bạn chưa chọn dữ liệu. Vui lòng chọn dữ liệu bạn muốn cập nhật!");
                return "NOOP";
            }

            if (this.gridView.GetDataRow(gridView.FocusedRowHandle) != null)
            {
                ShowGrid(false);
                //this.gridView.FocusedColumn = this.gridView.VisibleColumns[0];
                this.gridView.ShowEditor();
            }
            return "NOOP";
        }
        public object DeleteAction(object param)
        {
            if (gridView.FocusedRowHandle == GridControl.AutoFilterRowHandle ||
                gridView.FocusedRowHandle <= -1)
            {
                HelpMsgBox.ShowNotificationMessage("Bạn chưa chọn dữ liệu. Vui lòng chọn dữ liệu bạn muốn xóa!");
                return "NOOP";
            }

            try
            {
                if (_BeforeDelEvent != null) _BeforeDelEvent(this);

                DataRow row = this.gridView.GetDataRow(gridView.FocusedRowHandle);
                long id = HelpNumber.ParseInt64(row[IDField]);
                if (NotDeleteIDs != null && NotDeleteIDs.Contains(id))
                {
                    HelpMsgBox.ShowNotificationMessage("Dữ liệu này không được xóa. Xin vui lòng chọn dữ liệu khác.");
                    return "NOOP";
                }

                if (FWMsgBox.showConfirmRemoveCatelogy() == DialogResult.Yes)
                {
                    if (this.TableName != null)//Table
                    {
                        if (HelpDanhMucDB.DeleteDM(this.TableName, id) == false)
                        {
                            FWMsgBox.showDeleteUsing();
                            HelpGrid.ClearAllError(this.gridView);
                            if (_AfterDelFailEvent != null) _AfterDelFailEvent(this);
                            return "NOOP";
                        }
                        else
                        {
                            if (_AfterDelSuccEvent != null) _AfterDelSuccEvent(this);
                        }
                    }
                    else if (this.DataSource != null)//DataSource
                    {
                        if (DeleteFunc != null)
                        {
                            if (DeleteFunc(row) == false)
                            {
                                FWMsgBox.showDeleteUsing();
                                HelpGrid.ClearAllError(this.gridView);
                                if (_AfterDelFailEvent != null) _AfterDelFailEvent(this);
                                return "NOOP";
                            }
                            else
                            {
                                if (_AfterDelSuccEvent != null) _AfterDelSuccEvent(this);
                            }
                        }
                        else
                        {
                            if (HelpDanhMucDB.DeleteDM(this.DataSource.TableName, id) == false)
                            {
                                FWMsgBox.showDeleteUsing();
                                HelpGrid.ClearAllError(this.gridView);

                                if (_AfterDelFailEvent != null) _AfterDelFailEvent(this);
                                return "NOOP";
                            }
                            else
                            {
                                if (_AfterDelSuccEvent != null) _AfterDelSuccEvent(this);
                            }
                        }
                    }
                    row.Delete();
                }
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
            return "NOOP";
        }
        public object PrintAction(object param)
        {
            this.gridControl.ShowPrintPreview();
            return "NOOP";
        }
        public object SaveAction(object param)
        {
            int handle = this.gridView.FocusedRowHandle;
            try
            {
                    this.gridView.FocusedRowHandle -= 1;
            }
            catch { }
            finally
            {
                if (handle >= 0)
                {
                    this.gridView.FocusedRowHandle = handle;
                    this.gridView.SelectRow(handle);
                }
                else
                {
                    this.gridView.MoveLastVisible();
                }
            }
            return null;
        }
        public object NoSaveAction(object param)
        {
            try
            {
                ((DataRowView)this.gridView.GetRow(gridView.FocusedRowHandle)).Row.ClearErrors();
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
            this.gridView.CancelUpdateCurrentRow();
            this.ShowGrid(true);

            return null;
        }
        public object AddChildAction(object param)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IPermisionable Members

        public List<Control> GetPermisionableControls()
        {
            return null;
        }

        public List<object> GetObjectItems()
        {
            if (permission == null) return null;
            if (this.owner != null)
                return permission(this.owner);
            else if (this.ownerNew != null)
                return permission(this.ownerNew);
            else
                throw new Exception();
        }

        #endregion

        #region ILangable Members

        public List<Control> GetLangableControls()
        {
            return null;
        }

        #endregion

        #region IFormatable Members

        public List<Control> GetFormatControls()
        {
            return null;
        }

        #endregion

        
    }
}
