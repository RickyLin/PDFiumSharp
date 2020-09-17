using PDFiumSharp.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace PDFiumSharp
{
    public class PdfShadingObject : PdfPageObject
    {
        public PdfShadingObject(FPDF_PAGE pageHandle, FPDF_PAGEOBJECT pageObjectHandle) : base(pageHandle, pageObjectHandle)
        {
        }
    }
}
