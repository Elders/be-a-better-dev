using System;
using System.Configuration;
using System.IO;

namespace SOLID.ISP
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

            var inputRetriever = GetInputRetrieverForFileName(sourceFileName);

            try
            {
                input = inputRetriever.GetData(sourceFileName);
            }
            catch (FileNotFoundException)
            {
                return false;
            }

            var doc = inputParser.Parse(input);
            var serializedDoc = documentSerializer.Serialize(doc);

            try
            {
                var documentPersister = GetDocumentPersisterForFileName(targetFileName);
                documentPersister.Persist(serializedDoc, targetFileName);
            }
            catch (AccessViolationException)
            {
                return false;
            }

            return true;
        }

        private IDocumentPersister GetDocumentPersisterForFileName(string fileName)
        {
            if (IsBlobStorageUrl(fileName))
                return new BlobDocumentStorage();

            if (fileName.StartsWith("http", StringComparison.Ordinal))
                throw new NotSupportedException();

            return new FileDocumentStorage();
        }

        private IInputRetriever GetInputRetrieverForFileName(string fileName)
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
