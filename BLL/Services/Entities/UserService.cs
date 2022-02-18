using BLL.Exceptions;
using BLL.Interfaces.Entities;
using BLL.RequestFeatures;
using BLL.ServiceExtensions;
using DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.Entities
{
    public class UserService : IUserService
    {
        private readonly EnglishContext _context;
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager, EnglishContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task Create(User user, string password, ICollection<string> roles)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, roles);
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    throw new Exception($"Code: {error.Code}, Description: { error.Description}");
                }
            }
        }

        public async Task Edit(User user, ICollection<string> roles)
        {
            var oldUser = await _userManager.FindByIdAsync(user.Id);
            oldUser.UserName = user.UserName;
            oldUser.Email = user.Email;

            var oldRoles = await _userManager.GetRolesAsync(oldUser);
            await _userManager.RemoveFromRolesAsync(oldUser, oldRoles);
            await _userManager.AddToRolesAsync(oldUser, roles);

            await _userManager.UpdateAsync(oldUser);
        }

        public async Task Delete(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ItemNotFoundException($"{typeof(User).Name} with id {userId} not found");
            }
            await _userManager.DeleteAsync(user);
        }

        public async Task ChangePassword(string userId, string newPassword)
        {
            var oldUser = await _userManager.FindByIdAsync(userId);

            var token = await _userManager.GeneratePasswordResetTokenAsync(oldUser);

            await _userManager.ResetPasswordAsync(oldUser, token, newPassword);
        }

        public async Task<ICollection<CustomRole>> GetAllUserRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public virtual async Task<PagedList<User>> GetAllAsync(PaginationParameters parameters)
        {
            var users = GetUsersQueryable()
                        .Search(parameters.SearchTerm)
                        .Sort(parameters.OrderBy);

            return await PagedList<User>
                         .ToPagedList(users, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<User> GetByIdAsync(string userId)
        {
            var user = await GetUsersQueryable()
                                    .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ItemNotFoundException($"{typeof(User).Name} with id {userId} not found");
            }

            return user;
        }

        private IQueryable<User> GetUsersQueryable()
        {
            return _context.Users
                            .Include(u => u.UserRoles)
                                .ThenInclude(r => r.Role)
                            .AsSplitQuery();
        }
    }
}
