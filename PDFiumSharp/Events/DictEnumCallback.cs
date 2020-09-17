using PDFiumSharp.Types;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace PDFiumSharp.Events
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void DictEnumCallback([MarshalAs(UnmanagedType.LPStr)] string key, FPDF_PAGEOBJECT value);
}
