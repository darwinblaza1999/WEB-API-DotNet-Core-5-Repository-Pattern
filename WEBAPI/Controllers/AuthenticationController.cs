using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI.Adapter;
using WEBAPI.IRepository.IAdapterRepository;
using WEBAPI.Models;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAdapter _adapter;
        public AuthenticationController(IAdapter adapter)
        {
            _adapter = adapter;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Authentication")]
        public async Task<IActionResult> Authentication(string username, string password)
        {
            var adapter = await _adapter.authenticate.getUser(username, password);

            return Ok(adapter);

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken(TokenModel token)
        {
            var result = await _adapter.authenticate.RefreshToken(token);
            return Ok(result);
        }
    }
}
