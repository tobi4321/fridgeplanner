using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.EFCore
{
    public class EFCoreShoppingItemRepository : EFCoreRepository<Data.ShoppingItem, EFCoreDataBaseContext>
    {
        public EFCoreShoppingItemRepository(EFCoreDataBaseContext context) : base(context)
        {

        }
        // here are specific methods for the FridgeItem Repo
    }
}
