using Moso.NetworkM.IBLL;
using Moso.NetworkM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moso.NetworkM.BLL
{
    public partial class ManagerInfoService : BaseService<ManagerInfo>, IManagerInfoService
    {
        public bool SetManagerRoles(int managerId, List<int> roleIdList)
        {
            var manager = this.CurrentDBSession.ManagerInfoDal.LoadEntities(m => m.Id == managerId).FirstOrDefault();
            if (manager != null)
            {
                manager.RoleInfo.Clear();
                foreach (int roleId in roleIdList)
                {
                    var roleinfo = this.CurrentDBSession.RoleInfoDal.LoadEntities(r => r.Id == roleId).FirstOrDefault();
                    manager.RoleInfo.Add(roleinfo);
                }
                return this.CurrentDBSession.SaveChanges();
            }
            return false;
        }

    }
}
