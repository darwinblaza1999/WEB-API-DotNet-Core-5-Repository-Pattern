using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI.Models;

namespace WEBAPI.IRepository
{
    public interface IAuthenticate
    {
        Task<ServiceResponse<object>> getUser(string username, string password);
        Task<ServiceResponse<object>> RefreshToken(TokenModel token);
    }
}
