using System.Threading.Tasks;

namespace Application.Cloud
{
    public interface IImageCloud
    {
        ValueTask<string> AddImageAsync(string name, byte[] file);
    }
}
