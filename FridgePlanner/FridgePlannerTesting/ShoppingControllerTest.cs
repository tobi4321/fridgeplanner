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
        private ShoppingController controller { get; set; }

        public ShoppingControllerTest()
        {

        }
        [Fact]
        public void Index_ReturnsAViewResultWithViewModel()
        {
            // Capture
            var _configuration = new Mock<IConfiguration>();

            var oneSectionMock = new Mock<IConfigurationSection>();
            oneSectionMock.Setup(s => s.Value).Returns("Kg");
            var twoSectionMock = new Mock<IConfigurationSection>();
            twoSectionMock.Setup(s => s.Value).Returns("g");
            var unitsSectionMock = new Mock<IConfigurationSection>();
            unitsSectionMock.Setup(s => s.GetChildren()).Returns(new List<IConfigurationSection> { oneSectionMock.Object, twoSectionMock.Object });
            _configuration.Setup(c => c.GetSection("Units")).Returns(unitsSectionMock.Object);

            //create In Memory Database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "ShoppingDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                context.ShoppingListItems.Add(new ShoppingListItem
                {
                    Id = 2736459,
                    Name = "Tomate",
                    Amount = 1.0,
                    Unit = "Kg"
                });
                context.ShoppingListItems.Add(new ShoppingListItem
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
                Assert.Contains(context.ShoppingListItems.First(), model.ShoppingItems);
            }
        }
        [Fact]
        public void AddItem_ReturnsAViewResultWithModel()
        {
            // Capture
            var _configuration = new Mock<IConfiguration>();

            var oneSectionMock = new Mock<IConfigurationSection>();
            oneSectionMock.Setup(s => s.Value).Returns("Kg");
            var twoSectionMock = new Mock<IConfigurationSection>();
            twoSectionMock.Setup(s => s.Value).Returns("g");
            var unitsSectionMock = new Mock<IConfigurationSection>();
            unitsSectionMock.Setup(s => s.GetChildren()).Returns(new List<IConfigurationSection> { oneSectionMock.Object, twoSectionMock.Object });
            _configuration.Setup(c => c.GetSection("Units")).Returns(unitsSectionMock.Object);

            //create In Memory Database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "ShoppingDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                context.ShoppingListItems.RemoveRange(context.ShoppingListItems.ToList());
                context.SaveChanges();
            }

            using (var context = new DataBaseContext(options))
            {
                controller = new ShoppingController(context);

                ShoppingListItem item = new ShoppingListItem()
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
                Assert.Contains(context.ShoppingListItems.First(), model.ShoppingItems);
            }
        }
        [Fact]
        public void DeleteShoppingItem_ReturnsAViewResultWithEmptyShoppingList()
        {
            // Capture
            var _configuration = new Mock<IConfiguration>();

            var oneSectionMock = new Mock<IConfigurationSection>();
            oneSectionMock.Setup(s => s.Value).Returns("Kg");
            var twoSectionMock = new Mock<IConfigurationSection>();
            twoSectionMock.Setup(s => s.Value).Returns("g");
            var unitsSectionMock = new Mock<IConfigurationSection>();
            unitsSectionMock.Setup(s => s.GetChildren()).Returns(new List<IConfigurationSection> { oneSectionMock.Object, twoSectionMock.Object });
            _configuration.Setup(c => c.GetSection("Units")).Returns(unitsSectionMock.Object);

            //create In Memory Database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "ShoppingDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                context.ShoppingListItems.RemoveRange(context.ShoppingListItems.ToList());
                context.ShoppingListItems.Add(new ShoppingListItem
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
            var _configuration = new Mock<IConfiguration>();

            var oneSectionMock = new Mock<IConfigurationSection>();
            oneSectionMock.Setup(s => s.Value).Returns("Kg");
            var twoSectionMock = new Mock<IConfigurationSection>();
            twoSectionMock.Setup(s => s.Value).Returns("g");
            var unitsSectionMock = new Mock<IConfigurationSection>();
            unitsSectionMock.Setup(s => s.GetChildren()).Returns(new List<IConfigurationSection> { oneSectionMock.Object, twoSectionMock.Object });
            _configuration.Setup(c => c.GetSection("Units")).Returns(unitsSectionMock.Object);

            //create In Memory Database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "ShoppingDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                context.ShoppingListItems.RemoveRange(context.ShoppingListItems.ToList());
                context.ShoppingListItems.Add(new ShoppingListItem
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

                Assert.Equal(context.ShoppingListItems.First().Name,model.Item.Name);
                Assert.NotNull(model.Units);
            }
        }

        [Fact]
        public void UpdateShoppingItem_ReturnsAViewResultWithViewModel()
        {
            // Capture
            var _configuration = new Mock<IConfiguration>();

            var oneSectionMock = new Mock<IConfigurationSection>();
            oneSectionMock.Setup(s => s.Value).Returns("Kg");
            var twoSectionMock = new Mock<IConfigurationSection>();
            twoSectionMock.Setup(s => s.Value).Returns("g");
            var unitsSectionMock = new Mock<IConfigurationSection>();
            unitsSectionMock.Setup(s => s.GetChildren()).Returns(new List<IConfigurationSection> { oneSectionMock.Object, twoSectionMock.Object });
            _configuration.Setup(c => c.GetSection("Units")).Returns(unitsSectionMock.Object);

            //create In Memory Database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "ShoppingDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                context.ShoppingListItems.RemoveRange(context.ShoppingListItems.ToList());
                context.ShoppingListItems.Add(new ShoppingListItem
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

                Assert.Contains(model.ShoppingItems.First(), context.ShoppingListItems.ToList());
                Assert.Equal(1100, model.ShoppingItems.First().Id);
                Assert.Equal("Gurke",model.ShoppingItems.First().Name);
                Assert.NotNull(model.Units);
            }
        }
        [Fact]
        public void getShoppingListAsString_ReturnsAStringWithShoppingItemName()
        {
            // Capture
            List<ShoppingListItem> list = new List<ShoppingListItem>();

            list.Add(new ShoppingListItem()
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
            var _configuration = new Mock<IConfiguration>();

            var oneSectionMock = new Mock<IConfigurationSection>();
            oneSectionMock.Setup(s => s.Value).Returns("Kg");
            var twoSectionMock = new Mock<IConfigurationSection>();
            twoSectionMock.Setup(s => s.Value).Returns("g");
            var unitsSectionMock = new Mock<IConfigurationSection>();
            unitsSectionMock.Setup(s => s.GetChildren()).Returns(new List<IConfigurationSection> { oneSectionMock.Object, twoSectionMock.Object });
            _configuration.Setup(c => c.GetSection("Units")).Returns(unitsSectionMock.Object);

            //create In Memory Database
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "ShoppingDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
                context.ShoppingListItems.RemoveRange(context.ShoppingListItems.ToList());
                context.ShoppingListItems.Add(new ShoppingListItem
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

                var model = result.ShoppingItems as List<ShoppingListItem>;

                Assert.Contains(context.ShoppingListItems.First(), model);
                Assert.NotNull(result.Units);
            }
        }
    }
}
