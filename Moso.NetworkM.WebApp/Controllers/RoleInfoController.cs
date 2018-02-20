using Moso.NetworkM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moso.NetworkM.WebApp.Controllers
{
    public class RoleInfoController : Controller
    {
        //
        // GET: /RoleInfo/
        IBLL.IRoleInfoService RoleInfoService = new BLL.RoleInfoService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetRoleInfoList()
        {
            int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            //int pageSize = Request["rows"] == null ? 5 : int.Parse(Request["rows"]);//easyui
            int pageSize = Request["pagesize"] == null ? 15 : int.Parse(Request["pagesize"]);//ligerui
            int totalCount = 0;
            var roleInfoList = RoleInfoService.LoadPageEntities(pageIndex, pageSize, out totalCount, r => r.DelFlag == 0, r => r.Id, true).ToList();
            var temp = from r in roleInfoList
                       select new { Id = r.Id, RoleName = r.RoleName, Sort = r.Sort, ModifiedOn = r.ModifiedOn, Remark = r.Remark };
            return Json(new { Rows = temp, Total = totalCount }, JsonRequestBehavior.AllowGet);
        }

        #region 展示角色信息
        public ActionResult showRoleInfoDetail()
        {
            int id = int.Parse(Request["id"]);
            var selectedRoleInfoModel = RoleInfoService.LoadEntities(r => r.Id == id && r.DelFlag == 0).FirstOrDefault();
            if (selectedRoleInfoModel != null)
            {
                var temp = new { Id = selectedRoleInfoModel.Id, RoleName = selectedRoleInfoModel.RoleName, ModifiedOn = selectedRoleInfoModel.ModifiedOn, Remark = selectedRoleInfoModel.Remark, Sort = selectedRoleInfoModel.Sort };
                return Json(temp, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 编辑角色
        public ActionResult EditeRoleInfo()
        {
            int id = int.Parse(Request["idE"]);
            var serverRoleInfoModel = RoleInfoService.LoadEntities(r => r.Id == id).FirstOrDefault();
            string serverRoleInfoModelStr = string.Empty;
            bool isSame = true;
            if (serverRoleInfoModel != null)
            {
                if (serverRoleInfoModel.RoleName != Request["roleNameE"])
                {
                    isSame = false;
                    goto Jump;
                }
                if (serverRoleInfoModel.Id != int.Parse(Request["idE"]))
                {
                    isSame = false;
                    goto Jump;
                }
                if (serverRoleInfoModel.Remark != Request["remarkE"])
                {
                    isSame = false;
                    goto Jump;
                }
                if (serverRoleInfoModel.Sort != Request["sortE"])
                {
                    isSame = false;
                    goto Jump;
                }
            }
            if (isSame == true)
            {
                return Content("nojbChange");
            }
        Jump:
            serverRoleInfoModel.ModifiedOn = DateTime.Now;
            serverRoleInfoModel.RoleName = Request["roleNameE"];
            serverRoleInfoModel.Remark = Request["remarkE"];
            serverRoleInfoModel.Sort = Request["sortE"];
            if (RoleInfoService.EditEntity(serverRoleInfoModel))
            {
                return Content("ojbk");
            }
            return Content("shit");
        }
        #endregion


    }
}
