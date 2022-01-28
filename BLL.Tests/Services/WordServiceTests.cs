using BLL.Exceptions;
using BLL.RequestFeatures;
using BLL.Tests.Infrastructure.Fixtures;
using Models.Entities;
using System.Threading.Tasks;
using Xunit;

namespace BLL.Tests.Services
{
    public class WordServiceTests : IClassFixture<WordServiceFixture>
    {
        private readonly WordServiceFixture _fixture;
        public WordServiceTests(WordServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Should_Return_All_Words()
        {
            //arrange
            var service = _fixture.Create();
            var paginParam = new PaginationParameters();

            //act
            var response = await service.GetAllAsync(paginParam);

            //assert
            Assert.Equal(2, response.Count);
        }

        [Fact]
        public async Task Should_Return_Word_By_Id()
        {
            //arrange
            var service = _fixture.Create();
            int wordId = 1;

            //act
            var response = await service.GetByIdAsync(wordId);

            //assert
            Assert.Equal(wordId, response.Id);
        }

        [Fact]
        public async Task Should_Return_NotNull_Word_Relationship_Entity_By_Id()
        {
            //arrange
            var service = _fixture.Create();
            int wordId = 1;

            //act
            var response = await service.GetByIdAsync(wordId);

            //assert
            Assert.NotNull(response.Dictionary);
            Assert.NotNull(response.Translates);
        }

        [Fact]
        public async Task Should_Return_ItemNotFound_ThrowException_GetWordById()
        {
            //arrange
            var service = _fixture.Create();
            int wordId = 3;

            //act & assert
            await Assert.ThrowsAsync<ItemNotFoundException>(() => service.GetByIdAsync(wordId));
        }

        [Fact]
        public async Task Should_Add_Word_In_Db()
        {
            //arrange
            var service = _fixture.Create();
            var word = new Word()
            {
                Id = 3,
                Name = $"Word 3",
                Transcription = "Word Transcription 3",
                AudioUrl = "Word Audio 3",
                PictureUrl = "Word Picture 3",
                EnglishDictionaryId = 1

            };

            //act
            await service.AddAsync(word);
            var response = await service.GetByIdAsync(word.Id);

            //assert
            Assert.Equal(word.Id, response.Id);
            Assert.Equal(word.Name, response.Name);
            Assert.Equal(word.Transcription, response.Transcription);
            Assert.Equal(word.AudioUrl, response.AudioUrl);
            Assert.Equal(word.PictureUrl, response.PictureUrl);
        }

        [Fact]
        public async Task Should_Delete_Word_By_Id()
        {
            //arrange
            var service = _fixture.Create();
            int wordId = 1;

            //act
            await service.DeleteAsync(wordId);

            //assert
            await Assert.ThrowsAsync<ItemNotFoundException>(() => service.GetByIdAsync(wordId));
        }


        [Fact]
        public async Task Should_Return_ItemNotFound_ThrowException_WordDelete()
        {
            //arrange
            var service = _fixture.Create();
            int wordId = 3;

            //act & assert
            await Assert.ThrowsAsync<ItemNotFoundException>(() => service.DeleteAsync(wordId));
        }

        [Fact]
        public async Task Should_Update_Word_In_Db()
        {
            //arrange
            var service = _fixture.Create();
            var wordId = 1;
            var word = await service.GetByIdAsync(wordId);
            word.Name = $"Word 2";
            word.Transcription = "Word Transcription 2";
            word.AudioUrl = "Word Audio 2";
            word.PictureUrl = "Word Picture 2";

            //act
            await service.UpdateAsync(word);
            var response = await service.GetByIdAsync(wordId);

            //assert
            Assert.Equal(word.Id, response.Id);
            Assert.Equal(word.Name, response.Name);
            Assert.Equal(word.Transcription, response.Transcription);
            Assert.Equal(word.AudioUrl, response.AudioUrl);
            Assert.Equal(word.PictureUrl, response.PictureUrl);
        }


        [Fact]
        public async Task Should_Return_ItemNotFound_ThrowException_WordUpdate()
        {
            //arrange
            var service = _fixture.Create();
            var word = new Word()
            {
                Id = 3,
                Name = $"Word 2",
                Transcription = "Word Transcription 2",
                AudioUrl = "Word Audio 2",
                PictureUrl = "Word Picture 2"
            };

            //act & assert
            await Assert.ThrowsAsync<ItemNotFoundException>(() => service.UpdateAsync(word));
        }
    }
}
