using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Models.ViewModels
{
    public class ShoppingViewModel
    {
        public List<ShoppingListItem> ShoppingItems { get; set; }
        public byte[] QrCodeData { get; set; }
    }
}
