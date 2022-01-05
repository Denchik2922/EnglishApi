using BLL.Services.Entities;
using BLL.Tests.Infrastructure.Fixtures;
using BLL.Tests.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task Should_return_all_dictionaries()
        {
            //arrange
            var service = _fixture.Create();

            //act
            var response = await service.GetAllAsync();

            //assert
            Assert.Equal(1, response.Count);
        }

        [Fact]
        public async Task Should_return_dictionary_by_id()
        {
            //arrange
            var service = _fixture.Create();
            int dictionaryId = 1;

            //act
            var response = await service.GetByIdAsync(dictionaryId);

            //assert
            Assert.Equal(dictionaryId, response.Id);
        }


    }
}
