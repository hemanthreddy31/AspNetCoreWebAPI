﻿using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Models.DTO.Region;
using NZWalks.API.Models.DTO.Walk;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            CreateMap<Walk,WalkDto>().ReverseMap();
            CreateMap<Difficulty,DifficultyDto>().ReverseMap();
            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
        }
    }
}
 