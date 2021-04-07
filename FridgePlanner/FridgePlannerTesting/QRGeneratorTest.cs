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
            // create text to transform into a qr code image
            string inputText = "ShoppingList \n - Tomate\n -Brot";

            //Act
            byte[] qrData1 = QRGenerator.GenerateQR(inputText);
            byte[] qrData2 = QRGenerator.GenerateQR(inputText);

            //Assert 
            // compare if QRGenerator outputs equal data on multiple generations
            Assert.Equal(qrData1,qrData2);
        }

    }
}
