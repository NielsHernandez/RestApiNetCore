using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.api.Models.Domain;
using NZWalks.api.Models.DTO;
using NZWalks.api.Repositories;

namespace NZWalks.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        //new action method

        //POST: /api/Images/Upload

        [HttpPost]
        [Route("Upload")]

        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {

            ValidateFileUpload(request);

            //if any error on the model state

            if (ModelState.IsValid)
            {
               

                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription


                };
                //User repository to upload image

                await imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);


            }

            return BadRequest(ModelState);

        }

        //helper validate method

        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");

            }

            //validate file size in MB '10 MB'

            if (request.File.Length > 10485760) {

                ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller size file.");
            }

        }
    }
}
