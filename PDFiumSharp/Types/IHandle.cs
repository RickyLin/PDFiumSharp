using System;

namespace PDFiumSharp.Types
{
    public interface IHandle<T>
    {
		bool IsNull { get; }

        IntPtr IntPtr { get; }

        T SetToNull();
    }
}
