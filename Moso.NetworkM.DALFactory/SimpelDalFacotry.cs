 

using Moso.NetworkM.IDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Moso.NetworkM.DALFactory
{
    public partial class AbstractFactory
    {
      
   
		
	    public static IActionInfoDal CreateActionInfoDal()
        {

		 string fullClassName = NameSpace + ".ActionInfoDal";
          return CreateInstance(fullClassName) as IActionInfoDal;

        }
		
	    public static IADInfoDal CreateADInfoDal()
        {

		 string fullClassName = NameSpace + ".ADInfoDal";
          return CreateInstance(fullClassName) as IADInfoDal;

        }
		
	    public static IEmailInfoDal CreateEmailInfoDal()
        {

		 string fullClassName = NameSpace + ".EmailInfoDal";
          return CreateInstance(fullClassName) as IEmailInfoDal;

        }
		
	    public static IMainInfoDal CreateMainInfoDal()
        {

		 string fullClassName = NameSpace + ".MainInfoDal";
          return CreateInstance(fullClassName) as IMainInfoDal;

        }
		
	    public static IManagerInfoDal CreateManagerInfoDal()
        {

		 string fullClassName = NameSpace + ".ManagerInfoDal";
          return CreateInstance(fullClassName) as IManagerInfoDal;

        }
		
	    public static IO_ManagerInfo_ActionInfoDal CreateO_ManagerInfo_ActionInfoDal()
        {

		 string fullClassName = NameSpace + ".O_ManagerInfo_ActionInfoDal";
          return CreateInstance(fullClassName) as IO_ManagerInfo_ActionInfoDal;

        }
		
	    public static IPermissionInfoDal CreatePermissionInfoDal()
        {

		 string fullClassName = NameSpace + ".PermissionInfoDal";
          return CreateInstance(fullClassName) as IPermissionInfoDal;

        }
		
	    public static IRoleInfoDal CreateRoleInfoDal()
        {

		 string fullClassName = NameSpace + ".RoleInfoDal";
          return CreateInstance(fullClassName) as IRoleInfoDal;

        }
		
	    public static IStaffInfoDal CreateStaffInfoDal()
        {

		 string fullClassName = NameSpace + ".StaffInfoDal";
          return CreateInstance(fullClassName) as IStaffInfoDal;

        }
	}
	
}