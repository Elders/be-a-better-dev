using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace SOLID.Refactored
{
    class Program
    {
        static void Main(string[] args)
        {
            var sourceFileName = "InputFile.xml";
            var targetFileName = "OutputFile.json";

            var input = GetInput(sourceFileName);
            var doc = GetDocument(input);
            var serializedDoc = SerializeDocument(doc);
            PersistDocument(serializedDoc, targetFileName);
        }

        private static string GetInput(string sourceFileName)
        {
            string input;
            using (var stream = File.OpenRead(sourceFileName))
            using (var reader = new StreamReader(stream))
            {
                input = reader.ReadToEnd();
            }

            return input;
        }

        private static Document GetDocument(string input)
        {
            var xdoc = XDocument.Parse(input);
            var doc = new Document
            {
                Title = xdoc.Root.Element("title").Value,
                Text = xdoc.Root.Element("text").Value
            };

            return doc;
        }

        private static string SerializeDocument(Document doc)
        {
            var serializedDoc = JsonConvert.SerializeObject(doc);

            return serializedDoc;
        }

        private static void PersistDocument(string serializedDoc, string targetFileName)
        {
            using (var stream = File.Open(targetFileName, FileMode.Create, FileAccess.Write))
            using (var sw = new StreamWriter(stream))
            {
                sw.Write(serializedDoc);
                sw.Close();
            }
        }
    }
}
