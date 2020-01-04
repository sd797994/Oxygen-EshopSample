using Trade.Infrastructure.EfDataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Trade.Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TradeContext>
    {
        public TradeContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TradeContext>();
            builder.UseSqlServer("Server=192.168.1.174;Database=OxygenEshopSample;UId=sa;Pwd=sa;");
            return new TradeContext(builder.Options);
        }

    }
}
