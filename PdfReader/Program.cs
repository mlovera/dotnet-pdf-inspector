using System;
using System.IO;
using PdfReader;
class Program
{
    static void Main(string[] args)
    {
        // Path to the Files directory (next to the .csproj)
        string folder = Path.Combine(AppContext.BaseDirectory, "Files");

        if (!Directory.Exists(folder))
        {
            Console.WriteLine($"❌ ERROR: 'Files' directory not found at {folder}");
            Console.WriteLine("➡️  Create a folder named 'Files' next to the .csproj and put PDFs inside it.");
            return;
        }

        // Get all PDFs
        string[] pdfFiles = Directory.GetFiles(folder, "*.pdf", SearchOption.TopDirectoryOnly);

        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("⚠️ No PDF files found inside the 'Files' directory.");
            return;
        }

        Console.WriteLine($"📁 Found {pdfFiles.Length} PDF(s) in: {folder}");
        Console.WriteLine();

        var analyzer = new PdfAnalyzer();

        foreach (var pdfPath in pdfFiles)
        {
            Console.WriteLine("======================================================");
            Console.WriteLine($"📄 Processing: {Path.GetFileName(pdfPath)}");
            Console.WriteLine("======================================================");

            try
            {
                var report = analyzer.Analyze(pdfPath);
                PdfReport.Print(report);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed to process {pdfPath}: {ex.Message}");
            }

            Console.WriteLine();
        }

        Console.WriteLine("✅ Finished processing all PDF files.");
    }
}
