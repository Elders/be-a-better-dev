using System.Configuration;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace SOLID.ISP
{
    public class BlobDocumentStorage : DocumentStorage
    {
        private readonly CloudBlobClient cloudBlobClient;
        private readonly string container;

        public BlobDocumentStorage()
        {
            this.container = ConfigurationManager.AppSettings["blobStorageContainer"];
            var account = CloudStorageAccount.FromConfigurationSetting(ConfigurationManager.AppSettings["DataConnectionString"]);
            this.cloudBlobClient = account.CreateCloudBlobClient();
        }

        public override string GetData(string sourceFileName)
        {
            var cloudBlobContainer = cloudBlobClient.GetContainerReference(container);
            var blob = cloudBlobContainer.GetBlobReference(sourceFileName);

            return blob.DownloadText();
        }

        public override void Persist(string serializedDoc, string targetFileName)
        {
            var cloudBlobContainer = cloudBlobClient.GetContainerReference(container);
            cloudBlobContainer.CreateIfNotExist();

            var blob = cloudBlobContainer.GetBlobReference(targetFileName);
            blob.UploadText(serializedDoc);
        }
    }
}
