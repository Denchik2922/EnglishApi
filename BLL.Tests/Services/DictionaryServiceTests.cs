using BLL.Exceptions;
using BLL.RequestFeatures;
using BLL.Tests.Infrastructure.Fixtures;
using Models.Entities;
using System.Threading.Tasks;
using Xunit;

namespace BLL.Tests.Services
{
    public class DictionaryServiceTests : IClassFixture<DictionaryServiceFixture>
    {
        private readonly DictionaryServiceFixture _fixture;
        public DictionaryServiceTests(DictionaryServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Should_Return_All_Dictionaries()
        {
            //arrange
            var service = _fixture.Create();
            var paginParam = new PaginationParameters();

            //act
            var response = await service.GetAllAsync(paginParam);

            //assert
            Assert.Equal(1, response.Count);
        }

        [Fact]
        public async Task Should_Return_Dictionary_By_Id()
        {
            //arrange
            var service = _fixture.Create();
            int dictionaryId = 1;

            //act
            var response = await service.GetByIdAsync(dictionaryId);

            //assert
            Assert.Equal(dictionaryId, response.Id);
        }



        [Fact]
        public async Task Should_Return_NotNull_Dictionary_Relationship_Entity_By_Id()
        {
            //arrange
            var service = _fixture.Create();
            int dictionaryId = 1;

            //act
            var response = await service.GetByIdAsync(dictionaryId);

            //assert
            Assert.NotNull(response.Words);
            Assert.NotNull(response.Tags);
            Assert.NotNull(response.Creator);
        }

        [Fact]
        public async Task Should_Return_ItemNotFound_ThrowException_GetDictionaryById()
        {
            //arrange
            var service = _fixture.Create();
            int dictionaryId = 3;

            //act & assert
            await Assert.ThrowsAsync<ItemNotFoundException>(() => service.GetByIdAsync(dictionaryId));
        }

        [Fact]
        public async Task Should_Add_Dictionary_In_Db()
        {
            //arrange
            var service = _fixture.Create();
            var dictionary = new EnglishDictionary()
            {
                Id = 2,
                Name = $"Dictionary 2",
                Description = $"Dictionary description 2",
                UserId = "b54e2482-5cd6-40d1-be4f-0d14e7e614e7",
                IsPrivate = true,
            };

            //act
            await service.AddAsync(dictionary);
            var response = await service.GetByIdAsync(dictionary.Id);

            //assert
            Assert.Equal(dictionary.Id, response.Id);
            Assert.Equal(dictionary.Name, response.Name);
            Assert.Equal(dictionary.Description, response.Description);
            Assert.Equal(dictionary.UserId, response.UserId);
            Assert.Equal(dictionary.IsPrivate, response.IsPrivate);
        }

        [Fact]
        public async Task Should_Delete_Dictionary_By_Id()
        {
            //arrange
            var service = _fixture.Create();
            int dictionaryId = 1;

            //act
            await service.DeleteAsync(dictionaryId);

            //assert
            await Assert.ThrowsAsync<ItemNotFoundException>(() => service.GetByIdAsync(dictionaryId));
        }


        [Fact]
        public async Task Should_Return_ItemNotFound_ThrowException_DictionaryDelete()
        {
            //arrange
            var service = _fixture.Create();
            int dictionaryId = 3;

            //act & assert
            await Assert.ThrowsAsync<ItemNotFoundException>(() => service.DeleteAsync(dictionaryId));
        }

        [Fact]
        public async Task Should_Update_Dictionary_In_Db()
        {
            //arrange
            var service = _fixture.Create();
            var dictionaryId = 1;
            var dictionary = await service.GetByIdAsync(dictionaryId);
            dictionary.Name = "Dictionary 2";
            dictionary.Description = "Dictionary Desk 2";
            dictionary.IsPrivate = false;

            //act
            await service.UpdateAsync(dictionary);
            var response = await service.GetByIdAsync(dictionaryId);

            //assert
            Assert.Equal(dictionary.Id, response.Id);
            Assert.Equal(dictionary.Name, response.Name);
            Assert.Equal(dictionary.Description, response.Description);
            Assert.Equal(dictionary.IsPrivate, response.IsPrivate);
            Assert.Equal(dictionary.UserId, response.UserId);
        }


        [Fact]
        public async Task Should_Return_ItemNotFound_ThrowException_DictionaryUpdate()
        {
            //arrange
            var service = _fixture.Create();
            var dictionary = new EnglishDictionary()
            {
                Id = 3,
                Name = "Dictionary 2",
                Description = "Dictionary Desk 2"
            };

            //act & assert
            await Assert.ThrowsAsync<ItemNotFoundException>(() => service.UpdateAsync(dictionary));
        }

    }
}
