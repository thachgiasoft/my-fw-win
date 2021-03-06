using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using System.Drawing;
using System.Collections;
using ProtocolVN.Framework.Win;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public class RibbonMainMenu : MenuToolbar
    {
        protected RibbonControl ribbonControl;

        private void CreateSkinPage(frmRibbonMain main)
        {
            if (FrameworkParams.UsingGallerySkins == true)
            {
                new SkinGalleryHelper(main.ribbonGallerySkins);

                RibbonPageGroup pageGroup = new RibbonPageGroup();
                pageGroup.ItemLinks.Add(main.ribbonGallerySkins);                
                pageGroup.KeyTip = "";
                pageGroup.ShowCaptionButton = false;
                pageGroup.Text = "";
                main.SkinRibbonPage.Groups.Add(pageGroup);
                main.RibbonCtrl.Pages.Add(main.SkinRibbonPage);
            }
            else
            {
                if(FrameworkParams.currentSkin!=null)
                    SkinGalleryHelper.AddSkinMenuToQuickShortcut(main.Ribbon, FrameworkParams.currentSkin.Skin);
            }
        }
        public RibbonMainMenu(frmRibbonMain main, string username)
        {
            try
            {
                string MenuXML = "<?xml version='1.0' encoding='utf-8' standalone='yes'?><vn>";
                if (FrameworkParams.UsingHomePage || FrameworkParams.option._IsHomePage == "Y"){
                    MenuXML += FWMenuFactory.CreateHomePage();
                }
                MenuXML += FrameworkParams.Menu;
                MenuXML += FWMenuFactory.CreateSystemPage();
                MenuXML += FWMenuFactory.CreateToolPage();
                MenuXML += FWMenuFactory.CreatePluginPage();
                MenuXML += FWMenuFactory.CreateHelpPage();
                if(FrameworkParams.isSupportDeveloper)
                    MenuXML += FWMenuFactory.CreateDevPage();
                MenuXML += "</vn>";
                
                FrameworkParams.AllMenu = MenuXML;

                InitMenuToolbar(main, MenuXML, username);
                CreateSkinPage(main);
                drawMenuBar();

                if (FrameworkParams.option._IsMinMenu == "Y")
                {
                    main.RibbonCtrl.Minimized = true;
                }
                else
                {
                    main.RibbonCtrl.Minimized = false;
                }
            }
            catch (Exception Ex){
                PLException.AddException(Ex);
            }
        }
        //B3
        // Add cac item vao trong group
        private void addItemIntoPageGroup(RibbonPage page,DataTable dt, string Caption)
        {
            RibbonPageGroup pageGroup = new RibbonPageGroup();
            pageGroup.ShowCaptionButton = false;
            pageGroup.Text = Caption;
            page.Groups.Add(pageGroup);
            foreach (DataRow dr in dt.Rows)
            {
                string itemID = dr[0] as string;
                createItem(pageGroup, itemID);
            }
        }

        //B2
        //Tao cac group page
        private void createGroup(RibbonPage page,string menuID)
        {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("Name");
                DataRow drTemp;
                foreach (DataRow drChild in ds.Tables[0].Select("Parents='" + menuID + "'"))
                {
                    if (!getSep(drChild[0].ToString()))
                    {
                        drTemp = dt.NewRow();
                        drTemp[0] = drChild[0];
                        drTemp[1] = drChild[1];
                        dt.Rows.Add(drTemp);
                    }
                    else
                    {
                        addItemIntoPageGroup(page,dt, getPageGroup(drChild[0].ToString()));

                        dt.Rows.Clear();
                        drTemp = dt.NewRow();
                        drTemp[0] = drChild[0];
                        drTemp[1] = drChild[1];
                        dt.Rows.Add(drTemp);
                    }
                }
                addItemIntoPageGroup(page, dt, "");
        }

        //B1
        private void drawMenuBar()
        {
            ribbonControl = ((frmRibbonMain)this.mainForm).RibbonCtrl;
            ribbonControl.ToolbarLocation = RibbonQuickAccessToolbarLocation.Default;
            if (ribbonControl.RibbonStyle == RibbonControlStyle.Office2010)
            {
                ribbonControl.ApplicationIcon = (System.Drawing.Bitmap)FWImageDic.LOGO_IMAGE16;
            }
            else
            {
                ribbonControl.ApplicationIcon = (System.Drawing.Bitmap)FWImageDic.LOGO_IMAGE48;
            }
            //ribbonControl.ApplicationCaption = "www.protocolvn.com";
            ribbonControl.ApplicationCaption = HelpApplication.getProductName();
            bool isMergeGUIAndHP = false;
            foreach (DataRow dr in ds.Tables[0].Select("Parents='1'"))
            {
                string strMenuID = dr[0].ToString();
                if (strMenuID.Equals("WINDOW"))
                {
                    //Không xử lý
                    //RibbonPage page = new RibbonPage();
                    //page.Text = getName(strMenuID);
                    //ribbonControl.Pages.Add(page);
                    //createGroup(page, strMenuID);
                }                 
                else
                {
                    if (FrameworkParams.UsingGallerySkins && FrameworkParams.UsingHomePage & isMergeGUIAndHP == false)
                    {
                        RibbonPage page = ribbonControl.Pages[0];
                        page.Text = getName(strMenuID);
                        createGroup(page, strMenuID);
                        isMergeGUIAndHP = true;
                    }
                    else
                    {
                        RibbonPage page = new RibbonPage();
                        page.Text = getName(strMenuID);
                        ribbonControl.Pages.Add(page);
                        createGroup(page, strMenuID);
                    }
                }
            }
        }

        //Tao con cua item trong Page Group
        private void createChildItem(BarItem itemParent, string parentItemID)
        {
            BarSubItem subItemParent = (BarSubItem)itemParent;
            if (ds.Tables[0].Select("Parents='" + parentItemID + "'").Length > 0)
            {
                BarSubItem barSubItem = new BarSubItem();
                barSubItem.Caption = parentItemID;
                barSubItem.PaintStyle = BarItemPaintStyle.CaptionGlyph;
                barSubItem.RibbonStyle = RibbonItemStyles.Default;
                barSubItem.Enabled = getEnable(parentItemID);
                //subItemParent.ItemLinks.Add(barSubItem);
                subItemParent.ItemLinks.Add(barSubItem, getSep(parentItemID));
                try
                {
                    //Image image = ResourceMan.getImage16(getImageName(parentItemID));
                    Image image = this.getImage16(parentItemID);
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
                subItemParent.ItemLinks.Add(barButtonItem,getSep(parentItemID));
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

        private void createChildItem(PopupMenu popupMenu, string parentItemID)
        {
            if (ds.Tables[0].Select("Parents='" + parentItemID + "'").Length > 0)
            {
                BarSubItem barSubItem = new BarSubItem();
                barSubItem.Caption = getName(parentItemID);
                barSubItem.PaintStyle = BarItemPaintStyle.CaptionGlyph;
                barSubItem.RibbonStyle = RibbonItemStyles.Default;
                barSubItem.Enabled = getEnable(parentItemID);
                popupMenu.ItemLinks.Add(barSubItem,getSep(parentItemID));
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
      

        //Tao popup item
        private void CreatePopupItem(BarItem parentItem,string itemId)
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
        
        //Tao cac item trong page group
        private void createItem(RibbonPageGroup pageGroup, string strMenuItemID)
        {
            //Thuc hien viec add MenuItem con cua Menu dang xet neu co
            if (ds.Tables[0].Select("Parents='" + strMenuItemID + "'").Length > 0)
            {
                BarItem barItem;
                if (getForm(strMenuItemID) != "")
                {
                    barItem = new BarButtonItem();
                    barItem.ItemClick += new ItemClickEventHandler(itemClick);
                }
                else
                {
                    barItem = new BarSubItem();
                }
                barItem.Id = frmRibbonMain.IIII++;
                barItem.Name = strMenuItemID;

                barItem.Caption = getName(strMenuItemID);
                barItem.PaintStyle = BarItemPaintStyle.CaptionGlyph;
                barItem.RibbonStyle = RibbonItemStyles.Large;
                barItem.Enabled = getEnable(strMenuItemID);
                //Gan do rong cho item
                barItem.LargeWidth = 80;
                //Khong co separator giua cac item
                //pageGroup.ItemLinks.Add(barItem);
                //Co separator giua cac item
                pageGroup.ItemLinks.Add(barItem, getSep(strMenuItemID));
                try
                {
                    Image image = this.getImage48(strMenuItemID);
                    barItem.Glyph = image;
                }
                catch { }
                if (getToolTip(strMenuItemID) != "")
                {
                    CreateToolTip(barItem, getToolTip(strMenuItemID));
                }
                if (getForm(strMenuItemID) != "")
                {
                    CreatePopupItem(barItem, strMenuItemID);
                }
                else
                {
                    foreach (DataRow drTemp in ds.Tables[0].Select("Parents='" + strMenuItemID + "'"))
                    {
                        createChildItem(barItem, drTemp[0].ToString());
                    }
                }
            }
            else
            {
                BarButtonItem barButtonItem = new BarButtonItem();
                barButtonItem.Id = frmRibbonMain.IIII++;
                barButtonItem.Name = strMenuItemID;

                barButtonItem.Caption = getName(strMenuItemID);
                barButtonItem.PaintStyle = BarItemPaintStyle.CaptionGlyph;
                barButtonItem.RibbonStyle = RibbonItemStyles.Large;
                barButtonItem.Enabled = getEnable(strMenuItemID);
                barButtonItem.LargeWidth = 70;
                //Khong co separator giua cac item
                //pageGroup.ItemLinks.Add(barButtonItem);
                //Co separator giua cac item
                pageGroup.ItemLinks.Add(barButtonItem, getSep(strMenuItemID));
                if (getToolTip(strMenuItemID) != "")
                {
                    CreateToolTip(barButtonItem, getToolTip(strMenuItemID));
                }
                try
                {
                    Image image = this.getImage48(strMenuItemID);
                    barButtonItem.Glyph = image;
                }
                catch { }

                barButtonItem.ItemClick += new ItemClickEventHandler(itemClick);
            }
        }
    }
}