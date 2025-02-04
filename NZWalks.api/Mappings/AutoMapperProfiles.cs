using AutoMapper;
using NZWalks.api.Models.Domain;
using NZWalks.api.Models.DTO;

namespace NZWalks.api.Mappings
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            //mapping from model Region to Dto Region

            CreateMap<Region, RegionDto>().ReverseMap();

            CreateMap<AddRegionRequestDto, Region>().ReverseMap();

            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
        }


    }
}
