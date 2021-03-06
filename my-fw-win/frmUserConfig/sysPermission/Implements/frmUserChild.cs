using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public partial class frmUserChild : XtraFormPL
    {
        private Form formUserMan;
        private User selectedUser;
        private string actionName;
        private List<Group> groups = new List<Group>();
        
        private long employee_id;

        public frmUserChild(Form formUserMan, string actionName, object selectedUserId)
        {
            InitializeComponent();
            //this.btnClose.Image = FWImageDic.CLOSE_IMAGE16;
            //this.btnSave.Image = FWImageDic.SAVE_IMAGE16;
            this.btnSelect.Image = FWImageDic.CHOICE_IMAGE16;

            this.formUserMan = formUserMan;
            this.actionName = actionName;
            this.selectedUser = new User();
            if (selectedUserId != null)
            {
                selectedUser.id = HelpNumber.ParseInt64(selectedUserId.ToString());
                selectedUser.loadByUserId();
                employee_id = selectedUser.employee_id;
                txtUsername.Enabled = false;
                btnSelect.Enabled = false;
            }
            initData();

            //HUNG
            //string select = "select employee.id , employee.name as employee_name, employee.department_id, department.name as department_name, user_cat.userid, user_cat.username " +
            //                "from employee left join user_cat on  employee.id = user_cat.employee_id " +
            //                "join department on department.id=employee.department_id where user_cat.userid is null and 1=1";
            string select = "select DM_NHAN_VIEN.id , DM_NHAN_VIEN.name as employee_name, DM_NHAN_VIEN.department_id, department.name as department_name, user_cat.userid, user_cat.username " +
                            "from DM_NHAN_VIEN left join user_cat on  DM_NHAN_VIEN.id = user_cat.employee_id " +
                            "join department on department.id=DM_NHAN_VIEN.department_id where user_cat.userid is null and 1=1";
            DataSet dsGrid = DABase.getDatabase().LoadDataSet(new QueryBuilder(select), "DETAIL");
            if (plChonUser == null)
                plChonUser = new PLChonNhanVien();
            plChonUser.Init("DEPARTMENT", "ID", "PARENT_ID", new string[] { "NAME" }, new string[] {"Tên phòng ban" }, dsGrid, new string[] { "Nhân viên ID", "Họ tên nhân viên", "Phòng ban ID", "Tên phòng ban", "Người dùng ID", "Tên truy cập" }, "DEPARTMENT_ID", "USERNAME", HelpGen.G_FW_ID);
            plChonUser.SetShowChonNhanVien(btnSelect);
            plChonUser.mDlgGetSelectDataset = new PLChonNhanVien.DlgGetSelectDataset(_getSelectedDataSet);
            plChonUser.mDlgGetUnSelectDataset = new PLChonNhanVien.DlgGetUnSelectDataset(_getUnSelectedDataSet);
            plChonUser.m_IsMultiselect = true;
            plChonUser.mVisibleGridColumn(new string[] { "id", "department_id", "department_name", "userid" });
            //--------------------------
        }

        //HUNG
        PLChonNhanVien plChonUser = null;//HUNG

        //HUNG
        private void _getUnSelectedDataSet()
        {
            employee_id = -1;
            txtFullname.Text = "";
        }

        private void _getSelectedDataSet(DataSet ds)
        {
            DataRow row = ds.Tables[0].Rows[0];
            if (row["id"] != DBNull.Value && row["id"] != null)
            {
                employee_id = HelpNumber.ParseInt64(row["id"]);
            }            
            txtFullname.Text = row["employee_name"].ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (validate()==false) return;            
            this.getData();
            bool flag = false;

            if (actionName.Equals("INSERT"))
            {
                flag = selectedUser.insert();
            }
            else if (actionName.Equals("EDIT"))
            {
                flag = selectedUser.update();
            }

            if (flag == true)
                this.Close();
            else
            {
                HelpMsgBox.ShowNotificationMessage("Dữ liệu không hợp lệ. Vui lòng nhập lại!", this);
                errorProvider.SetError(txtFullname, ErrorMsgLib.errorRequired("Tên nhân viên"));                
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkGroupList_MouseCaptureChanged(object sender, EventArgs e)
        {
            List<Group> selectedGroup = new List<Group>();
            for (int i = 0; i < checkGroupList.Items.Count; i++)
            {
                if (checkGroupList.GetItemChecked(i))
                {
                    selectedGroup.Add(groups[i]);
                }
            }

            selectedUser.groups = selectedGroup;
        }
        
        #region IStandardForm Members

        private void initData()
        {
            if (actionName.Equals("INSERT"))
            {
                List<Group> listGroup = Group.loadAllGroupInfo();
                this.groups = listGroup;
                foreach (Group group in this.groups)
                {
                    this.checkGroupList.Items.Add(new DevExpress.XtraEditors.Controls.CheckedListBoxItem(group.groupName));
                }

            }
            else if (actionName.Equals("EDIT"))
            {
                selectedUser.loadGroups();

                txtFullname.Text = selectedUser.fullname;
                txtUsername.Text = selectedUser.username;
                txtPassword.Text = selectedUser.password;
                txtPasswordVerify.Text = selectedUser.password;
                chkNeverChangePwd.EditValue = !selectedUser.isChangePass;
                chkDisable.EditValue = !selectedUser.isActive;

                List<Group> listGroup = Group.loadAllGroupInfo();
                this.groups = listGroup;
                foreach (Group group in listGroup)
                {
                    bool status = false;
                    foreach (Group groupFromUser in selectedUser.groups)
                    {
                        if (group.id == groupFromUser.id)
                        {
                            status = true;
                            break;
                        }
                    }
                    //PHUOC
                    if (status)
                        this.checkGroupList.Items.Add(new DevExpress.XtraEditors.Controls.CheckedListBoxItem(group.groupName, System.Windows.Forms.CheckState.Checked));
                    else
                        this.checkGroupList.Items.Add(new DevExpress.XtraEditors.Controls.CheckedListBoxItem(group.groupName));
                }

                //HUNG
                //kiểm tra nếu ở chế độ xem
                try
                {
                    if (formUserMan != null)
                    {
                        if ((bool)formUserMan.GetType().GetField("isView").GetValue(formUserMan) == true)
                        {
                            btnSave.Enabled = false;
                            btnSelect.Enabled = false;
                        }
                        else
                        {
                            btnSelect.Enabled = true;
                        }
                    }
                    else
                    {
                        btnSave.Enabled = false;
                        btnSelect.Enabled = false;
                    }
                }
                catch
                {
                    btnSave.Enabled = false;
                    btnSelect.Enabled = false;
                }
            }
        }

        private void trimAllData()
        {
            txtFullname.Text = txtFullname.Text.Trim();
            txtUsername.Text = txtUsername.Text.Trim();
            txtPassword.Text = txtPassword.Text.Trim();
            txtPasswordVerify.Text = txtPasswordVerify.Text.Trim();
        }

        private bool validate()
        {
            trimAllData();
            bool flag = true;
            errorProvider.ClearErrors();

            //if (HelpIsCheck.isBlankString(txtFullname.Text))
            //{
            //    flag = false;
            //    errorProvider.SetError(txtFullname, ErrorMsgLib.errorRequired("Tên người dùng"));
            //}

            if (HelpIsCheck.isBlankString(txtUsername.Text))
            {
                flag = false;
                errorProvider.SetError(txtUsername, ErrorMsgLib.errorRequired("Tên đăng nhập"));
            }
            else
            {
                if (actionName.Equals("INSERT") && User.exist(txtUsername.Text))
                {
                    flag = false;
                    errorProvider.SetError(txtUsername, ErrorMsgLib.errorExist("Tên đăng nhập"));
                }
            }

            if (HelpIsCheck.isBlankString(txtPassword.Text))
            {
                flag = false;
                errorProvider.SetError(txtPassword, ErrorMsgLib.errorRequired("Mật khẩu"));
            }
            else
            {
                if (!HelpIsCheck.isTwoStringEqual(txtPassword.Text, txtPasswordVerify.Text))
                {
                    flag = false;
                    errorProvider.SetError(txtPasswordVerify, ErrorMsgLib.errorEqual("Xác nhận mật khẩu"));
                }
            }

            return flag;
        }

        private void getData()
        {
            List<Group> selectedGroup = new List<Group>();
            for (int i = 0; i < checkGroupList.Items.Count; i++)
            {
                if (checkGroupList.GetItemChecked(i))
                {
                    selectedGroup.Add(groups[i]);
                }
            }
            selectedUser.isChangePass = !chkNeverChangePwd.Checked;
            selectedUser.isActive = !chkDisable.Checked;

            selectedUser.fullname = txtFullname.Text;
            selectedUser.username = txtUsername.Text;
            selectedUser.password = txtPassword.Text;

            selectedUser.employee_id = employee_id;
            selectedUser.groups = selectedGroup;            
        }

        #endregion

        private void frmUserChild_Load(object sender, EventArgs e)
        {
            HelpXtraForm.SetFix(this);
        }
    }   
}