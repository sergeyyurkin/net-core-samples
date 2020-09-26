using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace CookieBasedAuth.Data
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public UserContext([NotNull] DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Role adminRole = new Role
            {
                Id = 1,
                Name = "admin"
            };

            Role userRole = new Role
            {
                Id = 2,
                Name = "user"
            };

            User adminUser = new User
            {
                Id = 1,
                Email = "admin@mail.ru",
                Password = "123456",
                RoleId = adminRole.Id
            };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });

            base.OnModelCreating(modelBuilder);
        }
    }
}
