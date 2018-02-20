using Moso.NetworkM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chs2PinYinTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string chs = "我好爱你呀Kryptonite";
            string pinYin = Chs2PinYinHelper.GetFirst(chs);
            Console.WriteLine(pinYin);
            Console.ReadKey();

            string pinYin2 = Chs2PinYinHelper.Get(chs);
            Console.WriteLine(pinYin2);
            Console.ReadKey();
        }
    }
}
