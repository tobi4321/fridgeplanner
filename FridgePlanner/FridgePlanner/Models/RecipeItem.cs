using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Models
{
    public class RecipeItem
    {
        public int RecipeItemId {get; set;}
        public string Name { get; set; }
        public int Amount { get; set; }
        public string Unit { get; set; }
    }
}
