using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Business.Repository
{
    public interface IRepository<T> where T : class
    {
        List<T> List(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] eagerloads);
        T Find(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] eagerloads);
        T Find(int id, params Expression<Func<T, object>>[] eagerloads);
        T Create(T model);
        void Update(T model);
        void Delete(T model);
    }
}
