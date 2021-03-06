using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using ProtocolVN.Framework.Core;

namespace ProtocolVN.Plugin.NoteBook
{
    public partial class frmStickiesMain : XtraForm
    {
        public List<frmStickyNote> stickyNotes;
        bool NotesHidden = false;
        
        public frmStickiesMain()
        {
            stickyNotes = new List<frmStickyNote>();

            InitializeComponent();
            
            Visible = false;

            frmMain_Load(null, null);

            StickiesMethodExec.IsOpen = true;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //load stickynotes
            List<int> NoteIds = Config.GetNoteIDs();
            foreach (int id in NoteIds)
            {
                frmStickyNote f = new frmStickyNote(id);
                f.Sticky_Deleted += new frmStickyNote.StickyDeletedHandler(f_Sticky_Deleted);
                stickyNotes.Add(f);
                f.Show();
            }
            topmostToolStripMenuItem1.Checked = true;
            NotesHidden = false;
        }

        void f_Sticky_Deleted(frmStickyNote f)
        {
            stickyNotes.Remove(f);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //close all sticky notes
            foreach (frmStickyNote f in stickyNotes)
            {
                f.Close();
            }
            stickyNotes = new List<frmStickyNote>();
            e.Cancel = false;
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            notifyIcon1.Dispose();
        }

        #region Menu Actions
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //DialogResult res = MessageBox.Show("Are you sure you want to close?", "Stickies", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            DialogResult res = PLMessageBox.ShowConfirmMessage("Bạn có chắc muốn đóng sổ ghi chú?");            
            if (res == DialogResult.Yes)
            {
                foreach (frmStickyNote f in stickyNotes)
                {
                    f.Close();
                }
                stickyNotes = new List<frmStickyNote>();
                this.Close();
                this.Dispose();
                StickiesMethodExec.IsOpen = false;
                //Application.Exit();
            }
        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmStickyNote f = new frmStickyNote(null);
            f.Sticky_Deleted += new frmStickyNote.StickyDeletedHandler(f_Sticky_Deleted);
            stickyNotes.Add(f);
            f.Show();
            //bring it to front too
            Interop.SetForegroundWindow(f.Handle);
        }

        private void hideAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (frmStickyNote f in stickyNotes)
            {
                f.Hide();
            }
            NotesHidden = true;
        }

        private void showAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (frmStickyNote f in stickyNotes)
            {
                f.Show();
                Interop.SetForegroundWindow(f.Handle);
            }
            NotesHidden = false;
        }

        private void topmostToolStripMenuItem1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (frmStickyNote f in stickyNotes)
            {
                f.TopMost = topmostToolStripMenuItem1.Checked;
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            //populate list
            if (stickyNotesToolStripMenuItem1.HasDropDownItems)
            {
                //remove these items
                stickyNotesToolStripMenuItem1.DropDownItems.Clear();
            }
            else
            {
                //create a new dropdown and attach to it
                stickyNotesToolStripMenuItem1.DropDown = new ToolStripDropDown();
            }

            foreach (frmStickyNote f in stickyNotes)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(f.main_context_list_entry_name);
                item.Image = f.main_context_list_entry_image;
                item.Tag = f;
                item.Click += new EventHandler(item_Click);
                stickyNotesToolStripMenuItem1.DropDown.Items.Add(item);
            }
            if (stickyNotesToolStripMenuItem1.DropDownItems.Count == 0)
            {
                stickyNotesToolStripMenuItem1.Enabled = false;
            }
            else
            {
                stickyNotesToolStripMenuItem1.Enabled = true;
            }
        }

        void item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (item.Tag == null) return;
            frmStickyNote f = (frmStickyNote)item.Tag;
            f.Show();
            Interop.SetForegroundWindow(f.Handle);
        }
        #endregion

        #region Notify Icon      
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (NotesHidden)
                    showAllToolStripMenuItem_Click(sender, e);
                else
                    hideAllToolStripMenuItem_Click(sender, e);
            }
            else if (e.Button == MouseButtons.Middle)
            {
                newToolStripMenuItem1_Click(sender, e);
            }
        }
        #endregion
    }
}