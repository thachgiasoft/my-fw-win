using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using System.Diagnostics;
using System.Data.Common;
using System.Xml;
using System.IO;
using ProtocolVN.Framework.Core;
using System.Drawing;

namespace ProtocolVN.Framework.Win
{
    public enum SPECIAL_MENU_ITEM
    {
        LOGOUT = 101,
        CLOSEALL = 102,
        UPDATEPROGRAM = 103,        
        GUIDE = 104,
        EXIT = 105
    }

    public class MenuBuilder
    {
        //PHUOCNC
        public static string CreatePlugin()
        {
            return "";
        }
        //PHUOCNC
        public static string CreatePluginItem()
        {
            return "";
        }

        public static string CreateItem(    
            string ID, string Caption, string ParentID, bool IsEnable, 
            string FormClass, bool IsMDI, bool IsSep, string ImageName,
            bool IsWaiting, string HelpPage, string ToolTip, bool IsModal)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<Item>");
            str.Append("<ID>" + ID + "</ID>");
            str.Append("<Name>" + Caption + "</Name>");
            str.Append("<Parents>" + ParentID + "</Parents>");
            str.Append("<Enable>" + (IsEnable?"Y":"N") + "</Enable>");
            str.Append("<Form>" + FormClass + "</Form>");
            str.Append("<MDI>" + (IsMDI ? "Y" : "N") + "</MDI>");
            str.Append("<Sep>" + (IsSep ? "Y" : "N") + "</Sep>");
            str.Append("<ImageName>" + ImageName + "</ImageName>");
            str.Append("<Waiting>" + (IsWaiting ? "Y" : "N") + "</Waiting>");
            str.Append("<HelpPage>" + HelpPage + "</HelpPage>");
            str.Append("<ToolTip>" + ToolTip + "</ToolTip>");
            str.Append("<Modal>" + (IsModal ? "Y" : "N") + "</Modal>");
            str.Append("</Item>");
            return str.ToString();
        }

        public static void CreateItem(
            StringBuilder str, string ID, string Caption, string ParentID, bool IsEnable,
            string FormClass, bool IsMDI, bool IsSep, string ImageName,
            bool IsWaiting, string HelpPage, string ToolTip, bool IsModal)
        {
            str.Append (CreateItem(ID, Caption, ParentID, IsEnable, FormClass, IsMDI,
                IsSep, ImageName, IsWaiting, HelpPage, ToolTip, IsModal));
        }

        public static string CreateItem(
            string ID, string Caption, string ParentID, bool IsEnable,
            string FormClass, bool IsMDI, bool IsSep, string ImageName,
            bool IsWaiting, string HelpPage, string ToolTip)
        {
            return CreateItem(ID, Caption, ParentID, IsEnable, FormClass, IsMDI,
                IsSep, ImageName, IsWaiting, HelpPage, ToolTip, true);
        }

        public static string CreateRootItem(string ID, string Caption, string ToolTip)
        {
            return CreateItem(ID, Caption, "1", true, "", false, false, "", false, "", ToolTip, false);
        }

        public static string CreateRootItem(string ID, string Caption, string ImageName, string ToolTip)
        {
            return CreateItem(ID, Caption, "1", true, "", false, false, ImageName, false, "", ToolTip, false);
        }

        public static string CreateRootItem(string ID, string Caption, string FormClass, string ImageName, string ToolTip)
        {
            return CreateItem(ID, Caption, "1", true, FormClass, false, false, ImageName, false, "", ToolTip, false);
        }

        public static string CreateSpecialItem(SPECIAL_MENU_ITEM ID, string Caption, string ParentID, bool IsSep, string ImageName)
        {
            return CreateItem(ID.ToString(), Caption, ParentID, true, "", false, IsSep, ImageName, false, "", "", true); 
        }
    }
    public class MenuToolbar
    {
        protected XtraForm mainForm;
        protected DataSet ds;
        protected string username;

        public MenuToolbar(){ }

        public void InitMenuToolbar(XtraForm mainForm, string MenuXML, string username){
            this.mainForm = mainForm;
            this.username = username;
            //Lấy dữ liệu từ fileName
            this.ds = new DataSet("vn");
            try {
                if (MenuXML != "")
                {
                    System.IO.StringReader sr = new System.IO.StringReader(MenuXML);
                    this.ds.ReadXml(sr);
                }
                else
                {
                    DbCommand command = DABase.getDatabase().GetSQLStringCommand("SELECT * FROM MENU");
                    DABase.getDatabase().LoadDataSet(command, ds, "Menu");
                }
            }
            catch
            {
                FrameworkParams.ExitApplication(FrameworkParams.EXIT_STATUS.ERROR);
            }
            if (User.isAdmin(username) || FrameworkParams.isPermision == null)
            {
                loadAdminCategory();
            }
            else
            {
                if(FrameworkParams.isPermision != null)  
                    loadCategory();
                else
                    loadAdminCategory();
            }
        }

        //Không phần quyền.
        private void loadAdminCategory()
        {
            //NOOP            
        }

        /// <summary>Load Quyền cho các menu
        /// </summary>
        private void loadCategory()
        {
            List<string> publicForms = ApplyPermissionAction.getPublicForm();
            Dictionary<string, string> formFeatureMap = ApplyPermissionAction.getFormFeatureMap();
            List<Feature> features = ApplyPermissionAction.GetPermissionFeatures();

            foreach (DataRow dr in ds.Tables[0].Select(""))
            {
                string id = dr["Id"].ToString();
                if (getEnable(id) == false) continue;

                string formName = getForm(id);
                formName = this.getFormClassName(formName);
                
                if (!formName.Equals(""))
                    setEnable(id, false);
                else
                    continue;

                if (getEnable(id) == false && publicForms.Contains(formName))
                {
                    setEnable(id, true);
                    continue;
                }

                //Check Permission cho truy cập
                string featureName = "";
                if (formFeatureMap.ContainsKey(formName)){
                    featureName = formFeatureMap[formName];
                    if (featureName == null) {
                        PLMessageBoxDev.ShowMessage("Lập danh sách featureMap bị sai tại form " + formName);
                    }
                    string per = "";    
                    if (featureName.Contains(";"))
                    {
                        per = featureName.Substring(featureName.IndexOf(';') + 1).Trim();
                        featureName = featureName.Substring(0, featureName.IndexOf(';')).Trim();
                    }
                    
                    PermissionType PerType = PermissionType.VIEW;
                    if(per.Equals("VIEW")){
                        PerType = PermissionType.VIEW;
                    }
                    else if (per.Equals("ADD")){
                        PerType = PermissionType.ADD;
                    }
                    else if (per.Equals("DELETE")){
                        PerType = PermissionType.DELETE;
                    }
                    else if(per.Equals("EDIT")){
                        PerType = PermissionType.EDIT;
                    }

                    PermissionItem item = new PermissionItem(featureName, PerType);
                    if (ApplyPermissionAction.checkPermission(item)==true)
                    {
                        setEnable(id, true);
                    }
                }
            }
        }

        private void showForm(string Id)
        {
            try
            {
                bool isWaitForm = getWaiting(Id);
                string formName = getForm(Id);

                string param = this.getParam(formName);
                formName = this.getFormClassName(formName);

                #region Mục chọn cho phép gọi một phương thức trong lớp 
                if (!formName.Contains("MethodExec") && !formName.Contains("PLDanhMuc"))
                {
                    XtraForm myForm = (XtraForm)GenerateClass.initObject(formName);

                    //Đặt một số tham số cho form         
                    object obj = myForm.Tag;
                    TagPropertyMan.InsertOrUpdate(ref obj, "FORM_PARAM", param);
                    TagPropertyMan.InsertOrUpdate(ref obj, "MENU_ID", Id);
                    TagPropertyMan.InsertOrUpdate(ref obj, "FORM_NAME", formName);

                    myForm.Tag = obj;

                    //Xử lý hiển thị file giúp đở
                    if (!getHelpPage(Id).Equals(""))
                    {
                        PLHelp.help(myForm, getHelpPage(Id));
                        myForm.HelpButton = true;
                    }
                    if (IsStandalone(Id))
                    {
                        myForm.Show();
                    }
                    else
                    {
                        //Hiển thị dialog hay window
                        if (getMDI(Id))
                            ProtocolForm.ShowWindow(mainForm, myForm, isWaitForm);
                        else
                        {
                            if (getModal(Id))
                                ProtocolForm.ShowModalDialog(mainForm, myForm, isWaitForm);
                            else
                                ProtocolForm.ShowDialog(mainForm, myForm, isWaitForm);
                        }
                    }
                }
                #endregion
                //Xử lý danh mục popup
                else if (formName.Contains("PLDanhMuc"))
                {
                    XtraUserControl control = null;
                    control = (XtraUserControl)GenerateClass.initMethod(formName, param, false);
                    string Title = null;
                    try { Title = TagPropertyMan.Get(control.Tag, "Title").ToString(); }
                    catch { }
                    Size? size = null;
                    try { size = ((Size?)TagPropertyMan.Get(control.Tag, "Size")); }
                    catch { }
                    frmCategory.ShowCategory(control, Title, size);
                }
                else
                {
                    GenerateClass.initMethod(formName, param, isWaitForm);
                }
            }
            catch(Exception ex)
            {
                PLException e = new PLException(ex);
                e.className = getForm(Id);
                PLException.AddException(e);

                //PLException.AddException(ex);
                //PLException.AddException(new Exception("+ Khởi tạo form " + getForm(Id) + " không thành công.+"));
                HelpMsgBox.ShowSystemErrorMessage("Chức năng này chưa hỗ trợ. Xin vui lòng liên lạc với PROTOCOLVN.");
            }
            finally
            {
                if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
            }
        }

        protected void itemClick(object sender, ItemClickEventArgs e)
        {
            string menuId = ds.Tables[0].Select("ID='" + e.Item.Name + "'")[0][0].ToString();
            string formName = getForm(menuId);

            string param = this.getParam(formName);
            formName = this.getFormClassName(formName);

            #region Các Item đặc biệt trên menu
            if (menuId.Equals(SPECIAL_MENU_ITEM.EXIT.ToString())){// Thoát khỏi chương trình
                if (frmFWRunExit.confirmExit() == false) return;
                FrameworkParams.ExitApplication(FrameworkParams.EXIT_STATUS.NORMAL_THANKS);
            }
            else if (menuId.Equals(SPECIAL_MENU_ITEM.LOGOUT.ToString())) // Đăng xuất khỏi chương trình
            {
                if (frmFWRunExit.confirmExit() == false) return;
                HelpUserLog.log("Đăng xuất hệ thống");
                ((IMainForm)this.mainForm).logout_Click(sender, e);

            }
            else if (menuId.Equals(SPECIAL_MENU_ITEM.CLOSEALL.ToString()))// Đóng tất cả các cửa sổ đang mở
            {
                foreach (Form childForm in mainForm.MdiChildren)
                {
                    if (RightClickTitleBarHelper.isDesktopForm(childForm) == false)
                        childForm.Dispose();
                }
            }
            else if (menuId.Equals(SPECIAL_MENU_ITEM.GUIDE.ToString()))// Mở hướng dẫn sử dụng
            {
                PLHelp.openCHM();
            }
            else if (menuId.Equals(SPECIAL_MENU_ITEM.UPDATEPROGRAM.ToString()))
            {
                LiveUpdateHelper.updateVersionFromLocalServer();
            }
            #endregion

            else
            {
                //#region Kích hoạt cùng MenuID
                ////Kích hoạt menu đang mở nếu đã mở
                //foreach (Form f in mainForm.MdiChildren)
                //{
                //    Object menu = TagPropertyMan.Get(f.Tag, "MENU_ID");
                //    if (menu != null && menu.ToString().Equals(menuId))
                //    {
                //        f.Activate();
                //        return;
                //    }
                //}
                ////Kích hoạt Modaless Form
                //foreach (Form f in mainForm.OwnedForms)
                //{
                //    Object menu = TagPropertyMan.Get(f.Tag, "MENU_ID");
                //    if (menu != null && menu.ToString().Equals(menuId))
                //    {
                //        f.Activate();
                //        return;
                //    }
                //}
                //#endregion

                //#region Kích hoạt menu đang mở nếu đã mở
                //foreach (Form f in mainForm.MdiChildren)
                //{
                //    if(f is IParamForm)
                //    {
                //        Object FName = TagPropertyMan.Get(f.Tag, "FORM_NAME");
                //        if (FName != null && FName.ToString().Equals(formName))
                //        {
                //            object obj = f.Tag;
                //            TagPropertyMan.InsertOrUpdate(ref obj, "FORM_PARAM", param);
                //            TagPropertyMan.InsertOrUpdate(ref obj, "MENU_ID", menuId);
                //            f.Tag = obj;

                //            f.Activate();
                //            ((IParamForm)f).Activate();
                //            return;
                //        }    
                //    }
                //}
                ////Kích hoạt Modaless Form
                //foreach (Form f in mainForm.OwnedForms)
                //{
                //    if (f is IParamForm)
                //    {
                //        Object FName = TagPropertyMan.Get(f.Tag, "FORM_NAME");
                //        if (FName != null && FName.ToString().Equals(formName))
                //        {
                //            object obj = f.Tag;
                //            TagPropertyMan.InsertOrUpdate(ref obj, "FORM_PARAM", param);
                //            TagPropertyMan.InsertOrUpdate(ref obj, "MENU_ID", menuId);
                //            f.Tag = obj;

                //            f.Activate();
                //            ((IParamForm) f).Activate();
                //            return;
                //        }
                //    }
                //}
                //#endregion

                if (HelpProtocolForm.ShowActiveForm(formName, menuId, param) == null)
                    //Mở window mới 
                    showForm(menuId);
            }               
        }
        

        protected void wait(object sender, EventArgs e)
        {
            if (FrameworkParams.wait != null)
                FrameworkParams.wait.Finish();
        }

        #region Lấy thông tin từ file XML
        /*
        <MenuItem>
            0<Id>ID PHAN BIET TREN MENU CONFIG</Id>
            1<Name>Phiếu Mua hàng NCC</MenuName>
            2<Parents>MUA_HANG</Parents>
            3<Enable>Y</Enable>//Không dùng - chẳn có ý nghĩa nhưng không xóa được
            4<Form>domain.sales.form.frmPhieuMuaHangQL</Form>
            5<MDI>Y</MDI>
            6<Sep>N</Sep>
            7<ImageName>pl-pdathang.png</ImageName>
            8<Waiting>Y</Waiting>
            9<HelpPage>10.htm</HelpPage>
        </MenuItem>
        - Trong TOOLBAR nếu Parent = "TOOLBAR" thì có nghĩa là một chọn lựa trong toolbar không có popup.
        */
        /// <summary>
        /// Hàm lấy thông tin "<Name>"
        /// Tên được được hiển thị trên menu
        /// </summary>
        public string getName(string Id)
        {
            return ds.Tables[0].Select("ID='" + Id + "'")[0][1].ToString();
        }

        /// <summary>
        /// Hàm lấy thông tin "<Enable>"
        /// "Y" : Cho phép chọn
        /// "N" : Không cho phép chọn
        /// </summary>
        public bool getEnable(string Id)
        {
            if (ds.Tables[0].Select("ID='" + Id + "'")[0][3].ToString() == "Y")
                return true;
            return false;
        }

        public void setEnable(string Id, bool enable)
        {
            if (enable)
                ds.Tables[0].Select("ID='" + Id + "'")[0][3] = "Y";
            else
                ds.Tables[0].Select("ID='" + Id + "'")[0][3] = "N";
        }

        /// <summary>
        /// Hàm lấy thông tin "<Form>"        
        /// Tên form thực thi tương ứng với chọn lựa
        /// </summary>
        public string getForm(string Id)
        {
            return ds.Tables[0].Select("ID='" + Id + "'")[0][4].ToString();
        }

        public string getFormClassName(string FormParam)
        {
            if (FormParam != null)
            {
                if (FormParam.Contains("?") && FormParam.Contains("="))
                {
                    return FormParam.Substring(0, FormParam.IndexOf('?')).Trim();
                }
                return FormParam;
            }
            return "";
        }

        public string getParam(string FormParam)
        {
            if (FormParam != null)
            {
                if (FormParam.Contains("?") && FormParam.Contains("="))
                {
                    return FormParam.Substring(FormParam.IndexOf('=') + 1).Trim();
                }
                return "";
            }
            return "";
        }
        /// <summary>
        /// Hàm lấy thông tin "<MDI>"
        /// "Y" : Hiển thị Window
        /// "N" : Hiển thị Dialog
        /// </summary>
        public bool getMDI(string Id)
        {
            if (ds.Tables[0].Select("ID='" + Id + "'")[0][5].ToString() == "Y")
                return true;
            return false;
        }

        /// <summary>
        /// Hàm lấy thông tin "<Sep>"
        /// "Y" : Có phân cách
        /// "N" : Không phân cách
        /// </summary>
        public bool getSep(string Id)
        {
            if (ds.Tables[0].Select("ID='" + Id + "'")[0][6].ToString() == "Y")
                return true;
            return false;
        }

        /// <summary>
        /// Hàm lấy thông tin "<ImageName>"
        /// Tên hình sẽ hiển thị trong menu Item
        /// </summary>
        public string getImageName(string Id)
        {
            string imgName = ds.Tables[0].Select("ID='" + Id + "'")[0][7].ToString();
            return (imgName != "") ? imgName : "pl-product.png";
            
        }

        /// <summary>
        /// Hàm lấy thông tin "<Waiting>"
        /// "Y" : Hiển thị màn hình chờ
        /// "N" : Không hiển thị màn hình chờ
        /// </summary>
        public bool getWaiting(string Id)
        {
            if (ds.Tables[0].Select("ID='" + Id + "'")[0][8].ToString() == "Y")
                return true;
            return false;
        }

        /// <summary>
        /// Hàm lấy thông tin "<HelpPage>"
        /// Tên của trang giúp đở
        /// </summary>
        public string getHelpPage(string Id)
        {
            return ds.Tables[0].Select("ID='" + Id + "'")[0][9].ToString();
        }

        public string getToolTip(string id)
        {
            string toolTip = ds.Tables[0].Select("ID='" + id + "'")[0][10].ToString().Trim();
            if (toolTip == "")
            {
                return getName(id);
            }
            else
            {
                int pos = toolTip.IndexOf(';');
                if (pos > 0){
                    toolTip = toolTip.Substring(pos + 1);
                    if (toolTip == "")
                        return getName(id);
                    else
                        return toolTip;
                }
                else
                    return toolTip;
            }
        }

        public string getPageGroup(string id)
        {
            string toolTip = ds.Tables[0].Select("ID='" + id + "'")[0][10].ToString();
            if (toolTip == "")
            {
                return "";
            }
            else
            {
                int pos = toolTip.IndexOf(';');
                if (pos > 0)
                    return toolTip.Substring(0, pos);
                else
                    return "";
            }            
        }

        public bool IsStandalone(string Id)
        {
            try
            {
                if (ds.Tables[0].Select("ID='" + Id + "'")[0][11].ToString() == "S")
                    return true;
            }
            catch { }
            return false;
        }

        public bool getModal(string Id)
        {
            try
            {
                if (ds.Tables[0].Select("ID='" + Id + "'")[0][11].ToString() == "N")
                    return false;
            }
            catch { }
            return true;
        }

        public string getId(string name)
        {
            return ds.Tables[0].Select("Name='" + name + "'")[0][0].ToString();
        }

        public void CreateToolTip(BarItem item, string itemName)
        {
            DevExpress.Utils.SuperToolTip superToolTip = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipItem tooltipItem = new DevExpress.Utils.ToolTipItem();
            tooltipItem.Text = itemName;
            superToolTip.Items.Add(itemName);
            item.SuperTip = superToolTip;
        }
        #endregion

        public Image getImage48(String id)
        {
            int num = HelpNumber.ParseInt32(getImageName(id));
            Image image = null;
            if (num < 0)
            {
                if (FrameworkParams.imageStore != null)
                {
                    image = FrameworkParams.imageStore.GetImage4848(getImageName(id));
                    if (image == null) image = ImageCollectionMan.Instance.GetImage4848(getImageName(id));
                }
                else
                {
                    image = ResourceMan.getImage48(getImageName(id));
                }
            }
            else
            {
                image = ImageCollectionMan.Instance.GetImage4848(num);
            }
            if (image == null) image = FWImageDic.LOGO_IMAGE48;

            return image;
        }
        
        public Image getImage16(String id)
        {
            int num = HelpNumber.ParseInt32(getImageName(id));
            Image image = null;
            if (num < 0)
            {
                if (FrameworkParams.imageStore != null)
                {
                    image = FrameworkParams.imageStore.GetImage1616(getImageName(id));
                    if (image == null) image = ImageCollectionMan.Instance.GetImage1616(getImageName(id));
                }
                else
                {
                    image = ResourceMan.getImage16(getImageName(id));
                }
            }
            else
            {
                image = ImageCollectionMan.Instance.GetImage1616(num);
            }
            
            if (image == null) image = FWImageDic.LOGO_IMAGE16;

            return image;
        }
    }
}
