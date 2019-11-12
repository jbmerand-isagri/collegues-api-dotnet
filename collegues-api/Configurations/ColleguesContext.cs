using collegues_api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace collegues_api.Configurations
{
    public class ColleguesContext : DbContext
    {
        public ColleguesContext(DbContextOptions<ColleguesContext> options) : base(options)
        {
        }
        public DbSet<Collegue> Collegues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Collegue>().ToTable("Collegue");
        }
    }
}
