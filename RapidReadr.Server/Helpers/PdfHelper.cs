using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;

namespace RapidReadr.Server.Helpers
{
    public class PdfHelper
    {
        public string MapPDF(string path) {
            
            PdfDocument pdf = PdfDocument.Open(path);

            //to be array
            var text = "";

            foreach (var page in pdf.GetPages())
            {
                text = text + string.Join(" ", page.GetWords()) + " "; 
            }

            return text;
        }
    }
}
