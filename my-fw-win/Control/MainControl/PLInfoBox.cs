using System;
using System.Windows.Forms;
using DevExpress.Utils.Controls;
using System.Drawing;

namespace ProtocolVN.Framework.Win
{
    /// <summary>Control cho phép hiển thị các thông tin 
    ///     Người cập nhật, 
    ///     Ngày cập nhật, 
    ///     Người duyệt,
    ///     Ngày duyệt    
    /// </summary>
    public partial class PLInfoBox : DevExpress.XtraEditors.XtraUserControl
    {
        frmNoteBox box;
        public PLInfoBox(){
            InitializeComponent();           
        }

        public void _init(string NguoiCapNhat, string NgayCapNhat, string NguoiDuyet, string NgayDuyet){
            this.box = new frmNoteBox(this.simpleButton1);
            box.AddItem("Người cập nhật", NguoiCapNhat);
            box.AddItem("Ngày cập nhật", NgayCapNhat);
            box.AddItem("Người duyệt", NguoiDuyet);
            box.AddItem("Ngày duyệt", NgayDuyet);
            this.simpleButton1.Enabled = true;
        }

        public void _init(string NguoiCapNhat, string NgayCapNhat)
        {
            this.box = new frmNoteBox(this.simpleButton1);
            box.AddItem("Người cập nhật", NguoiCapNhat);
            box.AddItem("Ngày cập nhật", NgayCapNhat);
            this.simpleButton1.Enabled = true;
        }

        private void simpleButton1_MouseDown(object sender, MouseEventArgs e)
        {
            if(box !=null) box.Show(e.X, e.Y);
        }

        private void simpleButton1_MouseUp(object sender, MouseEventArgs e)
        {
            if(box!=null) box.Hide();
        }

        private void simpleButton1_MouseHover(object sender, EventArgs e)
        {
            //box.Show(this.Location.X + 5, this.Location.Y + 5);
        }

        private void simpleButton1_MouseMove(object sender, MouseEventArgs e)
        {
            //box.Hide();
        }
    }


    /// <summary>
    /// Lớp cho phép hiển thị thông tin chỉ đọc trong không gian nhỏ.
    /// Ví dụ thông tin info
    /// </summary>
    public class frmNoteBox
    {
        private DataGridView grid = new DataGridView();
        private ControlBase control;
        private DataGridViewTextBoxColumn Label = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn Value = new DataGridViewTextBoxColumn();
        private DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();

        private int max_width = 0; 

        /// <summary>
        /// Vị trí ToolTip được đặt theo mặt định là BottomRight
        /// </summary>
        /// <param name="nameControl"></param>
        public frmNoteBox(ControlBase nameControl)
        {
            control = nameControl;
            Init();
        }
        private void Init()
        {
            control.FindForm().Controls.Add(grid);
            cellStyle.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            Label.DefaultCellStyle = cellStyle;
            grid.Visible = false;
            grid.AllowUserToAddRows = false;
            grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
            grid.ColumnHeadersVisible = false;
            grid.Columns.AddRange(new DataGridViewColumn[] { Label, Value });
            grid.ReadOnly = true;
            grid.RowHeadersVisible = false;
            grid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            grid.DefaultCellStyle.SelectionBackColor = Color.Yellow;
            grid.DefaultCellStyle.SelectionForeColor = Color.Black;

            //control.MouseMove += new MouseEventHandler(control_MouseMove);
            //control.MouseHover += new MouseEventHandler(control_MouseMove);
            //control.MouseLeave += new EventHandler(control_MouseLeave); 
        }
        /// <summary>
        /// Thêm mới 1 dòng vào ToolTip
        /// </summary>
        /// <param name="_label"></param>
        /// <param name="_value"></param>
        public void AddItem(string _label, string _value)
        {
            grid.Rows.Add();
            grid.Rows[grid.Rows.Count - 1].Cells[0].Value = _label;
            grid.Rows[grid.Rows.Count - 1].Cells[1].Value = _value;
            grid.SelectAll();
            grid.AutoResizeColumn(0);
            grid.AutoResizeColumn(1);
            //grid.Size = new System.Drawing.Size(Label.Width + Value.Width, grid.Rows.Count * 22);

            int width = TextRenderer.MeasureText(_value, grid.Font).Width;
            if (width > max_width)
                max_width = width;
            grid.Size = new System.Drawing.Size(Label.Width + max_width, grid.Rows.Count * 22);            
        }

        public void Show(int X, int Y)
        {
            grid.Visible = true;
            grid.BringToFront();
            grid.Location = new System.Drawing.Point(X, Y);
        }

        public void Hide()
        {
            grid.Visible = false;
        }
    }
}
