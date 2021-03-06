using System;
using ProtocolVN.Framework.Win;
using System.Windows.Forms;
using System.Text;
using ProtocolVN.Framework.Core;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    public partial class frmFWPromotionDesktop : XtraForm, IPublicForm
    {
        DXErrorProvider err = null;
        public frmFWPromotionDesktop()
        {
            InitializeComponent();
            //this.panel1.Visible = false;
            this.Load += new EventHandler(frmPromotionDesktop_Load);            
            err = HelpInputData.GetErrorProvider(this);
            this.Tag = typeof(frmFWPromotionDesktop).FullName;
        }

        void frmPromotionDesktop_Load(object sender, EventArgs e)
        {
            
            if (InternetConn.IsConnected() == false)
            {
                this.webBrowser1.Visible = false;
                this.pictureEdit1.Visible = true;
                this.pictureEdit1.Dock = System.Windows.Forms.DockStyle.Fill;

                //this.panel1.Visible = true;
                //this.panel1.Dock = DockStyle.Fill;
                //this.pictureEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            }
            else
            {
                try
                {
                    this.webBrowser1.Visible = true;
                    this.webBrowser1.Dock = DockStyle.Fill;
                    this.pictureEdit1.Visible = false;

                    this.webBrowser1.Navigate("http://www.protocolvn.com/hot.php");
                    //this.webBrowser1.Navigate("http://www.protocolvn.com");
                    this.webBrowser1.Navigated += new WebBrowserNavigatedEventHandler(wbs_Navigated);
                }
                catch
                {

                }
                finally
                {

                }
            }
        }

        void wbs_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (sender is WebBrowser )
            {
                String text = ((WebBrowser)sender).DocumentText;
                if (text.Contains("404 Not Found") || text.Contains("Cannot find server"))
                {
                    this.webBrowser1.Visible = false;
                    this.pictureEdit1.Visible = true;
                    this.pictureEdit1.Dock = System.Windows.Forms.DockStyle.Fill;

                    //this.panel1.Visible = true;
                    //this.panel1.Dock = DockStyle.Fill;
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //Xử lý dữ liệu
            HelpInputData.TrimAllData(new object[]{
                this.nguoiLienHe, 
                this.dienThoai,
                this.txtEmail,
                this.diaChi,
                this.tenDonVi,
                this.boPhan,
                this.chucVu,
                this.noiDungYeuCau
            });

            //Kiểm tra tính hợp lệ
            if (HelpInputData.ShowRequiredError(err, new object[]
            {
                nguoiLienHe, "Người liên hệ",
                dienThoai, "Số điện thoại",
                txtEmail, "Email",
                noiDungYeuCau, "Nội dung"
            }))
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("Người liên hệ: " + this.nguoiLienHe.Text);
                builder.AppendLine("Điện thoại: " + this.dienThoai.Text);
                builder.AppendLine("Email: " + this.txtEmail.Text);
                //builder.AppendLine("Địa chỉ: " + this.diaChi.Text);
                builder.AppendLine("Tên đơn vị: " + this.tenDonVi.Text);
                builder.AppendLine("Bộ phận: " + this.boPhan.Text);
                builder.AppendLine("Chức vụ: " + this.chucVu.Text);
                builder.AppendLine("Nội dung: " + this.noiDungYeuCau.Text);
                try
                {
                    String[] To = new String[1];
                    To[0] = "support@protocolvn.com";
                    HelpEmail.sendFromPLEmail(To, "[Phan hoi - Gop y] " + this.nguoiLienHe.Text, builder.ToString());
                    HelpMsgBox.ShowNotificationMessage("Thông tin này đã gửi đến PROTOCOL.\n Chân thành cám ơn.");
                    //Reset lại form.
                    this.nguoiLienHe.Text = "";
                    this.dienThoai.Text = "";
                    this.txtEmail.Text = "";
                    this.diaChi.Text = "";
                    this.tenDonVi.Text = "";
                    this.boPhan.Text = "";
                    this.chucVu.Text = "";
                    this.noiDungYeuCau.Text = "";
                }
                catch (Exception ex)
                {
                    PLException.AddException(ex);
                    HelpMsgBox.ShowNotificationMessage("Có lỗi trong quá trình gửi thông tin.\n Vui lòng kiểm tra lại kết nối Internet.");
                }
            }

        }

        private void email_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            try
            {
                string EmailAddress = "sales@protocolvn.com";
                System.Diagnostics.Process.Start(String.Format("mailto:{0}", EmailAddress));
            }
            catch { }
        }

        private void kt_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("ymsgr:sendIM?kt.protocol");
            }
            catch { }
        }

        private void kd_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("ymsgr:sendIM?kd.protocol");
            }
            catch { }
        }
    }
}