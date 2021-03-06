using System.Collections.Generic;
using ProtocolVN.Framework.Core;
using System.Data.Common;
using System;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Lớp cho phép tùy biến dự án
    /// </summary>
    public abstract class CustomProject
    {
        protected bool IsFinishInitRes = false;
        public bool IsFinish()
        {
            return IsFinishInitRes;
        }
        /// <summary>
        /// Cấu hình các tham số ban đầu cho hệ thống
        /// </summary>
        public abstract void initAppParam();

        public abstract System.Reflection.Assembly getAssemblyHelp();        

        public System.Reflection.Assembly getAssembly()
        {
            return getAssemblyHelp();
        }

        public void exitApplication(){
            try{
                //System.Windows.Forms.Application.Exit();
                System.Windows.Forms.Application.ExitThread();
            }
            catch (Exception ex)
            {
                if(FrameworkParams.wait!=null)
                    FrameworkParams.wait.Finish();
                System.Windows.Forms.Application.ExitThread();
            }
        }

        public virtual List<string> getPublicForm()
        {
            return null;
        }

        public virtual Dictionary<string, string> getFormFeatureMap()
        {
            return null;
        }

        /// <summary>
        /// Hàm này sẽ được gọi khi chưa đăng nhập vào hệ thống
        /// Sau khi hàm này hoàn tất thì vào Form Login
        /// </summary>
        [Obsolete("Hàm này không còn được sử dụng vì vấn đề giải quyết màn hình Splash ko cần nữa")]
        public virtual void InitResourceForApplication()
        {
            //IsFinishInitRes = true;
            DABase.checkDBConnection();                        
        }

        /// <summary>
        /// Hàm sẽ được gọi khi đăng nhập thành công.
        /// Thường có liên quan đến DB
        /// </summary>
        public virtual void InitResAfterLogin()
        {

        }

        public virtual void InitAferShowDesktop()
        {

        }

        public virtual void ReleaseResAfterLogout()
        {

        }
    }
}
