using PDFiumSharp.Types;

namespace PDFiumSharp
{
    public enum PdfError
    {
        Success = (int)FPDF_ERR.SUCCESS,
        Unknown = (int)FPDF_ERR.UNKNOWN,
        CannotOpenFile = (int)FPDF_ERR.FILE,
        InvalidFormat = (int)FPDF_ERR.FORMAT,
        PasswordProtected = (int)FPDF_ERR.PASSWORD,
        UnsupportedSecurityScheme = (int)FPDF_ERR.SECURITY,
        PageNotFound = (int)FPDF_ERR.PAGE
    }
}
