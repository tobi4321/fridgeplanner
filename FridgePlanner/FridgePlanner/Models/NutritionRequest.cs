using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Models
{
    public class NutritionRequest
    {
        public string title { get; set; }
        public List<string> ingr { get; set; }
    }
}
