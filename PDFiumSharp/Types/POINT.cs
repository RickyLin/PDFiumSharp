using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace PDFiumSharp.Types
{
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;
    }
}
