using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public partial class frmGopY : XtraFormPL, IPublicForm
    {
        public frmGopY()
        {
            InitializeComponent();
        }

        private void BtnGui_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnGui_Click(object sender, EventArgs e)
        {
            try
            {
                String[] To = new String[5];
                To[0] = "info@protocolvn.com";
                To[1] = "sales@protocolvn.com";
                To[2] = "tech@protocolvn.com";
                To[3] = "hr@protocolvn.com";
                To[4] = HelpEmail.DEFAULT_RECEIVER_EMAIL;

                HelpEmail.sendFromPLEmail(To, TxtHoTenGopY.Text.Trim(), getNoiDung());
                HelpMsgBox.ShowNotificationMessage("Cám ơn bạn đã gửi thông tin góp ý. Công ty chúng tôi sẽ liên lạc với bạn trong vòng 24 giờ.");
            }
            catch(Exception ex) {
                PLException.AddException(ex);
                HelpMsgBox.ShowNotificationMessage("Gửi không thành công. Xin vui lòng kiểm tra lại thông tin."); 
            }                        
            
            this.Close(); 
        }       

        private string getNoiDung() {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Tên đơn vị/công ty: " + this.TxtTenDonVi.Text.Trim()  );
            builder.AppendLine("    Địa chỉ: " + this.TxtDiaChi.Text.Trim() );
            builder.AppendLine("    Điện thoại: " + TxtDienThoai.Text.Trim()  );
            builder.AppendLine("    Email: " + this.TxtEmail.Text.Trim());
            builder.AppendLine("    Mã số thuế: " + TxtMaSoThue.Text.Trim());
            builder.AppendLine("    Website: " + this.TxtWebsite.Text.Trim());
            builder.AppendLine("    Fax: " + TxtFax.Text.Trim() );
            builder.AppendLine("   ");
            builder.AppendLine("Giám đốc:");
            builder.AppendLine("    Họ và tên: " + this.TxtTenGD.Text.Trim() );
            builder.AppendLine("    Di động: " + TxtDTGD.Text.Trim());
            builder.AppendLine("Kế toán trưởng:");
            builder.AppendLine("    Họ và tên: " + this.TxtTenKT.Text.Trim());
            builder.AppendLine("    Di động: " + TxtDTKT.Text.Trim());
            builder.AppendLine("    ");
            builder.AppendLine("Thông tin yêu cầu(góp ý):");
            builder.AppendLine("    Họ và tên: " + this.TxtHoTenGopY.Text.Trim());
            builder.AppendLine("    Điện thoại: " + TxtDTGoyY.Text.Trim() );
            builder.AppendLine("    Email: " + TxtEmailGopY.Text.Trim());
            builder.AppendLine("Nội dung yêu cầu:");
            builder.Append("    "+ MemoThongTinThem.Text);
            return builder.ToString();
        }


        #region Phần này gửi bằng HTML chưa cần thiết
        
        //private string getNoiDungHTML()
        //{
        //    StringBuilder builder = new StringBuilder(); 
        //    builder.AppendLine("Tên đơn vị/công ty:<span style='font-size: 13px;color: blue;'> " + this.TxtTenDonVi.Text.Trim() + "</span><br>");
        //    builder.AppendLine("    Địa chỉ: " + this.TxtDiaChi.Text.Trim()+ "<br>");
        //    builder.AppendLine("    Điện thoại: " + TxtDienThoai.Text.Trim() + "<br>");
        //    builder.AppendLine("    Email: " + this.TxtEmail.Text.Trim() + "<br>");
        //    builder.AppendLine("    Mã số thuế: " + TxtMaSoThue.Text.Trim() + "<br>");
        //    builder.AppendLine("    Website: " + this.TxtWebsite.Text.Trim() + "<br>");
        //    builder.AppendLine("    Fax: " + TxtFax.Text.Trim() + "<br>");
        //    builder.AppendLine("   <br>");
        //    builder.AppendLine("Giám đốc:<br>");
        //    builder.AppendLine("    Họ và tên: " + this.TxtTenGD.Text.Trim() + "<br>");
        //    builder.AppendLine("    Di động: " + TxtDTGD.Text.Trim() + "<br>");
        //    builder.AppendLine("Kế toán trưởng:<br>");
        //    builder.AppendLine("    Họ và tên: " + this.TxtTenKT.Text.Trim() + "<br>");
        //    builder.AppendLine("    Di động: " + TxtDTKT.Text.Trim() + "<br>");
        //    builder.AppendLine("    <br>");
        //    builder.AppendLine("Thông tin yêu cầu(góp ý):<br>");
        //    builder.AppendLine("    Họ và tên: " + this.TxtHoTenGopY.Text.Trim() + "<br>");
        //    builder.AppendLine("    Điện thoại: " + TxtDTGoyY.Text.Trim() + "<br>");
        //    builder.AppendLine("    Email: " + TxtEmailGopY.Text.Trim() + "<br>");
        //    builder.AppendLine("   <br>");
        //    builder.AppendLine("Nội dung yêu cầu:<br>");
        //    builder.Append("    <span style='font-size: 13px;color: red;'>" + MemoThongTinThem.Text + "</span><br>");
        //    return builder.ToString();
        //}
        
        //public void SendEmail(string from,string namefrom, string subject, string body)
        //{
        //    MailMessage msg = new MailMessage();
        //    msg.From = new MailAddress(from, namefrom, System.Text.Encoding.UTF8);
        //    msg.To.Add(new MailAddress("info@protocolvn.com", "Hành chính", Encoding.UTF8)); 
        //    msg.CC.Add(new MailAddress("sales@protocolvn.com ", "Kinh doanh", Encoding.UTF8));
        //    msg.CC.Add(new MailAddress("tech@protocolvn.com ", "Kỹ thuật", Encoding.UTF8));
        //    msg.CC.Add(new MailAddress("hr@protocolvn.com ", "Nhân sự", Encoding.UTF8));
        //    msg.Subject = subject;
        //    msg.Body = body;
        //    msg.Priority = MailPriority.High; 
        //    // su dung tieng viet cho subject va message
        //    msg.SubjectEncoding = Encoding.UTF8;
        //    msg.BodyEncoding = Encoding.UTF8;
        //    msg.IsBodyHtml = true;
        //    // giao thuc gui mail
        //    SmtpClient client = new SmtpClient();
        //    client.Credentials = new NetworkCredential("hosyduc.na@gmail.com", "15011984"); 
        //    client.Host = "smtp.gmail.com";
        //    client.Port = 587;     //bắt buộc phải là cổng 587  
        //    client.EnableSsl = true;  //server của gmail yêu cầu kết nối SSL (Secure Socket Layer) để bảo vệ thông tin đăng nhập  

        //    //guithuong
        //    // client.Send(msg);  

        //    // gui ko dong bo
        //    client.SendCompleted += new SendCompletedEventHandler(client_SendCompleted);           
        //    object userState = msg;
        //    client.SendAsync(msg, userState);
        //}
        
        //void client_SendCompleted(object sender, AsyncCompletedEventArgs e)
        //{
        //    if (e.Error != null)
        //        HelpMsgBox.ShowNotificationMessage("Đã có lỗi khi gửi yêu cầu. Cần xem lại máy có kết nối internet hay không?");
        //    else MessageBox.Show("Email đã được gửi đi thành công");
        //}

        #endregion

        
    }
}