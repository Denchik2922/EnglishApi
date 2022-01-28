using AutoMapper;
using BLL.Interfaces.Entities;
using BLL.RequestFeatures;
using EnglishApi.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<UserDto>>> GetAll([FromQuery] PaginationParameters parameters)
        {
            var users = await _userService.GetAllAsync(parameters);

            ICollection<UserDto> usersDto = _mapper.Map<ICollection<UserDto>>(users);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(users.MetaData));

            return Ok(usersDto);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDetailsDto>> GetUserById(string userId)
        {
            var user = await _userService.GetByIdAsync(userId);
            UserDetailsDto userDto = _mapper.Map<UserDetailsDto>(user);
            return Ok(userDto);
        }

    }
}
