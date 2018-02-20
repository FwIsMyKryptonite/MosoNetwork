using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moso.NetworkM.WebApp.Models
{
    public class EmailInfoDataModel
    {
        public string EAddress { get; set; }
        public string EDispaly { get; set; }
        public string EName { get; set; }
        public string Epwd { get; set; }
        public Nullable<short> EOut { get; set; }
        public short DelFlag { get; set; }
    }
}