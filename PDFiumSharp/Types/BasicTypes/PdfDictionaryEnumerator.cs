//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Text;

//namespace PDFiumSharp.Types.BasicTypes
//{
//	internal class PdfDictionaryEnumerator : IEnumerator<KeyValuePair<string, PdfTypeBase>>, IDisposable, IEnumerator
//	{
//		private PdfTypeDictionary _dict;

//		private ICollection<string> _keys;

//		private IEnumerator<string> _it;

//		public KeyValuePair<string, PdfTypeBase> Current
//		{
//			get
//			{
//				if (this._it.Current == null)
//				{
//					return default(KeyValuePair<string, PdfTypeBase>);
//				}
//				return new KeyValuePair<string, PdfTypeBase>(this._it.Current, this._dict[this._it.Current]);
//			}
//		}

//		object IEnumerator.Current
//		{
//			get
//			{
//				return this.Current;
//			}
//		}

//		public PdfDictionaryEnumerator(PdfTypeDictionary dictionary)
//		{
//			this._keys = dictionary.Keys;
//			this._dict = dictionary;
//			this._it = this._keys.GetEnumerator();
//		}

//		public void Dispose()
//		{
//		}

//		public bool MoveNext()
//		{
//			return this._it.MoveNext();
//		}

//		public void Reset()
//		{
//			this._it.Reset();
//		}
//	}
//}
