using BLL.Services.Entities;
using BLL.Tests.Infrastructure.Fakes;
using Microsoft.AspNetCore.Identity;
using Models.Entities;
using Moq;
using System.Threading.Tasks;

namespace BLL.Tests.Infrastructure.Fixtures
{
    public class AuthServiceFixture
    {
        /*public AuthService CreateService()
        {
            var userManager = new Mock<FakeUserManager>();
            userManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
            

            return new AuthService(userManager.Object);
        }*/

      /*  public UserManager<User> CreateUserManager(User user)
        {
            var userManager = new Mock<FakeUserManager>();

            userManager.Setup(x => x.FindByNameAsync(user.UserName))
                .ReturnsAsync(user);

            return userManager.Object;
        }*/
    }
}
