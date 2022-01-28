using BLL.Exceptions;
using BLL.Interfaces.Entities;
using BLL.RequestFeatures;
using DAL;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BLL.Services.Entities
{
    public abstract class BaseGenericService<T> : IBaseGenericService<T> where T : class
    {
        protected readonly EnglishContext _context;
        protected readonly DbSet<T> _dbSet;
        public BaseGenericService(EnglishContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<PagedList<T>> GetAllAsync(PaginationParameters parameters)
        {
            return await PagedList<T>
                         .ToPagedList(_dbSet, parameters.PageNumber, parameters.PageSize);
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                throw new ItemNotFoundException($"{typeof(T).Name} with id {id} not found");
            }

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                throw new ItemNotFoundException($"{typeof(T).Name} with id {id} not found");
            }
            return entity;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
