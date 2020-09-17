using PDFiumSharp.Enums;
using PDFiumSharp.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace PDFiumSharp
{
    public class PdfPathObject : PdfPageObject
    {
        public PdfPathObject(FPDF_PAGE pageHandle, FPDF_PAGEOBJECT pathObject) : base(pageHandle, pathObject)
        {
			
		}

		public PathFillModes FillTMode
		{
			get
			{
				PDFium.FPDFPath_GetDrawMode(PageObjectHandle, out var fillModes, out var isStroke);
				return fillModes;
			}
			
		}

		public bool IsStroke
		{
			get
			{
				PDFium.FPDFPath_GetDrawMode(PageObjectHandle, out var fillModes, out var isStroke);
				return isStroke;
			}
			
		}

		public FS_MATRIX Matrix
		{
			get
			{
				FS_MATRIX matrix = new FS_MATRIX();
				PDFium.FPDFPath_GetMatrix(PageObjectHandle, ref matrix);

				return matrix;
			}
		}
	}
}
