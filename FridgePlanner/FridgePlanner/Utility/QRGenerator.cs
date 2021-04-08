using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace FridgePlanner.Utility
{
    public static class QRGenerator
    {
        public static byte[] GenerateQR(string qrText)
        {
            //Create Instance of QRCodeGenerator
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            //Cerate QRCodeData with input String qrText
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText,QRCodeGenerator.ECCLevel.Q);
            //Create qrCode from qrCodeData
            QRCode qrCode = new QRCode(qrCodeData);
            //Create a Bitmap of the qrCode
            // using 20px per module in the qrCode
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            //Return the qrCode as Bytearray
            return BitmapToBytes(qrCodeImage);
        }

        private static Byte[] BitmapToBytes(Bitmap img)
        {
            //Create Memory Stream to save the Bitmap img temporary and Return the Bytearray
            using (MemoryStream stream = new MemoryStream())
            {
                //save the Bitmap to the Stream as Png
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}