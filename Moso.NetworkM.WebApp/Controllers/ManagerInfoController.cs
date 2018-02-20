using Moso.NetworkM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moso.NetworkM.WebApp.Controllers
{
    public class ManagerInfoController : Controller
    {
        //
        // GET: /ManagerInfo/
        IBLL.IManagerInfoService ManagerInfoService = new BLL.ManagerInfoService();
        IBLL.IRoleInfoService RoleInfoService = new BLL.RoleInfoService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetManagerInfoList()
        {
            int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int pageSize = Request["pagesize"] == null ? 5 : int.Parse(Request["pagesize"]);
            int totalCount;
            var managerInfos = ManagerInfoService.LoadPageEntities(pageIndex, pageSize, out totalCount, m => m.DelFlag == 0, m => m.Id, true).ToList();
            var temp = from m in managerInfos
                       select new { Id = m.Id, MName = m.MName, MPwd = m.MPwd, Remark = m.Remark, Sort = m.Sort, SubTime = m.SubTime };
            return Json(new { Rows = temp, Total = totalCount }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowSetManagerRoleInfo()
        {
            int id = int.Parse(Request["managerId"]);
            //根据id找出管理员信息
            ManagerInfo managerInfo = ManagerInfoService.LoadEntities(m => m.Id == id).FirstOrDefault();
            ViewBag.ManagerInfo = managerInfo;
            //查出系统所有的角色信息
            var allRoleInfo = RoleInfoService.LoadEntities(r => r.DelFlag == 0).ToList();
            ViewBag.AllRoleInfo = allRoleInfo;
            //查出该管理员所拥有的角色的id列表
            var owedRoleInfoIdList = (from r in managerInfo.RoleInfo
                                      select r.Id).ToList();
            ViewBag.OwedRoleInfoIdList = owedRoleInfoIdList;
            return View();
        }

        public ActionResult SetManagerRoleInfo()
        {
            List<int> owedRolesIdList = null;
            List<int> changedRolesIdList = new List<int>();
            int id = int.Parse(Request["managerId"]);
            var managerInfo = ManagerInfoService.LoadEntities(m => m.Id == id).FirstOrDefault();
            if (managerInfo != null)
            {
                owedRolesIdList = (from r in managerInfo.RoleInfo
                                   select r.Id).ToList();
            }
            string o = Common.SerializeHelper.SerializeToString(owedRolesIdList);
            string[] allKeys = Request.Form.AllKeys;//获取所有表单元素name属性的值。
            //List<string> temp = allKeys.Where(t => t.Contains("cba_")).ToList();
            foreach (string key in allKeys)
            {
                if (key.StartsWith("cba_"))
                {
                    string k = key.Replace("cba_", "");
                    changedRolesIdList.Add(int.Parse(k));
                }
            }
            string c = Common.SerializeHelper.SerializeToString(changedRolesIdList);
            if (o == c)
            {
                return Content("noChange");
            }
            else
            {
                if (ManagerInfoService.SetManagerRoles(id, changedRolesIdList))
                {
                    return Content("ok");
                }
                return Content("no");
            }
        }


    }
}
