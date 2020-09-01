using System;
using System.Collections.Generic;
using System.Text;

namespace PDFiumSharp.Types
{
    [Flags]
    public enum FPDF
    {
        ANNOT = 0x01,
        LCD_TEXT = 0x02,
        NO_NATIVETEXT = 0x04,
        GRAYSCALE = 0x08,
        DEBUG_INFO = 0x80,
        NO_CATCH = 0x100,
        RENDER_LIMITEDIMAGECACHE = 0x200,
        RENDER_FORCEHALFTONE = 0x400,
        PRINTING = 0x800,
        REVERSE_BYTE_ORDER = 0x10
    }
}
