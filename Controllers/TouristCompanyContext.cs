using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TouristOrgAdmin.Models;

namespace TouristOrgAdmin.Controllers
{
    public class TouristCompanyContext : DbContext
    {
        public DbSet<AdminAccount> AdminAccount { get; set; } = null!;
        public DbSet<Communications> Communications { get; set; } = null!;
        public DbSet<Employees> Employees { get; set; } = null!;
        public DbSet<Statuses> Statuses { get; set; } = null!;
        public DbSet<Roles> Roles { get; set; } = null!;

        public TouristCompanyContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlite("Data Source=touristorg.db");
        }
    }
}
