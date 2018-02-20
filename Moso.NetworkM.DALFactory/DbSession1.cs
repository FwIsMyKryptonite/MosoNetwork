 
using Moso.NetworkM.DAL;
using Moso.NetworkM.IDAL;
using Moso.NetworkM.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moso.NetworkM.DALFactory
{
	public partial class DBSession : IDBSession
    {
	
		private IActionInfoDal _ActionInfoDal;
        public IActionInfoDal ActionInfoDal
        {
            get
            {
                if(_ActionInfoDal == null)
                {
                    _ActionInfoDal = AbstractFactory.CreateActionInfoDal();
                }
                return _ActionInfoDal;
            }
            set { _ActionInfoDal = value; }
        }
	
		private IADInfoDal _ADInfoDal;
        public IADInfoDal ADInfoDal
        {
            get
            {
                if(_ADInfoDal == null)
                {
                    _ADInfoDal = AbstractFactory.CreateADInfoDal();
                }
                return _ADInfoDal;
            }
            set { _ADInfoDal = value; }
        }
	
		private IEmailInfoDal _EmailInfoDal;
        public IEmailInfoDal EmailInfoDal
        {
            get
            {
                if(_EmailInfoDal == null)
                {
                    _EmailInfoDal = AbstractFactory.CreateEmailInfoDal();
                }
                return _EmailInfoDal;
            }
            set { _EmailInfoDal = value; }
        }
	
		private IMainInfoDal _MainInfoDal;
        public IMainInfoDal MainInfoDal
        {
            get
            {
                if(_MainInfoDal == null)
                {
                    _MainInfoDal = AbstractFactory.CreateMainInfoDal();
                }
                return _MainInfoDal;
            }
            set { _MainInfoDal = value; }
        }
	
		private IManagerInfoDal _ManagerInfoDal;
        public IManagerInfoDal ManagerInfoDal
        {
            get
            {
                if(_ManagerInfoDal == null)
                {
                    _ManagerInfoDal = AbstractFactory.CreateManagerInfoDal();
                }
                return _ManagerInfoDal;
            }
            set { _ManagerInfoDal = value; }
        }
	
		private IO_ManagerInfo_ActionInfoDal _O_ManagerInfo_ActionInfoDal;
        public IO_ManagerInfo_ActionInfoDal O_ManagerInfo_ActionInfoDal
        {
            get
            {
                if(_O_ManagerInfo_ActionInfoDal == null)
                {
                    _O_ManagerInfo_ActionInfoDal = AbstractFactory.CreateO_ManagerInfo_ActionInfoDal();
                }
                return _O_ManagerInfo_ActionInfoDal;
            }
            set { _O_ManagerInfo_ActionInfoDal = value; }
        }
	
		private IPermissionInfoDal _PermissionInfoDal;
        public IPermissionInfoDal PermissionInfoDal
        {
            get
            {
                if(_PermissionInfoDal == null)
                {
                    _PermissionInfoDal = AbstractFactory.CreatePermissionInfoDal();
                }
                return _PermissionInfoDal;
            }
            set { _PermissionInfoDal = value; }
        }
	
		private IRoleInfoDal _RoleInfoDal;
        public IRoleInfoDal RoleInfoDal
        {
            get
            {
                if(_RoleInfoDal == null)
                {
                    _RoleInfoDal = AbstractFactory.CreateRoleInfoDal();
                }
                return _RoleInfoDal;
            }
            set { _RoleInfoDal = value; }
        }
	
		private IStaffInfoDal _StaffInfoDal;
        public IStaffInfoDal StaffInfoDal
        {
            get
            {
                if(_StaffInfoDal == null)
                {
                    _StaffInfoDal = AbstractFactory.CreateStaffInfoDal();
                }
                return _StaffInfoDal;
            }
            set { _StaffInfoDal = value; }
        }
	}	
}