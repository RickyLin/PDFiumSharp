using PDFiumSharp.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace PDFiumSharp
{
    public sealed class PdfText : NativeWrapper<FPDF_TEXTPAGE>
	{
		internal PdfPage PdfPage { get; }
		internal PdfText(FPDF_TEXTPAGE text, PdfPage page) : base(text)
        {
			if(Handle.IsNull) 
			{
				throw new PDFiumException();
			}

			PdfPage = page ?? throw new ArgumentNullException(nameof(page));
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

		/// <summary>
		/// Get the font size of a particular character.
		/// </summary>
		/// <param name="index">Zero-based index of the character.</param>
		/// <returns>
		/// The font size of the particular character, measured in points (about
		/// 1/72 inch). This is the typographic size of the font (so called "em size").
		/// </returns>
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

		/// <summary>
		/// Get the font name and flags of a particular character.
		/// </summary>
		/// <param name="charIndex">Zero-based index of the character.</param>
		/// <returns></returns>
		public string GetFontNameInfo(int charIndex)
        {
			// buffer[] is in UTF - 8 encoding.Return 0 on failure.
			byte[] buffer = new byte[1024 * 768 * 4];
			var length = PDFium.FPDFText_GetFontInfo(this.Handle, charIndex, buffer, (uint)buffer.Length, out int fontFlags);

			if(length > 0)
            {
				return Encoding.UTF8.GetString(buffer);
            }

			return string.Empty;
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
