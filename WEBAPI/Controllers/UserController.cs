using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using WEBAPI.IRepository.IAdapterRepository;
using WEBAPI.Models;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : BaseController
    {
        private readonly IAdapter _adapter;
        private readonly IWebHostEnvironment _webHosting;

        public UserController(IAdapter adapter, IWebHostEnvironment webHosting)
        {
            _adapter = adapter;
            _webHosting = webHosting;
        }
        [HttpPost]
        [Route("InsertUser")]
        public async Task<IActionResult> Insertuser(IFormFile Upload, [FromForm] UserModel model)
        {
            //var img = string.Empty;
            //if(Upload != null)
            //{
            //    if (Upload.Length > 0)
            //    {
            //        var path = _webHosting.WebRootPath + "\\images\\";
            //        if (!Directory.Exists(path))
            //        {
            //            Directory.CreateDirectory(path);
            //        } 
            //        using (FileStream fileStream = System.IO.File.Create(path + Upload.FileName))
            //        {
            //            Upload.CopyTo(fileStream);
            //            fileStream.Flush();

            //            img = $"{this.Request.Scheme}://{this.Request.Host}/images/{Upload.FileName}";
            //        }
            //    }
            //}
            //else
            //{
            //    img = "";
            //}

            var adapter = await _adapter.user.insertUser(model);

            return Ok(adapter);
        }
        [HttpPost]
        [Route("UpdateUser/{id}")]
        public async Task<IActionResult> userUpdate(IFormFile Upload,[FromForm] UserModel model, int id)
        {
            string photo = string.Empty;
            if (Upload != null)
            {
                if (Upload.Length > 0)
                {
                    var path = _webHosting.WebRootPath + "\\images\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + Upload.FileName))
                    {
                        Upload.CopyTo(fileStream);
                        fileStream.Flush();

                        photo = $"{this.Request.Scheme}://{this.Request.Host}/images/{Upload.FileName}";
                    }
                }
            }
            else
            {
                photo = "";
            }
            var adapter = await _adapter.user.updateUser(model, photo, id);

            return Ok(adapter);
        }
    }
}
