using User.Infrastructure.EfDataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace User.Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<UserContext>
    {
        public UserContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<UserContext>();
            builder.UseSqlServer("Server=192.168.1.174;Database=OxygenEshopSample;UId=sa;Pwd=sa;");
            return new UserContext(builder.Options);
        }

    }
}
