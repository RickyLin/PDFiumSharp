﻿#region Copyright and License
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
using PDFiumSharp.Events;
using PDFiumSharp.Types;

namespace PDFiumSharp
{
    public class PdfPage : NativeWrapper<FPDF_PAGE>
    {
		private PdfText _text;
		private IFSDK_PAUSE _pause;

		public event EventHandler<ProgressiveRenderEventArgs> ProgressiveRender;

		public event EventHandler Loaded;
		public event EventHandler Disposed;

        private PdfPageObjectsCollection _pageObjects;

		/// <summary>
		/// Gets the page width (excluding non-displayable area) measured in points.
		/// One point is 1/72 inch(around 0.3528 mm).
		/// </summary>
		public double Width => PDFium.FPDF_GetPageWidth(Handle);

		/// <summary>
		/// Gets the page height (excluding non-displayable area) measured in points.
		/// One point is 1/72 inch(around 0.3528 mm).
		/// </summary>
		public double Height => PDFium.FPDF_GetPageHeight(Handle);

		/// <summary>
		/// Gets the page width and height (excluding non-displayable area) measured in points.
		/// One point is 1/72 inch(around 0.3528 mm).
		/// </summary>
		public (double Width, double Height) Size
		{
			get
			{
				if (PDFium.FPDF_GetPageSizeByIndex(Document.Handle, Index, out var width, out var height))
					return (width, height);
				throw new PDFiumException();
			}
		}

		/// <summary>
		/// Gets the page orientation.
		/// </summary>
		public PageOrientations Orientation
		{
			get => PDFium.FPDFPage_GetRotation(Handle);
			set => PDFium.FPDFPage_SetRotation(Handle, value);
		}

        /// <summary>
        /// Get the transparency of the page
        /// </summary>
        public bool HasTransparency => PDFium.FPDFPage_HasTransparency(Handle);

        /// <summary>
        /// Gets the zero-based index of the page in the <see cref="Document"/>
        /// </summary>
        public int Index { get; internal set; } = -1;

		/// <summary>
		/// Gets the <see cref="PdfDocument"/> which contains the page.
		/// </summary>
		public PdfDocument Document { get; }

		public PdfText Text 
		{ 
			get
            {
				if(_text == null)
                {
					_text = new PdfText(PDFium.FPDFText_LoadPage(Handle), this);
                }

				return _text;
            }
		}

        public PdfPageObjectsCollection PageObjects
        {
            get
            {
                if (_pageObjects == null)
                {
                    _pageObjects = new PdfPageObjectsCollection(this);
                }
                return _pageObjects;
            }
        }

        //public string Label => PDFium.FPDF_GetPageLabel(Document.Handle, Index);

        PdfPage(PdfDocument doc, FPDF_PAGE page, int index)
			: base(page)
		{
			if (page.IsNull)
				throw new PDFiumException();
			Document = doc;
			Index = index;

            _pause = new IFSDK_PAUSE(null)
            {
                needToPauseNowCallback = (IFSDK_PAUSE pThis) =>
                {
                    ProgressiveRenderEventArgs progressiveRenderEventArgs = new ProgressiveRenderEventArgs(pThis.userData);
                    this.OnProgressiveRender(progressiveRenderEventArgs);
                    return progressiveRenderEventArgs.NeedPause;
                }
            };
        }

		internal static PdfPage Load(PdfDocument doc, int index) => new PdfPage(doc, PDFium.FPDF_LoadPage(doc.Handle, index), index);
		internal static PdfPage New(PdfDocument doc, int index, double width, double height) => new PdfPage(doc, PDFium.FPDFPage_New(doc.Handle, index, width, height), index);

		/// <summary>
		/// Renders the page to a <see cref="PDFiumBitmap"/>
		/// </summary>
		/// <param name="renderTarget">The bitmap to which the page is to be rendered.</param>
		/// <param name="rectDest">The destination rectangle in <paramref name="renderTarget"/>.</param>
		/// <param name="orientation">The orientation at which the page is to be rendered.</param>
		/// <param name="flags">The flags specifying how the page is to be rendered.</param>
		public void Render(PDFiumBitmap renderTarget, (int left, int top, int width, int height) rectDest, PageOrientations orientation = PageOrientations.Normal, RenderingFlags flags = RenderingFlags.None)
		{
			if (renderTarget == null)
				throw new ArgumentNullException(nameof(renderTarget));

            if (IsDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }

			//if((flags & RenderingFlags.CorrectFromDpi) != 0)
   //         {
			//	rectDest.width = 
   //         }

			PDFium.FPDF_RenderPageBitmap(renderTarget.Handle, this.Handle, rectDest.left, rectDest.top, rectDest.width, rectDest.height, orientation, FlagsToFPDFFlags(flags));
		}

		/// <summary>
		/// Renders the page to a <see cref="PDFiumBitmap"/>
		/// </summary>
		/// <param name="renderTarget">The bitmap to which the page is to be rendered.</param>
		/// <param name="orientation">The orientation at which the page is to be rendered.</param>
		/// <param name="flags">The flags specifying how the page is to be rendered.</param>
		public void Render(PDFiumBitmap renderTarget, PageOrientations orientation = PageOrientations.Normal, RenderingFlags flags = RenderingFlags.None)
		{
			Render(renderTarget, (0, 0, renderTarget.Width, renderTarget.Height), orientation, flags);
		}

		public (double X, double Y) DeviceToPage((int left, int top, int width, int height) displayArea, int deviceX, int deviceY, PageOrientations orientation = PageOrientations.Normal)
		{
			(var left, var top, var width, var height) = displayArea;
			PDFium.FPDF_DeviceToPage(Handle, left, top, width, height, orientation, deviceX, deviceY, out var x, out var y);
			return (x, y);
		}

		public (int X, int Y) PageToDevice((int left, int top, int width, int height) displayArea, double pageX, double pageY, PageOrientations orientation = PageOrientations.Normal)
		{
			(var left, var top, var width, var height) = displayArea;
			PDFium.FPDF_PageToDevice(Handle, left, top, width, height, orientation, pageX, pageY, out var x, out var y);
			return (x, y);
		}

		public FlattenResults Flatten(FlattenFlags flags) => PDFium.FPDFPage_Flatten(Handle, flags);

		public RenderingStatus StartProgressiveRender(PDFiumBitmap bitmap, int x, int y, int width, int height, PageOrientations rotate, RenderingFlags flags, byte[] userData)
		{
			this._pause.userData = userData;
			return PDFium.FPDF_RenderPageBitmap_Start(bitmap.Handle, this.Handle, x, y, width, height, rotate, flags, _pause);
		}

		public RenderingStatus StartProgressiveRender(PDFiumBitmap bitmap, Point location, Size size, PageOrientations rotate, RenderingFlags flags, byte[] userData)
		{
			return this.StartProgressiveRender(bitmap, location.X, location.Y, size.Width, size.Height, rotate, flags, userData);
		}

		public RenderingStatus StartProgressiveRender(PDFiumBitmap bitmap, Rectangle rect, PageOrientations rotate, RenderingFlags flags, byte[] userData)
		{
			return StartProgressiveRender(bitmap, rect.X, rect.Y, rect.Width, rect.Height, rotate, flags, userData);
		}

		public RenderingStatus ContinueProgressiveRender()
		{
			return PDFium.FPDF_RenderPage_Continue(Handle, _pause);
		}

		public void CancelProgressiveRender()
		{
			PDFium.FPDF_RenderPage_Close(Handle);
		}

		//public void Dispose() => ((IDisposable)this).Dispose();

        protected override void Dispose(FPDF_PAGE handle)
		{
			if(IsDisposed)
            {
				return;
            }

			if(_text != null)
            {
				((IDisposable)_text).Dispose();
            }

			if (!handle.IsNull)
			{
				PDFium.FPDF_ClosePage(handle);
			}
		}

		protected virtual void OnProgressiveRender(ProgressiveRenderEventArgs e)
		{
            ProgressiveRender?.Invoke(this, e);
        }

		protected virtual void OnLoaded()
		{
            Loaded?.Invoke(this, EventArgs.Empty);
        }

		protected virtual void OnDisposed()
		{
            Disposed?.Invoke(this, EventArgs.Empty);
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
        public void Render(Graphics graphics, float dpiX, float dpiY, Rectangle bounds, bool forPrinting)
        {
            Render(graphics, dpiX, dpiY, bounds, forPrinting ? RenderingFlags.Printing : RenderingFlags.None);
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
        public void Render(Graphics graphics, float dpiX, float dpiY, Rectangle bounds, RenderingFlags flags = RenderingFlags.None)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");
            if (this.IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

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

                PDFium.FPDF_RenderPage(dc, Handle, 0, 0, bounds.Width, bounds.Height, 0, FlagsToFPDFFlags(flags));

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
        public Image Render(float dpiX, float dpiY, bool forPrinting)
        {
            return Render((int)Width, (int)Height, dpiX, dpiY, forPrinting);
        }

        /// <summary>
        /// Renders a page of the PDF document to an image.
        /// </summary>
        /// <param name="page">Number of the page to render.</param>
        /// <param name="dpiX">Horizontal DPI.</param>
        /// <param name="dpiY">Vertical DPI.</param>
        /// <param name="flags">Flags used to influence the rendering.</param>
        /// <returns>The rendered image.</returns>
        public Image Render(float dpiX, float dpiY, RenderingFlags flags)
        {
            return Render((int)Width, (int)Height, dpiX, dpiY, flags);
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
        public Image Render(int width, int height, float dpiX, float dpiY, bool forPrinting)
        {
            return Render(width, height, dpiX, dpiY, forPrinting ? RenderingFlags.Printing : RenderingFlags.None);
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
        public Image Render(int width, int height, float dpiX, float dpiY, RenderingFlags flags = RenderingFlags.None)
        {
            return Render(width, height, dpiX, dpiY, 0, flags);
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
        public Image Render(int width, int height, float dpiX, float dpiY, PageOrientations rotate, RenderingFlags flags = RenderingFlags.None)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

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

                    PDFium.FPDF_RenderPageBitmap(handle, Handle, 0, 0, width, height, rotate, FlagsToFPDFFlags(flags));

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

        private RenderingFlags FlagsToFPDFFlags(RenderingFlags flags)
        {
            return (flags & ~(RenderingFlags.Transparent | RenderingFlags.CorrectFromDpi));
        }
        #endregion New Render() methods

        //public Rectangle BoundingBox
        //{
        //    get
        //    {
        //        PDFium.FPDFPageObj_GetBoundingBox(Handle, null, out int left, out int top, out int right, out int bottom);
        //        return new Rectangle(left, top, right - left, bottom - top);
        //    }
        //}

        //public Rectangle Box
        //{
        //    get
        //    {
        //        PDFium.FPDFPageObj_GetBounds(Handle, out float left, out float bottom, out float right, out float top);
        //        return new Rectangle((int)left, (int)top, (int)right - (int)left, (int)bottom - (int)top);
        //    }
        //}
    }
}
