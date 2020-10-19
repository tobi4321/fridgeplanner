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

namespace FridgePlannerTesting
{
    public class HomeControllerTest
    {
        private HomeController controller { get; set; }

        public HomeControllerTest()
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
            .UseInMemoryDatabase(databaseName: "HomeDataBase")
            .Options;

            using (var context = new DataBaseContext(options))
            {
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
                controller = new HomeController(context);
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
    }
}
