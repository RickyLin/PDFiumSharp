using System;
using PDFiumSharp;
using System.IO;

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
                        var textInfo = page.Text.GetTextInfo(0, page.Text.CountChars);
                        //textInfo.Rects
                        int width = (int)(page.Width / 72.0 * 96);
                        int height = (int)(page.Height / 72.0 * 96);

                        using (var bitmap = new PDFiumBitmap(width, height, true))
                        using (var stream = new FileStream($"{i++}.png", FileMode.Create))
                        {
                            Console.WriteLine("Rendering page " + i.ToString());
                            bitmap.FillRectangle(0, 0, width, height, new PDFiumSharp.Types.FPDF_COLOR(255, 255, 255));
                            page.Render(bitmap, rectDest: (0, 0, width, height), orientation: PDFiumSharp.Enums.PageOrientations.Normal, flags: PDFiumSharp.Enums.RenderingFlags.LcdText);
                            bitmap.Save(stream);
                        }
                    }
                }
                
			}
			Console.ReadKey();
		}
	}
}
