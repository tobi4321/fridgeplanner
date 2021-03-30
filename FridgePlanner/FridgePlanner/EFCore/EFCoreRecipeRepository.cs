using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.EFCore
{
    public class EFCoreRecipeRepository : EFCoreRepository<Data.Recipe, EFCoreDataBaseContext>
    {
        public EFCoreRecipeRepository(EFCoreDataBaseContext context) : base(context)
        {

        }
        // here are specific methods for the FridgeItem Repo
    }
}
