//using PDFiumSharp.Internals;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Text;

//namespace PDFiumSharp.Types.BasicTypes
//{
//    public class PdfTypeDictionary: PdfTypeBase, IDictionary<string, PdfTypeBase>, ICollection<KeyValuePair<string, PdfTypeBase>>, IEnumerable<KeyValuePair<string, PdfTypeBase>>, IEnumerable
//    {
//        private readonly PageObjectManager _manager = new PageObjectManager();

//        protected PdfTypeDictionary(FPDF_PAGEOBJECT handle) : base(handle)
//        {
//        }

//        public static PdfTypeDictionary Create()
//        {
//            var dict = PDFium.FPDFDICT_Create();
//            if (dict.IsNull)
//            {
//                throw new PDFiumException();
//            }
//            return new PdfTypeDictionary(dict);
//        }

//        public new static PdfTypeDictionary Create(FPDF_PAGEOBJECT handle)
//        {
//            if (handle.IsNull)
//            {
//                throw new ArgumentNullException(nameof(handle));
//            }

//            return new PdfTypeDictionary(handle);
//        }

//        public bool IsSignature
//        {
//            get
//            {
//                return PDFium.FPDFDICT_IsSignatureDict(Handle);
//            }
//        }


//        public PdfTypeBase this[string key] 
//        {
//            get
//            {
//                if (key == null)
//                {
//                    throw new ArgumentNullException("key");
//                }
//                if (!ContainsKey(key))
//                {
//                    throw new KeyNotFoundException();
//                }
//                return this._manager.Create(PDFium.FPDFDICT_GetObjectBy(Handle, key));
//            }
//            set
//            {
//                if (key == null)
//                {
//                    throw new ArgumentNullException("key");
//                }
//                if (value == null)
//                {
//                    throw new ArgumentNullException("value");
//                }

//                var handle = ((value != null) ? value.Handle : FPDF_OBJECTTYPE.Null) as IHandle<FPDF_OBJECTTYPE>;
//                PDFium.FPDFDICT_SetAt(Handle, key, handle.IntPtr);
//                _manager.Add(value);
//            }
//        }

//        public ICollection<string> Keys
//        {
//            get
//            {
//                List<string> keys = new List<string>();
//                PDFium.FPDFDICT_Enum(Handle, new Events.DictEnumCallback((string key, FPDF_OBJECTTYPE value) => {
//                    keys.Add(key);
//                }));
//                return keys;
//            }
//        }

//        public ICollection<PdfTypeBase> Values
//        {
//            get
//            {
//                List<PdfTypeBase> list = new List<PdfTypeBase>();
//                PDFium.FPDFDICT_Enum(Handle, new Events.DictEnumCallback((string key, FPDF_OBJECTTYPE value) => {
//                    list.Add(_manager.Create(Handle));
//                }));
//                return list;
//            }
//        }

//        public int Count => PDFium.FPDFDICT_GetCount(Handle);

//        public bool IsReadOnly => false;

//		public PdfTypeBase GetBy(string key)
//		{
//			return this[key];
//		}

//		public void SetAt(string key, PdfTypeBase value)
//		{
//			this[key] = value;
//		}

//		public FS_MATRIX GetMatrixBy(string key)
//		{
//			if (key == null)
//			{
//				throw new ArgumentNullException(nameof(key));
//			}
//			var matrix = new FS_MATRIX();

//			PDFium.FPDFDICT_GetMatrixBy(Handle, key, matrix);

//			return matrix;
//		}

//		public void SetMatrixAt(string key, FS_MATRIX matrix)
//		{
//			if (key == null)
//			{
//				throw new ArgumentNullException(nameof(key));
//			}
//			PDFium.FPDFDICT_SetAtMatrix(Handle, key, matrix);
//		}

//		public FS_RECTF GetRectBy(string key)
//		{
//			if (key == null)
//			{
//				throw new ArgumentNullException(nameof(key));
//			}
//			var rect = new FS_RECTF();

//			PDFium.FPDFDICT_GetRectBy(Handle, key, out rect);

//			return rect;
//		}

//		public void SetRectAt(string key, FS_RECTF rect)
//		{
//			if (key == null)
//			{
//				throw new ArgumentNullException(nameof(key));
//			}
//			PDFium.FPDFDICT_SetAtRect(Handle, key, ref rect);
//		}

//		public float GetRealBy(string key)
//		{
//			if (key == null)
//			{
//				throw new ArgumentNullException(nameof(key));
//			}

//			PDFium.FPDFDICT_GetNumberBy(Handle, key, out float number);

//			return number;
//		}

//		public void SetRealAt(string key, float value)
//		{
//			if (key == null)
//			{
//				throw new ArgumentNullException(nameof(key));
//			}
//			PDFium.FPDFDICT_SetAtNumber(Handle, key, value);
//		}

//		public int GetIntegerBy(string key)
//		{
//			if (key == null)
//			{
//				throw new ArgumentNullException(nameof(key));
//			}
//			return PDFium.FPDFDICT_GetIntegerBy(Handle, key);
//		}

//		public void SetIntegerAt(string key, int value)
//		{
//			if (key == null)
//			{
//				throw new ArgumentNullException(nameof(key));
//			}
//			PDFium.FPDFDICT_SetAtInteger(Handle, key, value);
//		}

//		public bool GetBooleanBy(string key)
//		{
//			if (key == null)
//			{
//				throw new ArgumentNullException(nameof(key));
//			}
//			return PDFium.FPDFDICT_GetBooleanBy(Handle, key, false);
//		}

//		public void SetBooleanAt(string key, bool value)
//		{
//			if (key == null)
//			{
//				throw new ArgumentNullException(nameof(key));
//			}
//			PDFium.FPDFDICT_SetAtBoolean(Handle, key, value);
//		}

//		public string GetStringBy(string key)
//		{
//			if (key == null)
//			{
//				throw new ArgumentNullException(nameof(key));
//			}
//			string result;

//			byte[] array = null;
//			int num = PDFium.FPDFDICT_GetStringBy(Handle, key, array, 0);
//			if (num <= 0)
//			{
//				result = null;
//			}
//			else
//			{
//				array = new byte[num];
//				num = PDFium.FPDFDICT_GetStringBy(Handle, key, array, num);
//				if (num <= 0)
//				{
//					result = null;
//				}
//				else
//				{
//					result = ASCIIEncoding.Default.GetString(array).Trim(new char[1]);
//				}
//			}

//			return result;
//		}

//		public string GetUnicodeBy(string key)
//		{
//			if (key == null)
//			{
//				throw new ArgumentNullException(nameof(key));
//			}

//			string result;
//			byte[] array = null;
//			int num = PDFium.FPDFDICT_GetUnicodeTextBy(Handle, key, array, 0);
//			if (num <= 0)
//			{
//				result = null;
//			}
//			else
//			{
//				array = new byte[num];
//				num = PDFium.FPDFDICT_GetUnicodeTextBy(Handle, key, array, num);
//				if (num <= 0)
//				{
//					result = null;
//				}
//				else
//				{
//					result = Encoding.Unicode.GetString(array).Trim(new char[1]);
//				}
//			}

//			return result;
//		}

//		public void SetStringAt(string key, string text)
//		{
//			if (key == null)
//			{
//				throw new ArgumentNullException(nameof(key));
//			}
//			PDFium.FPDFDICT_SetAtString(Handle, key, text);
//		}

//		public void SetNameAt(string key, string name)
//		{
//			if (key == null)
//			{
//				throw new ArgumentNullException(nameof(key));
//			}
//			PDFium.FPDFDICT_SetAtName(Handle, key, name);
//		}

//		/*public void SetIndirectAt(string key, PdfIndirectList list, int objectNumber)
//		{
//			if (key == null)
//			{
//				throw new ArgumentNullException(nameof(key));
//			}
//			if (list == null)
//			{
//				throw new ArgumentNullException("list");
//			}
//			PDFium.FPDFDICT_SetAtReference(Handle, key, list.Handle, objectNumber);
//		}

//		public void SetIndirectAt(string key, PdfIndirectList list, PdfTypeBase directObject)
//		{
//			if (key == null)
//			{
//				throw new ArgumentNullException(nameof(key));
//			}
//			if (list == null)
//			{
//				throw new ArgumentNullException("list");
//			}
//			if (directObject == null)
//			{
//				throw new ArgumentNullException("directObject");
//			}
//			PDFium.FPDFDICT_SetAtReferenceEx(Handle, key, list.Handle, directObject.Handle);
//		}*/

//		public void Add(KeyValuePair<string, PdfTypeBase> item)
//		{
//			this.Add(item.Key, item.Value);
//		}

//		public void Add(string key, PdfTypeBase value)
//		{
//			if (key == null)
//			{
//				throw new ArgumentNullException(nameof(key));
//			}
//			if (ContainsKey(key))
//			{
//				throw new ArgumentException($"An key with value \"{key}\" already presents in the list.");
//			}
//			this[key] = value;
//		}

//		public void Clear()
//		{
//			foreach (string current in this.Keys)
//			{
//				PDFium.FPDFDICT_RemoveAt(Handle, current);
//			}
//			this._manager.Clear();
//		}

//		public bool Contains(KeyValuePair<string, PdfTypeBase> item)
//		{
//			return this.ContainsKey(item.Key);
//		}

//		public bool ContainsKey(string key)
//		{
//			if (key == null)
//			{
//				throw new ArgumentNullException(nameof(key));
//			}
//			return PDFium.FPDFDICT_KeyExist(Handle, key);
//		}

//		public bool Remove(KeyValuePair<string, PdfTypeBase> item)
//		{
//			return this.Remove(item.Key);
//		}

//		public bool Remove(string key)
//		{
//			if (key == null)
//			{
//				throw new ArgumentNullException(nameof(key));
//			}
//			PdfTypeBase pdfTypeBase = this[key];
//			if (pdfTypeBase != null)
//			{
//				this._manager.Remove(pdfTypeBase.Handle);
//			}
//			PDFium.FPDFDICT_RemoveAt(Handle, key);
//			return true;
//		}

//		public bool TryGetValue(string key, out PdfTypeBase value)
//		{
//			if (key == null)
//			{
//				throw new ArgumentNullException(nameof(key));
//			}
//			value = null;
//			if (!this.ContainsKey(key))
//			{
//				return false;
//			}
//			value = this[key];
//			return true;
//		}

//		public void CopyTo(KeyValuePair<string, PdfTypeBase>[] array, int arrayIndex)
//		{
//			if (array == null)
//			{
//				throw new ArgumentNullException(nameof(array));
//			}
//			if (arrayIndex < 0)
//			{
//				throw new ArgumentOutOfRangeException(nameof(arrayIndex));
//			}
//			foreach (KeyValuePair<string, PdfTypeBase> current in this)
//			{
//				if (arrayIndex > array.Length - 1)
//				{
//					break;
//				}
//				array[arrayIndex++] = current;
//			}
//		}

//		IEnumerator IEnumerable.GetEnumerator()
//		{
//			return this.GetEnumerator();
//		}

//		public IEnumerator<KeyValuePair<string, PdfTypeBase>> GetEnumerator()
//		{
//			return new PdfDictionaryEnumerator(this);
//		}
//	}
//}
