using AutoMapper;
using EnglishApi.Dto;
using Models;
using System.Linq;


namespace EnglishApi.Infrastructure.Profiles
{
    public class WordProfile : Profile
	{
		public WordProfile()
		{
			CreateMap<Word, WordDto>();
			CreateMap<WordDto, Word>();
			CreateMap<Word, WordDetailsDto>();
			CreateMap<WordDetailsDto, Word>();
		}
	}
}
