using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace windowsService.utilityClasses
{
    public class utility
    {
        public static string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value, Formatting.None);   
        }
        public static void fireScannerUI()
        {
            string path = ProcessStarter.getAssemblyPath() + @"ScannerDesktopUI\ScannerDesktopUI\bin\Debug\ScannerDesktopUI.exe";
            ProcessStarter pst = new ProcessStarter("ScannerDesktopUI", @"" + path);
            pst.Run();
        }
        public static byte[] scannedImage()
        {
            byte[] imgBytes = new byte[0];
            if (twainHandler.Images.Count > 0)
            {
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    System.Drawing.Image img = twainHandler.Images[0];
                    twainHandler.Images.Remove(img);
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    imgBytes = ms.GetBuffer();
                }
            }
            return imgBytes;
        }
        public static byte[] getDocument()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4, 0, 0, 10, 10);
                PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                writer.CompressionLevel = PdfStream.BEST_COMPRESSION;
                doc.Open();
                twainHandler.Images = utility.compressImage(twainHandler.Images);
                for (int i = 0, length = twainHandler.Images.Count; i < length; i++)
                {
                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(twainHandler.Images[i], System.Drawing.Imaging.ImageFormat.Png);
                    img.ScaleToFit(doc.PageSize);
                    img.SetAbsolutePosition(0, 0);
                    img.ScaleAbsoluteHeight(doc.PageSize.Height);

                    doc.Add(img);
                    doc.NewPage();
                }
                twainHandler.Images = new List<System.Drawing.Image>();
                doc.Close();
                writer.Close();
                return ms.ToArray();
            }
        }
        public static System.Drawing.Image rotateImage(System.Drawing.Image img, string deg)
        {
            System.Drawing.RotateFlipType degree = ((deg == "180") ? (System.Drawing.RotateFlipType.Rotate180FlipX) : ((deg == "270") ? (System.Drawing.RotateFlipType.Rotate270FlipXY) : (System.Drawing.RotateFlipType.Rotate90FlipXY)));
            img.RotateFlip(degree);
            return img;
        }
        public static System.Drawing.Image cropImage(System.Drawing.Image img, int x, int y, int width, int height)
        {
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(x, y, width, height);
            Bitmap bit = new Bitmap(img, img.Width, img.Height);
            Bitmap crop = new Bitmap(width, height);
            Graphics gfx = Graphics.FromImage(crop);
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;//here add  System.Drawing.Drawing2D namespace;
            gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;//here add  System.Drawing.Drawing2D namespace;
            gfx.CompositingQuality = CompositingQuality.HighQuality;//here add  System.Drawing.Drawing2D namespace;
            gfx.DrawImage(bit, 0, 0, rect, GraphicsUnit.Pixel);
            gfx.ExcludeClip(rect);
            img = crop.Clone() as System.Drawing.Image;
            return img;
        }
        public static List<System.Drawing.Image> convertBytesToImages(List<byte[]> bytes)
        {
            List<System.Drawing.Image> images = new List<System.Drawing.Image>();
            for (int i = 0; i < bytes.Count; i++)
            {
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes[i]))
                {
                    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                    images.Add(img);
                }
            }
            return images;
        }
        public static List<System.Drawing.Image> compressImage(List<System.Drawing.Image> images)
        {
            List<System.Drawing.Image> newImages = new List<System.Drawing.Image>();
            System.Drawing.Imaging.ImageCodecInfo encoder = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders().First(i => i.MimeType == "image/png");

            System.Drawing.Imaging.EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters(2);
            System.Drawing.Imaging.EncoderParameter parameter = new System.Drawing.Imaging.EncoderParameter(
                System.Drawing.Imaging.Encoder.Compression, (long)100);
            encoderParams.Param[0] = parameter;
            parameter = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality,
                        (long)100);
            encoderParams.Param[1] = parameter;
            for (int i = 0; i < images.Count; i++)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    System.Drawing.Image newImage = new System.Drawing.Bitmap(images[i], new System.Drawing.Size(500, 650));
                    newImage.Save(ms, encoder, encoderParams);
                    newImages.Add(System.Drawing.Image.FromStream(ms));
                }
            }
            return newImages;
        }
    }
}
