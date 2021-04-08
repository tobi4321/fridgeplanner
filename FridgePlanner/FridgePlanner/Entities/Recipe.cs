using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Entities
{
    public class Recipe : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public List<RecipeItem> RecipeItems { get; set; }
        public List<RecipeStep> RecipeSteps { get; set; }
    }
}
