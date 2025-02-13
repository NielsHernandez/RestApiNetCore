using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.api.Models.Domain;
using NZWalks.api.Models.DTO;
using NZWalks.api.Repositories;

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

        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            await walkRepository.CreateAsync(walkDomainModel);

            //map to DTO before responding to the client

            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var walkDomainModel = await walkRepository.GetAllAsync();

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


    }
}
