using Microsoft.AspNetCore.Hosting;
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
        public IAuthenticate authenticate { get; }
        public IUser user { get; }

        public AdapterClass(IConfiguration config, IWebHostEnvironment webHosting)
        {
            employee = new EmployeeClass(config);

            authenticate = new AuthenticationClass(config);

            user = new UserClass(config, webHosting);
        }
        
    }
}
