using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public interface IBaseGenaricService<T> where T : class
    {
        public Task<T> GetByIdAsync(int id);
        public Task<ICollection<T>> GetAllAsync();
        public Task AddAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(int id);
    }
}
