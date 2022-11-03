using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IWebHostEnvironment _webHosting;
        private readonly IAdapter _adapter;

        public EmployeeController(IAdapter adapter, IWebHostEnvironment webHosting)
        {
            _adapter = adapter;
            _webHosting = webHosting;
        }
        //[HttpGet]
        //[Route("Getid/{id}")]
        //public async Task<IActionResult> GetID(int id)
        //{
        //    var adapter = await _adapter.employee.getbyID(id);
        //    return Ok(adapter);
        //}
        //[HttpPost]
        //[Route("employee")]
        //public async Task<IActionResult> insert([FromBody] EmployeeModel model)
        //{
        //    var adapter = await _adapter.employee.insert(model);
        //    return Ok(adapter);
        //}

        //[HttpPut]
        //[Route("UpdateEmployee/{id}")]
        //public async Task<IActionResult> update([FromBody] EmployeeModel model, int id)
        //{
        //    var adapter = await _adapter.employee.updatebyID(model, id);
        //    return Ok(adapter);
        //}

        //[HttpDelete]
        //[Route("Delete/{id}")]
        //public async Task<IActionResult> DeleteEmployee(int id)
        //{
        //    var adapter = await _adapter.employee.DeleteEmployee(id);
        //    return Ok(adapter);
        //}

        //[HttpGet]
        //[Route("GetAll")]
        //public async Task<IActionResult> GetAllData()
        //{
        //    var adapter = await _adapter.employee.GetAllData();
        //    return Ok(adapter);
        //}

        //[HttpPost]
        //[Route("UploadFile")]
        //public async Task<IActionResult> Uploadfile([FromForm] FileModel fmodel)
        //{
        //    try
        //    {
        //        if(fmodel.file.Length  > 0)
        //        {
        //            var path = _webHosting.WebRootPath + "\\images\\" ;
        //            if(!Directory.Exists(path))
        //            {
        //                Directory.CreateDirectory(path);
        //            }
        //            using (FileStream fileStream = System.IO.File.Create(path + fmodel.file.FileName))
        //            {
        //                fmodel.file.CopyTo(fileStream);
        //                fileStream.Flush();

        //                var imgpath = "\\images\\" + fmodel.file.FileName;

        //                return Ok("Successfully uploaded!");
        //            }
        //        }
        //        else
        //        {
        //            return Ok("Not uploaded");
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return Ok(ex.Message);
        //    }
        //}
    }
}
