using BLL.Services.Entities;
using BLL.Tests.Infrastructure.Fakes;
using BLL.Tests.Infrastructure.Helpers;
using Moq;

namespace BLL.Tests.Infrastructure.Fixtures
{
    public class UserServiceFixture
    {
        public UserService Create()
        {
            var context = new DbContextHelper().Context;
            return new UserService(context);
        }
    }
}
