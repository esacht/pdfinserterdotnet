using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PdfInsertionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string mainPdfPath = @"C:\Users\stuartm\Downloads\CHSPAddresses.pdf";
            string insertedPdfPath = @"C:\Users\stuartm\Downloads\NoticeToClients.pdf";
            string outputPdfPath = @"C:\Users\stuartm\Downloads\output.pdf";

            InsertPdfAfterEveryPage(mainPdfPath, insertedPdfPath, outputPdfPath);

            Console.WriteLine($"Inserted {insertedPdfPath} after every page in {mainPdfPath}. Output saved as {outputPdfPath}.");
        }

        static void InsertPdfAfterEveryPage(string mainPdfPath, string insertedPdfPath, string outputPdfPath)
        {
            using (var mainReader = new PdfReader(mainPdfPath))
            using (var insertedReader = new PdfReader(insertedPdfPath))
            using (var outputStream = new FileStream(outputPdfPath, FileMode.Create))
            {
                var outputDocument = new Document();
                var writer = new PdfCopy(outputDocument, outputStream);
                outputDocument.Open();

                int pageCount = mainReader.NumberOfPages;
                for (int i = 1; i <= pageCount; i++)
                {
                    writer.AddPage(writer.GetImportedPage(mainReader, i));
                    for (int j = 0; j < 5; j++)
                    {
                        writer.AddPage(writer.GetImportedPage(insertedReader, j + 1)); // Insert 5 pages from the inserted PDF
                    }
                }

                outputDocument.Close();
            }
        }
    }
}
