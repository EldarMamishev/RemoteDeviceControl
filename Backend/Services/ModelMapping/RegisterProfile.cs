using AutoMapper;
using Core.Entities.ApplicationIdentity;
using ViewModel.Auth;

namespace Services.ModelMapping
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