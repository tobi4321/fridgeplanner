using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FridgePlanner.Models;
using FridgePlanner.Models.ViewModels;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using FridgePlanner.Utility;
using System.Net.Http;

namespace FridgePlanner.Controllers
{
    public class FridgeController : Controller
    {
        private readonly IApiCaller _client;

        public FridgeController(IApiCaller caller)
        {
            _client = caller;
        }

        public async Task<IActionResult> Index([FromServices]IConfiguration config)
        {
            return View(await createViewModelAsync(config));
        }


        [HttpPost]
        [Route("Fridge/AddItem")]
        public async Task<IActionResult> AddFridgeItemAsync([FromBody] JObject t)
        {
            // new Layer to get data 

            JObject response = await _client.Post("http://localhost:5000/api/FridgeApi",t);

            FridgeItem test = response.ToObject<FridgeItem>();

            // ------------------------------------------------------

            return View("FridgeTablePartial", await GetFridgeItemsAsync());
        }



        [HttpPost]
        [Route("Fridge/DeleteItem")]
        public async Task<IActionResult> DeleteFridgeItemAsync(long Id)
        {

            JObject response = await _client.Delete("http://localhost:5000/api/FridgeApi/"+Id);

            return View("FridgeTablePartial",await GetFridgeItemsAsync());
        }

        private async Task<List<FridgeItem>> GetFridgeItemsAsync()
        {

            JArray items = await _client.GetList("http://localhost:5000/api/FridgeApi");
            List<FridgeItem> fridgeItems = items.ToObject<List<FridgeItem>>();

            DateTime now = DateTime.Today;
            fridgeItems = fridgeItems.OrderBy(item => item.ExpiryDate.Subtract(now).TotalDays).ToList();

            return fridgeItems;
        }
        [HttpGet]
        [Route("Fridge/GetItems")]
        public async Task<ActionResult<IEnumerable<FridgeItem>>> GetItems()
        {
            JArray items = await _client.GetList("http://localhost:5000/api/FridgeApi");
            return items.ToObject<List<FridgeItem>>();
        }

        [HttpPost]
        [Route("Fridge/GetEditFridgeModal")]
        public async Task<IActionResult> GetEditFridgeModalAsync([FromServices]IConfiguration config, int Id)
        {

            JObject response = await _client.GetItem("http://localhost:5000/api/FridgeApi/" + Id);
            FridgeItem edit = response.ToObject<FridgeItem>();

            List<string> units = config.GetSection("Units").Get<List<string>>();

            EditFridgeViewModel model = new EditFridgeViewModel() { Item = edit, Units = units };

            return View("EditFridgeItemPartial", model);
        }
        [HttpPost]
        [Route("Fridge/UpdateFridgeItem")]
        public async Task<IActionResult> UpdateFridgeItemAsync(int Id, string name, double amount, string unit, DateTime expiry)
        {
            JObject response = await _client.GetItem("http://localhost:5000/api/FridgeApi/"+Id);
            FridgeItem edit = response.ToObject<FridgeItem>();

            edit.Name = name;
            edit.Amount = amount;
            edit.Unit = unit;
            edit.ExpiryDate = expiry;
            edit.ExpiryDate = edit.ExpiryDate.Date;

            JObject updated = await _client.Update("http://localhost:5000/api/FridgeApi/" + Id,JObject.FromObject(edit));

            return View("FridgeTablePartial", await GetFridgeItemsAsync());
        }

        [HttpPost]
        [Route("Fridge/GetRecipeDetail")]
        public async Task<IActionResult> GetRecipeDetailAsync(int Id)
        {
            //Recipe detail = _context.Recipes.Include(r => r.RecipeItems).Include(r => r.RecipeSteps).Where(r => r.RecipeId == Id).First();

            JObject test = await _client.GetItem("http://localhost:5000/api/RecipeApi/" + Id);

            Recipe detail = test.ToObject<Recipe>();

            return View("RecipeHomeDetailPartial", detail);
        }


        private async Task<IndexViewModel> createViewModelAsync([FromServices]IConfiguration config)
        {

            List<FridgeItem> fridgeItems = await GetFridgeItemsAsync();

            List<Recipe> recipes = new List<Recipe>();

            if (fridgeItems.Count() > 0)
            {
                JArray response = await _client.GetList("http://localhost:5000/api/RecipeApi");

                List<Recipe> recipeList = response.ToObject<List<Recipe>>();


                recipes = recipeList.Where(item => item.RecipeItems.Any(i => fridgeItems.Any(f => i.Name == f.Name))).ToList();
            }
            
            List<string> units = config.GetSection("Units").Get<List<string>>();

            IndexViewModel model = new IndexViewModel { FridgeItems = fridgeItems, Recipes = recipes , Units = units};

            return model;
        }
    }
}
