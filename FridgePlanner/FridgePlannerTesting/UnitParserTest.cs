using FridgePlanner.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FridgePlannerTesting
{
    public class UnitParserTest
    {
        [Fact]
        public void ConvertLiterToMl()
        {
            // Arrange
            double l = 1.6;

            // Act
            double ml = UnitParser.LToMl(l);

            // Assert with precision of 4 decimal places
            Assert.Equal(1600.0,ml,4);
        }
        [Fact]
        public void ConvertMlToL()
        {
            // Arrange
            double ml = 1600.0;

            // Act
            double l = UnitParser.MlToL(ml);

            // Assert with precision of 4 decimal places
            Assert.Equal(1.6, l, 4);
        }
        [Fact]
        public void ConvertKgToG()
        {
            // Arrange
            double kg = 1.650;

            // Act
            double g = UnitParser.KgToG(kg);

            // Assert with precision of 4 decimal places
            Assert.Equal(1650.0, g, 4);
        }
        [Fact]
        public void ConvertGToKg()
        {
            // Arrange
            double g = 1650.0;

            // Act
            double kg = UnitParser.GToKg(g);

            // Assert with precision of 4 decimal places
            Assert.Equal(1.65, kg ,4);
        }
        [Fact]
        public void ConvertGToKgWithParser()
        {
            // Arrange
            double g = 1650.0;

            // Act
            double kg = (double)UnitParser.ParseToUnit("Kg",g);

            // Assert with precision of 4 decimal places
            Assert.Equal(1.65, kg, 4);
        }
        [Fact]
        public void ConvertKgToGWithParser()
        {
            // Arrange
            double kg = 1.650;

            // Act
            double g = (double)UnitParser.ParseToUnit("g",kg);

            // Assert with precision of 4 decimal places
            Assert.Equal(1650.0, g, 4);
        }
        [Fact]
        public void WrongParserInputReturnNull()
        {
            // Arrange
            double kg = 1.650;

            // Act
            var g = UnitParser.ParseToUnit("fail", kg);

            // Assert
            Assert.Null(g);
        }


    }
}
