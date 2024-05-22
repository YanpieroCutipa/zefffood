using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zefffood.Integration.jsonplaceholder.dto;

namespace zefffood.Integration.jsonplaceholder
{
    public class JsonplaceholderAPIIntegration
    {
         private readonly ILogger<JsonplaceholderAPIIntegration> _logger;
        private const string API_URL="https://jsonplaceholder.typicode.com/todos/";
    
    public JsonplaceholderAPIIntegration(ILogger<JsonplaceholderAPIIntegration> logger){
            _logger=logger;
        }

        public async Task<List<Todo>> GetAll(){
            string requestUrl = $"{API_URL}";
            List<Todo> listTodos = new List<Todo>();
            using (HttpClient client = new HttpClient())
            {
                try{
                    HttpResponseMessage response = await client.GetAsync(requestUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        listTodos =  await response.Content.ReadFromJsonAsync<List<Todo>>() ??
                                        new List<Todo>();
                    }
                }catch (HttpRequestException ex)
                {
                    _logger.LogDebug($"Error al llamar a la API: {ex.Message}");
                }
            }
            return listTodos;
        }
    }
}