using Core.DataAccess.Repositores.Abstract;
using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.Repositories.Concrete.EFCore
{
    public abstract class EFBaseRepository<TEntity, TContext> : IBaseRepository<TEntity>
          where TEntity : class, IEntity, new()
          where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly DbSet<TEntity> _entities;
        public EFBaseRepository(TContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _entities.AddAsync(entity);
        }
        public void Delete(TEntity entity)
        {
            _entities.Remove(entity);
        }
        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, params string[] incudes)
        {
            IQueryable<TEntity> query = GetQuery(incudes);
            return query.Where(filter).FirstOrDefaultAsync(filter);
        }

        public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, params string[] incudes)
        {
            IQueryable<TEntity> query = GetQuery(incudes);
            return filter == null ? query.ToListAsync() : query.Where(filter).ToListAsync();
        }

        public Task<List<TEntity>> GetAllPaginatedAsync(int page, int size, Expression<Func<TEntity, bool>> filter = null, params string[] incudes)
        {
            IQueryable<TEntity> query = GetQuery(incudes);
            return filter == null
                   ? query.Skip((page - 1) * size).Take(size).ToListAsync()
                   : query.Where(filter).Skip((page - 1) * size).Take(size).ToListAsync();
        }


        public void Update(TEntity entity)
        {
            _entities.Update(entity);

        }

        private IQueryable<TEntity> GetQuery(params string[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }

     
    }
}
