using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.api.CustomActionFilters;
using NZWalks.api.Models.Domain;
using NZWalks.api.Models.DTO;
using NZWalks.api.Repositories;
using System.Threading.Tasks;

namespace NZWalks.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

      

        [HttpPost]
        [ValidateModel]

        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {

            //validate state of the model

            
                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

                await walkRepository.CreateAsync(walkDomainModel);

                //map to DTO before responding to the client

                return Ok(mapper.Map<WalkDto>(walkDomainModel));
          
        }

        [HttpGet]

        //?filterOn=Name string?  filterOn, [FromQuery] string? filterQuery

        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, 
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            int pageNumber = 1, int pageSize = 1000)
        {
            var walkDomainModel = await walkRepository.GetAllAsync(filterOn,filterQuery,sortBy, isAscending ?? true, pageNumber, pageSize);

            return Ok(mapper.Map<List<WalkDto>>(walkDomainModel));
        }


        // Get Wakl By Id
        // GET: /api/Walks/{id}

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {

            var walkDomainModel = await walkRepository.GetByIdAsync(id);

            if(walkDomainModel == null)
            {
                return NotFound();
            }

            //mapp domain model to dto

            return Ok(mapper.Map<WalkDto>(walkDomainModel));

        }


        // Update walk by id
        // Put: /api/Walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]

        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {

           

                var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);


                walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

                //check if null

                if (walkDomainModel == null) return NotFound();




                return Ok(mapper.Map<WalkDto>(walkDomainModel));

         
        }


        //Delete a walk by id

        //Delete: /api/walks/{id}

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Delete(Guid id)
        {

            var deleteWalkDomainModel = await walkRepository.DeleteAsync(id);

            if (deleteWalkDomainModel == null)
            { return NotFound(); }


            return Ok(mapper.Map<WalkDto>(deleteWalkDomainModel));

        }



    }
}
