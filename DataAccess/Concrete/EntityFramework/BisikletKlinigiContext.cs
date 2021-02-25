using System;
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
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-63SM79K;Database=BisikletKlinigi;Trusted_Connection=true");
        }

        public DbSet<User> Users { get; set; }
    }
}
