using Configr.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Configr.Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity,new()
    {
        Task<T> Get(Expression<Func<T, bool>> filter);
        Task<List<T>> GetAll(Expression<Func<T, bool>> filter=null);
         Task<T> Add(T entity);
         Task<T> Update(T entity);
         Task<T> Delete(T entity);
    }
}
