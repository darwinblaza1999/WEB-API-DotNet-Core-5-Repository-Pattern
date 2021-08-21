using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI.Models;

namespace WEBAPI.IRepository.IAdapterRepository
{
    public interface IAuthManger
    {
        public Task<ServiceResponse<object>> authorize(HttpContext context, IAuthManger auth);
    }
}
