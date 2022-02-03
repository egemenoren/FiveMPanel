using Ems.Data.Model;
using Ems.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Service.Management
{
    public abstract class BaseManager<T> where T : EmsBaseEntity
    {
        internal readonly GenericRepository<T> repo;
        public BaseManager()
        {
            repo = new GenericRepository<T>();
        }
        public virtual T GetById(int Id)
        {
            var entity = repo.Find(Id);
            return entity;
        }
        public virtual T GetByParameter(Expression<Func<T, bool>> Filter = null)
        {
            if (Filter != null)
            {
                var entity = repo.Select(Filter).Where(x=>x.Status == Status.Active);
                return entity.SingleOrDefault();
            }
            return null;
        }
        public virtual void Add(T Entity)
        {
            repo.Insert(Entity);
            repo.SaveChanges();
        }
        public virtual void Update(T Entity)
        {
            repo.Update(Entity);
            repo.SaveChanges();
        }
        public virtual List<T> GetAll()
        {
            var listEntities = repo.SelectAll().Where(x=>x.Status == Status.Active).ToList();
            return listEntities;
        }
        public virtual void Remove(int Id)
        {
            var entity = repo.Select(x => x.Id == Id).FirstOrDefault();
            if (entity != null)
            {
                entity.Status = Status.InActive;
                repo.Update(entity);
            }
            repo.SaveChanges();
        }
        public virtual List<T> GetAllByParameter(Expression<Func<T, bool>> Filter = null)
        {
            if (Filter != null)
                return repo.Select(Filter).Where(x=>x.Status == Status.Active).ToList();
            return null;
        }
        public virtual bool CheckIfExists(Expression<Func<T, bool>> Filter = null)
        {
            if (Filter != null)
            {
                if (repo.Select(Filter).Count() > 0)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
