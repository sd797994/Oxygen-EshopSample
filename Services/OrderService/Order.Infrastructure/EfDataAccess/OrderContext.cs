using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Infrastructure.EfDataAccess
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {

        }
        public DbSet<PersistenceObject.Order> Order { get; set; }
        public DbSet<PersistenceObject.OrderItem> OrderItem { get; set; }
        public DbSet<PersistenceObject.OrderHandleLog> OrderHandleLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
