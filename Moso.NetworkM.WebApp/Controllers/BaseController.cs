using Moso.NetworkM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moso.NetworkM.WebApp.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/
        public ManagerInfo LoginManager { get; set; }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            bool isSucess = false;
            string sessionId = Request.Cookies["sessionId"] != null ? Request.Cookies["sessionId"].Value : string.Empty;
            if (!string.IsNullOrEmpty(sessionId))
            {
                //根据该值查Memcache.
                object obj = Common.MemcacheHelper.Get(sessionId);
                if (obj != null)
                {
                    ManagerInfo managerInfo = Common.SerializeHelper.DeserializeToObject<ManagerInfo>(obj.ToString());
                    LoginManager = managerInfo;
                    isSucess = true;
                    Common.MemcacheHelper.Set(sessionId, obj, DateTime.Now.AddMinutes(20));//模拟出滑动过期时间.
                    //留个后门
                    if (LoginManager.MName == "lsj")
                    {
                        return;
                    }
                }
            }
            if (!isSucess)
            {
                filterContext.Result = Redirect("/Login/Index");//注意.
            }
        }


    }
}
