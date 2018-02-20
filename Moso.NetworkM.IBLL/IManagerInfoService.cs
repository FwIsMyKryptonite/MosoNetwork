using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moso.NetworkM.IBLL
{
    public partial interface IManagerInfoService
    {
        bool SetManagerRoles(int managerId, List<int> roleIdList);
    }
}
