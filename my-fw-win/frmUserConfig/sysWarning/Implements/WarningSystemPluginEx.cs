using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;
using System.Data.Common;
using DevExpress.Utils;
using ProtocolVN.Framework.Core;
using Timer=System.Threading.Timer;
using ProtocolVN.Framework.Win;

namespace ProtocolVN.Plugin.WarningSystem
{
    public class WarningSystemPluginEx
    {
        /// <summary>
        /// Danh sách các warning cần giám sát
        /// </summary>
        
        private static System.Threading.Timer[] timer;
        private Dictionary<string, Timer> _dicRunWarning;

        private List<IWarningDefine> _warnings;
        private List<IWarningDefine> _warningFirst;
        private List<IWarningDefine> _warningTime;
        
       
        public WarningSystemPluginEx()
        {
            _dicRunWarning = new Dictionary<string, Timer>();

            _warnings = new List<IWarningDefine>();            
            _warningFirst = new List<IWarningDefine>();
            _warningTime = new List<IWarningDefine>();            
        }

        public WarningSystemPluginEx(List<IWarningDefine> warnings)
        {
            _warnings = warnings;
            _warningFirst = new List<IWarningDefine>();
            _warningTime = new List<IWarningDefine>();            
            _dicRunWarning = new Dictionary<string, Timer>();
        }

        //Phân loại warning chạy theo thời gian và warning chạy lúc đầu tiên.
        private void GetWarningType()
        {
            foreach (IWarningDefine war in _warnings)
                if (war.Type == WarningExecType.LoopTime)
                    _warningTime.Add(war);
                else
                    _warningFirst.Add(war);
        }

        #region Action

        /// <summary>
        /// Bắt đầu chạy giám sát
        /// Khởi tạo một thread cho tất cả các giám sát
        /// </summary>        
        public void Start()
        {         
            try
            {
                GetWarningType();
                runFirstTime();
                runSupervise();
            }
            catch { }

        }
   
        private void runFirstTime()
        {
            foreach (IWarningDefine w in _warningFirst)
                //w.Start();
                ShowMsg(w);
        }
        /// <summary>
        /// Hàm thực hiện giám sát từng loại warning
        /// </summary>
        private void runSupervise()
        {
            int _period;
            int i = 0;
            timer = new Timer[_warningTime.Count];
            foreach (IWarningDefine w in _warningTime)
            {
                _period = w.getPeriod();
                timer[i] = new System.Threading.Timer(new TimerCallback(ShowMsg), w, 0, _period);
                if (!_dicRunWarning.ContainsKey(w.Name))
                    _dicRunWarning.Add(w.Name, timer[i]);
                i++;
            }
        }
        
        static void ShowMsg(object data)
        {
            try
            {
                lock (new Object())
                {
                    IWarningDefine warning = (IWarningDefine)data;
                    PLOut outputType = warning.getOutputType();
                    string msg = warning.Supervise() as string;

                    if (msg != "")
                    {
                        outputType.write("Warning", msg);
                    }
                    else
                    {
                        outputType.close(null);
                    }
                        
                }
            }
            catch { }
           
        }

        public void Stop(params string[] names)
        {
            try
            {
                foreach (string name in names)
                {
                    Timer time;
                    _dicRunWarning.TryGetValue(name, out time);
                    if (timer != null) time.Dispose();
                }
            }
            catch { }
        }

        public void StopAll()
        {
            int countwarning = _warningTime.Count;
            for (int i = 0; i < countwarning; i++)
                if (timer[i] != null) timer[i].Dispose();
        }
        #endregion
    }
}
