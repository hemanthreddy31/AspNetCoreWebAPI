using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

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
            var regions = _context.Regions.ToList();
            return Ok(regions);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //var region = _context.Regions.Find(id);
            var region = _context.Regions.FirstOrDefault(r => r.Id == id);
            if (region == null)
            {
                return NotFound();
            }
            return Ok(region);
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