using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
namespace ProtocolVN.Framework.Win
{
    /// <summary>Control cho phép upload tập tin và xem lại thông tin đã upload thông qua forderName
    /// Chỉ sử dụng Control này khi mình có FTPServer
    /// - Tạo mới một Folder tài liệu 
    ///     + Kéo control vào form
    ///     + Save: Đặt tên FolderName mong đợi khi lưu.
    /// - Đọc một Folder đã tồn tại
    ///     + Kéo control vào form
    ///     + Gán giá trị vào FolderName tương ứng Folder cần xem
    /// - Xóa một Folder đã tồn tại
    ///     + Kéo control vào form
    ///     + Gán giá trị vào FolderName tương ứng Folder cần xóa
    ///     + Delete để xóa
    /// (Chú ý khi thao tác upload và download là đã lưu).    
    /// </summary>
    public partial class PLDocFolder : DevExpress.XtraEditors.XtraUserControl
    {
        public static string CHUNG_TU_TEMP_FOLDER = FrameworkParams.TEMP_FOLDER + @"/ChungTu";
        public string FolderTemp;   //Thư mục tạm dùng để lưu file trong quá trình xử lý chứng từ

        public string FolderName;   //Bắt buộc phải khởi tạo trước khi có thể upload và download
        
        private DataTable table;    //Lưu thông tin trên lưới
        public PLDocFolder()
        {
            InitializeComponent();
            FolderName = "";
            gridColumn2.OptionsColumn.AllowEdit = false;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;            
        }

        public void Save(string FolderName)
        {
            if (FolderName != this.FolderName)
            {
                RenameDirFTP(this.FolderName, FolderName);
            }
        }

        public void Delete()
        {
            DeleteDirFTP(this.FolderName);
        }

        /// <summary>Khi tạo mới một PLChungTu
        /// </summary>
        private void _init()
        {
            InitGrid();
            FolderTemp = CHUNG_TU_TEMP_FOLDER;
            if (!HelpFTP.Instance.checkConnect())
            {
                DialogResult result = PLMessageBox.ShowConfirmMessage("Bạn chưa cấu hình. Bạn có muốn cấu hình không?");
                if (result == DialogResult.Yes)
                {
                    frmConfigFTP ftpForm = new frmConfigFTP();
                    ProtocolForm.ShowModalDialog((XtraForm)this.FindForm(), ftpForm);
                }
            }
        }

        #region Hàm hỗ trợ trên lưới 
        private void InitGrid()
        {
            DataColumn column;
            table = new DataTable();

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "FILE_NAME";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "FILE_DESCRIPT";
            table.Columns.Add(column);

            gridFTPControl.DataSource = table;
        }
        private void AddRow(string fileName, string descript)
        {
            DataRow row;
            row = table.NewRow();
            row[0] = fileName;
            row[1] = descript;
            table.Rows.Add(row);
        }  
        #endregion
        
        #region Hàm hỗ trợ FTP 
        /// <summary>
        /// Hàm khởi tạo lưới từ một foldername trên ftpserver.
        /// </summary>
        private void LoadFileInFolder(string foldername)
        {
            table.Rows.Clear();
            if (!HelpFTP.Instance.isExist(foldername))
            {
                FolderName = "";
                return;
            }

            String[] FileNames = HelpFTP.Instance.getFilesFTP(foldername);
            if (FileNames != null)
            {
                foreach (string FileName in FileNames)
                {
                    if (FileName != "")
                        AddRow(FileName , "");
                }
            }
            gridFTPControl.DataSource = table;
        }
        private void UploadToFTP()
        {
            try
            {
                OpenFileDialog OpenDlg = new OpenFileDialog();
                OpenDlg.ShowDialog();
                if (OpenDlg.FileName != "")
                {
                    string fileName = Path.GetFileName(OpenDlg.FileName);

                    //PHUOCNC Nên lấy ngày tháng tại DBServer
                    string seed = DateTime.Now.Day.ToString();
                    seed += DateTime.Now.Month.ToString();
                    seed += DateTime.Now.Year.ToString();
                    seed += DateTime.Now.Hour.ToString();
                    seed += DateTime.Now.Minute.ToString();
                    seed += DateTime.Now.Second.ToString();
                    seed += DateTime.Now.Millisecond.ToString();

                    string ext = Path.GetExtension(fileName);
                    fileName = Path.GetFileNameWithoutExtension(fileName);
                    fileName += seed;
                    fileName += ext;

                    if (!HelpFTP.Instance.upload(OpenDlg.FileName, FolderName + "/" + fileName))
                    {
                        if (!tryDeleteFile(FolderName + "/" + fileName)) throw new Exception();
                    }
                    else
                        AddRow(fileName , "");
                }
               
            }
            catch
            {
                HelpMsgBox.ShowNotificationMessage("Lưu tập tin không thành công.");
                
            }
        }
        private void LoadData()
        {
            try
            {
                if ((FolderName == ""))
                {
                    FolderName = "_tmp" + DateTime.Now.ToLongTimeString();
                    FolderName = FolderName.Replace("/" , "_");
                    FolderName = FolderName.Replace(":" , "");
                    FolderName = FolderName.Replace(" " , "");

                    HelpFTP.Instance.createDir("", FolderName);
                }
                else
                {
                    LoadFileInFolder(FolderName);
                }
            }
            catch
            {
            }
        }

        #region PHUOCNC : Chưa ổn - Xử lý vụ xóa Folder
        public void DeleteDirFTP(string name)
        {
            try
            {
                if (!HelpFTP.Instance.isExist(name))
                {
                    FolderName = "";
                    return;
                }
                tryDeleteFolder(name);
                table.Rows.Clear();   
                FolderName = "";
            }
            catch{}
        }
        //Cố gắng xóa folder sau 4 lần
        private bool tryDeleteFolder(String folderName)
        {
            int i;
            for (i = 0 ; i < 4 ; i++)
                if (HelpFTP.Instance.removeDir(folderName))
                    break;
            if (i == 4) return false;
            return true;
        }
        //Cố gắng xóa file sau 4 lần
        private bool tryDeleteFile(string fileRemotePath)
        {
            int i;
            for (i = 0; i < 4; i++)
                if (HelpFTP.Instance.delete(fileRemotePath))
                    break;
            if (i == 4) return false;
            return true;
        }
        #endregion
        public void RenameDirFTP(string oldName , string newName)
        {
            try
            {
                if (!HelpFTP.Instance.isExist(oldName))
                {
                    FolderName = "";
                    return;
                }

                if (HelpFTP.Instance.isExist(newName))
                {
                    string[] fileNames = HelpFTP.Instance.getFilesFTP(oldName);

                    if (fileNames != null)
                    {
                        foreach (string file in fileNames)
                        {
                            HelpFTP.Instance.download(oldName + "/" + file, FolderTemp + "/" + file);
                            HelpFTP.Instance.upload(FolderTemp + "/" + file, newName + "/" + file);
                        }
                        DeleteDirFTP(oldName);
                    }
                }
                else
                    HelpFTP.Instance.renameDir(oldName, newName);
                FolderName = newName;
            }
            catch
            {
            }
        }
        private void CreateFolderTemp(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                FolderTemp = path;
            }
        }
        #endregion

        #region Danh sách các sự kiện trên lưới
        private void FTPControl_Load(object sender, EventArgs e)
        {
            gridView1.OptionsBehavior.Editable = true;
            if(!this.DesignMode) _init();
            LoadData();
        }    
        //Tai tu ftp len buffer tam de mo va doc tap tin
        //Sau khi xu ly xong thi xoa file tam tren local di
        //Button Download van hien thi khi chua co file nao tren grid
        //nhung bi vo hieu hoa, nguoi dung ko the su dung button nay khi chua
        //co tap tin duoc upload len
        private void repositoryItemButtonDownload_Click(object sender, EventArgs e)
        {
            if (FolderName == "")
                return;

            string fileName = gridView1.GetFocusedRowCellDisplayText("FILE_NAME");
            if (fileName == "")
                return;

            //DataRow row = gridView1.GetDataRow(gridView1.GetRowHandle(gridView1.FocusedRowHandle));
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            try
            {
                string ftpFile = FolderName + @"\" + fileName;
                CreateFolderTemp(FolderTemp);
                string localFile = FolderTemp + "/" + fileName;
                if (HelpFTP.Instance.download(ftpFile, localFile))
                     Process.Start(localFile);
                else
                     HelpMsgBox.ShowNotificationMessage("Mở tập tin không thành công.");
             }
             catch
             {
                HelpMsgBox.ShowNotificationMessage("Mở tập tin không thành công.");
             }  
        }
 
        //Khi chua upload file len thi button delete van hien thi
        //nhung bi vo hieu hoa, nguoi dung ko the su dung button nay khi chua
        //co tap tin duoc upload len
        private void repositoryItemButtonDelete_Click(object sender, EventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
           
            try
            {
                DialogResult result = PLMessageBox.ShowConfirmMessage("Bạn có chắc chắn muốn xóa?");
                if (result == DialogResult.Yes)
                {
                    string fileName = gridView1.GetFocusedRowCellDisplayText("FILE_NAME");
                    if (fileName!= "")
                    {
                        if (HelpFTP.Instance.delete(FolderName + "/" + fileName))
                            table.Rows.Remove(row);
                        else
                            HelpMsgBox.ShowNotificationMessage("Xóa tập tin không thành công.");
                    } 
                }                  
            }
            catch{}
        }
        #endregion

        private void btnUpload_Click(object sender, EventArgs e)
        {
            UploadToFTP();
        }        
    }   
}