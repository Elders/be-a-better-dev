using Newtonsoft.Json;

namespace SOLID.ISP
{
    public class PascalCaseJsonSerializer : IDocumentSerializer
    {
        public string Serialize(Document doc)
        {
            return JsonConvert.SerializeObject(doc);
        }
    }
}