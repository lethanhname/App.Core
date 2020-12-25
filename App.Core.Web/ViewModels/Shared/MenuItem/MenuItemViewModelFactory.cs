using System;
using System.Linq;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using App.Core.Contract;
using App.CoreLib;

namespace App.Core.Web.ViewModels.Shared
{
    public class MenuItemViewModelFactory
    {
        public IUrlHelper Url { get; set; }
        public HttpContext Context { get; set; }
        public MenuItemViewModelFactory(IUrlHelper helper, HttpContext context)
        {
            Url = helper;
            Context = context;
        }
        public MenuItemViewModel Create(MenuItem menuItem)
        {
            // if (!Context.User.Claims.Any(c => menuItem.PermissionCodes.Any(pc => string.Equals(c.Value, pc, StringComparison.OrdinalIgnoreCase))
            //     || !string.Equals(c.Value, Globals.DoEverything, StringComparison.OrdinalIgnoreCase)))
            //     return new MenuItemViewModel();
            if (string.IsNullOrEmpty(menuItem.Route) && !menuItem.Submenus.Any())
                return new MenuItemViewModel();

            var menu = new MenuItemViewModel()
            {
                Id = menuItem.Id,
                Title = menuItem.Title,
                Icon = menuItem.Icon,
                Route = menuItem.Route,
                Type = menuItem.Type,
                Active = menuItem.Active
            };
            if (menuItem.Submenus != null && menuItem.Submenus.FirstOrDefault() != null)
            {
                foreach (var m in menuItem.Submenus)
                {
                    var item = Create(m);
                    if(item != null)
                        menu.Submenus.Add(item);
                }
            }
            return menu;
        }
    }
}