using Moso.NetworkM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moso.NetworkM.WebApp.Controllers
{
    public class HomeController : BaseController// Controller
    {
        //
        // GET: /Home/
        IBLL.IManagerInfoService ManagerInfoService = new BLL.ManagerInfoService();
        public ActionResult Index()
        {
            //ManagerInfo m = LoginManager;
            ViewBag.Name = LoginManager.MName;
            var loginManagerInfo = ManagerInfoService.LoadEntities(m => m.Id == LoginManager.Id).FirstOrDefault();
            if (loginManagerInfo != null)
            {
                var loginManagerRoles = (from r in loginManagerInfo.RoleInfo
                                         select r.RoleName).FirstOrDefault();
                ViewData["RoleName"] = loginManagerRoles;
            }
            return View();
        }

    }
}
