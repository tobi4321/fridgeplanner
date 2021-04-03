using FridgePlanner.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Models.ViewModels
{
    public class EditRecipeItemViewModel
    {
        public RecipeItem Item { get; set;}
        public List<string> Units { get; set; }
        public Recipe RecipeElement { get; set; }
    }
}
