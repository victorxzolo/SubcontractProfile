using AutoMapper;
using DC = SubcontractProfile.WebApi.API.DataContracts;
using S = SubcontractProfile.WebApi.Services.Model;

namespace SubcontractProfile.WebApi.IoC.Configuration.AutoMapper.Profiles
{
    public class APIMappingProfile : Profile
    {
        public APIMappingProfile()
        {
            CreateMap<DC.User, S.User>().ReverseMap();
            CreateMap<DC.Address, S.Address>().ReverseMap();
        }
    }
}
