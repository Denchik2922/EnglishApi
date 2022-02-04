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
            CreateMap<TestResultDto, ResultForSpellingTest>();
            CreateMap<TestResultDto, ResultForMatchingTest>();

            CreateMap<ResultForMatchingTest, UserTestDto>();
            CreateMap<ResultForMatchingTest, DictionaryTestDto>();
            CreateMap<ResultForSpellingTest, UserTestDto>();
            CreateMap<ResultForSpellingTest, DictionaryTestDto>();
        }
    }
}
