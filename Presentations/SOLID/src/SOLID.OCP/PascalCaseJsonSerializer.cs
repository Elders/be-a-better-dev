using Newtonsoft.Json;

namespace SOLID.OCP
{
    public class PascalCaseJsonSerializer : IDocumentSerializer
    {
        public string Serialize(Document doc)
        {
            return JsonConvert.SerializeObject(doc);
        }
    }
}