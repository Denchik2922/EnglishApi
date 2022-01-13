using BLL.Exceptions;
using BLL.Tests.Infrastructure.Fixtures;
using Models.Entities;
using System.Threading.Tasks;
using Xunit;

namespace BLL.Tests.Services
{
    public class AuthServiceTests : IClassFixture<AuthServiceFixture>
    {
        private readonly AuthServiceFixture _fixture;
        public AuthServiceTests(AuthServiceFixture fixture)
        {
            _fixture = fixture;
        }

       /* [Fact]
        public async Task Should_Register_User()
        {
            //arrange
            var user = new User()
            {
                Email = "user@example.com",
                UserName = "UserTest",
            };
            var service = _fixture.CreateService();
            var userManager = _fixture.CreateUserManager(user);
            var password = "Den29_sep2000";
            
            //act
            await service.Register(user, password);
            var response = await userManager.FindByNameAsync(user.UserName);

            //assert
            Assert.Equal(user.Id, response.Id);
            Assert.Equal(user.UserName, response.UserName);
            Assert.Equal(user.Email, response.Email);
        }*/
    }
}
