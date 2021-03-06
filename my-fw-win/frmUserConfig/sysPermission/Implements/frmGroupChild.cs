using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public partial class frmGroupChild : XtraFormPL
    {
        private Form formUserMan;
        private Group selectedGroup;
        private string actionName;

        public frmGroupChild(Form formUserMan, string actionName, object selectedGroupId)
        {
            InitializeComponent();
            //this.btnClose.Image = FWImageDic.CLOSE_IMAGE16;
            //this.btnDelete.Image = FWImageDic.DELETE_IMAGE16;
            //this.btnSave.Image = FWImageDic.SAVE_IMAGE16;
            this.btnSelect.Image = FWImageDic.CHOICE_IMAGE16;

            this.formUserMan = formUserMan;
            this.actionName = actionName;
            this.selectedGroup = new Group();

            List<User> listUser = User.loadAllUserInfo();
            if (actionName.Equals("INSERT"))
            {
                
            }
            else if (actionName.Equals("EDIT"))
            {
                if (selectedGroupId != null)
                {
                    selectedGroup.id = HelpNumber.ParseInt64(selectedGroupId.ToString());
                    selectedGroup.load();
                }
                txtGroupName.EditValue = selectedGroup.groupName;
                //HUNG
                //string selectUserByGroup = "select  group_cat.groupid, group_cat.groupname, user_cat.userid as id,  user_cat.username, employee.name as employee_name ,department.name as department_name  from group_cat " +
                //                            "inner join group_user_rel  on group_cat.groupid=group_user_rel.groupid " +
                //                            "join user_cat on user_cat.userid=  group_user_rel.userid " +
                //                            "join employee  on employee.id=  user_cat.employee_id " +
                //                            "join department on department.id =  employee.department_id where 1=1";
                string selectUserByGroup = "select  group_cat.groupid, group_cat.groupname, user_cat.userid as id,  user_cat.username, DM_NHAN_VIEN.name as employee_name ,department.name as department_name  from group_cat " +
                                            "inner join group_user_rel  on group_cat.groupid=group_user_rel.groupid " +
                                            "join user_cat on user_cat.userid=  group_user_rel.userid " +
                                            "join DM_NHAN_VIEN  on DM_NHAN_VIEN.id=  user_cat.employee_id " +
                                            "join department on department.id =  DM_NHAN_VIEN.department_id where 1=1";
                QueryBuilder querySelect = new QueryBuilder(selectUserByGroup);
                querySelect.add("group_cat.groupid", Operator.Equal, selectedGroup.id, DbType.Int64);
                DataSet ds = DABase.getDatabase().LoadDataSet(querySelect, "DETAIL");
                if (ds != null)
                    gridControlThanhPhanUser.DataSource = ds.Tables[0]; ;
                                
                //kiểm tra nếu ở chế độ xem
                if ((bool)formUserMan.GetType().GetField("isView").GetValue(formUserMan) == true)
                {
                    btnSave.Enabled = false;
                    btnDelete.Enabled = false;
                    btnSelect.Enabled = false;
                }
            }

            //HUNG
            //string selectUserByDepartment = "select employee.id , employee.name as employee_name, employee.department_id, department.name as department_name, user_cat.userid, user_cat.username " +
            //               "from employee inner join user_cat on  employee.id = user_cat.employee_id " +
            //               "join department on department.id=employee.department_id where 1=1";
            string selectUserByDepartment = "select DM_NHAN_VIEN.id , DM_NHAN_VIEN.name as employee_name, DM_NHAN_VIEN.department_id, department.name as department_name, user_cat.userid, user_cat.username " +
                           "from DM_NHAN_VIEN inner join user_cat on  DM_NHAN_VIEN.id = user_cat.employee_id " +
                           "join department on department.id=DM_NHAN_VIEN.department_id where 1=1";
            DataSet dsGrid = DABase.getDatabase().LoadDataSet(new QueryBuilder(selectUserByDepartment), "DETAIL");
            if (plChonUser == null)
                plChonUser = new PLChonNhanVien();
            plChonUser.Init("DEPARTMENT", "ID","PARENT_ID", new string[] { "NAME" }, new string[] { "Tên phòng ban" }, dsGrid, new string[] { "Nhân viên ID", "Họ tên nhân viên", "Phòng ban ID", "Tên phòng ban", "Người dùng ID", "Tên truy cập" }, "DEPARTMENT_ID", "USERNAME", HelpGen.G_FW_ID);
            plChonUser.SetShowChonNhanVien(btnSelect);
            plChonUser.mDlgGetSelectDataset = new PLChonNhanVien.DlgGetSelectDataset(_getSelectedDataSet);
            plChonUser.mDlgGetUnSelectDataset = new PLChonNhanVien.DlgGetUnSelectDataset(_getUnSelectedDataSet);
            plChonUser.m_IsMultiselect = true;
            plChonUser.mVisibleGridColumn(new string[] { "id", "department_id","department_name", "userid" });
            
        }

        //HUNG
        PLChonNhanVien plChonUser = null;//HUNG
        
        //HUNG
        private void _getSelectedDataSet(DataSet  ds)
        {
            DataSet dstemp = null;
            if (gridControlThanhPhanUser.DataSource != null)
            {
                dstemp = (gridControlThanhPhanUser.DataSource as DataTable).DataSet;
                foreach (DataRow row in ds.Tables[0].Rows)
                    if (dstemp.Tables[0].Select("ID ='" + row["USERID"] + "'").Length == 0)
                    {
                        row["ID"] = row["USERID"];
                        dstemp.Merge(new DataRow[] { row });
                    }
            }
            else
                dstemp = ds.Copy();
            gridControlThanhPhanUser.DataSource = dstemp.Tables[0];           
        }//----------------------------

        private void _getUnSelectedDataSet()
        {
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (validate()==false) return;
            this.getData();
            if (actionName.Equals("INSERT"))
                selectedGroup.insert();
            else if (actionName.Equals("EDIT") )
                selectedGroup.update();
            this.Close();                     
        }

        public void trimAllData()
        {
            txtGroupName.Text = txtGroupName.Text.Trim();
        }

        public bool validate()
        {
            bool flag = true;
            trimAllData();
            errorProvider.ClearErrors();
            if (HelpIsCheck.isBlankString(txtGroupName.Text))
            {
                flag = false;
                errorProvider.SetError(txtGroupName, ErrorMsgLib.errorRequired("Tên nhóm người dùng"));
            }
            else
            {
                if (actionName.Equals("INSERT") && Group.exist(txtGroupName.Text))
                {
                    flag = false;
                    errorProvider.SetError(txtGroupName, ErrorMsgLib.errorExist("Tên nhóm người dùng"));
                }
            }

            return flag;
        }

        public void getData()
        {
            selectedGroup.groupName = txtGroupName.Text;
            List<User> selectedUser = new List<User>();

            //HUNG
            DataTable dtuser=gridControlThanhPhanUser.DataSource as DataTable;
            if(dtuser!=null)
                foreach (DataRow row in dtuser.Rows)
                {
                    if (row.RowState != DataRowState.Deleted)
                    {
                        User user = new User();
                        try
                        {
                            if (row["userid"] == DBNull.Value || row["userid"] == null)//Trường hợp edit
                                user.id = HelpNumber.ParseInt64(row["id"]);
                            else//Trường hợp add
                                user.id = HelpNumber.ParseInt64(row["userid"]);
                        }
                        catch
                        {
                            user.id = HelpNumber.ParseInt64(row["id"]);
                        }
                        selectedUser.Add(user);
                    }
                }
            
            selectedGroup.users = selectedUser;            
        }

        //HUNG
        private void simpleButtonXoaUser_Click(object sender, EventArgs e)
        {            
            this.gridViewThanhPhanUser.DeleteRow(this.gridViewThanhPhanUser.FocusedRowHandle);
        }

        private void frmGroupChild_Load(object sender, EventArgs e)
        {
            HelpXtraForm.SetFix(this);
        }
    }
}