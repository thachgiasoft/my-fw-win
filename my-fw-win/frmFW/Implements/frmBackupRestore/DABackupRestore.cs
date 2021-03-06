using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ProtocolVN.Framework.Core;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ProtocolVN.Framework.Win
{
    class DABackup
    {
        public static readonly DABackup Instance = new DABackup();
        private DABackup() { }

        #region Lưu trữ thông tin sao lưu và phục hồi
        public bool save(long UserId, string FilePath, string Note, bool isBackup)
        {
            try
            {
                DatabaseFB db = DABase.getDatabase();
                DbCommand dbInsert = db.GetSQLStringCommand("INSERT INTO BACK_REST_LOG (USERID, CDATE, FILEPATH, NOTE, ISBACKUP_BIT)" + " VALUES (@userid, 'NOW', @filepath, @note, @isbackup)");
                db.AddInParameter(dbInsert, "@userid", DbType.Int64, UserId);
                db.AddInParameter(dbInsert, "@filepath", DbType.String, FilePath);
                db.AddInParameter(dbInsert, "@note", DbType.String, Note);
                db.AddInParameter(dbInsert, "@isbackup", DbType.String, (isBackup == true ? "Y" : "N"));
                db.ExecuteNonQuery(dbInsert);
                return true;
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
                return false;
            }
            return true;
        }

        public DataSet LoadBackupRestoreLog(string tableName)
        {
            DatabaseFB db = DABase.getDatabase();
            DbCommand dbSelect = db.GetSQLStringCommand("SELECT USERNAME, CDATE, FILEPATH, NOTE, "
                + "CASE ISBACKUP_BIT WHEN 'Y' THEN 'Sao lưu' WHEN 'N' THEN 'Phục hồi' END AS ISB"
                + " FROM BACK_REST_LOG AS A, USER_CAT"
                + " AS B WHERE A.USERID = B.USERID ORDER BY CDATE DESC");
            DataSet ds = new DataSet();
            db.LoadDataSet(dbSelect, ds, tableName);

            return ds;
        }
        #endregion


        #region Sao lưu
        //PHUOCNT NC Giải pháp chưa backup từ xa được để backup từ xa ta phải thay câu lệnh hiện tại thành câu lệnh
        //gbak -b -v protocolvn.info:E/mydb.gdb C:\mybackup.fbk -user SYSDBA -pass masterkey
        private bool backup(string FilePath)
        {
            ConfigDB config = new ConfigDB();
            config.load();
            #region usingGBACK
            if (frmBackupRestore.usingGBACK == true)
            {
                try
                {
                    string arg = "";
                    if (config.databaseName == "")
                        arg = " -v -t -user " + config.username + " -password \"" + config.password + "\" " + config.database + " " + FilePath;
                    else
                        arg = " -v -t -user " + config.username + " -password \"" + config.password + "\" " + config.databaseName + " " + FilePath;

                    ProcessStartInfo psi = new ProcessStartInfo("gbak.exe", arg);
                    psi.WindowStyle = ProcessWindowStyle.Hidden;
                    Process p = Process.Start(psi);
                    if (p == null) return false;
                    while (p != null && !p.HasExited)
                    {
                        //NOOP 
                    }
                    return (p.ExitCode == 0);
                }
                catch (Exception ex)
                {
                    PLException.AddException(ex);
                    return false;
                }
            }
            #endregion
            #region usingFile
            else
            {
                try
                {
                    String dbFile = "";
                    if (config.databaseName == "")
                        dbFile = config.database;
                    else
                        dbFile = config.databaseName;

                    String dbCloneFile = "";
                    try
                    {
                        //Tạo 1 tập tin tương ứng với nội dung cần backup
                        //dbCloneFile = dbFile.Substring(0, dbFile.LastIndexOf(".")) + DateTime.Now.ToString("yyyyMMddHHmm") 
                        //    + ".gdb";
                        dbCloneFile = RadParams.RUNTIME_PATH + "\\temp\\" + dbFile.Substring(dbFile.LastIndexOf("\\") + 1);
                        if (File.Exists(dbCloneFile)) File.Delete(dbCloneFile);
                        File.Copy(dbFile, dbCloneFile);
                        
                        //Nén tập tin db lại
                        bool result = ZipFile.Zip(dbCloneFile, FilePath);

                        return result;
                    }
                    catch (Exception ex)
                    {
                        PLException.AddException(ex);
                    }
                    finally
                    {
                        //Xoa tập tin nếu đã tạo bảng sao.
                        try { if (File.Exists(dbCloneFile)) File.Delete(dbCloneFile); }
                        catch { }
                    }
                }
                catch (Exception ex)
                {
                    PLException.AddException(new PLException(ex));
                }
                //Using Zip 
            }
            #endregion
            return false;
        }

        public bool Backup(long UserId, string FilePath, string Note)
        {
            if (backup(FilePath))
               return save(UserId, FilePath, Note, true);
            return false;
        }

        #endregion

        





        #region Phục hồi

        //PHUOCNT NC Giải pháp hiện tại chưa phục hồi từ xa được
        //để làm được thay thành câu lệnh bên dưới
        //gbak -c -v C:/backup_file.fbk protocolvn.info:E:/new_db.gdb -user sysdba -pass masterkey
        private bool restore(string filePath)
        {
            ConfigDB config = new ConfigDB();
            config.load();
            #region Dùng GBACK
            if (frmBackupRestore.usingGBACK == true)
            {
                try
                {
                    string arg = "";
                    if (config.databaseName == "")
                    {
                        arg = " -r o -v -user " + config.username + " -password \"" + config.password + "\" " + filePath + " " + config.database + ".bak";
                    }
                    else
                        arg = " -r o -v -user " + config.username + " -password \"" + config.password + "\" " + filePath + " " + config.databaseName + ".bak";

                    ProcessStartInfo psi = new ProcessStartInfo("gbak.exe", arg);
                    psi.WindowStyle = ProcessWindowStyle.Hidden;
                    Process p = Process.Start(psi);
                    if (p == null) return false;
                    while (p != null && !p.HasExited)
                    {
                        //NOOP 
                    }
                    return (p.ExitCode == 0);
                }
                catch (Exception ex)
                {
                    PLException.AddException(ex);
                    return false;
                }
            }
            #endregion
            #region
            else
            {
                //Chuẩn bị tham số
                string fullDBFileName = config.databaseName;
                string dbInfo = RadParams.server + ";" + RadParams.database + ";" + RadParams.port + ";" + 
                    RadParams.username + ";" + RadParams.password + ";" + filePath + ";" + fullDBFileName;
                
                //Đóng chương trình
                FrameworkParams.ExitApplication(FrameworkParams.EXIT_STATUS.NORMAL_NO_THANKS);

                //Gọi restore.
                Process p = new Process();
                p.StartInfo.FileName = RadParams.RUNTIME_PATH + @"\update\RestoreDB.exe";
                p.StartInfo.Arguments = FrameworkParams.ExecuteFileName + ";" + dbInfo;
                try { p.Start(); } catch { }        
            }
            #endregion
            return false;
        }

        public bool Restore(long UserId, string FilePath, string Note)
        {
            bool flag = false;
            if (restore(FilePath))
            {
                //flag = save(UserId, FilePath, Note, false);
                flag = true;
            }
            return flag;
        }

        #endregion
    }
}
