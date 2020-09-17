#region Copyright and License
/*
This file is part of PDFiumSharp, a wrapper around the PDFium library for the .NET framework.
Copyright (C) 2017 Tobias Meyer
License: Microsoft Reciprocal License (MS-RL)
*/
#endregion
using System;

namespace PDFiumSharp.Enums
{
	public enum FontTypes
    {
		Type1 = 1,
		TrueType,
		Type3,
		CIDFont
	}

	[Flags]
	public enum FontSubstFlags
	{
		FXFONT_SUBST_MM = 1,
		FXFONT_SUBST_GLYPHPATH = 4,
		FXFONT_SUBST_CLEARTYPE = 8,
		FXFONT_SUBST_TRANSFORM = 16,
		FXFONT_SUBST_NONSYMBOL = 32,
		FXFONT_SUBST_EXACT = 64,
		FXFONT_SUBST_STANDARD = 128
	}

	[Flags]
	public enum FontFlags
	{
		FixedPitch = 1,
		Serif = 2,
		Symbolic = 4,
		Script = 8,
		NonSymbolic = 32,
		Italic = 64,
		AllCap = 65536,
		SmallCap = 131072,
		ForceBold = 262144,
		UseExternalTR = 524288
	}
}
