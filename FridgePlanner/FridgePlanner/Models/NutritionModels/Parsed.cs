namespace FridgePlanner.Models.NutritionModels{ 

    public class Parsed    {
        public double quantity { get; set; } 
        public string measure { get; set; } 
        public string foodMatch { get; set; } 
        public string food { get; set; } 
        public string foodId { get; set; } 
        public double weight { get; set; } 
        public double retainedWeight { get; set; } 
        public Nutrients nutrients { get; set; } 
        public string measureURI { get; set; } 
        public string status { get; set; } 
    }

}