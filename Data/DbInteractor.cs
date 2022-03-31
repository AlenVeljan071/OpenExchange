using Microsoft.EntityFrameworkCore;
using OpenExchange.Models;

namespace OpenExchange.Data
{
    public class DbInteractor : DbContext
    {
        public DbInteractor(DbContextOptions<DbInteractor> options) : base(options)
        {

        }
        public DbSet<Root> Roots { get; set; }
        public DbSet<Rates> Rates { get; set; }
        public DbSet<EUR> EURs { get; set; }
        public DbSet<GBP> GBPs { get; set; }
        public DbSet<RootEx> RootsExs { get; set; }
        public DbSet<RatesEx> RatesExs { get; set; }

    }
}
