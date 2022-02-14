using AutoMapper;
using EnglishApi.Dto.DictionaryDtos;
using Models.Entities;
using System.Linq;

namespace EnglishApi.Infrastructure.Profiles
{
    public class DictionaryProfile : Profile
    {
        public DictionaryProfile()
        {
            CreateMap<EnglishDictionary, EnglishDictionary>()
                .ForMember(dictionary => dictionary.Words, opt => opt.Ignore())
                .ForMember(dictionary => dictionary.Creator, opt => opt.Ignore());
            CreateMap<EnglishDictionary, DictionaryDto>()
                .ForMember(dictionaryDto => dictionaryDto.Tags, dictionary => dictionary.MapFrom(d => d.Tags.Select(t => t.Tag)));
            CreateMap<DictionaryDto, EnglishDictionary>()
                .ForMember(dictionary => dictionary.Tags, opt => opt
                     .MapFrom(dictionaryDto => dictionaryDto.Tags.Select(t => new EnglishDictionaryTag { TagId = t.Id })));
        }
    }
}
