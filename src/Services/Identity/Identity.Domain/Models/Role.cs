using Identity.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Identity.Domain.Models
{
    public class Role : IdentityRole
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
