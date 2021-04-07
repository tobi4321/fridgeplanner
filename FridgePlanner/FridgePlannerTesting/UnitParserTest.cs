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
            // input variable for the parser function
            double liter = 1.6;

            // Act
            // parse the value of liter into an other unit
            double milliliter = UnitParser.LToMl(liter);

            // Assert with precision of 4 decimal places
            Assert.Equal(1600.0,milliliter,4);
        }
        [Fact]
        public void ConvertMlToL()
        {
            // Arrange
            // input variable for the parser function
            double milliliter = 1600.0;

            // Act
            // parse the value of milliliter into an other unit
            double liter = UnitParser.MlToL(milliliter);

            // Assert with precision of 4 decimal places
            Assert.Equal(1.6, liter, 4);
        }
        [Fact]
        public void ConvertKgToG()
        {
            // Arrange
            // input variable for the parser function
            double kilogramm = 1.650;

            // Act
            // parse the value of kilogramm into an other unit
            double gramm = UnitParser.KgToG(kilogramm);

            // Assert with precision of 4 decimal places
            Assert.Equal(1650.0, gramm, 4);
        }
        [Fact]
        public void ConvertGToKg()
        {
            // Arrange
            // input variable for the parser function
            double gramm = 1650.0;

            // Act
            // parse the value of gramm into an other unit
            double kilogramm = UnitParser.GToKg(gramm);

            // Assert with precision of 4 decimal places
            Assert.Equal(1.65, kilogramm ,4);
        }
        [Fact]
        public void ConvertGToKgWithParser()
        {
            // Arrange
            // input variable for the parser function
            double gramm = 1650.0;
            string targetunit = "Kg";

            // Act
            // parse the value of gramm into the targetunit
            double kilogramm = (double)UnitParser.ParseToUnit(targetunit,gramm);

            // Assert with precision of 4 decimal places
            Assert.Equal(1.65, kilogramm, 4);
        }
        [Fact]
        public void ConvertKgToGWithParser()
        {
            // Arrange
            // input variable for the parser function
            double kilogramm = 1.650;
            string targetunit = "g";

            // Act
            // parse the value of gramm into the targetunit
            double gramm = (double)UnitParser.ParseToUnit(targetunit,kilogramm);

            // Assert with precision of 4 decimal places
            Assert.Equal(1650.0, gramm, 4);
        }
        [Fact]
        public void WrongParserInputReturnsNull()
        {
            // Arrange
            // input variable for the parser function
            double kilogramm = 1.650;
            string targetunit = "fail";

            // Act
            // parse the value of gramm into the targetunit
            var gramm = UnitParser.ParseToUnit(targetunit, kilogramm);

            // Assert
            Assert.Null(gramm);
        }


    }
}
