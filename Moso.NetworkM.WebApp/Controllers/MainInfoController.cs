using Moso.NetworkM.Common;
using Moso.NetworkM.Model;
using Moso.NetworkM.Model.EnumType;
using Moso.NetworkM.Model.Search;
using Moso.NetworkM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moso.NetworkM.WebApp.Controllers
{
    public class MainInfoController : Controller
    {
        //
        // GET: /MainInfo/
        IBLL.IMainInfoService MainInfoService = new BLL.MainInfoService();
        IBLL.IPermissionInfoService PermissionInfoService = new BLL.PermissionInfoService();
        IBLL.IStaffInfoService staffInfoService = new BLL.StaffInfoService();
        IBLL.IEmailInfoService EmailInfoService = new BLL.EmailInfoService();
        public ActionResult Index()
        {
            return View();
        }

        #region 分页显示用户数据
        public ActionResult GetMainInfoList()
        {
            int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            //int pageSize = Request["rows"] == null ? 5 : int.Parse(Request["rows"]);//easyui
            int pageSize = Request["pagesize"] == null ? 15 : int.Parse(Request["pagesize"]);//ligerui
            //接收搜索条件
            string queryOption = Request["queryOption"];
            string queryText = Request["queryText"];
            int totalCount = 0;
            //构建搜索条件.
            MainInfoSearch mainInfoSearch = new MainInfoSearch()
            {
                QueryOption = queryOption,
                QueryText = queryText,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount
            };
            short delFlag = (short)DeleteEnumType.Normal;
            //根据构建好的搜索条件完成搜索
            var mainInfoList = MainInfoService.LoadSearchEntities(mainInfoSearch, delFlag).ToList();
            //var mainInfoList = MainInfoService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, c => c.DelFlag == 0, c => c.Id, true).ToList();
            var temp = from u in mainInfoList
                       select new
                       {
                           Id = u.Id,
                           StaffName = u.StaffName,
                           EmpId = u.EmpId,
                           EAddress = Common.StringHelper.ProcessEmail(u.EAddress),
                           AdAcount = u.AdAcount,
                           AdCode = u.AdCode,
                           CompName = u.CompName,
                           Ip = u.Ip,
                           MacAddress = u.MacAddress,
                           ScreenAsset = u.ScreenAsset,
                           PCAsset = u.PCAsset,
                       };

            //返回总的记录数，datagrid根据总的记录数计算出页数
            return Json(new { Rows = temp, Total = mainInfoSearch.TotalCount }, JsonRequestBehavior.AllowGet);//前端datagrid表格遍历的是rows里面的数据
        }
        #endregion

        #region 展示用户权限信息
        public ActionResult showMainInfoActionDetail()
        {
            int id = int.Parse(Request["mainId"]);
            MainInfo mainInfo = MainInfoService.LoadEntities(m => m.Id == id).FirstOrDefault();
            ViewBag.MainInfo = mainInfo;

            var allPermissionInfo = PermissionInfoService.LoadEntities(p => p.DelFlag == 0).ToList();
            ViewBag.AllPermissionInfo = allPermissionInfo;

            var owedPermissionInfoIdList = (from p in mainInfo.PermissionInfo
                                            select p.PId).ToList();
            ViewBag.OwedPermissionInfoIdList = owedPermissionInfoIdList;
            return View();
        }
        #endregion

        #region 设置用户权限信息
        public ActionResult SetMainActionInfo()
        {
            int id = int.Parse(Request["mainId"]);
            MainInfo mainInfo = MainInfoService.LoadEntities(m => m.Id == id).FirstOrDefault();
            List<int> ownedPermissonInfoIdList = (from r in mainInfo.PermissionInfo
                                                  select r.PId).ToList();
            string[] allKeys = Request.Form.AllKeys;//获取所有表单元素name属性值。(只有被选中的会提交上来，而且value值与name值去掉cba_之后的值相同)
            List<int> permissionIdList = new List<int>();
            foreach (string key in allKeys)
            {
                if (key.StartsWith("cba_"))
                {
                    string k = key.Replace("cba_", "");
                    permissionIdList.Add(Convert.ToInt32(k));
                }
            }
            if (permissionIdList.Count == ownedPermissonInfoIdList.Count)//检查已经拥有的权限的id集合和提交的权限id集合是否相等
            {
                int count = permissionIdList.Except(ownedPermissonInfoIdList).ToList().Count;
                if (count == 0)
                {
                    return Content("noChange");
                }
            }
            if (MainInfoService.SetMainPermissionInfo(id, permissionIdList))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 展示用户邮箱详情
        public ActionResult ShowMainInfoEmailDetail()
        {
            int id = int.Parse(Request["Id"]);
            MainInfo mainInfo = MainInfoService.LoadEntities(m => m.Id == id).FirstOrDefault();
            var emailInfo = mainInfo.EmailInfo;
            if (emailInfo != null)
            {
                var temp = new { EAddress = emailInfo.EAddress, EDisplay = emailInfo.EDisplay, EAcount = emailInfo.EAcount, EPwd = emailInfo.EPwd, EOut = emailInfo.EOut, DelFlag = emailInfo.DelFlag };
                return Json(temp, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 批量加入姓名首字母
        public ActionResult Test()
        {
            var mainInfolist = MainInfoService.LoadEntities(m => m.DelFlag == 0);
            List<int> idList = (from m in mainInfolist
                                select m.Id).ToList();
            if (MainInfoService.MainInfoModelUpdate(idList))
            {
                return Content("Ok");
            }
            return Content("no");
        }
        #endregion

        #region 展示添加用户表单
        public ActionResult ShowAddMainInfo()
        {
            return View();
        }
        #endregion

        #region 添加用户
        public ActionResult AddMainInfo()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            MainInfo mainInfo = new MainInfo();
            StaffInfo staffInfo = new StaffInfo();
            EmailInfo emailInfo = new EmailInfo();
            ADInfo adInfo = new ADInfo();

            mainInfo.EmpId = Request["EmpId"].Trim();
            mainInfo.StaffName = Request["StaffName"].Trim();
            mainInfo.EAddress = Request["EAddress"].Trim();
            mainInfo.AdAcount = Request["AdAcount"].Trim();
            mainInfo.AdCode = Request["AdCode"];
            mainInfo.CompName = Request["CompName"].Trim();
            mainInfo.Ip = Request["Ip"].Trim();
            mainInfo.MacAddress = Request["MacAddress"].Trim();
            if (!string.IsNullOrEmpty(Request["PCAsset"].Trim()))
            {
                mainInfo.PCAsset = Request["PCAsset"];
            }
            if (!string.IsNullOrEmpty(Request["ScreenAsset"].Trim()))
            {
                mainInfo.ScreenAsset = Request["ScreenAsset"];
            }
            mainInfo.SName = Chs2PinYinHelper.GetFirst(Request["StaffName"].Trim());
            //dic.Add("mainInfo", mainInfo);

            staffInfo.EmpId = Request["EmpId"].Trim();
            staffInfo.StaffName = Request["StaffName"].Trim();
            if (!string.IsNullOrEmpty(Request["Company"].Trim()))
            {
                staffInfo.Company = Request["Company"].Trim();
            }
            if (!string.IsNullOrEmpty(Request["Depart"].Trim()))
            {
                staffInfo.Depart = Request["Depart"].Trim();
            }
            //if (!string.IsNullOrEmpty(Request["Position"].Trim()))
            //{
            //    staffInfo.Position = Request["Position"].Trim();
            //}
            //if (!string.IsNullOrEmpty(Request["Floor"].Trim()))
            //{
            //    staffInfo.Floor = Request["Floor"].Trim();
            //}
            dic.Add("staffInfo", staffInfo);

            emailInfo.EAcount = Request["EAcount"].Trim();
            emailInfo.EPwd = Request["EPwd"];
            emailInfo.EAddress = Request["EAddress"].Trim();
            emailInfo.EDisplay = Request["EDisplay"].Trim();
            emailInfo.EOut = 1;
            dic.Add("emailInfo", emailInfo);

            adInfo.AdAcount = Request["AdAcount"].Trim();
            adInfo.AdCode = Request["AdCode"];
            adInfo.AdDisplay = Request["AdDisplay"].Trim();
            dic.Add("adInfo", adInfo);

            string msg = string.Empty;
            if (MainInfoService.AddModels(dic, mainInfo, out msg))
            {
                return Content("ok");
            }
            else
            {
                if (msg == "no")
                {
                    return Content("no");
                }
                return Content("noChange");
            }
        }
        #endregion


        #region 编辑邮箱
        public ActionResult EditeMainInfoEmailInfo()
        {
            int mainId = int.Parse(Request["mainId"]);
            Model.EmailInfo emailInfo = new Model.EmailInfo();
            emailInfo.DelFlag = 0;
            emailInfo.EAcount = Request["EName"];
            emailInfo.EDisplay = Request["EDisplay"];
            emailInfo.EPwd = Request["EPwd"];
            emailInfo.EOut = Request["EOut"] == null ? (short)0 : (short)1;//checkebox只有被选中的时候才会被当作参数提交至服务端
            emailInfo.EAddress = Request["EAddress"];
            string clientInputEmailInfo = Common.SerializeHelper.SerializeToString(emailInfo);
            string emailInfoInDatabaseStr = string.Empty;
            if (!string.IsNullOrEmpty(Request["EAddress"]))
            {
                var emailInfoInDatabase = EmailInfoService.LoadEntities(e => e.EAddress == emailInfo.EAddress).FirstOrDefault();
                if (emailInfoInDatabase != null)
                {
                    emailInfoInDatabaseStr = Common.SerializeHelper.SerializeToString(emailInfoInDatabase);
                }
            }
            if (clientInputEmailInfo.Equals(emailInfoInDatabaseStr, StringComparison.InvariantCultureIgnoreCase))
            {
                return Content("noChange");
            }
            if (MainInfoService.UpdateMainEmailInfos(emailInfo, mainId))
            {
                return Content("ok");
            }
            return Content("no");
        }
        #endregion
       

        public ActionResult TestEdit()
        {
            var e = staffInfoService.LoadEntities(s => s.EmpId == "0000001BAT").FirstOrDefault();
            StaffInfo ss = new StaffInfo();
            ss.EmpId = "0000001BAT";
            ss.StaffName = "保安亭一号岗位";
            //e.Floor = "666";
            if (staffInfoService.EditEntity(ss))
            {
                return Content("ok");
            }
            return Content("no");
        }

    }
}
