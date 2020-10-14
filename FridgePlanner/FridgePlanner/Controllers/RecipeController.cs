using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FridgePlanner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult AddItemOverview()
        {
            return View("AddRecipe");
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