using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace substringTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string a = "真的爱你哦";
            Console.WriteLine(a.Substring(1, a.Length - 1));
            Console.ReadKey();
        }
    }
}
