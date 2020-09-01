using PDFiumSharp.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace PDFiumSharp
{
    public sealed class PdfText : NativeWrapper<FPDF_TEXTPAGE>
	{
		internal PdfText(FPDF_TEXTPAGE text) : base(text)
        {
			if(Handle.IsNull) 
			{
				throw new PDFiumException();
			}

        }

        protected override void Dispose(FPDF_TEXTPAGE handle)
        {
			if (!Handle.IsNull)
			{
				PDFium.FPDFText_ClosePage(Handle);
			}

			GC.SuppressFinalize(this);
		}

        public int CountChars
		{
			get
			{
				return PDFium.FPDFText_CountChars(Handle);
			}
		}

		public char GetCharacter(int index)
		{
			return PDFium.FPDFText_GetUnicode(Handle, index);
		}

		public float GetFontSize(int index)
		{
			return (float)PDFium.FPDFText_GetFontSize(Handle, index);
		}

		public FS_RECTF GetCharBox(int index)
		{
            PDFium.FPDFText_GetCharBox(Handle, index, out double left, out double right, out double bottom, out double top);
			return new FS_RECTF((float)left, (float)right, (float)bottom, (float)top);
		}

		public int GetCharIndexAtPos(float x, float y, float xTolerance, float yTolerance)
		{
			return PDFium.FPDFText_GetCharIndexAtPos(Handle, x, y, xTolerance, yTolerance);
		}

		/// <summary>
		/// Extracts text from the page.
		/// </summary>
		/// <param name="index">Index for the start characters.</param>
		/// <param name="count">Number of characters to be extracted.</param>
		/// <returns></returns>
		public string GetText(int index, int count)
		{
			return PDFium.FPDFText_GetText(Handle, index, count);
		}

		/// <summary>
		/// Extracts text information structure from the page.
		/// </summary>
		/// <param name="index">Index for the start characters.</param>
		/// <param name="count">Number of characters to be extracted.</param>
		/// <returns></returns>
		public PdfTextInfo GetTextInfo(int index, int count)
		{
			return new PdfTextInfo(this, index, count);
		}

		public string GetBoundedText(float left, float top, float right, float bottom)
		{
			return PDFium.FPDFText_GetBoundedText(Handle, left, top, right, bottom);
		}

		public string GetBoundedText(FS_RECTF rect)
		{
			return this.GetBoundedText(rect.Left, rect.Top, rect.Right, rect.Bottom);
		}
	}
}
