using FridgePlanner.Controllers;
using FridgePlanner.Models;
using FridgePlanner.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
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
    public class ShoppingControllerTest
    {
        // Instance of the specific controller we want to test
        private ShoppingController controller { get; set; }

        public ShoppingControllerTest()
        {
        }

        [Fact]
        public void Index_ReturnsAViewResultWithViewModel()
        {
            // Capture
            // setup the mock of IConfiguration
            var _configuration = CreateAppSettingsMock();

            //create In Memory Database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "ShoppingDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                // remove all data to start with a clean database instance
                CleanDataBase(context);
                // add required data
                context.ShoppingItems.Add(new ShoppingItem
                {
                    Id = 2736459,
                    Name = "Tomate",
                    Amount = 1.0,
                    Unit = "Kg"
                });
                context.ShoppingItems.Add(new ShoppingItem
                {
                    Id = 2736458,
                    Name = "Milch",
                    Amount = 4.0,
                    Unit = "L"
                });
                context.SaveChanges();
            }

            using (var context = new DataBaseContext(options))
            {
                controller = new ShoppingController(context);
                // Act
                var result = controller.Index(_configuration.Object);


                // Assert
                Assert.NotNull(result);
                Assert.IsType<ViewResult>(result);

                var viewResult = result as ViewResult;
                var model = viewResult.Model as ShoppingViewModel;

                Assert.NotNull(model.ShoppingItems);
                Assert.NotNull(model.QrCodeData);
                Assert.Contains(context.ShoppingItems.First(), model.ShoppingItems);
            }
        }
        [Fact]
        public void AddItem_ReturnsAViewResultWithModel()
        {
            // Capture
            // setup the mock of IConfiguration
            var _configuration = CreateAppSettingsMock();

            //Create In Memory Database instead of using the system database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "ShoppingDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                // remove all data to start with a clean database instance
                CleanDataBase(context);
                context.SaveChanges();
            }

            using (var context = new DataBaseContext(options))
            {
                controller = new ShoppingController(context);

                ShoppingItem item = new ShoppingItem()
                {
                    Id = 1100,
                    Name = "Tomaten",
                    Amount = 1.0,
                    Unit = "Kg"
                };

                // Act
                var result = controller.AddItem(_configuration.Object, JObject.FromObject(item));


                // Assert
                Assert.NotNull(result);
                Assert.IsType<ViewResult>(result);

                var viewResult = result as ViewResult;
                var model = viewResult.Model as ShoppingViewModel;

                Assert.NotNull(model.ShoppingItems);
                Assert.NotNull(model.QrCodeData);
                Assert.Contains(context.ShoppingItems.First(), model.ShoppingItems);
            }
        }
        [Fact]
        public void DeleteShoppingItem_ReturnsAViewResultWithEmptyShoppingList()
        {
            // Capture
            // setup the mock of IConfiguration
            var _configuration = CreateAppSettingsMock();

            //Create In Memory Database instead of using the system database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "ShoppingDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                // remove all data to start with a clean database instance
                CleanDataBase(context);
                // add required data
                context.ShoppingItems.Add(new ShoppingItem
                {
                    Id = 1100,
                    Name = "Tomate",
                    Amount = 1.0,
                    Unit = "Kg"
                });
                context.SaveChanges();
            }

            using (var context = new DataBaseContext(options))
            {
                controller = new ShoppingController(context);
                // Act
                var result = controller.DeleteShoppingItem(_configuration.Object,1100);


                // Assert
                Assert.NotNull(result);
                Assert.IsType<ViewResult>(result);

                var viewResult = result as ViewResult;
                var model = viewResult.Model as ShoppingViewModel;

                Assert.Empty(model.ShoppingItems);
                Assert.NotNull(model.QrCodeData);
            }
        }
        [Fact]
        public void GetEditShoppingModal_ReturnsAViewResultWithEditShoppingViewModel()
        {
            // Capture
            // setup the mock of IConfiguration
            var _configuration = CreateAppSettingsMock();

            //Create In Memory Database instead of using the system database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "ShoppingDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                // remove all data to start with a clean database instance
                CleanDataBase(context);
                // add required data
                context.ShoppingItems.Add(new ShoppingItem
                {
                    Id = 1100,
                    Name = "Tomate",
                    Amount = 1.0,
                    Unit = "Kg"
                });
                context.SaveChanges();
            }

            using (var context = new DataBaseContext(options))
            {
                controller = new ShoppingController(context);
                // Act
                var result = controller.GetEditShoppingModal(_configuration.Object, 1100);


                // Assert
                Assert.NotNull(result);
                Assert.IsType<ViewResult>(result);

                var viewResult = result as ViewResult;
                var model = viewResult.Model as EditShoppingViewModel;

                Assert.Equal(context.ShoppingItems.First().Name,model.Item.Name);
                Assert.NotNull(model.Units);
            }
        }

        [Fact]
        public void UpdateShoppingItem_ReturnsAViewResultWithViewModel()
        {
            // Capture
            // setup the mock of IConfiguration
            var _configuration = CreateAppSettingsMock();

            //Create In Memory Database instead of using the system database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "ShoppingDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                // remove all data to start with a clean database instance
                CleanDataBase(context);
                // add required data
                context.ShoppingItems.Add(new ShoppingItem
                {
                    Id = 1100,
                    Name = "Tomate",
                    Amount = 1.0,
                    Unit = "Kg"
                });
                context.SaveChanges();
            }

            using (var context = new DataBaseContext(options))
            {
                controller = new ShoppingController(context);
                // Act
                var result = controller.UpdateShoppingItem(_configuration.Object, 1100,"Gurke",2.0,"Kg");


                // Assert
                Assert.NotNull(result);
                Assert.IsType<ViewResult>(result);

                var viewResult = result as ViewResult;
                var model = viewResult.Model as ShoppingViewModel;

                Assert.Contains(model.ShoppingItems.First(), context.ShoppingItems.ToList());
                Assert.Equal(1100, model.ShoppingItems.First().Id);
                Assert.Equal("Gurke",model.ShoppingItems.First().Name);
                Assert.NotNull(model.Units);
            }
        }
        [Fact]
        public void getShoppingListAsString_ReturnsAStringWithShoppingItemName()
        {
            // Capture
            List<ShoppingItem> list = new List<ShoppingItem>();

            list.Add(new ShoppingItem()
                {
                    Id = 1100,
                    Name = "Tomate",
                    Amount = 1.0,
                    Unit = "Kg"
                });

            //create In Memory Database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "ShoppingDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                controller = new ShoppingController(context);
                // Act
                var result = controller.getShoppingListAsString(list);


                // Assert
                Assert.NotNull(result);
                Assert.IsType<string>(result);

                Assert.Contains("- Tomate   1Kg",result);
            }
        }
        [Fact]
        public void createViewModel_ReturnsAShoppingViewModelWithData()
        {
            // Capture
            // setup the mock of IConfiguration
            var _configuration = CreateAppSettingsMock();

            //Create In Memory Database instead of using the system database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "ShoppingDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                // remove all data to start with a clean database instance
                CleanDataBase(context);
                // add required data
                context.ShoppingItems.Add(new ShoppingItem
                {
                    Id = 1100,
                    Name = "Tomate",
                    Amount = 1.0,
                    Unit = "Kg"
                });
                context.SaveChanges();
            }

            using (var context = new DataBaseContext(options))
            {
                controller = new ShoppingController(context);
                // Act
                var result = controller.createViewModel(_configuration.Object);


                // Assert
                Assert.NotNull(result);
                Assert.IsType<ShoppingViewModel>(result);

                var model = result.ShoppingItems as List<ShoppingItem>;

                Assert.Contains(context.ShoppingItems.First(), model);
                Assert.NotNull(result.Units);
            }
        }

        // This Method should clean up the database context by removing existing data from previous tests within this class
        private void CleanDataBase(DataBaseContext context)
        {
            context.ShoppingItems.RemoveRange(context.ShoppingItems.ToList());
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
