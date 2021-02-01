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

namespace FridgePlanner.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataBaseContext _context;

        public HomeController(DataBaseContext con)
        {
            _context = con;
        }

        public IActionResult Index([FromServices]IConfiguration config)
        {
            return View(createViewModel(config));
        }


        [HttpPost]
        [Route("Home/AddItem")]
        public IActionResult AddItem([FromBody] JObject t)
        {
            FridgeItem x = t.ToObject<FridgeItem>();
            x.ExpiryDate = x.ExpiryDate.Date;
            _context.FridgeItems.Add(x);
            _context.SaveChanges();

            return View("FridgeTablePartial", getFridgeItems());
        }

        [HttpPost]
        [Route("Home/Delete")]
        public IActionResult DeleteFridgeItem(long Id)
        {
            FridgeItem remove = _context.FridgeItems
                .Where(t => t.Id == Id)
                .First();
            _context.FridgeItems.Remove(remove);
            _context.SaveChanges();

            return View("FridgeTablePartial", getFridgeItems());
        }

        private List<FridgeItem> getFridgeItems()
        {
            DateTime now = DateTime.Today;
            List<FridgeItem> fridgeItems = _context.FridgeItems.OrderBy(item => item.ExpiryDate.Subtract(now).TotalDays).ToList();

            return fridgeItems;
        }
        [HttpGet]
        [Route("Home/GetItems")]
        public async Task<ActionResult<IEnumerable<FridgeItem>>> GetItems()
        {
            return await _context.FridgeItems
                .ToListAsync();
        }

        [HttpPost]
        [Route("Home/GetEditFridgeModal")]
        public IActionResult GetEditFridgeModal([FromServices]IConfiguration config, int Id)
        {
            FridgeItem edit = _context.FridgeItems.Where(t => t.Id == Id).First();
            List<string> units = config.GetSection("Units").Get<List<string>>();

            EditFridgeViewModel model = new EditFridgeViewModel() { Item = edit, Units = units };

            return View("EditFridgeItemPartial", model);
        }
        [HttpPost]
        [Route("Home/UpdateFridgeItem")]
        public IActionResult UpdateFridgeItem(int Id, string name, double amount, string unit, DateTime expiry)
        {
            FridgeItem edit = _context.FridgeItems.Where(t => t.Id == Id).First();

            edit.Name = name;
            edit.Amount = amount;
            edit.Unit = unit;
            edit.ExpiryDate = expiry;
            edit.ExpiryDate = edit.ExpiryDate.Date;
            _context.SaveChanges();

            return View("FridgeTablePartial", getFridgeItems());
        }

        [HttpPost]
        [Route("Home/GetRecipeDetail")]
        public IActionResult GetRecipeDetail(int Id)
        {
            Recipe detail = _context.Recipes
                .Include(r => r.RecipeItems)
                .Include(r => r.RecipeSteps)
                .Where(r => r.RecipeId == Id).First();

            return View("RecipeHomeDetailPartial", detail);
        }


        private IndexViewModel createViewModel([FromServices]IConfiguration config)
        {
            DateTime now = DateTime.Today;
            List<FridgeItem> fridgeItems = _context.FridgeItems.OrderBy(item => item.ExpiryDate.Subtract(now).TotalDays).ToList();

            List<Recipe> recipes = _context.Recipes.Where(item => item.RecipeItems.Any(i => fridgeItems.Any(f => i.Name == f.Name))).ToList();

            List<string> units = config.GetSection("Units").Get<List<string>>();

            IndexViewModel model = new IndexViewModel { FridgeItems = fridgeItems, Recipes = recipes , Units = units};

            return model;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
