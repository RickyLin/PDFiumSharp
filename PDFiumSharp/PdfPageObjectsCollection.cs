using PDFiumSharp.Internals;
using PDFiumSharp.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PDFiumSharp
{
    public class PdfPageObjectsCollection: IList<PdfPageObject>, ICollection<PdfPageObject>, IEnumerable<PdfPageObject>, IEnumerable, IDisposable
    {
        private readonly PdfPage _pdfPage;
        //private readonly FPDF_FORMHANDLE _formContent;
        private readonly PageObjectManager _pageObjectManager;
        //private readonly List<FPDF_OBJECTTYPE> _listObjs;
        private readonly FPDF_PAGEOBJECT _formObject;

        internal PdfPageObjectsCollection(PdfPage pdfPage)
        {
            _pdfPage = pdfPage ?? throw new ArgumentNullException(nameof(pdfPage));
            _pageObjectManager = new PageObjectManager();
            //_formContent = FPDF_FORMHANDLE.Null;
            //_listObjs = new List<FPDF_OBJECTTYPE>();

        }

        internal PdfPageObjectsCollection(FPDF_PAGEOBJECT formObject)
        {
            if(formObject.IsNull)
            {
                throw new ArgumentNullException(nameof(formObject));
            }
            _formObject = formObject;
        }

        public bool IsDisposed
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the object of type <see cref="PdfPageObject"/> by given object index.
        /// </summary>
        /// <param name="index">The index to get object.</param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"/>
        /// <exception cref="NotImplementedException">When try to set an <see cref="PdfPageObject"/>.</exception>
        /// <exception cref="PDFiumException"/>
        public PdfPageObject this[int index]
        {
            get
            {
                if(index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException();
                }

                FPDF_PAGEOBJECT pageObj = _pdfPage != null
                    ? PDFium.FPDFPage_GetObject(_pdfPage.Handle, index)
                    : PDFium.FPDFFormObj_GetObject(_formObject, index);

                if (pageObj.IsNull)
                {
                    throw new PDFiumException();
                }

                return _pageObjectManager.Create(_pdfPage.Handle, pageObj);
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int Count
        {
            get
            {
                return _pdfPage != null
                    ? PDFium.FPDFPage_CountObjects(_pdfPage.Handle)
                    : PDFium.FPDFFormObj_CountObjects(_formObject);
            }
        }

        public bool IsReadOnly => false;

        public void Add(PdfPageObject item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            _pageObjectManager.Clear();
        }

        public bool Contains(PdfPageObject item)
        {
            return IndexOf(item) >= 0; ;
        }

        public void CopyTo(PdfPageObject[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.IsDisposed)
            {
                return;
            }
            //if (this._formContentShouldBeDeleted)
            //{
            //    Pdfium.FPDFFormContent_Delete(this._formContent);
            //}
            for (int i = Count - 1; i >= 0; i--)
            {
                IDisposable disposable = this[i] as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            this.Clear();
            this.IsDisposed = true;
            if (disposing)
            {
                GC.SuppressFinalize(this);
            }
        }

        public IEnumerator<PdfPageObject> GetEnumerator()
        {
            return new CollectionEnumerator<PdfPageObject>(this);
        }

        public int IndexOf(PdfPageObject item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i] == item)
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, PdfPageObject item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(PdfPageObject item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
