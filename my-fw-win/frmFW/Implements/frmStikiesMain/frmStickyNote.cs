using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using ProtocolVN.Framework.Core;
using ProtocolVN.Framework.Win;
using ProtocolVN.Framework.Win.frmFW.Implements.frmStikiesMain;

namespace ProtocolVN.Plugin.NoteBook
{
    public partial class frmStickyNote : XtraForm
    {
        private int Id;
        public delegate void StickyDeletedHandler(frmStickyNote f);
        public event StickyDeletedHandler Sticky_Deleted;

        public frmStickyNote(Nullable<int> id)
        {
            InitializeComponent();

            if (!id.HasValue)
                Create();
            else
                Id = id.Value;
        }

        private void frmStickyNote_Load(object sender, EventArgs e)
        {
            LoadNote();
            Interop.HideCaret(richTextBox1.Handle);
            Interop.HideCaret(txtTitle.Handle);
        }

        private void frmStickyNote_LocationChanged(object sender, EventArgs e)
        {
            //ignore if the user is dragging
            if (isMouseDown) return;

            int moveX = Location.X;
            int moveY = Location.Y;
            Random rnd=new Random();

            if (Location.X + 2 > Screen.PrimaryScreen.Bounds.Width)
            {
                //random X
                moveX = rnd.Next(0, Screen.PrimaryScreen.Bounds.Width);
            }else if (Location.X < 0) moveX = 0;

            if (Location.Y + 2 > Screen.PrimaryScreen.Bounds.Height)
            {
                //random Y
                moveY = rnd.Next(0, Screen.PrimaryScreen.Bounds.Height);
            }
            else if (Location.Y < 0) moveY = 0;

            if (Location.X != moveX || Location.Y != moveY)
            {
                Location = new Point(moveX, moveY);
                Save();
            }

        }

        private void frmStickyNote_BackColorChanged(object sender, EventArgs e)
        {
            txtTitle.BackColor = BackColor;
        }

        public string main_context_list_entry_name
        {
            get
            {
                string str;
                if (string.IsNullOrEmpty(txtTitle.Text))
                    str = richTextBox1.Text;
                else
                    str = txtTitle.Text;
                str = str.Trim();
                if (string.IsNullOrEmpty(str)) return "<empty> ...";
                if(str.Length>25)
                    return str.Substring(0, 25)+" ...";
                return str;
            }
        }

        public Image main_context_list_entry_image
        {
            get
            {
                return picFlag.Image;
            }
        }

        #region Moving 
        private bool isMouseDown = false;
        private Point mouseOffset;
        private void pnlTitle_MouseDown(object sender, MouseEventArgs e)
        {
            int xOffset;
            int yOffset;

            if (e.Button == MouseButtons.Left)
            {
                xOffset = -e.X - SystemInformation.FrameBorderSize.Width;
                yOffset = -e.Y - SystemInformation.FrameBorderSize.Height;
                mouseOffset = new Point(xOffset, yOffset);
                isMouseDown = true;
            }
        }

        private void pnlTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }
        }

        private void pnlTitle_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
                Save();

                Interop.HideCaret(richTextBox1.Handle);
                Interop.HideCaret(txtTitle.Handle);
            }
        }
        #endregion

        #region Resizing
        private bool isMouseDownResize = false;
        private Point mouseOffsetResize;
        private void pnlResize_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOffsetResize = new Point(e.X, e.Y);
                isMouseDownResize = true;
            }
        }

        private void pnlResize_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDownResize = false;
                Save();

                Interop.HideCaret(richTextBox1.Handle);
                Interop.HideCaret(txtTitle.Handle);
            }
        }

        private void pnlResize_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDownResize)
            {
                Size = new Size(Width + e.X - mouseOffsetResize.X, Height + e.Y - mouseOffsetResize.Y);
                mouseOffsetResize = new Point(e.X, e.Y);
            }

        }
        #endregion

        #region  Data

        private void LoadNote()
        {
            dsConfig.dtNotesRow dr = Config.LoadNode(Id);
            UpdateUIFromDR(dr);
        }

        private void Save()
        {
            dsConfig.dtNotesRow dr = new dsConfig.dtNotesDataTable().NewdtNotesRow();
            dr.IdMsg = Id;
            UpdateDRFromUI(dr);
            Config.SaveNode(dr);
        }

        private void Delete()
        {
            Config.Delete(Id);
            if (Sticky_Deleted != null)
                Sticky_Deleted(this);
        }

        private void Send()
        {

            //MessageBox.Show("Sorry! This functionality has not been implemented as yet!", "Featre not implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);
            HelpMsgBox.ShowNotificationMessage("Xin lỗi! Hiện tại chức năng chưa xây dựng!");
        }

        private void Create()
        {
            dsConfig.dtNotesRow dr = new dsConfig.dtNotesDataTable().NewdtNotesRow();
            //change location to center of screen
            int x = (int)Screen.PrimaryScreen.Bounds.Width / 2;
            int y = (int)Screen.PrimaryScreen.Bounds.Height / 2;
            Location = new Point(x, y);
            loadDefaultLook();
            UpdateDRFromUI(dr);
            Id = Config.AddNode(dr);
        }

        private void UpdateDRFromUI(dsConfig.dtNotesRow dr)
        {
            dr.Message = richTextBox1.Rtf;
            dr.Opacity = Opacity;
            dr.X = Location.X;
            dr.Y = Location.Y;
            dr.Width = Size.Width;
            dr.Height = Size.Height;
            dr.TitlebarColor = BackColor.ToArgb();
            dr.BgColor = richTextBox1.BackColor.ToArgb();
            dr.FontColor = richTextBox1.ForeColor.ToArgb();
            dr.Font = SerializeFont(richTextBox1.Font);
            dr.FlagImage = picFlag.Tag.ToString();
            dr.TitleFontColor = txtTitle.ForeColor.ToArgb();
            dr.TitleFont = SerializeFont(txtTitle.Font);
            dr.Title = txtTitle.Text;
            dr.UserId = FrameworkParams.currentUser.id;
            dr.id = DABase.getDatabase().GetID("G_FW_ID");
        }

        private void UpdateUIFromDR(dsConfig.dtNotesRow dr)
        {
            try
            {
                richTextBox1.Rtf = dr.Message;
                Opacity = dr.Opacity;
                Location = new Point(dr.X, dr.Y);
                Size = new Size(dr.Width, dr.Height);
                BackColor = Color.FromArgb(dr.TitlebarColor);
                richTextBox1.BackColor = Color.FromArgb(dr.BgColor);
                richTextBox1.ForeColor = Color.FromArgb(dr.FontColor);
                richTextBox1.Font = DeserializeFont(dr.Font);
                txtTitle.ForeColor = Color.FromArgb(dr.TitleFontColor);
                txtTitle.Font = DeserializeFont(dr.TitleFont);
                switch (dr.FlagImage)
                {
                    case "BlueFlag":
                        picFlag.Image = global::ProtocolVN.Framework.Win.Properties.Resources.BlueFlag;
                        break;
                    case "RedFlag":
                        picFlag.Image = global::ProtocolVN.Framework.Win.Properties.Resources.RedFlag;
                        break;
                    case "GreenFlag":
                        picFlag.Image = global::ProtocolVN.Framework.Win.Properties.Resources.GreenFlag;
                        break;
                    case "GoalFlag":
                        picFlag.Image = global::ProtocolVN.Framework.Win.Properties.Resources.GoalFlag;
                        break;
                    case "Bulb":
                        picFlag.Image = global::ProtocolVN.Framework.Win.Properties.Resources.Bulb;
                        break;
                    default:
                        picFlag.Image = global::ProtocolVN.Framework.Win.Properties.Resources.BlueFlag;
                        break;
                }
                txtTitle.Text = dr.Title;
            }
            catch { }
        }

        public static string SerializeFont(Font f)
        {
            return f.FontFamily.Name + "|" + f.Size + "|" + f.Style.ToString();
        }

        public static Font DeserializeFont(string s)
        {
            string[] t = s.Split(new char[] { '|' });
            return new Font(t[0], (float)Convert.ToDouble(t[1]), (FontStyle)Enum.Parse(typeof(FontStyle), t[2]));
        }

        #endregion

        #region Actions
        private void picClose_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void picSave_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            Save();
            picSave.Visible = false;
            Interop.HideCaret(richTextBox1.Handle);
            Interop.HideCaret(txtTitle.Handle);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            picSave_Click(sender, e);
        }
        private void richTextBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                picSave.Visible = true;
                if (!timer1.Enabled)
                    timer1.Start();
                else
                {
                    timer1.Stop();
                    timer1.Start();
                }
            }
        }
        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            picSave.Visible = true;
            if (!timer1.Enabled)
                timer1.Start();
            else
            {
                timer1.Stop();
                timer1.Start();
            }
        }
        private void txtTitle_KeyDown(object sender, KeyEventArgs e)
        {
            picSave.Visible = true;
            if (!timer1.Enabled)
                timer1.Start();
            else
            {
                timer1.Stop();
                timer1.Start();
            }
        }
        private void txtTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                picSave.Visible = true;
                if (!timer1.Enabled)
                    timer1.Start();
                else
                {
                    timer1.Stop();
                    timer1.Start();
                }
            }
        }
        
        private void sendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Send();
        }
        
        private void defaulLooktToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            loadDefaultLook();
            Save();

            Interop.HideCaret(richTextBox1.Handle);
            Interop.HideCaret(txtTitle.Handle);
        }

        private void customizeLookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCustomizeLook f = new frmCustomizeLook();
            f.ChosenTitleBarColor = BackColor;
            f.ChosenBackgroundColor = richTextBox1.BackColor;
            f.ChosenFontColor = richTextBox1.ForeColor;
            f.ChosenFont = richTextBox1.Font;
            f.ChosenOpacity =Opacity;
            f.ChosenTitleFont = txtTitle.Font;
            f.ChosenTitleFontColor = txtTitle.ForeColor;
            f.ShowDialog();
            if (f.IsSaved)
            {
                BackColor = f.ChosenTitleBarColor;
                richTextBox1.BackColor = f.ChosenBackgroundColor;
                richTextBox1.ForeColor = f.ChosenFontColor;
                richTextBox1.Font = f.ChosenFont;
                txtTitle.ForeColor = f.ChosenTitleFontColor;
                txtTitle.Font = f.ChosenTitleFont;
                Opacity = f.ChosenOpacity;
                Save();
            }

            Interop.HideCaret(richTextBox1.Handle);
            Interop.HideCaret(txtTitle.Handle);
        }

        private void smallSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Size = new Size(120, 100);
            Save();

            Interop.HideCaret(richTextBox1.Handle);
            Interop.HideCaret(txtTitle.Handle);
        }

        private void mediumSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Size = new Size(180, 160);
            Save();

            Interop.HideCaret(richTextBox1.Handle);
            Interop.HideCaret(txtTitle.Handle);
        }

        private void largeSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Size = new Size(220, 200);
            Save();

            Interop.HideCaret(richTextBox1.Handle);
            Interop.HideCaret(txtTitle.Handle);
        }

        private void defaultSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mediumSizeToolStripMenuItem_Click(sender, e);
        }

        private void insertImageToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.CheckFileExists = true;
            fd.Multiselect = false;
            fd.ShowReadOnly = false;
            fd.Title = "Choose image...";
            fd.Filter = "Image Files(*.bmp;*.jpg;*.gif;*.jpeg;*.png)|*.bmp;*.jpg;*.gif;*.jpeg;*.png";
            if (fd.ShowDialog().Equals(DialogResult.OK))
            {
                Bitmap myBitmap = new Bitmap(fd.FileName);
                // Copy the bitmap to the clipboard.
                Clipboard.SetDataObject(myBitmap);
                // Get the format for the object type.
                DataFormats.Format myFormat = DataFormats.GetFormat(DataFormats.Bitmap);
                // After verifying that the data can be pasted, paste
                if (richTextBox1.CanPaste(myFormat))
                {
                    richTextBox1.Paste(myFormat);
                }
                else
                {
                    //MessageBox.Show("Sorry! Unable to insert the image.");
                    HelpMsgBox.ShowNotificationMessage("Xin lỗi! Chưa hỗ trợ chèn hình");
                }

                //clear the clipboard
                Clipboard.Clear();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
            Close();
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(e.LinkText);
            }
            catch
            {
                //? should I show a box?
            }
        }

        #endregion

        #region Flag Menus
        private void blueFlagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            picFlag.Image = global::ProtocolVN.Framework.Win.Properties.Resources.BlueFlag;
            picFlag.Tag = "BlueFlag";
        }
        private void redFlagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            picFlag.Image = global::ProtocolVN.Framework.Win.Properties.Resources.RedFlag;
            picFlag.Tag = "RedFlag";
        }
        private void greenFlagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            picFlag.Image = global::ProtocolVN.Framework.Win.Properties.Resources.GreenFlag;
            picFlag.Tag = "GreenFlag";
        }
        private void goalFlagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            picFlag.Image = global::ProtocolVN.Framework.Win.Properties.Resources.GoalFlag;
            picFlag.Tag = "GoalFlag";
        }
        private void bulbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            picFlag.Image = global::ProtocolVN.Framework.Win.Properties.Resources.Bulb;
            picFlag.Tag = "Bulb";
        }
        #endregion

        #region Styles Handling
        private void loadDefaultLook()
        {
            BackColor = Color.DarkKhaki;
            richTextBox1.BackColor = Color.Yellow;
            richTextBox1.ForeColor = Color.Black;
            richTextBox1.Font = new Font(FontFamily.GenericSansSerif, (float)10, FontStyle.Regular);
            txtTitle.Font = new Font(FontFamily.GenericSansSerif, (float)8.5, FontStyle.Bold);
            txtTitle.ForeColor = Color.Black;
            Opacity = 1;
        }

        private void skyBlueToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(2, 98, 195);
            richTextBox1.BackColor = Color.FromArgb(148,203,255);
            richTextBox1.ForeColor = Color.Navy;
            richTextBox1.Font = new Font(new FontFamily("Tahoma"), (float)10, FontStyle.Regular);
            txtTitle.Font = new Font(FontFamily.GenericSansSerif, (float)8.5, FontStyle.Bold);
            txtTitle.ForeColor = Color.FromArgb(221, 238, 255);
            Opacity = 1;
            Save();

            Interop.HideCaret(richTextBox1.Handle);
            Interop.HideCaret(txtTitle.Handle);
        }

        private void sunSetToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(255, 108, 0);
            richTextBox1.BackColor = Color.FromArgb(255, 207, 103);
            richTextBox1.ForeColor = Color.Maroon;
            richTextBox1.Font = new Font(new FontFamily("Tahoma"), (float)10, FontStyle.Regular);
            txtTitle.Font = new Font(FontFamily.GenericSansSerif, (float)8.5, FontStyle.Bold);
            txtTitle.ForeColor = Color.Black;
            Opacity = 1;
            Save();

            Interop.HideCaret(richTextBox1.Handle);
            Interop.HideCaret(txtTitle.Handle);
        }

        private void nightToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(42, 42, 42);
            richTextBox1.BackColor = Color.FromArgb(88, 88, 88);
            richTextBox1.ForeColor = Color.FromArgb(228, 228, 228);
            richTextBox1.Font = new Font(new FontFamily("Tahoma"), (float)10, FontStyle.Regular);
            txtTitle.Font = new Font(FontFamily.GenericSansSerif, (float)8.5, FontStyle.Bold);
            txtTitle.ForeColor = Color.White;
            Opacity = 1;
            Save();

            Interop.HideCaret(richTextBox1.Handle);
            Interop.HideCaret(txtTitle.Handle);
        }

        private void peachToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(158, 46, 80);
            richTextBox1.BackColor = Color.FromArgb(253, 189, 209);
            richTextBox1.ForeColor = Color.Maroon;
            richTextBox1.Font = new Font(new FontFamily("Tahoma"), (float)10, FontStyle.Regular);
            txtTitle.Font = new Font(FontFamily.GenericSansSerif, (float)8.5, FontStyle.Bold);
            txtTitle.ForeColor = Color.White;
            Opacity = 1;
            Save();

            Interop.HideCaret(richTextBox1.Handle);
            Interop.HideCaret(txtTitle.Handle);
        }

        private void elegantToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(65, 98, 139);
            richTextBox1.BackColor = Color.FromArgb(164, 179, 198);
            richTextBox1.ForeColor = Color.Black;
            richTextBox1.Font = new Font(new FontFamily("Arial"), (float)10, FontStyle.Regular);
            txtTitle.Font = new Font(FontFamily.GenericSansSerif, (float)8.5, FontStyle.Bold);
            txtTitle.ForeColor = Color.White;
            Opacity = 1;
            Save();

            Interop.HideCaret(richTextBox1.Handle);
            Interop.HideCaret(txtTitle.Handle);
        }

        private void forestToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(12, 89, 2);
            richTextBox1.BackColor = Color.FromArgb(142, 194, 135);
            richTextBox1.ForeColor = Color.FromArgb(48, 80, 44);
            richTextBox1.Font = new Font(new FontFamily("Tahoma"), (float)10, FontStyle.Regular);
            txtTitle.Font = new Font(FontFamily.GenericSansSerif, (float)8.5, FontStyle.Bold);
            txtTitle.ForeColor = Color.White;
            Opacity = 1;
            Save();

            Interop.HideCaret(richTextBox1.Handle);
            Interop.HideCaret(txtTitle.Handle);
        }

        private void notepadToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(13, 92, 133);
            richTextBox1.BackColor = Color.White;
            richTextBox1.ForeColor = Color.Black;
            richTextBox1.Font = new Font(FontFamily.GenericSansSerif, (float)10, FontStyle.Regular);
            txtTitle.Font = new Font(FontFamily.GenericSansSerif, (float)8.5, FontStyle.Bold);
            txtTitle.ForeColor = Color.White;
            Opacity = 1;
            Save();

            Interop.HideCaret(richTextBox1.Handle);
            Interop.HideCaret(txtTitle.Handle);
        }

        #endregion

       

    }
}