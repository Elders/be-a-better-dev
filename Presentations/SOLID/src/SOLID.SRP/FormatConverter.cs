using System;
using System.IO;

namespace SOLID.SRP
{
    public class FormatConverter
    {
        private readonly DocumentStorage documentStorage;
        private readonly InputParser inputParser;
        private readonly DocumentSerializer documentSerializer;

        public FormatConverter()
            : this(new DocumentStorage(), new InputParser(), new DocumentSerializer())
        {

        }

        public FormatConverter(DocumentStorage documentStorage, InputParser inputParser, DocumentSerializer documentSerializer)
        {
            this.documentStorage = documentStorage;
            this.inputParser = inputParser;
            this.documentSerializer = documentSerializer;
        }

        public bool Convert(string sourceFileName, string targetFileName)
        {
            string input;

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
    }
}
