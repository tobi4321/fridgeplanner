using FridgePlanner.Controllers;
using FridgePlanner.Models;
using FridgePlanner.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FridgePlannerTesting
{
    public class ShoppingControllerTest
    {
        private ShoppingController controller { get; }
        DataBaseContext context { get; }
        ShoppingListItem item { get; }

        public ShoppingControllerTest()
        {
            // configure DB-Options ---> A Test db on your local DB will be used
            var optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>().UseSqlServer(@"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=FridgePlannerTesting2;  Integrated Security=True");

            // create the context object to ensure the db operations go through
            context = new DataBaseContext(optionsBuilder.Options);

            // Ensure that the DB is deleted and the newly created, so there is no leftover gibberish in the DB
            context.Database.EnsureDeleted();

            context.Database.EnsureCreated();

            // create a FridgeItem for testing purposes
            item = new ShoppingListItem()
            {
                Name = "Tomate",
                Amount = 1,
                Unit = "Kg"
            };

            if (!context.FridgeItems.Any())
            {
                context.ShoppingListItems.Add(item);
            }

            context.SaveChanges();

            controller = new ShoppingController(context);

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
            string qrCodeText = "ShoppingListe \n";
            foreach (ShoppingListItem item in context.ShoppingListItems) { qrCodeText += "- " + item.Name + "   " + item.Amount + item.Unit + " \n"; }

            byte[] qrCode = QRGenerator.GenerateQR(qrCodeText);

            // Act
            var result = controller.Index();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            var model = viewResult.Model as ShoppingViewModel;

            Assert.NotNull(model.ShoppingItems);
            Assert.NotNull(model.QrCodeData);
            Assert.Contains(item, model.ShoppingItems);
            Assert.Equal(qrCode,model.QrCodeData);
        }
    }
}
