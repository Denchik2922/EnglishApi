using AutoMapper;
using EnglishApi.Dto;
using Models;

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
