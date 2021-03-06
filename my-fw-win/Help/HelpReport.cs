using System;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.Data;
using DevExpress.XtraEditors;
using CrystalDecisions.CrystalReports.Engine;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using ProtocolVN.Framework.Win;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Import;
using ProtocolVN.Framework.Core;
using System.Collections;

namespace ProtocolVN.Framework.Win
{
    /// <summary>Lớp hỗ trợ làm với báo biểu
    /// </summary>
    public class HelpReport
    {
        /// <summary>Xem trước Crystal Report
        /// </summary>
        /// <param name="mainForm">Màn hình gọi report</param>
        /// <param name="_reportFile">Tên tập tin report</param>
        /// <param name="_parameter">Tham số</param>
        /// <param name="_mainDataSet"></param>
        /// <param name="_subreportDataSet"></param>
        /// <param name="subReportFileNames">Tên các tập tin làm subReport</param>
        public static void Preview(XtraForm mainForm, String _reportFile, Dictionary<string, object> _parameter, DataSet _mainDataSet, DataSet[] _subreportDataSet, string[] subReportFileNames)
        {
            ReportHelp helpReport = new ReportHelp(_reportFile, _parameter, _mainDataSet, _subreportDataSet, subReportFileNames);
            XtraForm form = helpReport.preview();
            if(form != null) ProtocolForm.ShowModalDialog(mainForm, form);
        }
        
        [Obsolete("TODO: Kiểm tra lại - Không dùng vì không thể có SubReport mà không truyền filename của SubReport.")]
        public static void Preview(XtraForm mainForm, String _reportFile, Dictionary<string, object> _parameter, DataSet _mainDataSet, DataSet[] _subreportDataSet)
        {
            Preview(mainForm, _reportFile, _parameter, _mainDataSet, _subreportDataSet, null);
        }

        /// <summary>Xem trước Crystal Report
        /// </summary>
        /// <param name="mainForm">Màn hình gọi report</param>
        /// <param name="_reportFile">Tên tập tin report</param>
        /// <param name="_parameter"></param>
        /// <param name="_mainDataSet"></param>
        public static void Preview(XtraForm mainForm, String _reportFile, Dictionary<string, object> _parameter, DataSet _mainDataSet)
        {
            Preview(mainForm, _reportFile, _parameter, _mainDataSet, null, null);
        }

        /// <summary>Xem trước Crystall Report - DynReport.
        /// </summary>
        /// <param name="mainForm"></param>
        /// <param name="ReportType"></param>
        /// <param name="Source"></param>
        /// <param name="FieldNames"></param>
        /// <param name="Captions"></param>
        /// <param name="ExpectedWidths"></param>
        /// <param name="Title"></param>
        /// <param name="SubTitle"></param>
        /// <returns></returns>
        public static XtraForm Preview(XtraForm mainForm, PLDynRepType ReportType,
            DataSet Source, string[] FieldNames, string[] Captions, int[] ExpectedWidths,
            string Title, string SubTitle)
        {
            return DynamicSheetReport.Preview(mainForm, ReportType, Source, FieldNames, 
                Captions, ExpectedWidths, Title, SubTitle);
        }

        /// <summary>Xem trước Crystall Report 
        /// </summary>
        public static void Preview(_Print PrintObj)
        {
            PrintObj.execPreviewWith();
        }
        




        //In trực tiếp
        public static void Print(String _reportFile, Dictionary<string, object> _parameter, DataSet _mainDataSet, DataSet[] _subreportDataSet,
                                 string _printerName, string _paperSize, int _marginLeft, int _marginRight, int _marginTop, int _marginBottom)
        {
            ReportHelp helpReport = new ReportHelp(_reportFile, _parameter, _mainDataSet, _subreportDataSet);
            helpReport.paperSetup(_printerName, _paperSize, _marginLeft, _marginRight, _marginTop, _marginBottom);
            if (helpReport.print() == false)
            {
                HelpMsgBox.ShowNotificationMessage("Lỗi máy in");
            }
        }
        public static void Print(String _reportFile, Dictionary<string, object> _parameter, DataSet _mainDataSet, DataSet[] _subreportDataSet){
            Print(_reportFile, _parameter, _mainDataSet, _subreportDataSet, FrameworkParams.option.printerName, "", 0, 0, 0, 0);
        }

        public static void Print(_Print PrintObj)
        {
            PrintObj.execDirectlyPrint();
        }


        //Chọn máy in trước khi in
        public static void PrintShowDialog(String _reportFile, Dictionary<string, object> _parameter, DataSet _mainDataSet, DataSet[] _subreportDataSet, string[] subReportFileNames)
        {
            ReportHelp helpReport = new ReportHelp(_reportFile, _parameter, _mainDataSet, _subreportDataSet, subReportFileNames);
            helpReport.printSelectedPrinter();
        }

        public static void PrintShowDialog(String _reportFile, Dictionary<string, object> _parameter, DataSet _mainDataSet, DataSet[] _subreportDataSet)
        {
            ReportHelp helpReport = new ReportHelp(_reportFile, _parameter, _mainDataSet, _subreportDataSet);
            helpReport.printSelectedPrinter();
        }

        public static void PrintShowDialog(String _reportFile, Dictionary<string, object> _parameter, DataSet _mainDataSet){
            PrintShowDialog(_reportFile, _parameter, _mainDataSet, null);
        }

        public static void PrintShowDialog(_Print PrintObj)
        {
            PrintObj.execPintShowDialog();
        }

        //PHUOCNT TODO: Chưa sử dụng
        public bool SetPrinterToDefault(string printer)
        {
            //path we need for WMI
            string queryPath = "win32_printer.DeviceId='" + printer + "'";
            try
            {
                //ManagementObject for doing the retrieval
                System.Management.ManagementObject managementObj = new System.Management.ManagementObject(queryPath);
                //ManagementBaseObject which will hold the results of InvokeMethod
                System.Management.ManagementBaseObject obj = managementObj.InvokeMethod("SetDefaultPrinter", null, null);
                //if we're null the something went wrong
                if (obj == null)
                    throw new Exception("Không cho phép chọn máy in mặc định.");
                //now get the return value and make our decision based on that
                int result = (int)obj.Properties["ReturnValue"].Value;
       
                if (result == 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static ParameterField CreateParameter(string ParamName, object Value)
        {
            ParameterField paramField = new ParameterField();
            paramField.Name = ParamName;
            ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = Value;
            paramField.CurrentValues.Add(paramDiscreteValue);

            return paramField;
        }

        //PHUOCNC Sửa lại cho tổng quát hơn
        //FieldName = Caption + index
        public static ParameterFields CalcParameters(string[] Captions)
        {
            //Không được thay đổi vì Template Report mình dùng những Field có tên là vậy          
            string Caption = "Caption";
            int MAX_COL = 15;

            ParameterFields paramFields = new ParameterFields();
            ParameterField paramField = null;
            ParameterDiscreteValue paramValue = null;
            for (int i = 0; i < Captions.Length; i++)
            {
                paramValue = new ParameterDiscreteValue();
                paramValue.Description = "Caption" + i;
                paramValue.Value = Captions[i];

                paramField = HelpCrystalReport.CreateParameter(Caption + i, Captions[i]);
                paramField.CurrentValues.Add(paramValue);
                paramFields.Add(paramField);

                //paramFields.Add(HelpCrystalReport.CreateParameter(Caption + i, Captions[i]));
            }

            for (int i = Captions.Length; i < MAX_COL; i++)
            {
                paramValue = new ParameterDiscreteValue();
                paramValue.Description = "Caption" + i;
                paramValue.Value = "";

                paramField = HelpCrystalReport.CreateParameter(Caption + i, "");
                paramField.CurrentValues.Add(paramValue);
                paramFields.Add(paramField);

                //paramFields.Add(HelpCrystalReport.CreateParameter(Caption + i, ""));
            }
            //Set value for first parameter






            return paramFields;
        }

        /// <summary>Hàm kiểm tra xem máy đã cài máy in chưa
        /// </summary>
        public static bool HasPrinter()
        {
            if (PrinterSettings.InstalledPrinters.Count == 0)
                return false;
            return true;
        }

        private PrinterSettings.PaperSizeCollection PopulatePaperSizesOfPrinter(string PrinterName)
        {
            try
            {
                PrinterSettings printSet = new PrinterSettings();
                printSet.PrinterName = PrinterName;
                return printSet.PaperSizes;
            }
            catch
            {
                return null;
            }
        }
    }
}
