using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moso.NetworkM.Common
{
    public class StringHelper
    {
        public static string ProcessEmail(string email)
        {
            if (!string.IsNullOrEmpty(email) && email.Contains("@moso"))
            {
                string[] temp = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
                email = temp[0];
            }
            return email;
        }

    }
}
