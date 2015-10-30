using System.IO;

namespace SOLID.ISP
{
    public class FileDocumentStorage : DocumentStorage
    {
        public override string GetData(string sourceFileName)
        {
            string input;
            using (var stream = File.OpenRead(sourceFileName))
            using (var reader = new StreamReader(stream))
            {
                input = reader.ReadToEnd();
            }

            return input;
        }

        public override void Persist(string serializedDoc, string targetFileName)
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
