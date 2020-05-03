using AutoMapper;
using Core.Entities.ApplicationIdentity;
using ViewModel.Auth;

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