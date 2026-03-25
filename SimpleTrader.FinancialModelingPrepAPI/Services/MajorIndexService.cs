using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using System.Text.Json;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public partial class MajorIndexService : IMajorIndexService
    {
        private const string API_KEY = "RYiPoM5uCF5boZ9HUh5u8pZ3w0k9yFcF";

        public async Task<MajorIndex> GetMajorIndex(MajorIndexType indexType)
        {
            using HttpClient client = new HttpClient();
            string url = GetUriForIndexType(indexType);

            HttpResponseMessage response = await client.GetAsync(url);
            //response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<MajorIndexDto>? dtoList = JsonSerializer.Deserialize<List<MajorIndexDto>>(jsonResponse, options);

            if (dtoList == null || dtoList.Count == 0)
                throw new Exception("No se han recibido datos del índice.");

            MajorIndexDto dto = dtoList[0];

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
                MajorIndexType.DowJones => $"https://financialmodelingprep.com/stable/profile?symbol=DTSQ&apikey={API_KEY}",
                MajorIndexType.Nasdaq => $"https://financialmodelingprep.com/stable/profile?symbol=AAPL&apikey={API_KEY}",
                MajorIndexType.SP500 => $"https://financialmodelingprep.com/stable/profile?symbol=RAAQ&apikey={API_KEY}",
                _ => throw new ArgumentException("Tipo de índice no soportado.")
            };
        }
    }
}
