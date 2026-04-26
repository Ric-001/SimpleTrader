using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.FinancialModelingPrepAPI.Options;
using SimpleTrader.FinancialModelingPrepAPI.Results;
using System.Text.Json;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{

    public class MajorIndexService : IMajorIndexService
    {
        private readonly FinancialModelingPrepOptions _options;

        private static readonly Dictionary<MajorIndexType, string> IndexSymbols = new()
        {
            { MajorIndexType.DowJones, "^DJI"  },
            { MajorIndexType.Nasdaq,   "^IXIC" },
            { MajorIndexType.SP500,    "^GSPC" },
        };

        public MajorIndexService(FinancialModelingPrepOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrWhiteSpace(_options.ApiKey))
                throw new InvalidOperationException("ApiKey de FinancialModelingPrep no configurada.");
        }

        public async Task<MajorIndex> GetMajorIndex(MajorIndexType indexType)
        {
            if (!IndexSymbols.TryGetValue(indexType, out string? symbol))
                throw new ArgumentException($"Tipo de índice no soportado: {indexType}");

            using FinancialModelingPrepHttpCliente client = new(_options);
            var dto = await client.GetAsync<MajorIndexDto>(
                Uri.EscapeDataString(symbol),   // ^IXIC → %5EIXIC
                _options.QuoteEndpoint
            );

            return new MajorIndex
            {
                Price = dto.Price,
                Changes = dto.Change,
                Type = indexType
            };
        }
    }

    public partial class MajorIndexServiceOld : IMajorIndexService
    {
        private readonly FinancialModelingPrepOptions _options;




        public MajorIndexServiceOld(FinancialModelingPrepOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrWhiteSpace(_options.ApiKey))
                throw new InvalidOperationException("ApiKey de FinancialModelingPrep no configurada.");
        }

        

        public async Task<MajorIndex> GetMajorIndex(MajorIndexType indexType)
        {

            using FinancialModelingPrepHttpCliente client = new(_options);
            //var endpoint = _options.ProfileEndpoint.TrimStart('/');
            string url = GetUriForIndexType(indexType);

            var dto = await client.GetAsync<MajorIndexDto>(url);

            MajorIndex majorIndex = new MajorIndex
            {
                Price = dto.Price,
                Changes = dto.Change,
                Type = indexType 
            };

            return majorIndex;
        }

        private string GetUriForIndexType(MajorIndexType indexType)
        {
            return indexType switch
            {
                MajorIndexType.DowJones => $"DTSQ",
                MajorIndexType.Nasdaq => $"AAPL",
                MajorIndexType.SP500 => $"RAAQ",
                _ => throw new ArgumentException("Tipo de índice no soportado.")
            };
        }
    }
}
