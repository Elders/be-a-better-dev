using System;
using System.IO;

namespace SOLID.DIP
{
    public class FormatConverter
    {
        private readonly IInputParser inputParser;
        private readonly IDocumentSerializer documentSerializer;

        public FormatConverter()
            : this(new XMLInputParser(), new CamelCaseJsonSerializer())
        {

        }

        public FormatConverter(IInputParser inputParser, IDocumentSerializer documentSerializer)
        {
            this.inputParser = inputParser;
            this.documentSerializer = documentSerializer;
        }

        public bool Convert(string sourceFileName, string targetFileName)
        {
            string input;

            var inputRetriever = InputRetriever.ForFileName(sourceFileName);

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
                var documentPersister = DocumentPersister.ForFileName(targetFileName);
                documentPersister.Persist(serializedDoc, targetFileName);
            }
            catch (AccessViolationException)
            {
                return false;
            }

            return true;
        }
    }
}
