using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors.DXErrorProvider;
using ProtocolVN.Framework.Core;
using ProtocolVN.Framework.Win;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Created by Khanhdtn.
    /// </summary>
    public abstract partial class frmCauHinhMauPhieu : XtraFormPL
    {
        private DXErrorProvider Error;
        protected Dictionary<int, string> ListMaPhieu;
        int MaxMainPanelHeigh = 540;
        #region Init
        public frmCauHinhMauPhieu()
        {
            InitializeComponent();
            this.Error = HelpInputData.GetErrorProvider(this);
            InitList();
            InitControl();
            this.Load += delegate(object sender, EventArgs e)
            {
                HelpXtraForm.SetFix(this);
            };
            btnXemTruoc_Click(null, null);
        }
        public abstract void InitList();
       
        private void InitControl()
        {
            //button
            this.btnClose.Image = FWImageDic.CLOSE_IMAGE16;
            this.btnSave.Image = FWImageDic.SAVE_IMAGE16;
            this.btnXemTruoc.Image = FWImageDic.PREVIEW_IMAGE16;

            int newMainPanleHeght = 0;
            if (ListMaPhieu.Count == 0)
            {
                HelpMsgBox.ShowNotificationMessage("Chưa có mã phiếu để cấu hình!");
                HelpXtraForm.CloseFormHasConfirm(this);
                return;
            }

            foreach (int key in ListMaPhieu.Keys)
            {
                PatternSelect ps = new PatternSelect();
                ps.Name = "PS" + key;
                this.flowLayoutPanelPattern.Controls.Add(ps);
                ps.f_setValue(ListMaPhieu[key].Split(';')[0]);

                LabelControl lbl = new LabelControl();
                lbl.Text = "Phiếu " + ListMaPhieu[key].Split(';')[1];
                lbl.ToolTip = lbl.Text;
                lbl.AutoSizeMode = LabelAutoSizeMode.None;
                lbl.AutoEllipsis = true;
                lbl.Size = new System.Drawing.Size(flowLayoutPanelLabel.Size.Width - 20, ps.Size.Height);
                this.flowLayoutPanelLabel.Controls.Add(lbl);

                TextEdit txt = new TextEdit();
                txt.Name = "TXT" + key;               
                txt.Properties.ReadOnly = true;
                txt.Properties.AutoHeight = false;
                txt.TabStop = false;
                txt.Size = new System.Drawing.Size(flowLayoutPanelDemo.Size.Width - 8, ps.Size.Height);
                this.flowLayoutPanelDemo.Controls.Add(txt);
                //  txt.Text = DatabaseFB.getSoPhieu(ps.f_getValue());


            }
            newMainPanleHeght = flowLayoutPanelPattern.Size.Height + 30;
            if (MaxMainPanelHeigh > newMainPanleHeght)
            {
                MaxMainPanelHeigh = newMainPanleHeght;
            }
            this.MaximumSize = new System.Drawing.Size(this.Size.Width, MaxMainPanelHeigh + panelControl1.Size.Height + 40);
            this.Size = this.MaximumSize;
            this.xtraScrollableControlConfig.VerticalScroll.Enabled = false;


        } 
        #endregion

        #region Save       
        private bool Save()
        {       

            foreach (int key in ListMaPhieu.Keys)
            {
                PatternSelect ps = flowLayoutPanelPattern.Controls["PS" + key] as PatternSelect;
                if (DatabaseFB.SetThamSo(ListMaPhieu[key].Split(';')[0], ps.f_getValue()) == false)
                    return false;
            }
            
            return true;
        }
        public bool ValidateData()
        {
            Error.ClearErrors();
            foreach (int key in ListMaPhieu.Keys)
            {
                PatternSelect ps = flowLayoutPanelPattern.Controls["PS" + key] as PatternSelect;
                ps.f_checkInput(Error);
            }
            if (Error.HasErrors) return false;
            return true;
        }
        #endregion

        #region Scroll event
        private void xtraScrollableControlExample_Scroll(object sender, XtraScrollEventArgs e)
        {
            xtraScrollableControlConfig.VerticalScroll.Value = xtraScrollableControlExample.VerticalScroll.Value;

        }
        private void xtraScrollableControlConfig_Scroll(object sender, XtraScrollEventArgs e)
        {
            xtraScrollableControlExample.VerticalScroll.Value = xtraScrollableControlConfig.VerticalScroll.Value;
        }

        #endregion

        #region Button event
        private void btnXemTruoc_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (int key in ListMaPhieu.Keys)
                {
                    PatternSelect ps = flowLayoutPanelPattern.Controls["PS" + key] as PatternSelect;
                    TextEdit txt = flowLayoutPanelDemo.Controls["TXT" + key] as TextEdit;
                    txt.Text = DatabaseFB.getSoPhieu(ps.f_getValue());
                }
            }
            catch { }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (PLMessageBox.ShowConfirmMessage("Bạn có chắc muốn đóng?") == DialogResult.Yes)
                this.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateData() == true)
            {
                if (PLMessageBox.ShowConfirmMessage("Bạn có chắc muốn lưu?") == DialogResult.Yes)
                {
                    if (Save() == true)
                        this.Close();
                    else HelpMsgBox.ShowNotificationMessage("Lưu cấu hình mã phiếu không thành công!");
                }
            }
        }        
        #endregion
    }
}