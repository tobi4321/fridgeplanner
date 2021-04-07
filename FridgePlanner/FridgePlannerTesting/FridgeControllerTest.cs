using FridgePlanner.Controllers;
using FridgePlanner.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FridgePlanner.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace FridgePlannerTesting
{
    public class FridgeControllerTest
    {
        // Instance of the specific controller we want to test
        private FridgeController controller { get; set; }

        public FridgeControllerTest()
        {
        }

        [Fact]
        public void Index_ReturnsAViewResultWithViewModel()
        {
            // Capture
            // setup the mock of IConfiguration
            var _configuration = CreateAppSettingsMock();

            //Create In Memory Database instead of using the system database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "HomeDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                // remove all data to start with a clean database instance
                CleanDataBase(context);
                // add required data
                context.FridgeItems.Add(new FridgeItem()
                {
                    Id = 120,
                    Name = "Tomaten",
                    Amount = 1,
                    Unit = "Kg",
                    ExpiryDate = new System.DateTime(2020, 10, 15)
                });
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
                controller = new FridgeController(context);
                // Act
                var result = controller.Index(_configuration.Object);


                // Assert
                Assert.NotNull(result);
                Assert.IsType<ViewResult>(result);

                var viewResult = result as ViewResult;
                var model = viewResult.Model as IndexViewModel;

                Assert.NotNull(model.FridgeItems);
                Assert.NotNull(model.Recipes);
                Assert.Contains(context.FridgeItems.First(), model.FridgeItems);
                Assert.Contains(context.Recipes.First(), model.Recipes);
            }
        }

        [Fact]
        public void AddItem_ReturnsAViewWithAddedItems()
        {

            //Create In Memory Database instead of using the system database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "HomeDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                // remove all data to start with a clean database instance
                CleanDataBase(context);

                controller = new FridgeController(context);

                FridgeItem testItem = new FridgeItem()
                {
                    Name = "Tomaten",
                    Amount = 1,
                    Unit = "Kg",
                    ExpiryDate = new System.DateTime(2020, 10, 15)
                };

                // Act
                var result = controller.AddFridgeItem(JObject.FromObject(testItem));


                // Assert
                Assert.NotNull(result);
                Assert.IsType<ViewResult>(result);

                var viewResult = result as ViewResult;
                var model = viewResult.Model as List<FridgeItem>;

                Assert.NotNull(model);
                Assert.Contains(model.ElementAt(0),context.FridgeItems.ToList());
            }
        }
        [Fact]
        public void DeleteFridgeItem_ReturnsAViewWithEmptyModelList()
        {
            //Create In Memory Database instead of using the system database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "HomeDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                // remove all data to start with a clean database instance
                CleanDataBase(context);
                // add required data
                context.FridgeItems.Add(new FridgeItem()
                {
                    Id = 120,
                    Name = "Tomaten",
                    Amount = 1,
                    Unit = "Kg",
                    ExpiryDate = new System.DateTime(2020, 10, 15)
                });
                context.SaveChanges();
            }

            using (var context = new DataBaseContext(options))
            {
                controller = new FridgeController(context);
                // Act
                var result = controller.DeleteFridgeItem(120);


                // Assert
                Assert.NotNull(result);
                Assert.IsType<ViewResult>(result);

                var viewResult = result as ViewResult;
                var model = viewResult.Model as List<FridgeItem>;

                Assert.Empty(model);
            }
        }

        [Fact]
        public async void GetItems_ReturnsAListWithFridgeItemsAsync()
        {
            //Create In Memory Database instead of using the system database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "HomeDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                // remove all data to start with a clean database instance
                CleanDataBase(context);
                // add required data
                context.FridgeItems.Add(new FridgeItem()
                {
                    Id = 120,
                    Name = "Tomaten",
                    Amount = 1,
                    Unit = "Kg",
                    ExpiryDate = new System.DateTime(2020, 10, 15)
                });
                context.SaveChanges();
            }

            using (var context = new DataBaseContext(options))
            {
                controller = new FridgeController(context);
                // Act
                var result = await controller.GetItems();


                // Assert
                Assert.NotNull(result);

                var viewResult = result as ActionResult<IEnumerable<FridgeItem>>;
                var model = viewResult.Value as List<FridgeItem>;

                Assert.NotNull(model);
                Assert.Contains(context.FridgeItems.First(), model);
            }
        }
        [Fact]
        public void GetEditFridgeModal_ReturnsAViewResultWithEditFridgeViewModel()
        {
            // Capture
            // setup the mock of IConfiguration
            var _configuration = CreateAppSettingsMock();

            //Create In Memory Database instead of using the system database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "HomeDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                // remove all data to start with a clean database instance
                CleanDataBase(context);
                // add required data
                context.FridgeItems.Add(new FridgeItem()
                {
                    Id = 120,
                    Name = "Tomaten",
                    Amount = 1,
                    Unit = "Kg",
                    ExpiryDate = new System.DateTime(2020, 10, 15)
                });
                context.SaveChanges();
            }

            using (var context = new DataBaseContext(options))
            {
                controller = new FridgeController(context);
                // Act
                var result = controller.GetEditFridgeModal(_configuration.Object,120);


                // Assert
                Assert.NotNull(result);
                Assert.IsType<ViewResult>(result);

                var viewResult = result as ViewResult;
                var model = viewResult.Model as EditFridgeViewModel;

                Assert.NotNull(model.Item);
                Assert.NotNull(model.Units);
                Assert.Equal(context.FridgeItems.First().Name, model.Item.Name);
            }
        }

        [Fact]
        public void UpdateFridgeItem_ReturnsAViewResultWithUpdatedValues()
        {
            //Create In Memory Database instead of using the system database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "HomeDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                // remove all data to start with a clean database instance
                CleanDataBase(context);
                // add required data
                context.FridgeItems.Add(new FridgeItem()
                {
                    Id = 120,
                    Name = "Tomaten",
                    Amount = 1,
                    Unit = "Kg",
                    ExpiryDate = new System.DateTime(2020, 10, 15)
                });
                context.SaveChanges();
            }

            using (var context = new DataBaseContext(options))
            {
                controller = new FridgeController(context);
                // Act
                var result = controller.UpdateFridgeItem(120,"Tomaten",2,"Kg", new System.DateTime(2020, 10, 15));


                // Assert
                Assert.NotNull(result);
                Assert.IsType<ViewResult>(result);

                var viewResult = result as ViewResult;
                var model = viewResult.Model as List<FridgeItem>;

                Assert.NotNull(model);
                Assert.Equal( 2.0, context.FridgeItems.First().Amount);
            }
        }

        [Fact]
        public void GetRecipeDetail_ReturnsAViewResultWithARecipe()
        {

            //Create In Memory Database instead of using the system database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "HomeDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                // remove all data to start with a clean database instance
                CleanDataBase(context);
                // add required data
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
                controller = new FridgeController(context);
                // Act
                var result = controller.GetRecipeDetail(100);


                // Assert
                Assert.NotNull(result);
                Assert.IsType<ViewResult>(result);

                var viewResult = result as ViewResult;
                var model = viewResult.Model as Recipe;

                Assert.NotNull(model);
                Assert.Equal(context.Recipes.First().Name, model.Name);
                Assert.NotNull(model.RecipeItems);
                Assert.Empty(model.RecipeSteps);
            }
        }
        // This Method should clean up the database context by removing existing data from previous tests within this class
        private void CleanDataBase(DataBaseContext context)
        {
            context.FridgeItems.RemoveRange(context.FridgeItems.ToList());
            context.RecipeItems.RemoveRange(context.RecipeItems.ToList());
            context.RecipeSteps.RemoveRange(context.RecipeSteps.ToList());
            context.Recipes.RemoveRange(context.Recipes.ToList());
        }
        // This Function creates a Mock Instance of the appsettings.json field Units
        private Mock<IConfiguration> CreateAppSettingsMock()
        {
            var _configuration = new Mock<IConfiguration>();

            var oneSectionMock = new Mock<IConfigurationSection>();
            oneSectionMock.Setup(s => s.Value).Returns("Kg");
            var twoSectionMock = new Mock<IConfigurationSection>();
            twoSectionMock.Setup(s => s.Value).Returns("g");
            var unitsSectionMock = new Mock<IConfigurationSection>();
            unitsSectionMock.Setup(s => s.GetChildren()).Returns(new List<IConfigurationSection>
                                                    { oneSectionMock.Object, twoSectionMock.Object });
            _configuration.Setup(c => c.GetSection("Units")).Returns(unitsSectionMock.Object);

            return _configuration;
        }
    }
}
