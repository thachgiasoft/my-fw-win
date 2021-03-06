using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
using System.Collections.Generic;
using System.Reflection;
using System;
//using DevExpres.Tutor;
using DevExpress.XtraBars.Ribbon;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Tham số toàn cục của toàn dự án
    /// </summary>
    public sealed class FrameworkParams
    {
        
        #region Tham số liên quan đến cập nhật phiên bản
      
       private uint _PropertyName;
        /// <summary>Mandatory - Có / Không kiểm tra xem có version mới hay không.
        /// Chú ý : - Việc kiểm tra này sẽ lấy thông tin từ DB nếu có version mới thì DB sẽ có thông tin mới đó.
        ///         - Giá trị mặc định là False.
        /// </summary>
        public static bool IsCheckNewVersion;//After

        /// <summary>Mandatory - Địa chỉ URL đến tập tin update.zip trên mạng
        /// </summary>
        public static string UpdateURL;

        /// <summary>Mandatory - Tên của file thực thi của chương trình
        /// </summary>
        [Obsolete("Không sử dụng")]
        internal static string ExecuteFileName = "\\" + HelpApplication.getExecuteFileName();

        /// <summary>Mandatory Cập nhật phiên bản từ PROTOCOL hay từ máy chủ nội bộ        
        /// </summary>
        public static bool IsUpdateVersionAtLocalServer = false;//After.True: IsUpdateVersionAtProtocol
        #endregion


        #region Tham số liên quan đến ID sản phẩm
        /// <summary>Mandatory - Tên khách hàng
        /// Chú ý: - Sản phẩm xây dựng cho khách hàng cụ thể thì để tên Công ty.
        ///        - Sản phẩm đóng gói để - Doanh nghiệp Việt Nam.
        /// </summary>
        public static string CustomerName;

        /// <summary>Mandatory - Tên sản phẩm
        /// </summary>
        public static string ProductName;
        #endregion

        #region Thông tin liên quan đến việc delivery sản phẩm
        /// <summary>Before - Option - Có / Không hỗ trợ hiện các thông tin debug trong ứng dụng
        /// Chú ý: Mặc định là TRUE.
        /// </summary>
        private static bool IsSupportDeveloper = true;
        public static bool isSupportDeveloper
        {
            get { return IsSupportDeveloper; }
            set
            {
                IsSupportDeveloper = value;
                RadParams.IsSupportDeveloper = value;
            }
        }

        /// <summary>After - Option - Sản phẩm đang là sản phẩm dùng thử hoặc chưa hoàn thiện
        /// Chú ý: - Mặc định là TRUE.
        /// </summary>
        public static bool IsTrial = true;
        #endregion

        #region Thông tin liên quan đến giấy phép
        internal static object Lic = null;
        public static string licenceString = null;
        #endregion

        #region Thông tin thường được sử dụng bên ngoài khi đã đăng nhập vào hệ thống
        /// <summary>Option - Người dùng hiện tại 
        /// </summary>
        public static User currentUser = new User();

        /// <summary>Option - Thông tin tùy chọn của chương trình
        /// </summary>
        public static Option option;

        /// <summary>Lấy thông tin từ màn hình chính của ứng dụng        
        /// </summary>
        public static XtraForm MainForm;
        #endregion


         #region Kiểm tra lại việc sử dụng các tham số

        #region Skins & Homepage
        /// <summary>Option - Hiển thị / Không hiển thị GallerySkins ( Các skin được gom nhóm thành 1 Gallary và đặt vào trang "Thường dùng" )
        /// Chú ý: - Xóa tập tin lưu cấu hình menu ([User]HomePage.xml)
        ///        - Giá trị mặc định là False
        ///        - Khi chọn True thì phải chọn sử dụng trang "Thường dùng"        
        /// </summary>        
        public static bool UsingGallerySkins = false;

        /// <summary>Option - Nạp / Không nạp Skin
        /// </summary>
        public static bool UsingSkin = true;
        
        /// <summary>Option - Hiển thị / Không hiển thị trang "Thường dùng".
        /// Chú ý - Xóa tập tin lưu cấu hình menu ([User]HomePage.xml)
        ///       - Giá trị mặc định là True
        /// </summary>
        public static bool UsingHomePage = true;
         
        /// <summary>Option - Danh sách các Item sẽ hiển thị trên trang "Thường dùng" ban đầu.
        /// Chú ý - Xóa tập tin lưu cấu hình menu ([User]HomePage.xml)
        ///       - Giá trị mặc định là "" ( ko có item nào )
        /// </summary>
        public static string HomePageMenu = "";


        /// <summary>After
        /// </summary>
        internal static RibbonControlStyle style = RibbonControlStyle.Office2010;
        #endregion

        #region Xử lý Menu
        /// <summary>NoUsing - Nội dung được hiển thị trong trung tâm điều khiển, không cấu hình
        /// Chú ý: - Tham số này hiện tại chưa được sử dụng
        /// </summary>
        internal static string AllMenu = "NOTSET"; 

        /// <summary>NoUsing - Danh sách các Item trên Ribbon Menu.
        /// Chú ý: Sử dụng ProtocolMenu.init(new T:ProtocolMenu()) để thay thế.
        /// </summary>
        [Obsolete("Dùng ProtocolMenu.init(new T:ProtocolMenu())")]
        public static string Menu = "";

        /// <summary>Option - Danh sách các item sẽ được gắn trực tiếp vào trang Hệ thống, đặt sau các item mặc định        
        /// - Chú ý : - Thường đặt vào đây những chức năng khi sử dụng nó ảnh hưởng đến toàn hệ thống chỉ ko riêng 1 người dùng
        /// </summary>
        internal static string appendPLSYSTEMMenu = "";

        /// <summary>Option - Danh sách các item sẽ được gắn trực tiếp vào trang Tiện ích, đặt sau các item mặc định
        /// - Chú ý : - Thường đặt vào đây các tiện ích mang tính chất hỗ trợ công việc bằng tính toán và khai thác số liệu
        /// </summary>
        internal static string appendPLTOOLMenu = "";

        /// <summary>Option - Danh sách các item sẽ được gắn trực tiếp vào trang Giúp đỡ, đặt sau các item mặc định
        /// - Chú ý : - Nhưng tính năng mang tính chất hỗ trợ của sản phẩm và bản quyền tác giả
        /// </summary>
        internal static string appendPLHELPMenu = "";

        internal static string appendPLDEVMenuItems = "";

        /// <summary>Option - Danh sách các item sẽ được gắn trực tiếp vào Application Menu, đặt trước các item mặc định
        /// - Chú ý : - Thường đặt vào đây những chức năng mang tính chất cá nhân của người sử dụng đó
        /// </summary>
        internal static string RibbonMenu = "";

        /// <summary>Option - Danh sách các item sẽ được gắn vào Quick Access Menu, đặt trước các item mặc định
        /// - Chú ý : - Thường đặt vào đây những chức năng thường dùng và muốn truy xuất nhanh
        /// </summary>
        internal static string QuickAccessMenu = "";
        
        
        /// <summary>Option - Chọn dạng menu bắt buộc của hệ thống là gì. Nếu có nhu cầu khác thì liên lạc PHUOCNT.
        /// Chú ý : - Hiện tại việc chọn cấu hình này đặt vào hàm dựng của lớp kế thừa từ ProtocolMenu
        ///         - Giá trị mặc định của nó là FWMenu.FW21x
        /// </summary>
        public static FWMenu fwMenu = FWMenu.FW21x;

        #endregion

        /// <summary>Option - Hệ quản trị CSDL sẽ sử dụng
        /// - Chú ý : - Mặc định là Firebird
        ///           - Hệ quản trị SQL Server chưa làm việc.
        /// </summary>
        public static WinRDBMS dbServer;

        #region Xử lý hệ thống report
        /// <summary>Mandatory - Danh sách các báo cáo sẽ hiện thị trong hệ thống báo cáo
        /// - Chú ý : Mọi chương trình đều có 1 hệ thống báo cáo chuẩn. Nếu hệ thống báo cáo chuẩn ko đủ 
        ///           mới bổ sung các báo các khác.
        /// </summary>
        public static string Report = @"<?xml version='1.0' encoding='utf-8' standalone='yes'?>
                                        <report></report>";

        /// <summary>Option - Cho phép tinh chỉnh tiêu đề cột, kích thước cột của báo cáo -> để việc in được đẹp hơn
        /// - Chú ý : - Phải tạo 1 lớp mở rộng ICustomReport
        ///           - Nếu có nhu cầu xin liên hệ PHUOCNT
        ///           - Phạm vi toàn ứng dụng
        /// </summary>
        public static ICustomReport isCustomReport = null;
        
        /// <summary>Option - Cho phép xem nội dung đang có trên report từ đây có thể copy ra excel và có thể click nếu cần thiết.
        /// - Chú ý: - Phải tạo 1 lớp mở rộng từ IDataReport
        ///          - Nếu có nhu cầu xin lien hệ PHUOCNT
        ///          - Phạm vi toàn ứng dụng
        /// </summary>
        public static IDataReport isDataReport = null;

        /// <summary>Option - Cho phép fax trên report hay không.
        /// - Chú ý: - Phải biết cách cài đặt mới có thể FAX qua phần mềm
        ///          - Trị mặc định = FALSE
        /// </summary>
        internal static bool isFax = false;
        #endregion

        #endregion

        /// <summary>Option - Xây dựng lớp mở rộng từ IEMBImageStore để có thể ẩn các Image trong EXE.
        /// Chú ý: - Nên copy từ 1 lớp có sẵn, đổi tên, cập nhật danh sách các hình.
        /// </summary>
        public static IEMBImageStore imageStore = null;

        /// <summary>After - Option - Cho phép nhấn Enter để đi đến control kế tiếp 
        /// Chú ý: - Nó chỉ apply cho các form XtraFormPL
        ///        - Giá trị mặc định là False.
        /// </summary>
        public static bool isEnterNextCtrl = false;

        /// <summary>Option - Khắc phục lỗi in nhiều cỡ giấy mà nó không thực hiện được 
        /// - Chú ý: ??? Kiểm tra lại trong trường hợp DP
        /// </summary>
        public static bool isPrintCustom = false;

        /// <summary>Option - Hỗ trợ hay không hỗ trợ dùng DB nhúng
        /// </summary>
        internal static bool supportEMB = true;

        #region Hằng số
        /// <summary>NoUsing
        /// </summary>
        public static string LICENCE_FILE;
        /// <summary>NoUsing
        /// </summary>
        public static string TEMP_FOLDER;
        /// <summary>NoUsing
        /// </summary>
        public static string CONF_FOLDER;
        /// <summary>NoUsing
        /// </summary>
        public static string LAYOUT_FOLDER;
        /// <summary>NoUsing
        /// </summary>
        public static string TEMPLETE_FOLDER;
        #endregion

        
        
        #region Tham số bắt buộc cấu hình theo dự án
        /// <summary>Mandatory - Hệ thống danh mục
        /// Chú ý: - Trên 1 ứng dụng phải có ít nhất 1 hệ thống danh mục
        ///        - Nếu có nhiều hệ thống danh mục thì mới cấu hình danh mục riêng
        /// </summary>
        public static string Category = @"<?xml version='1.0' encoding='utf-8' standalone='yes'?>
            <basiccats>
                <group id ='1'>
                    <lang id='vn'>Danh mục</lang>
                </group>
            </basiccats>
        ";

        
        #endregion 

        #region Xử lý phân quyền cho FW 
        /// <summary>After Mandatory - Định nghĩa cách phân quyền trên toàn bộ sản phẩm.
        /// </summary>
        public static IPermission isPermision = null;        
        #endregion

        /// <summary>Option - Định nghĩa danh sách các Assembly sử dụng trong ứng dụng
        /// </summary>
        public static List<Assembly> plAssemblies = new List<Assembly>();

        #region Tham số tùy chọn theo dự án mới
        /// <summary>Option - Tùy biến Icon của sản phẩm
        /// </summary>
        public static Icon ApplicationIcon;

        /// <summary>Mandatory - Định nghĩa các tham số của ứng dụng.
        /// </summary>
        public static CustomProject Custom;
                
        /// <summary>Option - Hiển thị dạng window hay dạng Tab
        /// </summary>
        [Obsolete("Không sử dụng")]
        internal static bool isTabWindow = true;

        /// <summary>Option - việc định dạng đã được apply vào form sử dụng ko định nghĩa trên từng form nữa
        /// </summary>
        [Obsolete("Không sử dụng")]
        public static IFormat isFormat = null;

        /// <summary>After Option - Định nghĩa nội dung cần LOG nó sẽ cung cấp thông tin cho phần theo dõi nhật ký sử dụng
        /// </summary>
        public static IPLLog isLog = null;        
        
        /// <summary>Option - màn hình nền cho ứng dụng 
        /// Chú ý : - Khi xây dựng màn hình này nên copy từ màn hình desktop có sẵn.
        /// </summary>
        public static string desktopForm;
        
        /// <summary>NoUsing: Chưa cài đặt chức năng này - Tùy biến màn hình Splash.
        /// </summary>
        public static DevExpress.XtraEditors.XtraForm SplashForm;


        

        /// <summary>Option - Hỗ trợ Menu Right Click trên form.
        /// Chú ý: - Hạn chế là nó chỉ apply vào những chỗ trống trên form còn nếu có component nó bị
        ///          hành động right click trên component chiếm giữ.
        /// </summary>
        [Obsolete("Không sử dụng")]
        internal static bool UsingRightClickForm = false;

        /// <summary>Option - Thông tin có trên statusbar
        /// </summary>
        public static FWStatusBar statusBar;

        /// <summary>Option - Current Skin 
        /// </summary>
        internal static DevExpressSkin currentSkin;

        #region Màn hình chờ dạng text
        /// <summary>Option - Màn hình chờ 
        /// </summary>
        public static WaitingMsg wait = null;
        internal static bool FixWaitingForm = true;//After
        #endregion

        /// <summary>Before - Option - Đăng nhập tự động
        /// - Chú ý: Giá trị mặc định là false
        /// </summary>
        internal static bool isSkipLogin = false;

        /// <summary>Option - Kiểm tra đăng nhập bằng LDAP.
        /// </summary>
        public static bool UsingLDAP = false;

        /// <summary>Before Option - Hỗ trợ phím nóng trên toàn ứng dụng 
        /// Chú ý: - Crl - F10: xem danh sách lỗi.
        ///        - Mặc định là TRUE.
        /// </summary>
        internal static bool IsHotKey = true;
        
        #endregion

        #region Phần không thay đổi
        
        public enum EXIT_STATUS
        {
            NORMAL_NO_THANKS,
            NORMAL_THANKS,
            ERROR
        }

        private static LOCKBOOL ExitFlag = new LOCKBOOL(false);

        [Obsolete("Sử dụng HelpApplication.ExitApplication")]
        public static void ExitApplication(EXIT_STATUS status)
        {
            HelpApplication.ExitApplication(status);
//            if(FrameworkParams.MainForm!=null && FrameworkParams.MainForm.IsDisposed == false)
//            {
//                if (FrameworkParams.MainForm.MdiChildren.Length > 0)
//                {
//                    for (int i = 0; i < FrameworkParams.MainForm.MdiChildren.Length; )
//                    {
//                        FrameworkParams.MainForm.MdiChildren[i].Dispose();
//                        //FrameworkParams.MainForm.MdiChildren[i].Close();
//                    }
//                }

//                try
//                {
//                    FrameworkParams.MainForm.Hide();
//                    FrameworkParams.MainForm.Dispose();
//                }
//                catch { }
//            }

//            if (status == EXIT_STATUS.NORMAL_THANKS)
//            {
////                if (IsThanksMsg)
////                {
////                    PLMessageBoxWin box = new PLMessageBoxWin(@"
////                        Cám ơn bạn đã sử dụng sản phẩm của công ty ProtocolVN.\n
////                        Khi gặp sự cố sử dụng sản phẩm xin vui lòng liên hệ với ProtocolVN");
////                }
//            }
//            else if(status == EXIT_STATUS.NORMAL_NO_THANKS)
//            {
                
//            }
//            else if (status == EXIT_STATUS.ERROR)
//            {
//                PLMessageBox box = PLMessageBox.GetSystemErrorMessage(@"Cám ơn bạn đã sử dụng sản phẩm của công ty ProtocolVN. Alt-F10 Xem thông thêm.");
//                PLKey key = new PLKey(box);
//                key.Add(Keys.Alt | Keys.F10, delegate(){
//                    PLDebug.ShowExceptionInfo();
//                });

//                box.ShowDialog();
//            }

//            ExitApplication();
        }

        [Obsolete("Sử dụng HelpApplication.ExitApplication")]
        public static void ExitApplication()//Hàm để thoát khởi ứng dụng
        {
            HelpApplication.ExitApplication();
            //if (FrameworkParams.MainForm != null && FrameworkParams.MainForm.IsDisposed == false)
            //{
            //    FrameworkParams.MainForm.Hide();
            //    FrameworkParams.MainForm.Dispose();
            //}
            //try
            //{
            //    lock (ExitFlag)
            //    {
            //        if (ExitFlag.Value == false)
            //        {
            //            //Close all Output
            //            SystemTrayOut.Dispose();
            //            RibbonStatusOut.Dispose();
            //            //Stop Stickies
            //            ProtocolVN.Plugin.NoteBook.StickiesMethodExec.StopStickies();
            //            //Class Microsoft Word
            //            try{
            //                if (PLMicrosoftWord.wd != null)
            //                {
            //                    object dummy = null;
            //                    object dummy2 = (object)false;
            //                    PLMicrosoftWord.wd.Quit(ref dummy2, ref dummy, ref dummy);
            //                }
            //            }catch{}
            //            //Cập nhật thông tin của Licence
            //            if (FrameworkParams.Lic != null)
            //                ((ILicence)FrameworkParams.Lic).updateReleaseLicence("NOOP");
            //            Custom.exitApplication();
            //            ExitFlag.Value = true;
            //        }
            //    }
            //}
            //catch (Exception ex){
            //    PLException pl = new PLException(ex, "", "", "", "Lỗi thoát khỏi ứng dụng");
            //    PLException.AddException(pl);

            //    lock (ExitFlag)
            //    {
            //        Custom.exitApplication();
            //        ExitFlag.Value = true;
            //    }
            //}
        }
        
        private static System.Reflection.Assembly getAssemblyHelp()
        {
            return System.Reflection.Assembly.GetCallingAssembly();
        }
        public static System.Reflection.Assembly getAssembly()
        {
            return getAssemblyHelp();
        }
        #endregion

        #region Phần tham số cho việc sử dụng Framework Form
        /// <summary>Mandatory Định nghĩa lớp cấu hình các màn hình chức năng
        /// - Chú ý : Như hệ thống danh mục, ...
        /// </summary>
        public static string defineAppParamExec = typeof(FWMethodExec).FullName;
        #endregion

        

        //After - Cho phép nhúm VietKey.
        internal static bool IsEmbededVietkey = false;//Hỗ trợ bổ gõ nhúng bằng phím tắt hoặc không hỗ trợ. | Mặc định là ON

        //Before - Kiểm tra lic trước khi đăng nhập.
        internal static bool IsBeforeLogin = false;

        #region Liên quan đến việc xuất report của grid
        /// <summary>After
        /// </summary>
        public static IHeaderStartTitleGridEndFooter headerLetter;
        #endregion

        #region Khanhdtn
        public static long LoginCompanyID = 1;
        public static CompanyInfo LoginCompanyInfo;
        public static DOServer ServerConfig;
       
        #endregion
    }
}