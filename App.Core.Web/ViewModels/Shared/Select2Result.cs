using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Web.ViewModels
{
    public class Select2Result
    {
        public string id { get; set; }
        public string text { get; set; }
    }

    public class ValueLabel
    {
        public string value { get; set; }
        public string label { get; set; }
    }
}