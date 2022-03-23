﻿using Microsoft.EntityFrameworkCore;
using Order.Domain;
using Order.Persistence.Database.conf;

namespace Order.Persistence.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options
        )
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ModelConfig(builder);
        }

        public DbSet<Domain.Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }

        private void ModelConfig(ModelBuilder modelBuilder)
        {
            new OrderConfiguration(modelBuilder.Entity<Domain.Order>());
            new OrderDetailConfiguration(modelBuilder.Entity<OrderDetail>());
        }
    }
}