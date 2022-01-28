using BLL.Exceptions;
using BLL.Tests.Infrastructure.Fixtures;
using Models.Entities;
using System.Threading.Tasks;
using Xunit;

namespace BLL.Tests.Services
{
    public class TagServiceTests : IClassFixture<TagServiceFixture>
    {
        private readonly TagServiceFixture _fixture;
        public TagServiceTests(TagServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Should_Return_All_Tags()
        {
            //arrange
            var service = _fixture.Create();

            //act
            var response = await service.GetAllAsync();

            //assert
            Assert.Equal(1, response.Count);
        }

        [Fact]
        public async Task Should_Return_Tag_By_Id()
        {
            //arrange
            var service = _fixture.Create();
            int tagId = 1;

            //act
            var response = await service.GetByIdAsync(tagId);

            //assert
            Assert.Equal(tagId, response.Id);
        }

        [Fact]
        public async Task Should_Return_NotNull_Tag_Relationship_Entity_By_Id()
        {
            //arrange
            var service = _fixture.Create();
            int tagId = 1;

            //act
            var response = await service.GetByIdAsync(tagId);

            //assert
            Assert.NotNull(response.EnglishDictionaries);
        }

        [Fact]
        public async Task Should_Return_ItemNotFound_ThrowException_GetTagById()
        {
            //arrange
            var service = _fixture.Create();
            int tagId = 3;

            //act & assert
            await Assert.ThrowsAsync<ItemNotFoundException>(() => service.GetByIdAsync(tagId));
        }

        [Fact]
        public async Task Should_Add_Tag_In_Db()
        {
            //arrange
            var service = _fixture.Create();
            var tag = new Tag()
            {
                Id = 2,
                Name = $"Tag 2",
                Description = $"Tag description 2",
            };

            //act
            await service.AddAsync(tag);
            var response = await service.GetByIdAsync(tag.Id);

            //assert
            Assert.Equal(tag.Id, response.Id);
            Assert.Equal(tag.Name, response.Name);
            Assert.Equal(tag.Description, response.Description);
        }

        [Fact]
        public async Task Should_Delete_Tag_By_Id()
        {
            //arrange
            var service = _fixture.Create();
            int tagId = 1;

            //act
            await service.DeleteAsync(tagId);

            //assert
            await Assert.ThrowsAsync<ItemNotFoundException>(() => service.GetByIdAsync(tagId));
        }

        [Fact]
        public async Task Should_Return_ItemNotFound_ThrowException_TagDelete()
        {
            //arrange
            var service = _fixture.Create();
            int tagId = 3;

            //act & assert
            await Assert.ThrowsAsync<ItemNotFoundException>(() => service.DeleteAsync(tagId));
        }

        [Fact]
        public async Task Should_Update_Tag_In_Db()
        {
            //arrange
            var service = _fixture.Create();
            var tagId = 1;
            var tag = await service.GetByIdAsync(tagId);
            tag.Name = "Tag 2";
            tag.Description = "Tag Desk 2";

            //act
            await service.UpdateAsync(tag);
            var response = await service.GetByIdAsync(tagId);

            //assert
            Assert.Equal(tag.Id, response.Id);
            Assert.Equal(tag.Name, response.Name);
            Assert.Equal(tag.Description, response.Description);
        }


        [Fact]
        public async Task Should_Return_ItemNotFound_ThrowException_TagUpdate()
        {
            //arrange
            var service = _fixture.Create();
            var tag = new Tag()
            {
                Id = 3,
                Name = "Tag 2",
                Description = "Tag Desk 2"
            };

            //act & assert
            await Assert.ThrowsAsync<ItemNotFoundException>(() => service.UpdateAsync(tag));
        }



    }
}
