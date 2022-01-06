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

        public async Task<ICollection<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
