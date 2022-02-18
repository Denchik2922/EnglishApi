using AutoMapper;
using EnglishApi.Dto.UserDtos;
using Models.Entities;
using System.Linq;

namespace EnglishApi.Infrastructure.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<CreateUserDto, User>();
            CreateMap<EditUserDto, User>();
            CreateMap<User, UserDto>()
                .ForMember(userDto => userDto.Roles, user => user.MapFrom(u => u.UserRoles.Select(r => r.Role.Name)));
                
        }
    }
}
