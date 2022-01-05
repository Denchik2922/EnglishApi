using BLL.Services.Entities;
using BLL.Tests.Infrastructure.Helpers;
using System.Threading.Tasks;
using Xunit;

namespace BLL.Tests.Services
{
    public class TagServiceTests
    {
         
        [Fact]
        public async Task Should_return_all_tags()
        {
            //arrange
            var context = new DbContextHelper().Context;
            var service = new TagService(context);

            //act
            var response = await service.GetAllAsync();

            //assert
            Assert.NotNull(response);
        }

    }
}
