using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Models
{
    public class RecipeStep
    {
        public int RecipeStepId { get; set; }
        public int StepNumber { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
    }
}
