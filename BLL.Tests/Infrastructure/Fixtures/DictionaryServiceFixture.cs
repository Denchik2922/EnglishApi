using BLL.Services.Entities;
using BLL.Tests.Infrastructure.Helpers;

namespace BLL.Tests.Infrastructure.Fixtures
{
    public class DictionaryServiceFixture
    {
        public DictionaryService Create()
        {
            var context = new DbContextHelper().Context;
            var mapper = MapperHelper.GetInstance();
            return new DictionaryService(context, mapper);
        }
    }
}
