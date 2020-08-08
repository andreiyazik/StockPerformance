using Microsoft.EntityFrameworkCore;
using StockPerformance.Domain.Entities;
using System.Reflection;

namespace StockPerformance.Persistence.SQLServer
{
    public class DataContext : DbContext
    {
        public DataContext( DbContextOptions<DataContext> options )
            : base( options )
        {
        }

        public DbSet<Candle> Candles { get; set; }

        protected override void OnModelCreating( ModelBuilder builder )
        {
            builder.ApplyConfigurationsFromAssembly( Assembly.GetExecutingAssembly() );

            builder.Entity<Candle>( entity =>
             {
                 entity.HasKey( c => new { c.Timestamp, c.Symbol } );
             } );

            base.OnModelCreating( builder );
        }
    }
}
