using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Models
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public int ResponseCode { get; set; }
        public string Message { get; set; }
    }
}
