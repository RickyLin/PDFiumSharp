using System;
using PDFiumSharp;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace TestConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var doc = new PdfDocument("TestDoc.pdf"))
			{
				int i = 0;
                foreach (var page in doc.Pages)
                {
                    using (page)
                    {
                        //var textInfo = page.Text.GetTextInfo(0, page.Text.CountChars);
                        var pageObjects = page.PageObjects;
                        List<PdfPageObject> pageObjs = new List<PdfPageObject>();

                        List<string> str = new List<string>();

                        for (int j = 0, len = pageObjects.Count; j < len; j++)
                        {
                            pageObjs.Add(pageObjects[j]);
                            if(pageObjects[j].ObjectType == PDFiumSharp.Enums.PageObjTypes.Text)
                            {
                                var txtPageObj = (PdfTextObject)pageObjects[j];
                                str.Add(txtPageObj.TextUnicode);
                                var deviceRect = doc.RectangleFromPdf(page, txtPageObj.BoundingBox);
                            }
                        }

                        var rects = page.Text.GetTextInfo(0, page.Text.CountChars).Rects;
                        
                        List<string> fontName = new List<string>();
                        int charIndex = 0;
                        
                        /*foreach (var rect in rects)
                        {
                            var txtContent = page.Text.GetBoundedText(rect);
                            str.Add( txtContent );
                            fontName.Add(page.Text.GetFontNameInfo(charIndex));

                            //var point = page.PageToDevice(0, 0, (int)(rect.Right - rect.Left), (int)(rect.Bottom - rect.Top), rect.Left, rect.Top, page.Orientation);
                            var deviceRect = doc.RectangleFromPdf(page, rect);
                            charIndex += txtContent.Length;
                        }*/

                        //var font = page.Text.Font;
                        //textInfo.Rects
                        int width = (int)(page.Width);
                        int height = (int)(page.Height);

                        float dpiX = 72f;
                        float dpiY = 72f;
                        var thumbWidth = 800;
                        var thumbHeight = 800;
                        var ratio = 1f;

                        var memStream = new MemoryStream();

                        // Get correct width/height for new DPI
                        var pdfBitmapWidth = width * (int)dpiX / 72;
                        var pdfBitmapHeight = height * (int)dpiY / 72;
                        try
                        {
                            using (var pdfBitmap = new PDFiumBitmap(pdfBitmapWidth, pdfBitmapHeight, false))
                            {

                                Console.WriteLine("Rendering page " + i.ToString());
                                pdfBitmap.FillRectangle(0, 0, pdfBitmapWidth, pdfBitmapHeight, new PDFiumSharp.Types.FPDF_COLOR(255, 255, 255));
                                page.Render(pdfBitmap, rectDest: (0, 0, pdfBitmapWidth, pdfBitmapHeight), orientation: PDFiumSharp.Enums.PageOrientations.Normal, flags: PDFiumSharp.Enums.RenderingFlags.None);
                                pdfBitmap.Save(memStream);
                            }

                            //memStream.Seek(0, SeekOrigin.Begin);

                            using (var img = Image.FromStream(memStream))
                            using (var bitmap = new Bitmap(img))
                            using (var fileStream = new FileStream($"{i}.png", FileMode.Create))
                            {
                                bitmap.SetResolution(dpiX, dpiY);

                                bitmap.Save(fileStream, ImageFormat.Png);
                            }

                            /*ratio = Math.Min((float)thumbWidth / (float)width, (float)thumbHeight / (float)height);

                            thumbWidth = (int)(width * ratio);
                            thumbHeight = (int)(height * ratio);

                            var originalImage = Bitmap.FromStream(memStream);
                            memStream.Dispose();

                            using (var thumbImage = new Bitmap(thumbWidth, thumbHeight))
                            using (var graphics = Graphics.FromImage(thumbImage))
                            using (var fileStream = new FileStream($"thumb_{i}.png", FileMode.Create))
                            {
                                //graphics.CompositingQuality = CompositingQuality.HighQuality;
                                graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
                                //graphics.SmoothingMode = SmoothingMode.HighQuality;
                                graphics.DrawImage(originalImage, 0, 0, thumbWidth, thumbHeight);
                                graphics.Save();
                                
                                thumbImage.SetResolution(dpiX, dpiY);
                                thumbImage.Save(fileStream, ImageFormat.Png);
                            } */
                        } 
                        finally
                        {
                            //memStream.Dispose();
                        }

                        var textInfo = page.Text.GetTextInfo(0, page.Text.CountChars);

                        //Console.WriteLine("==================================================");

                        //Console.WriteLine("Rendering page " + i.ToString());
                        //using (var image = page.Render(width, height, dpiX, dpiY, PDFiumSharp.Enums.RenderingFlags.CorrectFromDpi))
                        //{
                        //   image.Save($"page-{i}.png");
                        //}
                    }
                    i++;
                }
                
			}
			Console.ReadKey();
		}
	}
}
