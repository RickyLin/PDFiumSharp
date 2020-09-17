using PDFiumSharp.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace PDFiumSharp
{
    public class PdfFormObject : PdfPageObject, IDisposable
    {
        public PdfFormObject(FPDF_PAGE pageHandle, FPDF_PAGEOBJECT formObject) : base(pageHandle, formObject)
        {
            PageObjects = new PdfPageObjectsCollection(formObject);
        }

        public void Dispose()
        {
			Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
			if (IsDisposed)
			{
				return;
			}
			if (PageObjects != null)
			{
				PageObjects.Dispose();
			}
			PageObjects = null;
			IsDisposed = true;
			if (disposing)
			{
				GC.SuppressFinalize(this);
			}
		}


        public bool IsDisposed
		{
			get;
			private set;
		}

		public PdfPageObjectsCollection PageObjects
		{
			get;
			private set;
		}

		public FS_MATRIX Matrix
        {
			get
            {
                FS_MATRIX fS_MATRIX = new FS_MATRIX();
                PDFium.FPDFFormObj_GetMatrix(PageObjectHandle, ref fS_MATRIX);
                return fS_MATRIX;
            }
        }
	}
}
