using Newtonsoft.Json;

namespace SOLID.SRP
{
    public class DocumentSerializer
    {
        public string Serialize(Document doc)
        {
            var serializedDoc = JsonConvert.SerializeObject(doc);

            return serializedDoc;
        }
    }
}
