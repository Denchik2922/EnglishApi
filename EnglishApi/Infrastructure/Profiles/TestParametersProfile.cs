using AutoMapper;
using Models.Tests;

namespace EnglishApi.Infrastructure.Profiles
{
    public class TestParametersProfile : Profile
    {
        public TestParametersProfile()
        {
            CreateMap<TestParameters, ParamsForMatchingQuestion>();
            CreateMap<TestParameters, ParamsForTranslateQuestion>();
            CreateMap<ParamsForAnswer, ParamsForCheck>();
        }
    }
}
