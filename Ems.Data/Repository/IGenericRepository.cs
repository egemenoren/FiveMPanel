using Ems.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Data.Repository
{
    public interface IGenericRepository<T> where T: EmsBaseEntity
    {
        T Find(long EntityId);
        IEnumerable<T> Select(Expression<Func<T, bool>> Filter = null);
        void Insert(T Entity);
        void Remove(long EntityId);
        void Update(T Entity);
        List<T> SelectAll();
    }
}
