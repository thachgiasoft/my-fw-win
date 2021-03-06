//using NLog;
//using NLog.Targets;
using ProtocolVN.Framework.Core;
using ProtocolVN.Framework.Win;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraBars;
using System.Data;
using System.Collections.Generic;
using System;

namespace ProtocolVN.Framework.Win
{
    public class HelpUserLog
    {
        public static void log(string msg)
        {
            if (FrameworkParams.isLog != null)
                DAUserLog.Instance.insert(FrameworkParams.currentUser.id, msg);
        }

        public static void logOpenForm(XtraForm frm)
        {
            try {
                if (FrameworkParams.isLog != null && !(frm is INonLog))
                {
                    HelpUserLog.log("Mở màn hình '" + frm.Text + "'");

                    //Gắn sự kiện để log các control trong form
                    List<LogItem> ls = FrameworkParams.isLog.Log(frm);
                    for (int i = 0; i < ls.Count; i++)
                    {
                        HelpUserLog.addEventLog(ls[i]);
                    }
                }
            }catch(Exception ex){
                PLException.AddException(ex);
            }
        }

        public static void logCloseForm(XtraForm frm)
        {
            if (FrameworkParams.isLog != null && !(frm is INonLog))
            {
                try {
                    HelpUserLog.log("Đóng màn hình '" + frm.Text + "'");
                }catch(Exception ex){
                    PLException.AddException(ex);
                }
            }
        }

        public static void addEventLog(LogItem logItem)
        {
            object item = logItem.Item;

            if ((item as ToolStripMenuItem) != null)
            {
                object obj = ((ToolStripMenuItem)item).Tag;
                ((ToolStripMenuItem)item).Click += new System.EventHandler(ToolStripMenuItemClick);
                TagPropertyMan.InsertOrUpdate(ref obj, "LOG", logItem.ContentLog);
                ((ToolStripMenuItem)item).Tag = obj;
            }
            else if ((item as BarItem) != null)
            {
                object obj = ((BarItem)item).Tag;
                ((BarItem)item).ItemClick += new ItemClickEventHandler(BarItemClick);
                TagPropertyMan.InsertOrUpdate(ref obj, "LOG", logItem.ContentLog);
                ((BarItem)item).Tag = obj;
            }
            else if ((item as ToolStripButton) != null)
            {
                object obj = ((ToolStripButton)item).Tag;
                ((ToolStripButton)item).Click += new System.EventHandler(ToolStripClick);
                TagPropertyMan.InsertOrUpdate(ref obj, "LOG", logItem.ContentLog);
                ((ToolStripButton)item).Tag = obj;
            }
            else if ((item as SimpleButton) != null)
            {
                object obj = ((SimpleButton)item).Tag;
                ((SimpleButton)item).Click += new System.EventHandler(SimpleButtonClick);
                TagPropertyMan.InsertOrUpdate(ref obj, "LOG", logItem.ContentLog);
                ((SimpleButton)item).Tag = obj;
            }
            //else if ((item as XtraUserControl) != null)
            //{
            //    object obj = ((XtraUserControl)item).Tag;
            //    TagPropertyMan.InsertOrUpdate(ref obj, "LOG", permission);
            //    ((XtraUserControl)item).Tag = obj;
            //}
            //else if ((item as GridColumn) != null)
            //{
            //    object obj = ((GridColumn)item).Tag;
            //    TagPropertyMan.InsertOrUpdate(ref obj, "LOG", permission);
            //    ((GridColumn)item).Tag = obj;
            //}
            //else if ((item as TextEdit) != null)
            //{
            //    object obj = ((TextEdit)item).Tag;
            //    TagPropertyMan.InsertOrUpdate(ref obj, "LOG", permission);
            //    ((TextEdit)item).Tag = obj;
            //}
            //else if ((item as CalcEdit) != null)
            //{
            //    object obj = ((CalcEdit)item).Tag;
            //    TagPropertyMan.InsertOrUpdate(ref obj, "LOG", permission);
            //    ((CalcEdit)item).Tag = obj;
            //}
            //else if ((item as SpinEdit) != null)
            //{
            //    object obj = ((SpinEdit)item).Tag;
            //    TagPropertyMan.InsertOrUpdate(ref obj, "LOG", permission);
            //    ((SpinEdit)item).Tag = obj;
            //}
            else
            {
                HelpDevError.ShowMessage("AddEventLog : Item: " + item.GetType().Name + " chưa được hỗ trợ phân quyền.");
            }
            
        }

        


        private static void ToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            ProcessClick(sender);
        }

        private static void BarItemClick(object sender, ItemClickEventArgs e)
        {
            ProcessClick(sender);
        }

        private static void SimpleButtonClick(object sender, System.EventArgs e)
        {
            ProcessClick(sender);
        }

        private static void ToolStripClick(object sender, System.EventArgs e)
        {
            ProcessClick(sender);
        }

        private static void ProcessClick(object element)
        {
            #region ToolStripMenuItem nằm trên menu tool strip
            string tagValue = "";

            if ((element as ToolStripMenuItem) != null)
            {
                if (TagPropertyMan.Get(((ToolStripMenuItem)element).Tag, "LOG") != null)
                    tagValue = (string)TagPropertyMan.Get(((ToolStripMenuItem)element).Tag, "LOG");
                log(tagValue);
            }
            #endregion
            #region BarItem nằm trên Bars, BarManager
            else if ((element as BarItem) != null)
            {
                BarItem barItem = (BarItem)element;
                if (TagPropertyMan.Get(barItem.Tag, "LOG") != null)
                    tagValue = (string)TagPropertyMan.Get(barItem.Tag, "LOG");
                log(tagValue);
            }
            #endregion
            #region ToolStripButton
            else if ((element as ToolStripButton) != null)
            {
                ToolStripButton item = (ToolStripButton)element;
                if (TagPropertyMan.Get(item.Tag, "LOG") != null)
                    tagValue = (string)TagPropertyMan.Get(item.Tag, "LOG");
                log(tagValue);
            }
            #endregion
            #region SimpleButton
            else if ((element as SimpleButton) != null)
            {
                SimpleButton item = (SimpleButton)element;
                if (TagPropertyMan.Get(item.Tag, "LOG") != null)
                    tagValue = (string)TagPropertyMan.Get(item.Tag, "LOG");
                log(tagValue);
            }
            #endregion
            //#region XtraUserControl
            //else if ((element as XtraUserControl) != null)
            //{
            //    XtraUserControl item = (XtraUserControl)element;

            //    if (TagPropertyMan.Get(item.Tag, "LOG") != null)
            //        tagValue = (PermissionItem)TagPropertyMan.Get(item.Tag, "LOG");            
            //}
            //#endregion
            //#region GridColumn
            //else if ((element as GridColumn) != null)
            //{
            //    GridColumn item = (GridColumn)element;
            //    if (TagPropertyMan.Get(item.Tag, "LOG") != null)
            //        tagValue = (PermissionItem)TagPropertyMan.Get(item.Tag, "LOG");
            //}
            //#endregion
            //#region TextEdit
            //else if ((element as TextEdit) != null)
            //{
            //    TextEdit item = (TextEdit)element;
            //    if (TagPropertyMan.Get(item.Tag, "LOG") != null)
            //        tagValue = (PermissionItem)TagPropertyMan.Get(item.Tag, "LOG");
            //}
            //#endregion
            //#region SpinEdit
            //else if ((element as SpinEdit) != null)
            //{
            //    SpinEdit item = (SpinEdit)element;
            //    if (TagPropertyMan.Get(item.Tag, "LOG") != null)
            //        tagValue = (PermissionItem)TagPropertyMan.Get(item.Tag, "LOG");
            //}
            //#endregion
            //#region CalcEdit
            //else if ((element as CalcEdit) != null)
            //{
            //    CalcEdit item = (CalcEdit)element;
            //    if (TagPropertyMan.Get(item.Tag, "LOG") != null)
            //        tagValue = (PermissionItem)TagPropertyMan.Get(item.Tag, "LOG");
            //}
            //#endregion
            else
            {
                HelpDevError.ShowMessage("ProcessClick : Item: " + element.GetType().Name + " chưa được hỗ trợ phân quyền.");
            }
        }
    }
}