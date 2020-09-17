using System;
using System.Collections.Generic;
using System.Text;

namespace PDFiumSharp.Types.BasicTypes
{
	public class EventArgs<T> : EventArgs
	{
		public T Value
		{
			get;
			set;
		}

		public EventArgs(T value)
		{
			this.Value = value;
		}
	}
}
