using System;
using System.Collections.Generic;
using System.Text;
using QRCoder;
using Xunit;
using System.Drawing;
using FridgePlanner.Utility;

namespace FridgePlannerTesting
{
    public class QRGeneratorTest
    {
        [Fact]
        public void QrCodeGeneratorMultipleInputsWithSameOutput()
        {
            // Arrange 
            string inputText = "ShoppingList \n - Tomate\n -Brot";

            //Act
            byte[] qrData1 = QRGenerator.GenerateQR(inputText);
            byte[] qrData2 = QRGenerator.GenerateQR(inputText);

            //Assert
            Assert.Equal(qrData1,qrData2);
        }

    }
}
