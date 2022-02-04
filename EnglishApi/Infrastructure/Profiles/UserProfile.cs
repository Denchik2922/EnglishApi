using AutoMapper;
using EnglishApi.Dto.UserDtos;
using Models.Entities;

namespace EnglishApi.Infrastructure.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDetailsDto>()
                .ForMember(userDto => userDto.DictionaryMatchingTests, user => user.MapFrom(u => u.MatchingTestResults))
                .ForMember(userDto => userDto.DictionarySpellingTests, user => user.MapFrom(u => u.SpellingTestResults));
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<User, UserTestDto>();
        }
    }
}
