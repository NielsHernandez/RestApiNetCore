using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalks.api.Data;
using NZWalks.api.Models.Domain;
using NZWalks.api.Models.DTO;

namespace NZWalks.api.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {

        private readonly NZWalksDbContext dbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext) 
        {

            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            // usar asycn programin

            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public  async Task<Region?> DeleteAsync(Guid id)
        {
            //sear for id

            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {
                return null;
            }

            dbContext.Regions.Remove(existingRegion);
            await dbContext.SaveChangesAsync();

          

            return existingRegion;


            //if found delete

            /*dbContext.Regions.Remove(regionDomain);
            await dbContext.SaveChangesAsync();

            //mapping to dto

            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };*/

        }


        //implementar el metodo

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
          
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {
                return null;
            }

            //map 

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;
         
            await dbContext.SaveChangesAsync();
            return existingRegion;



        }

    }
}
