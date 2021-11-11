using Ems.Data.Data;
using Ems.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Data.Repository
{
    public class GenericRepository<T>:IGenericRepository<T> where T:EmsBaseEntity
    {
        private EmsContext _context;
        private DbSet<T> _dbSet;

        public GenericRepository()
        {
            _context = new EmsContext();
            _dbSet = _context.Set<T>();
        }
        public virtual T Find(long EntityId)
        {
            return _dbSet.Find(EntityId);
        }

        public virtual void Insert(T Entity)
        {
            _dbSet.Add(Entity);
        }

        public virtual void Remove(long EntityId)
        {
            var entity = _dbSet.Find(EntityId);
            _dbSet.Remove(entity);
        }

        public virtual void Update(T Entity)
        {
            _dbSet.Attach(Entity);
            _context.Entry(Entity).State = EntityState.Modified;
        }
        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }

        public List<T> SelectAll()
        {
            return _dbSet.ToList();
        }

        public IEnumerable<T> Select(Expression<Func<T, bool>> Filter = null)
        {
            if (Filter != null)
            {
                return _dbSet.Where(Filter);
            }
            return _dbSet;
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> Filter = null)
        {
            if (Filter != null)
            {
                return _dbSet.Where(Filter);
            }
            return _dbSet;
        }
    }
}
