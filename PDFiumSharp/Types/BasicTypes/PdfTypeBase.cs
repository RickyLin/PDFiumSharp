using PDFiumSharp.Enums;
using PDFiumSharp.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace PDFiumSharp.Types.BasicTypes
{
    public abstract class PdfTypeBase: NativeWrapper<FPDF_PAGEOBJECT> 
	{
		
		public ObjectTypes ObjectType
		{
			get
			{
				return PDFium.FPDFOBJ_GetType(Handle);
			}
		}

		public int ObjectNumber
		{
			get
			{
				return PDFium.FPDFOBJ_GetObjNum(Handle);
			}
		}

		public int GenerationNumber
		{
			get
			{
				return PDFium.FPDFOBJ_GetGenNum(Handle);
			}
		}

		protected PdfTypeBase(FPDF_PAGEOBJECT handle): base(handle)
		{
			//_onDestroyEvent += new EventHandler<EventArgs<IntPtr>>(this.PdfTypeBase__onDestroyEvent);
			//PDFium.FPDFOBJ_SetDestroyCallback(Handle, _onDestroyCallback);
		}

        public static PdfTypeBase Create(FPDF_PAGEOBJECT handle)
        {
            if (handle.IsNull)
            {
                return null;
            }

            switch (PDFium.FPDFOBJ_GetType(handle))
            {
                case ObjectTypes.Boolean:
                    return new PdfTypeBoolean(handle);
                case ObjectTypes.Number:
                    return new PdfTypeNumber(handle);
                //case ObjectTypes.String:
                //    return new PdfTypeString(handle);
                //case ObjectTypes.Name:
                //    return new PdfTypeName(handle);
                //case ObjectTypes.Array:
                //    return new PdfTypeArray(handle);
                //case ObjectTypes.Dictionary:
                //    return new PdfTypeDictionary(handle);
                //case ObjectTypes.Stream:
                //    return new PdfTypeStream(handle);
                //case ObjectTypes.Null:
                //    return new PdfTypeNull(handle);
                //case ObjectTypes.Reference:
                //    return new PdfTypeIndirect(handle);
                default:
                    //return new PdfTypeUnknown(handle);
                    return null;
            }
        }

        //~PdfTypeBase()
        //{
        //	this.Dispose(false);
        //}

        //public void Dispose()
        //{
        //	this.Dispose(true);
        //}

        //protected virtual void Dispose(bool disposing)
        //{
        //	if (IsDisposed || Handle.IsNull)
        //	{
        //		return;
        //	}
        //	if (PDFium.FPDFOBJ_GetParentObj(Handle) != IntPtr.Zero)
        //	{
        //		return;
        //	}
        //	Pdfium.FPDFOBJ_Release(this._handle);
        //	PdfTypeBase._onDestroyEvent -= new EventHandler<EventArgs<IntPtr>>(this.PdfTypeBase__onDestroyEvent);
        //	this._handle = IntPtr.Zero;
        //	this.IsDisposed = true;
        //	if (disposing)
        //	{
        //		GC.SuppressFinalize(this);
        //	}
        //}

        //public PdfTypeBase Clone(bool bDirect = false)
        //{
        //	IntPtr intPtr = Pdfium.FPDFOBJ_Clone(this.Handle, bDirect);
        //	if (intPtr == IntPtr.Zero)
        //	{
        //		return null;
        //	}
        //	return PdfTypeBase.Create(intPtr);
        //}

        //public T As<T>() where T : PdfTypeBase
        //{
        //	if (this is T)
        //	{
        //		return this as T;
        //	}
        //	if (this is PdfTypeIndirect)
        //	{
        //		PdfTypeBase direct = (this as PdfTypeIndirect).Direct;
        //		if (direct is T)
        //		{
        //			return direct as T;
        //		}
        //	}
        //	throw new UnexpectedTypeException(typeof(T), base.GetType());
        //}

        //public bool Is<T>() where T : PdfTypeBase
        //{
        //	return this is T || (this is PdfTypeIndirect && (this as PdfTypeIndirect).Direct is T);
        //}

        //internal PdfTypeArray AsArrayOf<T>() where T : PdfTypeBase
        //{
        //	if (this is PdfTypeArray)
        //	{
        //		return this as PdfTypeArray;
        //	}
        //	if (this is PdfTypeIndirect)
        //	{
        //		PdfTypeBase direct = (this as PdfTypeIndirect).Direct;
        //		if (direct is PdfTypeArray)
        //		{
        //			return direct as PdfTypeArray;
        //		}
        //	}
        //	if (this is T)
        //	{
        //		PdfTypeArray pdfTypeArray = PdfTypeArray.Create();
        //		if (typeof(T) == typeof(PdfTypeName))
        //		{
        //			pdfTypeArray.Add(this.Clone(false));
        //		}
        //		else
        //		{
        //			if (!(typeof(T) == typeof(PdfTypeDictionary)))
        //			{
        //				throw new NotImplementedException("Type of " + typeof(T).Name + " not implemented");
        //			}
        //			pdfTypeArray.Add(this.Clone(false));
        //		}
        //		return pdfTypeArray;
        //	}
        //	throw new UnexpectedTypeException(typeof(T), base.GetType());
        //}

        //private static void OnDestroyCalback(IntPtr handle)
        //{
        //	if (_onDestroyEvent != null)
        //	{
        //		_onDestroyEvent(null, new EventArgs<FPDF_PAGEOBJECT>(handle));
        //	}
        //}

        //private void PdfTypeBase__onDestroyEvent(object sender, EventArgs<FPDF_PAGEOBJECT> e)
        //{
        //	if (e.Value == Handle)
        //	{
        //		OnDestroy();
        //		this.IsDisposed = true;
        //		this._handle = IntPtr.Zero;
        //		_onDestroyEvent -= new EventHandler<EventArgs<FPDF_PAGEOBJECT>>(this.PdfTypeBase__onDestroyEvent);
        //	}
        //}

        //protected virtual void OnDestroy()
        //{
        //}

        //static PdfTypeBase()
        //{
        //	// Note: this type is marked as 'beforefieldinit'.
        //	PdfTypeBase._onDestroyEvent = null;
        //	PdfTypeBase._onDestroyCallback = new InternalObjectDestroyCallback(PdfTypeBase.OnDestroyCalback);
        //}
    }
}
