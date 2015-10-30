using System;
using System.Net;

namespace SOLID.OCP
{
    public class HttpInputRetriever : DocumentStorage
    {
        public override string GetData(string sourceFileName)
        {
            if (sourceFileName.StartsWith("http", StringComparison.Ordinal) == false)
            {
                throw new InvalidOperationException();
            }

            var client = new WebClient();

            var input = client.DownloadString(sourceFileName);

            return input;
        }

        public override void Persist(string serializedDoc, string targetFileName)
        {
            throw new NotImplementedException();
        }
    }
}
