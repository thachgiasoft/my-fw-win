using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;

//Title:       Simple Dynamic Report with SQL Query
//Author:      Pakorn Indhatep
//Email:       pakorncs@bizslash.com;phuocnt@gmail.com
//Environment: Visual Baic.NET
//Keywords:    Dynamic Report, Report, printDocument
//Description: Dynamic report by printDocument Component with SQL Query
//Section      VB.NET
//SubSection   Printing


namespace ProtocolVN.Framework.Win
{
	public class frmPrintDynamicForm : XtraForm
	{
		
		#region  Windows Form Designer generated code
		
		public frmPrintDynamicForm()
		{
			
			//This call is required by the Windows Form Designer.
			InitializeComponent();
			
			//Add any initialization after the InitializeComponent() call
			
		}
		
		//Form overrides dispose to clean up the component list.
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (!(components == null))
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		//Required by the Windows Form Designer
		private System.ComponentModel.Container components = null;
		
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.
		//Do not modify it using the code editor.
		public System.Drawing.Printing.PrintDocument prnDoc;
		public System.Windows.Forms.PrintPreviewDialog printdlg;
		public System.Windows.Forms.Panel Panel1;
		public SimpleButton btnPrint;
        public SimpleButton btnClose;
        public SimpleButton btnPrnDlg;
        public SimpleButton btnDocSet;
		public System.Windows.Forms.PrintDialog prnSetDlg;
		public System.Windows.Forms.PageSetupDialog PageSetDlg;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrintDynamicForm));
            this.prnDoc = new System.Drawing.Printing.PrintDocument();
            this.printdlg = new System.Windows.Forms.PrintPreviewDialog();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.btnDocSet = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrnDlg = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.PageSetDlg = new System.Windows.Forms.PageSetupDialog();
            this.prnSetDlg = new System.Windows.Forms.PrintDialog();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // prnDoc
            // 
            this.prnDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.prnDoc_PrintPage);
            // 
            // printdlg
            // 
            this.printdlg.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printdlg.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printdlg.ClientSize = new System.Drawing.Size(400, 300);
            this.printdlg.Document = this.prnDoc;
            this.printdlg.Enabled = true;
            this.printdlg.Icon = ((System.Drawing.Icon)(resources.GetObject("printdlg.Icon")));
            this.printdlg.Name = "printdlg";
            this.printdlg.Visible = false;
            // 
            // Panel1
            // 
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this.btnDocSet);
            this.Panel1.Controls.Add(this.btnPrnDlg);
            this.Panel1.Controls.Add(this.btnPrint);
            this.Panel1.Controls.Add(this.btnClose);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel1.Location = new System.Drawing.Point(0, 134);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(504, 43);
            this.Panel1.TabIndex = 14;
            // 
            // btnDocSet
            // 
            this.btnDocSet.Image = ((System.Drawing.Image)(resources.GetObject("btnDocSet.Image")));
            this.btnDocSet.Location = new System.Drawing.Point(136, 9);
            this.btnDocSet.Name = "btnDocSet";
            this.btnDocSet.Size = new System.Drawing.Size(116, 25);
            this.btnDocSet.TabIndex = 12;
            this.btnDocSet.Text = "Page Setup";
            this.btnDocSet.Click += new System.EventHandler(this.btnDocSet_Click);
            // 
            // btnPrnDlg
            // 
            this.btnPrnDlg.Image = ((System.Drawing.Image)(resources.GetObject("btnPrnDlg.Image")));
            this.btnPrnDlg.Location = new System.Drawing.Point(12, 9);
            this.btnPrnDlg.Name = "btnPrnDlg";
            this.btnPrnDlg.Size = new System.Drawing.Size(116, 25);
            this.btnPrnDlg.TabIndex = 11;
            this.btnPrnDlg.Text = "Printer Setup";
            this.btnPrnDlg.Click += new System.EventHandler(this.btnPrnDlg_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.Location = new System.Drawing.Point(340, 9);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(72, 25);
            this.btnPrint.TabIndex = 10;
            this.btnPrint.Text = "In";
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(420, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(72, 25);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // PageSetDlg
            // 
            this.PageSetDlg.Document = this.prnDoc;
            // 
            // prnSetDlg
            // 
            this.prnSetDlg.Document = this.prnDoc;
            // 
            // frmPrintDynamicForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(504, 177);
            this.Controls.Add(this.Panel1);
            this.Name = "frmPrintDynamicForm";
            this.Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		
		#endregion
		//----------------- Set Report Default Value
		public string HeadFirstString;
		public string HeadSecondString;
		public string DateString;
		public string FontName = "Tahoma";
		public float FontSize = 12;
		public float FontSizeHead = 16;
		public float FontSizeHead2 = 14;
		public float LineHeight = 1;
		public byte SkipLinePerRecord = 0;
		public bool WrapTextFlag = false;
		//------------------ Have Summary
		public bool hasSum = false;
		//------ Public for manual initial Dataset
		public DataSet dbSet = new DataSet();
		
		//------ Internal Use Counter and Flag
		private bool printSumComplete = false;
		private int PageNumber = 0;
		private long dbCursor = 0;
		private ArrayList SumList = new ArrayList();
		private int LineAmountPerRecord = 0;
		
		public const int C_column_name = 0;
		public const int C_start_position = 1;
		public const int C_end_position = 2;
		public const int C_justify = 3;
		public const int C_has_sumarize = 4;
		public const int C_display_format = 5;
		public const int C_rest_in_line = 6;
		public const int C_data = 7;
		public const int C_amount_of_column = 8;
		
		public float getPositionWidth(float p_width, float p_position)
		{
			return p_width * p_position / 100;
		}
		
		//----- Set SQL string for report by this format (1 set per 1 report column):
		//----- Column 1 -> column name
		//----- Column 2 -> Start position on paper, I set all width has range between 0-99
		//----- Column 3 -> End position on paper, I set all width has range between 0-99
		//----- Column 4 -> Justify (L-Left, R-Right, C-Center)
		//----- Column 5 -> Has summarize in this column (Y/N)
		//----- Column 6 -> Display format(such as #,##0.00)
		//----- Column 7 -> Rest in line? Begin with 1
		//----- Column 8 -> Data Column
		//----- Please look at this example-------------------------------------------
		private void prnDoc_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			int LeftMargin = e.MarginBounds.Left;
			int RightMargin = e.MarginBounds.Right;
			int TopMargin = e.MarginBounds.Top;
			int LinesInPage = 0;
			int YPosition = 0;
			int CountLine = 0;
			int CenterPosition = 0;
			Font myFont = new Font(FontName, FontSize, FontStyle.Regular, GraphicsUnit.Point);
			Font myHeadFont = new Font(FontName, FontSizeHead, FontStyle.Regular, GraphicsUnit.Point);
			Font myHead2Font = new Font(FontName, FontSizeHead2, FontStyle.Regular, GraphicsUnit.Point);
			Pen myPen = new Pen(Color.Black);
			StringFormat StrFormatCenter = new StringFormat();
			StringFormat StrFormatLeft = new StringFormat();
			StringFormat StrFormatRight = new StringFormat();
			int ColCount;
			int ColCountSum;
			
			System.Drawing.SizeF sizeR = new System.Drawing.SizeF();
			float ColumnWidthWrap = 0;
			System.Drawing.SizeF RowSizeWrap = new System.Drawing.SizeF();
			float RowHeightWrap = 0;
			float MaxRowHeightWrap = 0;
			try
			{
				if (dbSet.Tables[0].Rows.Count <= 0)
				{
					MessageBox.Show("have not data", "have not data", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
				PageNumber++;
				StrFormatLeft.Alignment = StringAlignment.Near;
				StrFormatRight.Alignment = StringAlignment.Far;
				StrFormatCenter.Alignment = StringAlignment.Center;
				LinesInPage =  (int) (e.MarginBounds.Height / myFont.GetHeight(e.Graphics));
				for (int i = 0; i <= dbSet.Tables[0].Columns.Count - 1; i += C_amount_of_column)
				{
					if (System.Convert.ToInt32(dbSet.Tables[0].Rows[0][i + C_rest_in_line]) > LineAmountPerRecord )
					{
						LineAmountPerRecord =  (int) (dbSet.Tables[0].Rows[0][i + C_rest_in_line]);
					}
				}
				
				//----- Decrease line in page by header line count --------
				LinesInPage -= 3;
				CenterPosition = LeftMargin + e.MarginBounds.Width / 2;
				
				YPosition = TopMargin;
				e.Graphics.DrawString(HeadFirstString, myHeadFont, Brushes.Black, CenterPosition, YPosition, StrFormatCenter);
				e.Graphics.DrawString("page " + PageNumber.ToString(), myFont, Brushes.Black, LeftMargin, YPosition, StrFormatLeft);
				e.Graphics.DrawString("date " + DateTime.Now.Date.ToString(), myFont, Brushes.Black, RightMargin, YPosition, StrFormatRight);
				CountLine++;
				YPosition =  (int) (TopMargin + (CountLine * myHead2Font.GetHeight(e.Graphics)));
				e.Graphics.DrawString(HeadSecondString, myHead2Font, Brushes.Black, CenterPosition, YPosition, StrFormatCenter);
				CountLine++;
				YPosition =  (int) (TopMargin + (CountLine * myHead2Font.GetHeight(e.Graphics)));
				e.Graphics.DrawString(DateString, myFont, Brushes.Black, CenterPosition, YPosition, StrFormatCenter);
				CountLine += 2;
				YPosition =  (int) (TopMargin + (CountLine * myFont.GetHeight(e.Graphics)));
				e.Graphics.DrawLine(myPen, LeftMargin, YPosition + (myFont.GetHeight(e.Graphics) / 2), RightMargin, YPosition + (myFont.GetHeight(e.Graphics) / 2));
				CountLine++;
				YPosition =  (int) (TopMargin + (CountLine * myFont.GetHeight(e.Graphics)));
				//----------- Column Header --------------
				int oldRestInLineH = 1;
				for (int j = 1; j <= LineAmountPerRecord; j++)
				{
					if (oldRestInLineH < j)
					{
						oldRestInLineH = j;
						CountLine +=  (int) (LineHeight);
						YPosition =  (int) (TopMargin + (CountLine * myFont.GetHeight(e.Graphics)));
					}
					for (int i = 0; i <= dbSet.Tables[0].Columns.Count - 1; i += C_amount_of_column)
					{
						if (System.Convert.ToInt32(dbSet.Tables[0].Rows[(int) dbCursor][i + C_rest_in_line]) == j )
						{
							if ((string) dbSet.Tables[0].Rows[0][i + C_justify] == "L")
							{
								e.Graphics.DrawString(System.Convert.ToString(dbSet.Tables[0].Rows[0][i]), myFont, Brushes.Black, RectangleF.FromLTRB(LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_start_position])), YPosition, LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_end_position])), YPosition + myFont.GetHeight(e.Graphics)), StrFormatLeft);
							}
							else if ((string) dbSet.Tables[0].Rows[0][i + C_justify] == "R")
							{
								e.Graphics.DrawString(System.Convert.ToString(dbSet.Tables[0].Rows[0][i]), myFont, Brushes.Black, RectangleF.FromLTRB(LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_start_position])), YPosition, LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_end_position])), YPosition + myFont.GetHeight(e.Graphics)), StrFormatRight);
							}
							else if ((string) dbSet.Tables[0].Rows[0][i + C_justify] == "C")
							{
								e.Graphics.DrawString(System.Convert.ToString(dbSet.Tables[0].Rows[0][i]), myFont, Brushes.Black, RectangleF.FromLTRB(LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_start_position])), YPosition, LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_end_position])), YPosition + myFont.GetHeight(e.Graphics)), StrFormatCenter);
							}
							//---------- Prepare Sum ------------------
							SumList.Add(0);
						}
					}
				}
				
				//----------- Report Detail --------------
				CountLine++;
				YPosition =  (int) (TopMargin + (CountLine * myFont.GetHeight(e.Graphics)));
				e.Graphics.DrawLine(myPen, LeftMargin, YPosition + (myFont.GetHeight(e.Graphics) / 2), RightMargin, YPosition + (myFont.GetHeight(e.Graphics) / 2));
				CountLine++;
				
				while ((CountLine < LinesInPage) && (dbCursor < dbSet.Tables[0].Rows.Count))
				{
					ColCount = 0;
					YPosition =  (int) (TopMargin + (CountLine * myFont.GetHeight(e.Graphics)));
					int oldRestInLine = 1;
					for (int j = 1; j <= LineAmountPerRecord; j++)
					{
						if (oldRestInLine < j)
						{
							oldRestInLine = j;
							CountLine +=  (int) (LineHeight);
							YPosition =  (int) (TopMargin + (CountLine * myFont.GetHeight(e.Graphics)));
						}
						
						// -------- Begin For Wrap Text Feature Only ----------
						if (WrapTextFlag)
						{
							MaxRowHeightWrap = 0;
							for (int i = 0; i <= dbSet.Tables[0].Columns.Count - 1; i += C_amount_of_column)
							{
								ColumnWidthWrap = getPositionWidth(e.MarginBounds.Width, System.Convert.ToInt32(dbSet.Tables[0].Rows[(int) dbCursor][i + C_end_position] )-System.Convert.ToInt32( dbSet.Tables[0].Rows[(int) dbCursor][i + C_start_position]));
								sizeR.Width = ColumnWidthWrap;
								sizeR.Height = 0;
								if ((string) dbSet.Tables[0].Rows[(int) dbCursor][i + C_display_format] == "") //--- Have Format
								{
									RowSizeWrap = e.Graphics.MeasureString(System.Convert.ToString(dbSet.Tables[0].Rows[(int) dbCursor][i + C_data]), myFont, sizeR);
								}
								else
								{
									RowSizeWrap = e.Graphics.MeasureString(Strings.Format(dbSet.Tables[0].Rows[(int) dbCursor][i + C_data], System.Convert.ToString(dbSet.Tables[0].Rows[(int) dbCursor][i + C_display_format])), myFont, sizeR);
								}
								RowHeightWrap = (float) (Math.Ceiling(RowSizeWrap.Height / myFont.GetHeight(e.Graphics)));
								if (RowHeightWrap > MaxRowHeightWrap)
								{
									MaxRowHeightWrap = RowHeightWrap;
								}
							}
							MaxRowHeightWrap -= 2;
						}
						// -------- End For Wrap Text Feature Only ----------
						
						for (int i = 0; i <= dbSet.Tables[0].Columns.Count - 1; i += C_amount_of_column)
						{
							if (System.Convert.ToInt32(dbSet.Tables[0].Rows[(int) dbCursor][i + C_rest_in_line]) == j )
							{
								if ((string) dbSet.Tables[0].Rows[(int) dbCursor][i + C_display_format] == "") //--- Have Format
								{
									if ((string) dbSet.Tables[0].Rows[(int) dbCursor][i + C_justify] == "L")
									{
										e.Graphics.DrawString(System.Convert.ToString(dbSet.Tables[0].Rows[(int) dbCursor][i + C_data]), myFont, Brushes.Black, RectangleF.FromLTRB(LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_start_position])), YPosition, LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_end_position])), YPosition + (myFont.GetHeight(e.Graphics) * (LineHeight + MaxRowHeightWrap))), StrFormatLeft);
									}
									else if ((string) dbSet.Tables[0].Rows[(int) dbCursor][i + C_justify] == "R")
									{
										e.Graphics.DrawString(System.Convert.ToString(dbSet.Tables[0].Rows[(int) dbCursor][i + C_data]), myFont, Brushes.Black, RectangleF.FromLTRB(LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_start_position])), YPosition, LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_end_position])), YPosition + (myFont.GetHeight(e.Graphics) * (LineHeight + MaxRowHeightWrap))), StrFormatRight);
									}
									else if ((string) dbSet.Tables[0].Rows[(int) dbCursor][i + C_justify] == "C")
									{
										e.Graphics.DrawString(System.Convert.ToString(dbSet.Tables[0].Rows[(int) dbCursor][i + C_data]), myFont, Brushes.Black, RectangleF.FromLTRB(LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_start_position])), YPosition, LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_end_position])), YPosition + (myFont.GetHeight(e.Graphics) * (LineHeight + MaxRowHeightWrap))), StrFormatCenter);
									}
								}
								else //-------- Have Format , Yes --------------------
								{
									if ((string) dbSet.Tables[0].Rows[(int) dbCursor][i + C_justify] == "L")
									{
										e.Graphics.DrawString(Strings.Format(dbSet.Tables[0].Rows[(int) dbCursor][i + C_data], System.Convert.ToString(dbSet.Tables[0].Rows[(int) dbCursor][i + C_display_format])), myFont, Brushes.Black, RectangleF.FromLTRB(LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_start_position])), YPosition, LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_end_position])), YPosition + (myFont.GetHeight(e.Graphics) * (LineHeight + MaxRowHeightWrap))), StrFormatLeft);
									}
									else if ((string) dbSet.Tables[0].Rows[(int) dbCursor][i + C_justify] == "R")
									{
										e.Graphics.DrawString(Strings.Format(dbSet.Tables[0].Rows[(int) dbCursor][i + C_data], System.Convert.ToString(dbSet.Tables[0].Rows[(int) dbCursor][i + C_display_format])), myFont, Brushes.Black, RectangleF.FromLTRB(LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_start_position])), YPosition, LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_end_position])), YPosition + (myFont.GetHeight(e.Graphics) * (LineHeight + MaxRowHeightWrap))), StrFormatRight);
									}
									else if ((string) dbSet.Tables[0].Rows[(int) dbCursor][i + C_justify] == "C")
									{
										e.Graphics.DrawString(Strings.Format(dbSet.Tables[0].Rows[(int) dbCursor][i + C_data], System.Convert.ToString(dbSet.Tables[0].Rows[(int) dbCursor][i + C_display_format])), myFont, Brushes.Black, RectangleF.FromLTRB(LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_start_position])), YPosition, LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_end_position])), YPosition + (myFont.GetHeight(e.Graphics) * (LineHeight + MaxRowHeightWrap))), StrFormatCenter);
									}
								}
								//------------------ sum ---------------------
								if ((string) dbSet.Tables[0].Rows[(int) dbCursor][i + C_has_sumarize] == "Y")
								{
									SumList[ColCount]  = System.Convert.ToInt32(SumList[ColCount]  )+System.Convert.ToInt32(  dbSet.Tables[0].Rows[(int) dbCursor][i + C_data]);
								}
								//------------------ end sum -----------------
							}
							ColCount++;
						}
					}
					CountLine +=  (int) (LineHeight + SkipLinePerRecord + MaxRowHeightWrap);
					dbCursor++;
				}
				CountLine--;
				
				if (dbCursor == dbSet.Tables[0].Rows.Count) //----------End of Data and Has Sum
				{
					if (hasSum)
					{
						CountLine++;
						YPosition =  (int) (TopMargin + (CountLine * myFont.GetHeight(e.Graphics)));
						e.Graphics.DrawLine(myPen, LeftMargin, YPosition + (myFont.GetHeight(e.Graphics) / 2), RightMargin, YPosition + (myFont.GetHeight(e.Graphics) / 2));
						
						CountLine++;
						YPosition =  (int) (TopMargin + (CountLine * myFont.GetHeight(e.Graphics)));
						int oldRestInLine = 1;
						for (int j = 1; j <= LineAmountPerRecord; j++)
						{
							if (oldRestInLine < j)
							{
								oldRestInLine = j;
								CountLine +=  (int) (LineHeight);
								YPosition =  (int) (TopMargin + (CountLine * myFont.GetHeight(e.Graphics)));
							}
							ColCountSum = 0;
							for (int i = 0; i <= dbSet.Tables[0].Columns.Count - 1; i += C_amount_of_column)
							{
								if (System.Convert.ToInt32(dbSet.Tables[0].Rows[0][i + C_rest_in_line]) == j )
								{
									
									if ((string) dbSet.Tables[0].Rows[0][i + 4] == "Y")
									{
										if ((string) dbSet.Tables[0].Rows[0][i + C_display_format] == "") //------- Have Format
										{
											if ((string) dbSet.Tables[0].Rows[0][i + C_justify] == "L")
											{
												e.Graphics.DrawString(System.Convert.ToString(SumList[ColCountSum]), myFont, Brushes.Black, RectangleF.FromLTRB(LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_start_position])), YPosition, LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_end_position])), YPosition + myFont.GetHeight(e.Graphics)), StrFormatLeft);
											}
											else if ((string) dbSet.Tables[0].Rows[0][i + C_justify] == "R")
											{
												e.Graphics.DrawString(System.Convert.ToString(SumList[ColCountSum]), myFont, Brushes.Black, RectangleF.FromLTRB(LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_start_position])), YPosition, LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_end_position])), YPosition + myFont.GetHeight(e.Graphics)), StrFormatRight);
											}
											else if ((string) dbSet.Tables[0].Rows[0][i + C_justify] == "C")
											{
												e.Graphics.DrawString(System.Convert.ToString(SumList[ColCountSum]), myFont, Brushes.Black, RectangleF.FromLTRB(LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_start_position])), YPosition, LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_end_position])), YPosition + myFont.GetHeight(e.Graphics)), StrFormatCenter);
											}
										}
										else //-------- Have Format , Yes --------------------
										{
											if ((string) dbSet.Tables[0].Rows[0][i + C_justify] == "L")
											{
												e.Graphics.DrawString(Strings.Format(SumList[ColCountSum], System.Convert.ToString(dbSet.Tables[0].Rows[0][i + C_display_format])), myFont, Brushes.Black, RectangleF.FromLTRB(LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_start_position])), YPosition, LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_end_position])), YPosition + myFont.GetHeight(e.Graphics)), StrFormatLeft);
											}
											else if ((string) dbSet.Tables[0].Rows[0][i + C_justify] == "R")
											{
												e.Graphics.DrawString(Strings.Format(SumList[ColCountSum], System.Convert.ToString(dbSet.Tables[0].Rows[0][i + C_display_format])), myFont, Brushes.Black, RectangleF.FromLTRB(LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_start_position])), YPosition, LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_end_position])), YPosition + myFont.GetHeight(e.Graphics)), StrFormatRight);
											}
											else if ((string) dbSet.Tables[0].Rows[0][i + C_justify] == "C")
											{
												e.Graphics.DrawString(Strings.Format(SumList[ColCountSum], System.Convert.ToString(dbSet.Tables[0].Rows[0][i + C_display_format])), myFont, Brushes.Black, RectangleF.FromLTRB(LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_start_position])), YPosition, LeftMargin + getPositionWidth(e.MarginBounds.Width, System.Convert.ToSingle(dbSet.Tables[0].Rows[0][i + C_end_position])), YPosition + myFont.GetHeight(e.Graphics)), StrFormatCenter);
											}
										}
									}
								}
								ColCountSum++;
							}
						}
						printSumComplete = true;
						
						CountLine++;
						YPosition =  (int) (TopMargin + (CountLine * myFont.GetHeight(e.Graphics)));
						e.Graphics.DrawLine(myPen, LeftMargin, YPosition + (myFont.GetHeight(e.Graphics) / 2), RightMargin, YPosition + (myFont.GetHeight(e.Graphics) / 2));
						e.Graphics.DrawLine(myPen, LeftMargin, 5 + YPosition + (myFont.GetHeight(e.Graphics) / 2), RightMargin, 5 + YPosition + (myFont.GetHeight(e.Graphics) / 2));
						
					}
				}
				
				if (hasSum)
				{
					if (!(dbCursor == dbSet.Tables[0].Rows.Count) && (! printSumComplete))
					{
						e.HasMorePages = true;
					}
					else
					{
						e.HasMorePages = false;
					}
				}
				else
				{
					if (!(dbCursor == dbSet.Tables[0].Rows.Count))
					{
						e.HasMorePages = true;
					}
					else
					{
						e.HasMorePages = false;
					}
				}
			}
			catch (Exception)
			{
				MessageBox.Show("Please send error message to me for better program, Thank you : " + '\r' + '\n' + Information.Err().Description);
			}
		}
		
		public virtual void InitPrint()
		{
			ClearPage();
			prnDoc.DefaultPageSettings.Margins.Left = 50;
			prnDoc.DefaultPageSettings.Margins.Top = 50;
			prnDoc.DefaultPageSettings.Margins.Right = 50;
			prnDoc.DefaultPageSettings.Margins.Bottom = 50;
		}
		
		public virtual void PrintPreview()
		{
			printdlg.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			printdlg.ShowDialog();
		}
		
		public virtual void btnClose_Click(System.Object sender, System.EventArgs e)
		{
			Close();
		}
		
		public virtual void btnPrnDlg_Click(System.Object sender, System.EventArgs e)
		{
			prnSetDlg.ShowDialog();
		}
		
		public virtual void btnDocSet_Click(System.Object sender, System.EventArgs e)
		{
			PageSetDlg.ShowDialog();
		}
		
		public void ClearPage()
		{
			printSumComplete = false;
			PageNumber = 0;
			dbCursor = 0;
			SumList.Clear();
			LineAmountPerRecord = 0;
		}
	}
}
