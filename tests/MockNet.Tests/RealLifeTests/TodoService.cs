using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SystemStringContent = System.Net.Http.StringContent;

namespace Theorem.MockNet.Http.Tests.RealLifeTests
{
    public class TodoService
    {
        private HttpClient _httpClient;

        public TodoService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public Task<Todo> GetAsync(int id) => GetAsync($"/todos/{id}");

        private async Task<Todo> GetAsync(string requestUri)
        {
            var response = await this._httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Todo not found");
            }

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Todo>(json);
        }

        public async Task<Todo> CreateAsync(Todo todo)
        {
            var content = new SystemStringContent(JsonConvert.SerializeObject(todo), Encoding.UTF8, "application/json");

            var response = await this._httpClient.PostAsync("/todos", content);

            if (response.IsSuccessStatusCode &&
                response.Headers.Location is Uri uri)
            {
                return await GetAsync(uri.ToString());
            }

            throw new Exception("Error creating todo");
        }
    }
}
