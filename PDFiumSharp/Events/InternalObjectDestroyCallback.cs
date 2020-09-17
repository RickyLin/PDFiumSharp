using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace PDFiumSharp.Events
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl, SetLastError = true)]
    public delegate void InternalObjectDestroyCallback(IntPtr obj_handle);
}
