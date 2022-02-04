using AutoMapper;
using EnglishApi.Dto.TranslatesDtos;
using Models.Entities;

namespace EnglishApi.Infrastructure.Profiles
{
    public class TranslatedWordProfile : Profile
    {
        public TranslatedWordProfile()
        {
            CreateMap<TranslatedWord, TranslatedWordDto>();
            CreateMap<TranslatedWordDto, TranslatedWord>();
        }
    }
}
