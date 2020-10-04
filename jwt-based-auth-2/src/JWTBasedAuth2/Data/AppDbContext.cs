using JWTBasedAuth2.Model;
using Microsoft.EntityFrameworkCore;

namespace JWTBasedAuth2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<User>()
                .HasData(
                    new User { Id = 1, Username = "alice", Password = "alice" },
                    new User { Id = 2, Username = "jone", Password = "jone" }
                );
        }
    }
}
