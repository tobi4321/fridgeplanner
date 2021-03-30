using FridgePlanner.Controllers;
using FridgePlanner.Models;
using FridgePlanner.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json.Linq;
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
                context.RecipeItems.RemoveRange(context.RecipeItems.ToList());
                context.RecipeSteps.RemoveRange(context.RecipeSteps.ToList());
                context.Recipes.RemoveRange(context.Recipes.ToList());

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

        [Fact]
        public void AddRecipe_ReturnsAnOkResultWithId()
        {
            // Capture

            //create In Memory Database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "RecipeDataBase")
            .Options;



            using (var context = new DataBaseContext(options))
            {
                context.Recipes.RemoveRange(context.Recipes.ToList());
                Recipe testItem = new Recipe()
                {
                    Name = "TestRezept",
                    Description = "Ein einfaches Test Rezept",
                };


                controller = new RecipeController(context);
                // Act
                var result = controller.AddRecipe(JObject.FromObject(testItem));


                // Assert
                Assert.NotNull(result);
                Assert.IsType<OkObjectResult>(result);

                var objectResult = result as OkObjectResult;
                var id = objectResult.Value;

                Assert.NotNull(id);
                Assert.Equal(context.Recipes.First().RecipeId, id);
            }
        }

        [Fact]
        public void EditRecipeOverview_ReturnsEditRecipeView()
        {
            // Capture
            var _configuration = new Mock<IConfiguration>();

            var oneSectionMock = new Mock<IConfigurationSection>();
            oneSectionMock.Setup(s => s.Value).Returns("Kg");
            var twoSectionMock = new Mock<IConfigurationSection>();
            twoSectionMock.Setup(s => s.Value).Returns("g");
            var unitsSectionMock = new Mock<IConfigurationSection>();
            unitsSectionMock.Setup(s => s.GetChildren()).Returns(new List<IConfigurationSection>
                                                    { oneSectionMock.Object, twoSectionMock.Object });
            _configuration.Setup(c => c.GetSection("Units")).Returns(unitsSectionMock.Object);

            //create In Memory Database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "RecipeDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                context.Recipes.RemoveRange(context.Recipes.ToList());

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
                var result = controller.EditRecipeOverview(_configuration.Object,100);


                // Assert
                Assert.NotNull(result);
                Assert.IsType<ViewResult>(result);

                var viewResult = result as ViewResult;
                var model = viewResult.Model as EditRecipeViewModel;

                Assert.NotNull(model);
                Assert.Equal(context.Recipes.First().RecipeId, model.RecipeElement.RecipeId);
            }
        }

        [Fact]
        public void EditRecipe_ReturnsAnOkResultWithRecipeId()
        {
            // Capture

            //create In Memory Database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "RecipeDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                context.RecipeItems.RemoveRange(context.RecipeItems.ToList());
                context.Recipes.RemoveRange(context.Recipes.ToList());

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
                var result = controller.EditRecipe(100,"ChangedTestRezept","Ein einfaches Test Rezept");


                // Assert
                Assert.NotNull(result);
                Assert.IsType<OkObjectResult>(result);

                var objectResult = result as OkObjectResult;
                var id = objectResult.Value;

                Assert.NotNull(id);
                Assert.Equal(context.Recipes.First().RecipeId, id);
                Assert.Equal("ChangedTestRezept", context.Recipes.First().Name);
            }
        }

        [Fact]
        public void AddRecipeItem_ReturnsAnOkResultWithRecipeId()
        {
            // Capture
            var _configuration = new Mock<IConfiguration>();

            var oneSectionMock = new Mock<IConfigurationSection>();
            oneSectionMock.Setup(s => s.Value).Returns("Kg");
            var twoSectionMock = new Mock<IConfigurationSection>();
            twoSectionMock.Setup(s => s.Value).Returns("g");
            var unitsSectionMock = new Mock<IConfigurationSection>();
            unitsSectionMock.Setup(s => s.GetChildren()).Returns(new List<IConfigurationSection>
                                                    { oneSectionMock.Object, twoSectionMock.Object });
            _configuration.Setup(c => c.GetSection("Units")).Returns(unitsSectionMock.Object);

            //create In Memory Database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "RecipeDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                context.RecipeItems.RemoveRange(context.RecipeItems.ToList());
                context.Recipes.RemoveRange(context.Recipes.ToList());

                context.Recipes.Add(new Recipe()
                {
                    RecipeId = 100,
                    Name = "TestRezept",
                    Description = "Ein einfaches Test Rezept"
                });
                context.SaveChanges();
            }

            using (var context = new DataBaseContext(options))
            {
                RecipeItem testItem = new RecipeItem()
                {
                    Name = "Tomate",
                    Amount = 1,
                    Unit = "kg"
                };


                controller = new RecipeController(context);
                // Act
                var result = controller.AddRecipeItem(_configuration.Object,100,JObject.FromObject(testItem));


                // Assert
                Assert.NotNull(result);
                Assert.IsType<OkObjectResult>(result);

                var objectResult = result as OkObjectResult;
                var id = objectResult.Value;

                Assert.NotNull(id);
                Assert.Equal(context.Recipes.First().RecipeId, id);
            }
        }

        [Fact]
        public void AddRecipeStep_ReturnsAnOkResultWithRecipeId()
        {
            // Capture
            var _configuration = new Mock<IConfiguration>();

            var oneSectionMock = new Mock<IConfigurationSection>();
            oneSectionMock.Setup(s => s.Value).Returns("Kg");
            var twoSectionMock = new Mock<IConfigurationSection>();
            twoSectionMock.Setup(s => s.Value).Returns("g");
            var unitsSectionMock = new Mock<IConfigurationSection>();
            unitsSectionMock.Setup(s => s.GetChildren()).Returns(new List<IConfigurationSection>
                                                    { oneSectionMock.Object, twoSectionMock.Object });
            _configuration.Setup(c => c.GetSection("Units")).Returns(unitsSectionMock.Object);

            //create In Memory Database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "RecipeDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                context.RecipeSteps.RemoveRange(context.RecipeSteps.ToList());
                context.RecipeItems.RemoveRange(context.RecipeItems.ToList());
                context.Recipes.RemoveRange(context.Recipes.ToList());

                context.Recipes.Add(new Recipe()
                {
                    RecipeId = 100,
                    Name = "TestRezept",
                    Description = "Ein einfaches Test Rezept"
                });
                context.SaveChanges();
            }

            using (var context = new DataBaseContext(options))
            {
                RecipeStep testItem = new RecipeStep()
                {
                    Name = "Tomaten pürieren",
                    StepNumber = 1,
                    Text = "Jetzt die Tomaten pürieren..."
                };


                controller = new RecipeController(context);
                // Act
                var result = controller.AddRecipeStep(_configuration.Object, 100, JObject.FromObject(testItem));


                // Assert
                Assert.NotNull(result);
                Assert.IsType<OkObjectResult>(result);

                var objectResult = result as OkObjectResult;
                var id = objectResult.Value;

                Assert.NotNull(id);
                Assert.Equal(context.Recipes.First().RecipeId, id);
            }
        }

        [Fact]
        public void DeleteRecipeItem_ReturnsOkResultWithRecipeId()
        {
            //create In Memory Database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "RecipeDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                context.RecipeSteps.RemoveRange(context.RecipeSteps.ToList());
                context.RecipeItems.RemoveRange(context.RecipeItems.ToList());
                context.Recipes.RemoveRange(context.Recipes.ToList());

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
                var result = controller.DeleteRecipeItem(100, 1100);


                // Assert
                Assert.NotNull(result);
                Assert.IsType<OkObjectResult>(result);

                var objectResult = result as OkObjectResult;
                var id = objectResult.Value;

                Assert.NotNull(id);
                Assert.Equal(context.Recipes.First().RecipeId, id);
                Assert.Empty(context.RecipeItems.ToList());
            }
        }

        [Fact]
        public void DeleteRecipeStep_ReturnsOkResultWithRecipeId()
        {
            //create In Memory Database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "RecipeDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                context.RecipeSteps.RemoveRange(context.RecipeSteps.ToList());
                context.RecipeItems.RemoveRange(context.RecipeItems.ToList());
                context.Recipes.RemoveRange(context.Recipes.ToList());

                context.Recipes.Add(new Recipe()
                {
                    RecipeId = 100,
                    Name = "TestRezept",
                    Description = "Ein einfaches Test Rezept",
                    RecipeSteps = new List<RecipeStep>
                    {
                        new RecipeStep(){
                            RecipeStepId = 1100,
                            Name = "Tomaten pürieren",
                            Text = "Jetzt die Tomaten pürieren..."
                        }
                    }
                });
                context.SaveChanges();
            }

            using (var context = new DataBaseContext(options))
            {

                controller = new RecipeController(context);
                // Act
                var result = controller.DeleteRecipeStep(100, 1100);


                // Assert
                Assert.NotNull(result);
                Assert.IsType<OkObjectResult>(result);

                var objectResult = result as OkObjectResult;
                var id = objectResult.Value;

                Assert.NotNull(id);
                Assert.Equal(context.Recipes.First().RecipeId, id);
                Assert.Empty(context.RecipeSteps.ToList());
            }
        }

        [Fact]
        public void UpdateRecipeItem_ReturnsOkResultWithRecipeId()
        {
            //create In Memory Database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "RecipeDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                context.RecipeSteps.RemoveRange(context.RecipeSteps.ToList());
                context.RecipeItems.RemoveRange(context.RecipeItems.ToList());
                context.Recipes.RemoveRange(context.Recipes.ToList());

                context.Recipes.Add(new Recipe()
                {
                    RecipeId = 100,
                    Name = "TestRezept",
                    Description = "Ein einfaches Test Rezept",
                    RecipeItems = new List<RecipeItem>
                    {
                        new RecipeItem(){
                            Id = 1100,
                            Name = "Tomate",
                            Amount = 1.0,
                            Unit = "kg"
                        }
                    }
                });
                context.SaveChanges();
            }

            using (var context = new DataBaseContext(options))
            {

                controller = new RecipeController(context);
                // Act
                var result = controller.UpdateRecipeItem(100, 1100,"Tomate",2.0,"kg");


                // Assert
                Assert.NotNull(result);
                Assert.IsType<OkObjectResult>(result);

                var objectResult = result as OkObjectResult;
                var id = objectResult.Value;

                Assert.NotNull(id);
                Assert.Equal(context.Recipes.First().RecipeId, id);
                Assert.Equal(2.0,context.Recipes.First().RecipeItems.First().Amount);
            }
        }

        [Fact]
        public void UpdateRecipeStep_ReturnsOkResultWithRecipeId()
        {
            //create In Memory Database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "RecipeDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                context.RecipeSteps.RemoveRange(context.RecipeSteps.ToList());
                context.RecipeItems.RemoveRange(context.RecipeItems.ToList());
                context.Recipes.RemoveRange(context.Recipes.ToList());

                context.Recipes.Add(new Recipe()
                {
                    RecipeId = 100,
                    Name = "TestRezept",
                    Description = "Ein einfaches Test Rezept",
                    RecipeSteps = new List<RecipeStep>
                    {
                        new RecipeStep(){
                            RecipeStepId = 1100,
                            Name = "Tomate hacken",
                            StepNumber = 1,
                            Text = "Tomaten hacken und so"
                        }
                    }
                });
                context.SaveChanges();
            }

            using (var context = new DataBaseContext(options))
            {

                controller = new RecipeController(context);
                // Act
                var result = controller.UpdateRecipeStep(100, 1100, "Tomate zerhacken",2,"Tomaten hacken und so");


                // Assert
                Assert.NotNull(result);
                Assert.IsType<OkObjectResult>(result);

                var objectResult = result as OkObjectResult;
                var id = objectResult.Value;

                Assert.NotNull(id);
                Assert.Equal(context.Recipes.First().RecipeId, id);
                Assert.Equal(2, context.Recipes.First().RecipeSteps.First().StepNumber);
            }
        }

        [Fact]
        public void GetRecipeDetail_ReturnsAViewResultWithRecipe()
        {
            //create In Memory Database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "RecipeDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                context.RecipeSteps.RemoveRange(context.RecipeSteps.ToList());
                context.RecipeItems.RemoveRange(context.RecipeItems.ToList());
                context.Recipes.RemoveRange(context.Recipes.ToList());

                context.Recipes.Add(new Recipe()
                {
                    RecipeId = 100,
                    Name = "TestRezept",
                    Description = "Ein einfaches Test Rezept",
                    RecipeSteps = new List<RecipeStep>
                    {
                        new RecipeStep(){
                            RecipeStepId = 1100,
                            Name = "Tomate hacken",
                            StepNumber = 1,
                            Text = "Tomaten hacken und so"
                        }
                    }
                });
                context.SaveChanges();
            }

            using (var context = new DataBaseContext(options))
            {

                controller = new RecipeController(context);
                // Act
                var result = controller.GetRecipeDetail(100);


                // Assert
                Assert.NotNull(result);
                Assert.IsType<ViewResult>(result);

                var viewResult = result as ViewResult;
                var model = viewResult.Model as Recipe;

                Assert.NotNull(model);
                Assert.Equal("TestRezept", model.Name);
            }
        }

        [Fact]
        public void AddToCard_ReturnsOkResult()
        {
            //create In Memory Database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "RecipeDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {

                context.ShoppingItems.RemoveRange(context.ShoppingItems.ToList());

                context.RecipeSteps.RemoveRange(context.RecipeSteps.ToList());
                context.RecipeItems.RemoveRange(context.RecipeItems.ToList());
                context.Recipes.RemoveRange(context.Recipes.ToList());

                context.Recipes.Add(new Recipe()
                {
                    RecipeId = 100,
                    Name = "TestRezept",
                    Description = "Ein einfaches Test Rezept",
                    RecipeSteps = new List<RecipeStep>
                    {
                        new RecipeStep(){
                            RecipeStepId = 1100,
                            Name = "Tomate hacken",
                            StepNumber = 1,
                            Text = "Tomaten hacken und so"
                        }
                    },
                    RecipeItems = new List<RecipeItem>
                    {
                        new RecipeItem()
                        {
                            Id = 110,
                            Name = "Tomate",
                            Amount = 1.0,
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
                var result = controller.AddToCart(100);


                // Assert
                Assert.NotNull(result);
                Assert.IsType<OkResult>(result);

                Assert.Equal("Tomate", context.ShoppingItems.First().Name);
                Assert.Equal(1.0, context.ShoppingItems.First().Amount);
            }
        }
        [Fact]
        public void AddToCard_ReturnsOkResult_AddingExistingItemWithDifferentUnit()
        {
            //create In Memory Database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "RecipeDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {

                context.ShoppingItems.RemoveRange(context.ShoppingItems.ToList());

                context.RecipeSteps.RemoveRange(context.RecipeSteps.ToList());
                context.RecipeItems.RemoveRange(context.RecipeItems.ToList());
                context.Recipes.RemoveRange(context.Recipes.ToList());

                context.ShoppingItems.Add(new ShoppingItem()
                {
                    Id = 123,
                    Name = "Tomate",
                    Amount = 1000.0,
                    Unit = "g"
                });

                context.Recipes.Add(new Recipe()
                {
                    RecipeId = 100,
                    Name = "TestRezept",
                    Description = "Ein einfaches Test Rezept",
                    RecipeSteps = new List<RecipeStep>
                    {
                        new RecipeStep(){
                            RecipeStepId = 1100,
                            Name = "Tomate hacken",
                            StepNumber = 1,
                            Text = "Tomaten hacken und so"
                        }
                    },
                    RecipeItems = new List<RecipeItem>
                    {
                        new RecipeItem()
                        {
                            Id = 110,
                            Name = "Tomate",
                            Amount = 1.0,
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
                var result = controller.AddToCart(100);


                // Assert
                Assert.NotNull(result);
                Assert.IsType<OkResult>(result);

                Assert.Equal("Tomate", context.ShoppingItems.First().Name);
                Assert.Equal(2000.0, context.ShoppingItems.First().Amount);
            }
        }

        [Fact]
        public void AddToCard_ReturnsOkResult_AddingExistingItemWithEqualUnit()
        {
            //create In Memory Database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "RecipeDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {

                context.ShoppingItems.RemoveRange(context.ShoppingItems.ToList());

                context.RecipeSteps.RemoveRange(context.RecipeSteps.ToList());
                context.RecipeItems.RemoveRange(context.RecipeItems.ToList());
                context.Recipes.RemoveRange(context.Recipes.ToList());

                context.ShoppingItems.Add(new ShoppingItem()
                {
                    Id = 123,
                    Name = "Tomate",
                    Amount = 3.0,
                    Unit = "Kg"
                });

                context.Recipes.Add(new Recipe()
                {
                    RecipeId = 100,
                    Name = "TestRezept",
                    Description = "Ein einfaches Test Rezept",
                    RecipeSteps = new List<RecipeStep>
                    {
                        new RecipeStep(){
                            RecipeStepId = 1100,
                            Name = "Tomate hacken",
                            StepNumber = 1,
                            Text = "Tomaten hacken und so"
                        }
                    },
                    RecipeItems = new List<RecipeItem>
                    {
                        new RecipeItem()
                        {
                            Id = 110,
                            Name = "Tomate",
                            Amount = 1.0,
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
                var result = controller.AddToCart(100);


                // Assert
                Assert.NotNull(result);
                Assert.IsType<OkResult>(result);

                Assert.Equal("Tomate", context.ShoppingItems.First().Name);
                Assert.Equal(4.0, context.ShoppingItems.First().Amount);
            }
        }
    }
}
