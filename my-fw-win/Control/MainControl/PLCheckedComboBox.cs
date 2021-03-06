using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
using DevExpress.XtraReports.Native;
using System.Data.Common;
using System.Collections;

namespace ProtocolVN.Framework.Win
{
    public partial class PLCheckedComboBox : DevExpress.XtraEditors.XtraUserControl
    {
        // Khai báo các biến thành viên

        private ArrayList listDisplay;
        private ArrayList listID;
        // Các thuộc tính
        private DataSet dataSource;

        public DataSet DataSource
        {
            get { return dataSource; }
            set { dataSource = value; }
        }
        private String displayField;

        public String DisplayField
        {
            get { return displayField; }
            set { displayField = value; }
        }
        private String valuesField;

        public String ValueField
        {
            get { return valuesField; }
            set { valuesField = value; }
        }

        public PLCheckedComboBox()
        {
            InitializeComponent();
        }

        #region Các hàm xử lý
        //Hàm Khỡi tạo Combobox
        public bool _Init(string tableName,string valueField,string displayField)
        {
            this.listID = new ArrayList();
            this.listDisplay = new ArrayList();
            try
            {
                DatabaseFB db = DABase.getDatabase();
                this.dataSource=DABase.getDatabase().LoadDataSet("select "+valueField+","+displayField+" from "+tableName+"");

                if (this.dataSource != null)
                {
                    foreach (DataRow dr in this.dataSource.Tables[0].Rows)
                    {
                        this.cCB_Edit.Properties.Items.Add(dr[displayField].ToString());
                        this.listDisplay.Add(dr[displayField].ToString());
                        this.listID.Add(dr[valueField].ToString());
                    }
                }
                
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public bool _Init(DataTable dtTable, string valueField, string displayField)
        {
            this.listID = new ArrayList();
            this.listDisplay = new ArrayList();
            try
            {
                if (dtTable != null)
                {
                    foreach (DataRow dr in dtTable.Rows)
                    {
                        this.cCB_Edit.Properties.Items.Add(dr[displayField].ToString());
                        this.listDisplay.Add(dr[displayField].ToString());
                        this.listID.Add(dr[valueField].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public bool _Init()//
        {
            return this._Init(this.dataSource.Tables[0], this.valuesField, this.displayField);
        }
        // Hàm Lấy danh sách các ID
        public string[] _GetCheckedID()
        {
            string[] str = this._GetText().Split(new char[] {','});
            string[] arrID = {"-1"};

            if (listID!=null && listID.Count > 0)
            {
                arrID = new string[str.Length];
                int i = 0;
                foreach (string t in str)
                {
                    int index = listDisplay.IndexOf(t.Trim());
                    if (index != -1)
                    {
                        arrID[i] = listID[index].ToString();
                        i++;
                    }
                }
            }
            return arrID;
        }
        // Hàm Lấy danh sách các ID
        public string[] _GetCheckedText()
        {
            return this._GetText().Split(new char[] { ',' });
        }
        //
        public string _GetText()
        {
            return this.cCB_Edit.Text;
        }
        #endregion     
    }
}
