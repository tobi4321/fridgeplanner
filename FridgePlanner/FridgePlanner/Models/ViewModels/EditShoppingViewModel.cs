using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Models.ViewModels
{
    public class EditShoppingViewModel
    {
        public ShoppingItem Item { get; set; }
        public List<string> Units { get; set; }
    }
}
