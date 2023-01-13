using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CitiesInfoWeb.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FileController:ControllerBase

    { private readonly FileExtensionContentTypeProvider _extensionContentTypeProvider;
        public FileController(FileExtensionContentTypeProvider extensionContentTypeProvider)
        {
            _extensionContentTypeProvider = extensionContentTypeProvider
                ?? throw new ArgumentNullException(nameof(extensionContentTypeProvider));
        }
    
        [HttpGet("fileId")]
        public ActionResult GetFile(string fileId)
        {
            var file = "File.xlsx";
            if(!System.IO.File.Exists(file))
            {
                return NotFound();
            }
            if(_extensionContentTypeProvider.TryGetContentType(file, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            var open = System.IO.File.ReadAllBytes(file);   
            return File(open, contentType, Path.GetFileName(file));
        }

    }
}
