using System.IO;
using System.Threading.Tasks;
using Domain.Models.Survey;

namespace Application.CloudStorage
{
    public interface ICloudStorage
    {
        ValueTask<ImageModel> UploadImageAsync(string fileName, Stream file);
        ValueTask DeleteImageAsync(string fileName);
    }
}
