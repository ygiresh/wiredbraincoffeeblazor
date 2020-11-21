using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WiredBrainCoffee.Models;

namespace WiredBrainCoffee.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public ContactController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("file")]
        public IActionResult OnPostUploadAsync()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("StaticFiles", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(dbPath);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

            
        }

        [HttpPost("image")]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            string uploadsFolder = Path.Combine(webHostEnvironment.ContentRootPath, "images");
            string filePath = Path.Combine(uploadsFolder, image.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }

            return Ok();
        }

        [HttpPost("upload")]
        public void Post(UploadedFile uploadedFile)
        {
            var path = $"{webHostEnvironment.ContentRootPath}\\{uploadedFile.FileName}";
            var fs = System.IO.File.Create(path);
            fs.Write(uploadedFile.FileContent, 0, uploadedFile.FileContent.Length);
            fs.Close();
        }
    }
}
