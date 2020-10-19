using FridgePlanner.Controllers;
using FridgePlanner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FridgePlannerTesting
{
    public class RecipeControllerTest
    {
        private RecipeController controller { get; set; }


        public RecipeControllerTest()
        {

        }
        [Fact]
        public void Index_ReturnsAViewResult()
        {
            // Capture

            //create In Memory Database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "RecipeDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                context.Recipes.Add(new Recipe()
                {
                    RecipeId = 100,
                    Name = "TestRezept",
                    Description = "Ein einfaches Test Rezept",
                    RecipeItems = new List<RecipeItem>
                    {
                        new RecipeItem(){
                            Id = 1100,
                            Name = "Tomaten",
                            Amount = 0.5,
                            Unit = "Kg"
                        }
                    }
                });
                context.SaveChanges();
            }

            using (var context = new DataBaseContext(options))
            {
                controller = new RecipeController(context);
                // Act
                var result = controller.Index();


                // Assert
                Assert.NotNull(result);
                Assert.IsType<ViewResult>(result);

                var viewResult = result as ViewResult;
                var model = viewResult.Model as List<Recipe>;

                Assert.NotNull(model);
                Assert.Contains(context.Recipes.First(), model);
            }
        }
    }
}
