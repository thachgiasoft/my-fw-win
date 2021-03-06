using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Cập nhật phiên bản mới vào DB 
    /// </summary>
    public partial class frmFWLiveUpdate : XtraFormPL
    {
        private string zipFilePath;     // Duong dan file du lieu update (.zip)
        protected string thongBaoHoiCoCapNhatMoi;
        protected string thongBaoHoiCoCapNhatCu;

        public frmFWLiveUpdate()
        {
            InitializeComponent();
            //this.rdoFile.Checked = true;
            this.txtURL.Enabled = false;
            this.btnCapNhat.Enabled = true;
        }

        private void frmLiveUpdate_Load(object sender, EventArgs e)
        {
            HelpXtraForm.SetFix(this);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected virtual bool checkAllowUpdate()
        {
            if (FrameworkParams.currentUser.username != "admin")
            {
                HelpMsgBox.ShowNotificationMessage("Chức năng này dành riêng cho admin.");
                return false;
            }

            this.thongBaoHoiCoCapNhatMoi = "Có phiên bản phần mềm mới nhất từ PROTOCOL Software. \nBạn có muốn cập nhật về máy chủ nội bộ không?";
            this.thongBaoHoiCoCapNhatCu = "Phiên bản phần mềm đang sử dụng là mới hơn phiên bản bạn muốn cập nhật. \nBạn có muốn cập nhật về máy chủ nội bộ ?";

            return true;
        }

        private void btnThucHien_Click(object sender, EventArgs e)
        {
            if (checkAllowUpdate() == false) return;

            DevExpress.Utils.WaitDialogForm wait = null;
            try
            {
                this.btnThoat.Enabled = false;
                #region Lấy thông tin local version
                int localVersion = -1;
                if (FrameworkParams.IsUpdateVersionAtLocalServer)
                {
                    localVersion = LiveUpdateHelper.getVersionFromCustomerServer();
                    if (localVersion == -1)
                    {
                        LiveUpdateHelper.initDataStructure();
                        localVersion = 0;
                    }
                }
                else
                {
                    localVersion = LiveUpdateHelper.getLocalhostVersion();
                }
                #endregion

                #region Lấy thông tin newVersion
                int newVersion = -1;
                if (!rdoURL.Checked)
                    newVersion = LiveUpdateHelper.getVersionFromZipFile(btnEditFileDuLieu.EditValue.ToString());
                else
                    newVersion = LiveUpdateHelper.getVersionFromProtocolServer();

                if (newVersion == -1)
                {
                    HelpMsgBox.ShowNotificationMessage("Có lỗi trong quá trình cập nhật. \n Vui lòng thử lại sau.");
                    return;
                }
                #endregion

                #region Kiểm tra nếu có phiên bản mới hay ko
                if (localVersion < newVersion)
                {
                    if (FrameworkParams.IsUpdateVersionAtLocalServer == false)
                    {
                        if (PLMessageBox.ShowConfirmMessage(this.thongBaoHoiCoCapNhatMoi) != DialogResult.Yes)
                        {
                            return;
                        }
                    }
                    else
                    {
                        //Cập nhật thẳng không hỏi ???
                    }
                }
                else if (localVersion == newVersion)
                {
                    HelpMsgBox.ShowNotificationMessage("Phiên bản phần mềm đang sử dụng là mới nhất.");
                    return;
                }
                else
                {
                    if(PLMessageBox.ShowConfirmMessage(this.thongBaoHoiCoCapNhatCu) != DialogResult.Yes)
                    {
                        return;
                    }
                    return;
                }
                #endregion

                #region Lấy dữ liệu cập nhật (từ HTTP SERVER || Từ localfile)
                wait = new DevExpress.Utils.WaitDialogForm("Đang xử lý ...", "");
                //Nếu URL != null thì sẽ lấy dữ liệu từ URL về máy cục bộ thông qua giao thức HTTP
                if (this.rdoURL.Checked)
                {
                    String URL = LiveUpdateHelper.getVersionURLFromProtocolServer();
                    //Quá trình download tập tin từ mạng
                    if (!downloadFileFromURL(URL))
                    {
                        //wait.Close();
                        HelpMsgBox.ShowNotificationMessage("Có lỗi trong quá trình cập nhật. \n Vui lòng thử lại sau.");
                        return;
                    }
                }
                else
                {
                    //Quá trình download tập tin từ cục bộ
                }
                
                #endregion

                #region Cập nhật phiên bản mới vào máy chủ Nội bộ
                if (FrameworkParams.IsUpdateVersionAtLocalServer)
                {
                    FileStream fileReader = null;
                    if (!rdoURL.Checked)
                        fileReader = new FileStream(btnEditFileDuLieu.EditValue.ToString(), FileMode.Open, FileAccess.Read);
                    else
                        fileReader = new FileStream(zipFilePath, FileMode.Open, FileAccess.Read);

                    byte[] fileByte = new byte[(int)fileReader.Length];
                    fileReader.Read(fileByte, 0, System.Convert.ToInt32(fileReader.Length));
                    fileReader.Flush();
                    fileReader.Close();

                    DatabaseFB db = DABase.getDatabase();
                    DbCommand command = DABase.getDatabase().GetSQLStringCommand("UPDATE FW_LIVE_UPDATE SET FILECONTENT = @file, VERSION = @newversion WHERE ID=1");
                    db.AddInParameter(command, "@newversion", DbType.Int16, newVersion);
                    db.AddInParameter(command, "@file", DbType.Binary, fileByte);

                    if (db.ExecuteNonQuery(command) == 0)
                    {
                        db = DABase.getDatabase();
                        command = DABase.getDatabase().GetSQLStringCommand("INSERT INTO FW_LIVE_UPDATE VALUES(1, @file, @newversion)");
                        db.AddInParameter(command, "@newversion", DbType.Int16, newVersion);
                        db.AddInParameter(command, "@file", DbType.Binary, fileByte);
                        db.ExecuteNonQuery(command);
                    }
                }
                #endregion

                #region Cập nhật vào Localhost
                else
                {
                    if (!rdoURL.Checked)
                    {
                        if (File.Exists(LiveUpdateHelper.UPDATE_DOWNLOAD_VERSION_ZIP_FILE))
                        {
                            File.Delete(LiveUpdateHelper.UPDATE_DOWNLOAD_VERSION_ZIP_FILE);
                        }

                        if (Directory.Exists(LiveUpdateHelper.UPDATE_DOWNLOAD_FOLDER) == false)
                            Directory.CreateDirectory(LiveUpdateHelper.UPDATE_DOWNLOAD_FOLDER);

                        File.Copy(btnEditFileDuLieu.EditValue.ToString(),
                            LiveUpdateHelper.UPDATE_DOWNLOAD_VERSION_ZIP_FILE);
                    }
                }
                #endregion
                if (FrameworkParams.IsUpdateVersionAtLocalServer == true)
                {
                    HelpMsgBox.ShowNotificationMessage("Đã cập nhật thành công. \nVui lòng cập nhật phiên bản mới từ máy chủ nội bộ.");
                }
                else
                {
                    LiveUpdateHelper.updateNewVersionHelper(FrameworkParams.IsUpdateVersionAtLocalServer, "" + newVersion);
                }
            }
            catch (Exception ex)
            {
                HelpMsgBox.ShowNotificationMessage("Có lỗi trong quá trình cập nhật. \nVui lòng thử lại sau.");
            }
            finally
            {
                if(wait != null ) wait.Close();
                btnCapNhat.Enabled = false;
                btnThoat.Enabled = true;
            }
        }
        
        #region Xử lý cập nhật từ file
        private void btnEditFileDuLieu_EditValueChanged(object sender, EventArgs e)
        {
            if (this.rdoFile.Checked && this.btnEditFileDuLieu.Text != "")
                this.btnCapNhat.Enabled = true;
            else
                this.btnCapNhat.Enabled = false;
        }
        //Chọn cập nhật từ File
        private void rdoFile_CheckedChanged(object sender, EventArgs e)
        {
            this.txtURL.Enabled = false;
            this.btnEditFileDuLieu.Enabled = true;
            this.btnEditFileDuLieu.Focus();

            if (this.btnEditFileDuLieu.Text != string.Empty)
                this.btnCapNhat.Enabled = true;
            else
                this.btnCapNhat.Enabled = false;
        }
        private void btnEditFileDuLieu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            this.zipFilePath = this.ShowDataOpenFileDialog();
            this.btnEditFileDuLieu.Text = this.zipFilePath;
        }
        private string ShowDataOpenFileDialog()
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.CheckFileExists = true;
            dlg.CheckPathExists = true;
            dlg.RestoreDirectory = true;
            dlg.Multiselect = false;
            dlg.Title = "Chon tập tin phiên bản";
            dlg.Filter = "Zip File (*.zip) | *.zip";

            string filePath = string.Empty;

            if (dlg.ShowDialog() == DialogResult.OK)
                filePath = dlg.FileName;

            return filePath;
        }
        #endregion

        #region Xử lý cập nhật từ PROTOCOL
        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.txtURL.Text != string.Empty)
                this.btnCapNhat.Enabled = true;
            else
                this.btnCapNhat.Enabled = false;
        }

        //Chọn cập nhật từ PROTOCOL
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.btnEditFileDuLieu.Enabled = false;
            this.txtURL.Enabled = true;
            //this.txtURL.Focus();
            //this.txtURL.SelectAll();
            //this.txtURL.Text = FrameworkParams.UpdateURL;
            
            btnCapNhat.Enabled = true;
        }

        private bool downloadFileFromURL(string url)
        {
            string pathDownLoad = LiveUpdateHelper.UPDATE_DOWNLOAD_VERSION_ZIP_FILE;
            try
            {
                //Kiểm tra địa chỉ chứa dữ liệu download có hợp lệ không
                string fileName = url.Substring(url.LastIndexOf('/') + 1);
                if (fileName == "" || fileName.LastIndexOf('.') < 0 ||
                        (fileName.LastIndexOf('.') >= 0 &&
                            fileName.Substring(fileName.LastIndexOf('.') + 1) != "zip"))
                {
                    HelpMsgBox.ShowNotificationMessage("File không đúng định dạng (*.zip)");
                    return false;
                }

                //Tạo thư mục để lưu dữ liệu chứa dữ liệu cập nhật
                if (Directory.Exists(LiveUpdateHelper.UPDATE_DOWNLOAD_FOLDER))
                {
                    Directory.Delete(LiveUpdateHelper.UPDATE_DOWNLOAD_FOLDER, true);
                    Directory.CreateDirectory(LiveUpdateHelper.UPDATE_DOWNLOAD_FOLDER);
                }
                
                //Download file from URL
                try
                {
                    System.Net.WebClient webClient = new System.Net.WebClient();
                    webClient.DownloadFile(url, pathDownLoad);
                    
                    this.zipFilePath = pathDownLoad;
                }
                catch (Exception err)
                {
                    PLException.AddException(err);
                    HelpMsgBox.ShowNotificationMessage("Lỗi xãy ra khi tải dữ liệu. Xin vui lòng kiểm tra kết lại kết nối mạng.");
                    Directory.Delete(pathDownLoad, true);
                    return false;
                }

                return true;
            }
            catch { 
                return false; 
            }
        }
        #endregion        
    }
}