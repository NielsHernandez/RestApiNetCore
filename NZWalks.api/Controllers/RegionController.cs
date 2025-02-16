using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.api.CustomActionFilters;
using NZWalks.api.Data;
using NZWalks.api.Models.Domain;
using NZWalks.api.Models.DTO;
using NZWalks.api.Repositories;
using System.Diagnostics.Eventing.Reader;



namespace NZWalks.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {

        private readonly NZWalksDbContext dbContext;

        private readonly IRegionRepository regionRepository;

        private readonly IMapper mapper;

        public RegionController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        //method to get all resutl
        // GET: https://localhost:portnumber/api/regions
        //make methos asyc

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {


        
            //get regions data from the db table 
            var regionsDomain = await regionRepository.GetAllAsync();

            //map domain models to the DTOs

            //var regionsDto = new List<RegionDto>();
            /*
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }*/

            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

        
            

            return Ok(regionsDto);
        }

        //method to get one region at the time
        // GET: https://localhost:portnumber/api/regions/{id}


        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //getting data from DomainModel
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            //mapping/converting data

            /*var regionDto = new RegionDto {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };*/

            var regionDtoM = mapper.Map<RegionDto>(regionDomain);




            return Ok(regionDtoM);
        }


        //method to post a new region
        // POST: https://localhost:portnumber/api/region

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {


                //Map or Convert DTO to Domain Model

                /* var regionDomainModel = new Region
                 {
                     Code = addRegionRequestDto.Code,
                     Name = addRegionRequestDto.Name,
                     RegionImageUrl = addRegionRequestDto.RegionImageUrl
                 };*/

                var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

                //Use domain model to create a Region

                await regionRepository.CreateAsync(regionDomainModel);
                //await regionRepository.SaveChangesAsync();

                //Map domain model back to DTO

                /*var regionDto = new RegionDto
                {
                    Id = regionDomainModel.Id,
                    Code = regionDomainModel.Code,
                    Name = regionDomainModel.Name,
                    RegionImageUrl = regionDomainModel.RegionImageUrl
                };*/

                var regionDto = mapper.Map<RegionDto>(regionDomainModel);



            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);

           
        }

        //method to pust modify a region

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]

        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)

        {


            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);
            //checks if a region id extis

            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            //it ends with an not found error
            if(regionDomainModel == null)
            {
                return NotFound();
            }

            // Map Dto to Domain model
            //usen same variable

            //save chagnges on the db

            //await dbContext.SaveChangesAsync();

            // returning back the dto to the client 
            //convert domain model back to DTo
            /*
            var regionDto = new RegionDto {
                
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            */


            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);


        }

        //delete region
        //DELETE: 

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {

        

                var regionDomain = await regionRepository.DeleteAsync(id);

                if (regionDomain == null)
                {
                    return NotFound();

                }

                //mapping to dto

                /* var regionDto = new RegionDto
                 {
                     Id = regionDomain.Id,
                     Code = regionDomain.Code,
                     Name = regionDomain.Name,
                     RegionImageUrl = regionDomain.RegionImageUrl
                 };*/


                var regionDto = mapper.Map<RegionDto>(regionDomain);

                return Ok(regionDto);

            
            //find the region

            
        }

    }
}