using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
    }
}
