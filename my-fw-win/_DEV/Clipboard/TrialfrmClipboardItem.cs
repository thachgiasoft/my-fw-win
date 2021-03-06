using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using ProtocolVN.Framework.Win;

using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public partial class frmClipboardItem : DevExpress.XtraEditors.XtraForm
    {
        public frmClipboardItem()
        {
            InitializeComponent();
        }
        
        string Entity = "";
        DataSet ds = null;
        public frmClipboardItem(string entity, GridView dgv)
        {
            InitializeComponent();
            ds = ((DataView)dgv.DataSource).Table.DataSet;
            gridControlDetails.DataSource = ClipboardMan.Instance.clipboard[entity].Data.Tables[0];
            Entity = entity;

        }

        private void btn_Chon_Click(object sender, EventArgs e)
        {
            int[] rowselected = gridViewDetails.GetSelectedRows();
            
            DataSet dsChon = ClipboardMan.Instance.GetDataSetContructor(Entity);
            DataRow row = dsChon.Tables[0].NewRow();
            for (int r = 0; r < rowselected.Length; r++)
            {
                for (int i = 0; i < dsChon.Tables[0].Columns.Count; i++)
                {
                    DataRow dr = gridViewDetails.GetDataRow(rowselected[r]);
                    row[i] = dr[i];
                }
                dsChon.Tables[0].Rows.Add(row);    
            }
            
            HelpDataSet.MergeDataSet(ClipboardMan.Instance.clipboard[Entity].Keys, ds, dsChon, false);
            this.Close();
        }

        private void btn_Dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmClipboardItem_Load(object sender, EventArgs e)
        {
            
        }
    }
}