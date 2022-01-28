using AutoMapper;
using Moq;

namespace BLL.Tests.Infrastructure.Helpers
{
    public static class MapperHelper
    {
        public static IMapper GetInstance()
        {
            var config = new MapperConfiguration(cfg => { });
            return config.CreateMapper();
        }

        public static Mock<IMapper> GetMock()
        {
            return new Mock<IMapper>();
        }
    }
}
