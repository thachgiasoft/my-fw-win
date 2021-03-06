using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public class RibbonQuickAccess:MenuToolbar
    {
        private RibbonControl ribbonControl;
        public RibbonQuickAccess(frmRibbonMain main, string username)
        {
            main.RibbonCtrl.ToolbarLocation = RibbonQuickAccessToolbarLocation.Default;
            //Không dùng chọn lựa bên trên
            string ToolBarXML = "<?xml version='1.0' encoding='utf-8' standalone='yes'?><vn>";
            ToolBarXML += FrameworkParams.QuickAccessMenu;
            ToolBarXML += FWMenuFactory.CreateQuickAccess();
            ToolBarXML += "</vn>";

            try
            {
                InitMenuToolbar(main, ToolBarXML, username);
                drawToolbar();
            }
            catch { }
        }

        private void drawToolbar()
        {
            ribbonControl = ((frmRibbonMain)this.mainForm).RibbonCtrl;
            foreach (DataRow dr in ds.Tables[0].Select("Parents='1'"))
            {
                string strID = dr[0].ToString();
                createItem(strID);
            }
        }

        private void createChildItem(BarItem itemParent,string itemId)
        {
            BarSubItem parentBar = (BarSubItem)itemParent;
            if (ds.Tables[0].Select("Parents='" + itemId + "'").Length > 0)
            {
                BarSubItem subItem = new BarSubItem();
                subItem.Id = frmRibbonMain.IIII++;
                subItem.Caption = getName(itemId);
                subItem.RibbonStyle = RibbonItemStyles.Default;
                subItem.PaintStyle = BarItemPaintStyle.CaptionGlyph;
                //subItem.Glyph = ResourceMan.getImage16(getImageName(itemId));
                try
                {
                    Image image = this.getImage16(itemId);
                    subItem.Glyph = image;
                }
                catch { }
                subItem.ItemClick += new ItemClickEventHandler(itemClick);
                parentBar.ItemLinks.Add(subItem);
                {
                    CreateToolTip(subItem, getToolTip(itemId));
                }
                //parentBar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] { new DevExpress.XtraBars.LinkPersistInfo(subItem) });
                foreach (DataRow dr in ds.Tables[0].Select("Parents='" + itemId + "'"))
                {
                    string childId = dr[0] as string;
                    createChildItem(subItem, childId);
                }
            }
            else
            {
                BarButtonItem buttonItem = new BarButtonItem();
                buttonItem.Id = frmRibbonMain.IIII++;
                buttonItem.Name = itemId;
                buttonItem.Caption = getName(itemId);
                buttonItem.RibbonStyle = RibbonItemStyles.Default;
                buttonItem.PaintStyle = BarItemPaintStyle.CaptionGlyph;
                try
                {
                    Image image = this.getImage16(itemId);
                    buttonItem.Glyph = image;
                    //buttonItem.Glyph = ResourceMan.getImage16(getImageName(itemId));
                }
                catch { }
                buttonItem.ItemClick+= new ItemClickEventHandler(itemClick);
                parentBar.ItemLinks.Add(buttonItem);
                if (getToolTip(itemId) != "")
                {
                    CreateToolTip(buttonItem, getToolTip(itemId));
                }
                //parentBar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[]{new DevExpress.XtraBars.LinkPersistInfo(buttonItem)});

            }
        }
        private void createChildItem(PopupMenu popupMenu, string parentItemID)
        {
            if (ds.Tables[0].Select("Parents='" + parentItemID + "'").Length > 0)
            {
                BarSubItem barSubItem = new BarSubItem();
                barSubItem.Id = frmRibbonMain.IIII++;
                barSubItem.Caption = getName(parentItemID);
                barSubItem.PaintStyle = BarItemPaintStyle.CaptionGlyph;
                barSubItem.RibbonStyle = RibbonItemStyles.Default;
                barSubItem.Enabled = getEnable(parentItemID);
                popupMenu.ItemLinks.Add(barSubItem, getSep(parentItemID));
                try
                {
                    Image image = this.getImage16(parentItemID);
                    //Image image = ResourceMan.getImage16(getImageName(parentItemID));
                    barSubItem.Glyph = image;
                }
                catch { }
                if (getToolTip(parentItemID) != "")
                {
                    CreateToolTip(barSubItem, getToolTip(parentItemID));
                }
                foreach (DataRow dr in ds.Tables[0].Select("Parents='" + parentItemID + "'"))
                {
                    createChildItem(barSubItem, dr[0].ToString());
                }

            }
            else
            {
                BarButtonItem barButtonItem = new BarButtonItem();
                barButtonItem.Id = frmRibbonMain.IIII++;
                barButtonItem.Name = parentItemID;
                barButtonItem.Caption = getName(parentItemID);
                barButtonItem.PaintStyle = BarItemPaintStyle.CaptionGlyph;
                barButtonItem.RibbonStyle = RibbonItemStyles.Large;
                barButtonItem.Enabled = getEnable(parentItemID);
                //subItemParent.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(barButtonItem, true) });
                popupMenu.ItemLinks.Add(barButtonItem, getSep(parentItemID));
                if (getToolTip(parentItemID) != "")
                {
                    CreateToolTip(barButtonItem, getToolTip(parentItemID));
                }
                try
                {
                    Image image = this.getImage16(parentItemID);
                    //Image image = ResourceMan.getImage16(getImageName(parentItemID));
                    barButtonItem.Glyph = image;
                }
                catch { }
                barButtonItem.ItemClick += new ItemClickEventHandler(itemClick);
            }

        }
        private void CreatePopupItem(BarItem parentItem, string itemId)
        {

            PopupMenu popup = new PopupMenu(new System.ComponentModel.Container());
            BarButtonItem item = (BarButtonItem)parentItem;
            item.Id = frmRibbonMain.IIII++;
            item.ButtonStyle = BarButtonStyle.DropDown;
            item.DropDownControl = popup;
            popup.Ribbon = ribbonControl;
            foreach (DataRow drTemp in ds.Tables[0].Select("Parents='" + itemId + "'"))
            {
                createChildItem(popup, drTemp[0].ToString());
            }
        }
        private void createItem(string itemId)
        {
            if (ds.Tables[0].Select("Parents='" + itemId + "'").Length > 0)
            {
                BarItem barItem;
                if (getForm(itemId) != "")
                {
                    barItem = new BarButtonItem();
                    barItem.Id = frmRibbonMain.IIII++;
                    barItem.ItemClick += new ItemClickEventHandler(itemClick);
                }
                else
                {
                    barItem = new BarSubItem();
                    barItem.Id = frmRibbonMain.IIII++;
                }
                barItem.Name = itemId;
                barItem.Caption = getName(itemId);
                barItem.RibbonStyle = RibbonItemStyles.Default;
                barItem.PaintStyle = BarItemPaintStyle.Standard;
                try
                {
                    Image image = this.getImage16(itemId);
                    barItem.Glyph = image;
                }
                catch { }
                ribbonControl.Toolbar.ItemLinks.Add(barItem,getSep(itemId));
                if (getToolTip(itemId) != "")
                {
                    CreateToolTip(barItem, getToolTip(itemId));
                }
                if (getForm(itemId) != "")
                {
                    CreatePopupItem(barItem, itemId);
                }
                else
                {
                    foreach (DataRow dr in ds.Tables[0].Select("Parents='" + itemId + "'"))
                    {
                        string childId = dr[0] as string;
                        createChildItem(barItem, childId);
                    }
                }
            }
            else
            {
                BarButtonItem buttonItem = new BarButtonItem();
                buttonItem.Id = frmRibbonMain.IIII++;
                buttonItem.Name = itemId;
                buttonItem.Caption = getName(itemId);
                buttonItem.RibbonStyle = RibbonItemStyles.Default;
                buttonItem.PaintStyle = BarItemPaintStyle.Standard;
                //buttonItem.Glyph = ResourceMan.getImage48(getImageName(itemId));
                try
                {
                    Image image = this.getImage16(itemId);
                    buttonItem.Glyph = image;
                }
                catch { }
                buttonItem.ItemClick += new ItemClickEventHandler(itemClick);
                ribbonControl.Toolbar.ItemLinks.Add(buttonItem);
                if (getToolTip(itemId) != "")
                {
                    CreateToolTip(buttonItem, getToolTip(itemId));
                }

            }

        }
      
    }
}
