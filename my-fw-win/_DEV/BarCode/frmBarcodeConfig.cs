using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
using System.Text;

namespace ProtocolVN.Framework.Win
{
    public partial class frmBarcodeConfig : XtraFormPL
    {       
        private DOBarcodeOption configOption;        
        private DevExpress.XtraPrinting.Control.PrintControl printControl;
        private ProductBarcode bc;

        public frmBarcodeConfig()
        {
            InitializeComponent();
            teCountry.Properties.Mask.EditMask = @"(\d{2}|\d{3})";
            teCountry.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            teProvider.Properties.Mask.EditMask = @"(\d{4}|\d{5})";
            teProvider.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.cbBCStyle.Properties.Items.Add(BarCodeType.EAN8);
            this.cbBCStyle.Properties.Items.Add(BarCodeType.CODE25_INDUSTRIAL);
            this.cbBCStyle.Properties.Items.Add(BarCodeType.CODE25_INTERLEAVED);
            this.cbBCStyle.Properties.Items.Add(BarCodeType.CODE_39);
            this.cbBCStyle.Properties.Items.Add(BarCodeType.CODE_39_EXT);
            this.cbBCStyle.Properties.Items.Add(BarCodeType.CODE_93);
            this.cbBCStyle.Properties.Items.Add(BarCodeType.CODE_93_EXT);
            this.cbBCStyle.Properties.Items.Add(BarCodeType.CODABAR);
            //this.cbBCStyle.Properties.Items.Add(BarCodeType.EAN128);
            //this.cbBCStyle.Properties.Items.Add(BarCodeType.UPCA);
            //this.cbBCStyle.Properties.Items.Add(BarCodeType.PDF417);
            EAN13Chk.Checked = true;
            OtherChk.Checked = false;
            this.cbBCStyle.SelectedIndex = 0;
        }

        private void frmOption_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            initData();

            this.printControl = new DevExpress.XtraPrinting.Control.PrintControl();
            this.gcPreview.Controls.Add(printControl);
            this.printControl.Dock = DockStyle.Fill;

            bc = new ProductBarcode(null);
            GetBarcodeOption();
            DevExpress.XtraReports.UI.XtraReport xr = bc.GetReportStamp(this.tenDonVi.Text.Trim());
            printControl.PrintingSystem = xr.PrintingSystem;
            xr.PrintingSystem.ClearContent();
            xr.CreateDocument();

            printControl.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.ViewWholePage);
            //if (symbc == -1)
            //{
            //    EAN13Chk.Focus();
            //}
            //else
            //{
            //    OtherChk.Focus();
            //}
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (checkData())
            {
                getData();
                configOption.update();

                this.Close();
            }            
        }

        private bool checkData()
        {
            if (teProvider.Text.Length + teCountry.Text.Length != 7 && EAN13Chk.Checked == true)
                return false;
            return true;
        }
        //Lấy dữ liệu từ DB.
        private void initData()
        {
            configOption = DOBarcodeOption.load();            

            this.teCountry.Text = configOption.COUNTRY;
            this.teProvider.Text = configOption.PROVIDER;
            this.teIdProduct.Text = configOption.PRODUCT;
            
            this.seWidthStamp.Value = ((Decimal)configOption.STAMP_WIDTH)/100;
            this.seHeightStamp.Value = ((Decimal)configOption.STAMP_HEIGHT)/100;
            this.seWidthBC.Value = ((Decimal)configOption.BARCODE_WIDTH)/100;
            this.seHeightBC.Value = ((Decimal)configOption.BARCODE_HEIGHT)/100;

            this.seModule.Value = (Decimal)configOption.BARCODE_MODULE;
            this.seUnit.Value = configOption.UNIT_POS;
            this.seNameProduct.Value = configOption.NAME_POS;
            this.seBC.Value = configOption.BARCODE_POS;
            this.sePrice.Value = configOption.PRICE_POS;
            this.ceUnit.Checked = (configOption.UNIT_USING == "Y" ? true : false);
            this.ceNameProduct.Checked = (configOption.NAME_USING == "Y" ? true : false);
            this.ceBarcode.Checked = (configOption.BARCODE_USING == "Y" ? true : false);
            this.cePrice.Checked = (configOption.PRICE_USING == "Y" ? true : false);
            this.cbFUnit.SelectedIndex = configOption.UNIT_ALIGHT;
            this.cbFNameProduct.SelectedIndex = configOption.NAME_ALIGHT;
            this.cbFBC.SelectedIndex = configOption.BARCODE_ALIGHT;
            this.cbPrice.SelectedIndex = configOption.PRICE_ALIGHT;

            int symbc = configOption.SYM_BARCODE;

            if (symbc == -1)
            {
                this.EAN13Chk.Checked = true;
                cera1_CheckedChanged(null, null);
            }
            else
            {
                this.OtherChk.Checked = true;
                this.cbBCStyle.SelectedIndex = symbc;
                cera2_CheckedChanged(null, null);
            }

            this.numChar.EditValue = (Decimal)configOption.CHAR_NUMBER;
            this.tenDonVi.Text = configOption.BARCODE_PARAM;
        }        

        private void getData()
        {                        
            configOption.COUNTRY = this.teCountry.Text;
            configOption.PROVIDER = this.teProvider.Text;
            configOption.PRODUCT = this.teIdProduct.Text;
            
            configOption.STAMP_WIDTH = this.seWidthStamp.Value * 100;
            configOption.STAMP_HEIGHT = this.seHeightStamp.Value * 100;
            configOption.BARCODE_WIDTH = this.seWidthBC.Value * 100;
            configOption.BARCODE_HEIGHT = this.seHeightBC.Value * 100;
            
            configOption.BARCODE_MODULE = this.seModule.Value;
            configOption.UNIT_USING = (this.ceUnit.Checked == true)?"Y":"N";
            configOption.UNIT_POS = (int)this.seUnit.Value;
            configOption.UNIT_ALIGHT = this.cbFUnit.SelectedIndex;
            configOption.NAME_USING = (this.ceNameProduct.Checked == true) ? "Y" : "N";
            configOption.NAME_POS = (int)this.seNameProduct.Value;
            configOption.PRICE_ALIGHT = this.cbFNameProduct.SelectedIndex;
            configOption.BARCODE_USING = (this.ceBarcode.Checked == true) ? "Y" : "N";
            configOption.BARCODE_POS = (int)this.seBC.Value;
            configOption.BARCODE_ALIGHT = this.cbFBC.SelectedIndex;
            configOption.PRICE_USING = (this.cePrice.Checked == true) ? "Y" : "N";
            configOption.PRICE_POS = (int)this.sePrice.Value;
            configOption.PRICE_ALIGHT = this.cbPrice.SelectedIndex;

            if (this.EAN13Chk.Checked)
                configOption.SYM_BARCODE = -1;
            else
                configOption.SYM_BARCODE = this.cbBCStyle.SelectedIndex;
            configOption.BARCODE_PARAM = this.tenDonVi.Text;
            configOption.CHAR_NUMBER = (int)((decimal)this.numChar.EditValue);
        }                

        private void GetBarcodeOption()
        {
            if (this.EAN13Chk.Checked)
            {
                bc.symBC = BarCodeType.EAN13;
                
            }
            else
            {
                bc.symBC = (BarCodeType)this.cbBCStyle.SelectedItem;
            }
            
            //initMaVachTotNhatTheoLoaiMa();

            bc.mauEAN13 = this.MauEAN13.Text;
            bc.mauKhac = this.MauMaKhac.Text;

            bc.heightBarcode = (int)(this.seHeightBC.Value * 100);
            bc.widthBarcode = (int)(this.seWidthBC.Value * 100);

            bc.idCountry = this.teCountry.Text;
            bc.idProvider = this.teProvider.Text;

            bc.widthStamp = (int)(this.seWidthStamp.Value * 100);
            bc.heightStamp = (int)(this.seHeightStamp.Value * 100);
            bc.moduleBarcode = (float)this.seModule.Value;

            bc.pos = new StyleLabelBarcode[4];
            bc.alight = new DevExpress.XtraPrinting.TextAlignment[4];

            if (ceUnit.Checked)
            {
                bc.pos[(int)this.seUnit.Value - 1] = StyleLabelBarcode.Unit;

                if (this.cbFUnit.SelectedIndex == 0)
                    bc.alight[(int)this.seUnit.Value - 1] = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                else if (this.cbFUnit.SelectedIndex == 1)
                    bc.alight[(int)this.seUnit.Value - 1] = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                else if (this.cbFUnit.SelectedIndex == 2)
                    bc.alight[(int)this.seUnit.Value - 1] = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            }

            if (ceNameProduct.Checked)
            {
                bc.pos[(int)this.seNameProduct.Value - 1] = StyleLabelBarcode.Name;

                if (this.cbFNameProduct.SelectedIndex == 0)
                    bc.alight[(int)this.seNameProduct.Value - 1] = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                else if (this.cbFNameProduct.SelectedIndex == 1)
                    bc.alight[(int)this.seNameProduct.Value - 1] = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                else if (this.cbFNameProduct.SelectedIndex == 2)
                    bc.alight[(int)this.seNameProduct.Value - 1] = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            }

            if (ceBarcode.Checked)
            {
                bc.pos[(int)this.seBC.Value - 1] = StyleLabelBarcode.Barcode;

                if (this.cbFBC.SelectedIndex == 0)
                    bc.alight[(int)this.seBC.Value - 1] = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                else if (this.cbFBC.SelectedIndex == 1)
                    bc.alight[(int)this.seBC.Value - 1] = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                else if (this.cbFBC.SelectedIndex == 2)
                    bc.alight[(int)this.seBC.Value - 1] = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            }

            if (cePrice.Checked)
            {
                bc.pos[(int)this.sePrice.Value - 1] = StyleLabelBarcode.Price;

                if (this.cbPrice.SelectedIndex == 0)
                    bc.alight[(int)this.sePrice.Value - 1] = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                else if (this.cbPrice.SelectedIndex == 1)
                    bc.alight[(int)this.sePrice.Value - 1] = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                else if (this.cbPrice.SelectedIndex == 2)
                    bc.alight[(int)this.sePrice.Value - 1] = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            }

            bc.char_number = (int)((decimal)numChar.EditValue);
            bc.barcode_param = tenDonVi.Text.Trim();
        }        

        private void sbPreview_Click(object sender, EventArgs e)
        {
            if (bc != null)
            {
                GetBarcodeOption();
                DevExpress.XtraReports.UI.XtraReport xr = bc.GetReportStamp(this.tenDonVi.Text.Trim());

                printControl.PrintingSystem = xr.PrintingSystem;
                xr.PrintingSystem.ClearContent();
                xr.CreateDocument();
                printControl.Zoom = 1;
                //printControl.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.ViewWholePage);
            }
        }
        
        private void cera1_CheckedChanged(object sender, EventArgs e)
        {
            if (EAN13Chk.Checked)
            {
                OtherChk.Checked = false;

                teCountry.Enabled = true;
                teProvider.Enabled = true;

                cbBCStyle.Enabled = false;
                numChar.Enabled = false;

                sbPreview_Click(null, null);
                teCountry.Focus();
            }
            else {
                OtherChk.Checked = true;
            }
            
        }

        private void cera2_CheckedChanged(object sender, EventArgs e)
        {
            if (OtherChk.Checked)
            {
                EAN13Chk.Checked = false;

                teCountry.Enabled = false;
                teProvider.Enabled = false;

                cbBCStyle.Enabled = true;
                if((BarCodeType)this.cbBCStyle.SelectedItem != BarCodeType.EAN8)
                    numChar.Enabled = true;

                sbPreview_Click(null, null);

                cbBCStyle.Focus();
            }
            else
            {
                EAN13Chk.Checked = true;
            }
            
        }

        private void simpleButtonDefault_Click(object sender, EventArgs e)
        {
            DOBarcodeOption defaultValue = new DOBarcodeOption();
            this.teCountry.Text = defaultValue.COUNTRY;
            this.teProvider.Text = defaultValue.PROVIDER;
            //this.teIdProduct.Text = defaultValue.PRODUCT;
            this.teIdProduct.Text = "99999";
            
            this.seWidthStamp.Value = defaultValue.STAMP_WIDTH / 100;
            this.seHeightStamp.Value = defaultValue.STAMP_HEIGHT / 100;
            this.seWidthBC.Value = defaultValue.BARCODE_WIDTH / 100;
            this.seHeightBC.Value = defaultValue.BARCODE_HEIGHT / 100;

            this.seModule.Value = defaultValue.BARCODE_MODULE;
            this.seUnit.Value = defaultValue.UNIT_POS;
            this.seNameProduct.Value = defaultValue.NAME_POS;
            this.seBC.Value = defaultValue.BARCODE_POS;
            this.sePrice.Value = defaultValue.PRICE_POS;
            this.ceUnit.Checked = true;
            this.ceNameProduct.Checked = true;
            this.ceBarcode.Checked = true;
            this.cePrice.Checked = true;
            this.cbFUnit.SelectedIndex = defaultValue.UNIT_ALIGHT;
            this.cbFNameProduct.SelectedIndex = defaultValue.NAME_ALIGHT;
            this.cbFBC.SelectedIndex = defaultValue.BARCODE_ALIGHT;
            this.cbPrice.SelectedIndex = defaultValue.PRICE_ALIGHT;

            //this.MauEAN13.Text = defaultValue.COUNTRY + defaultValue.PROVIDER + defaultValue.PRODUCT;
            this.MauEAN13.Text = defaultValue.COUNTRY + defaultValue.PROVIDER + "99999";
            this.MauEAN13.Text += EAN13CheckDigit.checkDigit(MauEAN13.Text);
            this.EAN13Chk.Checked = true;            
            this.OtherChk.Checked = false;
            this.cbBCStyle.SelectedIndex = 0;

            sbPreview_Click(null, null);
            
            this.EAN13Chk.Focus();
        }

        private void simpleButtonZoomOut_Click(object sender, EventArgs e)
        {
            printControl.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.ZoomOut);
        }

        private void simpleButtonZoomIn_Click(object sender, EventArgs e)
        {
            printControl.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.ZoomIn);            
        }

        private void cbBCStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((BarCodeType)this.cbBCStyle.SelectedItem != BarCodeType.EAN8){
                this.numChar.Enabled = true; 
                this.numChar.EditValue = 7;
                this.numChar.Properties.ReadOnly = false;
            }
            else{
                this.numChar.Enabled = false;
                this.numChar.EditValue = 8;
                this.numChar.Properties.ReadOnly = true;
            }

            initMaVachTotNhatTheoLoaiMa();            
        }

        private void teCountry_EditValueChanged(object sender, EventArgs e)
        {
            MauEAN13.Text = teCountry.Text + teProvider.Text + teIdProduct.Text;
            MauEAN13.Text += EAN13CheckDigit.checkDigit(MauEAN13.Text);
        }

        private void teProvider_EditValueChanged(object sender, EventArgs e)
        {
            MauEAN13.Text = teCountry.Text + teProvider.Text + teIdProduct.Text;
            MauEAN13.Text += EAN13CheckDigit.checkDigit(MauEAN13.Text);
        }

        private void numChar_EditValueChanged(object sender, EventArgs e)
        {
            setMau();
        }

        private void initMaVachTotNhatTheoLoaiMa()
        {
            if (EAN13Chk.Checked == true)
            {
                //3 chữ số mã quốc gia
                //3 chữ số nhà cung cấp                
                //6 chữ số là mã sản phẩm biểu diễn được 999.999
                //1 chữ số kiểm tra tính hợp lệ
                MauEAN13.Text = teCountry.Text + teProvider.Text + teIdProduct.Text;
                MauEAN13.Text += EAN13CheckDigit.checkDigit(MauEAN13.Text);
            }
            else
            {
                setMau();
            }
        }
        private void setMau() {
            BarCodeType input = (BarCodeType)this.cbBCStyle.SelectedItem;
            switch (input)
            {
                case BarCodeType.EAN8:
                    /*
                    * 3 digits prefix
                    * 4 digits: article identification.
                    * 1 check digit.
                    */
                    MauMaKhac.Text = "9999999" + EAN8CheckDigit.checkDigit("9999999");
                    
                    break;
                case BarCodeType.CODE25_INDUSTRIAL:
                    //Chỉ số từ 0123456789
                    //1 Bắt buộc ( chấp từ 1 - 9
                    //9 số
                    MauMaKhac.Text = num9((decimal)this.numChar.EditValue);
                    break;
                case BarCodeType.CODE25_INTERLEAVED:
                    //Chỉ số từ 0123456789
                    //1 Bắt buộc ( chấp từ 1 - 9
                    //9 số
                    MauMaKhac.Text = num9((decimal)this.numChar.EditValue);
                    break;
                case BarCodeType.CODE_39:
                    MauMaKhac.Text = "AZ." + num9(((decimal)this.numChar.EditValue) - 3);
                    break;
                case BarCodeType.CODE_39_EXT:
                    MauMaKhac.Text = "AZaz." + num9(((decimal)this.numChar.EditValue) - 5);
                    break;
                case BarCodeType.CODE_93:
                    MauMaKhac.Text = "AZ." + num9(((decimal)this.numChar.EditValue) - 3); ;
                    break;
                case BarCodeType.CODE_93_EXT:
                    MauMaKhac.Text = "AZaz." + num9(((decimal)this.numChar.EditValue) - 5); ;
                    break;
                case BarCodeType.CODABAR:
                    MauMaKhac.Text = "99." + num9(((decimal)this.numChar.EditValue) - 3); ;
                    break;
            }            
        }
        private string num9(decimal num)
        {
            StringBuilder builder = new StringBuilder("");
            for (int i = 0; i < num; i++)
            {
                builder.Append("9");
            }
            return builder.ToString();
        }

        private void ceUnit_CheckedChanged(object sender, EventArgs e)
        {
            if (ceUnit.Checked)
            {
                tenDonVi.Enabled = true;
            }
            else
            {
                tenDonVi.Enabled = false;
            }

        }
    }
}