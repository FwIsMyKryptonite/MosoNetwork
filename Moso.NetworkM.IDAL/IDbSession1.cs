 

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moso.NetworkM.IDAL
{
	public partial interface IDBSession
    {

	
		IActionInfoDal ActionInfoDal{get;set;}
	
		IADInfoDal ADInfoDal{get;set;}
	
		IEmailInfoDal EmailInfoDal{get;set;}
	
		IMainInfoDal MainInfoDal{get;set;}
	
		IManagerInfoDal ManagerInfoDal{get;set;}
	
		IO_ManagerInfo_ActionInfoDal O_ManagerInfo_ActionInfoDal{get;set;}
	
		IPermissionInfoDal PermissionInfoDal{get;set;}
	
		IRoleInfoDal RoleInfoDal{get;set;}
	
		IStaffInfoDal StaffInfoDal{get;set;}
	}	
}