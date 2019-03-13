using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Utilities
{
    class Program
    {
        static void Main(string[] args)
        {            
            var authContext = new AuthenticationContext("https://login.microsoftonline.com/" + args[0]);
            var result = authContext.AcquireTokenAsync("https://management.azure.com", args[1], new Uri(""+ args[2] +""), new PlatformParameters(PromptBehavior.Auto)).Result;

            if (result == null)
            {
                throw new InvalidOperationException("Failed to obtain JWT token");
            }

            Console.WriteLine(result.AccessToken);

            Console.ReadLine();

        }
    }
}
