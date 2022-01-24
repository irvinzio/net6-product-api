using Microsoft.EntityFrameworkCore;
using Tekton.Data.Entities;

namespace Tekton.Data.Context
{
    public class TektonContext : DbContext
    {
        public TektonContext(DbContextOptions<TektonContext> options)
        : base(options)
        { }

        public DbSet<Product> Products { get; set; }
    }
}
