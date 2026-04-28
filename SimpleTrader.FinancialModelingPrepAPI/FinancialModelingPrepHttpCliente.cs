using SimpleTrader.Domain.Exceptions;
using SimpleTrader.FinancialModelingPrepAPI.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace SimpleTrader.FinancialModelingPrepAPI
{
    public class FinancialModelingPrepHttpCliente 
    {
        private readonly HttpClient _client;
        private readonly  string baseUrl;
        private readonly string endpoint;
        private readonly string apiKey;

        public FinancialModelingPrepHttpCliente(FinancialModelingPrepOptions options, HttpClient client)
        {
            _client = client;
            baseUrl = options.BaseUrl.TrimEnd('/');
            endpoint = options.ProfileEndpoint.TrimStart('/');
            apiKey = options.ApiKey;
        }

        public async Task<T> GetAsync<T>(string symbol)
        {
            string uri = baseUrl + "/" + endpoint + "?symbol=" + symbol + "&apikey=" + apiKey;
            HttpResponseMessage response = await _client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(jsonResponse) || jsonResponse.Trim() == "[]")
                throw new InvalidSymbolException(symbol, $"El símbolo '{symbol}' no es válido o no existe.");

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

        public async Task<T> GetAsync<T>(string symbol, string endpoint)
        {
            string uri = baseUrl + "/" + endpoint.TrimStart('/') + "?symbol=" + symbol + "&apikey=" + apiKey;
            HttpResponseMessage response = await _client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(jsonResponse) || jsonResponse.Trim() == "[]")
                throw new InvalidSymbolException(symbol, $"El símbolo '{symbol}' no es válido o no existe.");

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<List<T>>(jsonResponse, options);

            if (result == null || result.Count == 0)
                throw new InvalidOperationException($"Failed to deserialize response from {uri}");

            return result[0];
        }
    }
}
