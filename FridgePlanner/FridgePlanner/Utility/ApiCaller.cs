using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FridgePlanner.Utility
{
    public class ApiCaller
    {
        private readonly HttpClient _client;
        public ApiCaller(HttpClient client)
        {
            _client = client;
        }

        public async Task<JObject> GetItem(string routeAttributes)
        {
            var response1 = _client.GetAsync(routeAttributes);
            response1.Wait();
            var response = response1.Result;
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                JObject jsonResponse = JObject.Parse(responseString);
                return jsonResponse;
            }
            else
            {
                return null;
            }
        }
        public async Task<JArray> GetList(string routeAttributes)
        {
            var response1 = _client.GetAsync(routeAttributes);
            response1.Wait();
            var response = response1.Result;
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                JArray jsonResponse = JArray.Parse(responseString);
                return jsonResponse;
            }
            else
            {
                return null;
            }
        }

        public async Task<JObject> Post(string routeAttributes,object data)
        {
            var response1 = _client.PostAsJsonAsync(routeAttributes,data);
            response1.Wait();
            var response = response1.Result;
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                JObject jsonResponse = JObject.Parse(responseString);
                return jsonResponse;
            }
            else
            {
                return null;
            }
        }
    }
}
