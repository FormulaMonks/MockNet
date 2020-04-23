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

        public async Task<Todo> GetAsync(int id)
        {
            var response = await this._httpClient.GetAsync($"/todos/{id}");

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
                response.Headers.TryGetValues("todo-id", out IEnumerable<string> values) &&
                int.TryParse(values.FirstOrDefault(), out int id))
            {
                return await GetAsync(id);
            }

            throw new Exception("Error creating todo");
        }
    }
}