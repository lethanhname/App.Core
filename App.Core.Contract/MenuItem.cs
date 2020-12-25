using System.Collections.Generic;

namespace App.Core.Contract
{
    public class MenuItem
    {
        public string Id { get; set; }
        public string Route { get; set; }
        public string Title { get; set;}
        public string Icon { get; set; }
        public string Type { get; set; }
        public bool Active { get; set; }
        public int Position { get; set; }

        public IEnumerable<string> PermissionCodes = new List<string>();

        public IEnumerable<MenuItem> Submenus = new List<MenuItem>();
    }
}