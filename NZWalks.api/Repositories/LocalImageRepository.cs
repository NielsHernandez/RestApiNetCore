using NZWalks.api.Data;
using NZWalks.api.Models.Domain;

namespace NZWalks.api.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostenvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalksDbContext dbContext;

        //injection IWebhostenvironment on the Constructor
        public LocalImageRepository(IWebHostEnvironment webHostenvironment, IHttpContextAccessor httpContextAccessor, NZWalksDbContext dbContext)
        {
            this.webHostenvironment = webHostenvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }

        

        public async Task<Image> Upload(Image image)
        {
            //throw new NotImplementedException();
            //get the path to store the file
            var localFilePath = Path.Combine(webHostenvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

            // Upload image to local path

            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            // serving file to url https://LocalHost:1234/images/image.jpg

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;

            // Add Image to the Images table.

            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;



        }
    }
}
