using Moso.NetworkM.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moso.NetworkM.WebApp.Controllers
{
    public class ActionInfoController : Controller
    {
        //
        // GET: /ActionInfo/
        IBLL.IActionInfoService ActionInfoService = new BLL.ActionInfoService();
        public ActionResult Index()
        {
            return View();
        }

        #region 获取管理员权限列表
        public ActionResult GetActionInfoList()
        {
            int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int pageSize = Request["pagesize"] == null ? 5 : int.Parse(Request["pagesize"]);
            int totalCount = 0;
            var actionInfoList = ActionInfoService.LoadPageEntities(pageIndex, pageSize, out totalCount, a => a.DelFlag == 0, a => a.Id, true).ToList();
            var temp = from a in actionInfoList
                       select new { Id = a.Id, ActionInfoName = a.ActionInfoName, ActionInfoType = a.ActionInfoType, Remark = a.Remark, Url = a.Url, HttpMethod = a.HttpMethod, Sort = a.Sort, SubTime = a.SubTime };
            return Json(new { Rows = temp, Total = totalCount }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 上传菜单权限图标
        public ActionResult GetFileUp()
        {
            HttpPostedFileBase file = Request.Files["fileUp"];
            if (file != null)
            {
                string fileName = Path.GetFileName(file.FileName);
                string fileExt = Path.GetExtension(fileName);
                if (fileExt == ".jpg")
                {
                    string dir = "/ImageIcon/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
                    Directory.CreateDirectory(Path.GetDirectoryName(Request.MapPath(dir)));
                    string newfileName = Guid.NewGuid().ToString();
                    string fullDir = dir + newfileName + fileExt;
                    file.SaveAs(Request.MapPath(fullDir));
                    //自己加上图片的缩略图
                    return Content("ok:" + fullDir);
                }
                else
                {
                    return Content("no:文件类型错误!!");
                }
            }
            else
            {
                return Content("no:请选择文件!!");
            }
        }
        #endregion

        #region 添加权限信息
        public ActionResult AddActionInfo()
        {
            ActionInfo actionInfo = null;
            if (string.IsNullOrEmpty(Request["ActionInfoName"]) || string.IsNullOrEmpty(Request["Url"]))
            {
                return Content("no*权限名称或者Url不能为空666");
            }
            actionInfo = new ActionInfo();
            actionInfo.SubTime = DateTime.Now;
            actionInfo.ModifiedOn = DateTime.Now;
            actionInfo.IconWidth = 0;
            actionInfo.IconHeight = 0;
            actionInfo.DelFlag = 0;
            actionInfo.ActionInfoName = Request["ActionInfoName"];
            actionInfo.Url = Request["Url"];
            actionInfo.Remark = Request["Remark"];
            actionInfo.Sort = Request["Sort"];
            actionInfo.HttpMethod = Request["HttpMethod"];
            actionInfo.ActionInfoType = short.Parse(Request["ActionTypeEnum"]);
            actionInfo.MenuIcon = Request["MenuIcon"];
            if (ActionInfoService.AddEntity(actionInfo) != null)
            {
                return Content("ok*恭喜，添加成功！");
            }
            else
            {
                return Content("no*服务器忙啊，请稍后再试！");
            }
        }
        #endregion

        public ActionResult DeleteActionInfo()
        {
            int id = int.Parse(Request["id"]);
            var actionInfo = ActionInfoService.LoadEntities(a => a.Id == id).FirstOrDefault();
            if (actionInfo != null)
            {
                actionInfo.DelFlag = 1;
                if (ActionInfoService.EditEntity(actionInfo))
                {
                    return Content("ok");
                }
            }
            return Content("no");
        }



    }
}
