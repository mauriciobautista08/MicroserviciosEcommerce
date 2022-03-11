using Identity.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Persistence.DB.config
{
    public class UserConfiguration
    {
        public UserConfiguration(EntityTypeBuilder<User> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder.Property(x => x.FirstName).IsRequired().HasMaxLength(30);
            entityTypeBuilder.Property(x => x.LastName).IsRequired().HasMaxLength(30);

            entityTypeBuilder.HasMany(e => e.UserRoles).WithOne(e => e.User).HasForeignKey(e => e.UserId).IsRequired();
        }
    }
}
