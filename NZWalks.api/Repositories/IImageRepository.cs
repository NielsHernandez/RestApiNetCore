using NZWalks.api.Models.Domain;

namespace NZWalks.api.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
