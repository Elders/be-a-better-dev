using System.Xml.Linq;

namespace SOLID.SRP
{
    public class InputParser
    {
        public Document Parse(string input)
        {
            var xdoc = XDocument.Parse(input);
            var doc = new Document
            {
                Title = xdoc.Root.Element("title").Value,
                Text = xdoc.Root.Element("text").Value
            };

            return doc;
        }
    }
}
