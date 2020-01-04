using Goods.Infrastructure.EfDataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Goods.Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<GoodsContext>
    {
        public GoodsContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<GoodsContext>();
            builder.UseSqlServer("Server=192.168.1.174;Database=OxygenEshopSample;UId=sa;Pwd=sa;");
            return new GoodsContext(builder.Options);
        }

    }
}
