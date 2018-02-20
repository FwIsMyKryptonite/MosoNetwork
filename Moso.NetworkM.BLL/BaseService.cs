using Moso.NetworkM.DALFactory;
using Moso.NetworkM.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moso.NetworkM.BLL
{
    /// <summary>
    /// 总结：抽象类中的方法

    ///1.抽象方法

    ///抽象方法不能有方法体，在被继承之后可以重载方法。而且必须被继承。

    ///2.普通方法

    ///普通方法可以被继承也可以不被继承。与虚方法的区别在于普通方法不能重载，也就是方法体无法改变。

    ///3.虚方法

    ///虚方法可以被继承也可以不被继承。与普通方法的区别在于虚方法继承时要加override关键字而且允许被重载，也就是修改方法体。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseService<T> where T : class,new()
    {
        public IDBSession CurrentDBSession
        {
            get
            {
                //return new DBSession();
                return DBSessionFactory.CreateDBSession();
            }
        }
        public IBaseDal<T> CurrentDal { get; set; }
        public abstract void SetCurrentDal();//定义一个抽象方法
        public BaseService()
        {
            SetCurrentDal();//子类必须实现抽象方法
        }

        public IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDal.LoadEntities(whereLambda);
        }
        public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, bool isAsc)
        {
            return CurrentDal.LoadPageEntities<s>(pageIndex, pageSize, out totalCount, whereLambda, orderbyLambda, isAsc);
        }
        public T AddEntity(T entity)
        {
            CurrentDal.AddEntity(entity);
            CurrentDBSession.SaveChanges();
            return entity;
        }
        public bool DeleteEntity(T entity)
        {
            CurrentDal.DeleteEntity(entity);
            return CurrentDBSession.SaveChanges();
        }
        public bool EditEntity(T entity)
        {
            CurrentDal.EditEntity(entity);
            return CurrentDBSession.SaveChanges();
        }

    }
}
