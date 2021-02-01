using System.Collections.Generic; 
namespace FridgePlanner.Models.NutritionModels{ 

    public class Ingredient    {
        public string text { get; set; } 
        public List<Parsed> parsed { get; set; } 
    }

}