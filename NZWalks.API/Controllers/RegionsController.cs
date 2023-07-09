using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _context;
        private readonly IRegionRepository _regionrepository;
        private readonly IMapper _mapper;

        public RegionsController(NZWalksDbContext context, IRegionRepository regionrepository, IMapper mapper)
        {
            this._context = context;
            this._regionrepository = regionrepository;
            this._mapper = mapper;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //get data from database --domain models
            var regionsDomain = await _regionrepository.GetAllAsync();
            //Map Domain models to DTOs
           /* var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto
                {
                    Id = regionDomain.Id,
                    Name = regionDomain.Name,
                    Code = regionDomain.Code,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }*/

            //map domain models to DTOs
            var regionsDto = _mapper.Map<List<RegionDto>>(regionsDomain);
            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = _context.Regions.Find(id);
            //get region domain model from database
            var regionDomain = await _regionrepository.GetByIdAsync(id);
            //
            if (regionDomain == null)
            {
                return NotFound();
            }
            //convert region domain model to region dto
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return Ok(regionDto);
        }

        //post method
        [HttpPost]
        public async  Task<IActionResult> Create([FromBody] AddRegionRequestDto addregionrequestdto)
        {
            var regionDomainModel = new Region
            {
                Code = addregionrequestdto.Code,
                Name = addregionrequestdto.Name,
                RegionImageUrl = addregionrequestdto.RegionImageUrl
            };
          regionDomainModel=  await _regionrepository.CreateAsync(regionDomainModel);

            //Map Domain model to Dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetById), new { Id = regionDto.Id },regionDto);
        }

        //put method
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateregionrequestdto )
        {
            //Map dto to domain model
            var regionDomianModel = new Region
            {
                Code = updateregionrequestdto.Code,
                Name = updateregionrequestdto.Name,
                RegionImageUrl = updateregionrequestdto.RegionImageUrl



            };

            regionDomianModel= await  _regionrepository.UpdateAsync(id, regionDomianModel);

            if (regionDomianModel == null)
            {
                return NotFound();
            }
          


            //convert domain model to dto
            var regionDto = new RegionDto
            {
                Id = regionDomianModel.Id,
                Code = regionDomianModel.Code,
                Name = regionDomianModel.Name,
                RegionImageUrl = regionDomianModel.RegionImageUrl
            };
            return Ok(regionDto);





        }


        //Delete Region
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel=await _regionrepository.DeleteAsync(id);
            if(regionDomainModel == null)
            {
                return NotFound();
            }
           
            //return delted region
            //map domain model to dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);
        }
       
    }
}
 