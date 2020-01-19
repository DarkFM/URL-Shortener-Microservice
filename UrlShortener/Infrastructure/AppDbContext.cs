using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Repositories;

namespace UrlShortener.Infrastructure
{
    public class AppDbContext : DbContext, IUnitOfWork
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Website> Sites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Website>().ToTable("sites").HasKey(w => w.Id);
            modelBuilder.Entity<Website>().Property(w => w.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Website>().HasData(
                new Website { Url = "https://www.google.com", Id = 1 },
                new Website { Url = "https://www.bing.com", Id = 2 });

            base.OnModelCreating(modelBuilder);
        }
    }
}
