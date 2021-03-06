using System;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using ProtocolVN.Framework.Core;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using System.IO;

namespace ProtocolVN.Framework.Win
{
    public class HelpGridColumn
    {
        /// <summary>Cho phép nhập nhanh 2 số 0 hoặc 3 số 0 bằng phím tắt.
        /// </summary>
        public static void SetKeys00_000(GridColumn Column)
        {
            RepositoryItem Repos = Column.ColumnEdit;
            if (Repos == null)
            {
                return;
            }
            if (Repos.EditorTypeName == "SpinEdit" || Repos.EditorTypeName == "CalcEdit")
            {
                Repos.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    decimal Val = 0;
                    try
                    {
                        //if (Repos.EditorTypeName == "SpinEdit")
                        if (Repos is RepositoryItemSpinEdit)
                        {
                            DataRow row = Column.View.GetDataRow(Column.View.FocusedRowHandle);
                            if (row[Column.FieldName] != null &&
                                row[Column.FieldName].ToString() != null &&
                                row[Column.FieldName].ToString() != "0")
                            {
                                Val = HelpNumber.ParseDecimal(row[Column.FieldName]);
                                if (ShortcutKey.K_00 == e.KeyCode)
                                    Column.View.SetFocusedRowCellValue(Column, Val * 100);
                                else if (ShortcutKey.K_000 == e.KeyCode)
                                    Column.View.SetFocusedRowCellValue(Column, Val * 1000);
                            }
                        }
                        //else if (Repos.EditorTypeName == "CalcEdit")
                        else if (Repos is RepositoryItemCalcEdit)
                        {
                            if (Column.View.EditingValue != null &&
                                Column.View.EditingValue.ToString() != "0")
                            {
                                Val = HelpNumber.ParseDecimal(Column.View.EditingValue);
                                if (ShortcutKey.K_00 == e.KeyCode)
                                    Column.View.SetFocusedRowCellValue(Column, Val * 100);
                                else if (ShortcutKey.K_000 == e.KeyCode)
                                    Column.View.SetFocusedRowCellValue(Column, Val * 1000);
                            }
                        }
                    }
                    catch { }
                };
            }
        }

        /// <summary>Đặt định dạng (Left, Center, Right) cho chiều cao của dòng.
        /// </summary>
        public static void SetHorzAlignment(GridColumn Column, HorzAlignment Content)
        {
            Column.AppearanceHeader.Options.UseTextOptions = true;
            Column.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            Column.AppearanceCell.Options.UseTextOptions = true;
            Column.AppearanceCell.TextOptions.HAlignment = Content;
        }

        /// <summary>Đặt định dạng số cho phần số tại SummaryItem của cột trong lưới
        /// soThapPhan = 0: Nó sẽ là số nguyên.
        /// </summary>
        public static void SetSummaryNumFormat(GridColumn column, int soThapPhan)
        {
            if (column.SummaryItem != null)
                column.SummaryItem.DisplayFormat = ApplyFormatAction.GetDisplayFormat(soThapPhan);
        }

        /// <summary>Đặt định dạng cho ngày. 
        /// Nếu "d" là date.
        /// Nếu "f" là full datetime.
        /// </summary>
        public static void SetDateDisplayFormat(GridColumn column, String formatStr)
        {
            column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            column.DisplayFormat.FormatString = formatStr;
        }

        /// <summary>Đặt định dạng số cho phần số tại cột column
        /// </summary>
        public static void SetNumDisplayFormat(GridColumn column, int soThapPhan)
        {
            column.DisplayFormat.FormatType = FormatType.Numeric;
            column.DisplayFormat.FormatString = ApplyFormatAction.GetDisplayFormat(soThapPhan);
        }

        /// <summary>
        /// 
        /// </summary>
        public static GridColumn CreateGridColumn(GridView grid, string caption, int VisibleIndex, int Width)
        {
            GridColumn Column = new GridColumn();
            Column.Caption = caption;
            Column.Name = caption;
            if (VisibleIndex != -2)
            {
                if (VisibleIndex == -1)
                {
                    Column.Visible = false;
                    Column.OptionsColumn.AllowFocus = false;
                    Column.OptionsColumn.ReadOnly = true;
                    Column.OptionsColumn.AllowEdit = false;
                    Column.Width = 0;
                    Column.OptionsColumn.FixedWidth = true;

                    Column.OptionsColumn.ShowInCustomizationForm = false;
                    Column.OptionsColumn.AllowShowHide = false;
                    Column.OptionsFilter.AllowFilter = false;
                }
                else
                    Column.VisibleIndex = VisibleIndex;
            }
            if (Width != -1)
            {
                Column.Width = Width;
            }

            grid.Columns.Add(Column);
            return Column;
        }

        public static BandedGridColumn CreateBandedGridColumn(GridView grid, string caption, int VisibleIndex, int Width)
        {
            BandedGridColumn Column = new BandedGridColumn();
            Column.Caption = caption;
            Column.Name = caption;
            if (VisibleIndex != -2)
            {
                if (VisibleIndex == -1)
                {
                    Column.Visible = false;
                    Column.OptionsColumn.AllowFocus = false;
                    Column.OptionsColumn.ReadOnly = true;
                    Column.OptionsColumn.AllowEdit = false;
                    Column.Width = 0;
                    Column.OptionsColumn.FixedWidth = true;

                    Column.OptionsColumn.ShowInCustomizationForm = false;
                    Column.OptionsColumn.AllowShowHide = false;
                    Column.OptionsFilter.AllowFilter = false;
                }
                else
                    Column.VisibleIndex = VisibleIndex;
            }
            if (Width != -1)
            {
                Column.Width = Width;
            }

            grid.Columns.Add(Column);
            return Column;
        }

        public static GridColumn ThemCot(GridView Grid, string Caption, int VisibleIndex, int Width)
        {
            return HelpGridColumn.CreateGridColumn(Grid, Caption, VisibleIndex, Width);
        }
        public static BandedGridColumn ThemCot(GridBand band, string Caption, int VisibleIndex, int Width)
        {
            BandedGridColumn Column = new BandedGridColumn();
            Column.Caption = Caption;
            Column.Name = Caption;
            if (VisibleIndex != -2)
            {
                if (VisibleIndex == -1)
                {
                    Column.Visible = false;
                    Column.OptionsColumn.AllowFocus = false;
                    Column.OptionsColumn.ReadOnly = true;
                    Column.OptionsColumn.AllowEdit = false;
                    Column.Width = 0;
                    Column.OptionsColumn.FixedWidth = true;

                    Column.OptionsColumn.ShowInCustomizationForm = false;
                    Column.OptionsColumn.AllowShowHide = false;
                    Column.OptionsFilter.AllowFilter = false;
                }
                else
                    Column.VisibleIndex = VisibleIndex;
            }
            if (Width != -1)
            {
                Column.Width = Width;
            }

            band.View.Columns.Add(Column);
            band.Columns.Add(Column);
            return Column;
        }

        public static void HideGridColumn(GridColumn Column)
        {
            Column.OptionsColumn.AllowFocus = false;
            Column.OptionsColumn.AllowSize = false;
            Column.OptionsColumn.FixedWidth = true;
            Column.OptionsColumn.ShowCaption = false;
            Column.OptionsColumn.ReadOnly = true;
            
            Column.OptionsColumn.ShowInCustomizationForm = false;
            Column.OptionsColumn.AllowShowHide = false;
            Column.OptionsFilter.AllowFilter = false;

            Column.Visible = false;
            Column.Width = 0;
            Column.VisibleIndex = -1;            
        }
        
        public static void AnCot(GridColumn Column)
        {
            HelpGridColumn.HideGridColumn(Column);
        }

        public static GridColumn[] CreateGridColumns(string[] Captions, bool[] Visible, params int[] Widths)
        {
            GridColumn[] Cols = new GridColumn[Captions.Length];
            int VisibleIndex = -1;
            for (int i = 0; i < Captions.Length; i++)
            {
                Cols[i] = new GridColumn();
                Cols[i].Caption = Captions[i];
                Cols[i].Name = Captions[i];
                if (Visible[i] == false) VisibleIndex = -1;
                else VisibleIndex = i;
                if (VisibleIndex == -1)
                {
                    Cols[i].Visible = false;
                    Cols[i].OptionsColumn.AllowFocus = false;
                    Cols[i].OptionsColumn.ReadOnly = true;
                    Cols[i].OptionsColumn.AllowEdit = false;
                    Cols[i].Width = 0;
                    Cols[i].OptionsColumn.FixedWidth = true;
                }
                else
                    Cols[i].VisibleIndex = VisibleIndex;
                if(Widths != null)
                    if (Widths[i] != -1) Cols[i].Width = Widths[i];
            }
            return Cols;
        }

        public static BandedGridColumn[] CreateBandedGridColumns(string[] Captions, bool[] Visible, params int[] Widths)
        {
            BandedGridColumn[] Cols = new BandedGridColumn[Captions.Length];
            int VisibleIndex = -1;
            for (int i = 0; i < Captions.Length; i++)
            {
                Cols[i] = new BandedGridColumn();
                Cols[i].Caption = Captions[i];
                Cols[i].Name = Captions[i];
                if (Visible[i] == false) VisibleIndex = -1;
                else VisibleIndex = i;
                if (VisibleIndex == -1)
                {
                    Cols[i].Visible = false;
                    Cols[i].OptionsColumn.AllowFocus = false;
                    Cols[i].OptionsColumn.ReadOnly = true;
                    Cols[i].OptionsColumn.AllowEdit = false;
                    Cols[i].Width = 0;
                    Cols[i].OptionsColumn.FixedWidth = true;
                }
                else
                    Cols[i].VisibleIndex = VisibleIndex;
                if (Widths != null)
                    if (Widths[i] != -1) Cols[i].Width = Widths[i];
            }
            return Cols;
        }

        public static void CotRepository(GridColumn Column, string ColumnField, RepositoryItem Repos, HorzAlignment HorzAlign)
        {
            HelpGridColumn.SetHorzAlignment(Column, HorzAlign);
            Column.ColumnEdit = Repos;
            if (ColumnField != null) Column.FieldName = ColumnField;
        }

        #region Phần I: Cột nhập liệu cơ bản
        public static RepositoryItemCheckEdit CotCheckEdit(GridColumn Column, string ColumnField)
        {
            HelpGridColumn.SetHorzAlignment(Column, HorzAlignment.Center);
            Column.ColumnEdit = HelpRepository.GetCheckEdit(false);            
            if (ColumnField != null) Column.FieldName = ColumnField;
            return (RepositoryItemCheckEdit)Column.ColumnEdit;
        }
        public static RepositoryItemCalcEdit CotCalcEdit(GridColumn Column, string ColumnField, int SoThapPhan)
        {
            HelpGridColumn.SetHorzAlignment(Column, HorzAlignment.Far);
            HelpGridColumn.SetSummaryNumFormat(Column, SoThapPhan);
            Column.ColumnEdit = HelpRepository.GetCalcEdit(SoThapPhan);
            if (ColumnField != null) Column.FieldName = ColumnField;
            return (RepositoryItemCalcEdit)Column.ColumnEdit;
        }
        public static RepositoryItemCalcEdit CotCalcEditInt(GridColumn Column, string ColumnField, decimal Min, decimal Max, bool AllowNULL)
        {
            HelpGridColumn.SetHorzAlignment(Column, HorzAlignment.Far);
            HelpGridColumn.SetSummaryNumFormat(Column, 0);            
            RepositoryItemCalcEdit calcHelp = HelpRepository.GetCalcEdit(-1);
            Column.ColumnEdit = calcHelp;            
            if (ColumnField != null) Column.FieldName = ColumnField;            

            if (Min == Decimal.MinValue && Max == Decimal.MaxValue) return calcHelp;
            
            if (Min >= 0)
            {
                calcHelp.KeyPress += delegate(object sender, KeyPressEventArgs e)
                {
                    if (e.KeyChar.Equals('-'))
                        e.Handled = true;
                };
            }

            calcHelp.ParseEditValue += delegate(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
            {
                CalcEdit be = sender as CalcEdit;
                if (be.EditValue == null)
                {
                    if (AllowNULL == false)
                    {
                        e.Value = Min;
                        e.Handled = true;
                    }
                    return;
                }
                decimal num = Decimal.MinValue;
                try{
                    num = decimal.Parse(be.EditValue.ToString());
                } catch { return; }

                if (num < Min)
                {
                    e.Value = Min;
                    e.Handled = true;
                }
                else if (num > Max)
                {
                    e.Value = Max;
                    e.Handled = true;
                }
            };
            return calcHelp;
        }
        public static RepositoryItemCalcEdit CotCalcEditInt(GridColumn Column, string ColumnField, decimal Min, bool AllowNULL)
        {
            return CotCalcEditInt(Column, ColumnField, Min, Decimal.MaxValue, AllowNULL);
        }
        public static RepositoryItemCalcEdit CotCalcEditInt(GridColumn Column, string ColumnField, bool AllowNULL)
        {
            return CotCalcEditInt(Column, ColumnField, decimal.MinValue, decimal.MaxValue, AllowNULL);
        }
        public static RepositoryItemCalcEdit CotCalcEditDec(GridColumn Column, string ColumnField, int SoThapPhan, decimal Min, decimal Max, bool AllowNULL)
        {
            RepositoryItemCalcEdit repos = CotCalcEditInt(Column, ColumnField, Min, Max, AllowNULL);
            ApplyFormatAction.applyElement(repos, SoThapPhan);
            HelpGridColumn.SetSummaryNumFormat(Column, SoThapPhan);
            return repos;
        }
        public static RepositoryItemCalcEdit CotCalcEditDec(GridColumn Column, string ColumnField, int SoThapPhan, decimal Min, bool AllowNULL)
        {
            return CotCalcEditDec(Column, ColumnField, SoThapPhan, Min, Decimal.MaxValue, AllowNULL);
        }
        public static RepositoryItemCalcEdit CotCalcEditDec(GridColumn Column, string ColumnField, int SoThapPhan, decimal Min, bool AllowNULL, bool AllowSpin)
        {
            RepositoryItemCalcEdit r = CotCalcEditDec(Column, ColumnField, SoThapPhan, Min, Decimal.MaxValue, AllowNULL);
            if (AllowSpin == false)
            {
                r.Spin += delegate(object sender, SpinEventArgs e)
                {
                    e.Handled = true;
                };
            }
            return r;
        }
        public static RepositoryItemCalcEdit CotCalcEditDec(GridColumn Column, string ColumnField, int SoThapPhan, bool AllowNULL)
        {
            return CotCalcEditDec(Column, ColumnField, SoThapPhan, decimal.MinValue, decimal.MaxValue, AllowNULL);
        }
        public static RepositoryItemCalcEdit CotCalcEditDec(GridColumn Column, string ColumnField, int SoThapPhan, bool AllowNULL, bool AllowSpin)
        {
            RepositoryItemCalcEdit r= CotCalcEditDec(Column, ColumnField, SoThapPhan, decimal.MinValue, decimal.MaxValue, AllowNULL);
            if (AllowSpin == false)
            {
                r.Spin += delegate(object sender, SpinEventArgs e)
                {
                    e.Handled = true;
                };
            }
            return r;
        }


        public static RepositoryItemSpinEdit CotSpinEdit(GridColumn Column, string ColumnField, int SoThapPhan)
        {
            HelpGridColumn.SetHorzAlignment(Column, HorzAlignment.Far);
            HelpGridColumn.SetSummaryNumFormat(Column, SoThapPhan);
            Column.ColumnEdit = HelpRepository.GetSpinEdit(SoThapPhan);
            if (ColumnField != null) Column.FieldName = ColumnField;
            return (RepositoryItemSpinEdit)Column.ColumnEdit;
        }
        public static RepositoryItemSpinEdit CotSpinEditInt(GridColumn Column, string ColumnField, decimal Min, decimal Max, bool AllowNULL)
        {
            HelpGridColumn.SetHorzAlignment(Column, HorzAlignment.Far);
            HelpGridColumn.SetSummaryNumFormat(Column, 0);
            RepositoryItemSpinEdit spinHelp = HelpRepository.GetSpinEdit(-1);
            Column.ColumnEdit = spinHelp;
            if (ColumnField != null) Column.FieldName = ColumnField;

            if (Min == decimal.MinValue && Max == decimal.MaxValue) return spinHelp;
            if (Min >= 0)
            {
                spinHelp.KeyPress += delegate(object sender, KeyPressEventArgs e)
                {
                    if (e.KeyChar.Equals('-'))
                        e.Handled = true;
                };
            }
            spinHelp.Spin += delegate(object sender, DevExpress.XtraEditors.Controls.SpinEventArgs e)
            {
                BaseEdit be = sender as BaseEdit;
                if (be.EditValue != null)
                {
                    try
                    {
                        if ((decimal)be.EditValue == Min)
                        {
                            if (!e.IsSpinUp)
                                e.Handled = true;
                        }
                        else if ((decimal)be.EditValue == Max)
                        {
                            if (e.IsSpinUp)
                                e.Handled = true;
                        }
                    }
                    catch { }
                }
            };
            spinHelp.ParseEditValue += delegate(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
            {
                BaseEdit be = sender as BaseEdit;
                if (be.EditValue == null)
                {
                    if (AllowNULL == false)
                    {
                        e.Value = Min;
                        e.Handled = true;
                    }
                    return;
                }
                try
                {
                    if ((decimal)be.EditValue < Min)
                    {
                        e.Value = Min;
                        e.Handled = true;
                    }
                    else if ((decimal)be.EditValue > Max)
                    {
                        e.Value = Max;
                        e.Handled = true;
                    }
                }
                catch { }
            };
            return spinHelp;
        }
        public static RepositoryItemSpinEdit CotSpinEditInt(GridColumn Column, string ColumnField, decimal Min, bool AllowNULL)
        {
            return CotSpinEditInt(Column, ColumnField, Min, Decimal.MaxValue, AllowNULL);
        }
        public static RepositoryItemSpinEdit CotSpinEditInt(GridColumn Column, string ColumnField, bool AllowNULL)
        {
            return CotSpinEditInt(Column, ColumnField, decimal.MinValue, decimal.MaxValue, AllowNULL);
        }
        public static RepositoryItemSpinEdit CotSpinEditDec(GridColumn Column, string ColumnField, int SoThapPhan, decimal Min, decimal Max, bool AllowNULL)
        {
            RepositoryItemSpinEdit repos = CotSpinEditInt(Column, ColumnField, Min, Max, AllowNULL);
            ApplyFormatAction.applyElement(repos, SoThapPhan);
            HelpGridColumn.SetSummaryNumFormat(Column, SoThapPhan);

            return repos;
        }
        public static RepositoryItemSpinEdit CotSpinEditDec(GridColumn Column, string ColumnField, int SoThapPhan, decimal Min, bool AllowNULL)
        {
            return CotSpinEditDec(Column, ColumnField, SoThapPhan, Min, Decimal.MaxValue, AllowNULL);
        }
        public static RepositoryItemSpinEdit CotSpinEditDec(GridColumn Column, string ColumnField, int SoThapPhan, bool AllowNULL)
        {
            return CotSpinEditDec(Column, ColumnField, SoThapPhan, Decimal.MinValue, Decimal.MaxValue, AllowNULL);
        }

        #region CotDateEdit
        public static RepositoryItemDateEdit CotDateEdit(GridColumn Column, string ColumnField, string Format)
        {
            HelpGridColumn.SetHorzAlignment(Column, HorzAlignment.Center);
            HelpGridColumn.SetDateDisplayFormat(Column, Format);
            Column.ColumnEdit = HelpRepository.GetDateEdit(Format);
            if (ColumnField != null) Column.FieldName = ColumnField;
            return (RepositoryItemDateEdit)Column.ColumnEdit;
        }
        public static RepositoryItemDateEdit CotDateEdit(GridColumn Column, string ColumnField)
        {
            return CotDateEdit(Column, ColumnField, "d");
        }
        public static RepositoryItemDateEdit CotDateEdit(GridColumn Column, string ColumnField, bool AllowSpin)
        {
            
            RepositoryItemDateEdit r= CotDateEdit(Column, ColumnField, "d");
            if (AllowSpin == false)
            {
                r.Spin += delegate(object sender, SpinEventArgs e)
                {
                    e.Handled = true;
                };
            }
            return r;
        }

        #endregion

        #region MemoEdit
        public static RepositoryItemMemoExEdit CotMemoExEdit(GridColumn Column, string ColumnField)
        {
            HelpGridColumn.SetHorzAlignment(Column, HorzAlignment.Near);
            Column.ColumnEdit = HelpRepository.GetMemoExEdit();
            if (ColumnField != null) Column.FieldName = ColumnField;
            return (RepositoryItemMemoExEdit)Column.ColumnEdit;
        }
        public static RepositoryItemMemoEdit CotMemoEdit(GridView grid, GridColumn Column, string ColumnField)
        {
            RepositoryItemMemoEdit MemoEditColumn = new RepositoryItemMemoEdit();
            ((System.ComponentModel.ISupportInitialize)(MemoEditColumn)).BeginInit();
            Column.ColumnEdit = MemoEditColumn;
            MemoEditColumn.Name = "MemoEditColumn";
            Column.FieldName = ColumnField;
            ((System.ComponentModel.ISupportInitialize)(MemoEditColumn)).EndInit();
            MemoEditColumn.LinesCount = 2;
            MemoEditColumn.AutoHeight = true;
            grid.OptionsView.RowAutoHeight = true;
            return MemoEditColumn;
        }
        #endregion

        #region CotCombobox
        //public static RepositoryItemImageComboBox CotCombobox(GridColumn column, DataSet ds, string IDField, string DisplayField, string ColumnField)
        public static RepositoryItemLookUpEdit CotCombobox(GridColumn column, DataSet ds, string IDField, string DisplayField, string ColumnField)
        {
            RepositoryItemLookUpEdit repos = CotPLLookUp(column, IDField, DisplayField, ds.Tables[0], 
                new string[] { DisplayField }, 
                new string[] { "Caption" }, 
                ColumnField, 
                new int[]{ column.VisibleWidth });
            repos.ShowHeader = false;
            return repos;
            //column.AppearanceHeader.Options.UseTextOptions = true;
            //column.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //column.AppearanceCell.Options.UseTextOptions = true;
            //column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            
            //RepositoryItemImageComboBox rCBB = new RepositoryItemImageComboBox();
            ////rCBB.Name = "repositoryItemImageComboBoxCode" + LookupTable;
            //foreach (DataRow row in ds.Tables[0].Rows)
            //    rCBB.Items.Add(new ImageComboBoxItem("" + row[DisplayField].ToString(), row[IDField]));

            //column.ColumnEdit = rCBB;
            //if (ColumnField != null) column.FieldName = ColumnField;
            //return rCBB;
        }
        public static RepositoryItemLookUpEdit CotCombobox(GridColumn column, DataSet ds, string IDField, string DisplayField, string ColumnField,bool AllowBlank)
        {
            RepositoryItemLookUpEdit repos = CotPLLookUp(column, IDField, DisplayField, ds.Tables[0],
                new string[] { DisplayField },
                new string[] { "Caption" },
                ColumnField,AllowBlank,
                new int[] { column.VisibleWidth });
            repos.ShowHeader = false;
            return repos;
            //column.AppearanceHeader.Options.UseTextOptions = true;
            //column.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //column.AppearanceCell.Options.UseTextOptions = true;
            //column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;

            //RepositoryItemImageComboBox rCBB = new RepositoryItemImageComboBox();
            ////rCBB.Name = "repositoryItemImageComboBoxCode" + LookupTable;
            //foreach (DataRow row in ds.Tables[0].Rows)
            //    rCBB.Items.Add(new ImageComboBoxItem("" + row[DisplayField].ToString(), row[IDField]));

            //column.ColumnEdit = rCBB;
            //if (ColumnField != null) column.FieldName = ColumnField;
            //return rCBB;
        }
        //public static RepositoryItemImageComboBox CotCombobox(GridColumn column, string LookupTable, string IDField, string DisplayField, string ColumnField)
        public static RepositoryItemLookUpEdit CotCombobox(GridColumn column, string LookupTable, string IDField, string DisplayField, string ColumnField)
        {
            RepositoryItemLookUpEdit repos = CotLookUp(column, IDField, DisplayField, LookupTable, 
                new string[]{DisplayField}, 
                new string[]{"Caption"},
                ColumnField, 
                new int[]{column.VisibleWidth});
            repos.ShowHeader = false;
            return repos;
            //DataSet ds = DatabaseFBExt.LoadTable(LookupTable, DisplayField);
            //return CotCombobox(column, ds, IDField, DisplayField, ColumnField);
        }
        //public static RepositoryItemImageComboBox CotCombobox(GridColumn column, string LookupTable, string IDField, string DisplayField, string ColumnField, string where)
        public static RepositoryItemLookUpEdit CotCombobox(GridColumn column, string LookupTable, string IDField, string DisplayField, string ColumnField, string where)
        {
            DataSet ds = DatabaseFBExt.LoadTable(LookupTable, where, DisplayField, true);
            return CotCombobox(column, ds, IDField, DisplayField, ColumnField);
        }
        #endregion

        #region CotPLCombobox
        //public static RepositoryItemImageComboBox CotPLCombobox(GridColumn Column, DataSet Src, string IDField, string DisplayField, string ColumnField)
        public static RepositoryItemLookUpEdit CotPLCombobox(GridColumn Column, DataSet Src, string IDField, string DisplayField, string ColumnField)
        {
            return CotCombobox(Column, Src, IDField, DisplayField, ColumnField);
        }
        #endregion

        #region CotLookUp
        public static RepositoryItemLookUpEdit CotLookUp(GridColumn column, string IDField, string DisplayField, string LookupTable, string[] LookupVisibleFields, string[] Captions, string ColumnField, int[] Widths, bool UsingVisible, bool AllowBlank)
        {
            DataTable Data = null;
            if (UsingVisible == false)
                Data = DABase.getDatabase().LoadDataSet(HelpSQL.SelectAll(LookupTable, LookupVisibleFields[0], true)).Tables[0];
            else
                Data = DABase.getDatabase().LoadDataSet(HelpSQL.SelectWhere(LookupTable, "VISIBLE_BIT = 'Y'", LookupVisibleFields[0], true)).Tables[0];

            return CotPLLookUp(column, IDField, DisplayField, Data, LookupVisibleFields, Captions, ColumnField, AllowBlank, Widths);
        }

        public static RepositoryItemLookUpEdit CotLookUp(GridColumn column, string IDField, string DisplayField, string LookupTable, string[] LookupVisibleFields, string[] Captions, string ColumnField, int[] Widths, bool UsingVisible)
        {
            return CotLookUp(column, IDField, DisplayField, LookupTable, LookupVisibleFields, Captions, ColumnField, Widths, UsingVisible, false);
        }

        public static RepositoryItemLookUpEdit CotLookUp(GridColumn column, string IDField, string DisplayField, string LookupTable, string[] LookupVisibleFields, string[] Captions, string ColumnField, int[] Widths)
        {
            return CotLookUp(column, IDField, DisplayField, LookupTable, LookupVisibleFields, Captions, ColumnField, Widths, false, false);
        }
        #endregion 

        #region CotText
        public static void CotText(GridColumn Column, string ColumnField, DevExpress.Utils.HorzAlignment Alignment)
        {
            HelpGridColumn.SetHorzAlignment(Column, Alignment);
            if (ColumnField != null) Column.FieldName = ColumnField;
        }
        public static void CotTextLeft(GridColumn Column, string ColumnField)
        {
            CotText(Column, ColumnField, HorzAlignment.Near);
        }
        public static void CotTextRight(GridColumn Column, string ColumnField)
        {
            CotText(Column, ColumnField, HorzAlignment.Far);
        }
        public static void CotTextCenter(GridColumn Column, string ColumnField)
        {
            CotText(Column, ColumnField, HorzAlignment.Center);
        }
        #endregion

        #endregion

        #region Phần II: Tạo cột chỉ đọc
        public static void CotReadOnlyNumber(GridColumn Column, string ColumnField, int SoThapPhan)
        {
            HelpGridColumn.SetHorzAlignment(Column, HorzAlignment.Far);
            HelpGridColumn.SetNumDisplayFormat(Column, SoThapPhan);
            if (ColumnField != null) Column.FieldName = ColumnField;
        }
        public static void CotReadOnlyNumber(GridColumn Column, string FieldName)
        {
            CotReadOnlyNumber(Column, FieldName, HelpNumber.ParseInt32(FrameworkParams.option.round));
        }
        public static void CotReadOnlyDate(GridColumn Column, string ColumnField, string Format)
        {
            HelpGridColumn.SetHorzAlignment(Column, HorzAlignment.Center);
            HelpGridColumn.SetDateDisplayFormat(Column, Format);
            if (ColumnField != null) Column.FieldName = ColumnField;
        }
        public static void CotReadOnlyDate(GridColumn Column, string ColumnField)
        {
            CotReadOnlyDate(Column, ColumnField, "d");
        }
        #endregion 
        
        #region Cột PL
        #region CotPLVerticalCheck && CotPLVerticalCheckUDB
        /// <summary>
        /// Cột cho phép chọn 1 trong nhiều dòng trên lưới.
        /// USING: TRIAL
        /// PROJECTS: DA
        /// </summary>
        public static RepositoryItemCheckEdit CotRadio(GridColumn Column, string FieldName)
        {
            GridView grid = Column.View as GridView;
            RepositoryItemCheckEdit itemCheck = HelpGridColumn.CotCheckEdit(Column, FieldName);
            itemCheck.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            grid.FocusedRowChanged += delegate(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
            {
                DataRow row = grid.GetDataRow(e.FocusedRowHandle);
                Column.OptionsColumn.AllowEdit = (row == null || !row[FieldName].Equals(itemCheck.ValueChecked));

            };
            grid.CellValueChanging += delegate(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
            {
                if (e.Column.FieldName == FieldName)
                {
                    if (e.Value.Equals(itemCheck.ValueChecked))
                    {
                        DataTable dtSource = grid.GridControl.DataSource as DataTable;
                        DataRow focusRow = grid.GetDataRow(e.RowHandle);
                        focusRow[FieldName] = itemCheck.ValueUnchecked;
                        DataRow[] rowsCheck = dtSource.Select(string.Format("{0}={1}", FieldName, (itemCheck.ValueChecked is string ? "'" + itemCheck.ValueChecked + "'" : itemCheck.ValueChecked)));
                        foreach (DataRow rC in rowsCheck)
                        {
                            if (rC.Equals(focusRow)) continue;
                            rC[FieldName] = itemCheck.ValueUnchecked;
                        }
                        focusRow[FieldName] = e.Value;
                        Column.OptionsColumn.AllowEdit = false;
                    }
                }
            };
            return itemCheck;
        }

        public static RepositoryItemCheckEdit CotPLImageCheck(GridColumn Column, string ColumnField)
        {
            HelpGridColumn.SetHorzAlignment(Column, HorzAlignment.Center);
            Column.ColumnEdit = HelpRepository.GetCheckEdit(true);
            if (ColumnField != null) Column.FieldName = ColumnField;
            return (RepositoryItemCheckEdit)Column.ColumnEdit;
        }
        /// <summary>Tạo cột chọn một trong các phần tử trên lưới
        /// Không cập nhật DB ngay mà cập nhật trên lưới sau đó update sau
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="column"></param>
        /// <param name="ColumnField"></param>
        /// <returns></returns>
        public static RepositoryItemCheckEdit CotPLVerticalCheck(GridView gridView, GridColumn column, string ColumnField)
        {
            RepositoryItemCheckEdit checkEdit = CotPLImageCheck(column, ColumnField);
            gridView.CellValueChanging += delegate(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
            {
                DataView view = (DataView)gridView.DataSource;
                DataTable table = view.Table;
                if (e.Column.FieldName == ColumnField)
                    for (int i = 0; i < gridView.RowCount; i++)
                        if (i != e.RowHandle)
                        {
                            gridView.SetRowCellValue(i, gridView.Columns[ColumnField], "");
                            table.Rows[i][ColumnField] = "N";
                        }
            };
            return checkEdit;
        }

        public static RepositoryItemCheckEdit CotPLVerticalCheckUDB(GridView gridView, GridColumn column, string ColumnField, PLDelegation.ProcessGridView UpdateYes, PLDelegation.ProcessGridView UpdateNo)
        {
            RepositoryItemCheckEdit checkEdit = CotPLImageCheck(column, ColumnField);
            gridView.CellValueChanging += delegate(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
            {
                if (e.Column.FieldName == ColumnField)
                    for (int i = 0; i < gridView.RowCount; i++)
                        if (i != e.RowHandle)
                            gridView.SetRowCellValue(i, gridView.Columns[ColumnField], "");
                DataView view = (DataView)gridView.DataSource;
                DataTable table = view.Table;
                string checkColumn = table.Rows[e.RowHandle][ColumnField].ToString();

                if (checkColumn == "Y")
                {
                    UpdateYes(gridView);
                    //UpdateStateNo(table.TableName, ColumnField);
                }
                else
                {
                    UpdateNo(gridView);
                    //string id = table.Rows[e.RowHandle][KeyField].ToString();
                    //if (UpdateStateNo(table.TableName, ColumnField))
                    //    UpdateStateYes(table.TableName, ColumnField, KeyField, id);
                }
            };
            return checkEdit;
        }
        /// <summary>Tạo cột chọn một trong các phần tử trên lưới cập nhật ngay khi chọn
        /// </summary>
        public static RepositoryItemCheckEdit CotPLVerticalCheckUDB(GridView gridView, GridColumn column, string ColumnField, string KeyField)
        {
            RepositoryItemCheckEdit checkEdit = CotPLImageCheck(column, ColumnField);
            gridView.CellValueChanging += delegate(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
            {
                if (e.Column.FieldName == ColumnField)
                    for (int i = 0; i < gridView.RowCount; i++)
                        if (i != e.RowHandle)
                            gridView.SetRowCellValue(i, gridView.Columns[ColumnField], "");
                DataView view = (DataView)gridView.DataSource;
                DataTable table = view.Table;
                string checkColumn = table.Rows[e.RowHandle][ColumnField].ToString();
                if (checkColumn == "Y")
                    UpdateStateNo(table.TableName, ColumnField);
                else
                {
                    string id = table.Rows[e.RowHandle][KeyField].ToString();
                    if (UpdateStateNo(table.TableName, ColumnField))
                        UpdateStateYes(table.TableName, ColumnField, KeyField, id);
                }
            };
            return checkEdit;
        }
        private static bool UpdateStateNo(string Table, string ValueField)
        {
            DbCommand command = DABase.getDatabase().GetSQLStringCommand("UPDATE " + Table + " SET " + ValueField + " = 'N' " + " WHERE " + ValueField + "= 'Y'");
            DABase.getDatabase().Open();
            try
            {
                command.Connection = DABase.getDatabase().OpenConnection();
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
            finally
            {
                DABase.getDatabase().Close();
            }
            return false;
        }
        private static bool UpdateStateYes(string Table, string ValueField, string KeyField, string id)
        {
            DbCommand command = DABase.getDatabase().
                GetSQLStringCommand("UPDATE " + Table + " SET " + ValueField + " = 'Y' "
                + " WHERE " + KeyField + "=" + id);
            DABase.getDatabase().Open();
            try
            {
                command.Connection = DABase.getDatabase().OpenConnection();
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
            finally
            {
                DABase.getDatabase().Close();
            }
            return false;
        }
        #endregion

        #region CotPLDuyet
        public static RepositoryItemImageComboBox CotPLDuyet(GridColumn Column, string ColumnField)
        {
            HelpGridColumn.SetHorzAlignment(Column, HorzAlignment.Center);
            Column.ColumnEdit = HelpRepository.GetCotDuyet();
            
            Column.Caption = "Tình trạng";
            Column.Width = 70;
            Column.OptionsColumn.FixedWidth = true;
            Column.OptionsColumn.AllowSize = false;            
            Column.VisibleIndex = 40;

            if (ColumnField != null) Column.FieldName = ColumnField;
            return (RepositoryItemImageComboBox)Column.ColumnEdit;
        }
        #endregion

        #region CotPLVAT
        public static RepositoryItemComboBox CotPLVAT(GridColumn Column, string ColumnField, bool HasInput)
        {
            if (HasInput)
                return CotVAT(Column, ColumnField);
            else
            {
                CotReadOnlyVAT(Column, ColumnField);
                return null;
            }
        }
        public static RepositoryItemComboBox CotVAT(GridColumn Column, string ColumnField)
        {
            Column.ColumnEdit = HelpRepository.GetCotVAT();
            if (ColumnField != null) Column.FieldName = ColumnField;
            Column.OptionsColumn.AllowSize = false;
            Column.OptionsColumn.FixedWidth = true;
            Column.Width = 60;
            return (RepositoryItemComboBox)Column.ColumnEdit;
        }
        public static void CotReadOnlyVAT(GridColumn Column, string ColumnField)
        {
            HelpGridColumn.SetHorzAlignment(Column, HorzAlignment.Far);

            if (ColumnField != null) Column.FieldName = ColumnField;
            Column.OptionsColumn.AllowSize = false;
            Column.OptionsColumn.FixedWidth = true;
            Column.Width = 60;
        }
        #endregion

        #region CotPLHienThi
        public static RepositoryItemCheckEdit CotPLHienThi(GridColumn column, string ColumnField)
        {
            RepositoryItemCheckEdit ret = CotCheckEdit(column, ColumnField);
            column.OptionsColumn.FixedWidth = true;
            column.Width = 90;
            return ret;
        }
        public static RepositoryItemCheckEdit CotPLDangDung(GridColumn column, string ColumnField)
        {
            return CotPLHienThi(column, ColumnField);
        }

        #endregion

        #region CotPLDong - PHUOCNT: Refactor thành 1
        public static GridColumn CotPLDong(GridControl GridCtrl, GridView Grid, bool isConfirmDelete)
        {
            DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditDEL = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            repositoryItemButtonEditDEL.AutoHeight = false;
            repositoryItemButtonEditDEL.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            repositoryItemButtonEditDEL.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", 10, true, true, false, DevExpress.Utils.HorzAlignment.Center, null, new DevExpress.Utils.KeyShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))});
            repositoryItemButtonEditDEL.Name = "repositoryItemButtonEditDEL";
            repositoryItemButtonEditDEL.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // repositoryItemButtonEditDEL.KeyUp += delegate(object sender, KeyEventArgs e)
            // {
            //    if (e.KeyData == Keys.Enter)
            //        Grid.DeleteRow(Grid.FocusedRowHandle);
            // };
            repositoryItemButtonEditDEL.Click += delegate(object sender, EventArgs e)
            {
                if (isConfirmDelete)
                {
                    if (PLMessageBox.ShowConfirmMessage("Bạn có muốn xóa dòng này không ?") == DialogResult.Yes)
                        Grid.DeleteRow(Grid.FocusedRowHandle);
                }
                else
                {
                    Grid.DeleteRow(Grid.FocusedRowHandle);

                }
            };
            GridCtrl.RepositoryItems.Add(repositoryItemButtonEditDEL);

            GridColumn CotXoa = new GridColumn();
            CotXoa.AppearanceHeader.Options.UseTextOptions = true;
            CotXoa.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            CotXoa.AppearanceCell.Options.UseTextOptions = true;
            CotXoa.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            CotXoa.Caption = "    ";
            CotXoa.ColumnEdit = repositoryItemButtonEditDEL;
            CotXoa.Name = "CotXoa";
            CotXoa.OptionsColumn.AllowSize = false;
            CotXoa.OptionsColumn.FixedWidth = true;
            CotXoa.Visible = true;
            CotXoa.VisibleIndex = 50;
            CotXoa.Width = 25;
            Grid.Columns.Add(CotXoa);

            return CotXoa;
        }
        public static BandedGridColumn CotPLDong(GridControl GridCtrl, BandedGridView Grid, bool isConfirmDelete)
        {
            DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditDEL = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            repositoryItemButtonEditDEL.AutoHeight = false;
            repositoryItemButtonEditDEL.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            repositoryItemButtonEditDEL.Buttons.Clear();
            repositoryItemButtonEditDEL.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", 10, true, true, false, DevExpress.Utils.HorzAlignment.Center, null, new DevExpress.Utils.KeyShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))});
            repositoryItemButtonEditDEL.Name = "repositoryItemButtonEditDEL";
            repositoryItemButtonEditDEL.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // repositoryItemButtonEditDEL.KeyUp += delegate(object sender, KeyEventArgs e)
            // {
            //    if (e.KeyData == Keys.Enter)
            //        Grid.DeleteRow(Grid.FocusedRowHandle);
            // };
            repositoryItemButtonEditDEL.Click += delegate(object sender, EventArgs e)
            {
                if (isConfirmDelete)
                {
                    if (PLMessageBox.ShowConfirmMessage("Bạn có muốn xóa dòng này không ?") == DialogResult.Yes)
                        Grid.DeleteRow(Grid.FocusedRowHandle);
                }
                else
                {
                    Grid.DeleteRow(Grid.FocusedRowHandle);

                }
            };
            GridCtrl.RepositoryItems.Add(repositoryItemButtonEditDEL);

            BandedGridColumn CotXoa = new BandedGridColumn();
            CotXoa.AppearanceHeader.Options.UseTextOptions = true;
            CotXoa.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            CotXoa.AppearanceCell.Options.UseTextOptions = true;
            CotXoa.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            CotXoa.Caption = "    ";
            CotXoa.ColumnEdit = repositoryItemButtonEditDEL;
            CotXoa.Name = "CotXoa";
            CotXoa.OptionsColumn.AllowSize = false;
            CotXoa.OptionsColumn.FixedWidth = true;
            CotXoa.Visible = true;
            CotXoa.VisibleIndex = 50;
            CotXoa.Width = 25;

            GridBand gridBand = new GridBand();
            gridBand.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridBand.Columns.Add(CotXoa);
            gridBand.MinWidth = 10;
            gridBand.Name = "gridBandCotXoa";
            gridBand.Caption = "    ";
            gridBand.OptionsBand.AllowSize = false;
            gridBand.Width = 10;

            Grid.Bands.Add(gridBand);
            Grid.Columns.Add(CotXoa);
            //Grid.Columns.Add(CotXoa);

            return CotXoa;
        }
        public static GridColumn CotPLDong(GridControl GridCtrl, GridView Grid)
        {
            return HelpGridColumn.CotPLDong(GridCtrl, Grid, true);
        }
        public static BandedGridColumn CotPLDong(GridControl GridCtrl, BandedGridView Grid)
        {
            return HelpGridColumn.CotPLDong(GridCtrl, Grid, true);
        }
        #endregion

        #region CotPLDate
        public static RepositoryItemDateEdit CotPLDate(GridColumn Column, string ColumnField, bool HasInput)
        {
            if (HasInput)
            {
                return CotDateEdit(Column, ColumnField, "d");
            }
            else
            {
                CotReadOnlyDate(Column, ColumnField, "d");
                return null;
            }
        }
        public static RepositoryItemDateEdit CotPLFullDate(GridColumn Column, string ColumnField, bool HasInput)
        {
            if (HasInput)
            {
                return CotDateEdit(Column, ColumnField, "d");
            }
            else
            {
                CotReadOnlyDate(Column, ColumnField, "d");
                return null;
            }
        }
        #endregion

        #region CotPLTien & CotPLSoLuong & CotPLTrongLuong
        public static RepositoryItemCalcEdit CotPLTien(GridColumn Column, string ColumnField, bool HasInput)
        {
            if (HasInput)
            {
                return CotCalcEdit(Column, ColumnField, FormatParams.SO_TIEN);
            }
            else
            {
                CotReadOnlyNumber(Column, ColumnField, FormatParams.SO_TIEN);
                return null;
            }
        }
        public static RepositoryItemCalcEdit CotPLSoLuong(GridColumn Column, string ColumnField, bool HasInput)
        {
            if (HasInput)
            {
                return CotCalcEdit(Column, ColumnField, FormatParams.SO_LUONG);
            }
            else
            {
                CotReadOnlyNumber(Column, ColumnField, FormatParams.SO_LUONG);
                return null;
            }
        }
        public static RepositoryItemCalcEdit CotPLTrongLuong(GridColumn Column, string ColumnField, bool HasInput)
        {
            if (HasInput)
            {
                return CotCalcEdit(Column, ColumnField, FormatParams.SO_TRONG_LUONG);
            }
            else
            {
                CotReadOnlyNumber(Column, ColumnField, FormatParams.SO_TRONG_LUONG);
                return null;
            }
        }
        #endregion

        #region CotPLMultilineEdit
        public static RepositoryItemMemoExEdit CotPLMultiLineEdit(GridView view, GridColumn Column, string ColumnField)
        {
            HelpGridColumn.SetHorzAlignment(Column, HorzAlignment.Near);

            RepositoryItemTextEdit editorForDisplay = new RepositoryItemTextEdit();
            RepositoryItemMemoExEdit editorForEditing = new RepositoryItemMemoExEdit();
            view.Columns[ColumnField].ColumnEdit = editorForDisplay;
            view.CustomRowCellEditForEditing += delegate(object sender, CustomRowCellEditEventArgs e)
            {
                if (e.Column.FieldName == ColumnField)
                {
                    e.RepositoryItem = editorForEditing;
                }
            };
            return editorForEditing;
        }
        #endregion

        #region CotPLLookUp
        public static RepositoryItemLookUpEdit CotPLLookUp(GridColumn column, string IDField, string DisplayField, DataTable DataLookup, string[] LookupVisibleFields, string[] Captions, string ColumnField, params int[] Widths)
        {
            return CotPLLookUp(column, IDField, DisplayField, DataLookup, LookupVisibleFields, Captions, ColumnField, true, Widths);
        }

        public static RepositoryItemLookUpEdit CotPLLookUp(GridColumn column, string IDField, string DisplayField, DataTable DataLookup, string[] LookupVisibleFields, string[] Captions, string ColumnField, bool AllowBlank, params int[] Widths)
        {
            HelpGridColumn.SetHorzAlignment(column, DevExpress.Utils.HorzAlignment.Near);
            column.ColumnEdit = HelpRepository.GetCotPLLookUp(IDField, DisplayField, DataLookup, 
                    LookupVisibleFields, Captions, ColumnField, AllowBlank, Widths);
            if (ColumnField != null) column.FieldName = ColumnField;
            return (RepositoryItemLookUpEdit)column.ColumnEdit;
        }        
        #endregion

        public static RepositoryItemTimeEdit CotPLTimeEdit(GridColumn Column, string ColumnField, string Format, HorzAlignment HAlign, HourFormat HFormat)
        {
            HelpGridColumn.SetHorzAlignment(Column, HAlign);
            Column.ColumnEdit = HelpRepository.GetCotPLTimeEdit(Format,HFormat);
            if (ColumnField != null) Column.FieldName = ColumnField;
            return (RepositoryItemTimeEdit)Column.ColumnEdit; 
        }
        public static RepositoryItemTimeEdit CotPLTimeEdit(GridColumn Column, string ColumnField, string Format)
        {
            return CotPLTimeEdit(Column, ColumnField, Format, HorzAlignment.Center,HourFormat.Default);
        }

        public static RepositoryItemTimeEdit CotPLTimeEdit(GridColumn Column, string ColumnField)
        {
            return CotPLTimeEdit(Column, ColumnField, null);
        }

        #endregion

        #region Cột phụ thuộc giá trị cột khác.
        /// <summary>
        /// TABLE DU_AN(ID, NAME, LOAI_DU_AN) - CotDuAn ( Da khoi tao)
        /// TABLE CONG_VIEC (ID, NAME, LOAI_DU_AN) - CotCongViec
        /// Cần tạo một cột công việc nhưng dựa vào giá trị đang chọn ở cột dự án
        /// CotPLDependRelation(Grid, 
        ///         CotCongViec, "CV_ID", CONG_VIEC, NAME, ID, 
        ///         LOAI_DU_AN, 
        ///         CotDuAn, "DA_ID", DU_AN, ID)
        /// </summary>        
        public static RepositoryItemPopupContainerEdit CotPLDependRelation(GridView gridView, 
            GridColumn Column, string ColumnField, string TableName, string DisplayField, string ValueField, 
            string LinkField, 
            GridColumn DependColumn, string DependColumnField, string DependTableName, string DependValueField)
        {
            DataTable srcValueObj = DABase.getDatabase().LoadTable(TableName).Tables[0];
            DataTable srcObj = DABase.getDatabase().LoadTable(DependTableName).Tables[0];

            DataTable srcNew = new DataTable();
            srcNew.Columns.Add(new DataColumn("OBJVALUE"));
            srcNew.Columns.Add(new DataColumn(ValueField));
            srcNew.Columns.Add(new DataColumn(DisplayField));
            RepositoryItemImageComboBox resCombo = (RepositoryItemImageComboBox)DependColumn.ColumnEdit;
            for (int i = 0; i < resCombo.Items.Count; i++)
            {
                string objId = resCombo.Items[i].Value.ToString();
                DataRow[] selRow = srcObj.Select(DependValueField + "=" + objId);
                string typeObj = "";
                if (selRow.Length > 0)
                    typeObj = selRow[0][LinkField].ToString();

                DataRow[] srcRowObj = srcValueObj.Select(LinkField + "=" + typeObj);

                foreach (DataRow dr in srcRowObj)
                {
                    DataRow newRow = srcNew.NewRow();
                    newRow["OBJVALUE"] = objId;
                    newRow[ValueField] = dr[ValueField];
                    newRow[DisplayField] = dr[DisplayField];
                    srcNew.Rows.Add(newRow);
                }

            }
            string cotAo = ColumnField + "_PLV";
            // colObject.FieldName = SrcColFieldName;
            // Tao cot ao trong GridView
            try
            {
                DataTable source = (DataTable)gridView.GridControl.DataSource;
                if (source != null)
                    source.Columns.Add(new DataColumn(cotAo));
                else
                {
                    gridView.GridControl.DataSourceChanged += delegate(object sender, EventArgs e)
                    {
                        // gridView.SetFocusedRowCellValue(valueOfObj, "");
                        DataTable src = (DataTable)(gridView.GridControl.DataSource);
                        if (!src.Columns.Contains(cotAo))
                            src.Columns.Add(new DataColumn(cotAo));
                        // SetValueTable(ref src, srcNew, SrcColumnField, DesColumnField, cotAo, DesDisplayField, DesValueField);
                        foreach (DataRow row in src.Rows)
                        {
                            string objid = row[DependColumnField].ToString();
                            string objvalueid = row[ColumnField].ToString();

                            DataView view = srcNew.DefaultView;
                            view.RowFilter = "OBJVALUE='" + objid + "' and " + ValueField + "='" + objvalueid + "'";
                            if (view.Count > 0)
                                row[cotAo] = view[0][DisplayField];
                        }
                    };
                }
            }
            catch { }
            Column.FieldName = cotAo;

            // Tao datasource moi

            int isUpdateLookup = 0;
            // Khoi tao doi tuong GridControl va GridView
            DevExpress.XtraGrid.Views.Grid.GridView gridViewLookup = new GridView();
            DevExpress.XtraGrid.GridControl gridLookup = new DevExpress.XtraGrid.GridControl();

            // Thiet lap cac thuoc tinh cho doi tuong GridControl va GridView
            gridLookup.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLookup.EmbeddedNavigator.Name = "";
            gridLookup.FormsUseDefaultLookAndFeel = false;
            gridLookup.Location = new System.Drawing.Point(0, 0);
            gridLookup.MainView = gridViewLookup;
            gridLookup.Name = "gridLookup";
            gridLookup.Size = new System.Drawing.Size(200, 100);
            gridLookup.TabIndex = 2;
            gridLookup.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewLookup });
            gridLookup.MouseMove += delegate(object sender, MouseEventArgs e)
            {
                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = gridViewLookup.CalcHitInfo(new System.Drawing.Point(e.X, e.Y));
                if (hi.RowHandle >= 0) gridViewLookup.FocusedRowHandle = hi.RowHandle;
            };
            gridLookup.Click += delegate(object sender, EventArgs e)
            {
                isUpdateLookup = 1;
                gridView.Focus();
            };
            gridLookup.DataSource = srcNew;
            // Tao gridView

            gridViewLookup.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            gridViewLookup.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewLookup.GridControl = gridLookup;
            gridViewLookup.Name = "gridViewLookup";
            gridViewLookup.OptionsBehavior.Editable = false;
            gridViewLookup.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewLookup.OptionsView.ShowColumnHeaders = false;
            gridViewLookup.OptionsView.ShowDetailButtons = false;
            gridViewLookup.OptionsView.ShowGroupPanel = false;
            gridViewLookup.OptionsView.ShowIndicator = false;
            DevExpress.XtraGrid.Columns.GridColumn gridColumnNameObject = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnNameObject.Caption = "NAME";
            gridColumnNameObject.FieldName = "NAME";
            gridColumnNameObject.Name = "gridColumnNameObject";
            gridColumnNameObject.Visible = true;
            gridColumnNameObject.VisibleIndex = 0;
            gridViewLookup.Columns.Add(gridColumnNameObject);

            gridViewLookup.KeyDown += delegate(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    isUpdateLookup = 1;
                    gridView.Focus();
                }
            };
            // Tao Grid Control

            DevExpress.XtraEditors.PopupContainerControl popupContainer = new DevExpress.XtraEditors.PopupContainerControl();
            popupContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            popupContainer.AutoSize = true;
            popupContainer.Controls.Add(gridLookup);
            popupContainer.Location = new System.Drawing.Point(617, 242);
            popupContainer.Name = "popupContainer";
            popupContainer.Size = new System.Drawing.Size(200, 100);
            popupContainer.TabIndex = 1;

            //
            // containEdit          
            //
            DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit containEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit();
            Column.ColumnEdit = containEdit;
            containEdit.AutoHeight = true;
            containEdit.Name = "containEdit";
            containEdit.PopupControl = popupContainer;
            containEdit.PopupSizeable = false;
            containEdit.ShowPopupCloseButton = false;
            containEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            containEdit.QueryResultValue += delegate(object sender, DevExpress.XtraEditors.Controls.QueryResultValueEventArgs e)
            {
                try
                {
                    if (isUpdateLookup == 1)
                    {
                        DataRow rowGridLookup = (gridLookup.MainView as GridView).GetDataRow((gridLookup.MainView as GridView).FocusedRowHandle);
                        DataRow rowGridView = gridView.GetDataRow(gridView.FocusedRowHandle);
                        rowGridView[cotAo] = rowGridLookup[DisplayField];
                        e.Value = rowGridLookup[DisplayField];
                        rowGridView[ColumnField] = rowGridLookup[ValueField];
                    }
                }
                catch { }
            };
            containEdit.Popup += delegate(object sender, EventArgs e)
            {
                if ((DataTable)gridView.GridControl.DataSource != null)
                {
                    DataTable dt = (DataTable)gridLookup.DataSource;
                    if (dt != null)
                    {
                        DataRow dr = gridView.GetDataRow(gridView.FocusedRowHandle);
                        if (dr != null)
                        {
                            string objName = dr[DependColumnField].ToString();
                            dt.DefaultView.RowFilter = "OBJVALUE='" + objName + "'";
                        }
                        else
                        {
                            dt.DefaultView.RowFilter = "OBJVALUE=''";
                        }
                    }
                }
            };

            if (DependColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox)
            {
                DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox imgComboBoxLoai = DependColumn.ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox;
                imgComboBoxLoai.SelectedIndexChanged += delegate(object sender, EventArgs e)
                {
                    gridView.SetFocusedRowCellValue(Column, "");
                    DataTable dt = (DataTable)gridLookup.DataSource;
                    if (dt != null)
                    {
                        dt.DefaultView.RowFilter = "OBJVALUE='" + ((DevExpress.XtraEditors.ImageComboBoxEdit)sender).EditValue.ToString() + "'";
                    }
                };
            }

            return containEdit;
        }

        #endregion

        #region Cot dung User Repository
        public static RepositoryComboboxAuto CotPLComboboxAuto(GridView gridView, GridColumn column, string ColumnField, string ValueField, string DisplayField, string TableName, bool StartWith)
        {
            HelpGridColumn.SetHorzAlignment(column, HorzAlignment.Center);
            column.FieldName = ColumnField + DisplayField;
            RepositoryComboboxAuto comboFind = new RepositoryComboboxAuto(gridView, ColumnField, TableName, ValueField, DisplayField, StartWith);
            column.ColumnEdit = comboFind;
            return comboFind;
        }

        public static RepositoryComboboxAdd CotComboboxAdd(GridView gridView, GridColumn column, string ColumnField, string ValueField, string DisplayField, string TableName)
        {
            column.AppearanceHeader.Options.UseTextOptions = true;
            column.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            column.FieldName = ColumnField + DisplayField;
            RepositoryComboboxAdd comboAdd = new RepositoryComboboxAdd(TableName, ColumnField, ValueField, DisplayField, HelpGen.G_FW_DM_ID, gridView);
            comboAdd._init();
            column.ColumnEdit = comboAdd;
            return comboAdd;
        }

        public static RepositoryItemDanhMucAdv CotDanhMucAdv(GridView gridView, GridColumn column, XtraForm frmDanhMuc, string ColumnField, string tableName, string valueField, string[] visibleField, string[] caption, string getField)
        {
            column.AppearanceHeader.Options.UseTextOptions = true;
            column.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            column.FieldName = ColumnField + getField;
            RepositoryItemDanhMucAdv comboDMAdv = new RepositoryItemDanhMucAdv(frmDanhMuc, ColumnField, tableName, valueField, visibleField, caption, getField, gridView);
            column.ColumnEdit = comboDMAdv;
            return comboDMAdv;
        }
        public static RepositoryItemDataTreeNew CotTreeDanhMucAdv(GridView gridView, GridColumn column, XtraForm danhMucForm, string ColumnField, string TableName, int[] RootID, string valueField, string IDParentField, string[] VisibleFields, string[] Captions, string getField)
        {
            column.AppearanceHeader.Options.UseTextOptions = true;
            column.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            column.FieldName = ColumnField + getField;
            RepositoryItemDataTreeNew comboTreeDMAdv = new RepositoryItemDataTreeNew(danhMucForm, gridView, ColumnField, TableName, RootID, valueField, IDParentField, VisibleFields, Captions, getField);
            column.ColumnEdit = comboTreeDMAdv;
            return comboTreeDMAdv;
        }
        public static RepositoryDMThamTri CotDanhMucThamTri(GridColumn column, string FieldName, string TableName, string Category)
        {
            column.AppearanceHeader.Options.UseTextOptions = true;
            column.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            column.FieldName = FieldName;
            RepositoryDMThamTri DMThamTri = new RepositoryDMThamTri(TableName, Category);
            column.ColumnEdit = DMThamTri;
            return DMThamTri;
        }
        public static RepositoryItemDanhMucExt CotDanhMucGridExt(GridView gridView, GridColumn column, XtraForm frmDanhMuc, string btnCaption, TrialPLDanhMucExtCtrl.GetValue function, string ColumnField, string tableName, string ValueField, string[] visibleField, string[] caption, string getField)
        {
            column.FieldName = ColumnField + getField;
            RepositoryItemDanhMucExt dmExt = new RepositoryItemDanhMucExt(frmDanhMuc, btnCaption, function, ColumnField, tableName, ValueField, visibleField, caption, getField, gridView);
            column.ColumnEdit = dmExt;
            return dmExt;
        }
        public static RepositoryItemDataTreeNewExt CotDanhMucTreeExt(GridView gridView, GridColumn column, XtraForm danhMucForm, string btnCaption, UserControlDataTreeExt.GetValue function, string ColumnField, string TableName, int[] RootID, string ValueField, string IDParentField, string[] VisibleFields, string[] Captions, string getField)
        {
            column.FieldName = ColumnField + getField;
            RepositoryItemDataTreeNewExt dmExt = new RepositoryItemDataTreeNewExt(danhMucForm, gridView, btnCaption, function, ColumnField, TableName, RootID, ValueField, IDParentField, VisibleFields, Captions, getField);
            column.ColumnEdit = dmExt;
            return dmExt;
        }

        public static RepositoryItemSelectDMGridTemplate CotPLDMGrid(GridView gridView, GridColumn column, GroupElementType type, string TableName,
           string IDField, string DislayField, string getField, string[] NameFields,
           string[] Subjects, string FilterField,
           DMBasicGrid.InitGridColumns InitGridCol,
           DMBasicGrid.GetRule Rule, DelegationLib.DefinePermission permission,
           string ColumnField,
           params string[] readOnlyField)
        {
            column.FieldName = ColumnField + getField + "_PLV";
            RepositoryItemSelectDMGridTemplate dmGrid =
                new RepositoryItemSelectDMGridTemplate(type, gridView, TableName, ColumnField, IDField, DislayField, getField,
                    NameFields, Subjects, FilterField, InitGridCol, Rule, permission, readOnlyField);
            column.ColumnEdit = dmGrid;
            gridView.OptionsBehavior.AutoExpandAllGroups = true;
            return dmGrid;
        }

        public static RepositoryItemSelectDMGridTemplate CotPLDMGrid(GridView gridView, GridColumn column, GroupElementType type, string TableName,
           string IDField, string DislayField, string getField, string[] NameFields,
           string[] Subjects, string FilterField,
           DMBasicGrid.InitGridColumns InitGridCol,
           DMBasicGrid.GetRule Rule, DelegationLib.DefinePermission permission,
           string ColumnField,
           DataSet dataTable0,
           params string[] readOnlyField)
        {
            column.FieldName = ColumnField + getField + "_PLV";
            RepositoryItemSelectDMGridTemplate dmGrid =
                new RepositoryItemSelectDMGridTemplate(type, gridView, TableName, ColumnField, IDField, DislayField, getField,
                    NameFields, Subjects, FilterField, InitGridCol, Rule, permission, dataTable0, readOnlyField);
            column.ColumnEdit = dmGrid;
            gridView.OptionsBehavior.AutoExpandAllGroups = true;
            return dmGrid;
        }

        public static RepositoryItemSelectDMTreeGroup CotDMTreeReadOnly(GridView gridView, GridColumn column, string ColumnField, string getField, String GroupTableName, int[] RootID, String IDField, String ParentIDField,
                                    string[] VisibleFields, string[] Captions, string FilterField)
        {
            column.FieldName = ColumnField + getField;
            RepositoryItemSelectDMTreeGroup dmTree = new RepositoryItemSelectDMTreeGroup();
            dmTree.InitReadOnly(gridView, GroupTableName, RootID, IDField, ParentIDField, VisibleFields, Captions, ColumnField, getField, FilterField);
            column.ColumnEdit = dmTree;
            return dmTree;
        }
        public static RepositoryItemSelectDMTreeGroup CotDMTreeReadOnly(TreeList treeList, TreeListColumn column, DataTable dtSource, string ColumnField, string getField, String GroupTableName, int[] RootID, String IDField, String ParentIDField,
                                    string[] VisibleFields, string[] Captions, string FilterField)
        {
            column.FieldName = ColumnField + getField;
            RepositoryItemSelectDMTreeGroup dmTree = new RepositoryItemSelectDMTreeGroup();
            dmTree.InitReadOnly(treeList, GroupTableName, dtSource, RootID, IDField, ParentIDField, VisibleFields, Captions, ColumnField, getField, FilterField);
            column.ColumnEdit = dmTree;
            return dmTree;
        }
        public static RepositoryItemSelectDMTreeGroup CotDMTreeUpdate(GridView gridView, GridColumn column, String GroupTableName, int[] RootID, String IDField, String ParentIDField,
                                string[] VisibleFields, string[] Captions, string GenName,
                                object[] InputColumnType, FieldNameCheck[] Rules, string ColumnField, string getField, string FilterField)
        {
            column.FieldName = ColumnField + getField;
            RepositoryItemSelectDMTreeGroup dmTree = new RepositoryItemSelectDMTreeGroup();
            dmTree.InitUpdate(gridView, GroupTableName, RootID, IDField, ParentIDField, VisibleFields, Captions, GenName, InputColumnType, Rules, ColumnField, getField, FilterField);
            column.ColumnEdit = dmTree;
            return dmTree;
        }
        #endregion

        /// <summary>Cột Hyperlink cho phép mở 1 form.
        /// Chỉ hoạt động đối với lưới có thể cập nhật
        /// </summary>
        public static RepositoryItemHyperLinkEdit CotModalFormLink(GridColumn column, String columnField, String ClassFormName)
        {
            DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repos = 
                new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            repos.Click += delegate(object sender, EventArgs e)
            {
                DataRow dr = column.View.GetDataRow(column.View.FocusedRowHandle);
                Int64 key = HelpNumber.ParseInt64(dr[columnField]);
                // Int64 Key = HelpNumber.ParseInt64(Column.View.EditingValue);
                if (key < 0)
                {
                    return;
                }
                else
                {
                    Object obj = GenerateClass.initObject(ClassFormName, key);
                    // Tag quan trọng
                    repos.Tag = obj;
                }
            };
            column.ColumnEdit = repos;
            column.FieldName = columnField;
            return repos;
        }

        public static void SetFilterMode(GridColumn column, DevExpress.XtraGrid.Columns.FilterPopupMode filterMode)
        {
            column.OptionsFilter.FilterPopupMode = filterMode;
            column.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
        }

        /// <summary>Cột cho phép chọn tập tin, lưu tên tập tin ( có thể lưu lại nội dung tập tin luôn )
        /// </summary>
        /// <param name="dataBinaryField">Nếu muốn lưu tập tin đang chọn vào DB
        /// thì chỉ định tên field có kiểu dữ liệu là nhị phân để có thể lưu vào
        /// DB.
        /// </param>
        /// <param name="filter">Giống như filter trong hàm HelpCommonDialog.showChooseFileByOpenFileDialog
        /// </param>
        /// <param name="allowMaxSize">-1: Không giới hạn kích thước tập tin</param>
        /// <returns></returns>
        public static RepositoryItemButtonEdit CotDBFile(GridColumn column, string columnField, 
            string dataBinaryField, string filter, string title, int allowMaxSize)
        {
            GridView view = column.View as GridView;

            RepositoryItemButtonEdit chonFile = new RepositoryItemButtonEdit();
            HelpGridColumn.CotRepository(column, columnField, chonFile, DevExpress.Utils.HorzAlignment.Default);
            column.AppearanceCell.Font = new System.Drawing.Font(column.AppearanceCell.Font, System.Drawing.FontStyle.Underline);
            chonFile.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            chonFile.ButtonClick += delegate(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
            {
                string file = HelpCommonDialog.showChooseFileByOpenFileDialog(filter, title, allowMaxSize);
                if (file == String.Empty || !File.Exists(file)) 
                    return;

                int handle = view.FocusedRowHandle;
                DataRow row = view.GetFocusedDataRow();
                if (handle < 0 && row == null)
                {
                    view.AddNewRow();
                    row = view.GetFocusedDataRow();
                }
                view.SetRowCellValue(handle, columnField, Path.GetFileName(file));                
                //view.SetRowCellValue(handle, FieldFileName, Path.GetFullPath(file));                
                if (dataBinaryField != String.Empty)
                {
                    row[dataBinaryField] = HelpFile.FileToBytes(file);
                }
            };

            //Cột không thể edit thì ko có sự kiện chosefile.Doubleclick
            view.RowCellClick += delegate(object sender, RowCellClickEventArgs e)
            {
                if (e.Clicks == 2 && e.Button == MouseButtons.Left)
                {
                    DataRow row = view.GetDataRow(e.RowHandle);
                    if (row == null) return;
                    if (dataBinaryField != String.Empty)
                    {
                        HelpFile.OpenFileFromBytes((byte[])view.GetFocusedRowCellValue(dataBinaryField),
                            view.GetFocusedRowCellValue(columnField).ToString());
                    }
                }
            };

            //Cột có thể edit thì ko có sự kiện view.RowCellClick cũng như RowClick
            chonFile.DoubleClick += delegate(object sender, EventArgs e)
            {
                DataRow row = view.GetFocusedDataRow();
                if (row == null) return;
                if (dataBinaryField != String.Empty)
                {
                    HelpFile.OpenFileFromBytes((byte[])view.GetFocusedRowCellValue(dataBinaryField), 
                        view.GetFocusedRowCellValue(columnField).ToString());
                }
            };

            return chonFile;
        }

        
    }
}
