using System.Threading.Tasks;
using StubAPI.Models;

namespace StubAPI.Services
{
    public interface IAuthenticationService
    {
        User Authenticate(string username, string password);        
    }
}