using System;
using System.Collections.Generic;
using System.Data;
using ProtocolVN.Framework.Core;
using System.Data.Common;

namespace ProtocolVN.Framework.Win
{
    public class Feature
    {
        public long id;
        public string featureName;
        public string description;

        public bool isRead;
        public bool isInsert;
        public bool isUpdate;
        public bool isDelete;
        public bool isPrint;
        public bool isExport;

        public Feature() {
            id = 0;
            isRead = false;
            isInsert = false;
            isUpdate = false;
            isDelete = false;
            isPrint = false;
            isExport = false;
        }

        public static Feature loadFeature(long featureID, string featureName, string username)
        {
            return DAFeature.Instance.load(featureID, featureName, username);
        }

        public static bool updateGroup(long groupId, List<Feature> features)
        {
            return DAFeature.Instance.UpdateGroupFeature(groupId, features);
        }

        public static bool updateUser(long userId, List<Feature> features)
        {
            return DAFeature.Instance.UpdateUserFeature(userId, features);
        }

        public static DataSet getFeatureUserAdapter(long userId, string tableName)
        {
            return DAFeature.Instance.getUserFeatureAdapter(userId, tableName);
        }

        public static DataSet getFeatureGroupAdapter(long groupId, string tableName)
        {
            return DAFeature.Instance.getGroupFeatureAdapter(groupId, tableName);
        }

        public static List<Feature> loadAllFeatureByUser(string username)
        {
            return DAFeature.Instance.getFeatures(username);
        }

        public static List<Feature> loadAllFeature()
        {
            return DAFeature.Instance.getAllFeature();
        }

        //PHUOCNC Nạp dang sách các Feature của 1 người dùng
        public static List<Feature> loadFeatures(String username)
        {            
            return null;
        }


    }

    class DAFeature
    {
        public static readonly DAFeature Instance = new DAFeature();
        private DAFeature() { }

        public Feature load(long featureID, string featureName, string username)
        {
            DatabaseFB db = DABase.getDatabase();
            DbCommand dbSelect = db.GetStoredProcCommand("FW_GET_FEATURE");
            db.AddInParameter(dbSelect, "@USERNAME", DbType.String, username);
            db.AddInParameter(dbSelect, "@FEATURENAME", DbType.String, featureName);
            IDataReader reader = db.ExecuteReader(dbSelect);
            Feature feature = new Feature();
            feature.featureName = featureName;
            feature.id = featureID;
            while (reader.Read())
            {
                feature.id = HelpNumber.ParseInt64(reader["ID"].ToString());
                if (reader["ISREAD_BIT"].ToString().Equals("Y"))
                    feature.isRead = true;
                if (reader["ISINSERT_BIT"].ToString().Equals("Y"))
                    feature.isInsert = true;
                if (reader["ISDELETE_BIT"].ToString().Equals("Y"))
                    feature.isDelete = true;
                if (reader["ISUPDATE_BIT"].ToString().Equals("Y"))
                    feature.isUpdate = true;
                if (reader["ISPRINT_BIT"].ToString().Equals("Y"))
                    feature.isPrint = true;
                if (reader["ISEXPORT_BIT"].ToString().Equals("Y"))
                    feature.isExport = true;

            }
            reader.Close();

            return feature;
        }

        public List<Feature> getAllFeature()
        //PHUOC OK
        {
            List<Feature> listFeature = new List<Feature>();
            DatabaseFB db = DABase.getDatabase();
            DbCommand dbSelect = db.GetSQLStringCommand("SELECT * FROM FEATURE_CAT ORDER BY lower(NAME) ASC WHERE VISIBLE_BIT='Y'");
            IDataReader reader = db.ExecuteReader(dbSelect);
            while (reader.Read())
            {
                Feature feature = new Feature();
                feature.id = HelpNumber.ParseInt64(reader["ID"].ToString());
                feature.featureName = reader["NAME"].ToString();
                listFeature.Add(feature);
            }
            reader.Close();
            return listFeature;
        }

        public List<Feature> getFeatures(string userName)
        //PHUOC OK
        {
            List<Feature> features = new List<Feature>();
            DatabaseFB db = DABase.getDatabase();
            DbCommand dbSelect = db.GetSQLStringCommand("SELECT ID, NAME FROM FEATURE_CAT");
            IDataReader reader = db.ExecuteReader(dbSelect);
            while (reader.Read())
            {
                features.Add(load(HelpNumber.ParseInt64(reader["ID"].ToString()), reader["NAME"].ToString(), userName));
            }
            reader.Close();
            return features;
        }

        public bool UpdateGroupFeature(long groupId, List<Feature> features)
        //PHUOC OK
        {
            DatabaseFB db = DABase.getDatabase();
            DbTransaction dbTrans = db.BeginTransaction(db.OpenConnection());

            foreach (Feature feature in features)
            {
                DbCommand dbUpdate = db.GetSQLStringCommand("UPDATE GROUP_FEATURE_REL SET " +
                                "ISREAD_BIT=@isread,ISINSERT_BIT=@isinsert,ISUPDATE_BIT=@isupdate," +
                                "ISDELETE_BIT=@isdelete, ISPRINT_BIT=@isprint, ISEXPORT_BIT=@isexport WHERE GROUPID=@groupId AND featureid=@featureId");
                db.AddInParameter(dbUpdate, "@isread", DbType.String, (feature.isRead == true ? "Y" : "N"));
                db.AddInParameter(dbUpdate, "@isinsert", DbType.String, (feature.isInsert == true ? "Y" : "N"));
                db.AddInParameter(dbUpdate, "@isupdate", DbType.String, (feature.isUpdate == true ? "Y" : "N"));
                db.AddInParameter(dbUpdate, "@isdelete", DbType.String, (feature.isDelete == true ? "Y" : "N"));
                db.AddInParameter(dbUpdate, "@isprint", DbType.String, (feature.isPrint == true ? "Y" : "N"));
                db.AddInParameter(dbUpdate, "@isexport", DbType.String, (feature.isExport == true ? "Y" : "N"));
                db.AddInParameter(dbUpdate, "@groupId", DbType.Int64, groupId);
                db.AddInParameter(dbUpdate, "@featureId", DbType.Int64, feature.id);

                DbCommand dbInsert = db.GetSQLStringCommand("INSERT INTO GROUP_FEATURE_REL VALUES ( " +
                                                "@featureId, @groupId, @isread, @isinsert, @isupdate," +
                                                "@isdelete, @isprint, @isexport ) ");
                db.AddInParameter(dbInsert, "@featureId", DbType.Int64, feature.id);
                db.AddInParameter(dbInsert, "@groupId", DbType.Int64, groupId);
                db.AddInParameter(dbInsert, "@isread", DbType.String, (feature.isRead == true ? "Y" : "N"));
                db.AddInParameter(dbInsert, "@isinsert", DbType.String, (feature.isInsert == true ? "Y" : "N"));
                db.AddInParameter(dbInsert, "@isupdate", DbType.String, (feature.isUpdate == true ? "Y" : "N"));
                db.AddInParameter(dbInsert, "@isdelete", DbType.String, (feature.isDelete == true ? "Y" : "N"));
                db.AddInParameter(dbInsert, "@isprint", DbType.String, (feature.isPrint == true ? "Y" : "N"));
                db.AddInParameter(dbInsert, "@isexport", DbType.String, (feature.isExport == true ? "Y" : "N"));
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

        public bool UpdateUserFeature(long userId, List<Feature> features)
        //PHUOC OK
        {
            DatabaseFB db = DABase.getDatabase();
            DbTransaction dbTrans = db.BeginTransaction(db.OpenConnection());

            foreach (Feature feature in features)
            {
                DbCommand dbUpdate = db.GetSQLStringCommand("UPDATE USER_FEATURE_REL SET " +
                                "ISREAD_BIT=@isread,ISINSERT_BIT=@isinsert,ISUPDATE_BIT=@isupdate," +
                                "ISDELETE_BIT=@isdelete, ISPRINT_BIT=@isprint, ISEXPORT_BIT=@isexport WHERE USERID=@userId AND featureid=@featureId");
                db.AddInParameter(dbUpdate, "@isread", DbType.String, (feature.isRead == true ? "Y" : "N"));
                db.AddInParameter(dbUpdate, "@isinsert", DbType.String, (feature.isInsert == true ? "Y" : "N"));
                db.AddInParameter(dbUpdate, "@isupdate", DbType.String, (feature.isUpdate == true ? "Y" : "N"));
                db.AddInParameter(dbUpdate, "@isdelete", DbType.String, (feature.isDelete == true ? "Y" : "N"));
                db.AddInParameter(dbUpdate, "@isprint", DbType.String, (feature.isPrint == true ? "Y" : "N"));
                db.AddInParameter(dbUpdate, "@isexport", DbType.String, (feature.isExport == true ? "Y" : "N"));
                db.AddInParameter(dbUpdate, "@userId", DbType.Int64, userId);
                db.AddInParameter(dbUpdate, "@featureId", DbType.Int64, feature.id);

                DbCommand dbInsert = db.GetSQLStringCommand("INSERT INTO USER_FEATURE_REL VALUES( " +
                                "@featureId, @userId, @isread, @isinsert, @isupdate," +
                                "@isdelete,@isprint,@isexport )");
                db.AddInParameter(dbInsert, "@featureId", DbType.Int64, feature.id);
                db.AddInParameter(dbInsert, "@userId", DbType.Int64, userId);
                db.AddInParameter(dbInsert, "@isread", DbType.String, (feature.isRead == true ? "Y" : "N"));
                db.AddInParameter(dbInsert, "@isinsert", DbType.String, (feature.isInsert == true ? "Y" : "N"));
                db.AddInParameter(dbInsert, "@isupdate", DbType.String, (feature.isUpdate == true ? "Y" : "N"));
                db.AddInParameter(dbInsert, "@isdelete", DbType.String, (feature.isDelete == true ? "Y" : "N"));
                db.AddInParameter(dbInsert, "@isprint", DbType.String, (feature.isPrint == true ? "Y" : "N"));
                db.AddInParameter(dbInsert, "@isexport", DbType.String, (feature.isExport == true ? "Y" : "N"));
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


        public DataSet getUserFeatureAdapter(long userId, string tableName)
        //PHUOC OK
        {
            DatabaseFB db = DABase.getDatabase();
            String selectStr = "SELECT f.ID, f.NAME, f.DESCRIPTION, u.ISREAD_BIT,u.ISINSERT_BIT,u.ISUPDATE_BIT,u.ISDELETE_BIT " +
            ", f.ISREAD, f.ISINSERT, f.ISUPDATE, f.ISDELETE,f.ISPRINT, F.ISEXPORT " +
            "FROM (FEATURE_CAT as f LEFT JOIN USER_FEATURE_REL as u ON (f.ID = u.featureid and u.userid=@userId)) " +
            "WHERE f.VISIBLE_BIT = 'Y' " +
            "UNION " +
            "SELECT f.ID, f.NAME, f.DESCRIPTION , u.ISREAD_BIT,u.ISINSERT_BIT,u.ISUPDATE_BIT,u.ISDELETE_BIT " +
            ", f.ISREAD, f.ISINSERT, f.ISUPDATE, f.ISDELETE, F.ISPRINT, F.ISEXPORT " +
            "FROM FEATURE_CAT as f INNER JOIN USER_FEATURE_REL as u ON (f.ID = u.featureid) " +
            "WHERE u.USERID = @userId and f.VISIBLE_BIT = 'Y'";

            DbCommand dbSelect = db.GetSQLStringCommand(selectStr);
            db.AddInParameter(dbSelect, "@userId", DbType.Int64, userId);
            DataSet ds = new DataSet();
            db.LoadDataSet(dbSelect, ds, tableName);

            return ds;
        }

        public DataSet getGroupFeatureAdapter(long groupId, string tableName)
        //PHUOC
        {
            DatabaseFB db = DABase.getDatabase();
            String selectStr = "SELECT f.ID, f.NAME, f.DESCRIPTION, u.ISREAD_BIT,u.ISINSERT_BIT,u.ISUPDATE_BIT,u.ISDELETE_BIT " +
            ", f.ISREAD, f.ISINSERT, f.ISUPDATE, f.ISDELETE,f.ISPRINT, f.ISEXPORT  " +
            "FROM (FEATURE_CAT as f LEFT JOIN GROUP_FEATURE_REL as u ON (f.ID = u.featureid and u.groupid=@groupId)) " +
            "WHERE f.VISIBLE_BIT = 'Y' " +
            "UNION " +
            "SELECT f.ID, f.NAME, f.DESCRIPTION, u.ISREAD_BIT,u.ISINSERT_BIT,u.ISUPDATE_BIT,u.ISDELETE_BIT " +
            ", f.ISREAD, f.ISINSERT, f.ISUPDATE, f.ISDELETE,,f.ISPRINT, f.ISEXPORT " +
            "FROM FEATURE_CAT as f INNER JOIN GROUP_FEATURE_REL as u ON (f.ID = u.featureid) " +
            "WHERE u.GROUPID = @groupId and f.VISIBLE_BIT = 'Y'";

            DbCommand dbSelect = db.GetSQLStringCommand(selectStr);
            db.AddInParameter(dbSelect, "@groupId", DbType.Int64, groupId);
            DataSet ds = new DataSet();
            db.LoadDataSet(dbSelect, ds, tableName);

            return ds;
        }
    }
}
