using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Goods.Infrastructure.EfDataAccess
{
    public class GoodsContext : DbContext
    {
        public GoodsContext(DbContextOptions<GoodsContext> options) : base(options)
        {

        }
        public DbSet<PersistenceObject.Goods> Goods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
