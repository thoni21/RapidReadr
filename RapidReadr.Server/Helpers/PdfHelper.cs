using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;

namespace RapidReadr.Server.Helpers
{
    public class PdfHelper
    {
        public string MapPDF(string path) {
            
            PdfDocument pdf = PdfDocument.Open(path);

            var text = "";

            foreach (Page page in pdf.GetPages())
            {
                text += ContentOrderTextExtractor.GetText(page) + " ";
            }

            return text;
        }
    }
}
