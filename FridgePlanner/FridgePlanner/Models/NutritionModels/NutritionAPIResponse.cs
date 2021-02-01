using System.Collections.Generic; 
namespace FridgePlanner.Models.NutritionModels{ 

    public class NutritionAPIResponse    {
        public string uri { get; set; } 
        public double yield { get; set; } 
        public int calories { get; set; } 
        public double totalWeight { get; set; } 
        public List<string> dietLabels { get; set; } 
        public List<string> healthLabels { get; set; } 
        public List<object> cautions { get; set; } 
        public TotalNutrients totalNutrients { get; set; } 
        public TotalDaily totalDaily { get; set; } 
        public List<Ingredient> ingredients { get; set; } 
        public TotalNutrientsKCal totalNutrientsKCal { get; set; } 
    }

}