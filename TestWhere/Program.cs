using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWhere
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] a = { "sadfasdf", "gdfgdfgdf", "fdgfgfre", "dfgdfe" };
            List<string> b = a.Where(c => c.Contains('e')).ToList();
            string d = string.Empty;
            foreach (string item in b)
            {
                d += item + "|";
            }
            Console.WriteLine(d);
            Console.ReadKey();
        }
    }
}
