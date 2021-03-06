using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Xử lý các vấn đề liên quan đến FORM được chạy trong hệ thống FW.
    /// </summary>
    public class HelpProtocolForm : ProtocolForm
    {
        #region Hiện thị màn hình sau một thời gian tự động tắt
        /// <summary>
        /// Hiện thị màn hình sẽ tự đóng sau time (x10 ms)
        /// Phát sinh: Chưa dùng
        /// </summary>
        public static void ShowTimerDialog(XtraForm mainform, XtraForm form, int time)//10ms
        {
            TimerForm timerForm = new TimerForm(form);
            timerForm.setTimer(time);
            ProtocolForm.ShowDialog(mainform, form);
        }

        /// <summary>
        /// Hiện thị màn hình sẽ tự đóng sau time (x10 ms)
        /// Phát sinh: Chưa dùng
        /// </summary>
        public static void ShowModalTimerDialog(XtraForm mainform, XtraForm form, int time)//10ms
        {
            TimerForm timerForm = new TimerForm(form);
            timerForm.setTimer(time);
            ProtocolForm.ShowModalDialog(mainform, form);
        }
        #endregion

        #region Hiện thị màn hình cho phép người dùng tùy biến ẩn / hiện
        /// <summary>Hiện màn hình cho phép người dùng chọn có / không cho lần hiện sau
        /// Phát sinh: màn hình sử dụng lần đầu, màn hình thoát FW.
        /// </summary>        
        public static void ShowUserDialog(XtraForm mainform, XtraForm form)
        {
            if (mainform == null) mainform = FrameworkParams.MainForm;

            UserForm perForm = new UserForm(form);
            if (perForm.IsShow(mainform, form))
            {
                form.TopLevel = true;
                //form.TopMost = true;
                ProtocolForm.ShowDialog(mainform, form);
            }
        }
        /// <summary>Hiện màn hình cho phép người dùng chọn có / không cho lần hiện sau
        /// Phát sinh: màn hình sử dụng lần đầu, màn hình thoát FW
        /// </summary>
        public static void ShowUserModalDialog(XtraForm mainform, XtraForm form)
        {
            if (mainform == null) mainform = FrameworkParams.MainForm;

            UserForm perForm = new UserForm(form);
            if (perForm.IsShow(mainform, form))
            {
                //form.TopMost = true;
                ProtocolForm.ShowModalDialog(mainform, form);
            }
        }
        #endregion
    }

    [Obsolete("Sử dụng lớp HelpProtocolForm để thay thế.")]
    public class ProtocolForm
    {
        //private static ProtocolFormHook HookForm = new ProtocolFormHook();

        List<ApplyAction> applyActions = new List<ApplyAction>();
        private DevExpress.XtraEditors.XtraForm form;
        public ProtocolForm() { }
        private ProtocolForm(XtraForm form)
        {
            this.form = form;
        }
        
        #region Các hàm chính trong ShowForm
        public static void ShowDialog(DevExpress.XtraEditors.XtraForm mainForm, DevExpress.XtraEditors.XtraForm form, bool isWaitForm, IFormat isFormat, IPermission isPermision,
            bool isMultiLang, bool isModal, bool ignoreCheckShowForm)
        {
            //if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
            //if (isWaitForm) FrameworkParams.wait = new WaitingMsg();

            if (ignoreCheckShowForm == false)
            {
                if (HelpPermission.CanShowForm(form) == false)
                {
                    ApplyPermissionAction.getPermissionFormFail().ShowDialog();
                    form.Dispose();
                    return;
                }
            }

            try
            {
                if (form.IsDisposed) return;
                //form.TopMost = true;

                HelpUserLog.logOpenForm(form);
                PLPlugin.HookShowAllPlugin(form);

                form.FormClosed += new FormClosedEventHandler(form_FormClosed);
                ProtocolForm.pl_wrapper(ref form, isFormat, isPermision);
                EventHandler showEvent = new EventHandler(wait);
                form.Shown += showEvent;
                //DEVEXPRESS
                if (FrameworkParams.UsingRightClickForm) HelpXtraForm.PopupRightClickForm(form);

                if (FrameworkParams.wait != null) FrameworkParams.wait.Finish(); 

                HelpXtraForm.SetModal(mainForm, form, isModal);
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
                PLMessageBox.ShowSystemErrorMessage(ex.ToString());
            }
        }
        
        public static void ShowDialog(DevExpress.XtraEditors.XtraForm mainForm,
            DevExpress.XtraEditors.XtraForm form, bool isWaitForm, bool isModal,
            bool ignoreCheckShowForm)
        {
            //if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
            //if (isWaitForm) FrameworkParams.wait = new WaitingMsg();
            //Kiểm tra quyền.
            if (ignoreCheckShowForm == false)
            {
                if (HelpPermission.CanShowForm(form) == false)
                {
                    ApplyPermissionAction.getPermissionFormFail().ShowDialog();
                    form.Dispose();
                    return;
                }
            }

            try
            {
                if (form.IsDisposed) return;

                HelpUserLog.logOpenForm(form);

                PLPlugin.HookShowAllPlugin(form);

                form.FormClosed += new FormClosedEventHandler(form_FormClosed);

                ProtocolForm.pl_wrapper(ref form);

                EventHandler showEvent = new EventHandler(wait);
                form.Shown += showEvent;
                
                //DEVEXPRESS
                if (FrameworkParams.UsingRightClickForm) HelpXtraForm.PopupRightClickForm(form);

                if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();

                if (form is frmPermissionFail)
                {
                    form.ShowDialog(mainForm);
                }
                else
                {
                    HelpXtraForm.SetModal(mainForm, form, isModal);
                }
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
                PLMessageBox.ShowSystemErrorMessage(ex.ToString());
            }
        }
        
        public static void ShowWindow(DevExpress.XtraEditors.XtraForm mainForm,
            DevExpress.XtraEditors.XtraForm form, bool isWait, IFormat isFormat,
            IPermission isPermision, bool ignoreCheckShowForm)
        {
            //if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
            //if (isWait) FrameworkParams.wait = new WaitingMsg();
            ////Kích hoạt menu đang mở nếu đã mở
            //foreach (Form f in mainForm.MdiChildren)
            //{
            //    if (f.Text.Equals(form.Text) && f.Controls.Count == form.Controls.Count)
            //    {
            //        f.Activate();
            //        return;
            //    }
            //}
            if (ignoreCheckShowForm == false)
            {
                if (HelpPermission.CanShowForm(form) == false)
                {
                    ApplyPermissionAction.getPermissionFormFail().ShowDialog();
                    form.Dispose();
                    return;
                }
            }

            try
            {
                if (form.IsDisposed) return;

                HelpUserLog.logOpenForm(form);
                PLPlugin.HookShowAllPlugin(form);

                form.FormClosed += new FormClosedEventHandler(form_FormClosed);
                //form.Disposed += new EventHandler(form_Disposed);

                form.MdiParent = mainForm;
                form.MinimizeBox = false;
                form.WindowState = FormWindowState.Maximized;
                form.ShowInTaskbar = false;
                form.Icon = FrameworkParams.ApplicationIcon;

                ProtocolForm.pl_wrapper(ref form, isFormat, isPermision);

                form.Shown += new EventHandler(wait);
                //DEVEXPRESS
                if (FrameworkParams.UsingRightClickForm) HelpXtraForm.PopupRightClickForm(form);
                
                if (form is frmPermissionFail)
                    form.ShowDialog(FrameworkParams.MainForm);
                else
                    form.Show();
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
                PLMessageBox.ShowSystemErrorMessage(ex.ToString());
            }
        }
        
        public static void ShowForm(FeatureInfo info)
        {
            try
            {
                bool isWaitForm = info.IsWaiting;
                string formName = info.FURL;

                string param = FeatureInfo.getParam(formName);
                formName = FeatureInfo.getFormClassName(formName);

                #region Mục chọn cho phép gọi một phương thức trong lớp
                if (!formName.Contains("MethodExec") && !formName.Contains("PLDanhMuc"))
                {
                    XtraForm myForm = (XtraForm)GenerateClass.initObject(formName);

                    //Đặt một số tham số cho form         
                    object obj = myForm.Tag;
                    TagPropertyMan.InsertOrUpdate(ref obj, "FORM_PARAM", param);
                    TagPropertyMan.InsertOrUpdate(ref obj, "MENU_ID", info.ID);
                    TagPropertyMan.InsertOrUpdate(ref obj, "FORM_NAME", formName);

                    myForm.Tag = obj;

                    //Xử lý hiển thị file giúp đở
                    if (!info.HelpPage.Equals(""))
                    {
                        PLHelp.help(myForm, info.HelpPage);
                        myForm.HelpButton = true;
                    }
                    if (info.IsModal == false)
                    {
                        myForm.Show();
                    }
                    else
                    {
                        //Hiển thị dialog hay window
                        if (info.IsMDI)
                            ProtocolForm.ShowWindow(FrameworkParams.MainForm, myForm, isWaitForm);
                        else
                        {
                            if (info.IsModal)
                                ProtocolForm.ShowModalDialog(FrameworkParams.MainForm, myForm, isWaitForm);
                            else
                                ProtocolForm.ShowDialog(FrameworkParams.MainForm, myForm, isWaitForm);
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
            catch (Exception ex)
            {
                PLException e = new PLException(ex);
                e.className = info.FURL;
                PLException.AddException(e);

                //PLException.AddException(ex);
                //PLException.AddException(new Exception("+ Khởi tạo form " + getForm(Id) + " không thành công.+"));
                PLMessageBox.ShowSystemErrorMessage("Chức năng này chưa hỗ trợ. Vui lòng liên hệ Công ty P R O T O C O L.");
            }
            finally
            {
                if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
            }
        }

        /// <summary>
        /// Hỗ trợ hiện thị 1 form với nhiều tiêu chí tùy biến
        /// </summary>
        public static void ShowForm(XtraForm mainForm, XtraForm form,
            bool isWaitForm,
            bool isModal,
            bool ignoreCheckShowForm,
            bool IsInTaskbar,
            bool IsFixForm)
        {
            //Kiểm tra quyền.
            if (ignoreCheckShowForm == false)
            {
                if (HelpPermission.CanShowForm(form) == false)
                {
                    ApplyPermissionAction.getPermissionFormFail().ShowDialog();
                    form.Dispose();
                    return;
                }
            }

            try
            {
                if (form.IsDisposed) return;

                HelpUserLog.logOpenForm(form);

                PLPlugin.HookShowAllPlugin(form);

                form.FormClosed += new FormClosedEventHandler(form_FormClosed);

                ProtocolForm.pl_wrapper(ref form);

                EventHandler showEvent = new EventHandler(wait);
                form.Shown += showEvent;

                //DEVEXPRESS
                if (FrameworkParams.UsingRightClickForm)
                    HelpXtraForm.PopupRightClickForm(form);

                if (FrameworkParams.wait != null)
                    FrameworkParams.wait.Finish();

                HelpXtraForm.SetModal(mainForm, form, isModal, IsInTaskbar, IsFixForm);
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
                PLMessageBox.ShowSystemErrorMessage(ex.ToString());
            }
        }

        static void form_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is XtraForm) HelpUserLog.logCloseForm((XtraForm)sender);
            if (sender is XtraForm) PLPlugin.HookHideAllPlugin((XtraForm)sender);
        }

        private static void wait(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.XtraForm form = (DevExpress.XtraEditors.XtraForm)sender;
            
            if (FrameworkParams.isLog != null) FrameworkParams.isLog.Log(form);

            //form.Shown -= showEvent;
            if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
        }
        #endregion

        #region ShowDialog
        
        public static void ShowDialog(DevExpress.XtraEditors.XtraForm mainForm, DevExpress.XtraEditors.XtraForm form, bool isWaitForm, IFormat isFormat, IPermission isPermision,
            bool isMultiLang, bool isModal)
        {
            ShowDialog( mainForm, form, isWaitForm, isFormat, isPermision,
                        isMultiLang, isModal, false);
        }

        public static void ShowDialog(DevExpress.XtraEditors.XtraForm mainForm, DevExpress.XtraEditors.XtraForm form, bool isWaitForm, IFormat isFormat, IPermission isPermision, bool isMultiLang)
        {
            ShowDialog(mainForm, form, isWaitForm, isFormat, isPermision, isMultiLang, false);
        }

        public static void ShowDialog(DevExpress.XtraEditors.XtraForm mainForm, DevExpress.XtraEditors.XtraForm form, bool isWaitForm, bool isModal)
        {
            ShowDialog(mainForm, form, isWaitForm, isModal, false);
        }

        public static void ShowDialog(DevExpress.XtraEditors.XtraForm mainForm, DevExpress.XtraEditors.XtraForm form, bool isWaitForm)
        {
            ShowDialog(mainForm, form, isWaitForm, false);
        }

        public static void ShowDialog(DevExpress.XtraEditors.XtraForm mainForm, DevExpress.XtraEditors.XtraForm form)
        {
            ProtocolForm.ShowDialog(mainForm, form, true, false);
        }

        public static void ShowModalDialog(DevExpress.XtraEditors.XtraForm mainForm, DevExpress.XtraEditors.XtraForm form)
        {
            ProtocolForm.ShowDialog(mainForm, form, true, true);
        }

        public static void ShowModalDialog(DevExpress.XtraEditors.XtraForm mainForm, DevExpress.XtraEditors.XtraForm form, 
            bool isWaitForm, bool ignoreCheckShowForm)
        {
            ProtocolForm.ShowDialog(mainForm, form, isWaitForm, true, ignoreCheckShowForm);
        }

        public static void ShowModalDialog(DevExpress.XtraEditors.XtraForm mainForm, DevExpress.XtraEditors.XtraForm form, bool isWaitForm)
        {
            ProtocolForm.ShowDialog(mainForm, form, isWaitForm, true);
        }

        #endregion

        #region ShowWindow
        public static void ShowWindow(DevExpress.XtraEditors.XtraForm mainForm, DevExpress.XtraEditors.XtraForm form, bool isWait, IFormat isFormat, IPermission isPermision)
        {
            ShowWindow(mainForm, form, isWait, isFormat, isPermision, false);
        }

        public static void ShowWindow(DevExpress.XtraEditors.XtraForm mainForm, DevExpress.XtraEditors.XtraForm form, bool isWait)
        {
            ProtocolForm.ShowWindow(mainForm, form, isWait, FrameworkParams.isFormat, FrameworkParams.isPermision);            
        }
        #endregion

        #region Xử lý mở ModalForm động
        public static XtraForm CreateInstance(String FormName, Int64 ID)
        {
            return HelpObject.CreateXtraFormInstance(FormName, ID);
        }
        public static XtraForm CreateInstance(String FormName, object ID)
        {
            return HelpObject.CreateXtraFormInstance(FormName, ID);
        }
        public static XtraForm CreateInstance(String FormName, List<Object> InitParams)
        {
            return HelpObject.CreateXtraFormInstance(FormName, InitParams);
        }
        /// <summary>
        /// Hiện Modal Form.
        ///     FormName phải là 1 Form có hàm dựng truyền vào là Int64
        /// Phát sinh: Hỗ trợ double click vào item mở form tương ứng. Sử dụng
        ///     nhiều trong các lưới tìm kiếm.
        /// </summary>
        public static XtraForm ShowModalForm(XtraForm MainForm, String FormName, Int64 ID)
        {
            if (HelpPermission.CanShowForm(FormName) == false)
            {
                XtraForm formFail = ApplyPermissionAction.getPermissionFormFail();
                formFail.ShowDialog();
                return formFail;
            }

            XtraForm form = HelpObject.CreateXtraFormInstance(FormName, ID);
            if (form != null)
            {
                if (MainForm != null)
                    ProtocolForm.ShowModalDialog(MainForm, form);
                else
                    ProtocolForm.ShowModalDialog(FrameworkParams.MainForm, form);
                return form;
            }
            return null;
        }
        /// <summary>
        /// Hiện Modal Form.
        ///     FormName phải là 1 Form có hàm dựng truyền vào là object
        /// Phát sinh: Hỗ trợ double click vào item mở form tương ứng. Sử dụng
        ///     nhiều trong các lưới tìm kiếm.
        /// </summary>
        public static XtraForm ShowModalForm(XtraForm MainForm, String FormName, object ID)
        {
            if (HelpPermission.CanShowForm(FormName) == false)
            {
                XtraForm formFail = ApplyPermissionAction.getPermissionFormFail();
                formFail.ShowDialog();
                return formFail;
            }

            XtraForm form = HelpObject.CreateXtraFormInstance(FormName, ID);
            if (form != null)
            {
                if (MainForm != null)
                    ProtocolForm.ShowModalDialog(MainForm, form, true, true);
                else
                    ProtocolForm.ShowModalDialog(FrameworkParams.MainForm, form, true, true);
                return form;
            }
            return null;
        }
        /// <summary>
        /// Hiện Modal Form.
        ///     FormName phải là 1 Form có hàm dựng truyền vào là params object[] ID
        /// Phát sinh: Hỗ trợ double click vào item mở form tương ứng. Sử dụng
        ///     nhiều trong các lưới tìm kiếm.
        /// </summary>
        public static XtraForm ShowModalForm(XtraForm MainForm, String FormName, params object[] ID)
        {
            if (HelpPermission.CanShowForm(FormName) == false)
            {
                XtraForm formFail = ApplyPermissionAction.getPermissionFormFail();
                formFail.ShowDialog();
                return formFail;
            }

            XtraForm form = HelpObject.CreateXtraFormInstance(FormName, ID);
            if (form != null)
            {
                if (MainForm != null)
                    ProtocolForm.ShowModalDialog(MainForm, form, true, true);
                else
                    ProtocolForm.ShowModalDialog(FrameworkParams.MainForm, form, true, true);
                return form;
            }
            return null;
        }
        #endregion

        #region Hàm cho phép tìm 1 màn hình hiện có đang mở hay ko.
        /// <summary>
        /// Active màn hình đang mở nếu có TYPE = formName.
        ///     NULL: Nếu màn hình đó ko đang mở
        ///     FORM: - Kích hoạt màn hình và trả về màn hình đó.
        /// Phát sinh: Khi xây dựng hệ thống cảnh báo của WH. 
        /// </summary>
        public static Form ShowActiveForm(String formName)
        {
            return ShowActiveForm(formName, null, null);
        }
        /// <summary>
        /// Active màn hình đang mở nếu có TYPE = formName.
        ///     NULL: Nếu màn hình đó ko đang mở
        ///     FORM: - Kích hoạt màn hình và trả về màn hình đó.
        /// Phát sinh: Khi xây dựng hệ thống cảnh báo của WH.
        /// </summary>
        public static Form ShowActiveForm(String formName, String menuId, object param)
        {
            XtraForm mainForm = FrameworkParams.MainForm;

            if (menuId != null)
            {
                //Kích hoạt menu đang mở nếu đã mở
                foreach (Form f in mainForm.MdiChildren)
                {
                    Object menu = TagPropertyMan.Get(f.Tag, "MENU_ID");
                    if (menu != null && menu.ToString().Equals(menuId))
                    {
                        f.Activate();
                        return f;
                    }
                }
                //Kích hoạt Modaless Form
                foreach (Form f in mainForm.OwnedForms)
                {
                    Object menu = TagPropertyMan.Get(f.Tag, "MENU_ID");
                    if (menu != null && menu.ToString().Equals(menuId))
                    {
                        f.Activate();
                        return f;
                    }
                }
            }

            #region Kích hoạt menu đang mở nếu đã mở
            foreach (Form f in mainForm.MdiChildren)
            {
                if (f is IParamForm)
                {
                    Object FName = TagPropertyMan.Get(f.Tag, "FORM_NAME");
                    if (FName == null) FName = f.GetType().FullName;
                    if (FName != null && FName.ToString().Equals(formName))
                    {
                        object obj = f.Tag;
                        TagPropertyMan.InsertOrUpdate(ref obj, "FORM_PARAM", param);
                        if (menuId != null) TagPropertyMan.InsertOrUpdate(ref obj, "MENU_ID", menuId);
                        f.Tag = obj;

                        f.Activate();
                        ((IParamForm)f).Activate();
                        return f;
                    }
                }
                else
                //Trường hợp Màn hình ko mở từ menu nên check MENU_ID không thấy 
                //phải check dựa vào tên form
                {
                    Object FName = TagPropertyMan.Get(f.Tag, "FORM_NAME");
                    if (FName == null) FName = f.GetType().FullName;
                    if (FName != null && FName.ToString().Equals(formName))
                    {
                        f.Activate();
                        return f;
                    }
                }
            }
            //Kích hoạt Modaless Form
            foreach (Form f in mainForm.OwnedForms)
            {
                if (f is IParamForm)
                {
                    Object FName = TagPropertyMan.Get(f.Tag, "FORM_NAME");
                    if (FName == null) FName = f.GetType().FullName;
                    if (FName != null && FName.ToString().Equals(formName))
                    {
                        object obj = f.Tag;
                        TagPropertyMan.InsertOrUpdate(ref obj, "FORM_PARAM", param);
                        if (menuId != null) TagPropertyMan.InsertOrUpdate(ref obj, "MENU_ID", menuId);
                        f.Tag = obj;

                        f.Activate();
                        ((IParamForm)f).Activate();
                        return f;
                    }
                }
                else
                //Trường hợp Màn hình ko mở từ menu nên check MENU_ID không thấy 
                //phải check dựa vào tên form
                {
                    Object FName = TagPropertyMan.Get(f.Tag, "FORM_NAME");
                    if (FName == null) FName = f.GetType().FullName;
                    if (FName != null && FName.ToString().Equals(formName))
                    {
                        f.Activate();
                        return f;
                    }
                }
            }
            #endregion

            return null;
        }
        #endregion




        #region PL-Wrapper - Hiện ko dùng cơ chế này. Dùng cơ chế DEFINE tập trung dạng PERMISSION.
        public static void pl_wrapper(ref XtraForm form, IFormat isFormat, IPermission isPermision)
        {
            ProtocolForm standard = new ProtocolForm(form);
            if (isPermision != null)
                standard.applyActions.Add(new ProtocolVN.Framework.Win.ApplyPermissionAction(form.GetType().ToString()));
            if (isFormat != null)
                standard.applyActions.Add(new ProtocolVN.Framework.Win.ApplyFormatAction());

            //PHUOCNC : Thất bại có thể rơi vào chức năng khác
            //          Nhưng hiện tại chắc chắn thất bại chỉ do phân quyền
            if (standard.wrapperForm() == false)
            {
                form.Close();
                form = ApplyPermissionAction.getPermissionFormFail();
            }
            else
            {

            }
        }
        public static void pl_wrapper(ref XtraForm form)
        {
            ProtocolForm.pl_wrapper(ref form, FrameworkParams.isFormat, FrameworkParams.isPermision);
        }
        private bool wrapperForm()
        {
            if (form is IProtocolForm)
            {
                return wrapperIProtocolFormHelp();
            }
            else
                return wrapperFormHelp(form);
        }
        private bool wrapperFormHelp(Control control)
        {
            foreach (ApplyAction action in applyActions)
            {
                if (action is ApplyPermissionAction)
                {
                    if (form is IPublicForm)
                    {
                        return true;
                    }
                    else
                    {
                        if (ApplyPermissionAction.getPublicForm().Contains(form.GetType().ToString()))
                            return true;
                    }
                }
                if (action.applyControl(control, form) == false) return false;
            }
            if (control.HasChildren)
            {
                foreach (Control Ctrl in control.Controls)
                {
                    if (wrapperFormHelp(Ctrl) == false)
                        return false;
                }
            }
            return true;
        }
        private bool wrapperIProtocolFormHelp()
        {
            bool flag = true;
            foreach (ApplyAction action in applyActions)
            {
                if (action is ApplyPermissionAction)
                {
                    IPermisionable pro = (IPermisionable)form;
                    if (form is IPublicForm)
                    {
                        flag = true;
                    }
                    else
                    {
                        if (ApplyPermissionAction.getPublicForm().Contains(form.GetType().ToString()))
                        {
                            flag = true;
                        }
                        else
                            flag = action.apply(pro.GetPermisionableControls(), pro.GetObjectItems(), form);
                    }
                }
                else if (action is ApplyFormatAction)
                {
                    IFormatable pro = (IFormatable)form;
                    flag = action.apply(pro.GetFormatControls(), null, form);
                }

                if (flag == false) return flag;
            }
            return true;
        }
        private void wrapperUserControl(DevExpress.XtraEditors.XtraUserControl userControl)
        {
            wrapperFormHelp(userControl);
        }
        public static void pl_ctrl_wrapper(DevExpress.XtraEditors.XtraForm form, DevExpress.XtraEditors.XtraUserControl userControl, bool isFormat)
        {
            ProtocolForm standard = new ProtocolForm(form);
            if (isFormat)
                standard.applyActions.Add(new ProtocolVN.Framework.Win.ApplyFormatAction());

            standard.wrapperUserControl(userControl);
        }
        #endregion


        #region Hàm ko dùng
        static void form_Disposed(object sender, EventArgs e)
        {
            if (sender is XtraForm) HelpUserLog.logCloseForm((XtraForm)sender);
            if (sender is XtraForm) PLPlugin.HookHideAllPlugin((XtraForm)sender);
        }
        #endregion
    }  
            
    public class FeatureInfo
    {
        public string ID;
        public string Caption;
        public string Parents;
        public bool Enable;
        public string FURL;
        public bool IsMDI;
        public bool IsSep;
        public string ImageName;
        public bool IsWaiting;
        public string HelpPage;
        public string ToolTip;
        public bool IsModal;

        public static string getParam(string FormParam)
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

        public static string getFormClassName(string FormParam)
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
    }

    public abstract class ApplyAction
    {
        public abstract bool applyControl(Control control, XtraForm form);
        public abstract bool applyElement(Object element, XtraForm form);

        public bool apply(List<Control> Ctrls, List<Object> Elements, XtraForm form)
        {
            if (Ctrls != null)
            {
                for (int i = 0; i < Ctrls.Count; i++)
                {
                    if (applyControl(Ctrls[i], form) == false)
                        return false;
                }
            }
            if (Elements != null)
            {
                for (int i = 0; i < Elements.Count; i++)
                {
                    if (applyElement(Elements[i], form) == false)
                        return false;
                }
            }
            return true;
        }
    }
}