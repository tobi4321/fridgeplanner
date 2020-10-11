using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FridgePlanner.Models;
using FridgePlanner.Models.ViewModels;
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

            string qrCodeText = "ShoppingListe \n";
            foreach (ShoppingListItem item in items) { qrCodeText += "- " +item.Name + "   " + item.Amount + item.Unit + " \n"; }

            byte[] qrCode = QRGenerator.GenerateQR(qrCodeText);

            ShoppingViewModel shoppingViewModel = new ShoppingViewModel { ShoppingItems = items, QrCodeData = qrCode };

            return View(shoppingViewModel);
        }
    }
}