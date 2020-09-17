using PDFiumSharp.Enums;
using PDFiumSharp.Events;
using PDFiumSharp.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PDFiumSharp
{
    public abstract class PdfPageObject
    {
        public readonly FPDF_PAGEOBJECT PageObjectHandle;
        protected readonly FPDF_PAGE PageHandle;

        protected PdfPageObject(FPDF_PAGE pageHandle, FPDF_PAGEOBJECT pageObjectHandle)
        {
            PageHandle = pageHandle;
            PageObjectHandle = pageObjectHandle;
        }

        public static PdfPageObject Create(FPDF_PAGE page, FPDF_PAGEOBJECT pageObj)
        {
            switch (PDFium.FPDFPageObj_GetType(pageObj))
            {
                case PageObjTypes.Text:
                    return new PdfTextObject(page, pageObj);
                case PageObjTypes.Path:
                    return new PdfPathObject(page, pageObj);
                case PageObjTypes.Image:
                    return new PdfImageObject(page, pageObj);
                case PageObjTypes.Shading:
                    return new PdfShadingObject(page, pageObj);
                case PageObjTypes.Form:
                    return new PdfFormObject(page, pageObj);
                default:
                    return new PdfUnknownObject(page, pageObj);
            }
        }

        public bool IsTransparency
        {
            get
            {
                return PDFium.FPDFPageObj_HasTransparency(PageObjectHandle);
            }
        }

        public PageObjTypes ObjectType
        {
            get
            {
                return PDFium.FPDFPageObj_GetType(PageObjectHandle);
            }
        }


        //public PdfMarkedContentCollection MarkedContent
        //{
        //    get;
        //    private set;
        //}

        //public Rectangle BoundingBox
        //{
        //    get
        //    {
        //        PDFium.FPDFPageObj_GetBoundingBox(Handle, null, out int left, out int top, out int right, out int bottom);
        //        return new Rectangle(left, top, right - left, bottom - top);
        //    }
        //}

        public FS_RECTF BoundingBox
        {
            get
            {
                PDFium.FPDFPageObj_GetBounds(PageObjectHandle, out float left, out float bottom, out float right, out float top);
                return new FS_RECTF(left, top, right, bottom);
            }
        }

        public Color FillColor
        {
            get
            {

                PDFium.FPDFPageObj_GetFillColor(PageObjectHandle, out uint R, out uint G, out uint B, out uint A);

                return Color.FromArgb((int)A, (int)R, (int)G, (int)B);
            }
            set
            {
                PDFium.FPDFPageObj_SetFillColor(PageObjectHandle, value.R, value.G, value.B, value.A);
            }
        }

        public Color StrokeColor
        {
            get
            {
                PDFium.FPDFPageObj_GetStrokeColor(PageObjectHandle, out uint R, out uint G, out uint B, out uint A);

                return Color.FromArgb((int)A, (int)R, (int)G, (int)B);
            }
            set
            {
                PDFium.FPDFPageObj_SetStrokeColor(PageObjectHandle, value.R, value.G, value.B, value.A);
            }
        }


    }
}
