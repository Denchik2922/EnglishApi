using AutoMapper;
using EnglishApi.Dto.TestResultDtos;
using Models.Entities;

namespace EnglishApi.Infrastructure.Profiles
{
    public class TestResultProfile : Profile
    {
        public TestResultProfile()
        {
            CreateMap<TypeOfTestingDto, TypeOfTesting>();
            CreateMap<TypeOfTesting, TypeOfTestingDto>();
            CreateMap<TestResultDto, TestResult>();
            CreateMap<TestResult, TestResultForStatisticDto>();
        }
    }
}
