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
    public class SupplierController : ControllerBase
    {

        [HttpGet]
        [AllowAnonymous]
        [Route("EchoHeaders")]
        public IActionResult EchoHeaders()
        {
            var headerKeys = Request.Headers.Keys;
            var response = new List<string>();
            foreach(var key in headerKeys)
            {
                response.Add(key + ":" + Request.Headers[key]);
            }

            return Ok(JsonConvert.SerializeObject(response));
        }

        [HttpGet]
        [Route("Supplier/BasicAuth")]
        public IActionResult GetBasicAuth([FromBody] ApiMessage payload)
        {
            return Ok(payload);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Supplier/NoAuth")]
        public IActionResult GetNoAuth([FromBody] ApiMessage payload)
        {
            return Ok(payload);
        }

        [HttpPost]
        [Route("Supplier/BasicAuth")]
        public async Task<IActionResult> BasicAuth([FromBody] ApiMessage payload)
        {
            var containerName = "securesupplier";
            return await UploadToBlob(payload, containerName);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Supplier/NoAuth")]
        public async Task<IActionResult> NoAuth([FromBody] ApiMessage payload)
        {
            var containerName = payload.TargetName;
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
