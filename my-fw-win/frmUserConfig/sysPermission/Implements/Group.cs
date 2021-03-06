using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ProtocolVN.Framework.Core;
using System.Data.Common;
namespace ProtocolVN.Framework.Win
{
    public class Group
    {
        public long id;
        public string groupName;
        public List<User> users;

        public Group() { }

        public Group(long id, string gName)
        {
            this.id = id;
            this.groupName = gName;
        }

        public void load()
        {
            Group tmp = DAGroup.Instance.findById(this.id);
            this.id = tmp.id;
            this.groupName = tmp.groupName;
        }

        public void loadUsers()
        {
            this.users = DAGroup.Instance.getListUserByIdGroup(this.id);
        }

        public void insert()
        {
            DAGroup.Instance.save( this.groupName, this.users);
        }

        public void update()
        {
            DAGroup.Instance.update(this.id,  this.groupName, this.users);
        }

        public bool delete()
        {
            return DAGroup.Instance.delete(this.id);
        }

        public static DataView loadAllGroup()
        {
            DataSet ds = DAGroup.Instance.loadDataSetGroup();
            DataViewManager dvManager = new DataViewManager(ds);
            DataView dv = dvManager.CreateDataView(ds.Tables["GROUP_TBL"]);
            return dv;
        }

        public static List<Group> loadAllGroupInfo()
        {
            return DAGroup.Instance.findAllGroup();
        }

        public static bool exist(string groupName)
        {
            return DAGroup.Instance.checkingExistGroupName(groupName);
        }
    }


    //PHIEN BAN HUNG
    class DAGroup
    {
        public static readonly DAGroup Instance = new DAGroup();
        private DAGroup() { }

        public List<Group> findAllGroup()
        //PHUOC OK
        {
            List<Group> groups = new List<Group>();
            DatabaseFB db = DABase.getDatabase();
            DbCommand select = db.GetSQLStringCommand("SELECT GROUPID, GROUPNAME FROM GROUP_CAT ORDER BY lower(GROUPNAME) ASC");
            IDataReader reader = db.ExecuteReader(select);
            while (reader.Read())
            {
                Group group = new Group(HelpNumber.ParseInt64(reader["GROUPID"].ToString()), reader["GROUPNAME"].ToString());
                groups.Add(group);
            }
            reader.Close();
            return groups;
        }

        public List<User> getListUserByIdGroup(long groupId)
        //PHUOC OK
        {
            List<User> users = new List<User>();
            DatabaseFB db = DABase.getDatabase();
            DbCommand select = db.GetSQLStringCommand("SELECT * FROM USER_CAT u,GROUP_USER_REL ug WHERE" +
                    " u.USERID=ug.USERID AND ug.GROUPID=@groupId ORDER BY lower(USERNAME) ASC");
            db.AddInParameter(select, "@groupId", DbType.Int64, groupId);
            IDataReader reader = db.ExecuteReader(select);
            while (reader.Read())
            {
                User user = new User();
                user.id = HelpNumber.ParseInt64(reader["USERID"]);
                user.username = reader["USERNAME"].ToString();
                user.fullname = "Chưa kết với Employee";
                user.isActive = (reader["ISACTIVE_BIT"].ToString() == "Y" ? true : false);
                user.isChangePass = (reader["ISCHANGEPWD_BIT"].ToString() == "Y" ? true : false);
                users.Add(user);
            }
            reader.Close();
            return users;
        }

        public bool checkingExistGroupName(string groupName)
        //PHUOC OK 
        {
            DatabaseFB db = DABase.getDatabase();
            DbCommand select = db.GetSQLStringCommand("SELECT COUNT(GROUPID) FROM GROUP_CAT WHERE GROUPNAME=@groupName");
            db.AddInParameter(select, "@groupName", DbType.String, groupName);
            if (db.ExecuteScalar(select).ToString().Equals("1"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private long raiseIdGroupNew()
        //PHUOC OK
        {
            return DABase.getDatabase().GetID(HelpGen.G_FW_ID);
            //DatabaseFB db = DABase.getDatabase();
            //DbCommand select = db.GetSQLStringCommand("SELECT FIRST 1 GROUPID FROM " + FrameworkParams.GROUP_TBL + " ORDER BY GROUPID DESC");
            //IDataReader reader = db.ExecuteReader(select);
            //int groupId = 1;
            //if (reader.Read())
            //    groupId = HelpNumber.ParseInt32(reader["GROUPID"].ToString()) + 1;
            //reader.Close();
            //return groupId;
        }

        public bool save(string groupName, List<User> listUser)
        //PHUOC OK
        {
            //Get new id for group
            long groupId = raiseIdGroupNew();

            DatabaseFB db = DABase.getDatabase();
            DbTransaction dbTrans = db.BeginTransaction(db.OpenConnection());
            //Insert into GROUP_TBL
            DbCommand insert = db.GetSQLStringCommand("INSERT INTO GROUP_CAT" +
                        "(GROUPID,GROUPNAME) VALUES(@groupId ,@groupName)");
            db.AddInParameter(insert, "@groupId", DbType.Int64, groupId);
            db.AddInParameter(insert, "@groupName", DbType.String, groupName);
            try
            {
                db.ExecuteNonQuery(insert, dbTrans);
                insert.Parameters.Clear();
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
                db.RollbackTransaction(dbTrans);
                return false;
            }
            //Insert into GROUP_USER_TBL
            insert = db.GetSQLStringCommand("INSERT INTO GROUP_USER_REL(USERID, GROUPID) VALUES(@userId ,@groupId)");
            foreach (User user in listUser)
            {
                db.AddInParameter(insert, "@userId", DbType.Int64, user.id);
                db.AddInParameter(insert, "@groupId", DbType.Int64, groupId);
                try
                {
                    db.ExecuteNonQuery(insert, dbTrans);
                    insert.Parameters.Clear();
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

        public bool update(long groupId, string groupName, List<User> users)
        //PHUOC OK
        {
            DatabaseFB db = DABase.getDatabase();
            DbTransaction dbTrans = db.BeginTransaction(db.OpenConnection());
            //Update GROUP_TBL
            DbCommand update = db.GetSQLStringCommand("UPDATE GROUP_CAT SET GROUPNAME=@groupName WHERE GROUPID=@groupId");
            db.AddInParameter(update, "@groupName", DbType.String, groupName);
            db.AddInParameter(update, "@groupId", DbType.Int64, groupId);
            try
            {
                db.ExecuteNonQuery(update, dbTrans);
                update.Parameters.Clear();
            }
            catch
            {
                db.RollbackTransaction(dbTrans);
                return false;
            }

            //Delete GROUP_USER_TBL with groupid
            update = db.GetSQLStringCommand("DELETE FROM GROUP_USER_REL WHERE groupid=@groupId");
            db.AddInParameter(update, "@groupId", DbType.Int64, groupId);
            try
            {
                db.ExecuteNonQuery(update, dbTrans);
                update.Parameters.Clear();
            }
            catch
            {
                db.RollbackTransaction(dbTrans);
                return false;
            }
            //Update GROUP_USER_TBL                
            update = db.GetSQLStringCommand("INSERT INTO GROUP_USER_REL(USERID, GROUPID)" +
                            " VALUES(@userid, @groupId)");
            foreach (User user in users)
            {
                db.AddInParameter(update, "@groupId", DbType.Int64, groupId);
                db.AddInParameter(update, "@userid", DbType.Int64, user.id);
                try
                {
                    db.ExecuteNonQuery(update, dbTrans);
                    update.Parameters.Clear();
                }
                catch (Exception ex)
                {
                    PLException.AddException(ex);
                    db.RollbackTransaction(dbTrans);
                    return false;
                }
            }
            db.CommitTransaction(dbTrans);
            return true;
        }


        public bool delete(long groupid)
        //PHUOC CANCEL --> Install Transaction
        {
            DatabaseFB db = DABase.getDatabase();
            DbTransaction dbTrans = db.BeginTransaction(db.OpenConnection());

            //Delete GROUP_FEATURE_TBL
            DbCommand delete = db.GetSQLStringCommand("DELETE FROM GROUP_FEATURE_REL WHERE GROUPID=@groupid");
            db.AddInParameter(delete, "@groupId", DbType.Int64, groupid);
            try
            {
                db.ExecuteNonQuery(delete, dbTrans);
                delete.Parameters.Clear();
            }
            catch
            {
                db.RollbackTransaction(dbTrans);
                return false;
            }

            //Delete GROUP_TBL
            delete = db.GetSQLStringCommand("DELETE FROM GROUP_CAT WHERE GROUPID=@groupid");
            db.AddInParameter(delete, "@groupId", DbType.Int64, groupid);
            try
            {
                db.ExecuteNonQuery(delete, dbTrans);
                delete.Parameters.Clear();
            }
            catch
            {
                db.RollbackTransaction(dbTrans);
                return false;
            }
            db.CommitTransaction(dbTrans);
            return true;
        }

        public DataSet loadDataSetGroup()
        //PHUOC OK
        {
            DatabaseFB db = DABase.getDatabase();
            db.Open();
            DbCommand dbSelect = db.GetSQLStringCommand("SELECT GROUPID, GROUPNAME FROM GROUP_CAT ORDER BY lower(GROUPNAME) ASC");
            DataSet ds = new DataSet();
            db.LoadDataSet(dbSelect, ds, "GROUP_TBL");
            return ds;
        }

        public Group findById(long groupId)
        //PHUOC OK
        {
            Group group = null;
            DatabaseFB db = DABase.getDatabase();
            DbCommand select = db.GetSQLStringCommand("SELECT GROUPID,  GROUPNAME FROM "
                                 + "GROUP_CAT WHERE GROUPID=@groupId");

            db.AddInParameter(select, "@groupId", DbType.Int64, groupId);
            IDataReader reader = db.ExecuteReader(select);
            if (reader.Read())
            {
                group = new Group();
                group.id = HelpNumber.ParseInt64(reader["GROUPID"].ToString());
                group.groupName = reader["GROUPNAME"].ToString();
            }
            reader.Close();
            return group;
        }
    }
}