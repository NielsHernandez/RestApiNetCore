using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.api.Data;
using NZWalks.api.Models.Domain;
using NZWalks.api.Models.DTO;



namespace NZWalks.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {

        private readonly NZWalksDbContext dbContext;

        public RegionController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //method to get all resutl
        // GET: https://localhost:portnumber/api/regions
        //make methos asyc

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {


        
            //get regions data from the db table 
            var regionsDomain = await dbContext.Regions.ToListAsync();

            //map domain models to the DTOs

            var regionsDto = new List<RegionDto>();

            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }

        
            

            return Ok(regionsDto);
        }

        //method to get one region at the time
        // GET: https://localhost:portnumber/api/regions/{id}


        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //getting data from DomainModel
            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            //mapping/converting data

            var regionDto = new RegionDto {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };






            return Ok(regionDto);
        }


        //method to post a new region
        // POST: https://localhost:portnumber/api/region

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map or Convert DTO to Domain Model

            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            //Use domain model to create a Region

            await dbContext.Regions.AddAsync(regionDomainModel);
            await dbContext.SaveChangesAsync();

            //Map domain model back to DTO

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };



            return CreatedAtAction(nameof(GetById), new {id = regionDto.Id}, regionDto);
        }

        //method to pust modify a region

        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)

        {

            //checks if a region id extis

            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            //it ends with an not found error
            if(regionDomainModel == null)
            {
                return NotFound();
            }

            // Map Dto to Domain model
            //usen same variable

            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            //save chagnges on the db

            await dbContext.SaveChangesAsync();

            // returning back the dto to the client 
            //convert domain model back to DTo

            var regionDto = new RegionDto {
                
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };



            return Ok(regionDto);
        }

        //delete region
        //DELETE: 

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            //find the region

            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomain == null)
            {
                return NotFound();

            }

            //if found delete

             dbContext.Regions.Remove(regionDomain);
            await dbContext.SaveChangesAsync();

            //mapping to dto

            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return Ok(regionDto);
        }

    }
}