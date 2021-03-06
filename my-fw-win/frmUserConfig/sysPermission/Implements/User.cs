using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ProtocolVN.Framework.Core;
using System.Data.Common;

namespace ProtocolVN.Framework.Win
{
    public class User
    {
        public long id;
        public string username = "";
        public string password;
        public string fullname;
        public DateTime lastAccess;
        public bool isChangePass;
        public bool isActive;
        public string savePass;
        public long employee_id;
        public string isAutoLogin;

        public List<Group> groups;

        public User() {
            this.groups = new List<Group>();
        }

        public User(long id, string username, string password, bool isChangePass)
        {
            this.id = id;
            this.isChangePass = isChangePass;
            this.username = username;
            this.password = password;
        }

        public bool login()
        {
            if (DAUser.Instance.checkPassword(username, password))
            {
                DAUser.Instance.updateLastLogin(username);
                return true;
            }
            return false;
        }

        public void loadCookies()
        {
            User user = FAUser.loadUser();
            this.username = user.username;
            this.password = user.password;
            this.savePass = user.savePass;
            this.isAutoLogin = user.isAutoLogin;
        }

        public void updateCookies(String savePass, string isAutoLogin)
        {
            FAUser.saveUser(this.username, this.password, savePass, isAutoLogin);
        }

        public void loadByUserName()
        {
            User user = DAUser.Instance.findByUserName(this.username);
            
            this.id = user.id;
            this.password = user.password;
            this.fullname = user.fullname;
            this.username = user.username;
            this.isChangePass = user.isChangePass;
            this.isActive = user.isActive;
            this.employee_id = user.employee_id;
        }

        public void loadByUserId()
        {
            User user = DAUser.Instance.findById(this.id);

            this.id = user.id;
            this.password = user.password;
            this.fullname = user.fullname;
            this.username = user.username;
            this.isChangePass = user.isChangePass;
            this.isActive = user.isActive;
            this.employee_id = user.employee_id;
        }

        public bool insert()
        {            
            return DAUser.Instance.save( this.username, this.password, this.isChangePass,
                                  this.isActive, this.employee_id, this.groups);
        }

        public bool updatePassword()
        {
            return DAUser.Instance.changePassword(this.username, this.password);
        }

        public bool update()
        {
            return DAUser.Instance.update(this.id, this.username, this.password, this.isChangePass,
                                    this.isActive, this.employee_id, this.groups);
        }
        
        public void loadGroups()
        {
            this.groups = DAUser.Instance.getListGroupByIdUser(this.id);
        }

        public bool delete()
        {
            return DAUser.Instance.delete(this.id);
        }

        public String toString()
        {
            return this.username + " - " + this.fullname;
        }

        public static DataView loadAllUser(){
            DataSet ds = DAUser.Instance.loadDataSetUser("USER_TBL");
            DataViewManager dvManager = new DataViewManager(ds);
            DataView dv = dvManager.CreateDataView(ds.Tables["USER_TBL"]);
            return dv;
        }

        public static List<User> loadAllUserInfo()
        {
            return DAUser.Instance.findAll();
        }

        
        public static bool exist(String username)
        {
            return DAUser.Instance.checkingExistUser(username);
        }

        public static bool isAdmin(string username)
        {
            if (username.Equals("admin"))
                return true;
            return false;
        }

        public static bool isAdmin()
        {
            if (FrameworkParams.currentUser.username.Equals("admin"))
                return true;
            return false;
        }
    }

    public class DAUser
    {
        public static DAUser Instance = new DAUser();
        private DAUser() { }

        public bool checkPassword(string username, string password)
        {
            DatabaseFB db = DABase.getDatabase();
            DbCommand dbSelect = db.GetSQLStringCommand("SELECT PWD FROM USER_CAT" +
                                    " WHERE USERNAME=@username AND ISACTIVE_BIT='Y'");
            db.AddInParameter(dbSelect, "@username", DbType.String, username);
            IDataReader reader = db.ExecuteReader(dbSelect);
            if (reader.Read())
            {
                String hashPwd = reader["PWD"].ToString();
                reader.Close();
                if (ProtocolHash.VerifyHash(password, "SHA512", hashPwd)) return true;
            }
            return false;
        }

        public string GetFullName(long UserID)
        {
            try
            {
                DatabaseFB db = DABase.getDatabase();
                string strsql = "select dm_nhan_vien.name from user_cat, dm_nhan_vien where user_cat.employee_id = dm_nhan_vien.id and user_cat.userid = " + UserID;
                DbCommand cmd = db.GetSQLStringCommand(strsql);
                IDataReader reader = db.ExecuteReader(cmd);
                if (reader.Read())
                    return reader[0].ToString();
                return "Chưa cấp cho nhân viên";
            }
            catch
            {
                return "DBProblem";
            }
        }

        public bool updateLastLogin(string username)
        {
            try
            {
                DatabaseFB db = DABase.getDatabase();
                DbCommand update = db.GetSQLStringCommand("UPDATE USER_CAT" +
                        " SET LASTACCESS='NOW' WHERE USERNAME=@username");
                db.AddInParameter(update, "@username", DbType.String, username);
                db.ExecuteNonQuery(update);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public User findByUserName(String username)
        //PHUOC TEST
        {
            DatabaseFB db = DABase.getDatabase();
            DbCommand dbSelect = db.GetSQLStringCommand("SELECT * FROM USER_CAT WHERE USERNAME=@username");
            db.AddInParameter(dbSelect, "@username", DbType.String, username);
            IDataReader reader = db.ExecuteReader(dbSelect);
            if (reader.Read())
            {
                User user = new User();
                user.id = HelpNumber.ParseInt64(reader["USERID"].ToString());
                //user.password = reader["PWD"].ToString();
                user.password = "~!#$%^&*()_+";
                user.username = reader["USERNAME"].ToString();
                user.isChangePass = (reader["ISCHANGEPWD_BIT"].ToString().Equals("Y") ? true : false);
                user.isActive = (reader["ISACTIVE_BIT"].ToString().Equals("Y") ? true : false);
                if (!reader["LASTACCESS"].ToString().Equals(""))
                    user.lastAccess = DateTime.Parse(reader["LASTACCESS"].ToString());
                user.employee_id = (reader["EMPLOYEE_ID"] == null? -1: HelpNumber.ParseInt64(reader["EMPLOYEE_ID"].ToString()));
                reader.Close();
                user.fullname = GetFullName(user.id);
                return user;
            }
            return null;
        }

        public User findById(long userId)
        //PHUOC TEST
        {
            DatabaseFB db = DABase.getDatabase();
            //DbCommand dbSelect = db.GetSQLStringCommand("SELECT user_cat.*, employee.name as fullname FROM user_cat inner join employee on employee.id=user_cat.employee_id WHERE USERID=@userId");
            DbCommand dbSelect = db.GetSQLStringCommand("SELECT user_cat.*, DM_NHAN_VIEN.name as fullname FROM user_cat left join DM_NHAN_VIEN on DM_NHAN_VIEN.id=user_cat.employee_id WHERE user_cat.USERID=@userId");
            db.AddInParameter(dbSelect, "@userId", DbType.Int64, userId);
            IDataReader reader = db.ExecuteReader(dbSelect);
            if (reader.Read())
            {
                User user = new User();
                user.id = HelpNumber.ParseInt64(reader["USERID"].ToString());
                user.fullname = (reader["fullname"]==null? "" : reader["fullname"].ToString());
                user.password = "~!#$%^&*()_+";
                user.username = reader["USERNAME"].ToString();
                user.isChangePass = (reader["ISCHANGEPWD_BIT"].ToString().Equals("Y") ? true : false);
                user.isActive = (reader["ISACTIVE_BIT"].ToString().Equals("Y") ? true : false);
                if (!reader["LASTACCESS"].ToString().Equals(""))
                    user.lastAccess = DateTime.Parse(reader["LASTACCESS"].ToString());
                user.employee_id = (reader["EMPLOYEE_ID"] == null? -1 : HelpNumber.ParseInt64(reader["EMPLOYEE_ID"]));
                reader.Close();
                return user;
            }
            return null;
        }

        public DataSet loadDataSetUser(string tableName)
        //PHUOC OK
        {
            DatabaseFB db = DABase.getDatabase();
            //HUNG
            //string select = "SELECT  user_cat.userid, user_cat.username, employee.name as fullname, department.name as department_name, lastaccess  " +
            //             "FROM employee INNER JOIN user_cat ON  employee.id = user_cat.employee_id " +
            //             "JOIN department ON department.id=employee.department_id ORDER BY lower(username) ASC";
            string select = "SELECT  user_cat.userid, user_cat.username, DM_NHAN_VIEN.name as fullname, department.name as department_name, lastaccess  " +
                         "FROM DM_NHAN_VIEN RIGHT JOIN user_cat ON  DM_NHAN_VIEN.id = user_cat.employee_id " +
                         "LEFT JOIN department ON department.id=DM_NHAN_VIEN.department_id ORDER BY lower(username) ASC";
            DbCommand dbSelect = db.GetSQLStringCommand(select);
            //------------------------------------------
            DataSet ds = new DataSet();
            db.LoadDataSet(dbSelect, ds, tableName);
            return ds;
        }

        public List<User> findAll()
        //PHUOC
        {
            List<User> users = new List<User>();
            DatabaseFB db = DABase.getDatabase();
            DbCommand dbSelect = db.GetSQLStringCommand("SELECT * FROM USER_CAT ORDER BY lower(USERNAME)");
            IDataReader reader = db.ExecuteReader(dbSelect);
            while (reader.Read())
            {
                User user = new User();
                user.id = HelpNumber.ParseInt64(reader["USERID"].ToString());
                user.username = reader["USERNAME"].ToString();
                //user.password = reader["PWD"].ToString();
                user.password = "~!#$%^&*()_+";

                user.isChangePass = (reader["ISCHANGEPWD_BIT"].ToString().Equals("Y") ? true : false);
                user.isActive = (reader["ISACTIVE_BIT"].ToString().Equals("Y") ? true : false);

                if (!reader["LASTACCESS"].ToString().Equals(""))
                    user.lastAccess = DateTime.Parse(reader["LASTACCESS"].ToString());

                users.Add(user);
            }
            reader.Close();
            return users;
        }

        public List<Group> getListGroupByIdUser(long userId)
        //PHUOC OK
        {
            List<Group> groups = new List<Group>();

            DatabaseFB db = DABase.getDatabase();
            DbCommand dbSelect = db.GetSQLStringCommand("SELECT * FROM GROUP_CAT g, GROUP_USER_REL ug WHERE" +
                                        " g.GROUPID=ug.GROUPID AND ug.USERID=@userId ORDER BY lower(GROUPNAME) ASC");
            db.AddInParameter(dbSelect, "@userId", DbType.Int64, userId);
            IDataReader reader = db.ExecuteReader(dbSelect);
            while (reader.Read())
            {
                Group group = new Group();
                group.id = HelpNumber.ParseInt64(reader["groupid"].ToString());
                group.groupName = reader["groupname"].ToString();
                groups.Add(group);
            }
            reader.Close();
            return groups;
        }

        public bool checkingExistUser(string username)
        //PHUOC TEST
        {
            DatabaseFB db = DABase.getDatabase();
            DbCommand dbSelect = db.GetSQLStringCommand("SELECT COUNT(UserID) FROM USER_CAT WHERE username=@username");
            db.AddInParameter(dbSelect, "@username", DbType.String, username);

            if (db.ExecuteScalar(dbSelect).ToString().Equals("1"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private long raiseIdUserNew()
        //PHUOC OK
        {
            long userId = 0;
            DatabaseFB db = DABase.getDatabase();
            DbCommand dbSelect = db.GetSQLStringCommand("SELECT FIRST 1 USERID FROM USER_CAT ORDER BY USERID DESC");
            IDataReader reader = db.ExecuteReader(dbSelect);
            if (reader.Read())
                userId = HelpNumber.ParseInt64(reader["USERID"].ToString()) + 1;
            reader.Close();
            return userId;
        }

        public bool updateFeature(long userid, List<Feature> features)
        //PHUOC OK
        {
            //Delete current feature
            DatabaseFB db = DABase.getDatabase();
            DbTransaction dbTrans = db.BeginTransaction(db.OpenConnection());

            DbCommand delete = db.GetSQLStringCommand("DELETE FROM USER_FEATURE_REL WHERE USERID = @userId");
            db.AddInParameter(delete, "@userId", DbType.Int64, userid);
            try
            {
                db.ExecuteNonQuery(delete, dbTrans);
            }
            catch
            {
                db.RollbackTransaction(dbTrans);
                return false;
            }

            //Insert new feature
            DbCommand insert = db.GetSQLStringCommand("INSERT INTO USER_FEATURE_REL(FEATUREID,USERID,ISREAD,ISINSERT,ISUPDATE,ISDELETE) " +
                "VALUES(@featureid,@userId,@isRead,@isInsert,@isUpdate,@isDelete)");
            foreach (Feature feature in features)
            {
                db.AddInParameter(insert, "@featureid", DbType.Int64, feature.id);
                db.AddInParameter(insert, "@userId", DbType.Int64, userid);
                db.AddInParameter(insert, "@isRead", DbType.String, (feature.isRead == true ? "Y" : "N"));
                db.AddInParameter(insert, "@isInsert", DbType.String, (feature.isInsert == true ? "Y" : "N"));
                db.AddInParameter(insert, "@isUpdate", DbType.String, (feature.isUpdate == true ? "Y" : "N"));
                db.AddInParameter(insert, "@isDelete", DbType.String, (feature.isDelete == true ? "Y" : "N"));
                try
                {
                    db.ExecuteNonQuery(insert, dbTrans);
                }
                catch
                {
                    db.RollbackTransaction(dbTrans);
                    return false;
                }
                insert.Parameters.Clear();
            }
            db.CommitTransaction(dbTrans);

            return true;
        }

        #region HUNG, sua lai ham save o ben duoi, them ao field employee_id
        public bool save(string username, string password, bool isChangePWD,
           bool isActive, long employee_id, List<Group> listGroup)
        //PHUOC OK
        {
            long userId = raiseIdUserNew();
            DatabaseFB db = DABase.getDatabase();
            DbTransaction dbTrans = db.BeginTransaction(db.OpenConnection());
            //Insert USER
            DbCommand insert = db.GetSQLStringCommand("INSERT INTO USER_CAT(USERID, USERNAME,PWD,ISCHANGEPWD_BIT,ISACTIVE_BIT, EMPLOYEE_ID) " +
                    "VALUES(@userId, @username,@password,@isChangePwd,@isActive, @employee_id)");
            db.AddInParameter(insert, "@userId", DbType.Int64, userId);
            db.AddInParameter(insert, "@username", DbType.String, username);
            string hashPass = ProtocolHash.ComputeHash(password, "SHA512", null);
            db.AddInParameter(insert, "@password", DbType.String, hashPass);
            db.AddInParameter(insert, "@isChangePwd", DbType.String, (isChangePWD == true ? "Y" : "N"));
            db.AddInParameter(insert, "@isActive", DbType.String, (isActive == true ? "Y" : "N"));
            //db.AddInParameter(insert, "@employee_id", DbType.Int64, employee_id);
            if (employee_id > 0) db.AddInParameter(insert, "@employee_id", DbType.Int64, employee_id);
            else db.AddInParameter(insert, "@employee_id", DbType.Int64, DBNull.Value);
            try
            {
                db.ExecuteNonQuery(insert, dbTrans);
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
                db.RollbackTransaction(dbTrans);
                return false;
            }
            insert.Parameters.Clear();
            //Insert GROUP_USER_TBL
            insert = db.GetSQLStringCommand("INSERT INTO GROUP_USER_REL(USERID, GROUPID) " +
                                            "VALUES(@userId,@groupId)");
            foreach (Group group in listGroup)
            {
                db.AddInParameter(insert, "@userId", DbType.Int64, userId);
                db.AddInParameter(insert, "@groupId", DbType.Int64, group.id);
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
        #endregion
        #region

        #endregion

        public bool update(long userId, string username, string password,
            bool isChangePWD, bool isActive, long employee_id, List<Group> listGroup)
        //PHUOC OK
        {
            DatabaseFB db = DABase.getDatabase();
            DbTransaction dbTrans = db.BeginTransaction(db.OpenConnection());
            //Update USER_TBL
            string query = "";
            string hashPass = "";
            if (password == "~!#$%^&*()_+")
            {
                query = "UPDATE USER_CAT SET  USERNAME=@username" +
                    ",ISCHANGEPWD_BIT=@isChangePWD,isactive_bit=@isActive,EMPLOYEE_ID=@employee_id" +
                    " WHERE USERID=@userId";
            }
            else
            {
                query = "UPDATE USER_CAT SET PWD = @password, USERNAME=@username" +
                    ",ISCHANGEPWD_BIT=@isChangePWD,isactive_bit=@isActive,EMPLOYEE_ID=@employee_id" +
                    " WHERE USERID=@userId";
                hashPass = ProtocolHash.ComputeHash(password, "SHA512", null);
            }
            DbCommand update = db.GetSQLStringCommand(query);
            db.AddInParameter(update, "@userId", DbType.Int64, userId);
            db.AddInParameter(update, "@username", DbType.String, username);
            if (hashPass != "") db.AddInParameter(update, "@password", DbType.String, hashPass);
            db.AddInParameter(update, "@isChangePwd", DbType.String, (isChangePWD == true ? "Y" : "N"));
            db.AddInParameter(update, "@isActive", DbType.String, (isActive == true ? "Y" : "N"));
            if(employee_id > 0 ) db.AddInParameter(update, "@employee_id", DbType.Int64, employee_id);
            else db.AddInParameter(update, "@employee_id", DbType.Int64, DBNull.Value);
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
            //Delete GROUP_USER_TBL
            update = db.GetSQLStringCommand("DELETE FROM GROUP_USER_REL WHERE USERID=@userId");
            db.AddInParameter(update, "@userId", DbType.Int64, userId);
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
            update = db.GetSQLStringCommand("INSERT INTO GROUP_USER_REL(USERID, GROUPID) VALUES(@userId,@groupId)");
            foreach (Group group in listGroup)
            {
                db.AddInParameter(update, "@userId", DbType.Int64, userId);
                db.AddInParameter(update, "@groupId", DbType.Int64, group.id);
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
            }

            db.CommitTransaction(dbTrans);
            return true;
        }

        public bool delete(long userid)
        //PHUOC OK
        {
            DatabaseFB db = DABase.getDatabase();
            DbTransaction dbTrans = db.BeginTransaction(db.OpenConnection());

            DbCommand dbDelete = db.GetSQLStringCommand("DELETE FROM USER_FEATURE_REL WHERE userid=@userid");
            db.AddInParameter(dbDelete, "@userid", DbType.Int64, userid);
            try
            {
                db.ExecuteNonQuery(dbDelete, dbTrans);
                dbDelete.Parameters.Clear();
            }
            catch
            {
                db.RollbackTransaction(dbTrans);
                return false;
            }

            dbDelete = db.GetSQLStringCommand("DELETE FROM GROUP_USER_REL WHERE USERID=@userid");
            db.AddInParameter(dbDelete, "@userid", DbType.Int64, userid);
            try
            {
                db.ExecuteNonQuery(dbDelete, dbTrans);
                dbDelete.Parameters.Clear();
            }
            catch
            {
                db.RollbackTransaction(dbTrans);
                return false;
            }

            dbDelete = db.GetSQLStringCommand("DELETE FROM USER_CAT WHERE userid=@userid");
            db.AddInParameter(dbDelete, "@userid", DbType.Int64, userid);
            try
            {
                db.ExecuteNonQuery(dbDelete, dbTrans);
                dbDelete.Parameters.Clear();
            }
            catch
            {
                db.RollbackTransaction(dbTrans);
                return false;
            }

            db.CommitTransaction(dbTrans);
            return true;
        }

        public bool changePassword(string username, string newPass)
        //PHUOC - TEST
        {
            DatabaseFB db = DABase.getDatabase();
            string hashPass = ProtocolHash.ComputeHash(newPass, "SHA512", null);
            DbCommand dbUpdate = db.GetSQLStringCommand("UPDATE USER_CAT SET PWD=@newPass WHERE username=@username AND ISCHANGEPWD_BIT='Y'");
            db.AddInParameter(dbUpdate, "@newPass", DbType.String, hashPass);
            db.AddInParameter(dbUpdate, "@username", DbType.String, username);

            if (db.ExecuteNonQuery(dbUpdate) > 0)
                return true;
            else
                return false;
        }
    }

    public class FAUser
    {
        public static string COOKIES = RadParams.RUNTIME_PATH + @"\conf\cookies.cpl";

        public static User loadUser()
        {
            User user = new User();
            DataSet ds = new DataSet();
            try
            {
                if (ConfigFile.ReadXML(FAUser.COOKIES, ds) == false)
                {
                    saveUser("admin", "admin", "Y", "N");
                    ConfigFile.ReadXML(FAUser.COOKIES, ds);
                }

                DataTable myDataTable = ds.Tables["SYS_USER"];
                if (myDataTable.Rows.Count > 0)
                {
                    DataRow row = myDataTable.Rows[0];
                    user.username = (string)row["username"];
                    user.password = (string)row["password"];
                    user.savePass = (string)row["savepassword"];
                    user.isAutoLogin = (string)row["isautologin"];
                }
            }
            catch
            {
                user.username = "";
                user.password = "";
                user.savePass = "";
                user.isAutoLogin = "";
            }
            return user;
        }

        public static void saveUser(string username, string password, string savePassword, string isAutoLogin)
        {
            string xmlDoc = @"<?xml version='1.0' encoding='utf-8' standalone='yes'?>
                     <NewDataSet>
                        <SYS_USER>                            
                            <username>" + username + @"</username>
                            <password>" + password + @"</password>                                 
                            <savepassword>" + savePassword + @"</savepassword>
                            <isautologin>" + isAutoLogin + @"</isautologin>
                        </SYS_USER>
                    </NewDataSet>";
            ConfigFile.Save(FAUser.COOKIES, xmlDoc);
        }

        public static string loadUsername()
        {
            string username = "";
            DataSet ds = new DataSet();
            ds.ReadXml(ConfigFile.Load(FAUser.COOKIES));
            DataTable myDataTable = ds.Tables["SYS_USER"];
            if (myDataTable.Rows.Count > 0)
            {
                DataRow row = myDataTable.Rows[0];
                username = (string)row["username"];
            }
            return username;
        }
    }
}