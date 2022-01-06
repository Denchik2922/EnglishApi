using BLL.Services.Entities;
using BLL.Tests.Infrastructure.Helpers;

namespace BLL.Tests.Infrastructure.Fixtures
{
    public class TagServiceFixture
    {
        public TagService Create()
        {
            var context = new DbContextHelper().Context;
            return new TagService(context);
        }
    }
}