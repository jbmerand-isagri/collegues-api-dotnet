using AutoMapper;
using collegues_api.Controllers.Dto;
using collegues_api.Models;

namespace collegues_api.Configurations
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<ColleguePostDto, Collegue>();
        }
    }
}
