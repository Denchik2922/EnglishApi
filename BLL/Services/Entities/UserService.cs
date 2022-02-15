using BLL.Exceptions;
using BLL.Interfaces.Entities;
using BLL.RequestFeatures;
using BLL.ServiceExtensions;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.Entities
{
    public class UserService : IUserService
    {
        private readonly EnglishContext _context;
        public UserService(EnglishContext context)
        {
            _context = context;
        }

        public virtual async Task<PagedList<User>> GetAllAsync(PaginationParameters parameters)
        {
            var users = _context.Users
                        .Search(parameters.SearchTerm)
                        .Sort(parameters.OrderBy);
            return await PagedList<User>
                         .ToPagedList(users, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<User> GetByIdAsync(string userId)
        {
            var user = await GetUsersWithAllInludesQueryable()
                                    .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ItemNotFoundException($"{typeof(User).Name} with id {userId} not found");
            }

            return user;
        }

        private IQueryable<User> GetUsersWithAllInludesQueryable()
        {
            return _context.Users
                            .Include(u => u.EnglishDictionaries)
                            .Include(u => u.TestResults)
                            .AsSplitQuery();
        }
    }
}
