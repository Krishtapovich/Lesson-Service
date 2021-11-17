using System.IO;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Models.Survey;
using Microsoft.Extensions.Options;

namespace Application.Cloud
{
    public class ImageCloud : IImageCloud
    {
        private readonly Cloudinary cloudinary;

        public ImageCloud(IOptions<CloudinarySettings> options)
        {
            var account = new Account(options.Value.CloudName, options.Value.ApiKey, options.Value.ApiSecret);
            cloudinary = new Cloudinary(account);
        }

        public async ValueTask<Image> AddImageAsync(string name, byte[] file)
        {
            var stream = new MemoryStream(file);
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(name, stream),
                Transformation = new Transformation().Height(500).Width(500).Crop("fill")
            };
            var result = await cloudinary.UploadAsync(uploadParams);
            await stream.DisposeAsync();

            return new Image { CloudId = result.PublicId, Url = result.Url.ToString() }; 
        }

        public async ValueTask DeleteImageAsync(string id)
        {
            var deleteParams = new DeletionParams(id);
            await cloudinary.DestroyAsync(deleteParams);
        }
    }
}
