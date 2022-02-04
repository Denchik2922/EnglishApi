using AutoMapper;
using EnglishApi.Dto.WordDtos;
using Models.Entities;

namespace EnglishApi.Infrastructure.Profiles
{
    public class WordProfile : Profile
    {
        public WordProfile()
        {
            CreateMap<Word, Word>().ForMember(w => w.Dictionary, opt => opt.Ignore());
            CreateMap<Word, WordDto>();
            CreateMap<WordDto, Word>();
        }
    }
}
