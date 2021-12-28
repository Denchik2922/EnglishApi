using AutoMapper;
using EnglishApi.Dto;
using Models.Entities;
using System.Linq;

namespace EnglishApi.Infrastructure.Profiles
{
    public class TagProfile : Profile
	{
		public TagProfile()
		{
			CreateMap<Tag, TagDto>();
			CreateMap<TagDto, Tag>();

			CreateMap<Tag, TagDetailsDto>()
				.ForMember(tagDto => tagDto.EnglishDictionaries, tag => tag.MapFrom(t => t.EnglishDictionaries.Select(d => d.EnglishDictionary)));
			CreateMap<TagDetailsDto, Tag>()
			   .ForMember(tag => tag.EnglishDictionaries, opt => opt
					 .MapFrom(tagDto => tagDto.EnglishDictionaries.Select(d => new EnglishDictionaryTag { EnglishDictionaryId = d.Id })));
		}
	}
}
