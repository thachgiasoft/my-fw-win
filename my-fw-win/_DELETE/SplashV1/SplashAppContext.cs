using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public sealed class SplashAppContext : ApplicationContext
    {
        //ProtocolVN.Framework.Lic.ILicence lic = null;
        DevExpress.XtraEditors.XtraForm loginForm = null;
        Timer splashTimer = new Timer();

        public SplashAppContext(DevExpress.XtraEditors.XtraForm loginForm, Form splashForm)
            : base(splashForm)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.loginForm = loginForm;
            splashTimer.Interval = 100;
            splashTimer.Tick += new EventHandler(SplashTimeUp);
            splashTimer.Enabled = true;
            try
            {
                System.Threading.ThreadStart thread = new System.Threading.ThreadStart(FrameworkParams.Custom.InitResourceForApplication);
                System.Threading.Thread thread1 = new System.Threading.Thread(thread);
                thread1.Start();
            }
            catch { }
        }
       
        private void SplashTimeUp(object sender, EventArgs e)
        {
            try
            {
                if (FrameworkParams.Custom.IsFinish())
                {
                    splashTimer.Enabled = false;
                    splashTimer.Dispose();
                    base.MainForm.Close();
                }
            }
            catch { }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        protected override void OnMainFormClosed(object sender, EventArgs e)
        {
            if (sender is SplashForm)
            {
                base.MainForm = this.loginForm;
                if (FrameworkParams.isSkipLogin) ((frmLogin)this.loginForm).LoginAction();                
                if(FrameworkParams.isSkipLogin==false) this.loginForm.Show();                    
            }            
        }
       
        //public int SecondsSplashShown
        //{
        //    set
        //    {
        //        splashTimer.Interval = value * 1000;
        //    }
        //}
    }
}