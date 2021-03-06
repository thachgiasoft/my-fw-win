using System;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ProtocolVN.Framework.Core;
namespace ProtocolVN.Framework.Win
{
    //Màn hình này đã được thay thế bằng frmXPOption
    [Obsolete("Không sử dụng")]
    public partial class frmOption : XtraForm
    {
        private Option configOption;
        private CompanyInfo company;                

        public frmOption()
        {           
            InitializeComponent();
            btnSave.Image = FWImageDic.SAVE_IMAGE16;
            btnClose.Image = FWImageDic.CLOSE_IMAGE16;

            object obj = this.btnSave.Tag;
            TagPropertyMan.InsertOrUpdate(ref obj, "SECURITY", new PermissionItem("ProtocolVN.Framework.Win.frmOption", PermissionType.EDIT));
            this.btnSave.Tag = obj;
            loadPrinters();
            initData();            
        }

        private void loadPrinters()
        {
            //khong cho sua text
            cbInstalledPrinters.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;

            PopulateInstalledPrintersCombo();
            if (cbInstalledPrinters.Properties.Items.Count > -1)
            {
                // The combo box's Text property returns the selected item's text, which is the printer name.
                cbInstalledPrinters.SelectedIndex = 0;
            }
        }

        private void PopulateInstalledPrintersCombo()
        {
            // Add list of installed printers found to the combo box.
            // The pkInstalledPrinters string will be used to provide the display string.
            String pkInstalledPrinters;
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                cbInstalledPrinters.Properties.Items.Add(pkInstalledPrinters);
            }
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
                //this.pictureEdit1.Image = CompanyInfo.displayImageLogo(logoByte);
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
            //this.pictureEdit1.Image = CompanyInfo.displayImageLogo(company.logo);
            HelpImage.LoadImage(this.pictureEdit1, company.logo);
            if (FrameworkParams.option==null){
                FrameworkParams.option = new Option();
                FrameworkParams.option.load();
            }
            configOption = FrameworkParams.option;

            seRound.Value = HelpNumber.ParseInt32(configOption.round);
            rgSperactorThousand.SelectedIndex = (configOption.thousandSeparator.Equals(".") ? 0 : 1);
            rgSperactorDec.SelectedIndex = (configOption.decSeparator.Equals(".") ? 1 : 0);
            cbFormatDay.EditValue = configOption.dateFormat;
            cbFormatHour.EditValue = configOption.timeFormat;
            cbSkin.SelectedIndex = HelpNumber.ParseInt32(configOption.Skin);

            for (int i = 0; i < cbInstalledPrinters.Properties.Items.Count ; i++)
			{
                if (cbInstalledPrinters.Properties.Items[i].ToString() == configOption.printerName)
                    cbInstalledPrinters.SelectedItem = configOption.printerName;
                else
                {
                    //cbInstalledPrinters.Text = "";
                }
			}      
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

            configOption.numFormat = "";
            configOption.dateTimeFormat = "";
            configOption.round = ""+(HelpNumber.ParseInt32(seRound.Value.ToString())>5?0:HelpNumber.ParseInt32(seRound.Value.ToString()));
            configOption.thousandSeparator = rgSperactorThousand.Properties.Items[rgSperactorThousand.SelectedIndex].Description;
            configOption.decSeparator = rgSperactorDec.Properties.Items[rgSperactorDec.SelectedIndex].Description;
            configOption.dateFormat = cbFormatDay.EditValue.ToString();
            configOption.timeFormat = cbFormatHour.EditValue.ToString();
            configOption.Skin = cbSkin.SelectedIndex.ToString();
            configOption.printerName = cbInstalledPrinters.Text;
        }

        private void rgSperactorThousand_SelectedIndexChanged(object sender, EventArgs e)
        {
            rgSperactorDec.SelectedIndex = rgSperactorThousand.SelectedIndex;
        }

        private void rgSperactorDec_SelectedIndexChanged(object sender, EventArgs e)
        {
            rgSperactorThousand.SelectedIndex = rgSperactorDec.SelectedIndex;
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