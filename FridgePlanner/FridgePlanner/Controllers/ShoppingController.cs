using System;
using System.Collections.Generic;
using System.Linq;
using FridgePlanner.Models;
using FridgePlanner.Models.NutritionModels;
using FridgePlanner.Models.ViewModels;
using FridgePlanner.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace FridgePlanner.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly DataBaseContext _context;

        public ShoppingController(DataBaseContext con)
        {
            _context = con;
        }
        public IActionResult Index([FromServices]IConfiguration config)
        {
            return View(createViewModel(config));
        }

        [HttpPost]
        [Route("Shopping/AddItem")]
        public IActionResult AddItem([FromServices]IConfiguration config,[FromBody] JObject t)
        {
            ShoppingListItem x = t.ToObject<ShoppingListItem>();
            _context.ShoppingListItems.Add(x);
            _context.SaveChanges();

            return View("ShoppingChangePartial", createViewModel(config));
        }

        [HttpPost]
        [Route("Shopping/Delete")]
        public IActionResult DeleteShoppingItem([FromServices]IConfiguration config,long Id)
        {
            ShoppingListItem remove = _context.ShoppingListItems
                .Where(t => t.Id == Id)
                .First();
            _context.ShoppingListItems.Remove(remove);
            _context.SaveChanges();

            return View("ShoppingChangePartial", createViewModel(config));
        }

        [HttpPost]
        [Route("Shopping/GetEditShoppingModal")]
        public IActionResult GetEditShoppingModal([FromServices]IConfiguration config, int Id)
        {
            ShoppingListItem edit = _context.ShoppingListItems.Where(t => t.Id == Id).First();
            List<string> units = config.GetSection("Units").Get<List<string>>();

            EditShoppingViewModel model = new EditShoppingViewModel() { Item = edit, Units = units };

            return View("EditShoppingItemPartial", model);
        }
        [HttpPost]
        [Route("Shopping/UpdateShoppingItem")]
        public IActionResult UpdateShoppingItem([FromServices]IConfiguration config,int Id, string name, double amount, string unit)
        {
            ShoppingListItem edit = _context.ShoppingListItems.Where(t => t.Id == Id).First();

            edit.Name = name;
            edit.Amount = amount;
            edit.Unit = unit;
            _context.SaveChanges();

            return View("ShoppingChangePartial", createViewModel(config));
        }

        public string getShoppingListAsString(List<ShoppingListItem> items)
        {
            string qrCodeText = "ShoppingListe \n";

            foreach (ShoppingListItem item in items) { qrCodeText += "- " + item.Name + "   " + item.Amount + item.Unit + " \n"; }

            return qrCodeText;
        }

        public ShoppingViewModel createViewModel([FromServices]IConfiguration config)
        {
            List<ShoppingListItem> items = _context.ShoppingListItems.ToList();

            //NutritionOutputForShoppingList(items);

            byte[] qrCode = QRGenerator.GenerateQR(getShoppingListAsString(items));

            List<string> units = config.GetSection("Units").Get<List<string>>();

            ShoppingViewModel shoppingViewModel = new ShoppingViewModel { ShoppingItems = items, Units = units, QrCodeData = qrCode };

            return shoppingViewModel;
        }
    }
}