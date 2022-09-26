using Microsoft.EntityFrameworkCore.Query;
using Myra.Domain.Core;
using System.Linq.Expressions;

namespace Myra.Domain.Interfaces.Common
{
    public interface IGenericRepository<T> where T : Register
    {
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Remove(int id);
        Task<T> GetById(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        Task<IEnumerable<T>> GetAll(
          Expression<Func<T, bool>>? predicate = null,
          Func<IQueryable<T>,
          IIncludableQueryable<T, object>>? include = null,
          int? skip = null,
          int? take = null);
        Task<int> CountAll(Expression<Func<T, bool>>? predicate = null);
        IQueryable<T> FirstQuery();

    }
}
