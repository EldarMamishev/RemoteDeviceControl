using AutoMapper;
using Core.Entities.ApplicationIdentity;
using WebApi.Model.Auth;

namespace WebApi.ModelMapping
{
    public class RegisterProfile : Profile
    {
        public RegisterProfile()
        {
            CreateMap<Register, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}