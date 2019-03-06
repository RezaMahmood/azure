using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace StubAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class OnPremController : ControllerBase
    {
        [HttpPost]
        [Route("OnPrem/BasicAuth")]
        public async Task<IActionResult> BasicAuth([FromBody] ApiMessage payload)
        {
            var containerName = "secureonprem";
            return await UploadToBlob(payload, containerName);
        }

        [HttpGet]        
        [Route("OnPrem/BasicAuth")]
        public IActionResult GetBasicAuth([FromBody] ApiMessage payload)
        {
            return Ok(payload);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("OnPrem/NoAuth")]
        public IActionResult GetNoAuth([FromBody] ApiMessage payload)
        {
            return Ok(payload);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("OnPrem/NoAuth")]
        public async Task<IActionResult> NoAuth([FromBody] ApiMessage payload)
        {
            var containerName = "onpremises";
            return await UploadToBlob(payload, containerName);
        }

        private async Task<IActionResult> UploadToBlob(ApiMessage payload, string containerName)
        {
            var payloadAsString = JsonConvert.SerializeObject(payload).ToString();
            Console.WriteLine("received message: " + payloadAsString);

            string storageConnectionString = Environment.GetEnvironmentVariable("STORAGE_CONNECTION_STRING");

            Console.WriteLine(storageConnectionString);

            CloudStorageAccount storageAccount = null;

            if (!CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                Console.WriteLine("Error parsing storageconnectionstring");
            }
            else
            {
                try
                {
                    var client = storageAccount.CreateCloudBlobClient();                    
                    var cloudBlobContainer = client.GetContainerReference(containerName);
                    var containerCreated = await cloudBlobContainer.CreateIfNotExistsAsync();

                    var fileName = DateTime.UtcNow.ToString() + ".json";

                    var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
                    await cloudBlockBlob.UploadTextAsync(payloadAsString);


                }
                catch (StorageException ex)
                {
                    Console.WriteLine("Error returned from Blob service: {0}", ex.Message);
                    throw;
                }
            }

            return StatusCode(202);
        }


    }
}
