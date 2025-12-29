using AccessControlApi.Domian.Common;
using AccessControlApi.Domian.Interfaces;
using AccessControlApi.Domian.Models;
using AccessControlApi.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AccessControlApi.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseModel, ISoftDeletable
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> Create(TEntity body)
        {
            _dbSet.Add(body);
            await _context.SaveChangesAsync();
            return body;
        }

        public async Task<bool> Delete(TEntity entity)
        {
            entity.Deleted = true;
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TEntity>> GetAll(IncludeDelegate<TEntity>? include = null)
        {
            IQueryable<TEntity> query = ApplySoftDeleteFilter(_dbSet);
            if (include != null)
            {
                query = include(query);
            }
            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetOne(int id, IncludeDelegate<TEntity>? include = null)
        {
            IQueryable<TEntity> query = ApplySoftDeleteFilter(_dbSet);
            if (include != null)
                query = include(query);
            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<TEntity> Update(TEntity body)
        {
            _dbSet.Update(body);
            await _context.SaveChangesAsync();
            return body;
        }
        protected IQueryable<TEntity> ApplySoftDeleteFilter(
       IQueryable<TEntity> query)
        {
            return query.Where(e => !e.Deleted);
        }
    }
}
