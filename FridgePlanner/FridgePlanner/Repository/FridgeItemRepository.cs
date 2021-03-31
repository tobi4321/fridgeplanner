using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Repository
{
    public class FridgeItemRepository : Repository<Data.FridgeItem,RepoDataBaseContext>
    {
        public FridgeItemRepository(RepoDataBaseContext context) : base(context)
        {

        }
        // here are specific methods for the FridgeItem Repo
    }
}
