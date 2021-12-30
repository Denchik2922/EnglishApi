﻿using Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
    }
}