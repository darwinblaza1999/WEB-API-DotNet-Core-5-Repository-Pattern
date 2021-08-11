using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI.Class;
using WEBAPI.IRepository;
using WEBAPI.IRepository.IAdapterRepository;

namespace WEBAPI.Adapter
{
    public class AdapterClass : IAdapter
    {
        public IEmployee employee { get; }

        public AdapterClass(IConfiguration config)
        {
            employee = new EmployeeClass(config);
        }
    }
}
