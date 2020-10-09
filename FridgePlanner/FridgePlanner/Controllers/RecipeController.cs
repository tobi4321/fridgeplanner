using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FridgePlanner.Models;
using Microsoft.AspNetCore.Mvc;

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
            List<Recipe> recipes = _context.Recipes.ToList();

            return View(recipes);
        }
    }
}