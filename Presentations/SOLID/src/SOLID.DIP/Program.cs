using System;
using System.Configuration;

namespace SOLID.DIP
{
    class Program
    {
        static void Main(string[] args)
        {
            var sourceFileName = "InputFile.xml";
            var targetFileName = "OutputFile.json";

            var documentSerializer = new CamelCaseJsonSerializer();
            var inputParser = new XMLInputParser();

            var formatConverter = new FormatConverter(inputParser, documentSerializer);
            if (formatConverter.Convert(sourceFileName, targetFileName) == false)
            {
                Console.WriteLine("Conversion failed.");
                Console.ReadLine();
            }
        }

        private static void ConfigureStorage()
        {
            var blobStorage = new BlobDocumentStorage();
            var fileStorage = new FileDocumentStorage();
            var httpInputRetriever = new HttpInputRetriever();

            InputRetriever.RegisterInputRetriever(x => x.StartsWith("http", StringComparison.Ordinal), httpInputRetriever);
            InputRetriever.RegisterInputRetriever(IsBlobStorageUrl, blobStorage);
            InputRetriever.RegisterInputRetriever(x => true, fileStorage);

            DocumentPersister.RegisterDocumentPersister(IsBlobStorageUrl, blobStorage);
            DocumentPersister.RegisterDocumentPersister(x => true, fileStorage);
        }

        private static bool IsBlobStorageUrl(string str)
        {
            var storageAccount = ConfigurationManager.AppSettings["storageAccount"];

            return str.StartsWith($"https://{storageAccount}.blob.core.windows.net/", StringComparison.Ordinal);
        }
    }
}
