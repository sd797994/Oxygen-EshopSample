using Order.Infrastructure.EfDataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Order.Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<OrderContext>
    {
        public OrderContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<OrderContext>();
            builder.UseSqlServer("Server=192.168.1.174;Database=OxygenEshopSample;UId=sa;Pwd=sa;");
            return new OrderContext(builder.Options);
        }

    }
}
