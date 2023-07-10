using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this._mapper = mapper;
            this._walkRepository = walkRepository;
        }
        //create walks
        //POST/api/walks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            //Map dto to domain model
            var walkDomainModel = _mapper.Map<Walk>(addWalkRequestDto);
            await _walkRepository.CreateAsync(walkDomainModel);
            //map domain model to dto

            return Ok(_mapper.Map<WalkDto>(walkDomainModel));

            
        }

        //Getall

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walkDomainModel = await _walkRepository.GetAllAsync();
            return Ok(_mapper.Map<List<WalkDto>>(walkDomainModel));
        }


        [HttpGet]
        //Walk by id
        //GET:/api/walks/{id}
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await _walkRepository.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<WalkDto>(walkDomainModel));

        }
    }
}
