using AutoMapper;
using ColleguesApi.Controllers.Dto;
using ColleguesApi.Models;

namespace ColleguesApi.Configurations
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<ColleguePostDto, Collegue>();
        }
    }
}
