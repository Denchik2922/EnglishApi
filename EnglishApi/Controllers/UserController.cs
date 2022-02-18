using AutoMapper;
using BLL.Interfaces.Entities;
using BLL.RequestFeatures;
using EnglishApi.Dto.UserDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin")]
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
        [Route("roles")]
        public async Task<ActionResult<ICollection<CustomRole>>> GetAllUserRoles()
        {
            var userRoles = await _userService.GetAllUserRoles();
            return Ok(userRoles);
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
        public async Task<ActionResult<UserDto>> GetUserById(string userId)
        {
            var user = await _userService.GetByIdAsync(userId);
            UserDto userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpPost]
        [Route("set-password")]
        public async Task<IActionResult> ChangePassword(SetPasswordDto setPassword)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem();
            }
            await _userService.ChangePassword(setPassword.UserId, setPassword.NewPassword);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditUser(EditUserDto userModel)
        {
            var user = _mapper.Map<User>(userModel);
            await _userService.Edit(user, userModel.Roles);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto userModel)
        {
            if (userModel == null)
            {
                return BadRequest("UserRegister object is null");
            }
            if (!ModelState.IsValid)
            {
                return ValidationProblem();
            }
            var user = _mapper.Map<User>(userModel);
            await _userService.Create(user, userModel.Password, userModel.Roles);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(string id)
        {
            await _userService.Delete(id);
            return Ok();
        }

    }
}
