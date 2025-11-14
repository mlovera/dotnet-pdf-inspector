using System;

namespace PdfReader
{
    public static class PdfReport
    {
        public static void Print(PdfAnalysisResult result)
        {
            Console.WriteLine("===========================================");
            Console.WriteLine($"PDF FILE: {result.FilePath}");
            Console.WriteLine("===========================================");
            Console.WriteLine();

            Console.WriteLine($"Total Pages: {result.TotalPages}");
            Console.WriteLine();

            foreach (var page in result.Pages)
            {
                Console.WriteLine($"------------- PAGE {page.PageNumber} -------------");
                Console.WriteLine($"Text Length:   {page.Text.Length}");
                Console.WriteLine($"Letters Count: {page.LettersCount}");
                Console.WriteLine($"Words Count:   {page.WordsCount}");
                Console.WriteLine($"Images:        {page.ImagesCount}");
                Console.WriteLine();

                Console.WriteLine("Text Content:");
                Console.WriteLine(string.IsNullOrWhiteSpace(page.Text)
                    ? "[NO TEXT]"
                    : page.Text);
                Console.WriteLine();
            }

            Console.WriteLine(); // final blank line
        }
    }
}
