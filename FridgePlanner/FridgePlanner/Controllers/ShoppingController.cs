using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IApiCaller _client;

        public ShoppingController(IApiCaller caller)
        {
            _client = caller;
        }
        public async Task<IActionResult> Index([FromServices] IConfiguration config)
        {
            return View(await createViewModel(config));
        }



        [HttpPost]
        [Route("Shopping/AddItem")]
        public async Task<IActionResult> AddShoppingItem([FromServices] IConfiguration config, [FromBody] JObject t)
        {
            JObject response = await _client.Post("http://localhost:5000/api/ShoppingApi", t);

            ShoppingItem test = response.ToObject<ShoppingItem>();

            return View("ShoppingChangePartial", await createViewModel(config));
        }

        [HttpPost]
        [Route("Shopping/Delete")]
        public async Task<IActionResult> DeleteShoppingItem([FromServices] IConfiguration config,long Id)
        {
            JObject response = await _client.Delete("http://localhost:5000/api/ShoppingApi/" + Id);

            return View("ShoppingChangePartial", await createViewModel(config));

        }


        [HttpPost]
        [Route("Shopping/GetEditShoppingModal")]
        public async Task<IActionResult> GetEditShoppingModal([FromServices] IConfiguration config, int Id)
        {

            JObject response = await _client.GetItem("http://localhost:5000/api/ShoppingApi/" + Id);
            ShoppingItem edit = response.ToObject<ShoppingItem>();

            List<string> units = config.GetSection("Units").Get<List<string>>();

            EditShoppingViewModel model = new EditShoppingViewModel() { Item = edit, Units = units };

            return View("EditShoppingItemPartial", model);

        }
        [HttpPost]
        [Route("Shopping/UpdateShoppingItem")]
        public async Task<IActionResult> UpdateShoppingItem([FromServices] IConfiguration config, int Id, string name, double amount, string unit)
        {

            JObject response = await _client.GetItem("http://localhost:5000/api/ShoppingApi/" + Id);
            ShoppingItem edit = response.ToObject<ShoppingItem>();


            edit.Name = name;
            edit.Amount = amount;
            edit.Unit = unit;

            JObject updated = await _client.Update("http://localhost:5000/api/ShoppingApi/" + Id, JObject.FromObject(edit));

            return View("ShoppingChangePartial", await createViewModel(config));

        }

        private async Task<ShoppingViewModel> createViewModel([FromServices] IConfiguration config)
        {

            JArray response = await _client.GetList("http://localhost:5000/api/ShoppingApi");
            List<ShoppingItem> items = response.ToObject<List<ShoppingItem>>();


            byte[] qrCode = QRGenerator.GenerateQR(getShoppingListAsString(items));

            List<string> units = config.GetSection("Units").Get<List<string>>();

            ShoppingViewModel shoppingViewModel = new ShoppingViewModel { ShoppingItems = items, Units = units, QrCodeData = qrCode };

            return shoppingViewModel;
        }
        public string getShoppingListAsString(List<ShoppingItem> items)
        {
            string qrCodeText = "ShoppingListe \n";

            foreach (ShoppingItem item in items) { qrCodeText += "- " + item.Name + " " + item.Amount + item.Unit + " \n"; }

            return qrCodeText;
        }
    }
}

