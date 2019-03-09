using System.Threading.Tasks;
using StubAPI.Models;

namespace StubAPI
{
    public interface IAuthenticationService
    {
        User Authenticate(string username, string password);        
    }

    public class AuthenticationService: IAuthenticationService
    {
        public User Authenticate(string username, string password)
        {
            if(username == "reza" && password=="ohreally!?!")
            {
                return new User(){UserName=username, Password=password, UserId=1};
            }
            else
            {
                return null;
            }
        }
    }
}