using FridgePlanner.Models.NutritionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Models.ViewModels
{
    public class NutritionViewModel
    {
        public Recipe Recipe { get; set; }
        public NutritionAPIResponse NutritionResponse { get; set; }
    }
}
