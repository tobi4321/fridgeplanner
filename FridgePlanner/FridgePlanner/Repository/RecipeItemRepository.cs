using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Repository
{
    public class RecipeItemRepository : Repository<Data.RecipeItem, RepoDataBaseContext>
    {
        public RecipeItemRepository(RepoDataBaseContext context) : base(context)
        {

        }
        // here are specific methods for the FridgeItem Repo
    }
}
