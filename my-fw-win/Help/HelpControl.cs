using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using ProtocolVN.Framework.Core;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.Utils;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Hỗ trợ xử lý trên các control của DevExpress.
    /// </summary>
    public class HelpControl
    {
        #region Một số xem xét lại vị trí đặt
        /// <summary>        
        /// </summary>
        /// <param name="Owner">
        /// Owner là Button
        /// Owner là GridView
        /// </param>
        public static XtraForm SetOpenModalDialog(Object ControlOwner, String FormName, List<Object> InitParams)
        {
            XtraForm form = null;
            if (ControlOwner is SimpleButton)
            {
                SimpleButton button = (SimpleButton)ControlOwner;
                button.Click += delegate(object sender, EventArgs e)
                {
                    form = (XtraForm)GenerateClass.initObject(FormName, InitParams);
                    ProtocolForm.ShowModalDialog((XtraForm)button.FindForm(), form);
                };
            }
            else if (ControlOwner is GridView)
            {
                GridView gridView = (GridView)ControlOwner;
                gridView.DoubleClick += delegate(object sender, EventArgs e)
                {
                    form = (XtraForm)GenerateClass.initObject(FormName, InitParams);
                    ProtocolForm.ShowModalDialog((XtraForm)gridView.GridControl.FindForm(), form);
                };
            }
            else
            {
                PLMessageBoxDev.ShowMessage("Control này chưa hỗ trợ.");
                throw new Exception();
            }
            return form;
        }
        #endregion


        /// <summary>Hàm cho phép khi chọn thì màu đỏ, Khi không chọn màu đen
        /// </summary>
        public static void RedCheckEdit(CheckEdit ck)
        {
            RedCheckEdit(ck, true);
        }
        public static void RedCheckEdit(params CheckEdit[] cks)
        {
            foreach (CheckEdit ck in cks)
            {
                RedCheckEdit(ck, true);
            }
        }
        public static void RedCheckEdit(CheckEdit ck, bool isBold)
        {
            ck.CheckedChanged += delegate(object sender, EventArgs e)
            {
                if (ck.Checked)
                {
                    if(isBold)
                        ck.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                    
                    ck.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    if (isBold)
                        ck.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular);
                    
                    ck.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
                }
                ck.Refresh();
            };
        }

        public static void SetDisable(BaseEdit control)
        {
            control.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.WhiteSmoke;
            control.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            control.Properties.AppearanceDisabled.Options.UseBackColor = true;
            control.Properties.AppearanceDisabled.Options.UseForeColor = true;
        }

        public static bool SetBarcodeEvent(TextEdit edit)
        {
            bool flag = false;
            edit.KeyUp += delegate(object sender, System.Windows.Forms.KeyEventArgs e)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.Enter)
                {
                    System.Windows.Forms.SendKeys.Send("{TAB}");
                }
            };
            return flag;
        }

        public static void SetBarcodeEvent(CalcEdit edit)
        {
            edit.KeyUp += delegate(object sender, System.Windows.Forms.KeyEventArgs e)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.Enter)
                {
                    System.Windows.Forms.SendKeys.Send("{TAB}");
                }
            };
        }

        public static void SetBarcodeEvent(GridView gridView, GridColumn column, string fieldName)
        {
            column.FieldName = fieldName;
            gridView.OptionsNavigation.EnterMoveNextColumn = true;
        }

        public static void SetToolTip(BaseControl control, String header, String htmlText, String footer){
            SuperToolTip superToolTip = new SuperToolTip();

            ToolTipItem toolTipItem = new ToolTipItem();
            toolTipItem.Text = htmlText;
            toolTipItem.AllowHtmlText = DefaultBoolean.True;
            if (header != null)
            {
                superToolTip.Items.AddTitle(header);
            }
            superToolTip.Items.Add(toolTipItem);
            if (footer != null)
            {
                superToolTip.Items.AddSeparator();
                superToolTip.Items.AddTitle(footer);
            }
            superToolTip.AllowHtmlText = DefaultBoolean.True;

            control.SuperTip = superToolTip;
        }

        #region Enter to Nextcontrol
        /// <summary>
        /// Nhấn Tab | Enter dể đến control kế tiếp
        /// Đối với các control đã sử dụng Enter cho việc khác thì dùng chỉ phím Tab để di chuyển.
        /// </summary>
        /// <param name="form"></param>
        public static void setEnterAsTab(XtraForm form)
        {
            PLKey k = new PLKey(form);
            k.Add(Keys.Enter, SetFocusOnNextControl);
        }
        private static void SetFocusOnNextControl()
        {
            SendKeys.Send("{TAB}");
        }
        #endregion

        /// <summary>
        /// Thêm 1 ButtonItem vào cuối Toolbar.
        /// </summary>
        /// <param name="man"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static BarButtonItem addBarButtonItem(BarManager man, String title)
        {
            BarButtonItem barItem = new BarButtonItem(man, title);
            barItem.Name = "barButtonItem";
            barItem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            if (man.Bars != null && man.Bars.Count > 0)
            {
                man.Bars[0].LinksPersistInfo.Add(new DevExpress.XtraBars.LinkPersistInfo(barItem));
            }            
            return barItem;
        }
        public static BarButtonItem addSaveQueryDialog(XtraForm form, BarManager barManager, GridControl gridControl, string dataSetID, string masterQueryNoCondition,
            params ProtocolVN.Framework.Win.SaveQueryDialog.HookAfterExecAdvQuery[] hooks)
        {
            BarButtonItem advancedSearch = HelpControl.addBarButtonItem(barManager, "Tìm kiếm nâng cao");
            advancedSearch.Glyph = FWImageDic.FIND_IMAGE20;
            advancedSearch.ItemClick += delegate(object sender, ItemClickEventArgs e)
            {
                FilterCase obj = new FilterCase(FrameworkParams.currentUser.id, dataSetID, "Truy vấn mới", masterQueryNoCondition);
                SaveQueryDialog q = new SaveQueryDialog(obj, gridControl);
                if (hooks != null && hooks.Length == 1)
                {
                    q.hook = hooks[0];
                }
                q.Owner = form;
                q.Show();
            };

            return advancedSearch;
        }

        public static BarButtonItem addBarButtonItem(BarManager man, PopupMenu menu, String title)
        {
            BarButtonItem barItem = new BarButtonItem(man, title);
            barItem.Name = "barButtonItem";
            barItem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            menu.LinksPersistInfo.Add(new DevExpress.XtraBars.LinkPersistInfo(barItem));
            
            return barItem;
        }
        public static BarButtonItem addSaveQueryDialog(XtraForm form, BarManager barManager, PopupMenu menu, GridControl gridControl, string dataSetID, string masterQueryNoCondition, 
            params ProtocolVN.Framework.Win.SaveQueryDialog.HookAfterExecAdvQuery[] hooks)
        {
            BarButtonItem advancedSearch = HelpControl.addBarButtonItem(barManager, menu, "Tìm kiếm nâng cao");
            advancedSearch.Glyph = FWImageDic.FIND_IMAGE20;
            advancedSearch.ItemClick += delegate(object sender, ItemClickEventArgs e)
            {
                FilterCase obj = new FilterCase(FrameworkParams.currentUser.id, dataSetID, "Truy vấn mới", masterQueryNoCondition);
                SaveQueryDialog q = new SaveQueryDialog(obj, gridControl);
                if (hooks != null && hooks.Length == 1)
                {
                    q.hook = hooks[0];
                }
                q.Owner = form;
                q.Show();
            };

            return advancedSearch;
        }
        
    }
}
