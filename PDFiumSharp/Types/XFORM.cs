using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace PDFiumSharp.Types
{
    [StructLayout(LayoutKind.Sequential)]
    public struct XFORM
    {
        public float eM11;
        public float eM12;
        public float eM21;
        public float eM22;
        public float eDx;
        public float eDy;
    }
}
