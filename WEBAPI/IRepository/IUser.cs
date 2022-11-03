using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI.Models;

namespace WEBAPI.IRepository.IAdapterRepository
{
    public interface IUser
    {
        Task<ServiceResponse<object>> insertUser(UserModel model);
        Task<ServiceResponse<object>> updateUser(UserModel model, string photo, int id);
    }
}
