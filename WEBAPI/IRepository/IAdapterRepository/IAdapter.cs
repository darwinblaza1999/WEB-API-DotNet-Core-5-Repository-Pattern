using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.IRepository.IAdapterRepository
{
    public interface IAdapter
    {
        IEmployee employee { get; }
        IAuthenticate authenticate { get; }
        IUser user { get; }
    }
}
