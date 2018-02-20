using Moso.NetworkM.IDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Moso.NetworkM.DALFactory
{
    /// <summary>
    /// 通过反射的形式创建出类的实例
    /// </summary>
    public partial class AbstractFactory
    {
        private static readonly string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
        private static readonly string NameSpace = ConfigurationManager.AppSettings["NameSpace"];
        //public static IMainInfoDal CreateMainInfoDal()
        //{
        //    string fullClassName = NameSpace + ".MainInfoDal";
        //    return CreateInstance(fullClassName) as IMainInfoDal;
        //}

        private static object CreateInstance(string className)
        {
            //先加载程序集
            var assembly = Assembly.Load(AssemblyPath);
            return assembly.CreateInstance(className);
        }
    }

}
