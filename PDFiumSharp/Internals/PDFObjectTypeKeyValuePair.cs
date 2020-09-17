using PDFiumSharp.Enums;
using PDFiumSharp.Types.BasicTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PDFiumSharp.Internals
{
    internal struct PDFPageObjectTypeKeyValuePair
    {
        public PdfPageObject obj;

        public PageObjTypes objType;
    }
}
