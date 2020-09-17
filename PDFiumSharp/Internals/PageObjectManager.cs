using PDFiumSharp.Enums;
using PDFiumSharp.Types;
using PDFiumSharp.Types.BasicTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PDFiumSharp.Internals
{
    internal class PageObjectManager
    {
		private Dictionary<FPDF_PAGEOBJECT, PDFPageObjectTypeKeyValuePair> _list = new Dictionary<FPDF_PAGEOBJECT, PDFPageObjectTypeKeyValuePair>();

		public PdfPageObject Create(FPDF_PAGE page, FPDF_PAGEOBJECT pageObject)
		{
			if (pageObject.IsNull)
			{
				return null;
			}

			PageObjTypes pageObjectType = GetPageObjectType(pageObject);
			
			if (_list.ContainsKey(pageObject))
			{
				PDFPageObjectTypeKeyValuePair mgrIntObjVal = _list[pageObject];
				if (mgrIntObjVal.objType == pageObjectType)
				{
					return mgrIntObjVal.obj;
				}
				_list.Remove(pageObject);
			}

			PDFPageObjectTypeKeyValuePair mgrIntObjVal2 = new PDFPageObjectTypeKeyValuePair
			{
				obj = PdfPageObject.Create(page, pageObject),
				objType = pageObjectType
			};
			this._list.Add(pageObject, mgrIntObjVal2);

			return mgrIntObjVal2.obj;
		}

		internal PageObjTypes GetPageObjectType(FPDF_PAGEOBJECT pageObject)
        {
			return PDFium.FPDFPageObj_GetType(pageObject);
        }

		internal void Add(PdfPageObject item)
		{
			if (item == null)
			{
				return;
			}
			if (this._list.ContainsKey(item.PageObjectHandle))
			{
				return;
			}
			PDFPageObjectTypeKeyValuePair value = new PDFPageObjectTypeKeyValuePair
			{
				obj = item,
				objType = item.ObjectType
			};
			this._list.Add(item.PageObjectHandle, value);
		}

		internal void Remove(FPDF_PAGEOBJECT handle)
		{
			if (this._list.ContainsKey(handle))
			{
				this._list.Remove(handle);
			}
		}

		internal void Clear()
		{
			this._list.Clear();
		}
	}
}
