using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public IActionResult GetAll()
        {


        
            //get regions data from the db table 
            var regionsDomain = dbContext.Regions.ToList();

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

        public IActionResult GetById([FromRoute] Guid id)
        {
            //getting data from DomainModel
            var regionDomain = dbContext.Regions.FirstOrDefault(r => r.Id == id);

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
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map or Convert DTO to Domain Model

            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            //Use domain model to create a Region

            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

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

    }
}