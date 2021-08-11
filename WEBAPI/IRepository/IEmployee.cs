using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI.Models;

namespace WEBAPI.IRepository
{
    public interface IEmployee
    {
        Task<ServiceResponse<object>> getbyID(int id);

        Task<ServiceResponse<object>> insert(EmployeeModel model);

        Task<ServiceResponse<object>> updatebyID(EmployeeModel model, int id);
        Task<ServiceResponse<object>> DeleteEmployee(int id);
    }
}
