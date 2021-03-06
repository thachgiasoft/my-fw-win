using System;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// PHUOCNC Chưa ổn định
    /// Lớp cho phép hiển thị form chờ đợi khi thực hiện một công việc đòi hỏi nhiều thời gian
    /// Form này có thanh ProgressBar nói lên tiến độ thực hiện.
    /// </summary>
    [Obsolete("Sử dụng HelpWaiting.LongProcess")]
    public partial class TrialWaitingBox : XtraForm, IPublicForm
    {
        TrialPLProgressBar Pro;
        public long estimateTime = 0;
        public TrialWaitingBox(ThreadStart process)
        {
            InitializeComponent();
            Pro = new TrialPLProgressBar(pbcWait, process);            
        }

        private void frmWaiting_Shown(object sender, EventArgs e)
        {
            Pro.Run(estimateTime);
            this.Close();
        }

        public static void LongProcess(XtraForm mainForm, ThreadStart process, long estimateTime)
        {
            HelpWaiting.longProcess(mainForm, process, estimateTime);
            //if (estimateTime == -1)
            //    estimateTime = 1;
            //mainForm.Cursor = Cursors.WaitCursor;
            //TrialWaitingBox frm = new TrialWaitingBox(process);
            //frm.estimateTime = estimateTime;
            //frm.ShowDialog(mainForm);
            //mainForm.Cursor = Cursors.Default;
        }
    }
}