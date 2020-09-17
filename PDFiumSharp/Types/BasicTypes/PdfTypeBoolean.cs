using System;
using System.Collections.Generic;
using System.Text;

namespace PDFiumSharp.Types.BasicTypes
{
    public class PdfTypeBoolean: PdfTypeBase
    {
        internal PdfTypeBoolean(FPDF_PAGEOBJECT handle) : base(handle) { }

		public bool Value
		{
			get
			{
				return PDFium.FPDFOBJ_GetInteger(Handle) == 1;
			}
			set
			{
				PDFium.FPDFOBJ_SetString(Handle, value ? "true" : "false");
			}
		}

		public static PdfTypeBoolean Create(bool bInitialVal)
		{
			FPDF_PAGEOBJECT expr_06 = PDFium.FPDFBOOLEAN_Create(bInitialVal);
			if (expr_06.IsNull)
			{
				throw new PDFiumException();
			}
			return new PdfTypeBoolean(expr_06);
		}

		public new static PdfTypeBoolean Create(FPDF_PAGEOBJECT handle)
		{
			if (handle.IsNull)
			{
				throw new ArgumentException();
			}
			return new PdfTypeBoolean(handle);
		}
	}
}
