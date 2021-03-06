using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using ProtocolVN.Framework.Core;
using ProtocolVN.Framework.Win;
using System.IO;
using System.Text;
using DevExpress.Utils;
using DevExpress.Utils.Controls;
using DevExpress.XtraEditors;
using DevExpress.Utils.Menu;
using DevExpress.XtraPrinting;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.Utils.Serializing;
namespace DevExpress.XtraGrid.Views.Grid
{
    /*
     * - _PLAutoWidth = null -> DevBestFit
     * - _PLAutoWidth = false -> User Layout
     *      Khong ton tai file 
     *          ColumnAutoWidth = false 
     *              Khong BestFit
     *          ColumnAutoWidth = true
     *              PLBestFit
     *      Ton Tai File
     *          Nap File
     * - _PLAutoWidth = true
     *      PLBestFit
     */
    sealed public class PLGridView : GridView
    {
        public bool _PLNo = true;
        public int _PLSTTWidth = 40;
        public string _fullQueryData = null;
        public TextExportMode _TextExportMode = TextExportMode.Text;
        private bool? _PLAutoWidth = false;
        private string _LayoutName = null;
        private string _LayoutNameDefault = null;
        private object _PrintElement;
        private object _ExportElement;
        public bool _UseExprortHearder = true;
        private String GetLayoutXMLPath()
        {
            return this._LayoutName;
        }

     
        #region Xử lý Lưu Layout
        public static String GetPLGridViewName(GridView gridView)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(FrameworkParams.currentUser.username);
            if (gridView.GridControl != null)
            {
                builder.Append(gridView.GridControl.FindForm() == null ? "" : gridView.GridControl.FindForm().Name);
                builder.Append(gridView.GridControl.Name);
                builder.Append(gridView.Name);
                builder.Append(gridView.GridControl.Parent == null ? "" : gridView.GridControl.Parent.Name);
                try
                {
                    builder.Append(gridView.GridControl.DataSource == null ? "" : ((DataTable)gridView.GridControl.DataSource).TableName);
                }
                catch { }
            }
            else PLMessageBoxDev.ShowMessage("GenName.NewName bằng rỗng. Báo ngay PHUOCNT");
            return builder.ToString();
        }

        public void _SetUserLayout()
        {
            _SetUserLayout(null);
        }

        public string _GetPLGUI()
        {
            return PLGridView.GetPLGridViewName(this);
        }

        public void _SetUserLayout(string prefixName)
        {
            this._PLAutoWidth = false;
            String temp = GetPLGridViewName(this);
            if (prefixName == null)
            {
                StringBuilder builder = new StringBuilder();
                if(this._LayoutName == null)
                    builder.Append(temp);
                builder.Append(".xml");

                this._LayoutName = FrameworkParams.LAYOUT_FOLDER + @"\" + builder.ToString();
                this._LayoutNameDefault = FrameworkParams.CONF_FOLDER + @"\layout\" + builder.ToString();
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(temp);
                builder.Append("_" + prefixName);
                builder.Append(this.GridControl == null ? "" : this.GridControl.Name);                
                builder.Append(".xml");

                this._LayoutName = FrameworkParams.LAYOUT_FOLDER + @"\" + builder.ToString();
                this._LayoutNameDefault = FrameworkParams.CONF_FOLDER + @"\layout\" + builder.ToString();
            }

            try
            {
                if (File.Exists(this._LayoutName)){
                    this.RestoreLayoutFromXml(this._LayoutName);
                }
                else if(File.Exists(this._LayoutNameDefault)){
                    this.RestoreLayoutFromXml(this._LayoutNameDefault);
                }
                else
                {
                    #region Cách 2
                    if (this.GridControl != null)
                        this.GridControl.Resize += new EventHandler(GridControl_Resize);
                    #endregion

                    if(this.OptionsView.ColumnAutoWidth == true)
                        this.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                if (this.OptionsView.ColumnAutoWidth == true)
                    this.BestFitColumns();
                PLException.AddException(ex);
            }
        }
       
        #endregion

        public void _SetPermissionElement(object print, object export)
        {
            this._PrintElement = print;
            this._ExportElement = export;
        }

        public void _RefreshLayout()
        {
            this.RestoreLayoutFromXml(this._LayoutName);
        }

        public void _SetAutoLayout()
        {
            this._PLAutoWidth = true;
        }

        public void _SetDesignLayout()
        {
            this._PLAutoWidth = null;
        }

        public PLGridView(): base()
        {
            WinLaw.checkLaw(this);
            this.ToPLGrid();
            this.CotSoTT();
            this.DataSourceChanged +=new EventHandler(gridview_DataSourceChanged);
            this.KeyUp += new KeyEventHandler(PLGridView_KeyUp);
          
        }

       


        void PLGridView_KeyUp(object sender, KeyEventArgs e)
        {                    
            //ALT-?
            if (ShortcutKey.K_ALT_QUESTION(e)){
                if (this.OptionsView.ShowAutoFilterRow == true)
                {
                    try
                    {
                        HelpGrid.setFocusFilterRow(this);
                        e.Handled = true;
                    }
                    catch { }
                }
            }
            //ALT-Z
            else if (ShortcutKey.K_ALT_Z(e))
            {
                if (this.Editable == true)
                {
                    try
                    {
                        this.FocusedRowHandle = -(Int32.MaxValue);//-2147483647
                        this.Focus();
                        this.FocusedColumn = this.VisibleColumns[0];
                        this.ShowEditor();
                    }
                    catch { }
                }                
            }
        }

        protected override void OnGridControlChanged(GridControl prevControl)
        {
            base.OnGridControlChanged(prevControl);
            
            try
            {
                if (_LayoutName == null)
                {
                    this.GridControl.Load += new EventHandler(GridControl_Load);
                }

                if (FrameworkParams.IsTrial)
                {
                    //if (this.GridControl != null && this.GridControl.BackgroundImage == null)
                    //    HelpGrid.SetWaterMark(this, FWImageDic.LOGO_IMAGE48, (float)0.5);
                    if (this.GridControl != null)
                        HelpGrid.SetWaterMark(this, FWImageDic.LOGO_IMAGE48, (float)0.5);
                }
            }
            catch { }
        }

        //PHUOCNT NC Trial Đặt option cho Layout
        void GridControl_Load(object sender, EventArgs e)
        {
            WinLaw.checkLaw(this);

            if (_LayoutName == null) _SetUserLayout();
            this.GridControl.Load -= new EventHandler(GridControl_Load);
        }

        #region Cách 2
        void GridControl_Resize(object sender, EventArgs e)
        {
            if (this.GridControl.Visible == true)
            {
                this.BestFitColumns();
                this.GridControl.Resize -= new EventHandler(GridControl_Resize);
            }
        }
        #endregion

        public override void BestFitColumns()
        {
            if (_PLAutoWidth == null)
            {
                base.BestFitColumns();
                return;
            }

            try
            {
                if (this.IsVisible == false)
                {
                    base.BestFitColumns();
                    return;
                }

                #region Cách 1
                //int totalWidth = 0;
                //OptionsView.ColumnAutoWidth = true;
                //base.BestFitColumns();
                //for (int i = 0; i < VisibleColumns.Count; i++)
                //{
                //    totalWidth += VisibleColumns[i].VisibleWidth;
                //}

                //int totalWidthBest = 0;
                //OptionsView.ColumnAutoWidth = false;
                //base.BestFitColumns();
                //for (int i = 0; i < VisibleColumns.Count; i++)
                //{
                //    totalWidthBest += VisibleColumns[i].VisibleWidth;
                //}

                //if (totalWidth < totalWidthBest)
                //{
                //    //OptionsView.ColumnAutoWidth = false;
                //    //base.BestFitColumns();
                //}
                //else
                //{
                //    OptionsView.ColumnAutoWidth = true;
                //    base.BestFitColumns();
                //}
                #endregion

                #region Cách 2
                //Chiều rộng các cột được tính tự động trong phạm vi của lưới ko tính thanh trượt                
                int totalWidth = 0;
                int totalWidthBest = 0;
                OptionsView.ColumnAutoWidth = true;
                base.BestFitColumns();

                for (int i = 0; i < VisibleColumns.Count; i++)
                {
                    totalWidth += VisibleColumns[i].VisibleWidth;
                    totalWidthBest += VisibleColumns[i].GetBestWidth();
                }

                if (totalWidth < totalWidthBest)//Xuất thanh truot
                {
                    OptionsView.ColumnAutoWidth = false;
                    base.BestFitColumns();
                }
                else//Không xuất thanh trượt
                {
                    //OptionsView.ColumnAutoWidth = true;
                    //base.BestFitColumns();
                }                
                #endregion
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
        }

        private void ToPLGrid()
        {
            this.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            this.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            
            this.OptionsNavigation.AutoFocusNewRow = true;

            this.OptionsLayout.Columns.AddNewColumns = false;
            this.OptionsNavigation.EnterMoveNextColumn = true;
            this.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.OptionsView.ColumnAutoWidth = true;
            this.OptionsView.EnableAppearanceEvenRow = true;
            this.OptionsView.EnableAppearanceOddRow = true;
            this.OptionsView.ShowGroupedColumns = true;

            this.ShowGridMenu += new DevExpress.XtraGrid.Views.Grid.GridMenuEventHandler(gv_ShowGridMenu);
            this.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(gb_DragObjectDrop);

            this.OptionsPrint.UsePrintStyles = true;
            
            GridValidation.AllowValidateGrid(this);
        }

        private void gv_ShowGridMenu(object sender, DevExpress.XtraGrid.Views.Grid.GridMenuEventArgs e)
        {            
            if (e.Menu is DevExpress.XtraGrid.Menu.GridViewColumnMenu)
            {
                InsertMenu((DevExpress.XtraGrid.Menu.GridViewColumnMenu)e.Menu);
            }
        }      
                
        #region Xử lý thêm cho phần menu click phải mặc định

        private void InsertMenu(DevExpress.XtraGrid.Menu.GridViewColumnMenu Menu)
        {
            // Insert Menu   
            DevExpress.XtraGrid.Menu.GridViewColumnMenu menu = (DevExpress.XtraGrid.Menu.GridViewColumnMenu)Menu;
            if (menu.Column == null) return;
            
            #region 1. Auto filter
            //Hiện Auto Filter
            if (this.OptionsView.ShowAutoFilterRow == false)
            {
                DevExpress.Utils.Menu.DXMenuItem itemAutoFilter;
                itemAutoFilter = new DevExpress.Utils.Menu.DXMenuItem("Hiện Auto Filter");
                itemAutoFilter.BeginGroup = true;
                itemAutoFilter.Click += new EventHandler(itemAutoFilter_Click);
                Menu.Items.Add(itemAutoFilter);
            }
            else
            {
                //Ẩn Auto Filter
                DevExpress.Utils.Menu.DXMenuItem itemHideAutoFilter;
                itemHideAutoFilter = new DevExpress.Utils.Menu.DXMenuItem("Ẩn Auto Filter");
                itemHideAutoFilter.BeginGroup = true;
                itemHideAutoFilter.Click += new EventHandler(itemHideAutoFilter_Click);
                Menu.Items.Add(itemHideAutoFilter);
            }
            //Cách filter trong AutoFilter
            DevExpress.Utils.Menu.DXSubMenuItem filterType = new DevExpress.Utils.Menu.DXSubMenuItem("Auto Filter theo dạng");
            Menu.Items.Add(filterType);

            DevExpress.Utils.Menu.DXMenuCheckItem filterEqualType = new DevExpress.Utils.Menu.DXMenuCheckItem("Lọc bằng (=)");
            filterEqualType.Tag = menu.Column.AbsoluteIndex;
            filterEqualType.Click += new EventHandler(filterEqualType_Click);
            filterType.Items.Add(filterEqualType);
            filterEqualType.Checked = (this.Columns[menu.Column.AbsoluteIndex].OptionsFilter.AutoFilterCondition == AutoFilterCondition.Equals);

            DevExpress.Utils.Menu.DXMenuCheckItem filterLikeType = new DevExpress.Utils.Menu.DXMenuCheckItem("Lọc giống (Like)");
            filterLikeType.Tag = menu.Column.AbsoluteIndex;
            filterLikeType.Click += new EventHandler(filterLikeType_Click);
            filterType.Items.Add(filterLikeType);
            filterLikeType.Checked = (this.Columns[menu.Column.AbsoluteIndex].OptionsFilter.AutoFilterCondition == AutoFilterCondition.Like);

            DevExpress.Utils.Menu.DXMenuCheckItem filterCotainType = new DevExpress.Utils.Menu.DXMenuCheckItem("Lọc chứa (Contains)");
            filterCotainType.Tag = menu.Column.AbsoluteIndex;
            filterCotainType.Click += new EventHandler(filterCotainType_Click);
            filterType.Items.Add(filterCotainType);
            filterCotainType.Checked = (this.Columns[menu.Column.AbsoluteIndex].OptionsFilter.AutoFilterCondition == AutoFilterCondition.Contains);
            #endregion

            #region 2. Lọc nâng cao
            if (this._fullQueryData != null)
            {
                DevExpress.Utils.Menu.DXMenuItem itemSaveFilter;
                itemSaveFilter = new DevExpress.Utils.Menu.DXMenuItem("Lọc nâng cao");
                itemSaveFilter.BeginGroup = true;
                itemSaveFilter.Click += new EventHandler(itemSaveFilter_Click);
                Menu.Items.Add(itemSaveFilter);
            }
            #endregion

            #region 3. Tính toán trong nhóm
            DevExpress.Utils.Menu.DXMenuItem itemDisplayFooter;
            itemDisplayFooter = new DevExpress.Utils.Menu.DXMenuItem("Hiện thanh tính toán");
            itemDisplayFooter.BeginGroup = true;
            if (this.OptionsView.ShowFooter == true)
                itemDisplayFooter.Caption = "Ẩn thanh tính toán";
            itemDisplayFooter.Click += new EventHandler(itemDisplayFooter_Click);
            Menu.Items.Add(itemDisplayFooter);

            if (this.GroupedColumns.Count > 0)
            {
                DevExpress.Utils.Menu.DXSubMenuItem calcTotalColumn = new DevExpress.Utils.Menu.DXSubMenuItem("Tính toán nhóm");
                Menu.Items.Add(calcTotalColumn);

                DevExpress.Utils.Menu.DXMenuItem countCalc;
                countCalc = new DevExpress.Utils.Menu.DXMenuItem("Tính số dòng");
                countCalc.Tag = menu.Column.AbsoluteIndex;
                countCalc.Click += new EventHandler(countCalc_Click);
                calcTotalColumn.Items.Add(countCalc);

                DevExpress.Utils.Menu.DXMenuItem sumCalc;
                sumCalc = new DevExpress.Utils.Menu.DXMenuItem("Tính tổng cộng");
                sumCalc.Tag = menu.Column.AbsoluteIndex;
                sumCalc.Click += new EventHandler(sumCalc_Click);
                calcTotalColumn.Items.Add(sumCalc);

                DevExpress.Utils.Menu.DXMenuItem averageCalc;
                averageCalc = new DevExpress.Utils.Menu.DXMenuItem("Tính trung bình");
                averageCalc.Tag = menu.Column.AbsoluteIndex;
                averageCalc.Click += new EventHandler(averageCalc_Click);
                calcTotalColumn.Items.Add(averageCalc);

                DevExpress.Utils.Menu.DXMenuItem maxCalc;
                maxCalc = new DevExpress.Utils.Menu.DXMenuItem("Tính cực đại");
                maxCalc.Tag = menu.Column.AbsoluteIndex;
                maxCalc.Click += new EventHandler(maxCalc_Click);
                calcTotalColumn.Items.Add(maxCalc);

                DevExpress.Utils.Menu.DXMenuItem minCalc;
                minCalc = new DevExpress.Utils.Menu.DXMenuItem("Tính cực tiểu");
                minCalc.Tag = menu.Column.AbsoluteIndex;
                minCalc.Click += new EventHandler(minCalc_Click);
                calcTotalColumn.Items.Add(minCalc);
            }
            #endregion
            
            #region 4. Canh lề
            // Menu Display Data    
            DevExpress.Utils.Menu.DXSubMenuItem itemDisplayData = new DevExpress.Utils.Menu.DXSubMenuItem("Canh lề");
            itemDisplayData.BeginGroup = true;
            Menu.Items.Add(itemDisplayData);
            
            DevExpress.Utils.Menu.DXMenuCheckItem itemLeft = new DevExpress.Utils.Menu.DXMenuCheckItem("Canh trái");
            itemLeft.Tag = menu.Column.AbsoluteIndex;
            itemLeft.Click += new EventHandler(itemLeft_Click);
            itemDisplayData.Items.Add(itemLeft);
            itemLeft.Checked = (this.Columns[menu.Column.AbsoluteIndex].AppearanceCell.TextOptions.HAlignment == DevExpress.Utils.HorzAlignment.Near);


            DevExpress.Utils.Menu.DXMenuCheckItem itemRight = new DevExpress.Utils.Menu.DXMenuCheckItem("Canh phải");
            itemRight.Tag = menu.Column.AbsoluteIndex;
            itemRight.Click += new EventHandler(itemRight_Click);
            itemDisplayData.Items.Add(itemRight);
            itemRight.Checked = (this.Columns[menu.Column.AbsoluteIndex].AppearanceCell.TextOptions.HAlignment == DevExpress.Utils.HorzAlignment.Far);

            DevExpress.Utils.Menu.DXMenuCheckItem itemCenter = new DevExpress.Utils.Menu.DXMenuCheckItem("Canh giữa");
            itemCenter.Tag = menu.Column.AbsoluteIndex;
            itemCenter.Click += new EventHandler(itemCenter_Click);
            itemDisplayData.Items.Add(itemCenter);                                    
            itemCenter.Checked = (this.Columns[menu.Column.AbsoluteIndex].AppearanceCell.TextOptions.HAlignment == DevExpress.Utils.HorzAlignment.Center);

            #endregion

            #region 5. Cố định cột

            var visbleColumns = this.VisibleColumns.Cast<GridColumn>();
            DevExpress.Utils.Menu.DXSubMenuItem fixColumn = new DevExpress.Utils.Menu.DXSubMenuItem("Cố định cột");
            fixColumn.BeginGroup = true;
            Menu.Items.Add(fixColumn);

            DevExpress.Utils.Menu.DXMenuItem fixLeftColumnAll;
            fixLeftColumnAll = new DevExpress.Utils.Menu.DXMenuItem("Về bên trái danh sách");
            fixLeftColumnAll.Tag = menu.Column.AbsoluteIndex;
            fixLeftColumnAll.Click += new EventHandler(fixLeftColumnAll_Click);
            fixColumn.Items.Add(fixLeftColumnAll);

            DevExpress.Utils.Menu.DXMenuItem fixRightColumnAll;
            fixRightColumnAll = new DevExpress.Utils.Menu.DXMenuItem("Về bên phải danh sách");
            fixRightColumnAll.Tag = menu.Column.AbsoluteIndex;
            fixRightColumnAll.Click += new EventHandler(fixRightColumnAll_Click);
            fixColumn.Items.Add(fixRightColumnAll);

            


            if (visbleColumns.Any(c => c.Fixed != FixedStyle.None))
            {
                DevExpress.Utils.Menu.DXMenuItem subNoFixColumn = new DevExpress.Utils.Menu.DXMenuItem("Bỏ cố định cột");
                subNoFixColumn.Tag = menu.Column.AbsoluteIndex;
                subNoFixColumn.Click += new EventHandler(noFixColumnAll_Click);
                Menu.Items.Add(subNoFixColumn);
            }

            #endregion

            #region 5.1 Thêm cột tính toán
            DevExpress.Utils.Menu.DXMenuItem calcColumn;
            calcColumn = new DevExpress.Utils.Menu.DXMenuItem("Thêm cột tính toán");
            calcColumn.Tag = menu.Column.AbsoluteIndex;
            calcColumn.BeginGroup = true;
            calcColumn.Click += delegate(object sender, EventArgs e)
            {
                GridColumn column = HelpGridColumn.ThemCot(this, "Cột mới 1", this.Columns.Count + 1, 100);
                column.Visible = false;
                column.OptionsColumn.AllowEdit = false;
                column.OptionsColumn.ReadOnly = true;
                column.ShowUnboundExpressionMenu = true;

                //GridColumn column = getCotTinhToan();
                
                this.ShowUnboundExpressionEditor(column);
                if (column.UnboundExpression != String.Empty){
                    InputBoxResult input = HelpMsgBox._showMsgInput("Vui lòng nhập vào chuỗi theo định dạng <Tên cột mới>;<Kiểu>. Với <Kiểu>:Văn bản, Số, Ngày, Logic\r\nVí dụ: Muốn tạo cột tên là Cột mới và kiểu là Văn bản thì nhập vào 'Cột mới;Văn bản'.");
                    if (input.ReturnCode == DialogResult.OK)
                    {
                        string caption = input.Text.Trim();
                        if (caption != String.Empty)
                        {
                            String[] captions = caption.Split(';');
                            captions[0] = captions[0].Trim();
                            column.FieldName = captions[0];
                            column.Caption = captions[0];
                            column.Visible = true;
                            column.Tag = "YES";                                                        

                            if(captions.Length == 2)
                                captions[1] = captions[1].Trim();                            
                            if(captions[1] == "Văn bản")
                                column.UnboundType = DevExpress.Data.UnboundColumnType.String;
                            else if (captions[1] == "Văn bản")
                                column.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
                            else if (captions[1] == "Ngày")
                                column.UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
                            else if (captions[1] == "Logic")
                                column.UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
                            else
                                column.UnboundType = DevExpress.Data.UnboundColumnType.String;
                            return;
                        }
                        else
                        {
                            HelpMsgBox.ShowNotificationMessage("Tên cột mới không rỗng");
                        }
                    }
                }
                column.Tag = "NO";
                column.Visible = false;
                this.Columns.Remove(column);
            };
            Menu.Items.Add(calcColumn);

            if (Menu.Column.ShowUnboundExpressionMenu)
            {
                DXSubMenuItem itemXoa = new DXSubMenuItem("Bỏ cột tính toán");
                Menu.Items.Add(itemXoa);
                itemXoa.Click += delegate(object senderXoa, EventArgs eXoa)
                {
                    Menu.Column.Tag = "NO";
                    Menu.Column.Caption = "";
                    Menu.Column.Visible = false;

                    this.Columns.Remove(Menu.Column);
                };
            }

            #endregion

            #region 6. Export dữ liệu

            bool isExport = true;

            if (_ExportElement != null)
            {
                if (_ExportElement is DevExpress.XtraBars.BarItem)
                {
                    if (((DevExpress.XtraBars.BarItem)_ExportElement).Visibility == DevExpress.XtraBars.BarItemVisibility.Never)
                        isExport = false;
                }
                else if (_ExportElement is SimpleButton)
                {
                    if (((SimpleButton)_ExportElement).Visible == false)
                        isExport = false;
                }
                else if (_ExportElement is ToolStripDropDownButton)
                {
                    if (((ToolStripDropDownButton)_ExportElement).Visible == false)
                        isExport = false;
                }
            }
            if (isExport)
            {
                //SubMenu Export Data
                DevExpress.Utils.Menu.DXSubMenuItem itemExport = new DevExpress.Utils.Menu.DXSubMenuItem("Xuất ra file");
                itemExport.BeginGroup = true;
                Menu.Items.Add(itemExport);

                //Menu Export Excel
                DevExpress.Utils.Menu.DXMenuItem itemExportExcel = new DevExpress.Utils.Menu.DXMenuItem("Excel 97 - 2003");
                itemExport.Items.Add(itemExportExcel);
                itemExportExcel.Tag = "xls";
                itemExportExcel.Click += new EventHandler(itemExport_Click);

                DevExpress.Utils.Menu.DXMenuItem itemExportExcel2007 = new DevExpress.Utils.Menu.DXMenuItem("Excel 2007");
                itemExport.Items.Add(itemExportExcel2007);
                itemExportExcel2007.Tag = "xlsx";
                itemExportExcel2007.Click += new EventHandler(itemExport_Click);

                DevExpress.Utils.Menu.DXMenuItem itemPDF = new DevExpress.Utils.Menu.DXMenuItem("PDF");
                itemExport.Items.Add(itemPDF);
                itemPDF.Tag = "pdf";
                itemPDF.Click += new EventHandler(itemExport_Click);

                //Menu Export HTML
                DevExpress.Utils.Menu.DXMenuItem itemExportHTML = new DevExpress.Utils.Menu.DXMenuItem("HTML");
                itemExport.Items.Add(itemExportHTML);
                itemExportHTML.Tag = "html";
                itemExportHTML.Click += new EventHandler(itemExport_Click);

                //Menu Export Text
                DevExpress.Utils.Menu.DXMenuItem itemExportText = new DevExpress.Utils.Menu.DXMenuItem("RTF");
                itemExport.Items.Add(itemExportText);
                itemExportText.Tag = "rtf";
                itemExportText.Click += new EventHandler(itemExport_Click);
            }
            #endregion

            #region 6.1 Gom nhóm dữ liệu
            #region Thêm item GroupInterval
            //DXSubMenuItem itemDateInterval = new DXSubMenuItem("Thay đổi cách gom nhóm");
            //itemDateInterval.BeginGroup = true;
            //menu.Items.Add(itemDateInterval);
            //itemDateInterval.Click += delegate(object senderDateInterval, EventArgs eDateInterval)
            //{
            //    HelpProtocolForm.ShowDialog(FrameworkParams.MainForm, new frmGroupIntervalOption(this));
            //};
            //DevExpress.Utils.Menu.DXSubMenuItem itemGomNhom = new DevExpress.Utils.Menu.DXSubMenuItem("Gom nhóm cột");
            //itemGomNhom.BeginGroup = true;
            //Menu.Items.Add(itemGomNhom);
            GridColumn columnInterval = menu.Column;

            //Văn bản            
            //DXSubMenuItem itemMacDinhInterval = new DXSubMenuItem("Dạng mặc định");
            //itemMacDinhInterval.Tag = menu.Column.AbsoluteIndex + 1000;
            //itemMacDinhInterval.Click += new EventHandler(itemInterval_Click);
            //itemGomNhom.Items.Add(itemMacDinhInterval);
            
            if (columnInterval.ColumnType.FullName == "System.String")
            {
                DXSubMenuItem itemAZInterval = new DXSubMenuItem("Gom nhóm cột này theo dạng A-Z");
                itemAZInterval.BeginGroup = true;
                itemAZInterval.Tag = menu.Column.AbsoluteIndex + 2000;
                itemAZInterval.Click += new EventHandler(itemInterval_Click);
                Menu.Items.Add(itemAZInterval);
            }

            //else if (columnInterval.ColumnType.FullName == "System.DateTime")
            //{
            //    //Kiểu thời gian
            //    DXSubMenuItem itemNgayInterval = new DXSubMenuItem("Dạng ngày");
            //    itemNgayInterval.BeginGroup = true;
            //    itemNgayInterval.Tag = menu.Column.AbsoluteIndex + 3000;
            //    itemNgayInterval.Click += new EventHandler(itemInterval_Click);
            //    itemGomNhom.Items.Add(itemNgayInterval);

            //    DXSubMenuItem itemThangInterval = new DXSubMenuItem("Dạng tháng");
            //    itemThangInterval.Tag = menu.Column.AbsoluteIndex + 4000;
            //    itemThangInterval.Click += new EventHandler(itemInterval_Click);
            //    itemGomNhom.Items.Add(itemThangInterval);

            //    DXSubMenuItem itemNamInterval = new DXSubMenuItem("Dạng năm");
            //    itemNamInterval.Tag = menu.Column.AbsoluteIndex + 5000;
            //    itemNamInterval.Click += new EventHandler(itemInterval_Click);
            //    itemGomNhom.Items.Add(itemNamInterval);

            //    DXSubMenuItem itemVungInterval = new DXSubMenuItem("Dạng vùng");
            //    itemVungInterval.Tag = menu.Column.AbsoluteIndex + 6000;
            //    itemVungInterval.Click += new EventHandler(itemInterval_Click);
            //    itemGomNhom.Items.Add(itemVungInterval);
            //}
            ////Kiểu khác
            //DXSubMenuItem itemDisplayInterval = new DXSubMenuItem("Dạng hiển thị");
            //itemDisplayInterval.Tag = menu.Column.AbsoluteIndex + 7000;
            //itemDisplayInterval.BeginGroup = true;
            //itemDisplayInterval.Click += new EventHandler(itemInterval_Click);
            //itemGomNhom.Items.Add(itemDisplayInterval);

            //DXSubMenuItem itemValueInterval = new DXSubMenuItem("Dạng giá trị");
            //itemValueInterval.Tag = menu.Column.AbsoluteIndex + 8000;
            //itemValueInterval.Click += new EventHandler(itemInterval_Click);
            //itemGomNhom.Items.Add(itemValueInterval);

            #endregion
            #endregion

            #region 7. In dữ liệu
             bool isPrint = true;
            if (_PrintElement != null)
            {
                if (_PrintElement is DevExpress.XtraBars.BarItem)
                {
                    if (((DevExpress.XtraBars.BarItem)_PrintElement).Visibility == DevExpress.XtraBars.BarItemVisibility.Never)
                        isPrint = false;
                }
                else if (_PrintElement is SimpleButton)
                {
                    if (((SimpleButton)_PrintElement).Visible == false)
                        isPrint = false;
                }
                else if (_PrintElement is ToolStripDropDownButton)
                {
                    if (((ToolStripDropDownButton)_PrintElement).Visible == false)
                        isPrint = false;
                }
            }
            if (isPrint)
            {
                //Menu Print Data                    
                DevExpress.Utils.Menu.DXMenuItem itemPrintData = new DevExpress.Utils.Menu.DXMenuItem("Xem trước khi in");
                itemPrintData.BeginGroup = true;
                Menu.Items.Add(itemPrintData);
                itemPrintData.Click += new EventHandler(itemPrintData_Click);
            }
            #endregion

            #region 8. Hình dạng lưới
            if (_PLAutoWidth != null && _PLAutoWidth == false)
            {
                // Menu Display Data    
                DevExpress.Utils.Menu.DXSubMenuItem itemGridLayout = new DevExpress.Utils.Menu.DXSubMenuItem("Tùy biến khác");
                itemGridLayout.BeginGroup = true;
                Menu.Items.Add(itemGridLayout);

                //Lưu hình dạng lưới như mặc định
                if(FrameworkParams.currentUser.username=="admin"){
                    DevExpress.Utils.Menu.DXMenuItem itemSaveLayOutDefault;
                    itemSaveLayOutDefault = new DevExpress.Utils.Menu.DXMenuItem("Lưu hình dạng danh sách như mặc định");
                    itemSaveLayOutDefault.Click += new EventHandler(itemSaveLayOutDefault_Click);
                    //Menu.Items.Add(itemSaveLayOutDefault);
                    itemGridLayout.Items.Add(itemSaveLayOutDefault);
                }

                //Lưu hình dạng lưới
                DevExpress.Utils.Menu.DXMenuItem itemSaveLayOut;
                itemSaveLayOut = new DevExpress.Utils.Menu.DXMenuItem("Lưu hình dạng danh sách hiện tại");
                if (FrameworkParams.currentUser.username != "admin")
                    itemSaveLayOut.BeginGroup = false;
                else
                    itemSaveLayOut.BeginGroup = true;
                itemSaveLayOut.Click += new EventHandler(itemSaveLayOut_Click);
                //Menu.Items.Add(itemSaveLayOut);
                itemGridLayout.Items.Add(itemSaveLayOut);

                //Phục hồi lại hình dạng ban đầu
                DevExpress.Utils.Menu.DXMenuItem itemResetLayOut;
                itemResetLayOut = new DevExpress.Utils.Menu.DXMenuItem("Xóa hình dạng danh sách đã lưu");
                itemResetLayOut.Click += new EventHandler(itemResetLayOut_Click);
                //Menu.Items.Add(itemResetLayOut);
                itemGridLayout.Items.Add(itemResetLayOut);

                //Phục hồi lại hình dạng ban đầu
                DevExpress.Utils.Menu.DXMenuItem itemScrollLayOut;
                itemScrollLayOut = new DevExpress.Utils.Menu.DXMenuItem("Điều chỉnh cột thủ công");
                itemScrollLayOut.Click += new EventHandler(itemScrollLayOut_Click);
                itemScrollLayOut.BeginGroup = true;
                //Menu.Items.Add(itemScrollLayOut);
                itemGridLayout.Items.Add(itemScrollLayOut);

                DevExpress.Utils.Menu.DXMenuItem itemScrollLayOut1;
                itemScrollLayOut1 = new DevExpress.Utils.Menu.DXMenuItem("Điều chỉnh cột tự động");
                itemScrollLayOut1.Click += new EventHandler(itemScrollLayOut1_Click);
                //Menu.Items.Add(itemScrollLayOut1);
                itemGridLayout.Items.Add(itemScrollLayOut1);
            }
            #endregion

            #region 9. Hỗ trợ Debug
            if (FrameworkParams.isSupportDeveloper == true)
            {
                // Menu Display Data    
                DevExpress.Utils.Menu.DXSubMenuItem itemDebug = new DevExpress.Utils.Menu.DXSubMenuItem("Debug");
                itemDebug.BeginGroup = true;
                Menu.Items.Add(itemDebug);

                DevExpress.Utils.Menu.DXMenuItem itemDebug01 = new DevExpress.Utils.Menu.DXMenuItem("Xem toàn bộ dữ liệu");
                itemDebug01.Tag = menu.Column.AbsoluteIndex;
                itemDebug01.Click += new EventHandler(itemDebug01_Click);
                itemDebug.Items.Add(itemDebug01);

                DevExpress.Utils.Menu.DXMenuItem itemDebug02 = new DevExpress.Utils.Menu.DXMenuItem("Xem toàn bộ dữ liệu và cấu trúc danh sách");
                itemDebug02.Tag = menu.Column.AbsoluteIndex;
                itemDebug02.Click += new EventHandler(itemDebug02_Click);
                itemDebug.Items.Add(itemDebug02);

            }
            #endregion
        }

      
     

     

        void itemInterval_Click(object sender, EventArgs e)
        {
            DevExpress.Utils.Menu.DXMenuItem item = sender as DevExpress.Utils.Menu.DXMenuItem;
            int value = (int)item.Tag;
            this.BeginSort();
            GridColumn col = null;
            if (value >= 8000)
            {
                col = this.Columns[value - 8000];
                col.GroupInterval = ColumnGroupInterval.Value;
            }
            else if (value >= 7000)
            {
                col = this.Columns[value - 7000];
                col.GroupInterval = ColumnGroupInterval.DisplayText;
            }
            else if (value > 6000)
            {
                col = this.Columns[value - 6000];
                col.GroupInterval = ColumnGroupInterval.DateRange;
            }
            else if (value >= 5000)
            {
                col = this.Columns[value - 5000];
                col.GroupInterval = ColumnGroupInterval.DateYear;
            }
            else if (value > 4000)
            {
                col = this.Columns[value - 4000];
                col.GroupInterval = ColumnGroupInterval.DateMonth;
            }
            else if (value > 3000)
            {
                col = this.Columns[value - 3000];
                col.GroupInterval = ColumnGroupInterval.Date;
            }
            else if (value > 2000)
            {
                col = this.Columns[value - 2000];
                col.GroupInterval = ColumnGroupInterval.Alphabetical;

            }
            else if (value > 1000)
            {
                col = this.Columns[value - 1000];
                col.GroupInterval = ColumnGroupInterval.Default;
            }

            if (col != null)
            {
                col.GroupIndex = this.GroupCount + 1;
            }

            this.EndSort();
        }

        void itemSaveFilter_Click(object sender, EventArgs e)
        {
            try{
                XtraForm f = (XtraForm) this.GridControl.FindForm();
                if (f != null)
                {
                    FilterCase obj = new FilterCase(FrameworkParams.currentUser.id, this._GetPLGUI(), "Truy vấn mới", this._fullQueryData);
                    SaveQueryDialog q = new SaveQueryDialog(obj, this.GridControl);
                    q.Owner = f;
                    q.Show();
                }
            }catch {

            }
        }

        void filterCotainType_Click(object sender, EventArgs e)
        {
            //gridView.OptionsView.ShowAutoFilterRow = true;
            foreach (GridColumn col in this.Columns)
                col.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;
        }

        void filterLikeType_Click(object sender, EventArgs e)
        {
            //gridView.OptionsView.ShowAutoFilterRow = true;
            foreach (GridColumn col in this.Columns)
                col.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Like;
        }

        void filterEqualType_Click(object sender, EventArgs e)
        {
            //gridView.OptionsView.ShowAutoFilterRow = true;
            foreach (GridColumn col in this.Columns)
                col.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Equals;
        }

        void itemDebug01_Click(object sender, EventArgs e)
        {
            PLDebug.ShowDataExt(((DataView)this.DataSource).DataViewManager.DataSet, "Nội dung danh sách và tình trạng dòng");
        }

        void itemDebug02_Click(object sender, EventArgs e)
        {
            DataTableReader dr = ((DataView)this.DataSource).DataViewManager.DataSet.CreateDataReader();
            PLDebug.ShowStructure(dr);            
        }

        void minCalc_Click(object sender, EventArgs e)
        {
            DevExpress.Utils.Menu.DXMenuItem item = sender as DevExpress.Utils.Menu.DXMenuItem;
            HelpGrid.ShowGroupCalcInfo(this, this.Columns[(int)item.Tag], HelpGrid.CalculationType.MIN);
        }

        void maxCalc_Click(object sender, EventArgs e)
        {
            DevExpress.Utils.Menu.DXMenuItem item = sender as DevExpress.Utils.Menu.DXMenuItem;
            HelpGrid.ShowGroupCalcInfo(this, this.Columns[(int)item.Tag], HelpGrid.CalculationType.MAX);
        }

        void averageCalc_Click(object sender, EventArgs e)
        {
            DevExpress.Utils.Menu.DXMenuItem item = sender as DevExpress.Utils.Menu.DXMenuItem;
            HelpGrid.ShowGroupCalcInfo(this, this.Columns[(int)item.Tag], HelpGrid.CalculationType.AVERAGE);
        }

        void countCalc_Click(object sender, EventArgs e)
        {
            DevExpress.Utils.Menu.DXMenuItem item = sender as DevExpress.Utils.Menu.DXMenuItem;
            HelpGrid.ShowGroupCalcInfo(this, this.Columns[(int)item.Tag], HelpGrid.CalculationType.COUNT);
        }

        void sumCalc_Click(object sender, EventArgs e)
        {
            DevExpress.Utils.Menu.DXMenuItem item = sender as DevExpress.Utils.Menu.DXMenuItem;
            HelpGrid.ShowGroupCalcInfo(this, this.Columns[(int)item.Tag], HelpGrid.CalculationType.SUM);
        }

      

      
        void fixLeftColumnAll_Click(object sender, EventArgs e)
        {
            var cols = this.VisibleColumns.Cast<GridColumn>().Where(c => c.Fixed == FixedStyle.Left).ToList();
            cols.ForEach(c => c.Fixed = FixedStyle.None);
            DevExpress.Utils.Menu.DXMenuItem item = sender as DevExpress.Utils.Menu.DXMenuItem;
            var col = this.Columns[(int) item.Tag];
            for(int i=0;i<=col.VisibleIndex;i++)
            {
                VisibleColumns[i].Fixed = FixedStyle.Left;
            }
        }

     
      
        void fixRightColumnAll_Click(object sender, EventArgs e)
        {
            var cols = this.VisibleColumns.Cast<GridColumn>().Where(c => c.Fixed == FixedStyle.Right).ToList();
            cols.ForEach(c => c.Fixed = FixedStyle.None);
            DevExpress.Utils.Menu.DXMenuItem item = sender as DevExpress.Utils.Menu.DXMenuItem;
            var col = this.Columns[(int)item.Tag];
            for (int i =VisibleColumns.Count-1; i>=col.VisibleIndex; i--)
            {
                VisibleColumns[i].Fixed = FixedStyle.Right;
            }
        }

        void noFixColumnAll_Click(object sender, EventArgs e)
        {
            var cols = this.VisibleColumns.Cast<GridColumn>().Where(c => c.Fixed != FixedStyle.None).ToList();
            cols.ForEach(c => c.Fixed = FixedStyle.None);

        }


        private void itemPrintData_Click(object sender, EventArgs e)
        {            
            if (this.GridControl != null)
            {
                this.OptionsView.ShowViewCaption = false;

                if (FrameworkParams.headerLetter != null)
                {
                    PrintableComponentLink link = FrameworkParams.headerLetter.Draw(this.GridControl, this.ViewCaption,
                        "Ngày xuất báo cáo: " + DateTime.Today.ToString(FrameworkParams.option.dateFormat));
                    link.ShowPreviewDialog();
                }
                else
                {
                    this.GridControl.ShowPrintPreview();
                }
                
                this.OptionsView.ShowViewCaption = true;
            }
        }
        private void itemAllowFilter_Click(object sender, EventArgs e)
        {
            try{
                DevExpress.Utils.Menu.DXMenuCheckItem item = sender as DevExpress.Utils.Menu.DXMenuCheckItem;
                for (int i = 0; i < this.Columns.Count; i++)
                {
                   this.Columns[i].OptionsFilter.AllowFilter = item.Checked;
                }
            }
            catch (Exception ex) { PLException.AddException(ex); }
        }
        private void itemDisplayFooter_Click(object sender, EventArgs e)
        {
            //DevExpress.Utils.Menu.DXMenuCheckItem item = sender as DevExpress.Utils.Menu.DXMenuCheckItem;
            this.OptionsView.ShowFooter = !this.OptionsView.ShowFooter;
        }
        private void itemLeft_Click(object sender, EventArgs e)
        {
            DevExpress.Utils.Menu.DXMenuItem item = sender as DevExpress.Utils.Menu.DXMenuItem;
            this.Columns[(int)item.Tag].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
        }
        private void itemRight_Click(object sender, EventArgs e)
        {
            DevExpress.Utils.Menu.DXMenuItem item = sender as DevExpress.Utils.Menu.DXMenuItem;
            this.Columns[(int)item.Tag].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
        }
        private void itemCenter_Click(object sender, EventArgs e)
        {
            DevExpress.Utils.Menu.DXMenuItem item = sender as DevExpress.Utils.Menu.DXMenuItem;
            this.Columns[(int)item.Tag].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }
        private void itemExport_Click(object sender, EventArgs e)
        {
            DevExpress.Utils.Menu.DXMenuItem item = sender as DevExpress.Utils.Menu.DXMenuItem;
            HelpGrid.exportFile(this, item.Tag.ToString(), _UseExprortHearder);
        }
        #endregion

        #region Xử lý Default Row
        public delegate void DefaultValueNewRow(DataRow dr);
        public DefaultValueNewRow DefaultNewRow;
        protected override void RaiseInitNewRow(InitNewRowEventArgs e)
        {
            base.RaiseInitNewRow(e);
            if (this.DefaultNewRow != null)
            {
                DataRow dr = this.GetDataRow(e.RowHandle);
                DefaultNewRow(dr);
            }
        }
        #endregion

        #region Tạo cột số TT
        void CotSoTT()
        {
            if (_PLNo)
            {
                this.IndicatorWidth = _PLSTTWidth;
                this.Appearance.HeaderPanel.Options.UseTextOptions = true;
                this.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                this.CustomDrawRowIndicator += delegate(object sender, RowIndicatorCustomDrawEventArgs e)
                {
                    if (e.Info.IsRowIndicator && e.RowHandle > -1)
                        e.Info.DisplayText = (e.RowHandle + 1).ToString();
                };
            }
        }
        void CotTrongCuoiLuoi()
        {
            GridColumn cotTrong = new GridColumn();
            cotTrong.Caption = " ";
            cotTrong.Visible = true;
            cotTrong.VisibleIndex = 100;
            cotTrong.Width = 5;
            cotTrong.OptionsColumn.FixedWidth = true;
            cotTrong.OptionsColumn.AllowSize = false;
            cotTrong.OptionsColumn.AllowMove = false;
            cotTrong.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            cotTrong.OptionsFilter.AllowFilter = false;
            Columns.Add(cotTrong);
        }
        #endregion

        #region Xử lý khi Drag Cột ra ngoài nó kô 
                private void gb_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            try
            {
                GridColumn col = (GridColumn)e.DragObject;
                if (!col.Visible) {
                    if(PLMessageBox.ShowConfirmMessage("Bạn có muốn ẩn cột này ?")== DialogResult.No){
                        col.Visible = true;
                    }                    
                }                                
            }
            catch { }
        }
        #endregion

        #region Layout
        private void itemHideAutoFilter_Click(object sender, EventArgs e)
        {
            this.OptionsView.ShowAutoFilterRow = false;
            this.ActiveFilterString = null;
        }
        private void itemAutoFilter_Click(object sender, EventArgs e)
        {
            this.OptionsView.ShowAutoFilterRow = true;
        }

        private void itemScrollLayOut_Click(object sender, EventArgs e)
        {
            this.OptionsView.ColumnAutoWidth = false;
        }
        private void itemScrollLayOut1_Click(object sender, EventArgs e)
        {
            this.OptionsView.ColumnAutoWidth = true;
        }
        
        private void itemSaveLayOut_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveLayoutToXml(this._LayoutName);
            }
            catch { }
        }
        private void itemSaveLayOutDefault_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveLayoutToXml(this._LayoutNameDefault);
            }
            catch { }
        }
        private void itemResetLayOut_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(this._LayoutName)) File.Delete(this._LayoutName);                
                this.BestFitColumns();
            }
            catch (Exception ex){
                PLException.AddException(ex);
            }
        }
        
        private void gridview_DataSourceChanged(object sender, EventArgs e)
        {
            if (_PLAutoWidth == null || _PLAutoWidth == false)
            {
                if (!File.Exists(this._LayoutName))
                    this.BestFitColumns();
                
                return;
            }
            else if(_PLAutoWidth == true)//Tính tự động
            {
                this.BestFitColumns();
            }
        }
        #endregion     
        
        //#region Xử lý cột tính toán
        //private GridColumn[] calcGridColumn;
        //private void initCotTinhToan()
        //{
        //    this.calcGridColumn = new GridColumn[3];

        //    this.calcGridColumn[0] = HelpGridColumn.ThemCot(this, "", 100, 100);
        //    this.calcGridColumn[0].Tag = "NO";
        //    //this.calcGridColumn[0].FieldName = "CỘT01";
        //    this.calcGridColumn[0].Visible = false;
        //    this.calcGridColumn[0].VisibleIndex = 100;
        //    //this.calcGridColumn[0].Caption = "CỘT01";

        //    this.calcGridColumn[1] = HelpGridColumn.ThemCot(this, "", 101, 100);
        //    this.calcGridColumn[1].Tag = "NO";
        //    //this.calcGridColumn[1].FieldName = "CỘT02";
        //    this.calcGridColumn[1].Visible = false;
        //    this.calcGridColumn[1].VisibleIndex = 100;
        //    //this.calcGridColumn[1].Caption = "CỘT02";

        //    this.calcGridColumn[2] = HelpGridColumn.ThemCot(this, "", 102, 100);
        //    this.calcGridColumn[2].Tag = "NO";
        //    //this.calcGridColumn[2].FieldName = "CỘT03";            
        //    this.calcGridColumn[2].Visible = false;
        //    this.calcGridColumn[2].VisibleIndex = 100;
        //    //this.calcGridColumn[2].Caption = "CỘT03";
        //}
        //private GridColumn getCotTinhToan()
        //{
        //    GridColumn col = null;
        //    if (this.calcGridColumn[0].Tag.ToString() == "NO")
        //    {
        //        col = this.calcGridColumn[0];
        //    }

        //    else if (this.calcGridColumn[1].Tag.ToString() == "NO")
        //    {
        //        col = this.calcGridColumn[1];
        //    }

        //    else if (this.calcGridColumn[2].Tag.ToString() == "NO")
        //    {
        //        col = this.calcGridColumn[2];
        //    }

        //    if (col == null)
        //    {
        //        col = HelpGridColumn.ThemCot(this, "Cột mới 1", this.Columns.Count + 1, 100);
        //        col.Visible = false;
        //    }
            
        //    col.OptionsColumn.AllowEdit = false;
        //    col.OptionsColumn.ReadOnly = true;
        //    col.ShowUnboundExpressionMenu = true;
        //    col.VisibleIndex = this.Columns.Count + 1;
        //    return col;
        //}
        //#endregion
    }
}