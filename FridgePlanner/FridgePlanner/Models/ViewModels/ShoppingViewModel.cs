using FridgePlanner.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Models.ViewModels
{
    public class ShoppingViewModel
    {
        public List<ShoppingItem> ShoppingItems { get; set; }
        public List<string> Units { get; set; }
        public byte[] QrCodeData { get; set; }
    }
}
