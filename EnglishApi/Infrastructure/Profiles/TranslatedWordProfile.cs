using AutoMapper;
using EnglishApi.Dto;
using Models;
using System.Linq;

namespace EnglishApi.Infrastructure.Profiles
{
    public class TranslatedWordProfile : Profile
	{
		public TranslatedWordProfile()
		{
			CreateMap<Tag, TagDto>();
			CreateMap<TagDto, Tag>();

			CreateMap<Tag, TagDetailsDto>()
				.ForMember(tagDto => tagDto.EnglishDictionaries, Tag => Tag.MapFrom(t => t.EnglishDictionaries.Select(d => d.EnglishDictionary)));
			CreateMap<TagDetailsDto, Tag>()
			   .ForMember(tag => tag.EnglishDictionaries, opt => opt
					 .MapFrom(tagDto => tagDto.EnglishDictionaries.Select(d => new EnglishDictionaryTag { EnglishDictionaryId = d.Id })));
		}
	}
}
