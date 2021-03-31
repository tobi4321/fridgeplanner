using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Repository
{
    public class RecipeRepository : Repository<Data.Recipe, RepoDataBaseContext>
    {
        public RecipeRepository(RepoDataBaseContext context) : base(context)
        {

        }
        // here are specific methods for the FridgeItem Repo
    }
}
