using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Contract.Security
{
    public static class SecurityHelpers
    {
        //public static string GetPermissionCode(string area, string controller)
        //{
        //    return GetPermissionCode(area, controller, "Index");
        //}
        public static string GetPermissionCode(string area, string controller, string action)
        {
            return string.Format(Constants.PermissionCodeFormat, area, controller, action);
        }
        public static string GetPermissionCode(string area, string controller)
        {
            return string.Format(Constants.PermissionControllerCodeFormat, area, controller);
        }
    }
}
