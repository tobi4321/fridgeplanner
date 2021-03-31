using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Repository
{
    public class ShoppingItemRepository : Repository<Data.ShoppingItem, RepoDataBaseContext>
    {
        public ShoppingItemRepository(RepoDataBaseContext context) : base(context)
        {

        }
        // here are specific methods for the FridgeItem Repo
    }
}
