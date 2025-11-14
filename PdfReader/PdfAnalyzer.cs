using System;
using System.Collections.Generic;
using System.Linq;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace PdfReader
{
    public class PdfAnalyzer
    {
        public PdfAnalysisResult Analyze(string path)
        {
            var result = new PdfAnalysisResult(path);

            using var pdf = PdfDocument.Open(path);

            result.TotalPages = pdf.NumberOfPages;

            foreach (var page in pdf.GetPages())
            {
                // Extract text
                string text = page.Text?.Trim() ?? "";

                // Extract images
                var images = page.GetImages(); // IReadOnlyList<IPdfImage>

                // Extract words
                var words = page.GetWords().ToList();

                // Extract letters
                int letterCount = page.Letters?.Count ?? 0;

                var info = new PdfPageInfo
                {
                    PageNumber = page.Number,
                    Text = text,
                    ImagesCount = images.Count(),        // ✔ works
                    WordsCount = words.Count,          // added for better structure inspection
                    LettersCount = letterCount,        // added for detailed analysis
                };

                result.Pages.Add(info);
            }

            return result;
        }
    }

    public class PdfAnalysisResult
    {
        public string FilePath { get; }
        public int TotalPages { get; set; }

        public List<PdfPageInfo> Pages { get; } = new();

        public PdfAnalysisResult(string path)
        {
            FilePath = path;
        }
    }

    public class PdfPageInfo
    {
        public int PageNumber { get; set; }
        public string Text { get; set; } = "";
        public int ImagesCount { get; set; }

        // New helpers to understand the structure
        public int WordsCount { get; set; }
        public int LettersCount { get; set; }
    }
}
