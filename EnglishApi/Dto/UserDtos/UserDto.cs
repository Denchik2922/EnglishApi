﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EnglishApi.Dto.UserDtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
