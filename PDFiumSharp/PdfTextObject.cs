using PDFiumSharp.Enums;
using PDFiumSharp.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PDFiumSharp
{
    public class PdfTextObject : PdfPageObject
    {
        private string _textUnicode;

        private string _textAscii;

        //private FPDF_TEXTPAGE _textPage;

        private PdfFont _font;

        private FS_MATRIX? _textObjectMatrix;
        private Lazy<FPDF_TEXTPAGE> pdfPageLazy;

        internal PdfTextObject(FPDF_PAGE pageHandle, FPDF_PAGEOBJECT pageObjectHandle) : base(pageHandle, pageObjectHandle)
        {
            pdfPageLazy = new Lazy<FPDF_TEXTPAGE>(() => PDFium.FPDFText_LoadPage(PageHandle));
        }

        public float CharSpacing
        {
            get
            {

                //PDFium.FPDFTextObj_GetCharSpacing(Handle, out float spacing);
                //return spacing;
                throw new NotSupportedException();
            }
            set
            {
                //PDFium.FPDFTextObj_SetCharSpacing(Handle, value);
                //RecalcPositionData();
                throw new NotSupportedException();
            }
        }

        public float WordSpacing
        {
            get
            {
                //PDFium.FPDFTextObj_GetWordSpacing(Handle, out float spacing);
                //return spacing;
                throw new NotSupportedException();
            }
            set
            {
                //PDFium.FPDFTextObj_SetWordSpacing(Handle, value);
                //RecalcPositionData();
                throw new NotSupportedException();
            }
        }

        public int CharsCount
        {
            get
            {
                //return PDFium.FPDFTextObj_CountChars(Handle);
                throw new NotSupportedException();
            }
        }

        public PdfFont Font
        {
            get
            {
                if (_font == null)
                {
                    FPDF_FONT fFont = PDFium.FPDFTextObj_GetFont(PageObjectHandle);
                    if (fFont.IsNull)
                    {
                        throw new FontNotFoundException();
                    }
                    _font = new PdfFont(fFont);
                }
                return _font;
            }
            set
            {
                /*
                if (_font != value)
                {
                    if (value == null)
                    {
                        throw new ArgumentNullException();
                    }
                    PDFium.FPDFTextObj_SetFont(Handle, value.Handle);
                    _font = value;
                } */
                throw new NotImplementedException();
            }
        }

        public float FontSize
        {
            get
            {
                return PDFium.FPDFTextObj_GetFontSize(PageObjectHandle);
            }
            set
            {
                /*PDFium.FPDFTextObj_SetFontSize(Handle, value);
                RecalcPositionData();*/
                throw new NotImplementedException();
            }
        }

        public FPDF_TEXT_RENDERMODE RenderMode
        {
            get
            {
                return PDFium.FPDFTextObj_GetTextRenderMode(PageObjectHandle);
            }
            set
            {
                //PDFium.FPDFTextObj_SetTextRenderMode(Handle, value);
                throw new NotImplementedException();
            }
        }

        //public PointF Location
        //{
        //    get
        //    {
        //        //PDFium.FPDFTextObj_GetPos(Handle, out float x, out float y);
        //        //return new PointF(x, y);
        //        throw new NotSupportedException();
        //    }
        //    set
        //    {
        //        //PDFium.FPDFTextObj_SetPosition(Handle, value.X, value.Y);
        //        throw new NotSupportedException();
        //    }
        //}

        public FS_MATRIX Matrix
        {
            get
            {
                if (_textObjectMatrix == null)
                {
                    var matrix = new FS_MATRIX();
                    if (PDFium.FPDFTextObj_GetMatrix(PageObjectHandle, ref matrix))
                    {
                        _textObjectMatrix = matrix;
                    }
                    else
                    {
                        throw new PDFiumException();
                    }
                }
                return _textObjectMatrix.Value;
            }
            set
            {
                /*PDFium.FPDFTextObj_SetTextMatrix(Handle, value);
                _textObjectMatrix = value;*/
                throw new NotImplementedException();
            }
        }

        public string TextUnicode
        {
            get
            {
                if (_textUnicode == null)
                {
                    _textUnicode = PDFium.FPDFTextObj_GetTextUnicode(PageObjectHandle, pdfPageLazy.Value);
                }
                return _textUnicode;
            }
            set
            {
                /*if (_textUnicode != value)
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        PDFium.FPDFTextObj_SetEmpty(Handle);
                    }
                    else
                    {
                        float[] kernings = new float[value.Length];
                        PDFium.FPDFText_SetText(Handle, value, kernings);
                    }
                    _textUnicode = null;
                }*/
                throw new NotImplementedException();
            }
        }

        public string TextAnsi
        {
            get
            {
                if (_textAscii == null)
                {
                    _textAscii = PDFium.FPDFTextObj_GetTextAnsi(PageObjectHandle, pdfPageLazy.Value);
                }
                return _textAscii;

            }
            set
            {
                /*if (_textAscii != value)
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        PDFium.FPDFTextObj_SetEmpty(Handle);
                    }
                    else
                    {
                        PDFium.FPDFText_SetText(Handle, value);
                    }
                    _textAscii = null;
                }*/
                throw new NotImplementedException();
            }
        }

        public void GetItemInfo(int index, out int charCode, out float originX, out float originY)
        {
            PDFium.FPDFTextObj_GetItemInfo(PageObjectHandle, index, out charCode, out originX, out originY);
        }

        public void GetCharInfo(int index, out int charCode, out float kerning)
        {
            PDFium.FPDFTextObj_GetCharInfo(PageObjectHandle, index, out charCode, out kerning);
        }

        //public void GetCharInfo(int index, out int charCode, out float originX, out float originY)
        //{
        //    PDFium.FPDFTextObj_GetCharInfo(Handle, index, out charCode, out originX, out originY);
        //}

        //public float GetCharWidth(int charCode)
        //{
        //    float result;
        //    PDFium.FPDFTextObj_GetCharWidth(Handle, charCode, out result);
        //    return result;
        //}

        public float GetSpaceWidth()
        {
            float result;
            PDFium.FPDFTextObj_GetSpaceCharWidth(PageObjectHandle, out result);
            return result;
        }

        //public FS_RECTF GetCharRect(int index)
        //{
        //    if (_textObjectMatrix == null)
        //    {
        //        PDFium.FPDFTextObj_GetMatrix(Handle, out _textObjectMatrix);
        //    }
        //    FS_RECTF result;
        //    PDFium.FPDFTextObj_GetCharRect(Handle, index, out result.left, out result.bottom, out result.right, out result.top, _textObjectMatrix);
        //    return result;
        //}

        //public FS_RECTF GetCharRect(int index, FS_MATRIX matrix)
        //{
        //    FS_RECTF result;
        //    PDFium.FPDFTextObj_GetCharRect(Handle, index, out result.left, out result.bottom, out result.right, out result.top, matrix);
        //    return result;
        //}

        //public float[] CalcCharPos()
        //{
        //    float[] array = new float[CharsCount * 2];
        //    PDFium.FPDFTextObj_CalcCharPos(Handle, array);
        //    return array;
        //}

        //public void RecalcPositionData()
        //{
        //    PDFium.FPDFTextObj_RecalcPositionData(Handle);
        //}
    }
}
