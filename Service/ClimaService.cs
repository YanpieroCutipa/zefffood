using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using zefffood.Models;

namespace zefffood.Service
{
    public class ClimaService : IClimaService
    {
        private readonly HttpClient _httpClient;

    public ClimaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

       public async Task<Clima> ObtenerClimaPorCiudadAsync(string ciudad)
        {
            var requestUri = $"https://api.climate.example.com/{ciudad}";
            var response = await _httpClient.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var clima = JsonConvert.DeserializeObject<Clima>(content);

            return clima;
        }
    }
}