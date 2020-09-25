using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace CookieBasedAuth.Data
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserContext([NotNull] DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
