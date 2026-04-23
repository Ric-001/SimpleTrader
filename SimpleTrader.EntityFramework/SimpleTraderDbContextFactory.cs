using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

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

            var provider = config["DatabaseProvider"] ?? throw new InvalidOperationException("Falta 'DatabaseProvider' en appsettings.json. Valores válidos: 'Sqlite' o 'SqlServer'.");

            var connectionString = config.GetConnectionString(provider) ?? throw new InvalidOperationException($"No hay cadena de conexión para '{provider}' en appsettings.json. " +                    $"Añade una entrada en 'ConnectionStrings:{provider}'.");

            var optionsBuilder = new DbContextOptionsBuilder<SimpleTraderDbContext>();

            _ = provider switch
            {
                "SqlServer" => optionsBuilder.UseSqlServer(connectionString),
                "Sqlite" => optionsBuilder.UseSqlite(connectionString),
                _ => throw new InvalidOperationException($"Proveedor '{provider}' no soportado. Usa 'Sqlite' o 'SqlServer'.")
            };

            return new SimpleTraderDbContext(optionsBuilder.Options);
        }

        private static IConfiguration BuildDesignTimeConfiguration() => new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: true)
           .Build();
    }
}
