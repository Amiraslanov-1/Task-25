using _25._06._2022Task.Models;
using Microsoft.EntityFrameworkCore;

namespace _25._06._2022Task.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext( DbContextOptions options) : base(options)
        {
        }
        public DbSet<UserSay> UserSays { get; set; }
    }
}
