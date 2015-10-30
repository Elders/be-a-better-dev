using Newtonsoft.Json;

namespace SOLID.DIP
{
    public class PascalCaseJsonSerializer : IDocumentSerializer
    {
        public string Serialize(Document doc)
        {
            return JsonConvert.SerializeObject(doc);
        }
    }
}