using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ProtocolVN.Framework.Core;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors;
namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Quản lý người dùng    
    /// </summary>
    /// Đã sử dụng trong các dự án nên không xóa
    [Obsolete("Không sử dụng")]
    public partial class frmUserManExt : XtraFormPL
    {
        public bool isView = false;

        public frmUserManExt()
        {
            InitializeComponent();
            this.btnClose.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.btnInsert.Glyph = FWImageDic.ADD_IMAGE20;
            this.btnSave.Glyph = FWImageDic.SAVE_IMAGE20;
            this.btnEdit.Glyph = FWImageDic.EDIT_IMAGE20;
            this.btnClose.Glyph = FWImageDic.CLOSE_IMAGE20;
            this.btnDelete.Glyph = FWImageDic.DELETE_IMAGE20;
            this.btnDontSave.Glyph = FWImageDic.NO_SAVE_IMAGE20;
            this.btnView.Glyph = FWImageDic.VIEW_IMAGE20;
            
        }

        private void frmUserMan_Load(object sender, EventArgs e)
        {
            gridViewFeature.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            gridViewReport.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            gridViewUser.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            gridViewGroup.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            gridViewThanhPhanGroupUser.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;

            xtraTabControlLeft.Width = this.Width / 2;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            this.gridControlGroup.DataSource = Group.loadAllGroup();
            this.gridControlUser.DataSource = User.loadAllUser();

            editEnabledButtonDeleteAndEdit();
            xtraTabControlRight.SelectedTabPageIndex = 0;
            this.xtraTabPage1.Text = "Người dùng";
            this.gridViewThanhPhanGroupUser.GroupPanelText = "Danh sách người dùng trong nhóm";   

            //XtraGridSupport.BitCBBToCheckImageCombo(this.gridViewFeature);
            //XtraGridSupport.BitCBBToCheckImageCombo(this.gridViewReport);

            HelpGridColumn.CotCheckEdit(colReportISREAD, "ISREAD_BIT");
            HelpGridColumn.CotCheckEdit(colISREAD, "ISREAD_BIT");
            HelpGridColumn.CotCheckEdit(colISINSERT, "ISINSERT_BIT");
            HelpGridColumn.CotCheckEdit(colISUPDATE, "ISUPDATE_BIT");
            HelpGridColumn.CotCheckEdit(colISDELETE, "ISDELETE_BIT");
            HelpGridColumn.CotCheckEdit(colISPRINT, "ISPRINT_BIT");
            HelpGridColumn.CotCheckEdit(colISEXPORT, "ISEXPORT_BIT");
            xtraTabControlLeft_SelectedPageChanged(null, null);
            
            
        }

        private void frmUserMan_Resize(object sender, EventArgs e)
        {
            xtraTabControlLeft.Width = this.Width / 2;
        }

        private void xtraTabControlLeft_Click(object sender, EventArgs e)
        {
            editEnabledButtonDeleteAndEdit();
            if (xtraTabControlLeft.SelectedTabPage.Name.Equals("xtraTabPageGroup"))
            {
                this.xtraTabPage1.Text = "Người dùng";
                this.gridViewThanhPhanGroupUser.GroupPanelText = "Danh sách người dùng trong nhóm";
                loadGridViewGroupSelectionChanged();
            }
            else
            {
                this.xtraTabPage1.Text = "Nhóm người dùng";
                this.gridViewThanhPhanGroupUser.GroupPanelText = "Danh sách nhóm chứa người dùng";
                loadGridViewUserSelectionChanged();
            }
        }

        private void gridControlGroupAndUser_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //PHUOCNC : Chưa xử lý phân quyền trên các sự kiện
            //btnEdit_Click();
        }

        private void gridViewGroup_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (xtraTabControlLeft.SelectedTabPage.Name.Equals("xtraTabPageGroup"))
            {
                loadGridViewGroupSelectionChanged();
                this.gridViewThanhPhanGroupUser.ExpandAllGroups();
            }
        }

        private void gridViewUser_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (xtraTabControlLeft.SelectedTabPage.Name.Equals("xtraTabPageUser"))
            {
                loadGridViewUserSelectionChanged();
            }
        }
    
        private void xtraTabControlLeft_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if(xtraTabControlLeft.SelectedTabPageIndex==0)
            {
                colGROUPNAME_TP.Visible = false;
                colDEPARTMENTNAME_TP.Visible = true;
                colFULLNAME_TP.Visible = true;
                colUSERNAME_TP.Visible = true;
            }
            else
            {
                colGROUPNAME_TP.Visible = true ;
                colDEPARTMENTNAME_TP.Visible = false ;
                colFULLNAME_TP.Visible = false ;
                colUSERNAME_TP.Visible = false ;
            }
        }

        private void barButtonItemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControlLeft.SelectedTabPage.Name.Equals("xtraTabPageGroup"))
            {
                frmGroupChild form = new frmGroupChild(this, "INSERT", null);
                ProtocolForm.ShowModalDialog(this, form);
                this.gridControlGroup.DataSource = Group.loadAllGroup();
                this.editEnabledButtonDeleteAndEdit();
            }
            else
            {
                frmUserChild form = new frmUserChild(this, "INSERT", null);
                ProtocolForm.ShowModalDialog(this, form);
                this.gridControlUser.DataSource = User.loadAllUser();
                this.editEnabledButtonDeleteAndEdit();
            }

            reLoadGroupUserThanhPhan();
        }

        private void barButtonItemView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.isView = true;
            btnEdit_Click();
            this.isView = false;
        }

        private void barButtonItemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnEdit_Click();
        }

        private void barButtonItemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControlLeft.SelectedTabPage.Name.Equals("xtraTabPageGroup"))
            {
                if (gridViewGroup.FocusedRowHandle < 0) return;
                DataRow row = gridViewGroup.GetDataRow(gridViewGroup.FocusedRowHandle);
                DialogResult reply = FWMsgBox.questionGroupDelete(row["GROUPNAME"].ToString());
                if (reply == DialogResult.Yes)
                {
                    Group group = new Group();
                    group.id = HelpNumber.ParseInt64(row["GROUPID"].ToString());
                    if (!group.delete())
                    {
                        FWMsgBox.showDeleteUsing();
                        return;
                    }
                    else
                    {
                        gridViewGroup.DeleteRow(gridViewGroup.FocusedRowHandle);
                        editEnabledButtonDeleteAndEdit();
                    }
                }
            }
            else
            {
                if (gridViewUser.FocusedRowHandle < 0) return;
                DataRow row = gridViewUser.GetDataRow(gridViewUser.FocusedRowHandle);
                if (row["USERNAME"].ToString() == "admin")
                {
                    HelpMsgBox.ShowNotificationMessage("Bạn không thể xóa người dùng 'admin'");
                    return;
                }
                DialogResult reply = FWMsgBox.questionUserDelete(row["USERNAME"].ToString());
                if (reply == DialogResult.Yes)
                {
                    User user = new User();
                    user.id = HelpNumber.ParseInt64(row["USERID"].ToString());    
                    if (!user.delete())
                    {
                        FWMsgBox.showDeleteUsing();
                        return;
                    }
                    else
                    {
                        this.gridViewUser.DeleteRow(gridViewUser.FocusedRowHandle);
                        editEnabledButtonDeleteAndEdit();
                    }
                }
            }
            //HUNG
            reLoadGroupUserThanhPhan();            
        }

        private void barButtonItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try { this.gridViewFeature.FocusedRowHandle += 1; } catch { }
            try { this.gridViewReport.FocusedRowHandle += 1; } catch { }

            List<Feature> features = new List<Feature>();
            List<BaoBieu> reports = new List<BaoBieu>();
            if (this.xtraTabControlLeft.SelectedTabPage.Name.Equals("xtraTabPageGroup"))
            {
                foreach (int j in gridViewGroup.GetSelectedRows())
                {
                    DataRow row1 = this.gridViewGroup.GetDataRow(j);
                    long groudId = HelpNumber.ParseInt64(row1["groupid"].ToString());

                    for (int i = 0; i < gridViewFeature.RowCount; i++)
                    {
                        features.Add(UserManUtil.getFeature(this.gridViewFeature.GetDataRow(i)));
                    }
                    for (int i = 0; i < gridViewReport.RowCount; i++)
                    {
                        reports.Add(UserManUtil.getReport(this.gridViewReport.GetDataRow(i)));
                    }
                    Feature.updateGroup(groudId, features);
                    BaoBieu.updateGroup(groudId, reports);
                }
            }
            else
            {
                foreach (int j in gridViewUser.GetSelectedRows())
                {
                    DataRow row1 = this.gridViewUser.GetDataRow(j);
                    long userID = HelpNumber.ParseInt64(row1["userid"]);

                    for (int i = 0; i < gridViewFeature.RowCount; i++)
                    {
                        features.Add(UserManUtil.getFeature(this.gridViewFeature.GetDataRow(i)));
                    }
                    for (int i = 0; i < gridViewReport.RowCount; i++)
                    {
                        reports.Add(UserManUtil.getReport(this.gridViewReport.GetDataRow(i)));
                    }
                    Feature.updateUser(userID, features);
                    BaoBieu.updateUser(userID, reports);
                }
            }

            if (xtraTabControlLeft.SelectedTabPage.Name.Equals("xtraTabPageGroup"))
                loadGridViewGroupSelectionChanged();
            else
                loadGridViewUserSelectionChanged();
        }

        private void barButtonItemDontSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControlLeft.SelectedTabPage.Name.Equals("xtraTabPageGroup"))
                loadGridViewGroupSelectionChanged();
            else
                loadGridViewUserSelectionChanged();
        }

        private void barButtonItemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        #region Hàm hỗ trợ
        private void btnEdit_Click()
        {
            if (xtraTabControlLeft.SelectedTabPage.Name.Equals("xtraTabPageGroup"))
            {
                if (btnEdit.Enabled)
                {
                    DataRow row = gridViewGroup.GetDataRow(gridViewGroup.FocusedRowHandle);
                    frmGroupChild form = new frmGroupChild(this, "EDIT", row["GROUPID"].ToString());
                    ProtocolForm.ShowModalDialog(this, form);
                    this.gridControlGroup.DataSource = Group.loadAllGroup();
                }
            }
            else
            {
                if (btnEdit.Enabled)
                {
                    DataRow row = gridViewUser.GetDataRow(gridViewUser.FocusedRowHandle);
                    frmUserChild form = new frmUserChild(this, "EDIT", row["userid"].ToString());
                    ProtocolForm.ShowModalDialog(this, form);
                    this.gridControlUser.DataSource = User.loadAllUser();
                }
            }
            reLoadGroupUserThanhPhan();
        }
        DataSet dsGetUserByGroup = null;
        DataSet dsGetGroupByUser = null;
        private void editEnabledButtonDeleteAndEdit()
        {
            if (xtraTabControlLeft.SelectedTabPage.Name.Equals("xtraTabPageGroup"))
            {
                if (gridViewGroup.RowCount == 0)
                {
                    this.btnDelete.Enabled = false;
                    this.btnEdit.Enabled = false;
                    this.btnSave.Enabled = false;
                    this.btnDontSave.Enabled = false;
                }
                else
                {
                    this.btnDelete.Enabled = true;
                    this.btnEdit.Enabled = true;
                    this.btnSave.Enabled = true;
                    this.btnDontSave.Enabled = true;
                }
            }
            else
            {
                if (gridViewUser.RowCount == 0)
                {
                    this.btnDelete.Enabled = false;
                    this.btnEdit.Enabled = false;
                    this.btnSave.Enabled = false;
                    this.btnDontSave.Enabled = false;
                }
                else
                {
                    this.btnDelete.Enabled = true;
                    this.btnEdit.Enabled = true;
                    this.btnSave.Enabled = true;
                    this.btnDontSave.Enabled = true;
                }
            }
        }
        private void reLoadGroupUserThanhPhan()
        {
            try
            {
                if (xtraTabControlLeft.SelectedTabPage.Name.Equals("xtraTabPageGroup"))
                {
                    UserManUtil.getAllUserByGroupId(gridControlThanhPhanGroupUser,ref dsGetUserByGroup,
                        HelpNumber.ParseInt64(gridViewGroup.GetDataRow(gridViewGroup.FocusedRowHandle)["GROUPID"]), true);
                }
                else
                {
                    UserManUtil.getAllGroupByUserId(gridControlThanhPhanGroupUser,ref dsGetGroupByUser,
                        HelpNumber.ParseInt64(gridViewUser.GetDataRow(gridViewUser.FocusedRowHandle)["USERID"]), true);
                }
            }
            catch { }
        }
        
        private void loadGridViewUserSelectionChanged()
        {
            if (gridViewUser.FocusedRowHandle >= 0)
            {
                DataRow row = gridViewUser.GetDataRow(gridViewUser.FocusedRowHandle);
                DataSet ds = Feature.getFeatureUserAdapter(HelpNumber.ParseInt64(row["userid"]), "USER_FEATURE_TBL");
                DataViewManager dvManager = new DataViewManager(ds);
                DataView dv = dvManager.CreateDataView(ds.Tables["USER_FEATURE_TBL"]);
                gridControlFeature.DataSource = dv;
                SetDisableFunction();

                DataSet ds2 = BaoBieu.getReportUserAdapter(HelpNumber.ParseInt64(row["userid"]), "USER_REPORT_TBL");
                DataViewManager dvManager2 = new DataViewManager(ds2);
                DataView dv2 = dvManager2.CreateDataView(ds2.Tables["USER_REPORT_TBL"]);
                gridControlReport.DataSource = dv2;
                
                UserManUtil.getAllGroupByUserId(gridControlThanhPhanGroupUser, ref dsGetGroupByUser, HelpNumber.ParseInt64(row["userid"]), false);
                if (row["username"].ToString() == "admin")
                {
                    this.btnDelete.Enabled = false;
                }
                else
                {
                    this.btnDelete.Enabled = true;
                }
            }
            else
            {
                gridControlFeature.DataSource = null;
                gridControlReport.DataSource = null;
            }
            
        }
        private void loadGridViewGroupSelectionChanged()
        {
            if (gridViewGroup.FocusedRowHandle >= 0)
            {
                DataRow row = gridViewGroup.GetDataRow(gridViewGroup.FocusedRowHandle);
                DataSet ds = Feature.getFeatureGroupAdapter(HelpNumber.ParseInt64(row["groupid"].ToString()), "GROUP_FEATURE_TBL");
                DataViewManager dvManager = new DataViewManager(ds);
                DataView dv = dvManager.CreateDataView(ds.Tables["GROUP_FEATURE_TBL"]);
                gridControlFeature.DataSource = dv;
                SetDisableFunction();

                DataSet ds2 = BaoBieu.getReportGroupAdapter(HelpNumber.ParseInt64(row["groupid"].ToString()), "GROUP_REPORT_TBL");
                DataViewManager dvManager2 = new DataViewManager(ds2);
                DataView dv2 = dvManager2.CreateDataView(ds2.Tables["GROUP_REPORT_TBL"]);
                gridControlReport.DataSource = dv2;

                UserManUtil.getAllUserByGroupId(gridControlThanhPhanGroupUser,ref dsGetUserByGroup, HelpNumber.ParseInt64(row["groupid"]), false);
            }
            else
            {
                gridControlFeature.DataSource = null;
                gridControlReport.DataSource = null;
            }
        }
        #endregion



        #region ILangable Members

        public List<Control> GetLangableControls()
        {
            List<Control> controls = new List<Control>();
            controls.Add(this.gridControlFeature);
            controls.Add(this.gridControlGroup);
            controls.Add(this.gridControlReport);
            controls.Add(this.gridControlUser);
            controls.Add(this.gridControlThanhPhanGroupUser);
            return controls;
        }

        #endregion

        #region IFormatable Members

        public List<Control> GetFormatControls()
        {
            return null;
        }

        #endregion       

        private void SetDisableFunction()
        {
            object obj = null;
            try { obj = ((DataView)gridViewFeature.DataSource).DataViewManager.DataSet.Tables[0]; } catch { }
            if (obj != null)
            {
                DataTable tb = (DataTable)obj;
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    DataRow row = tb.Rows[i];

                    if (row["ISREAD"] == null || row["ISREAD"].ToString() == "N" || row["ISREAD"].ToString() == "")
                    {
                        HelpGrid.SetCellDisable(gridViewFeature, "ISREAD_BIT", "ID", HelpNumber.ParseInt64(row["ID"]));
                        
                    }
                    if (row["ISINSERT"] == null || row["ISINSERT"].ToString() == "N" || row["ISINSERT"].ToString() == "")
                    {
                        HelpGrid.SetCellDisable(gridViewFeature, "ISINSERT_BIT", "ID", HelpNumber.ParseInt64(row["ID"]));
                        
                    }
                    if (row["ISUPDATE"] == null || row["ISUPDATE"].ToString() == "N" || row["ISUPDATE"].ToString() == "")
                    {
                        HelpGrid.SetCellDisable(gridViewFeature, "ISUPDATE_BIT", "ID", HelpNumber.ParseInt64(row["ID"]));
                        
                    }
                    if (row["ISDELETE"] == null || row["ISDELETE"].ToString() == "N" || row["ISDELETE"].ToString() == "")
                    {
                        HelpGrid.SetCellDisable(gridViewFeature, "ISDELETE_BIT", "ID", HelpNumber.ParseInt64(row["ID"]));
                        
                    }
                    if (row["ISPRINT"] == null || row["ISPRINT"].ToString() == "N" || row["ISPRINT"].ToString() == "")
                    {
                        HelpGrid.SetCellDisable(gridViewFeature, "ISPRINT_BIT", "ID", HelpNumber.ParseInt64(row["ID"]));

                    }
                    if (row["ISEXPORT"] == null || row["ISEXPORT"].ToString() == "N" || row["ISEXPORT"].ToString() == "")
                    {
                        HelpGrid.SetCellDisable(gridViewFeature, "ISEXPORT_BIT", "ID", HelpNumber.ParseInt64(row["ID"]));

                    }
                }
            }
        }                        
    }
}
