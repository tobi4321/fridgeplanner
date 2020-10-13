using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Models.ViewModels
{
    public class IndexViewModel
    {
        public List<FridgeItem> FridgeItems { get; set; }
        public List<Recipe> Recipes { get; set; }
        public List<string> Units { get; set; }
    }
}
