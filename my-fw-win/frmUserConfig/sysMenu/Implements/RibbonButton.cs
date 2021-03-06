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
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public class RibbonButton:MenuToolbar
    {
        protected ApplicationMenu appMenu = new ApplicationMenu();
        protected RibbonControl ribbonControl;
        public RibbonButton(frmRibbonMain main, string username)
        {
            try
            {
                string RibbonXML = "<?xml version='1.0' encoding='utf-8' standalone='yes'?><vn>";
                RibbonXML += FrameworkParams.RibbonMenu;
                RibbonXML += FWMenuFactory.CreateRibbonItem();
                RibbonXML += "</vn>";
                InitMenuToolbar(main, RibbonXML, username);
                drawRibbonButton();
            }
            catch { }
        }

        private void drawRibbonButton()
        {
            ribbonControl = ((frmRibbonMain)this.mainForm).RibbonCtrl;
            appMenu.BottomPaneControlContainer = null;
            appMenu.RightPaneControlContainer = null;
            //appMenu.RightPaneWidth = 240;
            ribbonControl.ApplicationButtonDropDownControl = appMenu;
            appMenu.MenuDrawMode = MenuDrawMode.LargeImagesText;
            appMenu.Ribbon = ribbonControl;
            SetButton();
            foreach (DataRow dr in ds.Tables[0].Select("Parents='1'"))
            {
                string strID = dr[0].ToString();
                createItem(strID);
            }
        }

        private void SetButton()
        {
            DevExpress.XtraBars.PopupControlContainer container = new DevExpress.XtraBars.PopupControlContainer();
            appMenu.BottomPaneControlContainer = container;            
            //Nút thoát
            SimpleButton button = new SimpleButton();
            button.Location = new System.Drawing.Point(124, 3);
            button.Size = new System.Drawing.Size(75, 23);
            button.Image = FWImageDic.EXIT_IMAGE16;
            button.Text = "Thoát";
            button.Click += new EventHandler(Button_click);
            
            //Nút tùy chỉnh giao diện
            SimpleButton buttonTuyChon = new SimpleButton();
            buttonTuyChon.Location = new System.Drawing.Point(45, 3);
            buttonTuyChon.Size = new System.Drawing.Size(75, 23);
            buttonTuyChon.Image = FWImageDic.CONFIG_IMAGE16;
            buttonTuyChon.Text = "Tùy chọn";
            buttonTuyChon.Click += new EventHandler(ButtonTuyChon_click);

            ///Container
            container.Controls.Add(button);
            container.Controls.Add(buttonTuyChon); 

            container.Appearance.BackColor = System.Drawing.Color.Transparent;
            container.Appearance.BackColor2 = System.Drawing.Color.Transparent;
            container.Appearance.Options.UseBackColor = true;
            container.AutoSize = true;
            container.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            container.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            container.Ribbon = ribbonControl;
            container.Size = new System.Drawing.Size(202, 20);
            container.Visible = false;
        }

        protected void Button_click(object sender, EventArgs e)
        {
            if (frmFWRunExit.confirmExit() == false) return;

            FrameworkParams.MainForm.Hide();
            FrameworkParams.ExitApplication(FrameworkParams.EXIT_STATUS.NORMAL_THANKS);
        }

        protected void ButtonTuyChon_click(object sender, EventArgs e)
        {
            appMenu.HidePopup();
            //ProtocolForm.ShowModalDialog(FrameworkParams.MainForm, new frmOption());
            ProtocolForm.ShowModalDialog(FrameworkParams.MainForm, new frmXPOption());
        }

        private void createChildItem(BarItem itemParent, string parentItemID)
        {
            BarSubItem subItemParent = (BarSubItem)itemParent;
            if (ds.Tables[0].Select("Parents='" + parentItemID + "'").Length > 0)
            {
                BarSubItem barSubItem = new BarSubItem();
                barSubItem.Id = frmRibbonMain.IIII++;
                barSubItem.Caption = parentItemID;
                barSubItem.PaintStyle = BarItemPaintStyle.CaptionGlyph;
                barSubItem.RibbonStyle = RibbonItemStyles.Default;
                barSubItem.Enabled = getEnable(parentItemID);
                subItemParent.ItemLinks.Add(barSubItem);
                try
                {
                    Image image = getImage48(parentItemID);
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
                subItemParent.ItemLinks.Add(barButtonItem, getSep(parentItemID));
                if (getToolTip(parentItemID) != "")
                {
                    CreateToolTip(barButtonItem, getToolTip(parentItemID));
                }
                try
                {
                    Image image = getImage48(parentItemID);
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
                barSubItem.Id = frmRibbonMain.IIII++;
                barSubItem.Caption = getName(parentItemID);
                barSubItem.PaintStyle = BarItemPaintStyle.CaptionGlyph;
                barSubItem.RibbonStyle = RibbonItemStyles.Default;
                barSubItem.Enabled = getEnable(parentItemID);
                popupMenu.ItemLinks.Add(barSubItem, getSep(parentItemID));
                try
                {
                    Image image = this.getImage48(parentItemID);
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
                    Image image = this.getImage48(parentItemID);
                    barButtonItem.Glyph = image;
                }
                catch { }
                barButtonItem.ItemClick += new ItemClickEventHandler(itemClick);
            }

        }

        private void CreatePopupItem(BarItem parentItem, string itemId)
        {

            PopupMenu popup = new PopupMenu(new System.ComponentModel.Container());
            popup.MenuDrawMode = MenuDrawMode.LargeImagesText;
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
        private void createItem(string strMenuItemID)
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
                    BarSubItem subItem  = (BarSubItem)barItem;
                    subItem.MenuDrawMode = MenuDrawMode.LargeImagesText;
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
                appMenu.ItemLinks.Add(barItem,getSep(strMenuItemID));
                //Co separator giua cac item
                //pageGroup.ItemLinks.Add(barSubItem, true);
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
                //barButtonItem.LargeWidth = 70;
                //Khong co separator giua cac item
                appMenu.ItemLinks.Add(barButtonItem,getSep(strMenuItemID));
                //Co separator giua cac item
                //pageGroup.ItemLinks.Add(barButtonItem, true);
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
