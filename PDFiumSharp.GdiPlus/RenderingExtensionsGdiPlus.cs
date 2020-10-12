#region Copyright and License
/*
This file is part of PDFiumSharp, a wrapper around the PDFium library for the .NET framework.
Copyright (C) 2017 Tobias Meyer
License: Microsoft Reciprocal License (MS-RL)
*/
#endregion
using System;
using System.Drawing;
using System.Drawing.Imaging;
using PDFiumSharp.Enums;
using PDFiumSharp.Types;

namespace PDFiumSharp
{
    /// <summary>
    /// extension methods for PdfPage.
    /// </summary>
	public static class RenderingExtensionsGdiPlus
    {
        /// <summary>
        /// Renders the page to a <see cref="Bitmap"/>
        /// </summary>
        /// <param name="page">The page which is to be rendered.</param>
        /// <param name="renderTarget">The bitmap to which the page is to be rendered.</param>
        /// <param name="rectDest">The destination rectangle in <paramref name="renderTarget"/>.</param>
        /// <param name="orientation">The orientation at which the page is to be rendered.</param>
        /// <param name="flags">The flags specifying how the page is to be rendered.</param>
        /// <param name="backgroundColor">background color of the image</param>
        public static void Render(this PdfPage page, Bitmap renderTarget, (int left, int top, int width, int height) rectDest
            , PageOrientations orientation = PageOrientations.Normal, RenderingFlags flags = RenderingFlags.None, FPDF_COLOR? backgroundColor = null)
        {
            if (renderTarget == null)
                throw new ArgumentNullException(nameof(renderTarget));

            var format = GetBitmapFormat(renderTarget);
            var data = renderTarget.LockBits(new Rectangle(0, 0, renderTarget.Width, renderTarget.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, renderTarget.PixelFormat);
            using (var tmp = new PDFiumBitmap(renderTarget.Width, renderTarget.Height, format, data.Scan0, data.Stride))
            {
                if (backgroundColor.HasValue)
                {
                    tmp.Fill(backgroundColor.Value);
                }

                page.Render(tmp, rectDest, orientation, flags);
            }

            renderTarget.UnlockBits(data);
        }

        /// <summary>
        /// Renders the page to a <see cref="Bitmap"/>
        /// </summary>
        /// <param name="page">The page which is to be rendered.</param>
        /// <param name="bitmap">The bitmap to which the page is to be rendered.</param>
        /// <param name="orientation">The orientation at which the page is to be rendered.</param>
        /// <param name="flags">The flags specifying how the page is to be rendered.</param>
        /// <param name="backgroundColor">background color of the image</param>
        public static void Render(this PdfPage page, Bitmap bitmap, PageOrientations orientation = PageOrientations.Normal, RenderingFlags flags = RenderingFlags.None
            , FPDF_COLOR? backgroundColor = null)
        {
            page.Render(bitmap, (0, 0, bitmap.Width, bitmap.Height), orientation, flags, backgroundColor);
        }

        #region New Render() methods
        /// <summary>
        /// Renders a page of the PDF document to the provided graphics instance.
        /// </summary>
        /// <param name="page">Number of the page to render.</param>
        /// <param name="graphics">Graphics instance to render the page on.</param>
        /// <param name="dpiX">Horizontal DPI.</param>
        /// <param name="dpiY">Vertical DPI.</param>
        /// <param name="bounds">Bounds to render the page in.</param>
        /// <param name="forPrinting">Render the page for printing.</param>
        public static void Render(this PdfPage page, Graphics graphics, float dpiX, float dpiY, Rectangle bounds, bool forPrinting)
        {
            page.Render(graphics, dpiX, dpiY, bounds, forPrinting ? RenderingFlags.Printing : RenderingFlags.None);
        }

        /// <summary>
        /// Renders a page of the PDF document to the provided graphics instance.
        /// </summary>
        /// <param name="page">Number of the page to render.</param>
        /// <param name="graphics">Graphics instance to render the page on.</param>
        /// <param name="dpiX">Horizontal DPI.</param>
        /// <param name="dpiY">Vertical DPI.</param>
        /// <param name="bounds">Bounds to render the page in.</param>
        /// <param name="flags">Flags used to influence the rendering.</param>
        public static void Render(this PdfPage page, Graphics graphics, float dpiX, float dpiY, Rectangle bounds, RenderingFlags flags = RenderingFlags.None)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");

            float graphicsDpiX = graphics.DpiX;
            float graphicsDpiY = graphics.DpiY;

            var dc = graphics.GetHdc();

            try
            {
                if ((int)graphicsDpiX != (int)dpiX || (int)graphicsDpiY != (int)dpiY)
                {
                    var transform = new XFORM
                    {
                        eM11 = graphicsDpiX / dpiX,
                        eM22 = graphicsDpiY / dpiY
                    };

                    PDFium.SetGraphicsMode(dc, PDFium.GM_ADVANCED);
                    PDFium.ModifyWorldTransform(dc, ref transform, PDFium.MWT_LEFTMULTIPLY);
                }

                var point = new POINT();
                PDFium.SetViewportOrgEx(dc, bounds.X, bounds.Y, out point);

                PDFium.FPDF_RenderPage(dc, page.Handle, 0, 0, bounds.Width, bounds.Height, 0, page.FlagsToFPDFFlags(flags));

                PDFium.SetViewportOrgEx(dc, point.X, point.Y, out point);
            }
            finally
            {
                graphics.ReleaseHdc(dc);
            }
        }

        /// <summary>
        /// Renders a page of the PDF document to an image.
        /// </summary>
        /// <param name="page">Number of the page to render.</param>
        /// <param name="dpiX">Horizontal DPI.</param>
        /// <param name="dpiY">Vertical DPI.</param>
        /// <param name="forPrinting">Render the page for printing.</param>
        /// <returns>The rendered image.</returns>
        public static Image Render(this PdfPage page, float dpiX, float dpiY, bool forPrinting)
        {
            return page.Render((int)page.Width, (int)page.Height, dpiX, dpiY, forPrinting);
        }

        /// <summary>
        /// Renders a page of the PDF document to an image.
        /// </summary>
        /// <param name="page">Number of the page to render.</param>
        /// <param name="dpiX">Horizontal DPI.</param>
        /// <param name="dpiY">Vertical DPI.</param>
        /// <param name="flags">Flags used to influence the rendering.</param>
        /// <returns>The rendered image.</returns>
        public static Image Render(this PdfPage page, float dpiX, float dpiY, RenderingFlags flags)
        {
            return page.Render((int)page.Width, (int)page.Height, dpiX, dpiY, flags);
        }

        /// <summary>
        /// Renders a page of the PDF document to an image.
        /// </summary>
        /// <param name="page">Number of the page to render.</param>
        /// <param name="width">Width of the rendered image.</param>
        /// <param name="height">Height of the rendered image.</param>
        /// <param name="dpiX">Horizontal DPI.</param>
        /// <param name="dpiY">Vertical DPI.</param>
        /// <param name="forPrinting">Render the page for printing.</param>
        /// <returns>The rendered image.</returns>
        public static Image Render(this PdfPage page, int width, int height, float dpiX, float dpiY, bool forPrinting)
        {
            return page.Render(width, height, dpiX, dpiY, forPrinting ? RenderingFlags.Printing : RenderingFlags.None);
        }

        /// <summary>
        /// Renders a page of the PDF document to an image.
        /// </summary>
        /// <param name="page">Number of the page to render.</param>
        /// <param name="width">Width of the rendered image.</param>
        /// <param name="height">Height of the rendered image.</param>
        /// <param name="dpiX">Horizontal DPI.</param>
        /// <param name="dpiY">Vertical DPI.</param>
        /// <param name="flags">Flags used to influence the rendering.</param>
        /// <returns>The rendered image.</returns>
        public static Image Render(this PdfPage page, int width, int height, float dpiX, float dpiY, RenderingFlags flags = RenderingFlags.None)
        {
            return page.Render(width, height, dpiX, dpiY, 0, flags);
        }

        /// <summary>
        /// Renders a page of the PDF document to an image.
        /// </summary>
        /// <param name="page">Number of the page to render.</param>
        /// <param name="width">Width of the rendered image.</param>
        /// <param name="height">Height of the rendered image.</param>
        /// <param name="dpiX">Horizontal DPI.</param>
        /// <param name="dpiY">Vertical DPI.</param>
        /// <param name="rotate">Rotation.</param>
        /// <param name="flags">Flags used to influence the rendering.</param>
        /// <returns>The rendered image.</returns>
        public static Image Render(this PdfPage page, int width, int height, float dpiX, float dpiY, PageOrientations rotate, RenderingFlags flags = RenderingFlags.None)
        {
            if ((flags & RenderingFlags.CorrectFromDpi) != 0)
            {
                width = width * (int)dpiX / 72;
                height = height * (int)dpiY / 72;
            }

            var bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            bitmap.SetResolution(dpiX, dpiY);

            var data = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

            try
            {
                var handle = PDFium.FPDFBitmap_CreateEx(width, height, BitmapFormats.BGRA, data.Scan0, width * 4);

                try
                {
                    uint background = (flags & RenderingFlags.Transparent) == 0 ? 0xFFFFFFFF : 0x00FFFFFF;

                    PDFium.FPDFBitmap_FillRect(handle, 0, 0, width, height, background);

                    PDFium.FPDF_RenderPageBitmap(handle, page.Handle, 0, 0, width, height, rotate, page.FlagsToFPDFFlags(flags));

                }
                finally
                {
                    PDFium.FPDFBitmap_Destroy(handle);
                }
            }
            finally
            {
                bitmap.UnlockBits(data);
            }

            return bitmap;
        }

        #endregion New Render() methods

        private static BitmapFormats GetBitmapFormat(Bitmap bitmap)
        {
            switch (bitmap.PixelFormat)
            {
                case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
                    return BitmapFormats.BGR;
                case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
                    return BitmapFormats.BGRA;
                case System.Drawing.Imaging.PixelFormat.Format32bppRgb:
                    return BitmapFormats.BGRx;
                default:
                    throw new NotSupportedException($"Pixel format {bitmap.PixelFormat} is not supported.");
            }
        }
    }
}
