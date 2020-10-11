using FridgePlanner.Controllers;
using FridgePlanner.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FridgePlanner.Models.ViewModels;

namespace FridgePlannerTesting
{
    public class HomeControllerTest
    {
        private HomeController controller { get; }
        DataBaseContext context { get; }
        FridgeItem item { get; }
        Recipe recipe { get; }

        public HomeControllerTest()
        {
            // configure DB-Options ---> A Test db on your local DB will be used
            var optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>().UseSqlServer(@"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=FridgePlannerTesting;  Integrated Security=True");

            // create the context object to ensure the db operations go through
            context = new DataBaseContext(optionsBuilder.Options);

            // Ensure that the DB is deleted and the newly created, so there is no leftover gibberish in the DB
            context.Database.EnsureDeleted();

            context.Database.EnsureCreated();

            // create a FridgeItem for testing purposes
            item = new FridgeItem()
            {
                Name = "Tomate",
                Amount = 1,
                Unit = "Kg",
                ExpiryDate = new System.DateTime(2020, 10, 15)
            };


            // create a Recipe for testing purposes
            recipe = new Recipe()
            {
                Name = "TestRezept",
                Description = "Ein einfaches Test Rezept"
            };

            if (!context.FridgeItems.Any())
            {
                context.FridgeItems.Add(item);
            }

            if (!context.Recipes.Any())
            {
                context.Recipes.Add(recipe);
            }

            context.SaveChanges();

            controller = new HomeController(context);

        }

        [Fact]
        public void Index_ReturnsAViewResult()
        {
            // Arrange
            //RecipeItem recipeItem = new RecipeItem()
            //{
            //    Name = "Tomate",
            //    Amount = 1,
            //    Unit = "Kg"
            //};
            //var recipeId = context.Recipes.First().RecipeId;
            //context.Recipes.Where(r => r.RecipeId == recipeId).First().RecipeItems.Add(recipeItem);
            //context.SaveChanges();


            // Act
            var result = controller.Index();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            var model = viewResult.Model as IndexViewModel;

            Assert.NotNull(model.FridgeItems);
            //Assert.NotNull(model.Recipes);
            Assert.Contains(item,model.FridgeItems); 
            //Assert.Contains(recipe,model.Recipes);

        }
    }
}
