using PDFiumSharp.Types;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace PDFiumSharp.Events
{
	[return: MarshalAs(UnmanagedType.Bool)]
	public delegate bool NeedToPauseNowCallback([MarshalAs(UnmanagedType.LPStruct)] IFSDK_PAUSE pThis);
}
