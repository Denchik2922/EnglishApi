using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Models.Entities
{
    public class CustomRole : IdentityRole
    {
        public ICollection<CustomUserRole> UserRoles { get; set; }
    }
}
