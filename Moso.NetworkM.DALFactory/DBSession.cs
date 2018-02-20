using Moso.NetworkM.DAL;
using Moso.NetworkM.IDAL;
using Moso.NetworkM.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moso.NetworkM.DALFactory
{
    /// <summary>
    /// 数据会话层：一个工厂类，封装了所有数据操作类的实例的创建。
    /// 然后业务层通过数据会话层来获取所有数据操作类的实例
    /// 数据会话层将数据层与业务层解耦
    /// </summary>
    public partial class DBSession : IDBSession
    {
        //MosoNetEntities Db = new MosoNetEntities();
        public DbContext Db
        {
            get
            {
                return DBContextFactory.CreateDbContext();
            }
        }
        //private IMainInfoDal _MainInfoDal;
        //public IMainInfoDal MainInfoDal
        //{
        //    get
        //    {
        //        if (_MainInfoDal == null)
        //        {
        //            //_MainInfoDal = new MainInfoDal();
        //            _MainInfoDal = AbstractFactory.CreateMainInfoDal();//通过抽象工厂封装了类的实例的创建
        //        }
        //        return _MainInfoDal;
        //    }
        //    set
        //    {
        //        _MainInfoDal = value;
        //    }
        //}

        public bool SaveChanges()
        {
            return Db.SaveChanges() > 0;
        }


    }
}
