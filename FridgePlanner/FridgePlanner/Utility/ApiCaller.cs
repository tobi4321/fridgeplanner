using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FridgePlanner.Utility
{
    public class ApiCaller : IApiCaller
    {
        private readonly HttpClient _client;
        public ApiCaller(HttpClient client)
        {
            _client = client;
        }

        public async Task<JObject> GetItem(string routeAttributes)
        {
            var apiResponse = _client.GetAsync(routeAttributes);
            apiResponse.Wait();
            var response = apiResponse.Result;
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
            var apiResponse = _client.GetAsync(routeAttributes);
            apiResponse.Wait();
            var response = apiResponse.Result;
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
            var apiResponse = _client.PostAsJsonAsync(routeAttributes,data);
            apiResponse.Wait();
            var response = apiResponse.Result;
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

        public async Task<JObject> Delete(string routeAttributes)
        {
            var apiResponse = _client.DeleteAsync(routeAttributes);
            apiResponse.Wait();
            var response = apiResponse.Result;
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

        public async Task<JObject> Update(string routeAttributes,object data)
        {
            var apiResponse = _client.PutAsJsonAsync(routeAttributes,data);
            apiResponse.Wait();
            var response = apiResponse.Result;
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
