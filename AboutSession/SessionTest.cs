using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboutSession
{
    public class SessionTest
    {
        private static IDictionary<string, IDictionary<string, object>> data = new Dictionary<string, IDictionary<string, object>>();
        public static IDictionary<string, object> GetSessionByID(string sessionID)
        {
            //判断泛型数据容器中是否存在这样的sessionID，如果有则返回session，没有的话则创建session(泛型容器)，同事保存到泛型容器中。
            //sessionID所对应的session其实也是一个泛型容器。
            if (data.ContainsKey(sessionID))
            {
                return data[sessionID];
            }
            else
            {
                IDictionary<string, object> session = new Dictionary<string, object>();
                data[sessionID] = session;//以传过来的SessionID创建一个Dictionary。
                return session;
            }
        }
    }
}
