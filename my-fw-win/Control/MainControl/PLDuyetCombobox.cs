using DevExpress.XtraEditors.Repository;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    /// <summary>Dùng để hiện thị tình trạng DUYET bằng combobox trên form và trên lưới
    ///     DOPhieu bắt buộc có các Property sau:
    ///         DUYET : 1: Chưa duyệt ; 2 : Duyệt ; 3 : Không Duyệt
    ///         NGUOI_DUYET : Người đăng nhập
    ///         NGAY_DUYET : Ngày server
    /// </summary>
    public partial class PLDuyetCombobox : DevExpress.XtraEditors.XtraUserControl
    {
        public PLDuyetCombobox()
        {
            InitializeComponent();            
        }

        private bool IsChoDuyet;

        public void _init()
        {
            _init(false);
        }

        public void _init(bool IsChoDuyet){
            this.IsChoDuyet = IsChoDuyet;

            FWImageDic.GET_DUYET_STATUS16(this.imageCollection1);
            if (IsChoDuyet)
            {
                this.imageComboBoxEdit1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
                    new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Chờ duyệt", 1, 0),
                    new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Duyệt", 2, 1),
                    new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Không duyệt", 3, 2)});
            }
            else
            {
                this.imageComboBoxEdit1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
                    new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Duyệt", 2, 1),
                    new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Không duyệt", 3, 2)});
            }
                       
        }

        /// <summary>Đặt tình trạng duyệt của Phiếu vào Control
        /// </summary>
        public void SetDuyet(DOPhieu dophieu)
        {
            ////Dùng Property
            

            //Dùng thuộc tính
            try
            {
                //Cách mới
                if (this.IsChoDuyet)
                    imageComboBoxEdit1.SelectedIndex = HelpNumber.ParseInt32(dophieu.GetType().GetField("DUYET").GetValue(dophieu).ToString()) - 1;
                else
                    imageComboBoxEdit1.SelectedIndex = HelpNumber.ParseInt32(dophieu.GetType().GetField("DUYET").GetValue(dophieu).ToString()) - 2;
                
            }
            catch
            {
                if (this.IsChoDuyet)
                    imageComboBoxEdit1.SelectedIndex = HelpNumber.ParseInt32(dophieu.GetType().GetProperty("DUYET").GetValue(dophieu, null).ToString()) - 1;
                else
                    imageComboBoxEdit1.SelectedIndex = HelpNumber.ParseInt32(dophieu.GetType().GetProperty("DUYET").GetValue(dophieu, null).ToString()) - 2;
            }
        }
        
        /// <summary>Đặt thông tin tình trạng duyệt của phiếu vào doPhieu
        /// 
        /// </summary>
        public void GetDuyet(DOPhieu dophieu)
        {           
            string []tempDuyet = new string[] { "DUYET", "NGAY_DUYET", "NGUOI_DUYET" };
            string id = imageComboBoxEdit1.EditValue.ToString();            
            //set DUYET
            try
            {
                dophieu.GetType().GetField(tempDuyet[0]).SetValue(dophieu, imageComboBoxEdit1.EditValue.ToString());
                //set NGAY_DUYET
                //set NGUOI_DUYET                    
                if (id == "1") // chờ duyệt
                {
                    dophieu.GetType().GetField(tempDuyet[1]).SetValue(dophieu, (new RepositoryItemDateEdit()).NullDate);
                    dophieu.GetType().GetField(tempDuyet[2]).SetValue(dophieu, -1);
                }
                if (id == "2" || id == "3") //duyệt, không duyệt 
                {
                    dophieu.GetType().GetField(tempDuyet[1]).SetValue(dophieu, DABase.getDatabase().GetSystemCurrentDateTime());
                    dophieu.GetType().GetField(tempDuyet[2]).SetValue(dophieu, FrameworkParams.currentUser.employee_id);
                }
            }
            catch
            {
                dophieu.GetType().GetProperty(tempDuyet[0]).SetValue(dophieu, imageComboBoxEdit1.EditValue.ToString(), null);            
                //set NGAY_DUYET
                //set NGUOI_DUYET                    
                if (id == "1") // chờ duyệt
                {
                    dophieu.GetType().GetProperty(tempDuyet[1]).SetValue(dophieu, (new RepositoryItemDateEdit()).NullDate, null);
                    dophieu.GetType().GetProperty(tempDuyet[2]).SetValue(dophieu,-1 , null);
                }
                if (id == "2" || id == "3") //duyệt, không duyệt 
                {
                    dophieu.GetType().GetProperty(tempDuyet[1]).SetValue(dophieu, DABase.getDatabase().GetSystemCurrentDateTime(), null);
                    dophieu.GetType().GetProperty(tempDuyet[2]).SetValue(dophieu, FrameworkParams.currentUser.employee_id, null);
                }
            }
        }
    }
}
