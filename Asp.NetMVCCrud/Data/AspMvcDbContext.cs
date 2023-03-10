using Asp.NetMVCCrud.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Asp.NetMVCCrud.Data
{
    public class AspMvcDbContext : DbContext
    {
        public AspMvcDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
    }
}
