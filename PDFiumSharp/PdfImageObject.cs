using PDFiumSharp.Enums;
using PDFiumSharp.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace PDFiumSharp
{
    public class PdfImageObject : PdfPageObject, IDisposable
    {
		private PDFiumBitmap _bitmap;

		private readonly Lazy<FPDF_BITMAP> _pdfBitmap;

		public PdfImageObject(FPDF_PAGE pageHandle, FPDF_PAGEOBJECT imageObj) : base(pageHandle, imageObj)
        {
			_pdfBitmap = new Lazy<FPDF_BITMAP>(() => PDFium.FPDFImageObj_GetBitmap(imageObj));
        }

        public bool IsDisposed
        {
            get;
            private set;
        }

		public FS_MATRIX Matrix
		{
			get
			{
				if(PDFium.FPDFImageObj_GetMatrix(PageObjectHandle, out var a, out var b, out var c, out var d, out var e, out var f))
                {
					
                }

				return new FS_MATRIX(a, b, c, d, e, f);
			}
			
		}

		public BitmapFormats Format
        {
			get
            {
				return PDFium.FPDFBitmap_GetFormat(_pdfBitmap.Value);
            }
        }


		public PDFiumBitmap Bitmap
        {
            get
            {
                PDFiumBitmap bitmap;
                try
                {
                    if (_bitmap == null)
                    {
						_bitmap = new PDFiumBitmap(_pdfBitmap.Value, Format);
                    }
					bitmap = _bitmap;
                }
				catch
                {
					bitmap = null;
                }

				return bitmap;
            }
        }

        protected virtual void Dispose(bool disposing)
		{
			if (IsDisposed)
			{
				return;
			}
            
            if (_bitmap != null)
            {
                _bitmap.Dispose();
            }

            _bitmap = null;

            IsDisposed = true;
			if (disposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		public void Dispose()
        {
			Dispose(true); 
        }

    }
}
