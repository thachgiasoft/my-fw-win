using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Blending;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Lớp hỗ trợ khi làm việc với XtraGrid
    /// </summary>
    public class XtraGridSupport
    {
        private GridView gridView;

        public XtraGridSupport(GridView gridView)
        {
            this.gridView = gridView;
        }

        #region Các xử lý liên quan đến việc tạo combobox tự động và chuyển đổi thành checkbox
        private List<string> listOfBitField = new List<string>();            //Danh sách các field có kiểu là BIT -> Checkbox
        private List<string> listOfImageComboBoxField = new List<string>();  //Danh sách các field có Tag la CBB -> Image Combobox        
        /// <summary>Hàm chuyển đổi các column có giá trị Tag = "CHECK" thành Checkbox trên lưới.
        /// Không giới hạn trên các Column có field kết thúc "_BIT" hay _bit
        /// </summary>
        public static void CheckTagToCheck(GridView gridView)
        {
            List<string> listOfBitField = new List<string>();
            for (int i = 0; i < gridView.Columns.Count; i++)
            {
                if (gridView.Columns[i].Tag != null && gridView.Columns[i].Tag.ToString().Equals("CHECK"))
                {
                    listOfBitField.Add(gridView.Columns[i].FieldName);
                }
            }
            bitToCheck(gridView, listOfBitField);
        }

        /// <summary>Hàm chuyển đổi các GridColumn có Tag Property là "CBB?TableName?ValueField?DisplayField"
        /// thành ImageComboBox
        /// </summary>
        public static void CBBTagToImageComboBox(GridView gridView)
        {
            List<string> listOfImageComboBoxField = new List<string>();
            for (int i = 0; i < gridView.Columns.Count; i++)
            {
                if (gridView.Columns[i].Tag != null && gridView.Columns[i].Tag.ToString().StartsWith("CBB?"))
                {
                    listOfImageComboBoxField.Add(gridView.Columns[i].FieldName);
                }
            }
            cbbToImageComboBox(gridView, listOfImageComboBoxField);
        }

        /// <summary>Hàm này cho phép chuyển Check Tag -> Checkbox
        /// CBB Tag -> Checkbox
        /// </summary>
        /// <param name="gridView"></param>
        public static void CheckCBBTagToCheckImageCombo(GridView gridView)
        {
            CheckTagToCheck(gridView);
            CBBTagToImageComboBox(gridView);
        }

        /// <summary>Hàm chuyển đổi các Field kết thúc bằng _BIT hoặc _bit thành Checkbox
        /// </summary>
        /// <param name="gridView"></param>
        public static void BitFieldToCheck(GridView gridView)
        {
            XtraGridSupport tool = new XtraGridSupport(gridView);
            string fieldName;
            for (int i = 0; i < gridView.Columns.Count; i++)
            {
                fieldName = gridView.Columns[i].FieldName;
                if (fieldName.EndsWith("_BIT") || fieldName.EndsWith("_bit"))
                {
                    tool.listOfBitField.Add(fieldName);
                }
                else if (fieldName.EndsWith("_wZzW"))
                {
                    tool.listOfBitField.Remove(fieldName.Substring(0, fieldName.Length - 5));
                }
            }

            tool.applyConvertYNToCheckbox();
        }

        /// <summary>Hàm chuyển các field có phần kết thúc là _BIT hoặc _bit
        /// và Tag CBB?... chuyển thành Image Combobox
        /// </summary>
        public static void BitCBBToCheckImageCombo(GridView gridView)
        {
            XtraGridSupport tool = new XtraGridSupport(gridView);
            string fieldName;
            for (int i = 0; i < gridView.Columns.Count; i++)
            {
                fieldName = gridView.Columns[i].FieldName;
                if (fieldName.EndsWith("_BIT") || fieldName.EndsWith("_bit"))
                {
                    tool.listOfBitField.Add(fieldName);
                }
                else if (gridView.Columns[i].Tag != null && gridView.Columns[i].Tag.ToString().StartsWith("CBB?"))
                {
                    tool.listOfImageComboBoxField.Add(fieldName);
                }
                else if (fieldName.EndsWith("_wZzW"))
                {
                    tool.listOfBitField.Remove(fieldName.Substring(0, fieldName.Length - 5));
                }
            }

            tool.applyImageCombobox();
            tool.applyConvertYNToCheckbox();
        }

        #region Chuyển từ Comlumn có Tag có nôi dung là CBB?TableName?ValueField?DisplayField thành ImageComboBox
        //TODO PHUOC : Phước Hiện tại đang dùng ImageCombobox 
        //Nên chuyển thành LookupEdit
        //Giá trị của Tag trên column muốn dùng cơ chế chuyển tự động qua combobox
        //CBB?TableName?ValueField?DisplayField
        private void applyImageCombobox()
        {
            try
            {
                string tag = null;
                string[] s = null;
                GridColumn currentColumn = null;
                for (int i = 0; i < gridView.Columns.Count; i++)
                {
                    currentColumn = gridView.Columns[i];
                    if (listOfImageComboBoxField.Contains(currentColumn.FieldName))
                    {
                        tag = currentColumn.Tag.ToString();
                        s = tag.Split(new char[] { '?' });

                        RepositoryItemImageComboBox rCBB = BuildRCBB(s[1], s[2], s[3]);//tableName = s[1]; valueField = s[2]; displayField = s[3];
                        this.gridView.GridControl.RepositoryItems.Add(rCBB);
                        currentColumn.ColumnEdit = rCBB;
                    }
                }
            }
            catch
            {
                throw new Exception("Programmer Error : Giá trị Tag trong Comlumn không hợp lê.");
            }
        }
        private RepositoryItemImageComboBox BuildRCBB(string tableName, string valueField, string displayField)
        {
            DatabaseFB db = DABase.getDatabase();
            DataSet ds = new DataSet();
            ds = db.LoadTable(tableName);

            RepositoryItemImageComboBox rCBB = new RepositoryItemImageComboBox();
            rCBB.Name = "repositoryItemImageComboBoxCode" + tableName;

            foreach (DataRow row in ds.Tables[0].Rows)
                rCBB.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(row[displayField].ToString(), (object)row[valueField]));

            return rCBB;
        }
        #endregion

        #region Y/N -> Check box trong lưới
        private static void bitToCheck(GridView gridView, List<string> listOfBitField)
        {
            XtraGridSupport tool = new XtraGridSupport(gridView);
            tool.listOfBitField = listOfBitField;
            tool.applyConvertYNToCheckbox();
        }
        private static void cbbToImageComboBox(GridView gridView, List<string> listOfImageComboBoxField)
        {
            XtraGridSupport tool = new XtraGridSupport(gridView);
            tool.listOfImageComboBoxField = listOfImageComboBoxField;
            tool.applyImageCombobox();
        }
        //TODO PHUOC : Phước
        //Xem lại cách xử lý này với cách đang làm tốn nhiều tài nguyên
        private void initBitToCheck()
        {
            if (TagPropertyMan.Get(this.gridView.Tag, "IS_YN_CHECK").ToString() != "2")
            {
                for (int i = 0; i < gridView.Columns.Count; i++)
                {
                    GridColumn currentColumn = gridView.Columns[i];
                    if (listOfBitField.Contains(currentColumn.FieldName))
                    {
                        //PHUOCNC CHECK
                        if (currentColumn.Visible == true)
                        {
                            GridColumn unbColumn = gridView.Columns.AddField(currentColumn.FieldName + "_wZzW");
                            unbColumn.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
                            unbColumn.Caption = currentColumn.Caption;
                            unbColumn.VisibleIndex = currentColumn.VisibleIndex;
                            unbColumn.AppearanceHeader.Options.UseTextOptions = true;
                            unbColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            unbColumn.AppearanceCell.Options.UseTextOptions = true;
                            unbColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            currentColumn.Visible = false;
                            currentColumn.OptionsColumn.AllowEdit = false;
                        }
                    }
                }
                object obj = this.gridView.Tag;
                TagPropertyMan.InsertOrUpdate(ref obj, "IS_YN_CHECK", 2);
                this.gridView.Tag = obj;
            }
        }

        private void applyConvertYNToCheckbox()
        {
            if (TagPropertyMan.Get(this.gridView.Tag, "IS_YN_CHECK") == null)
            {
                this.gridView.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.gridView_CustomUnboundColumnData);
                this.gridView.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView_CellValueChanging);
                object obj = this.gridView.Tag;
                TagPropertyMan.InsertOrUpdate(ref obj, "IS_YN_CHECK", 1);
                this.gridView.Tag = obj;
            }
            initBitToCheck();
        }

        private void gridView_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            string fieldName = e.Column.FieldName;
            if (fieldName.EndsWith("_wZzW"))
            {
                string realFieldName = fieldName.Substring(0, fieldName.Length - 5);
                DataRow row = gridView.GetDataRow(e.RowHandle);
                if (e.Value.ToString().Equals(Boolean.TrueString))
                    row[realFieldName] = "Y";
                else
                    row[realFieldName] = "N";
            }
        }

        private void gridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            string unBoundField;
            foreach (string fieldName in this.listOfBitField)
            {
                unBoundField = fieldName + "_wZzW";
                if (e.Column.FieldName == unBoundField && e.IsGetData)
                {
                    e.Value = getBooleanValue(fieldName, sender as ColumnView, e.ListSourceRowIndex);
                    break;
                }
            }
        }

        private bool getBooleanValue(string fieldName, ColumnView view, int listSourceRowIndex)
        {
            DataRow row = gridView.GetDataRow(listSourceRowIndex);
            if (row != null)
            {
                object value = row[fieldName];
                if (value != null && value.ToString().ToUpper().StartsWith("Y"))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #endregion
    }
}