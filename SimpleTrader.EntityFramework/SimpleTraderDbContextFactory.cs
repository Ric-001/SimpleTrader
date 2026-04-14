using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.EntityFramework
{

    public class SimpleTraderDbContextFactory : IDesignTimeDbContextFactory<SimpleTraderDbContext>
    {
        private readonly IConfiguration? _configuration;

        public SimpleTraderDbContextFactory(IConfiguration? configuration)
        {
            _configuration = configuration;
        }
        
        // Usado por EF Core en tiempo de diseño (migraciones), sin DI
        public SimpleTraderDbContextFactory() { }

        public SimpleTraderDbContext CreateDbContext(string[]? args = null)
        {
            var connectionString = _configuration?.GetConnectionString("Default")
                ?? "Server=(localdb)\\MSSQLLocalDB;Database=SimpleTraderDB;Trusted_Connection=True;";

            var options = new DbContextOptionsBuilder<SimpleTraderDbContext>();
            options.UseSqlServer(connectionString);

            return new SimpleTraderDbContext(options.Options);
        }
    }
}
