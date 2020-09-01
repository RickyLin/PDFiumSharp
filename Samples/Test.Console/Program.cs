using System;
using PDFiumSharp;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

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
                        //textInfo.Rects
                        int width = (int)(page.Width);
                        int height = (int)(page.Height);

                        float dpiX = 200f;
                        float dpiY = 200f;

                        /*using (var bitmap = new PDFiumBitmap(width, height, false))
                        using (var stream = new FileStream($"{i}.png", FileMode.Create))
                        {
                            Console.WriteLine("Rendering page " + i.ToString());
                            bitmap.FillRectangle(0, 0, width, height, new PDFiumSharp.Types.FPDF_COLOR(255, 255, 255));
                            page.Render(bitmap, rectDest: (0, 0, width, height), orientation: PDFiumSharp.Enums.PageOrientations.Normal, flags: PDFiumSharp.Enums.RenderingFlags.None);
                            bitmap.Save(stream, dpiX, dpiY);
                        }*/


                        Console.WriteLine("==================================================");

                        Console.WriteLine("Rendering page " + i.ToString());
                        using (var image = page.Render(width, height, dpiX, dpiY, PDFiumSharp.Enums.RenderingFlags.CorrectFromDpi))
                        {
                           image.Save($"page-{i}.png");
                        }
                    }
                    i++;
                }
                
			}
			Console.ReadKey();
		}
	}
}
