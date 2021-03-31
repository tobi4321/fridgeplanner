using FridgePlanner.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepoDataBaseContext _repoContext;
        private RecipeRepository _recipes;
        private RecipeItemRepository _items;
        private RecipeStepRepository _steps;

        public RecipeRepository Recipes
        {
            get {
                if (_recipes == null)
                {
                    _recipes = new RecipeRepository(_repoContext);
                }

                return _recipes;
            }
        }

        public RecipeItemRepository Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new RecipeItemRepository(_repoContext);
                }

                return _items;
            }
        }

        public RecipeStepRepository Steps
        {
            get
            {
                if (_steps == null)
                {
                    _steps = new RecipeStepRepository(_repoContext);
                }

                return _steps;
            }
        }

        public RepositoryWrapper(RepoDataBaseContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public void save()
        {
            _repoContext.SaveChanges();
        }
    }
}
