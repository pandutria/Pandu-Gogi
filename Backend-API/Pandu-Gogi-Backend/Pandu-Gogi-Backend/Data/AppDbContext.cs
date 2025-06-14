using Microsoft.EntityFrameworkCore;
using Pandu_Gogi_Backend.Models.Entites;

namespace Pandu_Gogi_Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {

        }

        public DbSet<User> users { get; set; }


    }
}
