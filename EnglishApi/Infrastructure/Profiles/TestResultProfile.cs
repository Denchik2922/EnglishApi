using AutoMapper;
using EnglishApi.Dto.DictionaryDtos;
using EnglishApi.Dto.TestResultDtos;
using EnglishApi.Dto.UserDtos;
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
        }
    }
}
