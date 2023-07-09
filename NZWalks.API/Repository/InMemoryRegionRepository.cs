using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository
{
    public class InMemoryRegionRepository : IRegionRepository
    {
        public async Task<List<Region>> GetAllAsync()
        {
            return new List<Region>{
                new Region()
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    Code = "Code",
                    RegionImageUrl = "img.jpg"

                }
            };
        }
    }
}
