using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace StubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnPremController : ControllerBase
    {

        CloudStorageAccount storageAccount = null;
        CloudBlobContainer cloudBlobContainer = null;

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> NoAuth([FromBody] string content)
        {
            var message = JsonConvert.DeserializeObject<ApiMessage>(content);

            string storageConnectionString = Environment.GetEnvironmentVariable("storageconnectionstring");

            if (!CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                Console.WriteLine(

                                    "A connection string has not been defined in the system environment variables. " +
                                    "Add a environment variable named 'storageconnectionstring' with your storage " +
                                    "connection string as a value.");
            }
            else{
                try
                {
                    var client = storageAccount.CreateCloudBlobClient();
                    cloudBlobContainer = client.GetContainerReference("onpremises/" + message.TargetName);
                    await cloudBlobContainer.CreateAsync();


                }
                catch (System.Exception)
                {
                    
                    throw;
                }
                finally
                {

                }
            }

            return StatusCode(202);

        }


    }
}
