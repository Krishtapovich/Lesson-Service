using System.IO;
using System.Threading.Tasks;
using Domain.Models.Survey;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;

namespace Application.CloudStorage
{
    public class CloudStorage : ICloudStorage
    {
        private readonly GoogleCredential googleCredential;
        private readonly StorageClient storageClient;
        private readonly string bucketName;

        public CloudStorage(IConfiguration configuration)
        {
            googleCredential = GoogleCredential.FromFile(configuration["GoogleCredentialFile"]);
            storageClient = StorageClient.Create(googleCredential);
            bucketName = configuration["GoogleCloudStorageBucket"];
        }

        public async ValueTask<ImageModel> UploadImageAsync(string fileName, Stream file)
        {
            var data = await storageClient.UploadObjectAsync(bucketName, fileName, null, file);
            return new ImageModel { FileName = fileName, Url = data.MediaLink };
        }

        public async ValueTask DeleteImageAsync(string fileName)
        {
            await storageClient.DeleteObjectAsync(bucketName, fileName);
        }
    }
}
