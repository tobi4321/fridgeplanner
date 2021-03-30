using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.EFCore
{
    public class EFCoreRecipeItemRepository : EFCoreRepository<Data.RecipeItem, EFCoreDataBaseContext>
    {
        public EFCoreRecipeItemRepository(EFCoreDataBaseContext context) : base(context)
        {

        }
        // here are specific methods for the FridgeItem Repo
    }
}
