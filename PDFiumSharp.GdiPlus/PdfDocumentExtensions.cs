using PDFiumSharp.Enums;
using PDFiumSharp.Types;
using System.Drawing;

namespace PDFiumSharp
{
    /// <summary>
    /// extension methods for PdfDocument
    /// </summary>
    public static class PdfDocumentExtensions
    {
        /// <summary>
        /// render all pages in pdf document to a Bitmap array
        /// </summary>
        /// <param name="doc">the pdf document</param>
        /// <param name="orientation">orientation</param>
        /// <param name="flags">flags</param>
        /// <param name="backgroundColor">background color of returned images</param>
        /// <returns></returns>
        public static Bitmap[] RenderAllPagesToBitmap(this PdfDocument doc, PageOrientations orientation = PageOrientations.Normal, RenderingFlags flags = RenderingFlags.None, FPDF_COLOR? backgroundColor = null)
        {
            var result = new Bitmap[doc.Pages.Count];
            PdfPage page;

            for (int i = 0; i < result.Length; i++)
            {
                page = doc.Pages[i];
                result[i] = new Bitmap((int)page.Width, (int)page.Height);
                page.Render(result[i], orientation: orientation, flags: flags, backgroundColor: backgroundColor);
            }

            return result;
        }
    }
}
