using AutoMapper;
using EnglishApi.Dto.ExamplesWordDtos;
using Models.Entities;

namespace EnglishApi.Infrastructure.Profiles
{
    public class ExamplesWordProfile : Profile
    {
        public ExamplesWordProfile()
        {
            CreateMap<WordExample, ExampleWordDto>();
            CreateMap<ExampleWordDto, WordExample>();
        }
    }
}
