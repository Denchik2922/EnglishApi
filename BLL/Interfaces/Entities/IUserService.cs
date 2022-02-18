using BLL.RequestFeatures;
using Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public interface IUserService
    {
        Task<PagedList<User>> GetAllAsync(PaginationParameters parameters);
        Task<User> GetByIdAsync(string userId);
        Task<ICollection<CustomRole>> GetAllUserRoles();
        Task Create(User user, string password, ICollection<string> roles);
        Task ChangePassword(string userId, string newPassword);
        Task Edit(User user, ICollection<string> roles);
        Task Delete(string userId);
    }
}