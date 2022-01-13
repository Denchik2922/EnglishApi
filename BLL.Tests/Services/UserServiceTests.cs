using BLL.Tests.Infrastructure.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BLL.Tests.Services
{
    public class UserServiceTests : IClassFixture<UserServiceFixture>
    {
        private readonly UserServiceFixture _fixture;
        public UserServiceTests(UserServiceFixture fixture)
        {
            _fixture = fixture;
        }

       /* [Fact]
        public async Task Should_Return_All_Users()
        {
            //arrange
            var service = _fixture.Create();

            //act
            var response = await service.GetAll();

            //assert
            Assert.Equal(1, response.Count);
        }*/
    }
}
