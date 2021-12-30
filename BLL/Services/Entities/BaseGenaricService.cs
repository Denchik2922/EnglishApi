using BLL.Exceptions;
using BLL.Interfaces.Entities;
using DAL;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.Entities
{
    public class BaseGenaricService<T> : IBaseGenaricService<T> where T : class
    {
        protected readonly EnglishContext _context;
        private readonly DbSet<T> _dbSet;
        public BaseGenaricService(EnglishContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
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

        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
