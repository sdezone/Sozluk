using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Api.Domain.Models;
using Sozluk.Infrastructure.Persistence.Context;

namespace Sozluk.Api.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public int Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            return _context.SaveChanges();
        }

        public int Add(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
            return _context.SaveChanges();
        }

        public async Task<int> AddAsync(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public int Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return _context.SaveChanges();
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public int Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            return _context.SaveChanges();
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
        public TEntity GetById(Guid id, bool noTracking = true)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (noTracking)
                query = query.AsNoTracking();

            return query.SingleOrDefault(e => e.Id == id);
        }

        public int Delete(Guid id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                return _context.SaveChanges();
            }
            return 0;
        }

        public bool DeleteRange(Expression<Func<TEntity, bool>> predicate)
        {
            var entitiesToDelete = _context.Set<TEntity>().Where(predicate);
            _context.Set<TEntity>().RemoveRange(entitiesToDelete);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entitiesToDelete = await _context.Set<TEntity>().Where(predicate).ToListAsync();
            _context.Set<TEntity>().RemoveRange(entitiesToDelete);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> AddOrUpdateAsync(TEntity entity)
        {
            if (entity.Id == Guid.Empty)
            {
                await _context.Set<TEntity>().AddAsync(entity);
            }
            else
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            return await _context.SaveChangesAsync();
        }

        public int AddOrUpdate(TEntity entity)
        {
            if (entity.Id == Guid.Empty)
            {
                _context.Set<TEntity>().Add(entity);
            }
            else
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            return _context.SaveChanges();
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public async Task<List<TEntity>> GetAll(bool noTracking = true)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (noTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (noTracking)
                query = query.AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = orderBy(query);

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (noTracking)
                query = query.AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (noTracking)
                query = query.AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.SingleOrDefaultAsync(predicate);
        }

        public async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (noTracking)
                query = query.AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (noTracking)
                query = query.AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.Where(predicate);
        }

        public async Task BulkDeleteById(IEnumerable<Guid> ids)
        {
            foreach (var id in ids)
            {
                var entity = await GetByIdAsync(id);
                if (entity != null)
                {
                    _context.Set<TEntity>().Remove(entity);
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task BulkDelete(Expression<Func<TEntity, bool>> predicate)
        {
            var entitiesToDelete = _context.Set<TEntity>().Where(predicate);
            _context.Set<TEntity>().RemoveRange(entitiesToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task BulkDelete(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task BulkUpdate(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        }

        public async Task BulkAdd(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public IQueryable<TEntity> AsQuaryable()
        {
            return _context.Set<TEntity>().AsQueryable();
        }
    }
}
