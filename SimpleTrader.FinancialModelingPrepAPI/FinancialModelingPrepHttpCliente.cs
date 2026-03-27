using SimpleTrader.FinancialModelingPrepAPI.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace SimpleTrader.FinancialModelingPrepAPI
{
    public class FinancialModelingPrepHttpCliente : HttpClient
    {
        readonly string baseUrl;
        readonly string endpoint;
        readonly string apiKey;

        //https://financialmodelingprep.com/stable/profile?symbol=AAPL&apikey=RYiPoM5uCF5boZ9HUh5u8pZ3w0k9yFcF

        public FinancialModelingPrepHttpCliente(FinancialModelingPrepOptions options)
        {
            baseUrl = options.BaseUrl.TrimEnd('/');
            endpoint = options.ProfileEndpoint.TrimStart('/');
            apiKey = options.ApiKey;

        }

        public async Task<T> GetAsync<T>(string symbol)
        {
            string uri = baseUrl + "/" + endpoint + symbol + "&apikey=" + apiKey;
            HttpResponseMessage response = await GetAsync(uri);
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();
            
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<T>? result;

            try
            {
                result = JsonSerializer.Deserialize<List<T>>(jsonResponse, options);
            }
            catch (Exception ex)
            {
                // Aquí podrás inspeccionar ex en el depurador
                System.Diagnostics.Debug.WriteLine(
                    $"Error deserializando respuesta para {uri}: {ex.Message}\nJSON: {jsonResponse}");
                throw; // vuelve a lanzar para no ocultar el error
            }

            if (result == null || result.Count == 0)
                throw new InvalidOperationException($"Failed to deserialize response from {uri}");
            
            return result[0];
        }
    }
}
