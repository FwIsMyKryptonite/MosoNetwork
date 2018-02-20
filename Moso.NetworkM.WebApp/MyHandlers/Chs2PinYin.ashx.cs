using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Moso.NetworkM.Common;

namespace Moso.NetworkM.WebApp.MyHandlers
{
    /// <summary>
    /// Summary description for Chs2PinYin
    /// </summary>
    public class Chs2PinYin : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            string temp = context.Request["abc"];
            temp = Common.Chs2PinYinHelper.Get(temp.Substring(1, temp.Length - 1)) + "." + Common.Chs2PinYinHelper.Get(temp[0]) + "@mosopower.com";
            context.Response.Write(temp.ToLower());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}