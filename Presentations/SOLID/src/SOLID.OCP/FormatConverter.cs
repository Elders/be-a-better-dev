using System;
using System.Configuration;
using System.IO;

namespace SOLID.OCP
{
    public class FormatConverter
    {
        private readonly InputParser inputParser;
        private readonly IDocumentSerializer documentSerializer;

        public FormatConverter()
            : this(new InputParser(), new CamelCaseJsonSerializer())
        {

        }

        public FormatConverter(InputParser inputParser, IDocumentSerializer documentSerializer)
        {
            this.inputParser = inputParser;
            this.documentSerializer = documentSerializer;
        }

        public bool Convert(string sourceFileName, string targetFileName)
        {
            string input;

            var documentStorage = GetDocumentStorageForFileName(sourceFileName);

            try
            {
                input = documentStorage.GetData(sourceFileName);
            }
            catch (FileNotFoundException)
            {
                return false;
            }

            var doc = inputParser.Parse(input);
            var serializedDoc = documentSerializer.Serialize(doc);

            try
            {
                documentStorage.Persist(serializedDoc, targetFileName);
            }
            catch (AccessViolationException)
            {
                return false;
            }

            return true;
        }

        private DocumentStorage GetDocumentStorageForFileName(string fileName)
        {
            if (IsBlobStorageUrl(fileName))
                return new BlobDocumentStorage();

            if (fileName.StartsWith("http", StringComparison.Ordinal))
                return new HttpInputRetriever();

            return new FileDocumentStorage();
        }

        private bool IsBlobStorageUrl(string str)
        {
            var storageAccount = ConfigurationManager.AppSettings["storageAccount"];

            return str.StartsWith($"https://{storageAccount}.blob.core.windows.net/", StringComparison.Ordinal);
        }
    }
}
