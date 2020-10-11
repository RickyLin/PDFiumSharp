$textTransformPath = .\vswhere.exe -latest -find Common7\IDE\TextTransform.exe | select-object -first 1
if ($textTransformPath) {
    Write-Output $msBuildPath
}
else {
    Write-Error "No TextTransform.exe was found."
    Return
}

& $textTransformPath "PDFiumSharp\PDFium.tt"
& $textTransformPath "PDFiumSharp\Types\FPDF_Typedefs.tt"
