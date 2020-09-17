//using PDFiumSharp.Enums;
//using PDFiumSharp.Types.BasicTypes;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace PDFiumSharp
//{
//    internal class ObjectTypeManager
//    {
//		private Dictionary<IntPtr, MgrIntObjVal> _list = new Dictionary<IntPtr, MgrIntObjVal>();

//		public PdfTypeBase Create(IntPtr Handle)
//		{
//			if (Handle == IntPtr.Zero)
//			{
//				return null;
//			}
//			ObjectTypes indirectObjectTypes = PDFium.FPDFOBJ_GetType(Handle);
//			if (this._list.ContainsKey(Handle))
//			{
//				MgrIntObjVal mgrIntObjVal = this._list[Handle];
//				if (!mgrIntObjVal.obj.IsDisposed && mgrIntObjVal.objType == indirectObjectTypes)
//				{
//					return mgrIntObjVal.obj;
//				}
//				this._list.Remove(Handle);
//			}
//			MgrIntObjVal mgrIntObjVal2 = new MgrIntObjVal
//			{
//				obj = PdfTypeBase.Create(Handle),
//				objType = indirectObjectTypes
//			};
//			this._list.Add(Handle, mgrIntObjVal2);
//			return mgrIntObjVal2.obj;
//		}

//		internal void Add(PdfTypeBase item)
//		{
//			if (item == null)
//			{
//				return;
//			}
//			if (this._list.ContainsKey(item.Handle))
//			{
//				return;
//			}
//			MgrIntObjVal value = new MgrIntObjVal
//			{
//				obj = item,
//				objType = item.ObjectType
//			};
//			this._list.Add(item.Handle, value);
//		}

//		internal void Remove(IntPtr handle)
//		{
//			if (this._list.ContainsKey(handle))
//			{
//				this._list.Remove(handle);
//			}
//		}

//		internal void Clear()
//		{
//			this._list.Clear();
//		}
//	}
//}
