using System;
using System.Collections.Generic;
using System.Text;

namespace PDFiumSharp.Types.BasicTypes
{
    public class PdfTypeNumber: PdfTypeBase
    {
		public int IntValue
		{
			get
			{
				return PDFium.FPDFOBJ_GetInteger(base.Handle);
			}
			set
			{
				PDFium.FPDFOBJ_SetString(base.Handle, value.ToString());
			}
		}

		public float FloatValue
		{
			get
			{

				PDFium.FPDFOBJ_GetNumber(Handle, out float num);
				return num;
			}
			set
			{
				PDFium.FPDFOBJ_SetString(base.Handle, value.ToString().Replace(",", ".").Replace(" ", ""));
			}
		}

		public bool IsInteger
		{
			get
			{
				return PDFium.FPDFNUMBER_IsInteger(base.Handle);
			}
		}

		internal PdfTypeNumber(FPDF_PAGEOBJECT handle) : base(handle)
		{
		}

		//public static PdfTypeNumber Create(int initialVal)
		//{
		//	IntPtr expr_06 = PDFium.FPDFNUMBER_CreateInt(initialVal);
		//	if (expr_06 == IntPtr.Zero)
		//	{
		//		throw new PDFiumException();
		//	}
		//	return new PdfTypeNumber(expr_06);
		//}

		//public static PdfTypeNumber Create(float initialVal)
		//{
		//	IntPtr expr_06 = PDFium.FPDFNUMBER_CreateFloat(initialVal);
		//	if (expr_06 == IntPtr.Zero)
		//	{
		//		throw new PDFiumException();
		//	}
		//	return new PdfTypeNumber(expr_06);
		//}

		public new static PdfTypeNumber Create(FPDF_PAGEOBJECT handle)
		{
			if (handle.IsNull)
			{
				throw new ArgumentException();
			}
			return new PdfTypeNumber(handle);
		}
	}
}
