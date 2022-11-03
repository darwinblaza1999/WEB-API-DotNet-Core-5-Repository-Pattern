using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI.IRepository.IAdapterRepository;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class FileController : BaseController
    {
        private readonly IAdapter _adapter;
        public FileController(IAdapter adapter)
        {
            _adapter = adapter;
        }
        [HttpPost]
        [Route("UploadFile")]
        public async Task<IActionResult> UFile()
        {
            return Ok();
        }
    }
}
