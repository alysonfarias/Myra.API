using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Myra.Domain.Core;
using Myra.Domain.Interfaces.Common;
using Myra.Infra.Context;
using System.Linq.Expressions;

namespace Myra.Infra.Repositories.Base
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Register
    {
        private readonly DbSet<T> _dbSet;
        private readonly ApplicationDbContext _context;
        private IQueryable<T> _firstQuery;

        public GenericRepository(ApplicationDbContext context)
        {
            _dbSet = context.Set<T>();
            _context = context;
            _firstQuery = _dbSet.AsQueryable();
        }

        public IQueryable<T> FirstQuery()
        {
            return _firstQuery;
        }

        public async Task<T> Add(T entity)
        {
            entity.CreatedAt = DateTime.Now;
            var result = await _dbSet.AddAsync(entity);
            return await Task.FromResult(result.Entity);
        }

        public async Task<T> Update(T entity)
        {
            entity.UpdatedAt = DateTime.Now;
            var result = _dbSet.Update(entity);
            return await Task.FromResult(result.Entity);
        }

        public async Task<T> Remove(int id)
        {
            var entityRemove = await GetById(filter: x => x.Id == id);
            var result = _dbSet.Remove(entityRemove);
            return await Task.FromResult(result.Entity);
        }

        public async Task<T> GetById(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {

            IQueryable<T> query = _dbSet;

            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync(filter);
        }

        public async Task<IEnumerable<T>> GetAll(
          Expression<Func<T, bool>>? predicate = null,
          Func<IQueryable<T>,
          IIncludableQueryable<T, object>>? include = null,
          int? skip = null,
          int? take = null)
        {
            IQueryable<T> query = _dbSet;


            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (include != null)
            {
                query = include(query);
            }

            if (skip != null && skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take != null && take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.ToListAsync();

        }

        public async Task<int> CountAll(
            Expression<Func<T, bool>>? predicate = null)
        {
            IQueryable<T> query = _dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return await query.CountAsync();
        }
    }
}
