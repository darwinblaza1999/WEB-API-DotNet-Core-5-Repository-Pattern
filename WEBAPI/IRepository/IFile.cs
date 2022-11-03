using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.IRepository
{
    public interface IFile
    {
        Task<Models.ServiceResponse<object>> uploadFile();
    }
}
