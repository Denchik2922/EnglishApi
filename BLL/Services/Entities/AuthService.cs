using BLL.Interfaces.Entities;
using Microsoft.AspNetCore.Identity;
using Models.Entities;
using System;
using System.Threading.Tasks;

namespace BLL.Services.Entities
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;

        public AuthService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task Register(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    throw new Exception($"Code: {error.Code}, Description: { error.Description}");
                }
            }
        }
    }
}
