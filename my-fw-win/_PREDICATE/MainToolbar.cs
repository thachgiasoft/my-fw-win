using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using ProtocolVN.Framework.Win;
using System.Drawing;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public class MainToolbar : MenuToolbar
    {
        public static string TOOLBAR = RadParams.RUNTIME_PATH + @"\conf\toolbar.xml";
        
        public MainToolbar(frmMain main, string username)
        {
            //Chưa có hỗ trợ 2 ngôn ngữ trên toolbar
            try
            {
                InitMenuToolbar(main, MainToolbar.TOOLBAR, username);
                drawToolbar();
            }
            catch { }
        }

        private BarLargeButtonItem buttonItem;
        private void drawToolbar()
        {
            BarManager barManager1 = ((IMainForm)this.mainForm).GetBarManager();
            Bar toolBar = new Bar();
            ((System.ComponentModel.ISupportInitialize)(barManager1)).BeginInit();
            
            barManager1.Bars.Add(toolBar);
            //PHUOC TODO Làm phẳng toolbar
            toolBar.BarName = "Tool Bar";
            toolBar.DockCol = 0;
            toolBar.DockRow = 1;
            toolBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            toolBar.OptionsBar.AllowQuickCustomization = false;
            toolBar.OptionsBar.DisableClose = true;
            toolBar.OptionsBar.DisableCustomization = true;
            toolBar.OptionsBar.UseWholeRow = true;
            toolBar.OptionsBar.DrawDragBorder = false;

            toolBar.Text = "Tool Bar";
            ///////////////////////////////
            //Lấy ra các Toolbar parents
            foreach (DataRow dr in ds.Tables[0].Select("Parents=''"))
            {
                buttonItem = new BarLargeButtonItem();
                createToolBarItem(dr, toolBar);
                string parentName = base.getId(dr["Name"].ToString());
                DataRow[] rowChild = ds.Tables[0].Select("Parents='" + parentName + "'");
                if (rowChild.Length > 0)
                {
                    buttonItem.ButtonStyle = BarButtonStyle.DropDown;
                    CreatePopupMenu(rowChild, barManager1);
                }
            }
            
            ((System.ComponentModel.ISupportInitialize)(barManager1)).EndInit();
        }
        private void createToolBarItem(DataRow dr, Bar bar)
        {
            BarManager barManager1 = ((IMainForm)this.mainForm).GetBarManager();
            string id = dr["ID"].ToString();
            string text = base.getName(id);

            buttonItem.Name = id;
            buttonItem.Caption = text;
            buttonItem.Hint = text;
            buttonItem.Enabled = base.getEnable(id);
            //buttonItem.ButtonStyle = BarButtonStyle.DropDown;
            buttonItem.PaintStyle = BarItemPaintStyle.CaptionGlyph;
            buttonItem.ItemClick += new ItemClickEventHandler(itemClick);
            try
            {
                Image image = ResourceMan.getImage32(base.getImageName(id));
                buttonItem.Glyph = image;
            }
            catch { }
            bar.LinksPersistInfo.Add(new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, buttonItem,
            buttonItem.Caption, base.getSep(id), false, base.getEnable(id), buttonItem.Width, buttonItem.Glyph, BarItemPaintStyle.CaptionGlyph));

            barManager1.Items.Add(buttonItem);
        }
        private void CreatePopupMenu(DataRow[] dr,BarManager bar)
        {
            PopupMenu popupMenu = new PopupMenu();
            foreach (DataRow row in dr)
            {
                string id = row["ID"].ToString();
                string text = base.getName(id);
                BarLargeButtonItem buttonItem1 = new BarLargeButtonItem();
                buttonItem1.Name = id;
                buttonItem1.Caption = text;
                buttonItem1.Hint = text;
                buttonItem1.PaintStyle = BarItemPaintStyle.CaptionGlyph;
                try
                {
                    Image image = ResourceMan.getImage16(getImageName(id));
                    buttonItem1.Glyph = image;    
                }
                catch { }
                popupMenu.LinksPersistInfo.Add(new DevExpress.XtraBars.LinkPersistInfo(buttonItem1,getSep(id)));
                buttonItem1.ItemClick += new ItemClickEventHandler(base.itemClick);
            }
            popupMenu.Manager = bar;
            buttonItem.DropDownControl = popupMenu;
        }
    }
}