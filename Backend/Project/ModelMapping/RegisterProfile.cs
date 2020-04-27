using AutoMapper;
using Core.Entities.Users;
using WebApi.Model.Auth;

namespace WebApi.ModelMapping
{
    public class RegisterProfile : Profile
    {
        public RegisterProfile()
        {
            CreateMap<Register, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}