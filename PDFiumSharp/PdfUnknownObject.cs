using PDFiumSharp.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace PDFiumSharp
{
    public class PdfUnknownObject: PdfPageObject
    {
        public PdfUnknownObject(FPDF_PAGE page, FPDF_PAGEOBJECT handle): base(page, handle)
        {

        }
    }
}
