using BLL.RequestFeatures;
using Models.Entities;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public interface IUserService
    {
        Task<PagedList<User>> GetAllAsync(PaginationParameters parameters);
        Task<User> GetByIdAsync(string userId);
    }
}