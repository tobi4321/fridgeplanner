using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Repository
{
    public interface IRepositoryWrapper
    {
        RecipeRepository Recipes { get; }
        RecipeItemRepository Items { get; }
        RecipeStepRepository Steps { get; }
        void save();
    }
}
