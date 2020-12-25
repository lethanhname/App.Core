using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Linq;
using App.Core.Contract.Security;
using App.CoreLib.EF;
using App.CoreLib.EF.Data.Entity;
using App.CoreLib.EF.Messages;

namespace App.Core.Web.AppControllers
{
    public abstract class AppControllerBase : Controller
    {
        public AppControllerBase()
        {

        }

        protected string AreaName => ControllerContext.ActionDescriptor.RouteValues["area"];
        protected string ControllerName => ControllerContext.ActionDescriptor.ControllerName;

        protected RedirectResult CreateRedirectToSelfResult()
        {
            return this.Redirect(this.Request.Path.Value + this.Request.QueryString.Value);
        }
        protected bool HasPermission(string area, string controller, string action)
        {
            return HasPermission(SecurityHelpers.GetPermissionCode(area, controller, action));
        }
        protected bool HasPermission(string permission)
        {
            return HttpContext.User.Claims.Any(c => string.Equals(c.Value, permission, StringComparison.OrdinalIgnoreCase));
        }
        protected string GetUrl(string areaName, string controller, string action)
        {
            return Url.Action(action, controller, new { area = areaName });
        }
        protected void AddErrors(EntityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }


    public class LinkedItem
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
    }


    public class FieldDefinition
    {
        public FieldDefinition()
        {
            searchable = true;
            autoWidth = true;
        }
        public string title { get; set; }
        public string data { get; set; }
        public bool searchable { get; set; }
        public bool autoWidth { get; set; }
    }
}