using System.Data;
using System.Drawing;
using DevExpress.XtraPrinting.BarCode;
using ProtocolVN.Framework.Core;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UI.BarCode;
using System;

namespace ProtocolVN.Framework.Core
{
    public enum BarCodeType
    {
        EAN13 = -1,        
        EAN8 = 0,
        CODE25_INDUSTRIAL = 1,
        CODE25_INTERLEAVED = 2,                
        CODE_39 = 3,
        CODE_39_EXT = 4,
        CODE_93 = 5,
        CODE_93_EXT = 6,        
        CODABAR = 7
        //EAN128 = 8,
        //UPCA = 9,
        //PDF417 = 10
    }
    /// <summary>
    /// NOTE Khi nâng cấp từ phiên bản 9.1.3 lên 9.2.6 thì có một số vấn đề 
    /// do đổi tên lớp nên chưa tìm được cái tương ứng sẽ nâng cấp sao khi 
    /// có ứng dụng dùng barcode. Do đó tạm thời sẽ ẩn lớp này.    
    /// </summary>
    /// 
    public enum StyleLabelBarcode
    {
        None, Unit, Name, Barcode, Price
    }    

    public class ProductBarcode
    {        
        //Thông tin sản phẩm
        public DataSet dsProduct;
        public string fieldId;
        public string fieldQuantity;
        public string fieldUnit;
        public string fieldName;
        public string fieldPrice;
        
        //Thông tin về khổ giấy truyền vào từ máy in
        public System.Drawing.Printing.PaperKind paperKind;
        public int widthPage;//chiều rộng của trang
        public int heightPage;//chiều cao của trang
        
        //Thông tin canh lề do người dùng nhập vào
        public int marginLeft;
        public int marginRight;
        public int marginTop;
        public int marginBottom;

        //Thông tin về con tem
        public int widthStamp;
        public int heightStamp;
        public int widthBarcode;
        public int heightBarcode;
        public float moduleBarcode;

        //Thông tin về con tem
        public int borderStamp;
        public Font fontUnit;
        public Font fontName;
        public Font fontPrice;
        public int heightUnit;
        public int heightName;
        public int heightPrice;
        public string idCountry;
        public string idProvider;

        public BarCodeType symBC;

        public int char_number;
        public string barcode_param;

        //Thuộc tính tính toán
        public StyleLabelBarcode[] pos;
        public DevExpress.XtraPrinting.TextAlignment[] alight;
        
        public string mauEAN13;
        public string mauKhac;

        public int widthContent;//chiều rộng vùng để vẽ mã vạch
        public int heightContent;//chiều cao vùng để vẽ mã vạch
        
        public int spaceCol;
        public int spaceRow;

        public bool inKhung;

        public ProductBarcode(DataSet ds, string fieldId, string fieldName, string fieldUnit, string fieldPrice, string fieldQuantity)
        {
            DevExpress.XtraPrinting.PrintingSystem printSys = new DevExpress.XtraPrinting.PrintingSystem();
            this.paperKind = printSys.PageSettings.PaperKind;
            this.widthPage = printSys.PageBounds.Width;
            this.heightPage = printSys.PageBounds.Height;
            this.marginLeft = printSys.PageMargins.Left;
            this.marginRight = printSys.PageMargins.Right;
            this.marginTop = printSys.PageMargins.Top;
            this.marginBottom = printSys.PageMargins.Bottom;

            this.fieldId = fieldId;
            this.fieldName = fieldName;
            this.fieldUnit = fieldUnit;
            this.fieldQuantity = fieldQuantity;
            this.fieldPrice = fieldPrice;

            this.dsProduct = ds;

            //this.spaceCol = 10;
            //this.spaceRow = 10;

            this.spaceCol = 0;
            this.spaceRow = 0;

            this.moduleBarcode = 1F;

            this.borderStamp = 1;

            this.heightUnit = 20;
            this.heightName = 20;
            this.heightPrice = 20;

            this.fontUnit = new Font("Tahoma", 8);
            this.fontName = new Font("Tahoma", 8);
            this.fontPrice = new Font("Tahoma", 8);

            this.symBC = BarCodeType.EAN13;
        }

        public ProductBarcode(DataSet ds)
        {
            DevExpress.XtraPrinting.PrintingSystem printSys = new DevExpress.XtraPrinting.PrintingSystem();
            this.paperKind = printSys.PageSettings.PaperKind;
            this.widthPage = printSys.PageBounds.Width;
            this.heightPage = printSys.PageBounds.Height;
            this.marginLeft = printSys.PageMargins.Left;
            this.marginRight = printSys.PageMargins.Right;
            this.marginTop = printSys.PageMargins.Top;
            this.marginBottom = printSys.PageMargins.Bottom;

            this.fieldId = "ID";
            this.fieldName = "NAME";
            this.fieldUnit = "UNIT";
            this.fieldQuantity = "QUANTITY";
            this.fieldPrice = "PRICE";

            this.dsProduct = ds;

            //this.spaceCol = 10;
            //this.spaceRow = 10;

            this.spaceCol = 0;
            this.spaceRow = 0;

            this.moduleBarcode = 1F;

            this.borderStamp = 1;

            this.heightUnit = 20;
            this.heightName = 20;
            this.heightPrice = 20;
            this.fontUnit = new Font("Tahoma", 8);
            this.fontName = new Font("Tahoma", 8);
            this.fontPrice = new Font("Tahoma", 8);

            this.symBC = BarCodeType.EAN13;
     
        }

        /// <summary>
        /// Lấy Generator từ BarCodeType
        /// </summary>
        public BarCodeGeneratorBase GetBCSymbology(BarCodeType sym)
        {
            BarCodeGeneratorBase gen = null;
            switch(sym){
                case BarCodeType.EAN8:
                    return new EAN8Generator();
                case BarCodeType.EAN13:                    
                    return new EAN13Generator();
                //case BarCodeType.EAN128:
                //    return new EAN128Generator();
                case BarCodeType.CODE25_INDUSTRIAL:
                    gen = new Industrial2of5Generator();
                    gen.CalcCheckSum = false;
                    return gen;
                case BarCodeType.CODE25_INTERLEAVED:
                    gen = new Interleaved2of5Generator();
                    gen.CalcCheckSum = false;
                    return gen;
                //case BarCodeType.PDF417:
                //    return new PDF417Generator();
                //case BarCodeType.UPCA:
                //    return new UPCAGenerator();
                case BarCodeType.CODE_39:
                    gen = new Code39Generator();
                    gen.CalcCheckSum = false;
                    return gen;
                case BarCodeType.CODE_39_EXT:
                    gen = new Code93ExtendedGenerator();
                    gen.CalcCheckSum = false;
                    return gen;
                case BarCodeType.CODE_93:
                    gen = new Code93Generator();
                    gen.CalcCheckSum = false;
                    return gen;
                case BarCodeType.CODE_93_EXT:
                    gen = new Code93ExtendedGenerator();
                    gen.CalcCheckSum = false;
                    return gen;
                case BarCodeType.CODABAR:
                    gen = new CodabarGenerator();
                    gen.CalcCheckSum = false;
                    return gen;
            }
            return new EAN13Generator();
        }
        
        /// <summary>
        /// Chuyển int -> BarCodeType
        /// </summary>
        public static BarCodeType getBarCodeType(int sym)
        {
            return (BarCodeType)sym;
            switch (sym)
            {
                case -1: return BarCodeType.EAN13;        
                case 0: return BarCodeType.EAN8;
                case 1: return BarCodeType.CODE25_INDUSTRIAL;
                case 2: return BarCodeType.CODE25_INTERLEAVED;
                case 3: return BarCodeType.CODE_39;
                case 4: return BarCodeType.CODE_39_EXT;
                case 5: return BarCodeType.CODE_93;
                case 6: return BarCodeType.CODE_93_EXT;
                case 7: return BarCodeType.CODABAR;
                //case 8: return BarCodeType.EAN128;
                //case 9: return BarCodeType.UPCA;
                //case 10: return BarCodeType.PDF417;
            }
            return BarCodeType.EAN13;
        }

        //Được sử dụng để Preview trong màn hình CONFIG
        private int GetHeightPos()
        {
            int h = 0;

            for (int i = 0; i < 4; i++)
            {
                if (this.pos[i] == StyleLabelBarcode.Unit)
                    h += this.heightUnit;
                else if (this.pos[i] == StyleLabelBarcode.Name)
                    h += this.heightName;
                else if (this.pos[i] == StyleLabelBarcode.Barcode)
                    h += this.heightBarcode;
                else if (this.pos[i] == StyleLabelBarcode.Price)
                    h += this.heightPrice;
            }

            return h;
        }
        int delta = 0;
        private DevExpress.XtraReports.UI.XRTableCell GetCellStampEx(int left, int top, string tenDV)
        {
            DevExpress.XtraReports.UI.XRTableCell cell = new DevExpress.XtraReports.UI.XRTableCell();
            cell.Location = new Point(left, top);
            cell.Size = new Size(this.widthStamp, this.heightStamp);
            cell.BorderColor = Color.Black;
            cell.Borders = DevExpress.XtraPrinting.BorderSide.All;
            cell.BorderWidth = this.borderStamp;

            int h = GetHeightPos();
            int startleft = (this.widthStamp - this.widthBarcode) / 2;
            int starttop = (this.heightStamp - h) / 2;

            for (int i = 0; i < 4; i++)
            {
                if (this.pos[i] == StyleLabelBarcode.Unit)
                {
                    DevExpress.XtraReports.UI.XRLabel lbUnit = new DevExpress.XtraReports.UI.XRLabel();
                    lbUnit.Text = tenDV;
                    lbUnit.TextAlignment = this.alight[i];
                    lbUnit.Font = fontUnit;
                    lbUnit.Location = new Point(startleft, starttop);
                    lbUnit.Size = new Size(this.widthBarcode, this.heightUnit);
                    lbUnit.BorderWidth = 0;
                    starttop += heightUnit - delta;
                    cell.Controls.Add(lbUnit);
                }
                else if (this.pos[i] == StyleLabelBarcode.Name)
                {
                    DevExpress.XtraReports.UI.XRLabel lbName = new DevExpress.XtraReports.UI.XRLabel();
                    lbName.Text = "Tên sản phẩm";
                    lbName.TextAlignment = this.alight[i];
                    lbName.Font = fontName;
                    lbName.BorderWidth = 0;
                    lbName.Location = new Point(startleft, starttop);
                    lbName.Size = new Size(this.widthBarcode, this.heightName);
                    starttop += heightName - delta;
                    cell.Controls.Add(lbName);
                }
                else if (this.pos[i] == StyleLabelBarcode.Barcode)
                {
                    DevExpress.XtraReports.UI.XRBarCode bcode = new DevExpress.XtraReports.UI.XRBarCode();

                    bcode.Alignment = this.alight[i];
                    bcode.BorderWidth = 0;
                    bcode.Location = new System.Drawing.Point(startleft, starttop);
                    bcode.Module = this.moduleBarcode;
                    bcode.Size = new System.Drawing.Size(this.widthBarcode, this.heightBarcode);
                    bcode.Symbology = GetBCSymbology(this.symBC);

                    if (this.symBC == BarCodeType.EAN13)
                        bcode.Text = this.mauEAN13;
                    else
                        bcode.Text = this.mauKhac;

                    bcode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    starttop += this.heightBarcode - delta;
                    cell.Controls.Add(bcode);
                }
                else if (this.pos[i] == StyleLabelBarcode.Price)
                {
                    DevExpress.XtraReports.UI.XRLabel lbPrice = new DevExpress.XtraReports.UI.XRLabel();
                    lbPrice.Text = "Giá sản phẩm";
                    lbPrice.TextAlignment = this.alight[i];
                    lbPrice.Font = fontPrice;
                    lbPrice.BorderWidth = 0;
                    lbPrice.Location = new Point(startleft, starttop);
                    lbPrice.Size = new Size(this.widthBarcode, this.heightPrice);
                    starttop += heightPrice - delta;
                    cell.Controls.Add(lbPrice);
                }
            }

            return cell;
        }

        public DevExpress.XtraReports.UI.XtraReport GetReportStamp(string tenDV)
        {
            DevExpress.XtraReports.UI.XtraReport xr = new DevExpress.XtraReports.UI.XtraReport();
            xr.ReportUnit = ReportUnit.HundredthsOfAnInch;
            xr.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            xr.PageWidth = this.widthStamp;
            xr.PageHeight = this.heightStamp + 1;
            xr.PageSize = new Size(this.widthStamp, this.heightStamp);            

            xr.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            DevExpress.XtraReports.UI.DetailBand Detail = new DevExpress.XtraReports.UI.DetailBand();
            DevExpress.XtraReports.UI.XRTable xrTable = new DevExpress.XtraReports.UI.XRTable();
            DevExpress.XtraReports.UI.XRTableRow row = new DevExpress.XtraReports.UI.XRTableRow();
            DevExpress.XtraReports.UI.XRTableCell cell;

            Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] { xrTable });

            xrTable.Location = new Point(0, 0);
            xrTable.Size = new System.Drawing.Size(this.widthStamp, this.heightStamp);
            xrTable.Rows.Add(row);
            xrTable.Borders = DevExpress.XtraPrinting.BorderSide.None;
            xrTable.BorderWidth = 0;

            row.Location = new Point(0, 0);
            row.Size = new Size(this.widthStamp, this.heightStamp);
            row.Borders = DevExpress.XtraPrinting.BorderSide.None;

            cell = GetCellStampEx(0, 0, tenDV);
            cell.Location = new Point(0, 0);
            cell.Size = new Size(this.widthStamp, this.heightStamp);
            row.Cells.Add(cell);
            cell.Borders = DevExpress.XtraPrinting.BorderSide.None;

            xr.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] { Detail });
            return xr;
        }

        public void GetCellStamp(DevExpress.XtraReports.UI.XRTableCell cell, DataRow dr, int cellWidth, int cellHeight)
        {
            int h = GetHeightPos();
            //int startleft = left;
            //int starttop = top;

            //int startleft = (this.widthStamp - this.widthBarcode) / 2;
            //int starttop = (this.heightStamp - h) / 2;

            int startleft = Math.Abs((cellWidth - this.widthStamp) / 2);
            int starttop = Math.Abs((cellHeight - this.heightStamp) / 2);

            for (int i = 0; i < 4; i++)
            {
                if (this.pos[i] == StyleLabelBarcode.Unit)
                {
                    DevExpress.XtraReports.UI.XRLabel lbUnit = new DevExpress.XtraReports.UI.XRLabel();
                    //lbUnit.Text = dr[this.fieldUnit].ToString();
                    lbUnit.Text = this.barcode_param;
                    lbUnit.TextAlignment = this.alight[i];
                    lbUnit.Font = fontUnit;
                    lbUnit.Location = new Point(startleft, starttop);
                    lbUnit.SizeF = new SizeF(this.widthBarcode, this.heightUnit);
                    lbUnit.BorderWidth = 0;
                    starttop += heightUnit - delta;
                    cell.Controls.Add(lbUnit);
                }
                else if (this.pos[i] == StyleLabelBarcode.Name)
                {
                    DevExpress.XtraReports.UI.XRLabel lbName = new DevExpress.XtraReports.UI.XRLabel();
                    lbName.Text = dr[this.fieldName].ToString();
                    lbName.TextAlignment = this.alight[i];
                    lbName.Font = fontName;
                    lbName.BorderWidth = 0;
                    lbName.Location = new Point(startleft, starttop);
                    lbName.SizeF = new SizeF(this.widthBarcode, this.heightName);
                    starttop += heightName - delta;
                    cell.Controls.Add(lbName);
                }
                else if (this.pos[i] == StyleLabelBarcode.Barcode)
                {
                    DevExpress.XtraReports.UI.XRBarCode bcode = new DevExpress.XtraReports.UI.XRBarCode();


                    bcode.Alignment = this.alight[i];
                    bcode.BorderWidth = 0;
                    bcode.Location = new System.Drawing.Point(startleft, starttop);
                    bcode.Module = this.moduleBarcode;
                    bcode.SizeF = new System.Drawing.SizeF(this.widthBarcode, this.heightBarcode);
                    bcode.Symbology = GetBCSymbology(this.symBC);
                    //bcode.Text = this.idCountry + this.idProvider + dr[this.fieldId].ToString();
                    //if (this.symBC == BarCodeType.EAN13)// DevExpress.XtraReports.UI.BarCode.XRBarCodeSymbology.EAN13
                    //    bcode.Text = this.idCountry + this.idProvider + dr[this.fieldId].ToString();
                    //else
                    bcode.Text = dr[this.fieldId].ToString();

                    bcode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    starttop += this.heightBarcode - delta;
                    cell.Controls.Add(bcode);
                }
                else if (this.pos[i] == StyleLabelBarcode.Price)
                {
                    DevExpress.XtraReports.UI.XRLabel lbPrice = new DevExpress.XtraReports.UI.XRLabel();
                    lbPrice.Text = dr[this.fieldPrice].ToString();
                    lbPrice.TextAlignment = this.alight[i];
                    lbPrice.Font = fontPrice;
                    lbPrice.BorderWidth = 0;
                    lbPrice.Location = new Point(startleft, starttop);
                    lbPrice.SizeF = new SizeF(this.widthBarcode, this.heightPrice);
                    starttop += heightPrice - delta;
                    cell.Controls.Add(lbPrice);
                }
            }
        }

        public XtraReport getReportAll(int colNum, int rowNum, int chieuDaiKhuonBe, int chieuRongKhuonBe, bool inKhung)
        {
            return new ProtocolVN.Plugin.Barcode.BarcodeNColMRow(this, colNum, rowNum, chieuDaiKhuonBe, chieuRongKhuonBe, inKhung);
        }

    }
}
