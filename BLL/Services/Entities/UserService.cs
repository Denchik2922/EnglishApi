using BLL.Exceptions;
using BLL.Interfaces.Entities;
using BLL.RequestFeatures;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
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
            return await PagedList<User>
                         .ToPagedList(_context.Users, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<User> GetByIdAsync(string userId)
        {
            var user = await _context.Users
                .Include(u => u.EnglishDictionaries)
                .Include(u => u.SpellingTestResults)
                .ThenInclude(r => r.EnglishDictionary)
                .Include(u => u.MatchingTestResults)
                .ThenInclude(r => r.EnglishDictionary)
                .AsSplitQuery()
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new ItemNotFoundException($"{typeof(User).Name} with id {userId} not found");
            }

            return user;
        }
    }
}
