using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI.IRepository;
using WEBAPI.Models;

namespace WEBAPI.Class
{
    public class FileClass : IFile
    {
        public async Task<ServiceResponse<object>> uploadFile()
        {
            var service = new ServiceResponse<object>();
            try
            {

            }
            catch (Exception ex)
            {

                service.Data = ex.Message;
                service.ResponseCode = 500;
                service.Message = "Exception Error";
            }
            return service;
        }
    }
}
