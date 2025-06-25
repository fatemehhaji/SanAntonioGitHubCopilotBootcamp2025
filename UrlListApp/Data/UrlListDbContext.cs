using Microsoft.EntityFrameworkCore;
using UrlListApp.Models;

namespace UrlListApp.Data
{
    public class UrlListDbContext : DbContext
    {
        public UrlListDbContext(DbContextOptions<UrlListDbContext> options) : base(options) { }

        public DbSet<UrlList> UrlLists { get; set; }
        public DbSet<UrlItem> UrlItems { get; set; }
    }
}
