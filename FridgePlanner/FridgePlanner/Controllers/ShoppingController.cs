using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FridgePlanner.Models;
using FridgePlanner.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            List<ShoppingListItem> items = _context.ShoppingListItems.ToList();

            string qrCodeText = "ShoppingListe \n";
            foreach (ShoppingListItem item in items) { qrCodeText += "- " +item.Name + "   " + item.Amount + item.Unit + " \n"; }

            byte[] qrCode = QRGenerator.GenerateQR(qrCodeText);

            ShoppingViewModel shoppingViewModel = new ShoppingViewModel { ShoppingItems = items, QrCodeData = qrCode };

            return View(shoppingViewModel);
        }

        [HttpPost]
        [Route("Shopping/AddItem")]
        public IActionResult AddItem([FromBody] JObject t)
        {
            ShoppingListItem x = t.ToObject<ShoppingListItem>();
            _context.ShoppingListItems.Add(x);
            _context.SaveChanges();

            List<ShoppingListItem> items = _context.ShoppingListItems.ToList();

            string qrCodeText = "ShoppingListe \n";
            foreach (ShoppingListItem item in items) { qrCodeText += "- " + item.Name + "   " + item.Amount + item.Unit + " \n"; }

            byte[] qrCode = QRGenerator.GenerateQR(qrCodeText);

            ShoppingViewModel shoppingViewModel = new ShoppingViewModel { ShoppingItems = items, QrCodeData = qrCode };

            return View("ShoppingChangePartial",shoppingViewModel);
        }

        [HttpPost]
        [Route("Shopping/Delete")]
        public IActionResult DeleteShoppingItem(long Id)
        {
            ShoppingListItem remove = _context.ShoppingListItems
                .Where(t => t.Id == Id)
                .First();
            _context.ShoppingListItems.Remove(remove);
            _context.SaveChanges();

            List<ShoppingListItem> items = _context.ShoppingListItems.ToList();

            string qrCodeText = "ShoppingListe \n";
            foreach (ShoppingListItem item in items) { qrCodeText += "- " + item.Name + "   " + item.Amount + item.Unit + " \n"; }

            byte[] qrCode = QRGenerator.GenerateQR(qrCodeText);

            ShoppingViewModel shoppingViewModel = new ShoppingViewModel { ShoppingItems = items, QrCodeData = qrCode };

            return View("ShoppingChangePartial", shoppingViewModel);
        }
    }
}