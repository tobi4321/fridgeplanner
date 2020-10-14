﻿using FridgePlanner.Controllers;
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

            // Assert
            Assert.Equal(1600.0,ml);
        }
        [Fact]
        public void ConvertMlToL()
        {
            // Arrange
            double ml = 1600.0;

            // Act
            double l = UnitParser.MlToL(ml);

            // Assert
            Assert.Equal(1.6, l);
        }
        [Fact]
        public void ConvertKgToG()
        {
            // Arrange
            double kg = 1.650;

            // Act
            double g = UnitParser.KgToG(kg);

            // Assert
            Assert.Equal(1650.0, g);
        }
        [Fact]
        public void ConvertGToKg()
        {
            // Arrange
            double g = 1650.0;

            // Act
            double kg = UnitParser.GToKg(g);

            // Assert
            Assert.Equal(1.65, kg);
        }
    }
}
