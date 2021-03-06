using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using ProtocolVN.Framework.Core;
namespace ProtocolVN.Framework.Win
{    
    public class ApplyFormatAction : ApplyAction
    {        
        private Option opt;
        public ApplyFormatAction()
        {            
            this.opt = FrameworkParams.option;
            
            Application.CurrentCulture = GetCultureInfo();
        }
        public static CultureInfo Culture;
        public static CultureInfo GetCultureInfo()
        {
            if (Culture == null)
            {
                CultureInfo cti = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                // Tao format rieng cua ung dung
                Option opt = FrameworkParams.option;
                // Number format
                if (opt != null)
                {
                    cti.NumberFormat.NumberGroupSeparator = opt.thousandSeparator;
                    cti.NumberFormat.NumberDecimalSeparator = opt.decSeparator;

                    cti.DateTimeFormat.ShortDatePattern = opt.dateFormat;
                    cti.DateTimeFormat.ShortTimePattern = opt.timeFormat;
                    cti.DateTimeFormat.FullDateTimePattern = opt.dateFormat + " " + opt.timeFormat;
                    cti.DateTimeFormat.AbbreviatedDayNames = new string[]{
                        "CN", "T2", "T3", "T4", "T5", "T6", "T7"
                    };
                    cti.DateTimeFormat.DayNames = new string[]{
                        "CN","T2", "T3", "T4", "T5", "T6", "T7"
                    };
                    cti.DateTimeFormat.MonthNames = new string[]{
                        "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", 
                        "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12", ""
                    };
                    cti.DateTimeFormat.MonthGenitiveNames = new string[]{
                        "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", 
                        "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12", ""
                    };
                    cti.NumberFormat.NumberDecimalDigits = HelpNumber.ParseInt32(opt.round);                   
                }
                Culture = cti;
            }
            return Culture;
        }

        #region ApplyAction Members
        private string GetEditMask(int num)
        {
            //CultureInfo cti = GetCultureInfo();
            //string ret = "###" + //cti.NumberFormat.NumberGroupSeparator +
            //             "###" + //cti.NumberFormat.NumberGroupSeparator +
            //             "###" + //cti.NumberFormat.NumberGroupSeparator +
            //             "###";
            //if (num > 0)
            //{
            //    ret += cti.NumberFormat.NumberDecimalSeparator;
            //    for (int i = 0; i < num; i++)
            //    {
            //        ret += "#";
            //    }
            //}
            //return ret;
            if (num == 0) return "n";
            return "n" + num.ToString();
        }
        // Nhan biet format cua control qua thuoc tinh MaskType
        public override bool applyControl(System.Windows.Forms.Control Ctrl, XtraForm form)
        {
            CultureInfo cti = (CultureInfo)GetCultureInfo().Clone();
            #region TextEdit  OK 
            if (Ctrl != null && Ctrl.GetType().ToString() == "DevExpress.XtraEditors.TextEdit")
            {
                TextEdit control = (DevExpress.XtraEditors.TextEdit)Ctrl;

                if (control.Properties.Mask.MaskType == DevExpress.XtraEditors.Mask.MaskType.Numeric)
                {
                    int tmp = cti.NumberFormat.NumberDecimalDigits;
                    if (control.Properties.Mask.EditMask != null &&
                        control.Properties.Mask.EditMask != "")
                    {
                        tmp = HelpNumber.ParseInt32(control.Properties.Mask.EditMask);
                    }
                    if (tmp == 0) cti.NumberFormat.NumberDecimalDigits = 0;
                    control.Properties.Mask.Culture = cti;
                    control.Properties.Mask.EditMask = GetEditMask(tmp);
                    control.Properties.Mask.UseMaskAsDisplayFormat = true;
                }
            }
            #endregion
            #region TimeEdit  OK
            else if ((Ctrl != null && Ctrl.GetType().ToString() == "DevExpress.XtraEditors.TimeEdit") ||
                     (Ctrl != null && Ctrl.GetType().ToString() == "DevExpress.XtraEditors.PLTimeEdit"))
            {
                TimeEdit control = (DevExpress.XtraEditors.TimeEdit)Ctrl;
                if (control.Properties.Buttons.Count < 1)
                    control.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                        new DevExpress.XtraEditors.Controls.EditorButton()});
                control.Properties.Mask.EditMask = FrameworkParams.option.timeFormat;
                control.Properties.Mask.UseMaskAsDisplayFormat = true;
            }
            #endregion
            #region CalcEdit OK 
            else if ((Ctrl != null && Ctrl.GetType().ToString() == "DevExpress.XtraEditors.CalcEdit") ||
                     (Ctrl != null && Ctrl.GetType().ToString() == "DevExpress.XtraEditors.PLCalcEdit"))
            {
                CalcEdit control = (DevExpress.XtraEditors.CalcEdit)Ctrl;
                
                int tmp = cti.NumberFormat.NumberDecimalDigits;

                if (control.Properties.Mask.EditMask != null &&
                    control.Properties.Mask.EditMask != "")
                {
                    tmp = HelpNumber.ParseInt32(control.Properties.Mask.EditMask);
                }

                control.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                if (tmp == 0) cti.NumberFormat.NumberDecimalDigits = 0;
                control.Properties.Mask.Culture = cti;
                
                control.Properties.Mask.EditMask = GetEditMask(tmp);
                control.Properties.Mask.UseMaskAsDisplayFormat = true;
            }
            //else if (Ctrl != null && Ctrl.GetType().ToString() == "DevExpress.XtraEditors.PLCalcEdit")
            //{
            //    CalcEdit control = (DevExpress.XtraEditors.CalcEdit)Ctrl;

            //    int tmp = cti.NumberFormat.NumberDecimalDigits;

            //    if (control.Properties.Mask.EditMask != null &&
            //        control.Properties.Mask.EditMask != "")
            //    {
            //        tmp = HelpNumber.ParseInt32(control.Properties.Mask.EditMask);
            //    }

            //    control.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            //    if (tmp == 0) cti.NumberFormat.NumberDecimalDigits = 0;
            //    control.Properties.Mask.Culture = cti;      
          
            //    control.Properties.Mask.EditMask = GetEditMask(tmp);
            //    control.Properties.Mask.UseMaskAsDisplayFormat = true;
            //}
            #endregion
            #region SpinEdit
            else if ((Ctrl != null && Ctrl.GetType().ToString() == "DevExpress.XtraEditors.SpinEdit") ||
                     (Ctrl != null && Ctrl.GetType().ToString() == "DevExpress.XtraEditors.PLSpinEdit"))
            {
                SpinEdit control = (DevExpress.XtraEditors.SpinEdit)Ctrl;

                int tmp = cti.NumberFormat.NumberDecimalDigits;

                if (control.Properties.Mask.EditMask != null &&
                    control.Properties.Mask.EditMask != "")
                {
                    tmp = HelpNumber.ParseInt32(control.Properties.Mask.EditMask);
                }

                control.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                if (tmp == 0) cti.NumberFormat.NumberDecimalDigits = 0;
                control.Properties.Mask.Culture = cti;
                control.Properties.Mask.EditMask = GetEditMask(tmp);
                control.Properties.Mask.UseMaskAsDisplayFormat = true;
            }
            //else if (Ctrl != null && Ctrl.GetType().ToString() == "DevExpress.XtraEditors.PLSpinEdit")
            //{
            //    SpinEdit control = (DevExpress.XtraEditors.SpinEdit)Ctrl;

            //    int tmp = cti.NumberFormat.NumberDecimalDigits;
            //    if (control.Properties.Mask.EditMask != null &&
            //        control.Properties.Mask.EditMask != "")
            //    {
            //        tmp = HelpNumber.ParseInt32(control.Properties.Mask.EditMask);
            //    }

            //    control.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            //    if (tmp == 0) cti.NumberFormat.NumberDecimalDigits = 0;
            //    control.Properties.Mask.Culture = cti;
            //    control.Properties.Mask.EditMask = GetEditMask(tmp);
            //    control.Properties.Mask.UseMaskAsDisplayFormat = true;
            //}
            #endregion
            return true;
        }
        
                

        #endregion
        public override bool applyElement(object element, XtraForm form)
        {
            return true;
        }
        public static bool applyElement(object element, object param)
        {
            CultureInfo cti = GetCultureInfo();
            if ((element as RepositoryItemCalcEdit) != null)
            {
                RepositoryItemCalcEdit item = (RepositoryItemCalcEdit)element;
                item.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                item.Properties.Mask.UseMaskAsDisplayFormat = true;
                int tmp = cti.NumberFormat.NumberDecimalDigits;
                int numDec = HelpNumber.ParseInt32(param.ToString());
                if (numDec == -1)//So nguyen
                    numDec = 0;
                else if (numDec == -2) //So chu so thap phan cua Option
                    numDec = tmp;

                item.Properties.Mask.EditMask = "n" + numDec.ToString();
                //if (numDec > 0) item.Properties.Mask.EditMask = "n" + numDec.ToString();
                //else item.Properties.Mask.EditMask = "n";

                item.Properties.DisplayFormat.FormatString = GetDisplayFormat(numDec);
                item.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                item.Properties.Mask.Culture = cti;
            }
            else if ((element as RepositoryItemSpinEdit) != null)
            {
                RepositoryItemSpinEdit item = (RepositoryItemSpinEdit)element;
                item.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;                
                item.Properties.Mask.UseMaskAsDisplayFormat = true;
                int tmp = cti.NumberFormat.NumberDecimalDigits;
                int numDec = HelpNumber.ParseInt32(param.ToString());
                if (numDec == -1) numDec = 0;
                else if (numDec == -2) numDec = tmp;

                item.Properties.Mask.EditMask = "n" + numDec.ToString();
                //if(numDec>0) item.Properties.Mask.EditMask = "n" + numDec.ToString();
                //else item.Properties.Mask.EditMask = "n";

                item.Properties.DisplayFormat.FormatString = GetDisplayFormat(numDec);
                item.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                item.Properties.Mask.Culture = cti;                
            }
            else if ((element as RepositoryItemTimeEdit) != null)
            {
                RepositoryItemTimeEdit timeEdit = (RepositoryItemTimeEdit)element;
                if(param==null) timeEdit.Mask.EditMask = FrameworkParams.option.timeFormat;
                else timeEdit.Mask.EditMask = param.ToString();
                timeEdit.Mask.UseMaskAsDisplayFormat = true;
            }
            return true;
        }
        
        public static string GetDisplayFormat(int DecimalNum)
        {
            string ret = "{0:#,##0";
            if (DecimalNum > 0) ret += ".";
            for(int i = 0; i < DecimalNum; i++){
                ret += "#";
            }
            return ret + "}";
        }

        public static string GetCurrentDisplayFormat()
        {
            int decNum = HelpNumber.ParseInt32(FrameworkParams.option.round);
            return GetDisplayFormat(decNum);
        }
    }

    public interface IFormat
    {
        List<Control> GetFormatControls(XtraForm formObj);
    }

    public class FormatParams
    {
        public static int SO_TIEN = 2;
        public static int SO_TRONG_LUONG = 2;
        public static int SO_LUONG = 0;
    }
}