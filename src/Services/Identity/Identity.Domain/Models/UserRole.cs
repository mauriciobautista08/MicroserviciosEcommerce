using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Models
{
    public class UserRole : IdentityUserRole<string>
    {
        public Role Role { get; set; }
        public User User { get; set; }
    }
}
