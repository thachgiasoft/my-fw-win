using System.Collections.Generic;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Win.Dev;
using System;
using System.Windows.Forms;
using System.Text;
using System.Diagnostics;
using ProtocolVN.Plugin.VietInput;
namespace ProtocolVN.Framework.Win
{
    public class XtraFormPL : XtraForm, IProtocolForm
    {
        public XtraFormPL(){
            if (FrameworkParams.FixWaitingForm == true)
            {
                if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
                FrameworkParams.wait = new WaitingMsg();
            }

            if (FrameworkParams.isEnterNextCtrl)
            {
                HelpControl.setEnterAsTab(this);
            }

            HelpApplication.enableVietKey(this);

            this.Load += new EventHandler(XtraFormPL_Load);

            this.Disposed += delegate(object sender, EventArgs e)
            {
                if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
            };
        }

        #region IPermisionable Members

        public List<System.Windows.Forms.Control> GetPermisionableControls()
        {
            List<System.Windows.Forms.Control> controls = FrameworkParams.isPermision.GetPermisionableControls(this);
            if (controls == null) controls = new List<System.Windows.Forms.Control>();
            //Public Form của Plugin
            for (int i = 0; i < PLPlugin.plugins.Count; i++)
            {
                if (PLPlugin.plugins[i].GetPermission() != null)
                {
                    List<System.Windows.Forms.Control> publics = PLPlugin.plugins[i].GetPermission().GetPermisionableControls(this);
                    if (publics != null) controls.AddRange(publics);
                }
            }
            return controls;            
        }
        
        public List<object> GetObjectItems()
        {
            return ApplyPermissionAction.GetGlobalObjectItems(this);
        }
        
        #endregion

        #region IFormatable Members

        public List<System.Windows.Forms.Control> GetFormatControls()
        {
            List<System.Windows.Forms.Control> controls = FrameworkParams.isFormat.GetFormatControls(this);
            if (controls == null) controls = new List<System.Windows.Forms.Control>();
            //Public Form của Plugin
            for (int i = 0; i < PLPlugin.plugins.Count; i++)
            {
                if (PLPlugin.plugins[i].GetFormat() != null)
                {
                    List<System.Windows.Forms.Control> publics = PLPlugin.plugins[i].GetFormat().GetFormatControls(this);
                    if (publics != null) controls.AddRange(publics);
                }
            }
            return controls;
        }

        #endregion
        
        private RightClickTitleBarDialog titleMenu = null;
        void XtraFormPL_Load(object sender, EventArgs e)
        {
            if (this.IsMdiChild == false)
            {
                this.titleMenu = new RightClickTitleBarDialog(this);
            }
            if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
        }

        //private int count = 0; 
        protected override void WndProc(ref Message msg)
        {
            //#region PHUOCNT TODO Kiểm tra tình huống sao lập vô tận
            //count++;
            //if (count == 10)
            //{
            //    count = 0;
            //    return;
            //}
            //#endregion

            if (titleMenu != null)
            {
                titleMenu.execMenuItem(ref msg);
                //base.WndProc(ref msg);
            }
            base.WndProc(ref msg);
            
        }
    }
}
