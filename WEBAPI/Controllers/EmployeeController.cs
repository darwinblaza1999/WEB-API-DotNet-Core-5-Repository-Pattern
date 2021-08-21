using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI.Class;
using WEBAPI.IRepository.IAdapterRepository;
using WEBAPI.Models;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class EmployeeController : BaseController
    {
        private readonly IAdapter _adapter; 
        public EmployeeController(IAdapter adapter)
        {
            _adapter = adapter;
        }
        [HttpGet]
        [Route("Getid/{id}")]
        public async Task<IActionResult> GetID(int id)
        {
            var adapter = await _adapter.employee.getbyID(id);
            return Ok(adapter);
        }
        [HttpPost]
        [Route("employee")]
        public async Task<IActionResult> insert([FromBody] EmployeeModel model)
        {
            var adapter = await _adapter.employee.insert(model);
            return Ok(adapter);
        }

        [HttpPut]
        [Route("UpdateEmployee/{id}")]
        public async Task<IActionResult> update([FromBody] EmployeeModel model, int id)
        {
            var adapter = await _adapter.employee.updatebyID(model,id);
            return Ok(adapter);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var adapter = await _adapter.employee.DeleteEmployee(id);
            return Ok(adapter);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllData()
        {
            var adapter = await _adapter.employee.GetAllData();
            return Ok(adapter);
        }
    }
}
