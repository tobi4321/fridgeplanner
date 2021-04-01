using FridgePlanner.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Repository
{
    public class RecipeItemRepository : Repository<Data.RecipeItem, RepoDataBaseContext>
    {
        private readonly RepoDataBaseContext _context;
        public RecipeItemRepository(RepoDataBaseContext context) : base(context)
        {
            _context = context;
        }

        // here are specific methods for the FridgeItem Repo
    }
}
