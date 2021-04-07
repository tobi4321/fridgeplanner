using FridgePlanner;
using FridgePlanner.Controllers;
using FridgePlanner.Models;
using FridgePlanner.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FridgePlannerTesting
{
    public class Startup_IntegrationTest : IClassFixture<WebAppFactory<Startup>>
    {
        // HttpClient is required to send a Api Request
        private readonly HttpClient _client;

        public Startup_IntegrationTest(WebAppFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CanGetFridgeItems()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/Fridge/GetItems");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var fridgeItems = JsonConvert.DeserializeObject<IEnumerable<FridgeItem>>(stringResponse);
            Assert.NotNull(fridgeItems);
        }

    }
}
