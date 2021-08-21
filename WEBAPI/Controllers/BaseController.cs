using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI.Helper;
using WEBAPI.Models;

namespace WEBAPI.Controllers
{
    [Controller]
    [Authorize]
    public abstract class BaseController : ControllerBase
    {
        public UserModel _user => (UserModel)HttpContext.Items["Users"];
    }
}
