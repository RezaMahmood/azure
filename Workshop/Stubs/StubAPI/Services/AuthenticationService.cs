using StubAPI.Models;

namespace StubAPI.Services
{
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