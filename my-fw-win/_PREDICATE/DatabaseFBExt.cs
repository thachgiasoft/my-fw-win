using System.Data;
using ProtocolVN.Framework.Win;
using System;
namespace ProtocolVN.Framework.Core
{
    /// <summary>
    /// Predicate - Using DBScriptExec
    /// </summary>
    [Obsolete("Không sử dụng")]
    public class DatabaseFBExt
    {
        public static bool RunStringSQLScript(string SQLScript)
        {
            return DBScriptExec.RunStringSQLScript(SQLScript);
        }

        public static bool RunFileSQLScript(string SQLFile)
        {
            return DBScriptExec.RunFileSQLScript(SQLFile);
        }

        public static DataSet LoadTable(string TableName, string SortFieldName, bool IgnoreCase)
        {
            return DABase.getDatabase().LoadDataSet(HelpSQL.SelectAll(TableName, SortFieldName, IgnoreCase), TableName);
        }

        public static DataSet LoadTable(string TableName, string Where, string SortFieldName, bool IgnoreCase)
        {
            return DABase.getDatabase().LoadDataSet(HelpSQL.SelectWhere(TableName, Where, SortFieldName, IgnoreCase), TableName);
        }        
    }
}