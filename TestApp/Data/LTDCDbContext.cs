using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LTDCWebservice.Models;

namespace LTDCWebservice.Data
{
    public class LTDCDbContext : DbContext
    {
        public LTDCDbContext (DbContextOptions<LTDCDbContext> options)
            : base(options)
        {

        }

        public DbSet<Team> Teams { get; set; } = default!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=ltdc.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().ToTable("Teams");
        }

    }
}
