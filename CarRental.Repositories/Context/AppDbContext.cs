using CarRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Repositories.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
