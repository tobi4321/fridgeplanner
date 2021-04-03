using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FridgePlanner.Models;
using FridgePlanner.Models.NutritionModels;
using FridgePlanner.Models.ViewModels;
using FridgePlanner.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace FridgePlanner.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IApiCaller _client;

        public RecipeController(IApiCaller caller)
        {
            _client = caller;
        }
        public async Task<IActionResult> Index()
        {
            JArray response = await _client.GetList("http://localhost:5000/api/RecipeApi");

            List<Recipe> recipeList = response.ToObject<List<Recipe>>();

            return View(recipeList);
        }
        [HttpPost]
        [Route("Recipe/AddRecipe")]
        public async Task<IActionResult> AddRecipe([FromBody] JObject t)
        {
            JObject response = await _client.Post("http://localhost:5000/api/RecipeApi", t);

            Recipe test = response.ToObject<Recipe>();

            return Ok(test.Id);
        }
        [HttpGet]
        [Route("Recipe/EditRecipeOverview/{Id}")]
        public async Task<IActionResult> EditRecipeOverview([FromServices]IConfiguration config, int Id)
        {
            JObject response = await _client.GetItem("http://localhost:5000/api/RecipeApi/" + Id);
            Recipe edit = response.ToObject<Recipe>();

            List<string> units = config.GetSection("Units").Get<List<string>>();

            EditRecipeViewModel viewModel = new EditRecipeViewModel() { RecipeElement = edit, Units = units };

            return View("EditRecipe",viewModel);
        }
        [HttpPost]
        [Route("Recipe/EditRecipe/")]
        public async Task<IActionResult> EditRecipe(int Id, string name, string description)
        {
            JObject response = await _client.GetItem("http://localhost:5000/api/RecipeApi/" + Id);
            Recipe edit = response.ToObject<Recipe>();

            edit.Name = name;
            edit.Description = description;

            JObject updated = await _client.Update("http://localhost:5000/api/RecipeApi/" + Id, JObject.FromObject(edit));
            edit = updated.ToObject<Recipe>();

            return Ok(edit.Id);
        }

        [HttpPost]
        [Route("Recipe/AddRecipeItem/{RecipeId}")]
        public async Task<IActionResult> AddRecipeItem([FromServices]IConfiguration config, int RecipeId, [FromBody] JObject t)
        {
            RecipeItem item = t.ToObject<RecipeItem>();

            JObject added = await _client.Post("http://localhost:5000/api/RecipeApi/Item/" + RecipeId, JObject.FromObject(item));
            Recipe edit = added.ToObject<Recipe>();

            return Ok(edit.Id);
        }
        [HttpPost]
        [Route("Recipe/AddRecipeStep/{RecipeId}")]
        public async Task<IActionResult> AddRecipeStep([FromServices]IConfiguration config, int RecipeId, [FromBody] JObject t)
        {
            RecipeStep step = t.ToObject<RecipeStep>();

            JObject added = await _client.Post("http://localhost:5000/api/RecipeApi/Step/" + RecipeId, JObject.FromObject(step));
            Recipe edit = added.ToObject<Recipe>();

            return Ok(edit.Id);
        }
        [HttpPost]
        [Route("Recipe/DeleteRecipeItem/")]
        public async Task<IActionResult> DeleteRecipeItem(long RecipeId,long RecipeItemId)
        {
            JObject updated = await _client.Delete("http://localhost:5000/api/RecipeApi/Item/" + RecipeId + "/" + RecipeItemId);
            Recipe edit = updated.ToObject<Recipe>();

            return Ok(edit.Id);
        }

        [HttpPost]
        [Route("Recipe/DeleteRecipeStep/")]
        public async Task<IActionResult> DeleteRecipeStep(long RecipeId, long RecipeStepId)
        {
            JObject updated = await _client.Delete("http://localhost:5000/api/RecipeApi/Step/" + RecipeId + "/" + RecipeStepId);
            Recipe edit = updated.ToObject<Recipe>();

            return Ok(edit.Id);
        }
        [HttpPost]
        [Route("Recipe/UpdateRecipeItem/")]
        public async Task<IActionResult> UpdateRecipeItem(int Id, long RecipeItemId, string name, double amount, string unit)
        {

            JObject response = await _client.GetItem("http://localhost:5000/api/RecipeApi/" + Id);
            Recipe edit = response.ToObject<Recipe>();

            RecipeItem item = edit.RecipeItems.Where(r => r.Id == RecipeItemId).First();

            item.Name = name;
            item.Amount = amount;
            item.Unit = unit;

            JObject updated = await _client.Update("http://localhost:5000/api/RecipeApi/Item/" + Id, JObject.FromObject(item));
            edit = updated.ToObject<Recipe>();

            return Ok(edit.Id);
        }

        [HttpPost]
        [Route("Recipe/UpdateRecipeStep/")]
        public async Task<IActionResult> UpdateRecipeStep(int Id, long RecipeStepId, string name, int number, string text)
        {
            JObject response = await _client.GetItem("http://localhost:5000/api/RecipeApi/" + Id);
            Recipe edit = response.ToObject<Recipe>();

            RecipeStep step = edit.RecipeSteps.Where(r => r.Id == RecipeStepId).First();

            step.Name = name;
            step.StepNumber = number;
            step.Text = text;

            JObject updated = await _client.Update("http://localhost:5000/api/RecipeApi/Step/" + Id, JObject.FromObject(step));
            edit = updated.ToObject<Recipe>();

            return Ok(edit.Id);
        }

        [HttpPost]
        [Route("Recipe/GetRecipeDetail")]
        public async Task<IActionResult> GetRecipeDetail(int Id)
        {
            JObject response = await _client.GetItem("http://localhost:5000/api/RecipeApi/" + Id);
            Recipe detail = response.ToObject<Recipe>();

            return View("RecipeDetailPartial", detail);
        }
        [HttpPost]
        [Route("Recipe/AddToCart")]
        public async Task<IActionResult> AddToCart(int Id)
        {
            JObject response = await _client.GetItem("http://localhost:5000/api/RecipeApi/" + Id);
            Recipe toAdd = response.ToObject<Recipe>();

            await _client.Post("http://localhost:5000/api/ShoppingApi/Cart", JObject.FromObject(toAdd));

            return Ok();
        }

        [HttpPost]
        [Route("Recipe/GetNutritionInfo")]
        public async Task<IActionResult> GetNutritionInfo([FromServices]IConfiguration config,int Id)
        {
            JObject res = await _client.GetItem("http://localhost:5000/api/RecipeApi/" + Id);
            Recipe recipe = res.ToObject<Recipe>();

            NutritionAPIResponse response = NutritionOutputForRecipe(config, recipe);

            // add the response to the viewmodel
            NutritionViewModel viewModel = new NutritionViewModel() { Recipe = recipe, NutritionResponse = response };

            return View("RecipeNutritionPartial", viewModel);
        }

        public NutritionAPIResponse NutritionOutputForRecipe(IConfiguration config, Recipe recipe)
        {
            // Testing with Nutrition and Translator

            // create Request Object 
            NutritionRequest request = new NutritionRequest() { title = "Test Recipe" };
            List<string> ingredients = new List<string>();

            foreach (RecipeItem item in recipe.RecipeItems)
            {
                // Translate each item from german to english language and add the item to the ingredient list
                ingredients.Add(item.Amount + " " + item.Unit + " " + Translator.TranslateText(item.Name, "de", "en"));
            }
            request.ingr = ingredients;
            // initialize the NutritionApiHandler
            NutritionApiHandler handler = new NutritionApiHandler();
            // send the request and wait for response
            NutritionAPIResponse response = handler.sendRequest(config,request);

            return response;
        }
    }
}