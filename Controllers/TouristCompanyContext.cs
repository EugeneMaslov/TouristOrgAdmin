﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TouristOrgAdmin.Models;

namespace TouristOrgAdmin.Controllers
{
    public class TouristCompanyContext : DbContext
    {
        public DbSet<AdminAccount> AdminAccount { get; set; }

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
