using System;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ProtocolVN.Framework.Core;
using System.Drawing;
namespace ProtocolVN.Framework.Win
{
    public partial class frmCompanyInfo : XtraFormPL
    {
        private Option configOption;
        private CompanyInfo company;

        public frmCompanyInfo()
        {           
            InitializeComponent();
            //btnSave.Image = FWImageDic.SAVE_IMAGE16;
            //btnClose.Image = FWImageDic.CLOSE_IMAGE16;

            object obj = this.btnSave.Tag;
            TagPropertyMan.InsertOrUpdate(ref obj, "SECURITY", new PermissionItem("ProtocolVN.Framework.Win.frmOption", PermissionType.EDIT));
            this.btnSave.Tag = obj;
            initData();

            //PLKey k = new PLKey(this);
            //k.Add(Keys.F12, InputImage);
        }
       
        private void InputImage(){
            this.pictureEdit1.Properties.ReadOnly = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            getData();
            company.update();
            configOption.update();
            ApplyFormatAction.Culture = null;
            Application.CurrentCulture = ApplyFormatAction.GetCultureInfo();
            FrameworkParams.currentSkin.SelectSkin(HelpNumber.ParseInt32(configOption.Skin));
            this.Close();
        }

        private void pictureEdit1_DoubleClick(object sender, EventArgs e)
        {
            DialogResult dialogResult= this.openFileDialog1.ShowDialog();
            if (dialogResult==DialogResult.Cancel||openFileDialog1.FileName == "")
                return;
            try{
                byte[] logoByte = CompanyInfo.readBitmap2ByteArray(openFileDialog1.FileName);
                company.logo = logoByte;
                this.pictureEdit1.Image = CompanyInfo.displayImageLogo(logoByte);
                HelpImage.LoadImage(this.pictureEdit1, logoByte);
            }catch (Exception ex){
                PLException.AddException(ex);
                FWMsgBox.showErrorImage();
            }
        }

        public void initData()
        {
            company = new CompanyInfo();
            company.load();
            this.txtCompanyName.Text = company.name;
            this.txtTradeName.Text = company.tradeName;
            this.txtRepresentative.Text = company.representative;
            this.mmeAddress.Text = company.address;
            this.txtPhone.Text = company.phone;
            this.txtFax.Text = company.fax;
            this.txtEmail.Text = company.email;
            this.txtWebsite.Text = company.website;
            this.txtAccountNo.Text = company.accountNo;
            this.txtBankName.Text = company.bankName;
            this.txtTaxCode.Text = company.taxCode;
            this.pictureEdit1.Image = CompanyInfo.displayImageLogo(company.logo);
            this.headerLetter.Image = CompanyInfo.displayImageLogo(company.headerletter);
            HelpImage.LoadImage(this.pictureEdit1, company.logo);
            HelpImage.LoadImage(this.headerLetter, company.headerletter);
            if (FrameworkParams.option==null){
                FrameworkParams.option = new Option();
                FrameworkParams.option.load();
            }
            configOption = FrameworkParams.option;
        }

        public void trimAllData()
        {
            GUIValidation.TrimAllData(
                new object[]{
                    this.txtCompanyName,
                    this.txtTradeName,
                    this.txtRepresentative,
                    this.mmeAddress,
                    this.txtPhone,
                    this.txtFax,
                    this.txtEmail,
                    this.txtWebsite,
                    this.txtAccountNo,
                    this.txtBankName,
                    this.txtTaxCode
                }
            );
        }

        public void getData()
        {
            trimAllData();
            company.name = txtCompanyName.Text;
            company.tradeName = txtTradeName.Text;
            company.representative = txtRepresentative.Text;
            company.address = mmeAddress.Text;
            company.phone = txtPhone.Text;
            company.fax = txtFax.Text;
            company.email = txtEmail.Text;
            company.website = txtWebsite.Text;
            company.accountNo = txtAccountNo.Text;
            company.bankName = txtBankName.Text;
            company.taxCode = txtTaxCode.Text;
            //try{ company.logo = (byte[])pictureEdit1.EditValue; } catch { }
            //try { company.headerletter = (byte[])headerLetter.EditValue; }
            //catch { }
            company.logo = HelpByte.ImageToByteArray((Image)pictureEdit1.EditValue);
            company.headerletter = HelpByte.ImageToByteArray((Image)headerLetter.EditValue);

            configOption.numFormat = "";
            configOption.dateTimeFormat = "";
        }

        private void frmOption_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            ProtocolForm.ShowModalDialog(this, new frmConfigFTP());
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                Directory.Delete(FrameworkParams.TEMP_FOLDER, true);
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
        }
    }
}