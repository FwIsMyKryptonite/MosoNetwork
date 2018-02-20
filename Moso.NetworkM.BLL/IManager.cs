 
using Moso.NetworkM.IBLL;
using Moso.NetworkM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Moso.NetworkM.BLL
{
	
	public partial class ActionInfoService :BaseService<ActionInfo>,IActionInfoService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.ActionInfoDal;
        }
    }   
	
	public partial class ADInfoService :BaseService<ADInfo>,IADInfoService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.ADInfoDal;
        }
    }   
	
	public partial class EmailInfoService :BaseService<EmailInfo>,IEmailInfoService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.EmailInfoDal;
        }
    }   
	
	public partial class MainInfoService :BaseService<MainInfo>,IMainInfoService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.MainInfoDal;
        }
    }   
	
	public partial class ManagerInfoService :BaseService<ManagerInfo>,IManagerInfoService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.ManagerInfoDal;
        }
    }   
	
	public partial class O_ManagerInfo_ActionInfoService :BaseService<O_ManagerInfo_ActionInfo>,IO_ManagerInfo_ActionInfoService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.O_ManagerInfo_ActionInfoDal;
        }
    }   
	
	public partial class PermissionInfoService :BaseService<PermissionInfo>,IPermissionInfoService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.PermissionInfoDal;
        }
    }   
	
	public partial class RoleInfoService :BaseService<RoleInfo>,IRoleInfoService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.RoleInfoDal;
        }
    }   
	
	public partial class StaffInfoService :BaseService<StaffInfo>,IStaffInfoService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.StaffInfoDal;
        }
    }   
	
}