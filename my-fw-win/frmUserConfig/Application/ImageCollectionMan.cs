using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    public interface IEMBImageStore
    {
        Image GetImage1616(String ImageName);        
        Image GetImage4848(String ImageName);
        Image GetImage3232(String ImageName);
        Image GetImage2020(String ImageName);
    }

    /// <summary>
    /// Mục đích của lớp là chứa đựng các hình cho toàn bộ ứng dụng.
    /// Giúp cho việc sử dụng các hình sưu tập được nhanh
    /// </summary>
    public partial class ImageCollectionMan : DevExpress.XtraEditors.XtraForm
    {
        public static ImageCollectionMan Instance = new ImageCollectionMan();

        private ImageCollectionMan()
        {
            InitializeComponent();
        }

        public Image GetImage1616(int ImageIndex)
        {
            return this.images1616.Images[ImageIndex];
        }

        public Image GetImage1616(String ImageName)
        {
            return this.images1616.Images[ImageName];
        }

        public Image GetImage4848(String ImageName)
        {
            return this.images4848.Images[ImageName];
        }

        public Image GetImage4848(int ImageIndex)
        {
            return this.images4848.Images[ImageIndex];
        }

        public Image GetImage2020(int ImageIndex)
        {
            return this.images2020.Images[ImageIndex];
        }

        public Image GetImage2020(String ImageName)
        {
            return this.images2020.Images[ImageName];
        }

        public Image GetImage3232(int ImageIndex)
        {
            return this.images3232.Images[ImageIndex];
        }

        public Image GetImage3232(String ImageName)
        {
            return this.images3232.Images[ImageName];
        }
    }
}