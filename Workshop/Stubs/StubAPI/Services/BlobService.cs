using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using StubAPI.Models;

namespace StubAPI.Services
{
    public class BlobService : IBlobService{
        public async Task<bool> UploadToBlob(ApiMessage payload, string containerName)
        {
            var payloadAsString = JsonConvert.SerializeObject(payload).ToString();
            Console.WriteLine("received message: " + payloadAsString);

            string storageConnectionString = Environment.GetEnvironmentVariable("STORAGE_CONNECTION_STRING");

            Console.WriteLine(storageConnectionString);

            CloudStorageAccount storageAccount = null;

            if (!CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                Console.WriteLine("Error parsing storageconnectionstring");
                return false;
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

            return true;
        }


    }
}