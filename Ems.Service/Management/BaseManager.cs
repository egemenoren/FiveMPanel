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
    public abstract class BaseManager<T> where T:EmsBaseEntity
    {
        private readonly GenericRepository<T> repo;
        public BaseManager()
        {
            repo = new GenericRepository<T>();
        }
        public T GetById(int Id)
        {
            var entity = repo.Find(Id);
            return entity;
        }
        public void Add(T Entity)
        {
            repo.Insert(Entity);
            repo.SaveChanges();
        }
        public void Update(T Entity)
        {
            repo.Update(Entity);
            repo.SaveChanges();
        }
        public List<T> GetAll()
        {
            var listEntities = repo.SelectAll();
            return listEntities;
        }
        public List<T> GetAllByParameter(Expression<Func<T,bool>> Filter = null)
        {
            if (Filter != null)
            {
                var list = repo.Select(Filter).ToList();
                return list;
            }
                
            return null;
        }
        public bool CheckIfExists(Expression<Func<T,bool>> Filter = null)
        {
            if(Filter != null)
            {
                if(repo.Select(Filter).FirstOrDefault() != null)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        public void Remove(int id)
        {
            repo.Remove(id);
            repo.SaveChanges();
        }
    }
}
