using BLL.RequestFeatures;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public interface IBaseGenericService<T> where T : class
    {
        public Task<T> GetByIdAsync(int id);
        public Task<PagedList<T>> GetAllAsync(PaginationParameters parameters);
        public Task AddAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(int id);
    }
}
