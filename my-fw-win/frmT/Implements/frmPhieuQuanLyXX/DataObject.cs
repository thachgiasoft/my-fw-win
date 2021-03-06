using System.Collections.Generic;
using System.Data;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
namespace ProtocolVN.Framework.Win
{
    #region Lớp hỗ trợ
    /// <summary>Biểu diễn 1 Item.
    /// </summary>
    public class _MenuItem
    {
        #region fields
        string fieldName;
        string[] captionNames;
        string[] imageNames;
        PermissionItem[] permissions;
        DelegationLib.CallFunction_MulIn_NoOut[] funcs;
        #endregion

        #region properties
        public string[] CaptionNames
        {
            get { return captionNames; }
            set
            {
                captionNames = value;
            }
        }

        public string[] ImageNames
        {
            get { return imageNames; }
            set { imageNames = value; }
        }

        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }

        public DelegationLib.CallFunction_MulIn_NoOut[] Funcs
        {
            get { return funcs; }
            set { funcs = value; }
        }

        public PermissionItem[] Permissions
        {
            get { return permissions; }
            set { permissions = value; }
        }
        #endregion

        #region constructors
        public _MenuItem() { }

        public _MenuItem(string[] _captions, string[] _imageNames, string _fieldName, DelegationLib.CallFunction_MulIn_NoOut[] _funcs, PermissionItem[] pers)
        {
            captionNames = _captions;
            imageNames = _imageNames;
            fieldName = _fieldName;
            funcs = _funcs;
            permissions = pers;
        }

        public _MenuItem(string[] _captions, string[] _imageNames, string _fieldName, DelegationLib.CallFunction_MulIn_NoOut[] _funcs):
            this(_captions, _imageNames, _fieldName, _funcs, null)
        { }

        public _MenuItem(string[] _captions, string[] _imageNames, DelegationLib.CallFunction_MulIn_NoOut[] _funcs, PermissionItem[] pers) :
            this(_captions, _imageNames, null, _funcs, pers)
        { }
        
        #endregion
    };    
    #endregion
}
