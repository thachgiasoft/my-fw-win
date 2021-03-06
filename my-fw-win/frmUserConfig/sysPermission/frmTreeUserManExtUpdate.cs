using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;
using ProtocolVN.Framework.Core;
using System.ComponentModel;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using System.Drawing;
using DevExpress.XtraTreeList.Nodes;

namespace ProtocolVN.Framework.Win
{
    //Châu sửa khắc phục vấn đề mở những option ko cho phép chọn chưa kiểm tra

    /// <summary>
    /// PHUOCC
    /// Quản lý người dùng
    /// Hỗ trợ phân quyền trên Truy cập - Thêm - Xóa - Sửa
    /// </summary>
    public sealed partial class frmTreeUserManExt : XtraFormPL
    {
        #region Static
        public static string MenuItem(string ParentID, bool IsSep)
        {
            return MenuBuilder.CreateItem(typeof(frmTreeUserManExt).FullName,
                "Quản lý người dùng",
                ParentID, true,
                typeof(frmTreeUserManExt).FullName,
                true, IsSep, "", true, "", "");
        }
        #endregion     

        public bool isView = false;

        public frmTreeUserManExt()
        {
            InitializeComponent();
            
            //WinLaw.checkLaw(this);

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
            //gridViewFeature.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
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
            //this.xtraTabPage1.Text = "Người dùng";
            //this.gridViewThanhPhanGroupUser.GroupPanelText = "Danh sách người dùng trong nhóm";
            HelpGrid.SetTitle(this.gridViewThanhPhanGroupUser, "Danh sách người dùng trong nhóm");
            HelpGrid.ShowNumOfRecord(this.gridControlGroup);
            HelpGrid.ShowNumOfRecord(this.gridControlThanhPhanGroupUser);
            HelpGrid.ShowNumOfRecord(this.gridControlUser);
            HelpGrid.ShowNumOfRecord(this.gridControlReport);
            //XtraGridSupport.BitCBBToCheckImageCombo(this.gridViewFeature);
            XtraGridSupport.BitCBBToCheckImageCombo(this.gridViewReport);

            HelpTreeColumn.CotCheckEdit(colTreeListISREAD_BIT, "ISREAD_BIT");
            HelpTreeColumn.CotCheckEdit(colTreeListISINSERT_BIT, "ISINSERT_BIT");
            HelpTreeColumn.CotCheckEdit(colTreeListISUPDATE_BIT, "ISUPDATE_BIT");
            HelpTreeColumn.CotCheckEdit(colTreeListISDELETE_BIT, "ISDELETE_BIT");
            HelpTreeColumn.CotCheckEdit(colTreeListISPRINT_BIT, "ISPRINT_BIT");
            HelpTreeColumn.CotCheckEdit(colTreeListISEXPORT_BIT, "ISEXPORT_BIT");    
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
                //this.xtraTabPage1.Text = "Người dùng";
                //this.gridViewThanhPhanGroupUser.GroupPanelText = "Danh sách người dùng trong nhóm";
                HelpGrid.SetTitle(this.gridViewThanhPhanGroupUser, "Danh sách người dùng trong nhóm");
                loadGridViewGroupSelectionChanged();
            }
            else
            {
                //this.xtraTabPage1.Text = "Nhóm người dùng";
                //this.gridViewThanhPhanGroupUser.GroupPanelText = "Danh sách nhóm chứa người dùng";
                HelpGrid.SetTitle(this.gridViewThanhPhanGroupUser, "Danh sách nhóm chứa người dùng");
                loadGridViewUserSelectionChanged();
            }
        }

        private void gridControlGroupAndUser_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnEdit_Click();
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
            //xtraTabControlLeft_Click(null, null);
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
            List<Feature> features = new List<Feature>();
            List<BaoBieu> reports = new List<BaoBieu>();
            if (this.xtraTabControlLeft.SelectedTabPage.Name.Equals("xtraTabPageGroup"))
            {
                foreach (int j in gridViewGroup.GetSelectedRows())
                {
                    DataRow row1 = this.gridViewGroup.GetDataRow(j);
                    long groudId = HelpNumber.ParseInt64(row1["groupid"].ToString());

                    //for (int i = 0; i < gridViewFeature.RowCount; i++)
                    //{
                    //    Feature feature = new Feature();
                    //    feature = this.getFeature(this.gridViewFeature.GetDataRow(i));
                    //    features.Add(feature);
                    //}
                    //HUNG//treelist                    
                    DataTable dt = treeListFeature.DataSource as DataTable;
                    foreach (DataRow row in dt.Rows)
                    {
                        //Chi luu child row, khong luu parent row
                        if (row["PARENTID"] != DBNull.Value)
                        {
                            Feature feature = new Feature();
                            feature = this.getFeature(row);
                            features.Add(feature);
                        }
                    }                
                    ///---------------

                    for (int i = 0; i < gridViewReport.RowCount; i++)
                    {
                        BaoBieu report = new BaoBieu();
                        report = this.getReport(this.gridViewReport.GetDataRow(i));
                        reports.Add(report);
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
                    long userID = HelpNumber.ParseInt64(row1["userid"].ToString());

                    //for (int i = 0; i < gridViewFeature.RowCount; i++)
                    //{
                    //    Feature feature = new Feature();
                    //    feature = this.getFeature(this.gridViewFeature.GetDataRow(i));
                    //    features.Add(feature);
                    //}

                    //HUNG//treelist                    
                    DataTable dt = treeListFeature.DataSource as DataTable;
                    foreach (DataRow row in dt.Rows)
                    {
                        //Chi luu child row, khong luu parent row
                        if (row["PARENTID"] != DBNull.Value)
                        {
                            Feature feature = new Feature();
                            feature = this.getFeature(row);
                            features.Add(feature);
                        }
                    }
                    ///---------------
                    for (int i = 0; i < gridViewReport.RowCount; i++)
                    {
                        BaoBieu report = new BaoBieu();
                        report = this.getReport(this.gridViewReport.GetDataRow(i));
                        reports.Add(report);
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
        private void getAllUserByGroupId(ref DataSet ds, long groupid, bool isNew)
        {
            //string select = "select group_cat.groupid, group_cat.groupname, user_cat.userid, user_cat.username, employee.name as employee_name ,department.name as department_name  from group_cat " +
            //"inner join group_user_rel  on group_cat.groupid=group_user_rel.groupid " +
            //"join user_cat on user_cat.userid=  group_user_rel.userid " +
            //"join employee  on employee.id=  user_cat.employee_id " +
            //"join department on department.id=  employee.department_id where 1=1";
            string select = "select group_cat.groupid, group_cat.groupname, user_cat.userid, user_cat.username, DM_NHAN_VIEN.name as employee_name ,department.name as department_name  from group_cat " +
            "inner join group_user_rel on group_cat.groupid=group_user_rel.groupid " +
            "join user_cat on user_cat.userid=group_user_rel.userid " +
            "join DM_NHAN_VIEN  on DM_NHAN_VIEN.id=  user_cat.employee_id " +
            "join department on department.id=DM_NHAN_VIEN.department_id where 1=1";
            if (ds == null)
            {
                ds = new DataSet();
                QueryBuilder query = new QueryBuilder(select);
                query.addCondition("1=1");
                ds = DABase.getDatabase().LoadDataSet(query, "tblUSER");
            }
            if (isNew == true)
            {
                ds = DABase.getDatabase().LoadDataSet(new QueryBuilder(select), "tblUSER");
            }

            //gridControlThanhPhanGroupUser.DataSource = null;
            gridControlThanhPhanGroupUser.DataSource = ds.Tables[0].DefaultView;
            ds.Tables[0].DefaultView.RowFilter = "groupid =" + groupid;
        }

        private void getAllGroupByUserId(ref DataSet ds, long Userid, bool isNew)
        {
            string select = "select  group_cat.groupid, groupname, user_cat.userid from group_cat " +
                            "inner join group_user_rel  on group_cat.groupid=group_user_rel.groupid " +
                            "join user_cat on user_cat.userid = group_user_rel.userid where 1=1";
            if (ds == null)
            {
                ds = new DataSet();
                ds = DABase.getDatabase().LoadDataSet(new QueryBuilder(select), "tblGROUP");
            }
            if (isNew == true)
            {
                ds = DABase.getDatabase().LoadDataSet(new QueryBuilder(select), "tblGROUP");
            }
            //gridControlThanhPhanGroupUser.DataSource = null;
            gridControlThanhPhanGroupUser.DataSource = ds.Tables[0].DefaultView;
            ds.Tables[0].DefaultView.RowFilter = "userid =" + Userid;
        }
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
                    getAllUserByGroupId(ref dsGetUserByGroup,
                        HelpNumber.ParseInt64(gridViewGroup.GetDataRow(gridViewGroup.FocusedRowHandle)["GROUPID"]), true);
                }
                else
                {
                    getAllGroupByUserId(ref dsGetGroupByUser,
                        HelpNumber.ParseInt64(gridViewUser.GetDataRow(gridViewUser.FocusedRowHandle)["USERID"]), true);
                }
            }
            catch { }
        }
        private Feature getFeature(DataRow row)
        {
            Feature feature = new Feature();

            feature.id = HelpNumber.ParseInt64(row["ID"].ToString().Trim());
            feature.featureName = row["NAME"].ToString().Trim();
            feature.isRead = (row["isread_bit"].ToString().Equals("Y") ? true : false);
            feature.isInsert = (row["isinsert_bit"].ToString().Equals("Y") ? true : false);
            feature.isUpdate = (row["isupdate_bit"].ToString().Equals("Y") ? true : false);
            feature.isDelete = (row["isdelete_bit"].ToString().Equals("Y") ? true : false);
            feature.isPrint = (row["isprint_bit"].ToString().Equals("Y") ? true : false);
            feature.isExport = (row["isexport_bit"].ToString().Equals("Y") ? true : false);
            return feature;
        }
        private BaoBieu getReport(DataRow row)
        {
            BaoBieu report = new BaoBieu();

            report.id = HelpNumber.ParseInt64(row["ID"].ToString().Trim());
            report.keyid = row["KEYID"].ToString().Trim();
            report.reportName = row["NAME"].ToString().Trim();
            report.isRead = (row["isread_bit"].ToString().Equals("Y") ? true : false);

            return report;
        }
        private void loadGridViewUserSelectionChanged()
        {
            if (gridViewUser.FocusedRowHandle >= 0)
            {
                DataRow row = gridViewUser.GetDataRow(gridViewUser.FocusedRowHandle);
                //DataSet ds = Feature.getFeatureUserAdapter(HelpNumber.ParseInt64(row["userid"].ToString()), "USER_FEATURE_TBL");
                //DataViewManager dvManager = new DataViewManager(ds);
                //DataView dv = dvManager.CreateDataView(ds.Tables["USER_FEATURE_TBL"]);
                //gridControlFeature.DataSource = dv;


                //HUNG///
                DataSet dsTemp = getUserFeatureAdapter1(HelpNumber.ParseInt64(row["userid"].ToString()), "USER_FEATURE_TBL");
                setModifyFeatureDataSetAfterLoad(dsTemp);
                treeListFeature.KeyFieldName = "ID";
                treeListFeature.ParentFieldName = "PARENTID";
                treeListFeature.DataSource = dsTemp.Tables[0];
                //treeListFeature.ExpandAll();
                //-----------------------------

                //START : CHAUTV
                SetDisableFunction(dsTemp);
                //END : CHAUTV

                DataSet ds2 = BaoBieu.getReportUserAdapter(HelpNumber.ParseInt64(row["userid"].ToString()), "USER_REPORT_TBL");
                DataViewManager dvManager2 = new DataViewManager(ds2);
                DataView dv2 = dvManager2.CreateDataView(ds2.Tables["USER_REPORT_TBL"]);
                gridControlReport.DataSource = dv2;
               
                getAllGroupByUserId(ref dsGetGroupByUser, HelpNumber.ParseInt64(row["userid"]), false);
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
                //gridControlFeature.DataSource = null;
                treeListFeature.DataSource = null;
                gridControlReport.DataSource = null;
            }
            
        }

        private static void setModifyFeatureDataSetAfterLoad(DataSet dsTemp)
        {
            if (dsTemp == null || dsTemp.Tables.Count == 0) return;
            foreach (DataRow parentRow in dsTemp.Tables[0].Rows)
            {
                if (parentRow["PARENTID"] == DBNull.Value)//parent row
                {
                    //4 bien nay dung de kiem tra va khoi tao gia tri cho parent row trong dataset
                    bool isRead = true; bool isInsert = true; bool isUpdate = true; bool isDelete = true;

                    foreach (DataRow childRow in dsTemp.Tables[0].Rows)
                    {
                        //kiem tra cac childRow thuoc parent row, de cap nhat parent row
                        if (childRow["PARENTID"].ToString() == parentRow["ID"].ToString())
                        {
                            if (childRow["ISREAD_BIT"] == DBNull.Value || childRow["ISREAD_BIT"].ToString() == "N")
                            {
                                childRow["ISREAD_BIT"] = "N";
                                isRead = false;
                            }
                            if (childRow["ISINSERT_BIT"] == DBNull.Value || childRow["ISINSERT_BIT"].ToString() == "N")
                            {
                                childRow["ISINSERT_BIT"] = "N";
                                isInsert = false;
                            }
                            if (childRow["ISUPDATE_BIT"] == DBNull.Value || childRow["ISUPDATE_BIT"].ToString() == "N")
                            {
                                childRow["ISUPDATE_BIT"] = "N";
                                isUpdate = false;
                            }
                            if (childRow["ISDELETE_BIT"] == DBNull.Value || childRow["ISDELETE_BIT"].ToString() == "N")
                            {
                                childRow["ISDELETE_BIT"] = "N";
                                isDelete = false;
                            }
                        }
                    }
                    //sau khi kiem tra tat cac child row thuoc parent row
                    //luc nay se gan gia tri
                    if (isRead == true)//toan bo cac dong con deu Y
                        parentRow["ISREAD_BIT"] = "Y";
                    else//nguoc lai
                        parentRow["ISREAD_BIT"] = "N";
                    if (isInsert == true)
                        parentRow["ISINSERT_BIT"] = "Y";
                    else
                        parentRow["ISINSERT_BIT"] = "N";
                    if (isUpdate == true)
                        parentRow["ISUPDATE_BIT"] = "Y";
                    else
                        parentRow["ISUPDATE_BIT"] = "N";
                    if (isDelete == true)
                        parentRow["ISDELETE_BIT"] = "Y";
                    else
                        parentRow["ISDELETE_BIT"] = "N";
                }
            }
        }
        private void loadGridViewGroupSelectionChanged()
        {
            if (gridViewGroup.FocusedRowHandle >= 0)
            {
                DataRow row = gridViewGroup.GetDataRow(gridViewGroup.FocusedRowHandle);
                //DataSet ds = Feature.getFeatureGroupAdapter(HelpNumber.ParseInt64(row["groupid"].ToString()), "GROUP_FEATURE_TBL");
                //DataViewManager dvManager = new DataViewManager(ds);
                //DataView dv = dvManager.CreateDataView(ds.Tables["GROUP_FEATURE_TBL"]);
                //gridControlFeature.DataSource = dv;

                //HUNG
                DataSet dsTemp = getGroupFeatureAdapter1(HelpNumber.ParseInt64(row["groupid"].ToString()), "GROUP_FEATURE_TBL");
                setModifyFeatureDataSetAfterLoad(dsTemp);
                treeListFeature.KeyFieldName = "ID";
                treeListFeature.ParentFieldName = "PARENTID";
                treeListFeature.DataSource = dsTemp.Tables[0];
                //treeListFeature.ExpandAll();
                //-----------------------------
                //S:CHAUTV
                SetDisableFunction(dsTemp);
                //E:CHAUTV

                DataSet ds2 = BaoBieu.getReportGroupAdapter(HelpNumber.ParseInt64(row["groupid"].ToString()), "GROUP_REPORT_TBL");
                DataViewManager dvManager2 = new DataViewManager(ds2);
                DataView dv2 = dvManager2.CreateDataView(ds2.Tables["GROUP_REPORT_TBL"]);
                gridControlReport.DataSource = dv2;

                getAllUserByGroupId(ref dsGetUserByGroup, HelpNumber.ParseInt64(row["groupid"]), false);

            }
            else
            {
                //gridControlFeature.DataSource = null;
                treeListFeature.DataSource = null;
                gridControlReport.DataSource = null;
            }
        }
        #endregion

        

        #region ILangable Members

        public List<Control> GetLangableControls()
        {
            List<Control> controls = new List<Control>();
            //controls.Add(this.gridControlFeature);
            controls.Add(this.treeListFeature);
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

        #region CHAUTV: Disable Cell
        public static void SetCellDisableTree(TreeList tree,string FieldName,string KeyField,long KeyValue){
            SetCellDisableTree(tree, FieldName, KeyField, new long[] { KeyValue });
        }

        public static void SetCellDisableTree(TreeList tree,string FieldName,string KeyField,long[]KeyValues) {
            tree.CustomDrawNodeCell += delegate(object sender, CustomDrawNodeCellEventArgs e) {
                if (e.Node == tree.FocusedNode) {
                    return; 
                }

                if (e.Column.FieldName == FieldName)
                {
                    for (int vt = 0; vt <= KeyValues.Length - 1; vt++)
                    {
                        if (Convert.ToInt64(e.Node.GetValue(KeyField)) == KeyValues[vt])
                        {
                            e.Appearance.BackColor = Color.LightGray;
                        }
                    }
                }
            };
            tree.ShowingEditor += delegate(object sender, CancelEventArgs e) {
                TreeListNode note = tree.FocusedNode;
                if (note != null)
                {
                    for (int vt = 0; vt <= KeyValues.Length - 1; vt++)
                    {
                        if (Convert.ToInt64(note.GetValue(KeyField)) == KeyValues[vt]
                            && tree.FocusedColumn.FieldName == FieldName)
                        {
                            e.Cancel = true;
                        }
                    }
                }
            };
        }

        public void SetDisableFunction(DataSet dsTemp)
        {
            if (dsTemp == null) return;

            for (int i = 0; i < dsTemp.Tables[0].Rows.Count; i++)
            {
                DataRow _row = dsTemp.Tables[0].Rows[i];
                if (((_row["ISREAD"] == null) || (_row["ISREAD"].ToString() == "N")) || (_row["ISREAD"].ToString() == ""))
                {
                    SetCellDisableTree(this.treeListFeature, "ISINSERT_BIT", "ID", HelpNumber.ParseInt64(_row["ID"]));
                }
                if (((_row["ISINSERT"] == null) || (_row["ISINSERT"].ToString() == "N")) || (_row["ISINSERT"].ToString() == ""))
                {
                    SetCellDisableTree(this.treeListFeature, "ISINSERT_BIT", "ID",HelpNumber.ParseInt64(_row["ID"]));
                }
                if (((_row["ISUPDATE"] == null) || (_row["ISUPDATE"].ToString() == "N")) || (_row["ISUPDATE"].ToString() == ""))
                {
                    SetCellDisableTree(this.treeListFeature, "ISUPDATE_BIT", "ID", HelpNumber.ParseInt64(_row["ID"]));
                }
                if (((_row["ISDELETE"] == null) || (_row["ISDELETE"].ToString() == "N")) || (_row["ISDELETE"].ToString() == ""))
                {
                    SetCellDisableTree(this.treeListFeature, "ISDELETE_BIT", "ID", HelpNumber.ParseInt64(_row["ID"]));
                }
                if (((_row["ISPRINT"] == null) || (_row["ISPRINT"].ToString() == "N")) || (_row["ISPRINT"].ToString() == ""))
                {
                    SetCellDisableTree(this.treeListFeature, "ISPRINT_BIT", "ID", HelpNumber.ParseInt64(_row["ID"]));
                }
                if (((_row["ISEXPORT"] == null) || (_row["ISEXPORT"].ToString() == "N")) || (_row["ISEXPORT"].ToString() == ""))
                {
                    SetCellDisableTree(this.treeListFeature, "ISEXPORT_BIT", "ID", HelpNumber.ParseInt64(_row["ID"]));
                }
            }
        }
        #endregion


        public DataSet getUserFeatureAdapter1(long userId, string tableName)
        //PHUOC OK
        {
            //CHAUTV : Sửa
            DatabaseFB db = DABase.getDatabase();
            String selectStr = "SELECT f.ID, f.PARENT_ID PARENTID, f.NAME, f.DESCRIPTION, u.ISREAD_BIT,u.ISINSERT_BIT,u.ISUPDATE_BIT,u.ISDELETE_BIT, u.ISPRINT_BIT, u.ISEXPORT_BIT " + /*S:CHAUTV*/ ",f.ISREAD,f.ISINSERT,f.ISUPDATE,f.ISDELETE,f.ISPRINT, f.ISEXPORT " /*E:CHAUTV*/ + 
            "FROM (FEATURE_CAT as f LEFT JOIN USER_FEATURE_REL as u ON (f.ID = u.featureid and u.userid=@userId)) " +
            "WHERE f.VISIBLE_BIT = 'Y' " +
            "UNION " +
            "SELECT f.ID,f.PARENT_ID PARENTID, f.NAME, f.DESCRIPTION , u.ISREAD_BIT,u.ISINSERT_BIT,u.ISUPDATE_BIT,u.ISDELETE_BIT,u.ISPRINT_BIT, u.ISEXPORT_BIT " + /*S:CHAUTV*/ ",f.ISREAD,f.ISINSERT,f.ISUPDATE,f.ISDELETE,f.ISPRINT, f.ISEXPORT  " /*E:CHAUTV*/ +
            "FROM FEATURE_CAT as f INNER JOIN USER_FEATURE_REL as u ON (f.ID = u.featureid) " +
            "WHERE u.USERID = @userId and f.VISIBLE_BIT = 'Y'";

            DbCommand dbSelect = db.GetSQLStringCommand(selectStr);
            db.AddInParameter(dbSelect, "@userId", DbType.Int64, userId);
            DataSet ds = new DataSet();
            db.LoadDataSet(dbSelect, ds, tableName);

            return ds;
        }

        //public DataSet getFeatureGroupAdapter1(int groupId, string tableName)
        //{
        //    string cmdstr = "select feature_cat.id, feature_cat.parent_id parentid,  feature_cat.name, feature_cat.description, user_feature_rel.featureid, user_feature_rel.userid, user_feature_rel.isread_bit, user_feature_rel.isinsert_bit, user_feature_rel.isupdate_bit, user_feature_rel.isdelete_bit" +
        //                    " from user_feature_rel," +
        //                    " feature_cat where 1=1";
        //    return rad.db.DABase.getDatabase().LoadDataSet(new rad.db.QueryBuilder(cmdstr), "abc");
        //}

        public DataSet getGroupFeatureAdapter1(long groupId, string tableName)
        //PHUOC
        {
            DatabaseFB db = DABase.getDatabase();
            String selectStr = "SELECT f.ID, f.NAME,f.PARENT_ID PARENTID, f.DESCRIPTION, u.ISREAD_BIT,u.ISINSERT_BIT,u.ISUPDATE_BIT,u.ISDELETE_BIT,u.ISPRINT_BIT, u.ISEXPORT_BIT " + /*S:CHAUTV*/ ",f.ISREAD,f.ISINSERT,f.ISUPDATE,f.ISDELETE,f.ISPRINT, f.ISEXPORT  " /*E:CHAUTV*/ + 
            "FROM (FEATURE_CAT as f LEFT JOIN GROUP_FEATURE_REL as u ON (f.ID = u.featureid and u.groupid=@groupId)) " + 
            "WHERE f.VISIBLE_BIT = 'Y' " +
            "UNION " +
            "SELECT f.ID, f.NAME,f.PARENT_ID PARENTID, f.DESCRIPTION, u.ISREAD_BIT,u.ISINSERT_BIT,u.ISUPDATE_BIT,u.ISDELETE_BIT,u.ISPRINT_BIT, u.ISEXPORT_BIT " +  /*S:CHAUTV*/ ",f.ISREAD,f.ISINSERT,f.ISUPDATE,f.ISDELETE,f.ISPRINT, f.ISEXPORT  " /*E:CHAUTV*/ + 
            "FROM FEATURE_CAT as f INNER JOIN GROUP_FEATURE_REL as u ON (f.ID = u.featureid) " +
            "WHERE u.GROUPID = @groupId and f.VISIBLE_BIT = 'Y'";

            DbCommand dbSelect = db.GetSQLStringCommand(selectStr);
            db.AddInParameter(dbSelect, "@groupId", DbType.Int64, groupId);
            DataSet ds = new DataSet();
            db.LoadDataSet(dbSelect, ds, tableName);

            return ds;
        }
        private void treeListFeature_CellValueChanging(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Node != null)//co the kiem tra them kieu cot, chi cho cot kieu bool thoi, && e.Column.ColumnType==Type.GetType("System.Boolean"))
                {
                    string checkState = e.Value.ToString();
                    e.Node.SetValue(e.Column.VisibleIndex, checkState);
                    //chon nodeList cha
                    if (e.Node.Nodes.Count > 0)   //lap qua cac nodeList con
                        foreach (DevExpress.XtraTreeList.Nodes.TreeListNode item in e.Node.Nodes)
                            item.SetValue(e.Column.VisibleIndex, checkState);
                    else//chon node con
                    {
                        //lay nodeList cha cua nodeList hien hanh
                        DevExpress.XtraTreeList.Nodes.TreeListNode parentTreeListNode = e.Node.ParentNode;
                        bool flagFullCheck = true;
                        //lap qua cac nodeList con
                        foreach (DevExpress.XtraTreeList.Nodes.TreeListNode childTreeListNode in parentTreeListNode.Nodes)
                            if (childTreeListNode != null)
                                if (childTreeListNode.GetValue(e.Column.VisibleIndex).ToString() == "N")
                                    flagFullCheck = false;
                        if (flagFullCheck == false)
                            parentTreeListNode.SetValue(e.Column.VisibleIndex, "N");
                        else
                            parentTreeListNode.SetValue(e.Column.VisibleIndex, "Y");
                    }
                }
            }
            catch { }
        }

        private void splitContainerControl2_Resize(object sender, EventArgs e)
        {
           // splitContainerControl2.SplitterPosition = splitContainerControl2.Width / 2;
        }

        private void splitContainerControl1_Resize(object sender, EventArgs e)
        {
          //  splitContainerControl1.SplitterPosition = splitContainerControl1.Height / 2;
        }
    }
}
