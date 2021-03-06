using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System.Drawing;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public class MainMenu : MenuToolbar
    {
        public MainMenu(frmMain main, string username)
        {
            try
            {
                string MenuXML = "<?xml version='1.0' encoding='utf-8' standalone='yes'?><vn>";
                if (FrameworkParams.UsingHomePage)
                {
                    MenuXML += FWMenuFactory.CreateHomePage();
                }
                MenuXML += FrameworkParams.Menu;
                MenuXML += FWMenuFactory.CreatePluginPage();
                MenuXML += FWMenuFactory.CreateHelpPage();
                MenuXML += "</vn>";
                
                InitMenuToolbar(main, MenuXML, username);
                drawMenuBar();
            }
            catch { }
        }

        private void drawMenuBar()
        {
            BarManager barManager1 = ((IMainForm)this.mainForm).GetBarManager();
            
            ((System.ComponentModel.ISupportInitialize)(barManager1)).BeginInit();
            barManager1.Bars.Clear();
            Bar menuBar = new Bar();
            
            barManager1.Bars.Add(menuBar);
            barManager1.Form = mainForm;
            barManager1.MainMenu = menuBar;
            
            menuBar.BarName = "Menu Bar";
            menuBar.DockCol = 0;
            menuBar.DockRow = 0;
            menuBar.DockStyle = BarDockStyle.Top;
            menuBar.OptionsBar.AllowQuickCustomization = false;
            menuBar.OptionsBar.DisableClose = true;
            menuBar.OptionsBar.DisableCustomization = true;
            menuBar.OptionsBar.UseWholeRow = true;
            menuBar.OptionsBar.DrawDragBorder = false;
            
            menuBar.Text = "Menu Bar";

            //Lấy ra các parent
            foreach (DataRow dr in ds.Tables[0].Select("Parents=''"))
            {
                 string strMenu = dr[0].ToString();
                 if (strMenu.Equals("WINDOW"))
                 {
                     BarMdiChildrenListItem barMdi = new BarMdiChildrenListItem();
                     barMdi.Caption = base.getName(strMenu);
                     barMdi.Enabled = base.getEnable(strMenu);
                     menuBar.LinksPersistInfo.Add(new LinkPersistInfo(barMdi));
                     barManager1.Items.Add(barMdi);

                     //PHUOC TODO Thêm vào menu "CLOSE ALL"
                 }                 
                 else
                 {
                     BarSubItem barSubItem = new BarSubItem();
                     barSubItem.Caption = base.getName(strMenu);
                     barSubItem.Enabled = base.getEnable(strMenu);                     
                     menuBar.LinksPersistInfo.Add(new LinkPersistInfo(barSubItem));
                     barManager1.Items.Add(barSubItem);
                     if (ds.Tables[0].Select("Parents='" + strMenu + "'").Length > 0)
                     {
                         foreach (DataRow dr1 in ds.Tables[0].Select("Parents='" + strMenu + "'"))
                         {
                             createMenuItems(barSubItem, dr1[0].ToString());
                         }
                     }
                 }
            }
             
            ((System.ComponentModel.ISupportInitialize)(barManager1)).EndInit();          
        }

        private void createMenuItems(BarSubItem barSubItem, string strMenu)
        {
            BarManager barManager1 = ((IMainForm)this.mainForm).GetBarManager();
            
            if (ds.Tables[0].Select("Parents='" + strMenu + "'").Length > 0)
            {
                BarSubItem subItem = new BarSubItem();
                subItem.Caption = getName(strMenu);
                subItem.Enabled = getEnable(strMenu);
                barSubItem.LinksPersistInfo.Add(new LinkPersistInfo(subItem, getSep(strMenu)));
                barManager1.Items.Add(subItem);              
                foreach (DataRow drTemp1 in ds.Tables[0].Select("Parents='" + strMenu + "'"))
                {
                    createMenuItems(subItem, drTemp1[0].ToString());
                }
            }
            else
            {               
                BarButtonItem staticItem = new BarButtonItem();
                staticItem.Name = strMenu;
                staticItem.Caption = getName(strMenu);
                staticItem.Enabled = getEnable(strMenu);
                staticItem.PaintStyle = BarItemPaintStyle.CaptionGlyph;
                try{
                    Image image = ResourceMan.getImage16(getImageName(strMenu));
                    staticItem.Glyph = image;
                }
                catch{ }
                
                staticItem.ItemClick += new ItemClickEventHandler(itemClick);
                barSubItem.LinksPersistInfo.Add(new LinkPersistInfo(staticItem, base.getSep(strMenu)));
                barManager1.Items.Add(staticItem);
            }
        }
    }
}