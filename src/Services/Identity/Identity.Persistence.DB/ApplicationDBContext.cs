using Identity.Domain.Models;
using Identity.Persistence.DB.config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Persistence.DB
{
    public class ApplicationDBContext : IdentityDbContext<User, Role, string>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ModelConfig(builder);

        }

        private void ModelConfig(ModelBuilder builder)
        {
            new RoleConfiguration(builder.Entity<Role>());
            new UserConfiguration(builder.Entity<User>());
        }
    }
}
