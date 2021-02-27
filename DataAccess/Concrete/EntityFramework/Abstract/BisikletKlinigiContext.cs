﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BisikletKlinigi.DataAccess.Concrete
{
    public class BisikletKlinigiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string localEmirhan = @"Server=(localdb)\MSSQLLocalDB;Database=BisikletKlinigi;Trusted_Connection=true";
            string local = @"Server=DESKTOP-63SM79K;Database=BisikletKlinigi;Trusted_Connection=true";
            string server = @"Server=MSSQLSERVER2019;Database=BisikletKlinigi;User=BKAdmin;Password=%Mgi11l5";

            optionsBuilder.UseSqlServer(localEmirhan);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<ContactUs> Contacts { get; set; }
    }
}