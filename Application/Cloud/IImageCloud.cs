using System.Threading.Tasks;
using Domain.Models.Survey;

namespace Application.Cloud
{
    public interface IImageCloud
    {
        ValueTask<Image> AddImageAsync(string name, byte[] file);
        ValueTask DeleteImageAsync(string id);
    }
}
