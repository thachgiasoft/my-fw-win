using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public class XtraGridSupportExt
    {
        #region Hỗ trợ tạo cột trong GridView
        public static GridColumn CreateGridColumn(GridView Grid, string Caption, int VisibleIndex)
        {
            return HelpGridColumn.ThemCot(Grid, Caption, VisibleIndex, -1);
        }
        public static GridColumn CreateGridColumn(GridView Grid, string Caption)
        {
            return HelpGridColumn.ThemCot(Grid, Caption, -2, -1);
        }
        public static GridColumn CreateGridColumn(GridView Grid, string Caption, int VisibleIndex, int Width)
        {
            return HelpGridColumn.ThemCot(Grid, Caption, VisibleIndex, Width);
        }
        public static RepositoryItemComboBox InitGridColumnCotVAT(GridColumn Column, string InputField)
        {
            return HelpGridColumn.CotVAT(Column, InputField);
        }
        #endregion
        
        #region Text Column
        public static void TextLeftColumn(GridColumn Column, string FieldName)
        {
            HelpGridColumn.CotTextLeft(Column, FieldName);
        }
        public static void TextRightColumn(GridColumn Column, string FieldName)
        {
            HelpGridColumn.CotTextRight(Column, FieldName);
        }
        public static void TextCenterColumn(GridColumn Column, string FieldName)
        {
            HelpGridColumn.CotTextCenter(Column, FieldName);
        }
        #endregion

        #region Cột cho phép nhập vào số thập phân.
        public static RepositoryItemCalcEdit DecimalGridColumn(GridColumn Column, string FieldName, int SoThapPhan)
        {
            return HelpGridColumn.CotCalcEdit(Column, FieldName, SoThapPhan);
        }
        public static RepositoryItemCalcEdit DecimalGridColumn(GridColumn Column, string FieldName)
        {
            return DecimalGridColumn(Column, FieldName, 2);
        }
        public static RepositoryItemCalcEdit DecimalGridColumn(GridColumn Column)
        {
            return DecimalGridColumn(Column, null);
        }
        public static GridColumn DecimalGridColumn(GridView Grid, string Caption, string FieldName)
        {
            GridColumn Column = CreateGridColumn(Grid, Caption);
            DecimalGridColumn(Column, FieldName);
            return Column;
        }
        #endregion 

        #region Cột cho phép nhập vào số nguyên
        public static RepositoryItemSpinEdit IntegerGridColum(GridColumn Column, string FieldName, int SoThapPhan)
        {
            //return HelpGridColumn.CotSpinEdit(Column, FieldName, SoThapPhan);
            return HelpGridColumn.CotSpinEditInt(Column, FieldName, Decimal.MinValue, Decimal.MaxValue, true);
        }
        public static RepositoryItemSpinEdit IntegerGridColum(GridColumn Column, string FieldName)
        {
            //return HelpGridColumn.CotSpinEdit(Column, FieldName, 0);
            return HelpGridColumn.CotSpinEditInt(Column, FieldName, Decimal.MinValue, Decimal.MaxValue, true);
        }
        public static RepositoryItemSpinEdit IntegerGridColum(GridColumn Column)
        {
            return IntegerGridColum(Column, null);
        }
        public static GridColumn IntegerGridColum(GridView Grid, string Caption, string FieldName)
        {
            GridColumn Column = CreateGridColumn(Grid, Caption);
            IntegerGridColum(Column, FieldName);
            return Column;
        }
        #endregion

        #region Cột cho phép chọn ngày
        public static RepositoryItemDateEdit DateTimeGridColumn(GridColumn column, string fieldName)
        {
            return HelpGridColumn.CotDateEdit(column, fieldName);
        }
        public static RepositoryItemDateEdit DateTimeGridColumn(GridColumn column){
            return DateTimeGridColumn(column, null);
        }
        public static GridColumn DateTimeGridColumn(GridView Grid, string Caption, string FieldName)
        {
            GridColumn Column = CreateGridColumn(Grid, Caption);
            DateTimeGridColumn(Column, FieldName);
            return Column;
        }
        #endregion

        #region Cột cho phép chọn Y/N
        public static void BitGridColumn(GridColumn column, string FieldName)
        {
            HelpGridColumn.CotCheckEdit(column, FieldName);
        }
        public static void BitGridColumn(GridColumn column){
            BitGridColumn(column, null);
        }
        public static GridColumn BitGridColumn(GridView Grid, string Caption, string FieldName)
        {
            GridColumn Column = CreateGridColumn(Grid, Caption);
            BitGridColumn(Column, FieldName);
            return Column;
        }
        #endregion

        public static RepositoryItemMemoExEdit InitGridColumnMemoEdit(GridColumn Column, string InputField)
        {
            return HelpGridColumn.CotMemoExEdit(Column, InputField);
        }

        public static GridColumn CloseButton(GridControl Ctrl, GridView Grid)
        {
            return HelpGridColumn.CotPLDong(Ctrl, Grid);
        }

        public static RepositoryItemImageComboBox CreateDuyetGridColumn(GridColumn col)
        {
            return HelpGridColumn.CotPLDuyet(col, "DUYET");
        }

        #region Chọn dữ liệu từ ComboBox
        //public static RepositoryItemImageComboBox ComboboxGridColumn(GridColumn column, string LookupTable, string ValueField, string DisplayField, string InputField)
        public static RepositoryItemLookUpEdit ComboboxGridColumn(GridColumn column, string LookupTable, string ValueField, string DisplayField, string InputField)
        {
            return HelpGridColumn.CotCombobox(column, LookupTable, ValueField, DisplayField, InputField);
        }
        public static RepositoryItemImageComboBox ComboboxGridColumn(GridColumn column, DataTable data, string ValueField, string DisplayField, string fieldName)
        //public static RepositoryItemLookUpEdit ComboboxGridColumn(GridColumn column, DataTable data, string ValueField, string DisplayField, string fieldName)
        {
            //return HelpGridColumn.CotPLCombobox(column, data.DataSet, ValueField, DisplayField, fieldName);
            RepositoryItemImageComboBox rCBB = new RepositoryItemImageComboBox();
            foreach (DataRow row in data.Rows)
                rCBB.Items.Add(new ImageComboBoxItem("" + row[DisplayField].ToString(), row[ValueField]));

            column.ColumnEdit = rCBB;
            if (fieldName != null) column.FieldName = fieldName;
            return rCBB;
        }

        //public static RepositoryItemImageComboBox ComboboxGridColumn(GridColumn column, string LookupTable, string ValueField, string DisplayField)
        public static RepositoryItemLookUpEdit ComboboxGridColumn(GridColumn column, string LookupTable, string ValueField, string DisplayField)
        {
            return ComboboxGridColumn(column, LookupTable, ValueField, DisplayField, null);
        }
        
        public static RepositoryItemImageComboBox ComboboxGridColumn(GridColumn column, DataTable data, string ValueField, string DisplayField)
        //public static RepositoryItemLookUpEdit ComboboxGridColumn(GridColumn column, DataTable data, string ValueField, string DisplayField)
        {
            return ComboboxGridColumn(column, data, ValueField, DisplayField, null);
        }
        //public static RepositoryItemImageComboBox IDGridColumn(GridColumn column, string ValueField, string DisplayField, string TableName)
        public static RepositoryItemLookUpEdit IDGridColumn(GridColumn column, string ValueField, string DisplayField, string TableName)
        {
            return ComboboxGridColumn(column, TableName, ValueField, DisplayField);
        }
        //public static RepositoryItemImageComboBox IDGridColumn(GridColumn column, string ValueField, string DisplayField, string TableName, string FieldName)
        public static RepositoryItemLookUpEdit IDGridColumn(GridColumn column, string ValueField, string DisplayField, string TableName, string FieldName)
        {
            return ComboboxGridColumn(column, TableName, ValueField, DisplayField, FieldName);
        }
        #endregion

        #region Chọn dữ liệu dạng Lookup
        public static RepositoryItemLookUpEdit LookUpGridColumn(GridColumn column, string ValueField, string DisplayField, string LookupTable, string[] LookupVisibleFields, string[] Captions, string FieldName, int[] widths)
        {
            return HelpGridColumn.CotLookUp(column, ValueField, DisplayField, LookupTable, LookupVisibleFields, Captions, FieldName, widths);
        }
        public static RepositoryItemLookUpEdit LookUpGridColumn(GridColumn column, string ValueField, string DisplayField, string LookupTable, string[] LookupVisibleFields, string[] Captions, string FieldName)
        {
            return LookUpGridColumn(column, ValueField, DisplayField, LookupTable, LookupVisibleFields, Captions, FieldName, null);            
        }
        public static RepositoryItemLookUpEdit LookUpGridColumn(GridColumn column, string ValueField, string DisplayField, string LookupTable, string[] LookupVisibleFields, string[] Captions)
        {
            return LookUpGridColumn(column, ValueField, DisplayField, LookupTable, LookupVisibleFields, Captions, null, null);
        }
        
        #endregion               
    }
}
