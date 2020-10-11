﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FridgePlanner.Models;
using FridgePlanner.Models.ViewModels;
using Newtonsoft.Json.Linq;

namespace FridgePlanner.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataBaseContext _context;

        public HomeController(DataBaseContext con)
        {
            _context = con;
        }

        public IActionResult Index()
        {
            DateTime now = DateTime.Today;
            List<FridgeItem> fridgeItems = _context.FridgeItems.OrderBy(item => item.ExpiryDate.Subtract(now).TotalDays).ToList();

            List<Recipe> recipes = _context.Recipes.Where(item => item.RecipeItems.Any(i => fridgeItems.Any( f => i.Name == f.Name ))).ToList();

            IndexViewModel model = new IndexViewModel { FridgeItems = fridgeItems, Recipes = recipes };
            return View(model);
        }

        [HttpPost]
        [Route("Home/AddItem")]
        public IActionResult AddItem([FromBody] JObject t)
        {
            FridgeItem x = t.ToObject<FridgeItem>();
            x.ExpiryDate = x.ExpiryDate.Date;
            x.ExpiryDate.AddDays(1);
            _context.FridgeItems.Add(x);
            _context.SaveChanges();

            DateTime now = DateTime.Today;
            List<FridgeItem> fridgeItems = _context.FridgeItems.OrderBy(item => item.ExpiryDate.Subtract(now).TotalDays).ToList();
            return View("FridgeTablePartial",fridgeItems);
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

            DateTime now = DateTime.Today;
            List<FridgeItem> fridgeItems = _context.FridgeItems.OrderBy(item => item.ExpiryDate.Subtract(now).TotalDays).ToList();
            return View("FridgeTablePartial", fridgeItems);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
