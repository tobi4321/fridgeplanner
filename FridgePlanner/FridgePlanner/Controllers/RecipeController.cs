using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FridgePlanner.Models;
using FridgePlanner.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace FridgePlanner.Controllers
{
    public class RecipeController : Controller
    {
        private readonly DataBaseContext _context;

        public RecipeController(DataBaseContext con)
        {
            _context = con;
        }
        public IActionResult Index()
        {
            List<Recipe> recipes = _context.Recipes
                .Include(r => r.RecipeItems)
                .Include(r => r.RecipeSteps)
                .ToList();

            return View(recipes);
        }
        [HttpPost]
        [Route("Recipe/AddRecipe")]
        public IActionResult AddRecipe([FromBody] JObject t)
        {
            Recipe add = t.ToObject<Recipe>();
            _context.Recipes.Add(add);
            _context.SaveChanges();

            int id = add.RecipeId;

            return Ok(id);
        }
        [HttpGet]
        [Route("Recipe/EditRecipeOverview/{Id}")]
        public IActionResult EditRecipeOverview([FromServices]IConfiguration config, int Id)
        {
            Recipe model = _context.Recipes
                .Include(r => r.RecipeItems)
                .Include(r => r.RecipeSteps)
                .Where(r => r.RecipeId == Id).First();

            List<string> units = config.GetSection("Units").Get<List<string>>();

            EditRecipeViewModel viewModel = new EditRecipeViewModel() { RecipeElement = model, Units = units };

            return View("EditRecipe",viewModel);
        }
        [HttpPost]
        [Route("Recipe/EditRecipe/")]
        public IActionResult EditRecipe(int Id, string name, string description)
        {
            Recipe model = _context.Recipes
                .Include(r => r.RecipeItems)
                .Include(r => r.RecipeSteps)
                .Where(r => r.RecipeId == Id).First();

            model.Name = name;
            model.Description = description;

            _context.SaveChanges();

            return Ok(model.RecipeId);
        }

        [HttpPost]
        [Route("Recipe/AddRecipeItem/{RecipeId}")]
        public IActionResult AddRecipeItem([FromServices]IConfiguration config, int RecipeId, [FromBody] JObject t)
        {
            Recipe model = _context.Recipes
                .Include(r => r.RecipeItems)
                .Include(r => r.RecipeSteps)
                .Where(r => r.RecipeId == RecipeId).First();

            RecipeItem item = t.ToObject<RecipeItem>();
            model.RecipeItems.Add(item);
            _context.SaveChanges();

            List<string> units = config.GetSection("Units").Get<List<string>>();

            EditRecipeViewModel viewModel = new EditRecipeViewModel() { RecipeElement = model, Units = units };


            return Ok(model.RecipeId);
        }
        [HttpPost]
        [Route("Recipe/AddRecipeStep/{RecipeId}")]
        public IActionResult AddRecipeStep([FromServices]IConfiguration config, int RecipeId, [FromBody] JObject t)
        {
            Recipe model = _context.Recipes
                .Include(r => r.RecipeItems)
                .Include(r => r.RecipeSteps)
                .Where(r => r.RecipeId == RecipeId).First();

            RecipeStep step = t.ToObject<RecipeStep>();
            model.RecipeSteps.Add(step);
            _context.SaveChanges();

            List<string> units = config.GetSection("Units").Get<List<string>>();

            EditRecipeViewModel viewModel = new EditRecipeViewModel() { RecipeElement = model, Units = units };

            return Ok(model.RecipeId);
        }
        [HttpPost]
        [Route("Recipe/DeleteRecipeItem/")]
        public IActionResult DeleteRecipeItem(long RecipeId,long RecipeItemId)
        {
            Recipe recipe = _context.Recipes.Include(r => r.RecipeItems).Include(r => r.RecipeSteps)
                .Where(r => r.RecipeId == RecipeId)
                .First();

            RecipeItem remove = recipe.RecipeItems.Where(r => r.Id == RecipeItemId).First();

            _context.RecipeItems.Remove(remove);
            _context.SaveChanges();

            return Ok(recipe.RecipeId);
        }

        [HttpPost]
        [Route("Recipe/DeleteRecipeStep/")]
        public IActionResult DeleteRecipeStep(long RecipeId, long RecipeStepId)
        {
            Recipe recipe = _context.Recipes.Include(r => r.RecipeItems).Include(r => r.RecipeSteps)
                .Where(r => r.RecipeId == RecipeId)
                .First();

            RecipeStep remove = recipe.RecipeSteps.Where(r => r.RecipeStepId == RecipeStepId).First();

            _context.RecipeSteps.Remove(remove);
            _context.SaveChanges();

            return Ok(recipe.RecipeId);
        }
        [HttpGet]
        [Route("Recipe/EditRecipeItemOverview/{Id}/{RecipeItemId}")]
        public IActionResult GetEditRecipeItemModal([FromServices]IConfiguration config, int Id, long RecipeItemId)
        {
            Recipe model = _context.Recipes
                .Include(r => r.RecipeItems)
                .Include(r => r.RecipeSteps)
                .Where(r => r.RecipeId == Id).First();

            RecipeItem item = _context.RecipeItems.Where(i => i.Id == Id).First();
            
            List<string> units = config.GetSection("Units").Get<List<string>>();

            EditRecipeItemViewModel viewModel = new EditRecipeItemViewModel() { Item = item, Units = units, RecipeElement = model};

            return View("EditRecipeItemPartial", viewModel);
        }
        [HttpPost]
        [Route("Recipe/UpdateRecipeItem/")]
        public IActionResult UpdateRecipeItem(int Id, long RecipeItemId, string name, double amount, string unit)
        {
            Recipe model = _context.Recipes
                .Include(r => r.RecipeItems)
                .Include(r => r.RecipeSteps)
                .Where(r => r.RecipeId == Id).First();

            RecipeItem item = model.RecipeItems.Where(i => i.Id == RecipeItemId).First();

            item.Name = name;
            item.Amount = amount;
            item.Unit = unit;

            _context.SaveChanges();

            return Ok(model.RecipeId);
        }

        [HttpPost]
        [Route("Recipe/UpdateRecipeStep/")]
        public IActionResult UpdateRecipeStep(int Id, long RecipeStepId, string name, int number, string text)
        {
            Recipe model = _context.Recipes
                .Include(r => r.RecipeItems)
                .Include(r => r.RecipeSteps)
                .Where(r => r.RecipeId == Id).First();

            RecipeStep item = model.RecipeSteps.Where(i => i.RecipeStepId == RecipeStepId).First();

            item.Name = name;
            item.StepNumber = number;
            item.Text = text;

            _context.SaveChanges();

            return Ok(model.RecipeId);
        }

        [HttpPost]
        [Route("Recipe/GetRecipeDetail")]
        public IActionResult GetRecipeDetail(int Id)
        {
            Recipe detail = _context.Recipes
                .Include(r => r.RecipeItems)
                .Include(r => r.RecipeSteps)
                .Where(r => r.RecipeId == Id).First();

            return View("RecipeDetailPartial", detail);
        }
    }
}