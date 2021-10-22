using Microsoft.EntityFrameworkCore;

using RecruitmentWpfApp.Interfaces;
using RecruitmentWpfApp.Models;

namespace RecruitmentWpfApp.Database
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<PersonData> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            _ = optionsBuilder.UseInMemoryDatabase("PeoplesDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            _ = modelBuilder.Entity<PersonData>().HasKey(item => item.Id);
        }
    }
}
