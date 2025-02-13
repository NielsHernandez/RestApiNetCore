using NZWalks.api.Models.Domain;

namespace NZWalks.api.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);


        Task<List<Walk>> GetAllAsync();


        Task<Walk?> GetByIdAsync(Guid id);
    }
}
