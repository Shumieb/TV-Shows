using Microsoft.EntityFrameworkCore;
using TV_Shows.Models;

namespace TV_Shows.Services
{
    public class ShowsDb : DbContext
    {
        public ShowsDb(DbContextOptions<ShowsDb> options)
            : base(options) { }

        public DbSet<Show> Shows { get; set; }
        public DbSet<Platform> Platforms { get; set; }       

    }
}
