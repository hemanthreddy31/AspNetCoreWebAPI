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
    }
}
/* var regions = new List<Region>
            {
                new Region
                {
                    Id=Guid.NewGuid(),
                    Name="Hyderabad",
                    Code="Hyd",
                    RegionImageUrl="https://www.holidify.com/images/bgImages/HYDERABAD.jpg"
                },
                  new Region
                {
                    Id=Guid.NewGuid(),
                    Name="Karimnagar",
                    Code="Knr",
                    RegionImageUrl="https://www.holidify.com/images/bgImages/HYDERABAD.jpg"
                }
            };*/