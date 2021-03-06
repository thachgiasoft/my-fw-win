using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraTreeList;
using System.Data;
using ProtocolVN.Framework.Core;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;

using System.Windows.Forms;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// PHUOCNC 
    /// - Chưa việt hóa được menu trong TreeList
    /// - Chưa hoàn chỉnh việc nhập nhiều dữ liệu cho từng cột dạng CalcEdit, ..
    /// - Validation
    /// - Định dạng
    /// Lớp cho phép tạo một TreeList
    /// </summary>
    [Obsolete("Sử dụng PLTreeList thay thế")]
    public class PLDataTree : PLTreeList
    {
        private string TableName;
        private int[] RootID;
        private string order = "";
        private bool IsVisibleBit = false;

        public PLDataTree() : base()
        {
            //PHUOCNC : Chưa việt hóa cho TreeGrid
            //this.OptionsMenu.EnableColumnMenu = true;
            //this.OptionsMenu.EnableFooterMenu = true;
        }
        private void initTree(string IDField, string IDParentField, string[] VisibleFields, string[] Captions, object[] InputColumnType)
        {
            this.Nodes.Clear();
            //Cột ID
            TreeListColumn colID = this.Columns.Add();
            colID.Caption = IDField;
            colID.FieldName = IDField;
            colID.Name = IDField;
            colID.VisibleIndex = -1;
            colID.OptionsColumn.AllowFocus = false;
            //Cây ko thể  sắp xếp nhiều cấp được
            this.order = VisibleFields[0].ToString();
            //this.order = IDField.ToString();

            //Khởi tạo các cột Visible
            for (int i = 0; i < VisibleFields.Length; i++)
            {
                TreeListColumn col = this.Columns.Add();
                col.AppearanceHeader.Options.UseTextOptions = true;
                col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                col.Caption = Captions[i];
                col.FieldName = VisibleFields[i];
                col.Name = i.ToString();
                col.VisibleIndex = i;
                if (InputColumnType != null) 
                    if(InputColumnType[i]!=null)
                        TreeListSupport.SetColumnType(col, InputColumnType[i]);
            }
            //Thuộc tính của TreeList
            this.KeyFieldName = IDField;
            this.ParentFieldName = IDParentField;
            this.OptionsBehavior.Editable = false;
        }
        public void _BuildTree(string TableName, int[] RootID, string IDField, string IDParentField, string[] VisibleFields, string[] Captions, object[] InputColumnType)
        {
            _BuildTree(TableName, RootID, IDField, IDParentField, VisibleFields, Captions, InputColumnType, false);
        }

        public void _BuildTree(string TableName, int[] RootID, string IDField, string IDParentField, string[] VisibleFields, string[] Captions, object[] InputColumnType,bool IsVisibleBit)
        {
            this.IsVisibleBit = IsVisibleBit;
            this.initTree(IDField, IDParentField, VisibleFields, Captions, InputColumnType);
            this.TableName = TableName;
            this.RootID = RootID;
            DataTable dt = this.LoadTable(TableName, RootID);
            this.DataSource = dt;
        }

        public void PLRefresh()
        {
            DataTable dt = this.LoadTable(TableName, RootID);
            this.DataSource = dt;
        }

        private DataTable LoadTable(string TableName, int[] RootID)
        {
            QueryBuilder query = null;
            if(IsVisibleBit == true)
                query = new QueryBuilder("SELECT * FROM " + TableName + " WHERE VISIBLE_BIT = 'Y' AND 1=1");
            else
                query = new QueryBuilder("SELECT * FROM " + TableName + " WHERE 1=1");
               
            if(RootID!=null)
            {
                string ids = "";
                for (int i = 0; i < RootID.Length; i++){
                    ids += RootID[i].ToString() + ",";
                    //query.addID(PLFN.ID_ROOT, RootID[i]);
                    //query.addID(this.ParentFieldName, RootID[i]);
                }
                query.addCondition(GlobalConst.ID_ROOT + " in (" + ids + "0)");
            }
            else
            {
                //query.addCondition(this.ParentFieldName + " is null ");
                //query.addID(this.ParentFieldName, -1);
            }
            //Cây không thể sắp xếp nhiều cấp được
            query.setAscOrderBy("ID_ROOT," + this.ParentFieldName + "," + this.order);
            DataSet ds = DABase.getDatabase().LoadDataSet(query, TableName);
            return ds.Tables[0];
        }

        #region Hàm cũ ít dùng
        public void _BuildTree(string TableName, string IDField, string IDParentField, string[] VisibleFields, string[] Captions, object[] InputColumnType)
        {
            _BuildTree(TableName, null, IDField, IDParentField, VisibleFields, Captions, InputColumnType);
        }

        public void _BuildTree(string TableName, string IDField, string IDParentField, string[] VisibleFields, string[] Captions)
        {
            _BuildTree(TableName, null, IDField, IDParentField, VisibleFields, Captions, null);
        }
        #endregion
    }    
}
