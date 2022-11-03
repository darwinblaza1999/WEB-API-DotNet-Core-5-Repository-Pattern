using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI.Models;

namespace WEBAPI.Helper
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (AutorizeModel)context.HttpContext.Items["Users"];
            var tokenexpiry = (bool)context.HttpContext.Items["token_expiry"];
            if (tokenexpiry)
            {
                context.Result = new JsonResult(new { Message = "Token Expired" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else if (user == null)
            {
                context.Result = new JsonResult(new { Message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
