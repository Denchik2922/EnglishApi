using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Models.Entities;

namespace EnglishApi.Infrastructure.Helpers
{
    public static class DbInitializerHelper
    {
        public static void SeedAdmins(UserManager<User> userManager, IConfiguration configuration)
        {
            var email = configuration["SuperAdmins:Denis:Email"];
            var userName = configuration["SuperAdmins:Denis:UserName"];
            var password = configuration["SuperAdmins:Denis:Password"];

            if (userManager.FindByEmailAsync(email).Result == null)
            {
                User user = new User
                {
                    UserName = userName,
                    Email = email
                };

                IdentityResult result = userManager.CreateAsync(user, password).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                    userManager.AddToRoleAsync(user, "SuperAdmin").Wait();
                }
            }
        }
    }
}
