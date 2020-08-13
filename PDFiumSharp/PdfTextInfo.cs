using PDFiumSharp.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace PDFiumSharp
{
    public class PdfTextInfo
    {
		public string Text
		{
			get;
			private set;
		}

		public ReadOnlyList<FS_RECTF> Rects
		{
			get;
			private set;
		}

		internal PdfTextInfo(PdfText textPage, int index, int count)
		{
			Text = PDFium.FPDFText_GetText(textPage.Handle, index, count);
			int num = PDFium.FPDFText_CountRects(textPage.Handle, index, count);
			Rects = new ReadOnlyList<FS_RECTF>();
			for (int i = 0; i < num; i++)
			{
				PDFium.FPDFText_GetRect(textPage.Handle, i, out double left, out double top, out double right, out double bottom);
				Rects.Add(new FS_RECTF((float)left, (float)top, (float)right, (float)bottom));
			}
		}
	}
}
