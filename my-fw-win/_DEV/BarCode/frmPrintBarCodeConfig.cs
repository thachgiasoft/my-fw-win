using System;
using System.Data;
using System.Drawing.Printing;
using ProtocolVN.Framework.Core;
using ProtocolVN.Framework.Win;
using DevExpress.XtraEditors.DXErrorProvider;
namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Màn hình cấu hình Barcode
    /// </summary>
    public partial class frmPrintBarCodeConfig : XtraFormPL, IFormFixSize
    {
        public DataTable data;
        private DXErrorProvider Error = new DXErrorProvider();
        public frmPrintBarCodeConfig()
        {
            InitializeComponent();
            this.Error = GUIValidation.GetErrorProvider(this);
        }

        private void frmTestPrintBarCodeConfig_Load(object sender, EventArgs e)
        {
            //Danh sách máy in
            PopulateInstalledPrintersCombo();
            if (this.cboPrinter.Properties.Items.Count > -1)
            {
                this.cboPrinter.SelectedIndex = 1;
            }

            //Danh sách khổ giấy của từng máy in
            //PopulatePaperSizesOfPrinter(this.cboPrinter.Text);
            //if (this.cboPaperSize.Properties.Items.Count > -1)
            //{
            //    this.cboPaperSize.SelectedIndex = 1;
            //}
        }

        #region Danh sách các hàm đang dùng
        //**************Lay danh sach cac may in va hien thi tren cmb
        private void PopulateInstalledPrintersCombo()
        {
            String pkInstalledPrinters;

            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                this.cboPrinter.Properties.Items.Add(pkInstalledPrinters);
            }
        }

        ////**************Lay danh sach cac may in va hien thi tren cmb
        private void PopulatePaperSizesOfPrinter(string strPrinterName)
        {
            this.cboPaperSize.Properties.Items.Clear();
            PrinterSettings printSet = new PrinterSettings();

            printSet.PrinterName = strPrinterName;
            string strPaperSize;
            for (int i = 0; i < printSet.PaperSizes.Count; i++)
            {
                strPaperSize = printSet.PaperSizes[i].Kind.ToString();
                if(!this.cboPaperSize.Properties.Items.Contains(strPaperSize.Trim()))
                    this.cboPaperSize.Properties.Items.Add(strPaperSize.Trim());
                if(strPaperSize == "Custom")
                    this.cboPaperSize.SelectedIndex = i;
            }
            if (this.cboPaperSize.SelectedIndex == -1)
            {
                this.cboPaperSize.SelectedIndex = 0;
            }
        }
        #endregion

        private void btnAdPreview_Click(object sender, EventArgs e)
        {
            ProductBarcode bc = InitBarCodeOption();

            int totalColPerPage =HelpNumber.ParseInt32(this.TemDoc.EditValue);
            int totalRowPerPage = HelpNumber.ParseInt32(this.TemNgang.EditValue);
            
            int chieuRongKhuonBe = (int)(this.ChieuRong.Value * 100);//96DotPerInch
            int chieuCaoKhuonBe = (int)(this.ChieuCao.Value * 100);//96DotPerInch
            if (this.ValidateData())
            {                
                DevExpress.XtraReports.UI.XtraReport xr = bc.getReportAll(totalColPerPage, totalRowPerPage, chieuCaoKhuonBe, chieuRongKhuonBe, false);
                xr.ShowPreviewDialog();
            }
        }

        private void cboPrinter_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulatePaperSizesOfPrinter(this.cboPrinter.Text);
        }

        private void btnAdPrintToPrinter_Click(object sender, EventArgs e)
        {
            ProductBarcode bc = InitBarCodeOption();
            int totalColPerPage = HelpNumber.ParseInt32(this.TemDoc.EditValue);
            int totalRowPerPage = HelpNumber.ParseInt32(this.TemNgang.EditValue);
            int chieuRongKhuonBe = (int)(this.ChieuRong.Value * 100);
            int chieuCaoKhuonBe = (int)(this.ChieuCao.Value * 100);
            if (this.ValidateData())
            {
                DevExpress.XtraReports.UI.XtraReport xr = bc.getReportAll(totalColPerPage, totalRowPerPage, chieuCaoKhuonBe, chieuRongKhuonBe, false);
                //xr.PrintDialog();
                xr.Print(cboPrinter.SelectedText);
            }
        }

        ////*************Khoi tao BarCodeOption
        private ProductBarcode InitBarCodeOption()
        {
            DOBarcodeOption configOption = DOBarcodeOption.load();

            //DataTable tab = InitDataTable();
            //TrialPLBarCodeProduct bc = new TrialPLBarCodeProduct(InitDataFromTable(tab),"ID", "NAME", "UNIT", "PRICE", "QUANTITY");

            ProductBarcode bc = new ProductBarcode(InitDataFromTable(data), "ID", "NAME", "UNIT", "PRICE", "QUANTITY");

            int symbc = HelpNumber.ParseInt32(configOption.SYM_BARCODE);
            if (symbc == -1)
                bc.symBC = BarCodeType.EAN13;
            else
                bc.symBC = ProductBarcode.getBarCodeType(symbc);

            bc.pos = new StyleLabelBarcode[4];
            bc.alight = new DevExpress.XtraPrinting.TextAlignment[4];

            bc.pos[0] = StyleLabelBarcode.None;
            bc.pos[1] = StyleLabelBarcode.None;
            bc.pos[2] = StyleLabelBarcode.None;
            bc.pos[3] = StyleLabelBarcode.None;

            bc.alight[0] = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            bc.alight[1] = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            bc.alight[2] = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            bc.alight[3] = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            if (configOption.UNIT_USING == "Y")
            {
                bc.barcode_param = configOption.BARCODE_PARAM;

                bc.pos[HelpNumber.ParseInt32(configOption.UNIT_POS) - 1] = StyleLabelBarcode.Unit;

                if (configOption.UNIT_ALIGHT == 0)
                    bc.alight[HelpNumber.ParseInt32(configOption.UNIT_POS) - 1] = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                else if (configOption.UNIT_ALIGHT == 1)
                    bc.alight[HelpNumber.ParseInt32(configOption.UNIT_POS) - 1] = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                else if (configOption.UNIT_ALIGHT == 2)
                    bc.alight[HelpNumber.ParseInt32(configOption.UNIT_POS) - 1] = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            }

            if (configOption.NAME_USING == "Y")
            {
                bc.pos[HelpNumber.ParseInt32(configOption.NAME_POS) - 1] = StyleLabelBarcode.Name;

                if (configOption.NAME_ALIGHT == 0)
                    bc.alight[HelpNumber.ParseInt32(configOption.NAME_POS) - 1] = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                else if (configOption.NAME_ALIGHT == 1)
                    bc.alight[HelpNumber.ParseInt32(configOption.NAME_POS) - 1] = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                else if (configOption.NAME_ALIGHT == 2)
                    bc.alight[HelpNumber.ParseInt32(configOption.NAME_POS) - 1] = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            }

            if (configOption.BARCODE_USING == "Y")
            {
                bc.pos[HelpNumber.ParseInt32(configOption.BARCODE_POS) - 1] = StyleLabelBarcode.Barcode;

                if (configOption.BARCODE_ALIGHT == 0)
                    bc.alight[HelpNumber.ParseInt32(configOption.BARCODE_POS) - 1] = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                else if (configOption.BARCODE_ALIGHT == 1)
                    bc.alight[HelpNumber.ParseInt32(configOption.BARCODE_POS) - 1] = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                else if (configOption.BARCODE_ALIGHT == 2)
                    bc.alight[HelpNumber.ParseInt32(configOption.BARCODE_POS) - 1] = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            }

            if (configOption.PRICE_USING == "Y")
            {
                bc.pos[HelpNumber.ParseInt32(configOption.PRICE_POS) - 1] = StyleLabelBarcode.Price;

                if (configOption.PRICE_ALIGHT == 0)
                    bc.alight[HelpNumber.ParseInt32(configOption.PRICE_POS) - 1] = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                else if (configOption.PRICE_ALIGHT == 1)
                    bc.alight[HelpNumber.ParseInt32(configOption.PRICE_POS) - 1] = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                else if (configOption.PRICE_ALIGHT == 2)
                    bc.alight[HelpNumber.ParseInt32(configOption.PRICE_POS) - 1] = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            }

            bc.heightBarcode = HelpNumber.ParseInt32(configOption.BARCODE_HEIGHT);
            bc.widthBarcode = HelpNumber.ParseInt32(configOption.BARCODE_WIDTH);

            bc.idCountry = configOption.COUNTRY;
            bc.idProvider = configOption.PROVIDER;

            bc.widthStamp = HelpNumber.ParseInt32(configOption.STAMP_WIDTH);
            bc.heightStamp = HelpNumber.ParseInt32(configOption.STAMP_HEIGHT);

            //Thiet lap kho giay
            bc.paperKind = GetPaperSize().Kind;

            //Thiet lap canh le
            //bc.marginBottom = GetMarginBottom();
            //bc.marginTop = GetMarginTop();
            //bc.marginLeft = GetMarginLeft();
            //bc.marginRight = GetMarginRight();

            bc.marginBottom = 0;
            bc.marginTop = 0;
            bc.marginLeft = 0;
            bc.marginRight = 0;

            bc.widthPage = GetPaperSize().Width;
            bc.heightPage = GetPaperSize().Height;

            bc.char_number = configOption.CHAR_NUMBER;
            bc.barcode_param = configOption.BARCODE_PARAM;

            return bc;
        }

        ////**************Khoi tao du lieu cho cac san pham can in ma vach
        private DataSet InitDataFromTable(DataTable tab)
        {
            DataSet ds = new DataSet();
            DataTable tabNew = new DataTable();

            if (!tab.Columns.Contains("QUANTITY"))
                tab.Columns.Add("QUANTITY");

            //foreach (DataRow r in tab.Rows)
            //{
            //    r["QUANTITY"] = GetNumberOfStamp().ToString();
            //}

            if (tab.DataSet == null)
                ds.Tables.Add(tab);
            else
                ds.Tables.Add(tab.Copy());
            return ds;
        }

        ////**************Cac ham lay thong tin in
        ////Lay thong tin ve le trai
        //public int GetMarginLeft()
        //{
        //    int intLeft = 100;
        //    if (this.txtMarginLeft.Text != "")
        //        intLeft = HelpNumber.ParseInt32(this.txtMarginLeft.Text);

        //    return intLeft;
        //}

        ////Lay thong tin ve le phai
        //public int GetMarginRight()
        //{
        //    int intRight = 100;
        //    if (this.txtMarginRight.Text != "")
        //        intRight = HelpNumber.ParseInt32(this.txtMarginRight.Text);

        //    return intRight;
        //}

        ////Lay thong tin ve le tren
        //public int GetMarginTop()
        //{
        //    int intTop = 100;
        //    if (this.txtMarginTop.Text != "")
        //        intTop = HelpNumber.ParseInt32(this.txtMarginTop.Text);

        //    return intTop;
        //}

        ////Lay thong tin ve le duoi
        //public int GetMarginBottom()
        //{
        //    int intBottom = 100;
        //    if (this.txtMarginBottom.Text != "")
        //        intBottom = HelpNumber.ParseInt32(this.txtMarginBottom.Text);

        //    return intBottom;
        //}

        ////Lay thong tin ve may in
        public string GetPrinterName()
        {
            return this.cboPrinter.Text;
        }

        ////Lay thong tin ve kho giay
        public PaperSize GetPaperSize()
        {
            PrinterSettings printSet = new PrinterSettings();
            printSet.PrinterName = this.cboPrinter.Text;

            return printSet.PaperSizes[this.cboPaperSize.SelectedIndex];

        }

        
        private void cboPaperSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrinterSettings printSet = new PrinterSettings();
            if (cboPaperSize.Text == "Custom")
            {
                this.ChieuCao.Enabled = true;
                this.ChieuRong.Enabled = true;
                this.ChieuRong.EditValue = 5.0;
                this.ChieuCao.EditValue = 5.0;
                this.TemDoc.EditValue = 4;
                this.TemNgang.EditValue = 6;
            }
            else
            {
                this.ChieuCao.Enabled = false;
                this.ChieuRong.Enabled = false;
                printSet.PrinterName = cboPrinter.Text;
                foreach (PaperSize size in printSet.PaperSizes)
                {
                    if (size.PaperName == cboPaperSize.Text)
                    {
                        //ChieuCao.EditValue = HelpNumber.RoundDecimal(HelpNumber.ParseDecimal((size.Height * 25.40) / 100), 0);
                        //ChieuRong.EditValue = HelpNumber.RoundDecimal(HelpNumber.ParseDecimal((size.Width * 25.40) / 100), 0);
                        ChieuCao.EditValue = ((decimal)size.Height) / 100;
                        ChieuRong.EditValue = ((decimal)size.Width) / 100;
                        break;
                    }
                }
            }
        }
        private bool ValidateData()
        {
            Error.ClearErrors();
            if (HelpNumber.ParseDecimal(TemDoc.Value) <= 0)
            {
                Error.SetError(TemDoc, "Vui lòng nhập cột dọc lớn hơn 0");
                return false;
            }
            if (HelpNumber.ParseDecimal(TemNgang.Value) <= 0)
            {
                Error.SetError(TemNgang, "Vui lòng nhập cột ngang lớn hơn 0");
                return false;
            }
            if (cboPaperSize.Text == "Custom")
            {
                if (HelpNumber.ParseDecimal(ChieuCao.Value) <= 0)
                {
                    Error.SetError(ChieuCao, "Vui lòng nhập chiều cao lớn hơn 0");
                    return false;
                }
                if (HelpNumber.ParseDecimal(ChieuRong.Value) <= 0)
                {
                    Error.SetError(ChieuRong, "Vui lòng nhập chiều rộng lớn hơn 0");
                    return false;
                }
            }
            
            return true;
        }
    }
}