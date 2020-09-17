#region Copyright and License
/*
This file is part of PDFiumSharp, a wrapper around the PDFium library for the .NET framework.
Copyright (C) 2017 Tobias Meyer
License: Microsoft Reciprocal License (MS-RL)
*/
#endregion
using PDFiumSharp.Types.BasicTypes;
using System.Runtime.InteropServices;

namespace PDFiumSharp.Types
{
	[StructLayout(LayoutKind.Sequential)]
    public struct FS_MATRIX
    {
		public float A { get; }
		public float B { get; }
		public float C { get; }
		public float D { get; }
		public float E { get; }
		public float F { get; }

		public FS_MATRIX(float a, float b, float c, float d, float e, float f)
		{
			A = a;
			B = b;
			C = c;
			D = d;
			E = e;
			F = f;
		}

		public FS_MATRIX(double a, double b, double c, double d, double e, double f)
		{
			A = (float)a;
			B = (float)b;
			C = (float)c;
			D = (float)d;
			E = (float)e;
			F = (float)f;
		}

		//public FS_MATRIX(PdfTypeBase array)
		//{
		//	PdfTypeArray pdfTypeArray = array.As<PdfTypeArray>();
		//	int count = pdfTypeArray.Count;
		//	A = ((count > 0) ? pdfTypeArray[0].As<PdfTypeNumber>().FloatValue : 0f);
		//	B = ((count > 1) ? pdfTypeArray[1].As<PdfTypeNumber>().FloatValue : 0f);
		//	C = ((count > 2) ? pdfTypeArray[2].As<PdfTypeNumber>().FloatValue : 0f);
		//	D = ((count > 3) ? pdfTypeArray[3].As<PdfTypeNumber>().FloatValue : 0f);
		//	E = ((count > 4) ? pdfTypeArray[4].As<PdfTypeNumber>().FloatValue : 0f);
		//	F = ((count > 5) ? pdfTypeArray[5].As<PdfTypeNumber>().FloatValue : 0f);
		//}

		//public void SetIdentity()
		//{
		//	PDFium.FPDFMatrix_SetIdentity(this);
		//}

		//public void SetReverse()
		//{
		//	FS_MATRIX reverse = new FS_MATRIX(this.a, this.b, this.c, this.d, this.e, this.f);
		//	this.SetReverse(reverse);
		//}

		//public void SetReverse(FS_MATRIX matrix)
		//{
		//	PDFium.FPDFMatrix_SetReverse(this, matrix);
		//}

		//public void Concat(FS_MATRIX matrix, bool prepended = false)
		//{
		//	PDFium.FPDFMatrix_Concat(this, matrix, prepended);
		//}

		//public void ConcatInverse(FS_MATRIX matrix, bool prepended = false)
		//{
		//	PDFium.FPDFMatrix_ConcatInverse(this, matrix, prepended);
		//}

		//public void Rotate(float angle, bool prepended = false)
		//{
		//	PDFium.FPDFMatrix_Rotate(this, angle, prepended);
		//}

		//public void Rotate(float angle, float x, float y, bool prepended = false)
		//{
		//	PDFium.FPDFMatrix_RotateAt(this, angle, x, y, prepended);
		//}

		//public void Scale(float sx, float sy, bool prepended = false)
		//{
		//	PDFium.FPDFMatrix_Scale(this, sx, sy, prepended);
		//}

		//public void Translate(float x, float y, bool prepended = false)
		//{
		//	PDFium.FPDFMatrix_Translate(this, x, y, prepended);
		//}

		//public void Translate(int x, int y, bool prepended = false)
		//{
		//	PDFium.FPDFMatrix_Translate(this, x, y, prepended);
		//}

		//public void Shear(float shearX, float shearY, bool prepended = false)
		//{
		//	PDFium.FPDFMatrix_Shear(this, shearX, shearY, prepended);
		//}

		//public void TransformPoint(ref float x, ref float y)
		//{
		//	PDFium.FPDFMatrix_TransformPoint(this, ref x, ref y);
		//}

		//public void TransformPoint(ref int x, ref int y)
		//{
		//	PDFium.FPDFMatrix_TransformPoint(this, ref x, ref y);
		//}

		//public float TransformDistance(float dx, float dy)
		//{
		//	return PDFium.FPDFMatrix_TransformDistance(this, dx, dy);
		//}

		//public int TransformDistance(int dx, int dy)
		//{
		//	return PDFium.FPDFMatrix_TransformDistance(this, dx, dy);
		//}

		//public void TransformDistance(ref float distance)
		//{
		//	PDFium.FPDFMatrix_TransformDistance(this, ref distance);
		//}

		//public void TransformXDistance(ref float dx)
		//{
		//	PDFium.FPDFMatrix_TransformXDistance(this, ref dx);
		//}

		//public void TransformXDistance(ref int dx)
		//{
		//	PDFium.FPDFMatrix_TransformXDistance(this, ref dx);
		//}

		//public void TransformYDistance(ref float dy)
		//{
		//	PDFium.FPDFMatrix_TransformYDistance(this, ref dy);
		//}

		//public void TransformYDistance(ref int dy)
		//{
		//	PDFium.FPDFMatrix_TransformYDistance(this, ref dy);
		//}

		//public void TransformRect(ref FS_RECTF rect)
		//{
		//	PDFium.FPDFMatrix_TransformRect(this, ref rect);
		//}

		//public void TransformVector(ref float vx, ref float vy)
		//{
		//	PDFium.FPDFMatrix_TransformVector(this, ref vx, ref vy);
		//}

		//public float GetUnitArea()
		//{
		//	return PDFium.FPDFMatrix_GetUnitArea(this);
		//}

		//public float GetXUnit()
		//{
		//	return PDFium.FPDFMatrix_GetXUnit(this);
		//}

		//public float GetYUnit()
		//{
		//	return PDFium.FPDFMatrix_GetYUnit(this);
		//}

		//public bool Is90Rotated()
		//{
		//	return PDFium.FPDFMatrix_Is90Rotated(this);
		//}

		//public bool IsIdentity()
		//{
		//	return PDFium.FPDFMatrix_IsIdentity(this);
		//}

		//public bool IsInvertible()
		//{
		//	return PDFium.FPDFMatrix_IsInvertible(this);
		//}

		//public bool IsScaled()
		//{
		//	return PDFium.FPDFMatrix_IsScaled(this);
		//}

		//public FS_MATRIX GetReverse()
		//{
		//	FS_MATRIX expr_05 = new FS_MATRIX();
		//	expr_05.SetReverse(this);
		//	return expr_05;
		//}
	}
}
