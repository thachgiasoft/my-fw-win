using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using ProtocolVN.Framework.Core;
using ProtocolVN.Framework.Win;

namespace ProtocolVN.Framework.Win
{
    //PHUOCNC : Chưa xây dựng chức năng In
    //          Nhân viên chưa thể hiện được dạng cây tổ chức
    //          Dư một ToolStrip Container
    public partial class frmUserLog : XtraFormPL
    {
        public frmUserLog()
        {
            InitializeComponent();
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.btnDong.Visible = false;
        }

        private void frmOperationLog_Load(object sender, EventArgs e)
        {
            this.btnDong.Image = FWImageDic.CLOSE_IMAGE20;
            this.btnIn.Image = FWImageDic.PRINT_IMAGE20;
            this.btnXoa.Image = FWImageDic.DELETE_IMAGE20;
            this.btnRefresh.Image = FWImageDic.REFRESH_IMAGE20;
            
            initData();           
        }

        private void lbUser_DoubleClick(object sender, System.EventArgs e)
        {
            if (lbUser.SelectedIndex <= 0) return;
            User u = new User();
            u.username = lbUser.SelectedItem.ToString();
            u.loadByUserName();
            frmUserChild form = new frmUserChild(null, "EDIT", u.id.ToString());
            ProtocolForm.ShowModalDialog(this, form);
        }

        private void lbUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbUser.SelectedIndex < 0) return;
            if (lbUser.SelectedIndex == 0)
            {
                if ((DataView) gridOperationLog.DataSource!=null)
                    ((DataView)gridOperationLog.DataSource).RowFilter = "";
            }
            else
            {
                string username = lbUser.SelectedItem.ToString();
                ((DataView)gridOperationLog.DataSource).RowFilter = "USERNAME='" + username + "'";                
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (PLMessageBox.ShowConfirmMessage("Bạn có chắc chắn muốn xóa dữ liệu này?") == DialogResult.Yes)
            {
                Delete();
            }            
        }

        public void initData()
        {
            loadUsersListBox();
            gridOperationLog.DataSource = UserLog.LoadAllOperationLog();
            ((DataView)gridOperationLog.DataSource).Sort = "CDATE DESC";
        }

        //PHUOCNC : Hàm này ko tối ưu nên viết vào 1 câu truy vấn.
        private void loadUsersListBox()
        {
            lbUser.Items.Clear();
            List<User> listUser = User.loadAllUserInfo();
            
            lbUser.Items.Add("Tất cả người dùng");

            foreach (User user in listUser)
            {
                //lbUser.Items.Add(DAUser.Instance.GetFullName(user.id));
                lbUser.Items.Add(user.username);
            }
        }

        public void Delete()        
        {
            if (gridView1.SelectedRowsCount <= 0) return;

            try
            {
                for (int i = gridView1.SelectedRowsCount - 1; i >= 0; i--)
                {
                    int index = gridView1.GetSelectedRows()[i];
                    if (index >= 0)
                    {
                        long userid = HelpNumber.ParseInt64(gridView1.GetRowCellValue(index, "USERID"));
                        DateTime date = (DateTime)gridView1.GetRowCellValue(index, "CDATE");
                        UserLog.Delete(userid, date);
                        gridView1.DeleteRow(index);
                    }
                    else
                    {
                        int count = gridView1.GetChildRowCount(index);
                        for (int j = count - 1; j >= 0; j--)
                        {
                            int indexchild = gridView1.GetChildRowHandle(index, j);
                            if (indexchild >= 0)
                            {
                                long userid2 = HelpNumber.ParseInt64(gridView1.GetRowCellValue(indexchild, "USERID"));
                                DateTime date2 = (DateTime)gridView1.GetRowCellValue(indexchild, "CDATE");
                                UserLog.Delete(userid2, date2);
                                gridView1.DeleteRow(indexchild);
                            }
                        }
                    }
                }
            }
            catch{}
        }

        private void btnDong_Click(object sender , EventArgs e)
        {
            this.Close();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            this.gridOperationLog.ShowPrintPreview();
        }
     }
}