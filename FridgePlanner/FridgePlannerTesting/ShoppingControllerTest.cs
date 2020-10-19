using FridgePlanner.Controllers;
using FridgePlanner.Models;
using FridgePlanner.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
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
    }
}
