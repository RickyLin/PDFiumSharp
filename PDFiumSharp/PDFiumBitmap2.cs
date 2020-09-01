//using PDFiumSharp.Enums;
//using PDFiumSharp.Types;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Text;

//namespace PDFiumSharp
//{
//    public class PDFiumBitmap2: IDisposable
//    {
//		public int Width => PDFium.FPDFBitmap_GetWidth(Handle);
//		public int Height => PDFium.FPDFBitmap_GetHeight(Handle);
//		public int Stride => PDFium.FPDFBitmap_GetStride(Handle);
//		public IntPtr Scan0 => PDFium.FPDFBitmap_GetBuffer(Handle);
//		public BitmapFormats Format { get; }
//		public int BytesPerPixel => GetBytesPerPixel(Format);

//		private readonly Bitmap bitmap;

//		PDFiumBitmap2(FPDF_BITMAP bitmap, BitmapFormats format)
//			: base(bitmap)
//		{
//			if (bitmap.IsNull)
//				throw new PDFiumException();
			
//			Format = format;
//		}

//		static int GetBytesPerPixel(BitmapFormats format)
//		{
//			if (format == BitmapFormats.BGR)
//				return 3;
//			if (format == BitmapFormats.BGRA || format == BitmapFormats.BGRx)
//				return 4;
//			if (format == BitmapFormats.Gray)
//				return 1;
//			throw new ArgumentOutOfRangeException(nameof(format));
//		}

//        public void Dispose()
//        {
//            throw new NotImplementedException();
//        }

//        /// <summary>
//        /// Creates a new <see cref="PDFiumBitmap"/>. Unmanaged memory is allocated which must
//        /// be freed by calling <see cref="Dispose"/>.
//        /// </summary>
//        /// <param name="width">The width of the new bitmap.</param>
//        /// <param name="height">The height of the new bitmap.</param>
//        /// <param name="hasAlpha">A value indicating wheter the new bitmap has an alpha channel.</param>
//        /// <remarks>
//        /// A bitmap created with this overload always uses 4 bytes per pixel.
//        /// Depending on <paramref name="hasAlpha"/> the <see cref="Format"/> is then either
//        /// <see cref="BitmapFormats.BGRA"/> or <see cref="BitmapFormats.BGRx"/>.
//        /// </remarks>
//        public PDFiumBitmap2(int width, int height, bool hasAlpha)
//			: this(PDFium.FPDFBitmap_Create(width, height, hasAlpha), hasAlpha ? BitmapFormats.BGRA : BitmapFormats.BGRx) { }

//		/// <summary>
//		/// Creates a new <see cref="PDFiumBitmap"/> using memory allocated by the caller.
//		/// The caller is responsible for freeing the memory and that the adress stays
//		/// valid during the lifetime of the returned <see cref="PDFiumBitmap"/>. To free
//		/// unmanaged resources, <see cref="Dispose"/> must be called.
//		/// </summary>
//		/// <param name="width">The width of the new bitmap.</param>
//		/// <param name="height">The height of the new bitmap.</param>
//		/// <param name="format">The format of the new bitmap.</param>
//		/// <param name="scan0">The adress of the memory block which holds the pixel values.</param>
//		/// <param name="stride">The number of bytes per image row.</param>
//		public PDFiumBitmap2(int width, int height, BitmapFormats format, IntPtr scan0, int stride)
//			: this(PDFium.FPDFBitmap_CreateEx(width, height, format, scan0, stride), format) { }
//	}
//}
