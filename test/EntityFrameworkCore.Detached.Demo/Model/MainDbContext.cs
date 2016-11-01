﻿using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Detached.Demo.Model
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }

        public DbSet<SellPoint> SellPoints { get; set; }

        public DbSet<SellPointType> SellPointTypes { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }
    }
}
