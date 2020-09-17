using PDFiumSharp.Enums;
using PDFiumSharp.Types;
using PDFiumSharp.Types.BasicTypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PDFiumSharp
{
    public class PdfFont: NativeWrapper<FPDF_FONT>
    {
        public PdfFont(FPDF_FONT handle): base(handle)
        {

        }

		public FontTypes FontType
		{
			get
			{
				return PDFium.FPDFFont_GetFontType(Handle);
			}
		}

		public string FontTypeName
		{
			get
			{
				string fontName;
				int num = PDFium.FPDFFont_GetFontTypeName(Handle, null, 0);
				
				if (num == 0)
				{
					fontName = "";
				}
				else
				{
					byte[] array = new byte[num];
					PDFium.FPDFFont_GetFontTypeName(Handle, array, num);
					fontName = ASCIIEncoding.ASCII.GetString(array, 0, array.Length - 1);
				}

				return fontName;
			}
		}

		public string BaseFontName
		{
			get
			{
				string fontName;

				int num = PDFium.FPDFFont_GetBaseFont(Handle, null, 0);
				if (num == 0)
				{
					fontName = "";
				}
				else
				{
					byte[] array = new byte[num];
					num = PDFium.FPDFFont_GetBaseFont(Handle, array, num);
					fontName = ASCIIEncoding.ASCII.GetString(array, 0, array.Length - 1);
				}

				return fontName;
			}
		}

		public FontFlags Flags
		{
			get
			{
				return PDFium.FPDFFont_GetFlags(Handle);
			}
		}

		public bool IsVertWriting
		{
			get
			{
				return PDFium.FPDFFont_IsVertWriting(Handle);
			}
		}

		public bool IsEmbedded
		{
			get
			{
				return PDFium.FPDFFont_IsEmbedded(Handle);
			}
		}

		public bool IsUnicodeCompatible
		{
			get
			{
				return PDFium.FPDFFont_IsUnicodeCompatible(Handle);
			}
		}

		public bool IsStandardFont
		{
			get
			{
				return PDFium.FPDFFont_IsStandardFont(Handle);
			}
		}

		public int Ascent
		{
			get
			{
				return PDFium.FPDFFont_GetTypeAscent(Handle);
			}
		}

		public int Descent
		{
			get
			{
				return PDFium.FPDFFont_GetTypeDescent(Handle);
			}
		}

		public int ItalicAngel
		{
			get
			{
				return PDFium.FPDFFont_GetItalicAngle(Handle);
			}
		}

		public int StemV
		{
			get
			{
				return PDFium.FPDFFont_GetStemV(Handle);
			}
		}

		public int Weight
		{
			get
			{
				if (StemV >= 140)
				{
					return StemV * 4 + 140;
				}
				return StemV * 5;
			}
		}

		//public PdfTypeDictionary Dictionary
		//{
		//	get
		//	{
		//		if (Handle.IsNull)
		//		{
		//			return null;
		//		}
		//		FPDF_PAGEOBJECT dictHandle = PDFium.FPDFFont_GetFontDict(Handle);

		//		if (dictHandle.IsNull)
		//		{
		//			return null;
		//		}
		//		return PdfTypeDictionary.Create(dictHandle);
		//	}
		//}

		public int GetStringWidth(string text)
		{
			if(text == null)
            {
				throw new ArgumentNullException(nameof(text));
            }

			return PDFium.FPDFFont_GetStringWidth(Handle, text, text.Length);
		}

		public int GetCharFontWidth(int charCode)
		{
			return PDFium.FPDFFont_GetCharWidthF(Handle, charCode, 0);
		}

		public int GetCharTypeWidth(int charCode, out bool isVert)
		{
			return PDFium.FPDFFont_GetCharTypeWidth(Handle, charCode, out isVert);
		}

		public Rectangle GetCharBBox(int charCode)
		{
			PDFium.FPDFFont_GetCharBBox(Handle, charCode, out int left, out int top, out int right, out int bottom, 0);
			return new Rectangle(left, top, right - left, bottom - top);
		}

		public char ToUnicode(int charCode)
		{
			return PDFium.FPDFFont_UnicodeFromCharCode(Handle, charCode);
		}

		public int ToCharCode(char unicode)
		{
			return PDFium.FPDFFont_CharCodeFromUnicode(Handle, unicode);
		}


	}
}
