using EasySales.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EasySales.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FilesaveController : ControllerBase
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public FilesaveController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<ActionResult<UploadResult>> PostFile(IFormFile file)
        {
            var maxAllowedFiles = 1;
            long maxFileSize = 1024 * 1024;
            var filesProcessed = 0;
            var resourcePath = new Uri($"{Request.Scheme}://{Request.Host}/");
            UploadResult uploadResult = new();

            string trustedFileNameForFileStorage;
            var untrustedFileName = file.FileName;
            uploadResult.FileName = untrustedFileName;
            var trustedFileNameForDisplay =
                WebUtility.HtmlEncode(untrustedFileName);

            if (filesProcessed < maxAllowedFiles)
            {
                if (file.Length == 0)
                {
                    uploadResult.Message = "{FileName} length is 0 (Err: 1)" + trustedFileNameForDisplay;
                    uploadResult.ErrorCode = 1;
                }
                else if (file.Length > maxFileSize)
                {
                    uploadResult.Message = "{FileName} of {Length} bytes is " +
                        "larger than the limit of {Limit} bytes (Err: 2) " +
                        trustedFileNameForDisplay + file.Length + maxFileSize;
                    uploadResult.ErrorCode = 2;
                }
                else
                {
                    try
                    {
                        trustedFileNameForFileStorage = Path.GetRandomFileName();
                        //var path = Path.Combine(webHostEnvironment.ContentRootPath,
                        //    webHostEnvironment.EnvironmentName, "Files/FotosProductos",
                        //    trustedFileNameForFileStorage); // Agregando directorio por enviroment.
                        //var path = Path.Combine(webHostEnvironment.ContentRootPath, "wwwroot/Files/FotosProductos",
                        //    trustedFileNameForFileStorage);//Usando nombre seguro de almacenaje, no usado porque no se utiliza el nombre brindado por el usuario.                     
                        var path = Path.Combine(webHostEnvironment.ContentRootPath, "wwwroot/Files/FotosProductos",
                            file.FileName);
                        await using FileStream fs = new(path, FileMode.Create);
                        await file.CopyToAsync(fs);

                        //uploadResult.Message = "{FileName} saved at {Path} " +
                        //    trustedFileNameForDisplay + path;//Usando nombre seguro de almacenaje, no usado porque no se utiliza el nombre brindado por el usuario.                                             
                        uploadResult.Message = "{FileName} saved at {Path} " +
                            file.FileName + path;

                        //uploadResult.StoredFileName = trustedFileNameForFileStorage;//Usando nombre seguro de almacenaje, no usado porque no se utiliza el nombre brindado por el usuario.                     
                        uploadResult.Uploaded = true;
                        uploadResult.StoredFileName = file.FileName;
                    }
                    catch (IOException ex)
                    {
                        uploadResult.Message = "{FileName} error on upload (Err: 3): {Message}" +
                            trustedFileNameForDisplay + ex.Message;
                        uploadResult.ErrorCode = 3;
                    }
                }

                filesProcessed++;
            }
            else
            {
                uploadResult.Message = "{FileName} not uploaded because the " +
                    "request exceeded the allowed {Count} of files (Err: 4)" +
                    trustedFileNameForDisplay + maxAllowedFiles;
                uploadResult.ErrorCode = 4;
            }

            return new CreatedResult(resourcePath, uploadResult);
        }
    }
}
