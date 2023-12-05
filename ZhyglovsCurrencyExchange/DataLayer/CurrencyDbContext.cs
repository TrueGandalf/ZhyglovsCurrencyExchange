using static System.Collections.Specialized.BitVector32;
using System.Collections.Generic;
using ZhyglovsCurrencyExchange.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using ZhyglovsCurrencyExchange.DataLayer.Configurations;

namespace ZhyglovsCurrencyExchange.DataLayer
{
    public class CurrencyDbContext : DbContext
    {
        public DbSet<Currency> Currencies { get; set; }


        public CurrencyDbContext(DbContextOptions<CurrencyDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CurrencyConfiguration());

            builder.HasDefaultSchema("exchange");
            builder.ApplyConfigurationsFromAssembly(typeof(CurrencyDbContext).Assembly);
        }
    }
}
