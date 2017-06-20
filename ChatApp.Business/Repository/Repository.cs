using ChatApp.Model.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Business.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IModel
    {
        private readonly ChatAppContext _context;
        private readonly DbQuery<T> _readOnlySet;

        public Repository(ChatAppContext context)
        {
            _context = context;
            _readOnlySet = _context.Set<T>().AsNoTracking();
        }

        public List<T> List(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] eagerloads)
        {
            var set = _readOnlySet.Where(filter ?? (x => true)).AsQueryable();

            if (eagerloads != null)
                foreach (var eagerload in eagerloads)
                    set = set.Include(eagerload);

            return set.ToList();
        }

        public T Find(int id, params Expression<Func<T, object>>[] eagerloads)
        {
            if (eagerloads != null)
                foreach (var eagerload in eagerloads)
                    _readOnlySet.Include(eagerload);

            return _readOnlySet.FirstOrDefault(x => x.Id.Equals(id));
        }

        public T Find(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] eagerloads)
        {
            if (eagerloads != null)
                foreach (var eagerload in eagerloads)
                    _readOnlySet.Include(eagerload);

            return _readOnlySet.FirstOrDefault(filter ?? (x => true));
        }

        public T Create(T model)
        {
            if (model == null)
                return null;

            var entry = _context.Entry(model);
            entry.State = EntityState.Added;
            _context.SaveChanges();

            return model;
        }

        public void Update(T model)
        {
            if (model == null)
                return;

            var entry = _context.Entry(model);
            entry.State = EntityState.Modified;
            entry.CurrentValues.SetValues(model);
            _context.SaveChanges();
        }

        public void Delete(T model)
        {
            if (model == null)
                return;

            var entry = _context.Entry(model);
            entry.State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
