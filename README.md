# PdfReader – PDF Structure Inspector

A lightweight C# console tool for inspecting PDF structure, detecting content, and comparing blank vs. filled PDFs.
This tool is especially useful when debugging auto-generated PDFs where content may be invisible or empty.

## 🚀 Features

Automatically scans all PDF files in the Files/ directory

Prints, for each PDF:
- Total pages
- Text length
- Word & letter count
- Number of images
- Page text content
- Helps you detect PDFs that are effectively “blank”
- No command-line args needed — simple dotnet run
- Uses PdfPig (free & open-source)

## 📁 Project Structure
```
PdfReader/
│
├── Files/                   # Put your PDF files here
│   ├── sample1.pdf
│   ├── sample2.pdf
│
├── Program.cs               # Entry point – scans Files/ folder
├── PdfAnalyzer.cs           # Extracts PDF structure information
├── PdfReport.cs             # Formats and prints analysis results
├── PdfReader.csproj
└── README.md
```

## 🛠 Requirements

- .NET 8.0 or later
- Windows, Linux, or macOS
- NuGet package: UglyToad.PdfPig
- Install PdfPig:

    ```shell
    dotnet add package UglyToad.PdfPig
    ```

## 📦 Installation

- Restore dependencies:
```shell
dotnet restore
```

Build the project:
```shell
dotnet build
```
## 📂 Adding PDF Files

Inside the project directory, create a folder named:

- Files

Add any .pdf files you want to analyze:
```
PdfReader/Files/
    blank.pdf
    document1.pdf
    invoice.pdf
```

## ▶️ Running the Project

Run:
```
dotnet run
```

The app will:

Automatically detect all PDFs inside Files/

Process each PDF one by one

Print structured reports to the console

Example output:

📁 Found 3 PDF(s) in: .../PdfReader/bin/Debug/net8.0/Files
```
======================================================
📄 Processing: blank.pdf
======================================================
------------- PAGE 1 -------------
Text Length:   0
Letters Count: 0
Words Count:   0
Images:        0

Text Content:
[NO TEXT]

======================================================
📄 Processing: filled.pdf
======================================================
------------- PAGE 1 -------------
Text Length:   120
Letters Count: 145
Words Count:   18
Images:        1

Text Content:
Patient: John Doe...
```

## ⚙️ Ensuring Files/ Folder Is Copied to Output

The Files folder must be included in your build output so the app can find the PDFs when running from bin/.

Your PdfReader.csproj must contain:
```xml
<ItemGroup>
  <Content Include="Files\**\*">
    <CopyToOutputDirectory>Always</CopyToOutputDirectory>
  </Content>
</ItemGroup>
```

This ensures that files are available at runtime under:
```
bin/Debug/net8.0/Files/
```
📐 Code Overview
`PdfAnalyzer.cs`

Responsible for analyzing each PDF and building a PdfAnalysisResult with:

Page text

Word count

Letter count

Images count

Example shape (simplified):
```cs
public class PdfPageInfo
{
    public int PageNumber { get; set; }
    public string Text { get; set; } = "";
    public int ImagesCount { get; set; }
    public int WordsCount { get; set; }
    public int LettersCount { get; set; }
}
```
`PdfReport.cs`

Formats and prints readable console output for each PdfAnalysisResult.

Program.cs

Locates the Files/ folder

Enumerates all *.pdf files

Uses PdfAnalyzer to analyze each PDF

Uses PdfReport to print the results

## 🧪 Detecting “Blank PDFs” (Optional Helper)

You can add a small helper method to classify a PDF as “basically empty”:
```cs
using System.Linq;

bool IsBasicallyEmpty(PdfAnalysisResult pdf)
{
    return pdf.Pages.All(p =>
        string.IsNullOrWhiteSpace(p.Text) &&
        p.ImagesCount == 0 &&
        p.LettersCount == 0 &&
        p.WordsCount == 0
    );
}
```

You can then call this per file after analysis to quickly decide if the PDF has meaningful content or not.

## 📈 Possible Future Enhancements

Export analysis results to JSON or CSV

Compare two PDFs side by side

Highlight structural differences between PDFs

Colored console output for better readability

Save reports into a /Reports directory

Heuristics to distinguish scanned-image PDFs vs. digital-text PDFs

## 📝 License

This project is intended for debugging, testing, and internal development use.
You are free to modify or extend it according to your needs.