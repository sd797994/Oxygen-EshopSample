using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Trade.Infrastructure.EfDataAccess
{
    public class TradeContext : DbContext
    {
        public TradeContext(DbContextOptions<TradeContext> options) : base(options)
        {

        }
        public DbSet<PersistenceObject.Trade> Trade { get; set; }
        public DbSet<PersistenceObject.TradeRecord> TradeRecord { get; set; }
    }
}
