using BLL.Services.Entities;
using BLL.Tests.Infrastructure.Helpers;
using System.Threading.Tasks;
using Xunit;

namespace BLL.Tests.Services
{
    public class TranslatedWordServiceTests
    {
        [Fact]
        public async Task Should_Return_Translated_Word_By_Id()
        {
            //arrange
            var context = new DbContextHelper().Context;
            var service = new TranslatedWordService(context);
            int translatedId = 1;

            //act
            var response = await service.GetByIdAsync(translatedId);

            //assert
            Assert.Equal(translatedId, response.Id);
        }

        [Fact]
        public async Task Should_Return_NotNull_Translated_Word_Relationship_Entity_By_Id()
        {
            //arrange
            var context = new DbContextHelper().Context;
            var service = new TranslatedWordService(context);
            int translatedId = 1;

            //act
            var response = await service.GetByIdAsync(translatedId);

            //assert
            Assert.NotNull(response.Word);
        }
    }
}
