//using PDFiumSharp.Enums;
//using PDFiumSharp.Types.BasicTypes;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace PDFiumSharp
//{
//	public class PdfMarkedContentCollection : IList<PdfMarkedContent>, ICollection<PdfMarkedContent>, IEnumerable<PdfMarkedContent>, IEnumerable
//	{
//		private PdfPageObject _pageObject;

//		public PdfMarkedContent this[int index]
//		{
//			get
//			{
//				if (index < 0 || index >= this.Count)
//				{
//					throw new IndexOutOfRangeException();
//				}
//				string arg_73_0 = PDFium.FPDFPageObjMark_GetName(this._pageObject.Handle, index);
//				PropertyListTypes paramType = PDFium.FPDFPageObj_GetMarkParamType(this._pageObject.Handle, index);
//				IntPtr intPtr = PDFium.FPDFPageObj_GetMarkParam(this._pageObject.Handle, index);
//				bool hasMCID = PDFium.FPDFPageObj_MarkHasMCID(this._pageObject.Handle, index);
//				PdfTypeDictionary parameters = null;
//				if (intPtr != IntPtr.Zero)
//				{
//					parameters = new PdfTypeDictionary(intPtr);
//				}
//				return new PdfMarkedContent(arg_73_0, hasMCID, paramType, parameters);
//			}
//			set
//			{
//				throw new NotSupportedException(Error.err0051);
//			}
//		}

//		public int Count
//		{
//			get
//			{
//				return PDFium.FPDFPageObj_CountMarks(this._pageObject.Handle);
//			}
//		}

//		public bool IsReadOnly
//		{
//			get
//			{
//				return true;
//			}
//		}

//		public PdfMarkedContentCollection(PdfPageObject pageObject)
//		{
//			this._pageObject = pageObject;
//		}

//		public bool Contains(PdfMarkedContent item)
//		{
//			return this.IndexOf(item) >= 0;
//		}

//		public int IndexOf(PdfMarkedContent item)
//		{
//			for (int i = 0; i < this.Count; i++)
//			{
//				if (this[i] == item)
//				{
//					return i;
//				}
//			}
//			return -1;
//		}

//		public void Clear()
//		{
//			throw new NotSupportedException(Error.err0051);
//		}

//		public void CopyTo(PdfMarkedContent[] array, int arrayIndex)
//		{
//			if (array == null)
//			{
//				throw new ArgumentNullException("array");
//			}
//			if (arrayIndex < 0)
//			{
//				throw new ArgumentOutOfRangeException();
//			}
//			foreach (PdfMarkedContent current in this)
//			{
//				if (arrayIndex > array.Length - 1)
//				{
//					break;
//				}
//				array[arrayIndex++] = current;
//			}
//		}

//		public void Add(PdfMarkedContent item)
//		{
//			throw new NotSupportedException(Error.err0051);
//		}

//		public IEnumerator<PdfMarkedContent> GetEnumerator()
//		{
//			return new CollectionEnumerator<PdfMarkedContent>(this);
//		}

//		IEnumerator IEnumerable.GetEnumerator()
//		{
//			return this.GetEnumerator();
//		}

//		public bool Remove(PdfMarkedContent item)
//		{
//			throw new NotSupportedException(Error.err0051);
//		}

//		public void RemoveAt(int index)
//		{
//			throw new NotSupportedException(Error.err0051);
//		}

//		public void Insert(int index, PdfMarkedContent item)
//		{
//			throw new NotSupportedException(Error.err0051);
//		}
//	}
//}
