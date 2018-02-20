using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moso.NetworkM.WebApp.Controllers
{

    public class EmailInfoController : Controller
    {
        //
        // GET: /EmailInfo/
        IBLL.IEmailInfoService EmailInfoService = new BLL.EmailInfoService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditeEmailInfo()
        {
            Model.EmailInfo emailInfo = new Model.EmailInfo();
            emailInfo.DelFlag = 0;
            emailInfo.EAcount = Request["EName"];
            emailInfo.EDisplay = Request["EDisplay"];
            emailInfo.EPwd = Request["EPwd"];
            emailInfo.EOut = short.Parse(Request["EOut"]);
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
            return Content("ok");
        }
    }
}
