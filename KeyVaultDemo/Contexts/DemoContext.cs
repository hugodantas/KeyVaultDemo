using KeyVaultDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace KeyVaultDemo.Contexts
{
    public class DemoContext : DbContext
    {
        public DemoContext(DbContextOptions<DemoContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasKey(x => x.Id);
            modelBuilder.Entity<Book>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Book>().Property(x => x.Author).HasMaxLength(50);
        }
    }
}
