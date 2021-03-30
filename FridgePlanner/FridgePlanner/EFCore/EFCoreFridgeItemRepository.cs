using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.EFCore
{
    public class EFCoreFridgeItemRepository : EFCoreRepository<Data.FridgeItem,EFCoreDataBaseContext>
    {
        public EFCoreFridgeItemRepository(EFCoreDataBaseContext context) : base(context)
        {

        }
        // here are specific methods for the FridgeItem Repo
    }
}
