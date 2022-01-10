using AutoMapper;
using EnglishApi.Dto;
using Models.Entities;
using System.Linq;

namespace EnglishApi.Infrastructure.Profiles
{
    public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<User, UserDto>()
				.ForMember(userDto => userDto.DictionaryMatchingTests, user => user.MapFrom(u => u.MatchingTestResults.Select(t => t.EnglishDictionary)))
				.ForMember(userDto => userDto.DictionarySpellingTests, user => user.MapFrom(u => u.SpellingTestResults.Select(t => t.EnglishDictionary)));
			CreateMap<UserDto, User>();
			CreateMap<User, UserTestDto>();
		}
	}
}
