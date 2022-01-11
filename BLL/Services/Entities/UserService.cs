using BLL.Exceptions;
using BLL.Interfaces.Entities;
using DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Collections.Generic;
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

        public async Task<ICollection<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
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
