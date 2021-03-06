using System;
namespace ProtocolVN.Framework.Win
{
    /// <summary>PREDICATE: Chọn tiền tệ và nhập tỷ giá nếu cần
    /// </summary>
    public partial class PLTienTeSelect : DevExpress.XtraEditors.XtraUserControl
    {
        private double value = 1;

        public PLTienTeSelect()
        {
            InitializeComponent();
        }

        public void _Init(bool IsTyGia)
        {
            if (IsTyGia == false)
            {
                calcEdit1.Visible = false;
                this.Width -= 20;
            }

        }
        public void _SetTienTe(bool isVND, double value)
        {
            if (value > 0){
                this.value = value;
                calcEdit1.Text = "" + value;
            }
            if (isVND == true)
                rdotiente.SelectedIndex = 0;
            else
                rdotiente.SelectedIndex = 1;
        }

        public double _GetTienTe()
        {            
            return value;
        }

        public bool _IsVND()
        {
            if (rdotiente.SelectedIndex == 0)
                return true;
            return false;
        }   

        private void rdotiente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdotiente.SelectedIndex == 0)
                calcEdit1.Enabled = false;
            else if(rdotiente.SelectedIndex ==1)
                calcEdit1.Enabled = true;
        }

        private void PLTienTeSelect_Load(object sender, EventArgs e)
        {
            calcEdit1.Enabled = false;
        }

        private void calcEdit1_Leave(object sender, EventArgs e)
        {
            if (calcEdit1.Value <= 0)
            {                
                calcEdit1.Focus();
            }
        }
    }
}
