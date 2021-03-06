using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public partial class PLDateTime : DevExpress.XtraEditors.XtraUserControl
    {
        public PLDateTime()
        {
            InitializeComponent();
        }

        public void _init()
        {
            // định dạng lại thời gian theo kiểu giờ phút
            if (ThoiGian.Properties.Buttons.Count < 1)
                ThoiGian.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                                        new DevExpress.XtraEditors.Controls.EditorButton()});
            ThoiGian.Properties.Mask.EditMask = FrameworkParams.option.timeFormat;
            ThoiGian.Properties.Mask.UseMaskAsDisplayFormat = true;
        }

        public void _setNow()
        {
            Ngay.DateTime = DateTime.Now;
            ThoiGian.EditValue = Ngay.DateTime.TimeOfDay;
        }

        public TimeSpan? _getTime()
        {
            try
            {
                if (ThoiGian.Time.TimeOfDay.Hours == 0 && ThoiGian.Time.TimeOfDay.Minutes == 0 && ThoiGian.Time.TimeOfDay.Seconds == 0 && ThoiGian.Time.TimeOfDay.Milliseconds == 0)
                    return null;
                else
                    return ThoiGian.Time.TimeOfDay;
            }
            catch { return null; }
        }

        public DateTime? _getDate()
        {
            try
            {
                if(HelpDate.IsBlankDate(Ngay.DateTime))
                    return null;
                return Ngay.DateTime;
            }
            catch { return null; }

        }

        public void _setDateTime(DateTime d, TimeSpan t)
        {
            try
            {
                Ngay.EditValue = d;
                ThoiGian.EditValue = t;
            }
            catch { }
        }
        public void _setDateTime(DateTime? d, TimeSpan? t)
        {
            try
            {
                if(d!=null) Ngay.EditValue = d;
                if(t!=null) ThoiGian.EditValue = t;
            }
            catch { }
        }
        public void _setDate(DateTime? d)
        {
            try
            {
                if (d != null) Ngay.EditValue = d;
            }
            catch { }
        }
        public void _setTime(TimeSpan? t)
        {
            try
            {
                if (t != null) ThoiGian.EditValue = t;
            }
            catch { }
        }

        public void _setDateTime(DateTime? d)
        {
            try{
                if(d!=null) Ngay.EditValue = d;
                ThoiGian.EditValue = d.Value.TimeOfDay;
            }catch{
            }
        }
        public void _setDateTime(DateTime d)
        {
            try
            {
                if (d != null)
                {
                    Ngay.EditValue = d;
                    ThoiGian.EditValue = d.TimeOfDay;
                }
            }
            catch
            {
            }
        }
        public DateTime? _getDateTime()
        {
            try
            {
                DateTime? d = new DateTime(Ngay.DateTime.Year, Ngay.DateTime.Month, Ngay.DateTime.Day,
                    ((TimeSpan)ThoiGian.EditValue).Hours, ((TimeSpan)ThoiGian.EditValue).Minutes, ((TimeSpan)ThoiGian.EditValue).Seconds);
                return d;
            }
            catch { return null; }
        }

        public void SetError(DXErrorProvider errorProvider, string errorMsg)
        {
            errorProvider.SetError(this.Ngay, errorMsg);
        }
    }
}
