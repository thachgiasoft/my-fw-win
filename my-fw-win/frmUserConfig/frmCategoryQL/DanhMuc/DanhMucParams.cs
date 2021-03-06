using System;
using System.Collections.Generic;
using System.Text;
using ProtocolVN.Framework.Core;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Win;

namespace ProtocolVN.DanhMuc
{
    [Obsolete("...")]
    public enum DanhMucPermType
    {
        NO,
        READ_ADD_DELETE_EDIT,
        READ_EDIT,
        READ_EDIT_COMMIT
    }

    public class DanhMucParams
    {
        //public static System.Reflection.Assembly getAssemblyHelp()
        //{
        //    return System.Reflection.Assembly.GetCallingAssembly();
        //}

        //public static System.Reflection.Assembly getAssembly()
        //{
        //    return getAssemblyHelp();
        //}

        [Obsolete("...")]
        public static DanhMucPermType DMPermission = DanhMucPermType.NO;
        
        [Obsolete("...")]
        public static DelegationLib.DefinePermission GetPermission(XtraUserControl DM, String feature)
        {
            return DanhMucPermission.GetPermission(DM, feature);
        }

        public static DelegationLib.DefinePermission GetPermission(XtraUserControl control, String feature, String description)
        {
            return DanhMucPermission.GetPermission(control, feature, description);
        }
    }
}
