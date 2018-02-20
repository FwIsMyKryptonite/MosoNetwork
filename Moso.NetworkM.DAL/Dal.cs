 

using Moso.NetworkM.IDAL;
using Moso.NetworkM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moso.NetworkM.DAL
{
		
	public partial class ActionInfoDal :BaseDal<ActionInfo>,IActionInfoDal
    {

    }
		
	public partial class ADInfoDal :BaseDal<ADInfo>,IADInfoDal
    {

    }
		
	public partial class EmailInfoDal :BaseDal<EmailInfo>,IEmailInfoDal
    {

    }
		
	public partial class MainInfoDal :BaseDal<MainInfo>,IMainInfoDal
    {

    }
		
	public partial class ManagerInfoDal :BaseDal<ManagerInfo>,IManagerInfoDal
    {

    }
		
	public partial class O_ManagerInfo_ActionInfoDal :BaseDal<O_ManagerInfo_ActionInfo>,IO_ManagerInfo_ActionInfoDal
    {

    }
		
	public partial class PermissionInfoDal :BaseDal<PermissionInfo>,IPermissionInfoDal
    {

    }
		
	public partial class RoleInfoDal :BaseDal<RoleInfo>,IRoleInfoDal
    {

    }
		
	public partial class StaffInfoDal :BaseDal<StaffInfo>,IStaffInfoDal
    {

    }
	
}