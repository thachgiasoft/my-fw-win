using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using ProtocolVN.Framework.Core;
using System.Data;

namespace ProtocolVN.Plugin.Barcode
{
    public partial class BarcodeNColMRow : DevExpress.XtraReports.UI.XtraReport
    {
        public ProductBarcode info;
        public BarcodeNColMRow()
        {
            InitializeComponent();
        }

        public BarcodeNColMRow(ProductBarcode info, int totalColPerNum, int totalRowPerPage, 
                                    int chieuCaoKhuonBe, int chieuRongKhuonBe, bool inKhung)
        {
            ReportUnit = ReportUnit.HundredthsOfAnInch;

            this.info = info;             
            if (chieuCaoKhuonBe == -1)
            {
                info.heightContent = info.heightPage - info.marginTop - info.marginBottom;
            }
            else
            {
                info.heightContent = chieuCaoKhuonBe - info.marginTop - info.marginBottom;
            }

            if (chieuRongKhuonBe == -1)
            {
                info.widthContent = info.widthPage - info.marginLeft - info.marginRight;
            }
            else {
                info.widthContent = chieuRongKhuonBe - info.marginLeft - info.marginRight;
            }
            
            //Xác định số row trên 1 trang 
            int rowNum = 0;
            if (this.info.dsProduct != null && this.info.dsProduct.Tables[0] != null)
            {
                rowNum = 0;
                for (int i = 0; i < this.info.dsProduct.Tables[0].Rows.Count; i++)
                {
                    rowNum += HelpNumber.ParseInt32(this.info.dsProduct.Tables[0].Rows[i][this.info.fieldQuantity]);
                }
                rowNum = ((rowNum % totalColPerNum) > 0 ? (rowNum / totalColPerNum + 1) : (rowNum / totalColPerNum));
            }
            InitializeComponent();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable)).BeginInit();

            this.Detail.KeepTogether = false;
            this.Detail.Height = info.heightContent;
            
            //Dùng các khổ giấy của máy in
            //this.PaperKind = info.paperKind;
            //this.PageWidth = info.widthPage;
            //this.PageHeight = info.heightPage;
            //this.PageSize = new Size(info.widthPage, info.heightPage);
            //this.Margins = new System.Drawing.Printing.Margins(info.marginLeft, info.marginRight,
            //    info.marginTop - 1, info.marginBottom - 1);            

            //Dùng khổ giấy do mình định nghĩa
            this.PaperKind = 0;
            this.PageWidth = info.widthContent;
            this.PageHeight = info.heightContent;
            this.PageSize = new Size(info.widthContent, info.heightContent);
            //this.Margins = new System.Drawing.Printing.Margins(info.marginLeft, info.marginRight,
            //    info.marginTop, info.marginBottom);            
            this.Margins = new System.Drawing.Printing.Margins(2, 2, 2, 2);     
            int cellHeight = info.heightContent / totalRowPerPage;
            int cellWidth = info.widthContent / totalColPerNum - 10;

            this.xrTable.Size = new System.Drawing.Size(totalColPerNum * cellWidth, rowNum * cellHeight);
            this.xrTable.Rows.Clear();
            
            for (int i = 1; i <= rowNum; i++)
            {
                XRTableRow tmpRow = new XRTableRow();
                tmpRow.Name = "Row" + i;
                tmpRow.Height = cellHeight;
                for (int j = 0; j < totalColPerNum; j++)
                {
                    XRTableCell tmpCell = new XRTableCell();
                    tmpCell.Name = "Cell" + i + j;                    
                    tmpRow.Cells.Add(tmpCell);
                }
                this.xrTable.Rows.Add(tmpRow);
            }
            ((System.ComponentModel.ISupportInitialize)(this.xrTable)).EndInit();

            int currentDataRow = -1;
            ((System.ComponentModel.ISupportInitialize)(this.xrTable)).BeginInit();
            int quantity = 0;
            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < totalColPerNum; j++)
                {
                    XRTableCell tmpCell = xrTable.Rows[i].Cells[j];
                    if(inKhung)
                        tmpCell.BorderWidth = 1;
                    else
                        tmpCell.BorderWidth = 0;

                    if (quantity == 0)
                    {
                        currentDataRow++;
                        if (this.info.dsProduct.Tables[0].Rows.Count > currentDataRow)
                        {
                            quantity = HelpNumber.ParseInt32(this.info.dsProduct.Tables[0].Rows[currentDataRow][this.info.fieldQuantity]);
                        }
                    }
                    if (this.info.dsProduct.Tables[0].Rows.Count > currentDataRow)
                    {
                        //this.info.GetCellStamp(tmpCell, this.info.dsProduct.Tables[0].Rows[currentDataRow], tmpCell.Left, tmpCell.Top);
                        this.info.GetCellStamp(tmpCell, this.info.dsProduct.Tables[0].Rows[currentDataRow], cellWidth, cellHeight);
                        quantity--;
                    }
                }
            }
            ((System.ComponentModel.ISupportInitialize)(this.xrTable)).EndInit();
        }
    }
}
