using ProtocolVN.Framework.Core;
namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// PHUOCC
    /// Dùng cho việc tùy chọn sinh mã phiếu.
    /// </summary>
    public partial class PatternSelect : DevExpress.XtraEditors.XtraUserControl
    {
        public PatternSelect()
        {
            InitializeComponent();
    
        }

        public bool f_checkInput(DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider)
        {
            bool error = true;
            if (txtPattern.Text == string.Empty)
            {
                errorProvider.SetError(txtPattern, "Dữ liệu rỗng ?");
                error = false;
            }
            if (txtNumber.Text == string.Empty)
            {
                errorProvider.SetError(txtNumber, "Dữ liệu rỗng ?");
                error = false;
            }
            return error;
        }

        public string f_getValue()
        {         
            string temp = "";
            if (txtDate.Text == string.Empty)
                temp = txtPattern.Text.Trim() + ":" + txtNumber.Text.Trim();
            else
            {
                string date = txtDate.Text.Trim();
                string s="";
                int dem = 1;
                foreach (char c in date)
                {
                    if (c == 'D')
                    {
                        s += "D";
                        if (dem == 2)
                        {
                            s += ":";
                            dem = 1;
                            continue;
                        }                                             
                    }
                    else if (c == 'M')
                    {
                        s += "M";
                        if (dem == 2)
                        {
                            s += ":";
                            dem = 1;
                            continue;
                        }                       
                    }
                    else if (c == 'Y')
                    {
                        s += "Y";
                        if (dem == 2 && date.IndexOf("YYYY") == -1)
                        {
                            s += ":";
                            dem = 1;
                            continue;
                        }
                        else if (dem == 4)
                        {
                            s += ":";
                            dem = 1;
                            continue;
                        }                                              
                    }
                    dem++;
                }
                //có thể khoi cần cắt mà lấy luôn như                 
                temp = txtPattern.Text.Trim() + ":" + s /*+ ":"*/ + txtNumber.Text.Trim();
            }
            return temp;            
        }

        public void f_setValue(string _tenThamSo)
        {
            string temp= DatabaseFB.GetThamSo(_tenThamSo);
            if(temp==null)
                return ;
            string[] thamso = temp.Split(':');
            if (thamso.Length == 2)//PNK:####
            {
                txtPattern.Text = thamso[0];
                txtNumber.Text = thamso[thamso.Length-1];
            }
            else//PNK:DDMMYYYY:#####
            {
                int d1 = thamso[0].Length;
                int d2 = temp.Length - (thamso[0].Length + thamso[thamso.Length - 1].Length);
                int d3 = thamso[thamso.Length - 1].Length;
                string s = temp.Substring(d1,d2);

                txtPattern.Text = thamso[0];
                txtDate.Text = s.Replace(":","").Trim();
                txtNumber.Text = thamso[thamso.Length-1];
            }
        }

        //public void SetError(DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider, string errorName)
        //{
        //    errorProvider.SetError(txtPattern, errorName);
        //}
    }
}
