using KeyVaultDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace KeyVaultDemo.Contexts
{
    public class DemoContext : DbContext
    {
        public DemoContext(DbContextOptions<DemoContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasKey(x => x.Id);
            modelBuilder.Entity<Movie>().Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
