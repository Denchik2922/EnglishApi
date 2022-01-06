using BLL.Services.Entities;
using BLL.Tests.Infrastructure.Helpers;

namespace BLL.Tests.Infrastructure.Fixtures
{
    public class WordServiceFixture
    {
        public WordService Create()
        {
            var context = new DbContextHelper().Context;
            var mapper = MapperHelper.GetInstance();
            return new WordService(context, mapper);
        }
    }
}
