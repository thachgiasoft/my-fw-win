using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using pl.fw.layout;
using ProtocolVN.Framework.Win;

namespace pl.fw.layout
{
    public partial class frmTrialXtraLayoutControl : DevExpress.XtraEditors.XtraForm
    {
      
        public frmTrialXtraLayoutControl()
        {
            InitializeComponent();
            //CHAUTV : Gọi thực hiện layout
            TrialXtraLayoutControl.InitLayoutControlXtraForm (this, "pl.fw.layout", layoutControl1, Application.StartupPath + "\\xmllayout");
        }

        public static DataTable CreateDatatable()
        {
            DataTable data = new DataTable("KHACH_HANG");
            data.Columns.AddRange(new DataColumn[] { 
                new DataColumn("ID",Type.GetType("System.Int32")),
                new DataColumn("NAME",Type.GetType("System.String")),
                new DataColumn("NGAY_SINH",Type.GetType("System.DateTime")),
                new DataColumn("NOI_SINH",Type.GetType("System.String")),
                new DataColumn("TEN_NUOC",Type.GetType("System.String")),
                new DataColumn("NGHE_NGHIEP",Type.GetType("System.String")),
                new DataColumn("THONG_TIN_THEM",Type.GetType("System.String")),
                new DataColumn("HINH",Type.GetType("System.Byte"))
            });
            data.Rows.Add(new object[] { 1, "Trần Văn Châu", new DateTime(1984, 3, 9), "Hai Minh - Hai Hau - Nam Dinh", "Việt Nam", "Lap trinh vien", "Email : ngocminhchau_0903@yahoo.com", null });
            data.Rows.Add(new object[] { 2, "Nguyễn Văn Thoại", new DateTime(1984, 3, 13), "Kiên Giang", "Việt Nam", "Giáo viên", "SĐT : 0955990903", null });
            data.Rows.Add(new object[] { 3, "Phạm Thị Quyên", new DateTime(1984, 11, 4), "Thanh Hóa", "Việt Nam", "Công nhân", "Sở trường : nhanh nhẹn", null });
            data.Rows.Add(new object[] { 4, "Nguyễn Giang Nam", new DateTime(1984, 8, 2), "Nam Định", "Việt Nam", "Lập trình viênn", "Sở thích : thể thao", null });
            data.Rows.Add(new object[] { 5, "Phạm Châu Thành", new DateTime(1984, 6, 19), "Biên Hòa", "Viet nam", "Giáo viên", "", null });
            return data;
        }

        private void frmDemoXtralayout2_Load_1(object sender, EventArgs e)
        {
            DataTable data = CreateDatatable();
            dataNavigator1.DataSource = data;
            txt_HoTen.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", data, "NAME", true));
            NoiSinh.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", data, "NOI_SINH", true));
            NgaySinh.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", data, "NGAY_SINH", true));
            NgheNghiep.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", data, "NGHE_NGHIEP", true));
            TenNuoc.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", data, "TEN_NUOC", true));
            ThongTinThem.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", data, "THONG_TIN_THEM", true));
            //pictureEditHinhAnh.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", data, "HINH", true));
            //pictureEditHinhAnh.Image = Image.FromFile(Application.StartupPath +  "\\images\\film1-de.png");
            dsFile.Properties.Items.Add("chautv");
            dsFile.Properties.Items.Add("tuoitt");
            checkEdit1.Checked = false;
        }
        
        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (dsFile.Text.Trim() != string.Empty)
            {
                TrialXtraLayoutControl.LoadFromXml(layoutControl1, Application.StartupPath + "\\" +  dsFile.Text + ".xml");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (filename.Text.Trim().Length > 0)
            {
                TrialXtraLayoutControl.SaveXmlToDr(layoutControl1,"", filename.Text.Trim() + ".xml");
                dsFile.Properties.Items.Add(filename.Text.Trim() + ".xml");
                filename.Text = "";
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked == true)
            {
                layoutControl1.ShowCustomizationForm();
            }
            else
                layoutControl1.HideCustomizationForm();
        }

      

        
    }
}