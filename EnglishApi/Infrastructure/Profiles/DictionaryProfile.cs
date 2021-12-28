using AutoMapper;
using EnglishApi.Dto;
using Models.Entities;
using System.Linq;

namespace EnglishApi.Infrastructure.Profiles
{
    public class DictionaryProfile : Profile
	{
		public DictionaryProfile()
		{
			CreateMap<EnglishDictionary, EnglishDictionary>();
			CreateMap<EnglishDictionary, DictionaryDto>()
				.ForMember(dictionaryDto => dictionaryDto.Tags, dictionary => dictionary.MapFrom(d => d.Tags.Select(t => t.Tag)));
			CreateMap<DictionaryDto, EnglishDictionary>()
				.ForMember(dictionary => dictionary.Tags, opt => opt
					 .MapFrom(dictionaryDto => dictionaryDto.Tags.Select(t => new EnglishDictionaryTag { TagId = t.Id })));

			CreateMap<EnglishDictionary, DictionaryDetailsDto>()
				.ForMember(dictionaryDto => dictionaryDto.Tags, dictionary => dictionary.MapFrom(d => d.Tags.Select(t => t.Tag)))
				.ForMember(dictionaryDto => dictionaryDto.TestUsers, dictionary => dictionary.MapFrom(d => d.TestResults.Select(t => t.User)));
		}
	}
}
