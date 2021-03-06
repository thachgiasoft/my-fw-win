using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ProtocolVN.Framework.Core;
namespace ProtocolVN.Framework.Win
{
    public class DAXtraReport
    {
        public static DAXtraReport Instance = new DAXtraReport();
        private DAXtraReport() { }

        public BaoBieu load2(string reportKeyID, string username)
        {
            try{
                DatabaseFB db = DABase.getDatabase();
                DbCommand dbSelect = db.GetStoredProcCommand("FW_GET_REPORT");
                db.AddInParameter(dbSelect, "@username", DbType.String, username);
                db.AddInParameter(dbSelect, "@reportkeyid", DbType.String, reportKeyID);
                IDataReader reader = db.ExecuteReader(dbSelect);
                //Không tồn tại report đó trong reportcat
                BaoBieu report = new BaoBieu();
                report.id = 0;
                report.keyid = reportKeyID;
                if (reader.Read())
                {
                    report.id = HelpNumber.ParseInt64(reader["ID"].ToString());
                    report.reportName = reader["NAME"].ToString();
                    if (reader["ISREAD_BIT"].ToString().Equals("Y"))
                        report.isRead = true;
                    else
                        report.isRead = false;
                }
                reader.Close();

                if (report.id > 0 || report.id == -1)
                    return report;
            }
            catch { }
            //Không tồn tại phân quyền trên report này
            //Xem như là Public Report
            return null;
        }

        public List<BaoBieu> getAllReport()
        {
            List<BaoBieu> listReport = new List<BaoBieu>();
            DatabaseFB db = DABase.getDatabase();
            DbCommand dbSelect = db.GetSQLStringCommand("SELECT * FROM REPORT_CAT ORDER BY lower(NAME) ASC WHERE VISIBLE_BIT='Y'");
            IDataReader reader = db.ExecuteReader(dbSelect);
            while (reader.Read())
            {
                BaoBieu report = new BaoBieu();
                report.id = HelpNumber.ParseInt64(reader["ID"].ToString());
                report.keyid = reader["KEYID"].ToString();
                report.reportName = reader["NAME"].ToString();
                listReport.Add(report);
            }
            reader.Close();
            return listReport;
        }

        public bool IsPublicReport(string reportKeyID, string username)
        {
            DatabaseFB db = DABase.getDatabase();
            DbCommand dbSelect = db.GetSQLStringCommand("SELECT KEYID FROM REPORT_CAT WHERE VISIBLE_BIT='Y' AND KEYID = '" + reportKeyID + "'");
            IDataReader reader = db.ExecuteReader(dbSelect);
            bool IsPublic = true;
            while (reader.Read())
            {
                IsPublic = false;
                break;
            }
            reader.Close();
            return IsPublic;
        }

        public List<BaoBieu> getReports(string userName)
        {
            List<BaoBieu> reports = new List<BaoBieu>();
            DatabaseFB db = DABase.getDatabase();
            DbCommand dbSelect = db.GetSQLStringCommand("SELECT KEYID FROM USER_REPORT_REL WHERE VISIBLE_BIT='Y' ORDER BY KEYID ASC");
            IDataReader reader = db.ExecuteReader(dbSelect);
            while (reader.Read())
            {
                reports.Add(load2(reader["KEYID"].ToString(), userName));
            }
            reader.Close();
            return reports;
        }

        public bool UpdateGroupReport(long groupId, List<BaoBieu> reports)
        {
            DatabaseFB db = DABase.getDatabase();
            DbTransaction dbTrans = db.BeginTransaction(db.OpenConnection());

            foreach (BaoBieu report in reports)
            {
                DbCommand dbUpdate = db.GetSQLStringCommand("UPDATE GROUP_REPORT_REL SET " +
                                "ISREAD_BIT=@isread" +
                                " WHERE GROUPID=@groupId AND reportid=@reportId");
                db.AddInParameter(dbUpdate, "@isread", DbType.String, (report.isRead == true ? "Y" : "N"));                
                db.AddInParameter(dbUpdate, "@groupId", DbType.Int64, groupId);
                db.AddInParameter(dbUpdate, "@reportId", DbType.Int64, report.id);

                DbCommand dbInsert = db.GetSQLStringCommand("INSERT INTO GROUP_REPORT_REL VALUES ( " +
                                                "@reportId, @groupId, @isread" +
                                                ") ");
                db.AddInParameter(dbInsert, "@reportId", DbType.Int64, report.id);
                db.AddInParameter(dbInsert, "@groupId", DbType.Int64, groupId);
                db.AddInParameter(dbInsert, "@isread", DbType.String, (report.isRead == true ? "Y" : "N"));                

                try
                {
                    if (db.ExecuteNonQuery(dbUpdate, dbTrans) == 0)
                        db.ExecuteNonQuery(dbInsert, dbTrans);
                }
                catch
                {
                    db.RollbackTransaction(dbTrans);
                    return false;
                }
            }
            db.CommitTransaction(dbTrans);
            return true;
        }

        public bool UpdateUserReport(long userId, List<BaoBieu> reports)  
        {
            DatabaseFB db = DABase.getDatabase();
            DbTransaction dbTrans = db.BeginTransaction(db.OpenConnection());

            foreach (BaoBieu report in reports)
            {
                DbCommand dbUpdate = db.GetSQLStringCommand("UPDATE USER_REPORT_REL SET " +
                                "ISREAD_BIT=@isread" +
                                " WHERE USERID=@userId AND reportid=@reportId");
                db.AddInParameter(dbUpdate, "@isread", DbType.String, (report.isRead == true ? "Y" : "N"));                
                db.AddInParameter(dbUpdate, "@userId", DbType.Int64, userId);
                db.AddInParameter(dbUpdate, "@reportId", DbType.Int64, report.id);

                DbCommand dbInsert = db.GetSQLStringCommand("INSERT INTO USER_REPORT_REL VALUES( " +
                                "@reportId, @userId, @isread" +
                                " )");
                db.AddInParameter(dbInsert, "@reportId", DbType.Int64, report.id);
                db.AddInParameter(dbInsert, "@userId", DbType.Int64, userId);
                db.AddInParameter(dbInsert, "@isread", DbType.String, (report.isRead == true ? "Y" : "N"));                

                try
                {
                    if (db.ExecuteNonQuery(dbUpdate, dbTrans) == 0)
                        db.ExecuteNonQuery(dbInsert, dbTrans);
                }
                catch
                {
                    db.RollbackTransaction(dbTrans);
                    return false;
                }
            }
            db.CommitTransaction(dbTrans);
            return true;
        }

        public DataSet getUserReportAdapter(long userId, string tableName)
        {
            DatabaseFB db = DABase.getDatabase();
            String selectStr = "SELECT f.ID, f.KEYID, f.NAME, u.ISREAD_BIT " +
            "FROM (REPORT_CAT as f LEFT JOIN USER_REPORT_REL as u ON (f.ID = u.reportid and u.userid=@userId)) " +
            "WHERE f.VISIBLE_BIT = 'Y' " +
            "UNION " +
            "SELECT f.ID, f.KEYID, f.NAME, u.ISREAD_BIT " +
            "FROM REPORT_CAT as f INNER JOIN USER_REPORT_REL as u ON (f.ID = u.reportid) " +
            "WHERE u.USERID = @userId and f.VISIBLE_BIT = 'Y'";

            DbCommand dbSelect = db.GetSQLStringCommand(selectStr);
            db.AddInParameter(dbSelect, "@userId", DbType.Int64, userId);
            DataSet ds = new DataSet();
            db.LoadDataSet(dbSelect, ds, tableName);

            return ds;
        }

        public DataSet getGroupReportAdapter(long groupId, string tableName)        
        {
            DatabaseFB db = DABase.getDatabase();
            String selectStr = "SELECT f.ID, f.KEYID, f.NAME, u.ISREAD_BIT " +
            "FROM (REPORT_CAT as f LEFT JOIN GROUP_REPORT_REL as u ON (f.ID = u.reportid and u.groupid=@groupId)) " +
            "WHERE f.VISIBLE_BIT = 'Y' " +
            "UNION " +
            "SELECT f.ID, f.KEYID, f.NAME, u.ISREAD_BIT " +
            "FROM REPORT_CAT as f INNER JOIN GROUP_REPORT_REL as u ON (f.ID = u.reportid) " +
            "WHERE u.GROUPID = @groupId and f.VISIBLE_BIT = 'Y'";

            DbCommand dbSelect = db.GetSQLStringCommand(selectStr);
            db.AddInParameter(dbSelect, "@groupId", DbType.Int64, groupId);
            DataSet ds = new DataSet();
            db.LoadDataSet(dbSelect, ds, tableName);

            return ds;
        }
    }
}