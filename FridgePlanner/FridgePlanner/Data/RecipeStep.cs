using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Data
{
    public class RecipeStep :  IEntity
    {
        public int Id { get; set; }

        public string Titel { get; set; }
        public string Text { get; set; }
        public int StepNumber { get; set; }
    }
}
