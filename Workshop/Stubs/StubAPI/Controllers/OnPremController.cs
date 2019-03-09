using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using StubAPI.Models;
using StubAPI.Services;

namespace StubAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class OnPremController : ControllerBase
    {
        private readonly IBlobService blobService;

        public OnPremController(IBlobService blobService)
        {
            this.blobService = blobService;
        }

        [HttpPost]
        [Route("OnPrem/BasicAuth")]
        public async Task<IActionResult> BasicAuth([FromBody] ApiMessage payload)
        {
            var containerName = "secureonprem";
            return await blobService.UploadToBlob(payload, containerName) ? StatusCode(202) : StatusCode(500);
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
            return await blobService.UploadToBlob(payload, containerName) ? StatusCode(202) : StatusCode(500);
        }



    }
}
