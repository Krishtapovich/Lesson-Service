using System.IO;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
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

        public async ValueTask<string> AddImageAsync(string name, byte[] file)
        {
            var stream = new MemoryStream(file);
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(name, stream),
                Transformation = new Transformation().Height(500).Width(500).Crop("fill")
            };
            var result = await cloudinary.UploadAsync(uploadParams);
            await stream.DisposeAsync();

            return result.SecureUrl.ToString();
        }
    }
}
