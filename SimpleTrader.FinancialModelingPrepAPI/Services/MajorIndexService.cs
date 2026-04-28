using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.FinancialModelingPrepAPI.Options;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{

    public class MajorIndexService : IMajorIndexService
    {
        private readonly FinancialModelingPrepHttpCliente _client;
        private readonly FinancialModelingPrepOptions _options;

        private static readonly Dictionary<MajorIndexType, string> IndexSymbols = new()
        {
            { MajorIndexType.DowJones, "^DJI"  },
            { MajorIndexType.Nasdaq,   "^IXIC" },
            { MajorIndexType.SP500,    "^GSPC" },
        };

        public MajorIndexService(FinancialModelingPrepHttpCliente client, FinancialModelingPrepOptions options)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<MajorIndex> GetMajorIndex(MajorIndexType indexType)
        {
            if (!IndexSymbols.TryGetValue(indexType, out string? symbol))
                throw new ArgumentException($"Tipo de índice no soportado: {indexType}");

            
            var dto = await _client.GetAsync<MajorIndexDto>(
                Uri.EscapeDataString(symbol), _options.QuoteEndpoint);

            return new MajorIndex
            {
                Price = dto.Price,
                Changes = dto.Change,
                Type = indexType
            };
        }
    }
}
