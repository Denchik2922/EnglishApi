using AutoMapper;
using EnglishApi.Dto;
using Models.Entities;

namespace EnglishApi.Infrastructure.Profiles
{
    public class TestResultProfile : Profile
	{
		public TestResultProfile()
		{
			CreateMap<TestResultDto, TestResult>();
		}
	}
}
