using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ProtocolVN.Framework.Core;
using System.Data.Common;
namespace ProtocolVN.Framework.Core
{
    public class HelpDanhMucDB
    {
        public static bool DeleteDM(string TableName, long ID)
        {
            return DatabaseFB.DeleteRecord(TableName, "ID", ID);
        }

        #region Danh mục ID, NAME, VISIBLE_BIT, Gen là tương ứng với G_TABLE_NAME
        public static DataSet LoadDM1Cot(string TableName, string where)
        {
            return DABase.getDatabase().LoadTable(TableName,
                    new string[]{
                        "ID",
                        "NAME",
                        "VISIBLE_BIT"
                    },
                    where
          );          
        }
        public static DataSet LoadDM1Cot(string TableName)
        {
            return DABase.getDatabase().LoadTable(TableName, 
                new string[]{
                    "ID",
                    "NAME",
                    "VISIBLE_BIT"
                }
            );
        }

        public static long InsertDM1Cot(string TableName, string Name, bool Visible_Bit)
        {
            return InsertItem(TableName, HelpGen.G_FW_DM_ID, Name, "ID", "NAME", Visible_Bit);
        }

        public static bool UpdateDM1Cot(string TableName, long ID, string Name, bool Visible_Bit){
            return HelpDanhMucDB.UpdateItem(TableName, ID, Name, "ID", "NAME", Visible_Bit);
        }

        #endregion

        #region Danh mục nhiều cột ID, ... , VISIBLE_BIT
        private static bool IsColAo(DataColumnCollection colsName, string ColName)
        {
            for (int i = 0; i < colsName.Count; i++)
            {
                if (colsName[i].ColumnName.StartsWith(ColName) &&
                    colsName[i].ColumnName.EndsWith("_PLV"))
                    return true;
            }
            return false;
        }

        public static bool InsertDMNCot(string TableName, DataRow row)
        {
            DataSet ds = DatabaseFB.LoadDataSet(TableName, "ID", -2);
            long id = DABase.getDatabase().GetID(HelpGen.G_FW_DM_ID);
            row["ID"] = id;
            if(row.Table.Columns.Contains("VISIBLE_BIT"))
                if (row["VISIBLE_BIT"] == DBNull.Value || row["VISIBLE_BIT"].ToString() == "")
                    row["VISIBLE_BIT"] = 'Y';
            DataRow newRow = ds.Tables[0].NewRow();
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                string ColName = ds.Tables[0].Columns[i].ColumnName;
                if (row[ColName] != null && row[ColName].ToString().Equals("-1"))
                {
                    if (IsColAo(row.Table.Columns, ColName))
                    {
                        newRow[ColName] = DBNull.Value;
                    }
                }
                else
                {
                    newRow[ColName] = row[ColName];
                }
            }

            //newRow.ItemArray = row.ItemArray;

            ds.Tables[0].Rows.Add(newRow);
            if (DABase.getDatabase().UpdateTable(ds) == 1){
                return true;
            }
            else{
                return false;
            }
        }
        public static bool UpdateDMNCot(string TableName, DataRow row)
        {
            DataSet ds = DatabaseFB.LoadDataSet(TableName, "ID",
                HelpNumber.ParseInt64(row["ID"].ToString()));

            //Giải pháp này gặp vấn đề với các cột giả
            //object[] oldItemArray = ds.Tables[0].Rows[0].ItemArray;
            //ds.Tables[0].Rows[0].ItemArray = row.ItemArray;
            //if (DABase.getDatabase().UpdateTable(ds) == 1) {
            //    return true;
            //}
            //else {
            //    row.ItemArray = oldItemArray;
            //    return false;
            //}

            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                string ColName = ds.Tables[0].Columns[i].ColumnName;
                if (row[ColName] != null && row[ColName].ToString().Equals("-1"))
                {
                    if (IsColAo(row.Table.Columns, ColName))
                    {
                        ds.Tables[0].Rows[0][ColName] = DBNull.Value;
                    }
                }
                else
                {
                    ds.Tables[0].Rows[0][ColName] = row[ColName];
                }
            }


            if (DABase.getDatabase().UpdateTable(ds) == 1)
            {
                return true;
            }
            else
            {
                ds = DatabaseFB.LoadDataSet(TableName, "ID",
                    HelpNumber.ParseInt64(row["ID"].ToString()));
                row = ds.Tables[0].Rows[0];
                return false;
            }
        }
        #endregion

        public static DataTable LoadData(string tableName, string idField, string displayField)
        {
            DatabaseFB db = DABase.getDatabase();
            DbCommand command = db.GetSQLStringCommand("SELECT " + idField + ", " + displayField + " FROM " + tableName);
            DataSet ds = db.LoadDataSet(command, "TEMPTABLE");
            return ds.Tables[0];
        }

        public static long InsertItem(string TableName, string GenName, string NameValue, string IDField, string NameField, bool VisibleValue )
        {
            DatabaseFB db = DABase.getDatabase();
            DbCommand dbInsert = db.GetSQLStringCommand("INSERT INTO " + TableName + "(" + IDField + "," + NameField + ",VISIBLE_BIT) VALUES (" +
                                                    "@ID, @NAME, @VISIBLE_BIT)");
            long genID = DABase.getDatabase().GetID(GenName);
            db.AddInParameter(dbInsert, "@ID", DbType.Int64, genID);
            db.AddInParameter(dbInsert, "@NAME", DbType.String, NameValue);
            db.AddInParameter(dbInsert, "@VISIBLE_BIT", DbType.String, (VisibleValue==true?"Y":"N"));
            try
            {
                int num = db.ExecuteNonQuery(dbInsert);
                if (num == 0) return -1;
            }
            catch { return -1; }
            return genID;
        }

        public static long InsertItem(string TableName, string GenName, string NameValue, string IDField, string NameField)
        {
            return InsertItem(TableName, GenName, NameValue, IDField, NameField, true);
        }

        public static bool UpdateItem(string TableName, long IDValue, string NameValue, string IDField, string NameField, bool Visible)
        {
            DatabaseFB db = DABase.getDatabase();
            DbCommand dbUpdate = db.GetSQLStringCommand("UPDATE " + TableName
                        + " SET " + NameField + "=@NAME, VISIBLE_BIT=@VISIBLE WHERE ID=@ID");
            db.AddInParameter(dbUpdate, "@NAME", DbType.String, NameValue);
            db.AddInParameter(dbUpdate, "@VISIBLE", DbType.String, (Visible==true ? "Y": "N"));
            db.AddInParameter(dbUpdate, "@ID", DbType.Int64, IDValue);
            try
            {
                int num = db.ExecuteNonQuery(dbUpdate);
                if (num == 0) return false;
            }
            catch { return false; }
            return true;
        }

        public static bool UpdateItem(string TableName, long IDValue, string NameValue, string IDField, string NameField)
        {
            DatabaseFB db = DABase.getDatabase();
            DbCommand dbUpdate = db.GetSQLStringCommand("UPDATE " + TableName 
                        + " SET "+NameField+"=@NAME, VISIBLE_BIT=@VISIBLE WHERE ID=@ID");
            db.AddInParameter(dbUpdate, "@NAME", DbType.String, NameValue);
            db.AddInParameter(dbUpdate, "@VISIBLE", DbType.String, "Y");
            db.AddInParameter(dbUpdate, "@ID", DbType.Int64, IDValue);
            try
            {
                int num = db.ExecuteNonQuery(dbUpdate);
                if (num == 0) return false;
            }
            catch { return false; }
            return true;
        }

        public static bool DeleteItem(string TableName ,string IDField, long IDValue)
        {
            return DatabaseFB.DeleteRecord(TableName, IDField, IDValue);
        }
    }
}
