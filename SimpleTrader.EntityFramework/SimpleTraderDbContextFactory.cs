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
            var config = _configuration ?? BuildDesignTimeConfiguration();

            var provider = config["DatabaseProvider"]
                ?? throw new InvalidOperationException("No se encontró 'DatabaseProvider' en la configuración.");

            var connectionString = config.GetConnectionString(provider)
                ?? throw new InvalidOperationException($"No se encontró la cadena de conexión para el proveedor '{provider}'.");

            var optionsBuilder = new DbContextOptionsBuilder<SimpleTraderDbContext>();

            switch (provider)
            {
                case "SqlServer":
                    optionsBuilder.UseSqlServer(connectionString);
                    break;
                case "Sqlite":
                    optionsBuilder.UseSqlite(connectionString);
                    break;
                default:
                    throw new InvalidOperationException($"Proveedor de base de datos no soportado: '{provider}'.");
            }

            return new SimpleTraderDbContext(optionsBuilder.Options);
        }

        private static IConfiguration BuildDesignTimeConfiguration() => new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: true)
           .Build();
    }
}
