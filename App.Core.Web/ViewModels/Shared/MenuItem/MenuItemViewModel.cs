using System.Collections.Generic;

namespace App.Core.Web.ViewModels.Shared
{
    public class MenuItemViewModel
    {
        public string Id { get; set; }
        public string Route { get; set; }
        public string Title { get; set;}
        public string Icon { get; set; }
        public string Type { get; set; }
        public bool Active { get; set; }

        public List<MenuItemViewModel> Submenus = new List<MenuItemViewModel>();
    }
}