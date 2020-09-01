#region Copyright and License
/*
This file is part of PDFiumSharp, a wrapper around the PDFium library for the .NET framework.
Copyright (C) 2017 Tobias Meyer
License: Microsoft Reciprocal License (MS-RL)
*/
#endregion
using PDFiumSharp.Types;
using System;

namespace PDFiumSharp.Enums
{
	[Flags]
	public enum RenderingFlags
    {
		None = 0,

		/// <summary>
		/// Set if annotations are to be rendered.
		/// </summary>
		Annotations = FPDF.ANNOT,

		/// <summary>
		/// Set if using text rendering optimized for LCD display.
		/// </summary>
		LcdText = FPDF.LCD_TEXT,

		/// <summary>
		/// Don't use the native text output available on some platforms
		/// </summary>
		NoNativeText = FPDF.NO_NATIVETEXT,

		/// <summary>
		/// Grayscale output.
		/// </summary>
		Grayscale = FPDF.GRAYSCALE,

		/// <summary>
		/// Set if you want to get some debug info.
		/// </summary>
		DebugInfo = FPDF.DEBUG_INFO,

		/// <summary>
		/// Set if you don't want to catch exceptions.
		/// </summary>
		DontCatch = FPDF.NO_CATCH,

		/// <summary>
		/// Limit image cache size.
		/// </summary>
		LimitImageCache = FPDF.RENDER_LIMITEDIMAGECACHE,

		/// <summary>
		/// Always use halftone for image stretching.
		/// </summary>
		ForceHalftone = FPDF.RENDER_FORCEHALFTONE,

		/// <summary>
		/// Render for printing.
		/// </summary>
		Printing = FPDF.PRINTING,

		/// <summary>
		/// Set to disable anti-aliasing on text.
		/// </summary>
		NoSmoothText = 0x1000,

		/// <summary>
		/// Set to disable anti-aliasing on images.
		/// </summary>
		NoSmoothImage = 0x2000,

		/// <summary>
		/// Set to disable anti-aliasing on paths.
		/// </summary>
		NoSmoothPath = 0x4000,

		/// <summary>
		/// Set whether to render in a reverse Byte order, this flag is only used when rendering to a bitmap.
		/// </summary>
		ReverseByteOrder = 0x10,

		/// <summary>
        /// Render with a transparent background.
        /// </summary>
        Transparent = 0x6000,
        /// <summary>
        /// Correct height/width for DPI.
        /// </summary>
        CorrectFromDpi = 0x8000
	}
}
