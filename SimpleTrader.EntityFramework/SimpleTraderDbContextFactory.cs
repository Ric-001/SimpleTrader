using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
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

        // Usado por EF en tiempo de diseño — lee appsettings.json él mismo
        public SimpleTraderDbContextFactory() { }

        public SimpleTraderDbContext CreateDbContext(string[]? args = null)
        {
            var connectionString = _configuration?.GetConnectionString("Default")
            ?? BuildDesignTimeConfiguration().GetConnectionString("Default")
            ?? throw new InvalidOperationException("No se encontró la cadena de conexión.");

            var options = new DbContextOptionsBuilder<SimpleTraderDbContext>();
            options.UseSqlServer(connectionString);

            return new SimpleTraderDbContext(options.Options);
        }

        private static IConfiguration BuildDesignTimeConfiguration() => new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: true)
           .Build();
    }
}
