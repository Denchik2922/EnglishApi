using Microsoft.AspNetCore.Identity;

namespace Models.Entities
{
    public class CustomUserRole : IdentityUserRole<string>
    {
        public User User { get; set; }
        public CustomRole Role { get; set; }
    }
}
