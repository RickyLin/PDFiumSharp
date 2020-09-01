using PDFiumSharp.Events;
using System;
using System.Runtime.InteropServices;

namespace PDFiumSharp.Types
{
	[StructLayout(LayoutKind.Sequential)]
    public class IFSDK_PAUSE
    {
		private readonly int Version;

		[MarshalAs(UnmanagedType.FunctionPtr)]
		public NeedToPauseNowCallback needToPauseNowCallback;

		[MarshalAs(UnmanagedType.SafeArray)]
		public byte[] userData;

		public IFSDK_PAUSE(byte[] userData = null)
		{
			Version = 1;
			this.userData = userData;
		}
    }
}
