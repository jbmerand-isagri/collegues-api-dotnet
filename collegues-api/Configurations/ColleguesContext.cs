using ColleguesApi.Models;
using ColleguesApi.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;

namespace ColleguesApi.Configurations
{
    public class ColleguesContext : DbContext
    {
        public ColleguesContext(DbContextOptions<ColleguesContext> options) : base(options)
        {
        }

        public DbSet<Collegue> Collegues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder != null)
            {
                modelBuilder.Entity<Collegue>()
                    .ToTable("Collegue");
            }
            else
            {
                throw new ProblemeTechniqueException();
            }
        }
    }
}