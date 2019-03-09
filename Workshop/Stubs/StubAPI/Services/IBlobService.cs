using System.Threading.Tasks;
using StubAPI.Models;

namespace StubAPI.Services
{
    public interface IBlobService{
        Task<bool> UploadToBlob(ApiMessage payload, string containerName);
    }
}