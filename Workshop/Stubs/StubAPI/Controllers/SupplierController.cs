using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using StubAPI.Services;
using StubAPI.Models;

namespace StubAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IBlobService blobService;

        public SupplierController(IBlobService blobService)
        {
            this.blobService = blobService;
        }

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
            var successfullyUploaded = await blobService.UploadToBlob(payload, containerName);
            if(successfullyUploaded){
                return StatusCode(202);
            }
            else
            {
                return StatusCode(500);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Supplier/NoAuth")]
        public async Task<IActionResult> NoAuth([FromBody] ApiMessage payload)
        {
            var containerName = payload.TargetName;
            return await blobService.UploadToBlob(payload, containerName) ? StatusCode(202) : StatusCode(500);
        }

        
    }
}
