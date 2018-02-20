using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moso.NetworkM.WebApp.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        //IBLL.IManagerInfoService managerInfoService { get; set; }
        IBLL.IManagerInfoService managerInfoService = new BLL.ManagerInfoService();
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 显示验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowValidateCode()
        {
            Common.ValidateCode validateCode = new Common.ValidateCode();
            string code = validateCode.CreateValidateCode(4);//产生验证码
            Session["validateCode"] = code;
            byte[] buffer = validateCode.CreateValidateGraphic(code);//将验证码画到画布上
            return File(buffer, "image/jpeg");
        }

        public ActionResult ManagerLogin()
        {
            string validateCode = Session["validateCode"] != null ? Session["validateCode"].ToString() : string.Empty;
            if (string.IsNullOrEmpty(validateCode))
            {
                return Content("no:验证码错误啊！");
            }
            Session["validateCode"] = null;
            string managerInputValidateCode = Request["vCode"];
            if (!managerInputValidateCode.Equals(validateCode, StringComparison.InvariantCultureIgnoreCase))
            {
                return Content("no:验证码错误啊！");
            }
            string managerName = Request["LoginName"];
            string managerPwd = Request["LoginPwd"];
            var managerInfo = managerInfoService.LoadEntities(m => m.MName == managerName && m.MPwd == managerPwd).FirstOrDefault();
            if (managerInfo != null)
            {
                string sessionId = Guid.NewGuid().ToString();
                string managerInfoStr = Common.SerializeHelper.SerializeToString(managerInfo);
                Common.MemcacheHelper.Set(sessionId, managerInfoStr, DateTime.Now.AddMinutes(20));
                //Response.SetCookie(new HttpCookie(sessionId));
                Response.Cookies["sessionId"].Value = sessionId;//以cookie的形式将sessionId返回给浏览器,没有过期时间,说明该cookie是存在于浏览器的内存中
                return Content("ok:卧槽！登录成功！");
            }
            return Content("no:登录失败！");
        }
    }
}
