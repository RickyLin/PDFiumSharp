using System;
using System.Collections.Generic;
using System.Text;

namespace PDFiumSharp.Events
{
	public class ProgressiveRenderEventArgs : EventArgs
	{
		public byte[] UserData
		{
			get;
			private set;
		}

		public bool NeedPause
		{
			get;
			set;
		}

		public ProgressiveRenderEventArgs(byte[] userData)
		{
			this.UserData = userData;
		}
	}
}
