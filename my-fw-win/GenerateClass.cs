using System;
using System.Collections.Generic;
using System.Reflection;
using ProtocolVN.Framework.Core;
using ProtocolVN.Plugin;
using ProtocolVN.DanhMuc;
namespace ProtocolVN.Framework.Win
{
    [Obsolete("Sử dụng HelpObject để thay thế")]
    public class GenerateClass
    {
        #region Init Object
        /// <summary>Hàm sẽ khởi tạo đối tượng dựa vào tên đối tượng
        /// </summary>        
        public static object initObject(string className)
        {
            System.Reflection.Assembly myAssembly1 = null;
            object obj = null;
            //Host Application
            if (obj == null)
            {
                try{
                    myAssembly1 = FrameworkParams.Custom.getAssembly();
                    obj = myAssembly1.CreateInstance(className);
                }
                catch (Exception ex){
                    PLException.AddException(ex);
                }
            }

            //From PL Products.
            if (obj == null)
            {
                foreach (Assembly ass in FrameworkParams.plAssemblies)
                {
                    try
                    {
                        obj = ass.CreateInstance(className);
                        if (obj == null) continue;
                        else break;
                    }
                    catch (Exception ex)
                    {
                        PLException.AddException(ex);
                    }
                }
            }

            //ProtocolVN.Framework.Win
            if (obj == null)
            {
                try{
                    myAssembly1 = FrameworkParams.getAssembly();
                    obj = myAssembly1.CreateInstance(className);
                }
                catch (Exception ex){
                    PLException.AddException(ex);
                }
            }

            //ProtocoVN.Framework.Dev
            if (obj == null)
            {
                //DEVEXPRESS
                try{
                    myAssembly1 = DevFrameworkParams.getAssembly();
                    obj = myAssembly1.CreateInstance(className);
                }
                catch (Exception ex){
                    PLException.AddException(ex);
                }
            }

            ////ProtocolVN.DanhMuc
            //if (obj == null)
            //{
            //    try
            //    {
            //        myAssembly1 = DanhMucAssembly.getAssembly();
            //        obj = myAssembly1.CreateInstance(className);
            //    }
            //    catch (Exception ex)
            //    {
            //        PLException.AddException(ex);
            //    }
            //}

            //From Plugin
            if (obj == null)
            {
                for (int i = 0; i < PLPlugin.plugins.Count; i++)
                {
                    try{
                        IPlugin plugin = PLPlugin.plugins[i];
                        myAssembly1 = plugin.getAssembly();
                        obj = myAssembly1.CreateInstance(className);
                        if (obj == null) continue;
                        else break;
                    }
                    catch (Exception ex){
                        PLException.AddException(ex);
                    }
                }
            }
            if (obj == null) {
                PLException.AddException(new Exception("Chưa nạp được lớp " + className + "()"));
            }

            return obj;
        }

        public static object initObject(string className, object param)
        {
            System.Reflection.Assembly myAssembly1 = null;
            object obj = null;
            //Host
            if (obj == null)
            {
                try
                {
                    myAssembly1 = FrameworkParams.Custom.getAssembly();
                    obj = Activator.CreateInstance(myAssembly1.GetType(className), param);
                }
                catch (Exception ex){
                    PLException.AddException(ex);
                }
            }

            //From PL Products.
            if (obj == null)
            {
                foreach (Assembly ass in FrameworkParams.plAssemblies)
                {
                    try
                    {
                        obj = Activator.CreateInstance(ass.GetType(className), param);
                        if (obj == null) continue;
                        else break;
                    }
                    catch (Exception ex)
                    {
                        PLException.AddException(ex);
                    }
                }
            }

            //Win
            if (obj == null)
            {   try{
                    myAssembly1 = FrameworkParams.getAssembly();
                    obj = Activator.CreateInstance(myAssembly1.GetType(className), param);
                }
                catch (Exception ex){
                    PLException.AddException(ex);
                }
            }
            //Dev
            if (obj == null)
            {
                //DEVEXPRESS
                try{
                    myAssembly1 = DevFrameworkParams.getAssembly();
                    obj = Activator.CreateInstance(myAssembly1.GetType(className), param);
                }
                catch (Exception ex){
                    PLException.AddException(ex);
                }
            }
            ////DanhMuc
            //if (obj == null)
            //{
            //    try
            //    {
            //        myAssembly1 = DanhMucAssembly.getAssembly();
            //        obj = Activator.CreateInstance(myAssembly1.GetType(className), param);
            //    }
            //    catch (Exception ex)
            //    {
            //        PLException.AddException(ex);
            //    }
            //}
            //From Plugin
            if (obj == null)
            {
                for (int i = 0; i < PLPlugin.plugins.Count; i++)
                {
                    try{
                        IPlugin plugin = PLPlugin.plugins[i];
                        myAssembly1 = plugin.getAssembly();
                        obj = Activator.CreateInstance(myAssembly1.GetType(className), param);
                        if (obj == null) continue;
                        else break;
                    }
                    catch (Exception ex){
                        PLException.AddException(ex);
                    }
                }
            }
            

            if (obj == null) {
                PLException.AddException(new Exception("Chưa nạp được lớp " + className + "(Object param)"));
            }
            return obj;
        }

        public static object initObject(string className, List<Object> param)
        {
            System.Reflection.Assembly myAssembly1 = null;
            object obj = null;
            //Host Application
            if (obj == null)
            {
                try{
                    myAssembly1 = FrameworkParams.Custom.getAssembly();
                    obj = Activator.CreateInstance(myAssembly1.GetType(className), param);
                }
                catch (Exception ex){
                    PLException.AddException(ex);
                }
            }

            //From PL Products.
            if (obj == null)
            {
                foreach (Assembly ass in FrameworkParams.plAssemblies)
                {
                    try
                    {
                        obj = Activator.CreateInstance(ass.GetType(className), param);
                        if (obj == null) continue;
                        else break;
                    }
                    catch (Exception ex)
                    {
                        PLException.AddException(ex);
                    }

                }
            }

            //ProtocolVN.Framework.Win
            if (obj == null)
            {
                try{
                    myAssembly1 = FrameworkParams.getAssembly();
                    obj = Activator.CreateInstance(myAssembly1.GetType(className), param);
                }
                catch (Exception ex){
                    PLException.AddException(ex);
                }
            }
            //ProtocolVN.Framework.Dev
            if (obj == null)
            {
                //DEVEXPRESS
                try{
                    myAssembly1 = DevFrameworkParams.getAssembly();
                    obj = Activator.CreateInstance(myAssembly1.GetType(className), param);
                }
                catch (Exception ex){
                    PLException.AddException(ex);
                }
            }
            ////ProtocolVN.DanhMuc
            //if (obj == null)
            //{
            //    try
            //    {
            //        myAssembly1 = DanhMucAssembly.getAssembly();
            //        obj = Activator.CreateInstance(myAssembly1.GetType(className), param);
            //    }
            //    catch (Exception ex)
            //    {
            //        PLException.AddException(ex);
            //    }
            //}
            //From Plugin
            if (obj == null)
            {
                for (int i = 0; i < PLPlugin.plugins.Count; i++)
                {
                    try{
                        IPlugin plugin = PLPlugin.plugins[i];
                        myAssembly1 = plugin.getAssembly();
                        obj = Activator.CreateInstance(myAssembly1.GetType(className), param);
                        if (obj == null) continue;
                        else break;
                    }
                    catch (Exception ex){
                        PLException.AddException(ex);
                    }
                }
            }
            

            if (obj == null) 
                PLException.AddException(new Exception("Chưa nạp được lớp " + className + "(List<Object> param)"));
            return obj;
        }

        public static object initObject(string className, Int64 param)
        {
            System.Reflection.Assembly myAssembly1 = null;
            object obj = null;
            //Host
            if (obj == null)
            {
                try
                {
                    myAssembly1 = FrameworkParams.Custom.getAssembly();
                    obj = Activator.CreateInstance(myAssembly1.GetType(className), param);
                }
                catch (Exception ex)
                {
                    PLException.AddException(ex);
                }
            }
            //Win
            if (obj == null)
            {
                try{
                    myAssembly1 = FrameworkParams.getAssembly();
                    obj = Activator.CreateInstance(myAssembly1.GetType(className), param);
                }
                catch (Exception ex)
                {
                    PLException.AddException(ex);
                }
            }
            //Dev
            if (obj == null)
            {
                //DEVEXPRESS
                try{
                    myAssembly1 = DevFrameworkParams.getAssembly();
                    obj = Activator.CreateInstance(myAssembly1.GetType(className), param);
                }
                catch (Exception ex)
                {
                    PLException.AddException(ex);
                }
            }
            ////DanhMuc
            //if (obj == null)
            //{
            //    try
            //    {
            //        myAssembly1 = DanhMucAssembly.getAssembly();
            //        obj = Activator.CreateInstance(myAssembly1.GetType(className), param);
            //    }
            //    catch (Exception ex)
            //    {
            //        PLException.AddException(ex);
            //    }
            //}
            //From Plugin
            if (obj == null)
            {
                for (int i = 0; i < PLPlugin.plugins.Count; i++)
                {
                    try{
                        IPlugin plugin = PLPlugin.plugins[i];
                        myAssembly1 = plugin.getAssembly();
                        obj = Activator.CreateInstance(myAssembly1.GetType(className), param);
                        if (obj == null) continue;
                        else break;
                    }
                    catch (Exception ex)
                    {
                        PLException.AddException(ex);
                    }
                }
            }
            //From PL Products.
            if (obj == null)
            {
                foreach (Assembly ass in FrameworkParams.plAssemblies)
                {
                    try{
                        obj = Activator.CreateInstance(ass.GetType(className), param);
                        if (obj == null) continue;
                        else break;
                    }
                    catch (Exception ex)
                    {
                        PLException.AddException(ex);
                    }
                }
            }
            
            if (obj == null) PLException.AddException(new Exception("Chưa nạp được lớp " + className + "(Int64 param)"));
            return obj;
        }

        public static object initObject(string className, params object[] param)
        {
            System.Reflection.Assembly myAssembly1 = null;
            object obj = null;
            //Host
            if (obj == null)
            {
                try{
                    myAssembly1 = FrameworkParams.Custom.getAssembly();
                    obj = Activator.CreateInstance(myAssembly1.GetType(className), param);  
                }
                catch (Exception ex){
                    PLException.AddException(ex);
                }
            }
            //From PL Products.
            if (obj == null)
            {
                foreach (Assembly ass in FrameworkParams.plAssemblies)
                {
                    try
                    {
                        obj = Activator.CreateInstance(ass.GetType(className), param);
                        if (obj == null) continue;
                        else break;
                    }
                    catch (Exception ex)
                    {
                        PLException.AddException(ex);
                    }
                }
            }
            //Win
            if (obj == null)
            {
                try{
                    myAssembly1 = FrameworkParams.getAssembly();
                    obj = Activator.CreateInstance(myAssembly1.GetType(className), param);
                }
                catch (Exception ex){
                    PLException.AddException(ex);
                }
            }
            //Dev
            if (obj == null)
            {
                //DEVEXPRESS
                try{
                    myAssembly1 = DevFrameworkParams.getAssembly();
                    obj = Activator.CreateInstance(myAssembly1.GetType(className), param);
                }
                catch (Exception ex){
                    PLException.AddException(ex);
                }
            }
            ////DanhMuc
            //if (obj == null)
            //{
            //    try
            //    {
            //        myAssembly1 = DanhMucAssembly.getAssembly();
            //        obj = Activator.CreateInstance(myAssembly1.GetType(className), param);
            //    }
            //    catch (Exception ex)
            //    {
            //        PLException.AddException(ex);
            //    }
            //}
            //From Plugin
            if (obj == null)
            {
                for (int i = 0; i < PLPlugin.plugins.Count; i++)
                {
                    try{
                        IPlugin plugin = PLPlugin.plugins[i];
                        myAssembly1 = plugin.getAssembly();
                        obj = Activator.CreateInstance(myAssembly1.GetType(className), param);
                        if (obj == null) continue;
                        else break;
                    }
                    catch (Exception ex){
                        PLException.AddException(ex);
                    }
                }
            }
            
            if (obj == null) 
                PLException.AddException(new Exception("Chưa nạp được lớp " + className + "(Object param)"));
            return obj;
        }

        #endregion

        #region Init Method
        private static object initMethod(object dynObj, String methodName, bool isWaitingForm)
        {
            if (dynObj != null)
            {
                MethodInfo invokedMethod = dynObj.GetType().GetMethod(methodName);
                if (invokedMethod != null)
                {
                    object retObj = invokedMethod.Invoke(dynObj, new object[0]);
                    return retObj;
                }
                else { 
                    PLException.AddException( new Exception("Chưa nạp động được phương thức " + dynObj.GetType().FullName + "." + methodName + "()")); 
                }
            }
            return null;
        }

        public static object initMethod(String className, String methodName, bool isWaitingForm)
        {
            object dynObj = initObject(className);
            if (dynObj != null)
            {
                return initMethod(dynObj, methodName, isWaitingForm);
            }
            return null;
        }
        #endregion
    }
}
