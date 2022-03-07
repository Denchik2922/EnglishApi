using AutoMapper;
using EnglishApi.Dto.WordDtos;
using Models.Entities;
using System.Linq;

namespace EnglishApi.Infrastructure.Profiles
{
    public class WordProfile : Profile
    {
        public WordProfile()
        {
            CreateMap<LearnedWord, LearnedWord>();
            CreateMap<LearnedWord, LearnedWordDto>();
            CreateMap<LearnedWordDto, LearnedWord>();
            CreateMap<Word, Word>().ForMember(w => w.Dictionary, opt => opt.Ignore());
            CreateMap<Word, WordDto>().ForMember(wordDto => wordDto.LearnedWord, word => word.MapFrom(w => w.LearnedWords.FirstOrDefault()));
            CreateMap<WordDto, Word>();
        }
    }
}
