using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace PDFiumSharp.Types
{
	/// <summary>Handle to a PDF_HCBITMAP</summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct NATIVE_HCBITMAP : IHandle<NATIVE_HCBITMAP>
	{
		IntPtr _ptr;

		/// <summary>Gets a value indicating whether the handle is <c>null</c>.</summary>
		public bool IsNull => _ptr == IntPtr.Zero;

		public override string ToString() => "NATIVE_HCBITMAP: 0x" + _ptr.ToString("X16");

		/// <summary>Gets a handle representing <c>null</c>.</summary>
		public static NATIVE_HCBITMAP Null => new NATIVE_HCBITMAP();

		NATIVE_HCBITMAP(IntPtr ptr)
		{
			_ptr = ptr;
		}

		NATIVE_HCBITMAP IHandle<NATIVE_HCBITMAP>.SetToNull() => new NATIVE_HCBITMAP(Interlocked.Exchange(ref _ptr, IntPtr.Zero));
	}
}
