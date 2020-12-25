using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using App.CoreLib;
using App.Core.Contract;

namespace App.Core.Web.ViewModels.Shared
{
    public class MenuViewModelFactory
    {
        static readonly string MenuFilename = "menus.json";
        public IUrlHelper Url { get; set; }
        public HttpContext Context { get; set; }
        public MenuViewModelFactory(IUrlHelper helper, HttpContext context)
        {
            Url = helper;
            Context = context;
        }
        public MenuViewModel Create()
        {
            var menuItems = GetMenus();
            var menu = new MenuViewModel();
            foreach (var menuItem in menuItems.OrderBy(mi => mi.Position))
            {
                var itemModel = new MenuItemViewModelFactory(Url, Context).Create(menuItem);
                if(itemModel != null &&(!string.IsNullOrEmpty(itemModel.Route) || itemModel.Submenus.Any()))
                    menu.MenuItems.Add(itemModel);
            } 
            
            return menu;
        }

        public List<MenuItem> GetMenus()
        {
            var modulesPath = Path.Combine(Globals.ContentRootPath, MenuFilename);
            using (var reader = new StreamReader(modulesPath))
            {
                string content = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<MenuItem>>(content);
            }
        }
    }
}