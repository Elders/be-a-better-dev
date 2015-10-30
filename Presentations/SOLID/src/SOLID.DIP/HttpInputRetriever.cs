using System;
using System.Net;

namespace SOLID.DIP
{
    public class HttpInputRetriever : IInputRetriever
    {
        public string GetData(string sourceFileName)
        {
            if (sourceFileName.StartsWith("http", StringComparison.Ordinal) == false)
            {
                throw new InvalidOperationException();
            }

            var client = new WebClient();

            var input = client.DownloadString(sourceFileName);

            return input;
        }
    }
}
