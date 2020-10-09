using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FridgePlanner.Models;
using Microsoft.AspNetCore.Mvc;

namespace FridgePlanner.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly DataBaseContext _context;

        public ShoppingController(DataBaseContext con)
        {
            _context = con;
        }
        public IActionResult Index()
        {
            List<ShoppingListItem> items = _context.ShoppingListItems.ToList();

            return View(items);
        }
    }
}