using Microsoft.EntityFrameworkCore;
using RestaurantWeb.Model;

namespace RestaurantWeb.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {

        }

        public DbSet<Category> Category { get; set; }
    }
}
