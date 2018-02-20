using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moso.NetworkM.Model.Search
{
    /// <summary>
    /// 构建搜索用户信息的条件
    /// </summary>
    public class MainInfoSearch : BaseSearch
    {
        public string QueryOption { get; set; }
        public string QueryText { get; set; }
    }
}
