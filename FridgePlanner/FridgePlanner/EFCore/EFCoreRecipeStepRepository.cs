using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.EFCore
{
    public class EFCoreRecipeStepRepository : EFCoreRepository<Data.RecipeStep, EFCoreDataBaseContext>
    {
        public EFCoreRecipeStepRepository(EFCoreDataBaseContext context) : base(context)
        {

        }
        // here are specific methods for the FridgeItem Repo
    }
}
