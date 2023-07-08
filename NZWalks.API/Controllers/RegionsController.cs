using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _context;
        public RegionsController(NZWalksDbContext context)
        {
            this._context = context;
        }



        [HttpGet]
        public IActionResult GetAll()
        {
            //get data from database --domain models
            var regionsDomain = _context.Regions.ToList();
            //Map Domain models to DTOs
            var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto
                {
                    Id = regionDomain.Id,
                    Name = regionDomain.Name,
                    Code = regionDomain.Code,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }
            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //var region = _context.Regions.Find(id);
            //get region domain model from database
            var regionDomain = _context.Regions.FirstOrDefault(r => r.Id == id);
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
        public IActionResult Create([FromBody] AddRegionRequestDto addregionrequestdto)
        {
            var regionDomainModel = new Region
            {
                Code = addregionrequestdto.Code,
                Name = addregionrequestdto.Name,
                RegionImageUrl = addregionrequestdto.RegionImageUrl
            };
            _context.Regions.Add(regionDomainModel);
            _context.SaveChanges();
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
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateregionrequestdto )
        {
            //check if region exist
            var regionDomianModel= _context.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDomianModel == null)
            {
                return NotFound();
            }
            //Map DTO to Domain Model
            regionDomianModel.Code = updateregionrequestdto.Code;
            regionDomianModel.Name = updateregionrequestdto.Name;
            regionDomianModel.RegionImageUrl=updateregionrequestdto.RegionImageUrl;
            _context.SaveChanges();


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
        public IActionResult Delete([FromRoute] Guid id)
        {
            var regionDomainModel=_context.Regions.FirstOrDefault(x=>x.Id==id);
            if(regionDomainModel == null)
            {
                return NotFound();
            }
            _context.Regions.Remove(regionDomainModel);
            _context.SaveChanges();
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
 